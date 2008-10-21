// ****************************************************************************
// 
// Copyright (C) 2005-2008  Doom9 & al
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// 
// ****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MeGUI
{
    public enum ResizeFilterType
    {

        [EnumTitle("Bilinear (Soft)", "BilinearResize({0},{1})")]
        Bilinear=0,
        [EnumTitle("Bicubic (Sharp)", "BicubicResize({0},{1},0,0.75)")]
        BicubicSharp,
        [EnumTitle("Bicubic (Neutral)", "BicubicResize({0},{1},0,0.5)")]
        BicubicNeutral,
        [EnumTitle("Bicubic (Soft)", "BicubicResize({0},{1},0.333,0.333)")]
        BicubicSoft,
        [EnumTitle("Lanczos (Sharp)", "LanczosResize({0},{1})")]
        Lanczos,
        [EnumTitle("Lanczos4 (Sharp)", "Lanczos4Resize({0},{1})")]
        Lanczos4,
        [EnumTitle("Gauss (Neutral)", "GaussResize({0},{1})")]
        Gauss,
        [EnumTitle("Point (Sharp)", "PointResize({0},{1})")]
        Point,
        [EnumTitle("Spline16 (Neutral)", "Spline16Resize({0},{1})")]
        Spline16,
        [EnumTitle("Spline36 (Neutral)", "Spline36Resize({0},{1})")]
        Spline32

    }

    public enum DenoiseFilterType
    {

        [EnumTitle("Minimal Noise", "Undot()")]
        MinimalNoise = 0,
        [EnumTitle("Little Noise", "mergechroma(blur(1.3))")]
        LittleNoise,
        [EnumTitle("Medium Noise", "FluxSmoothST(7,7)")]
        MediumNoise,
        [EnumTitle("Heavy Noise", "Convolution3D(\"movielq\")")]
        HeavyNoise
    }

    public enum UserSourceType
    {
        [EnumTitle("Progressive", SourceType.PROGRESSIVE)]
        Progressive,
        [EnumTitle("Interlaced", SourceType.INTERLACED)]
        Interlaced,
        [EnumTitle("Film", SourceType.FILM)]
        Film,
        [EnumTitle("M-in-5 decimation required", SourceType.DECIMATING)]
        Decimating,
        [EnumTitle("Hybrid film/interlaced. Mostly film", SourceType.HYBRID_FILM_INTERLACED)]
        HybridFilmInterlaced,
        [EnumTitle("Hybrid film/interlaced. Mostly interlaced", SourceType.HYBRID_FILM_INTERLACED)]
        HybridInterlacedFilm,
        [EnumTitle("Partially interlaced", SourceType.HYBRID_PROGRESSIVE_INTERLACED)]
        HybridProgressiveInterlaced,
        [EnumTitle("Partially film", SourceType.HYBRID_PROGRESSIVE_FILM)]
        HybridProgressiveFilm
    }

    public enum UserFieldOrder
    {
        [EnumTitle("Top Field First", FieldOrder.TFF)]
        TFF,
        [EnumTitle("Bottom Field First", FieldOrder.BFF)]
        BFF,
        [EnumTitle("Varying field order", FieldOrder.VARIABLE)]
        Varying
    }

    public class ScriptServer
    {
        public static readonly IList ListOfResizeFilterType = EnumProxy.CreateArray(typeof(ResizeFilterType));

        public static readonly IList ListOfDenoiseFilterType = EnumProxy.CreateArray(typeof(DenoiseFilterType));

        public static readonly IList ListOfSourceTypes = EnumProxy.CreateArray(typeof(UserSourceType));

        public static readonly IList ListOfFieldOrders = EnumProxy.CreateArray(typeof(UserFieldOrder));

        public static string CreateScriptFromTemplate(string template, string inputLine, string cropLine, string resizeLine, string denoiseLines, string deinterlaceLines)
        {
            string newScript = template;
            newScript = newScript.Replace("<crop>", cropLine);
            newScript = newScript.Replace("<resize>", resizeLine);
            newScript = newScript.Replace("<denoise>", denoiseLines);
            newScript = newScript.Replace("<deinterlace>", deinterlaceLines);
            newScript = newScript.Replace("<input>", inputLine);
            return newScript;
        }

        public static string GetInputLine(string input, bool interlaced, PossibleSources sourceType,
            bool colormatrix, bool mpeg2deblock, bool flipVertical, double fps)
        {
            string inputLine = "#input";
      //      FileInfo fi = new FileInfo(input);
       //     long size = fi.Length;

            switch (sourceType)
            {
                case PossibleSources.d2v:
                    inputLine = "DGDecode_mpeg2source(\"" + input + "\"";
                    if (mpeg2deblock)
                        inputLine += ",cpu=4";
                    if (colormatrix)
                        inputLine += ",info=3";
                    inputLine += ")";
                    if (colormatrix)
                        inputLine += string.Format("\r\nColorMatrix(hints=true{0})", interlaced ? ",interlaced=true" : "");
                    break;
                case PossibleSources.vdr:
                        inputLine = "AVISource(\"" + input + "\", audio=false)";
                    break;
                case PossibleSources.directShow:
                    if (input.ToLower().EndsWith(".avi"))
                    {
                        if (input.Length >= 268435456) // 1GB = 134217728 bytes
                            inputLine = "OpenDMLSource(\"" + input + "\", audio=false)";
                        else
                            inputLine = "AVISource(\"" + input + "\", audio=false)";
                    }
                    else
                    {
                        inputLine = "DirectShowSource(\"" + input + "\"" + ((fps > 0) ? ",fps=" + fps.ToString(new CultureInfo("en-us")) : string.Empty) + ",audio=false)";
                        if (flipVertical)
                            inputLine = inputLine + "\r\nFlipVertical()";
                    }
                    break;
            }
            return inputLine;
        }

        public static string GetCropLine(bool crop, CropValues cropValues)
        {
            return GetCropLine(crop, cropValues.left, cropValues.top, cropValues.right, cropValues.bottom);
        }

        public static string GetCropLine(bool crop, int cropLeft, int cropTop, int cropRight, int cropBottom)
        {
            string cropLine = "#crop";
            if (crop)
            {
                cropLine = string.Format("crop( {0}, {1}, {2}, {3}){4}",cropLeft, cropTop, -cropRight, -cropBottom, Environment.NewLine );
            }
            return cropLine;
        }

        public static string GetResizeLine(bool resize, int hres, int vres, ResizeFilterType type)
        {
            if (!resize)
                return "#resize";
            EnumProxy p = EnumProxy.Create(type);
            if (p.Tag != null)
                return string.Format(p.Tag + " # {2}", hres, vres, p);
            else
                return "#resize - " + p;
        }

        public static string GetDenoiseLines(bool denoise, DenoiseFilterType type)
        {
            string denoiseLines = "#denoise";
            if (denoise)
            {
                EnumProxy p = EnumProxy.Create(type);
                if (p.Tag != null)
                    denoiseLines = string.Format(p.Tag + " # " + p);
                else
                    denoiseLines = "#denoise - " + p;
            }
            return denoiseLines;
        }

        public static List<DeinterlaceFilter> GetDeinterlacers(SourceInfo info)
        {
            List<DeinterlaceFilter> filters = new List<DeinterlaceFilter>();
            if (info.sourceType == SourceType.PROGRESSIVE)
            {
                filters.Add(new DeinterlaceFilter(
                    "Do nothing",
                    "#Not doing anything because the source is progressive"));
            }
            else if (info.sourceType == SourceType.DECIMATING)
            {
                ScriptServer.AddTDecimate(info.decimateM, filters);
            }
            else if (info.sourceType == SourceType.INTERLACED)
            {
                ScriptServer.AddYadif(info.fieldOrder, filters);
                ScriptServer.AddTDeint(info.fieldOrder, filters, true, false);
                ScriptServer.AddTDeint(info.fieldOrder, filters, true, true);
                if (info.fieldOrder != FieldOrder.VARIABLE)
                    ScriptServer.AddLeakDeint(info.fieldOrder, filters);
                ScriptServer.AddTMC(info.fieldOrder, filters);
                ScriptServer.AddFieldDeint(info.fieldOrder, filters, true, true);
                ScriptServer.AddFieldDeint(info.fieldOrder, filters, true, false);
            }
            else if (info.sourceType == SourceType.FILM)
            {
                ScriptServer.AddTIVTC("", info.isAnime, false, true, false, info.fieldOrder, filters);
                ScriptServer.AddIVTC(info.fieldOrder, false, true, filters);
            }
            else if (info.sourceType == SourceType.HYBRID_FILM_INTERLACED ||
                info.sourceType == SourceType.HYBRID_PROGRESSIVE_FILM)
            {
                ScriptServer.AddTIVTC("", info.isAnime, true, info.majorityFilm, false,
                    info.fieldOrder, filters);
                ScriptServer.AddTIVTC("", info.isAnime, true, info.majorityFilm, true,
                    info.fieldOrder, filters);
                ScriptServer.AddIVTC(info.fieldOrder, true, info.majorityFilm, filters);
            }
            else if (info.sourceType == SourceType.HYBRID_PROGRESSIVE_INTERLACED)
            {
                ScriptServer.AddYadif(info.fieldOrder, filters);
                ScriptServer.AddTDeint(info.fieldOrder, filters, false, false);
                ScriptServer.AddTDeint(info.fieldOrder, filters, false, true);
                ScriptServer.AddFieldDeint(info.fieldOrder, filters, false, true);
                ScriptServer.AddFieldDeint(info.fieldOrder, filters, false, false);
                if (info.fieldOrder != FieldOrder.VARIABLE)
                    ScriptServer.AddLeakDeint(info.fieldOrder, filters);
                ScriptServer.AddTMC(info.fieldOrder, filters);
            }
            return filters;
        }

        #region deinterlacing snippets
        public static int Order(FieldOrder order)
        {
            int i_order = -1;
            if (order == FieldOrder.BFF)
                i_order = 0;
            if (order == FieldOrder.TFF)
                i_order = 1;
            return i_order;
        }

        public static void AddYadif(FieldOrder order, List<DeinterlaceFilter> filters)
        {
            filters.Add(new DeinterlaceFilter(
                "Yadif",
                string.Format("Load_Stdcall_Plugin(\"{0}\"){1}Yadif(order={2})", 
                    MainForm.Instance.Settings.YadifPath, Environment.NewLine,
                    Order(order))));
        }

        public static void AddLeakDeint(FieldOrder order, List<DeinterlaceFilter> filters)
        {
            filters.Add(new DeinterlaceFilter(
                "LeakKernelDeint",
                string.Format("LeakKernelDeint(order={0},sharp=true)", Order(order))));
        }

        public static void AddTDeint(FieldOrder order, List<DeinterlaceFilter> filters, bool processAll, bool eedi2)
        {
            StringBuilder script = new StringBuilder();
            if (eedi2)
            {
                script.Append("edeintted = last.");
                if (order == FieldOrder.TFF)
                    script.Append("AssumeTFF().");
                else if (order == FieldOrder.BFF)
                    script.Append("AssumeBFF().");
                script.Append("SeparateFields().SelectEven().EEDI2(field=-1)\r\n");
            }
            script.Append("TDeint(");
            

            if (order != FieldOrder.VARIABLE)
                script.Append("order=" + Order(order) + ",");
            if (!processAll) // For hybrid clips
                script.Append("full=false,");
            if (eedi2)
                script.Append("edeint=edeintted,");

            script = new StringBuilder(script.ToString().TrimEnd(new char[] { ',' }));
            script.Append(")");
            filters.Add(new DeinterlaceFilter(
                eedi2 ? "TDeint (with EDI)" : "TDeint",
                script.ToString()));
        }

        public static void AddFieldDeint(FieldOrder order, List<DeinterlaceFilter> filters, bool processAll, bool blend)
        {
            string name = "FieldDeinterlace";
            if (!blend)
                name = "FieldDeinterlace (no blend)";

            StringBuilder script = new StringBuilder();
            if (order == FieldOrder.TFF)
                script.Append("AssumeTFF().");
            else if (order == FieldOrder.BFF)
                script.Append("AssumeBFF().");

            script.Append("FieldDeinterlace(");
            
            if (!blend)
                script.Append("blend=false");

            if (!processAll)
            {
                if (!blend)
                    script.Append(",");
                script.Append("full=false");
            }
            script.Append(")");
            filters.Add(new DeinterlaceFilter(
                name,
                script.ToString()));
        }

        public static void AddTMC(FieldOrder order, List<DeinterlaceFilter> filters)
        {
            filters.Add(new DeinterlaceFilter(
                "TomsMoComp",
                string.Format("TomsMoComp({0},5,1)", Order(order))));
        }

        public static void Portionize(List<DeinterlaceFilter> filters, string trimLine)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                string script = filters[i].Script;
                StringBuilder newScript = new StringBuilder();
                newScript.AppendLine("original = last");
                newScript.Append("deintted = original.");
                newScript.AppendLine(script);
                newScript.Append(trimLine);
                filters[i].Script = newScript.ToString();
            }
        }

        #endregion
        #region IVTC snippets
        public static void AddTIVTC(string d2vFile, bool anime, bool hybrid, bool mostlyFilm, bool advancedDeinterlacing,
            FieldOrder fieldOrder, List<DeinterlaceFilter> filters)
        {
            StringBuilder script = new StringBuilder();
            if (advancedDeinterlacing)
            {
                script.Append("edeintted = ");
                if (fieldOrder == FieldOrder.TFF)
                    script.Append("AssumeTFF().");
                else if (fieldOrder == FieldOrder.BFF)
                    script.Append("AssumeBFF().");
                script.AppendFormat("SeparateFields().SelectEven().EEDI2(field=-1)\r\n");
                script.Append("tdeintted = TDeint(edeint=edeintted");
                if (fieldOrder != FieldOrder.VARIABLE)
                    script.Append(",order=" + Order(fieldOrder));
                script.Append(")\r\n");
            }

            script.Append("tfm(");
            if (d2vFile.Length <= 0)
                script.AppendFormat("order={0}", Order(fieldOrder));
            if (advancedDeinterlacing)
            {
                if (d2vFile.Length <= 0)
                    script.Append(",");
                script.Append("clip2=tdeintted");
            }
            script.Append(")");

            script.Append(".tdecimate(");
            if (anime)
                script.Append("mode=1");
            if (hybrid)
            {
                if (anime)
                    script.Append(",");
                if (mostlyFilm)
                    script.Append("hybrid=1");
                else
                    script.Append("hybrid=3");
                
            }
            script.Append(")");
            filters.Add(new DeinterlaceFilter(
                advancedDeinterlacing ? "TIVTC + TDeint(EDI) -- slow" : "TIVTC",
                script.ToString()));
        }

        public static void AddIVTC(FieldOrder order, bool hybrid, bool mostlyFilm,
            List<DeinterlaceFilter> filters)
        {
            StringBuilder script = new StringBuilder();
            if (order == FieldOrder.TFF)
                script.Append("AssumeTFF().");
            else if (order == FieldOrder.BFF)
                script.Append("AssumeBFF().");

            script.Append("Telecide(guide=1).Decimate(");

            if (hybrid)
            {
                if (mostlyFilm)
                    script.Append("mode=3,");
                else
                    script.Append("mode=1,");

                script.Append("threshold=2.0");
            }

            script.Append(")");
            filters.Add(new DeinterlaceFilter(
                "Decomb IVTC",
                script.ToString()));
        }


        #endregion
        #region decimate snippet
        public static void AddTDecimate(int decimateM, List<DeinterlaceFilter> filters)
        {
            filters.Add(new DeinterlaceFilter(
                "Tritical Decimate",
                string.Format("TDecimate(cycleR={0})", decimateM)));
        }
        #endregion
        #region analysis scripting
        private const string DetectionScript =
@"{0} #original script
{1} #trimming
global unused_ = blankclip(pixel_type=""yv12"", length=10).TFM()
file=""{2}""
global sep=""-""
function IsMoving() {{
  global b = (diff < 1.0) ? false : true}}
ConvertToYV12()
c = last
global clip = last
c = WriteFile(c, file, ""a"", ""sep"", ""b"")
c = FrameEvaluate(c, ""global a = IsCombedTIVTC(clip, cthresh=9)"")
c = FrameEvaluate(c, ""IsMoving"")
c = FrameEvaluate(c,""global diff = 0.50*YDifferenceFromPrevious(clip) + 0.25*UDifferenceFromPrevious(clip) + 0.25*VDifferenceFromPrevious(clip)"")
crop(c,0,0,16,16)
SelectRangeEvery({3},{4},0)";

        private const string FieldOrderScript =
@"{0} # original script
{1} #trimming
file=""{2}""
global sep=""-""
ConvertToYV12()
d = last
global abff = d.assumebff().separatefields()
global atff = d.assumetff().separatefields()
c = d.loop(2)
c = WriteFile(c, file, ""diffa"", ""sep"", ""diffb"")
c = FrameEvaluate(c,""global diffa = 0.50*YDifferenceFromPrevious(abff) + 0.25*UDifferenceFromPrevious(abff) + 0.25*VDifferenceFromPrevious(abff)"")
c = FrameEvaluate(c,""global diffb = 0.50*YDifferenceFromPrevious(atff) + 0.25*UDifferenceFromPrevious(atff) + 0.25*VDifferenceFromPrevious(atff)"")
crop(c,0,0,16,16)
SelectRangeEvery({3},{4},0)
";

        public static string getScript(int scriptType, string originalScript, string trimLine, string logFileName, int selectEvery, int selectLength)
        {
            if (scriptType == 0) // detection
                return string.Format(DetectionScript, originalScript, trimLine, logFileName, selectEvery, selectLength);
            else if (scriptType == 1) // field order
                return string.Format(FieldOrderScript, originalScript, trimLine, logFileName, selectEvery, selectLength);
            else
                return null;
        }
        #endregion

        public static void undercrop(ref CropValues crop)
        {
            if (crop.left % 2 != 0 && crop.top % 2 != 0 && crop.bottom % 2 != 0 && crop.right % 2 != 0)
                throw new Exception("Cropping by odd numbers not supported in undercropping to mod16");

            while ((crop.left + crop.right) % 16 > 0)
            {
                if (crop.left > crop.right)
                {
                    if (crop.left > 1)
                        crop.left -= 2;
                    else
                        crop.left = 0;
                }
                else
                {
                    if (crop.right > 1)
                        crop.right -= 2;
                    else
                        crop.right = 0;
                }
            }
            while ((crop.top + crop.bottom) % 16 > 0)
            {
                if (crop.top > crop.bottom)
                {
                    if (crop.top > 1)
                        crop.top -= 2;
                    else
                        crop.top = 0;
                }
                else
                {
                    if (crop.bottom > 1)
                        crop.bottom -= 2;
                    else
                        crop.bottom = 0;
                }
            }
        }

        public static void overcrop(ref CropValues crop)
        {
            if (crop.left % 2 != 0 && crop.top % 2 != 0 && crop.bottom % 2 != 0 && crop.right % 2 != 0)
                throw new Exception("Cropping by odd numbers not supported in overcropping to mod16");

            bool doLeftNext = true;
            while ((crop.left + crop.right) % 16 != 0)
            {
                if (doLeftNext)
                    crop.left += 2;
                else
                    crop.right += 2;
                doLeftNext = !doLeftNext;
            }

            bool doTopNext = true;
            while ((crop.top + crop.bottom) % 16 != 0)
            {
                if (doTopNext)
                    crop.top += 2;
                else
                    crop.bottom += 2;
                doTopNext = !doTopNext;
            }
        }

        public static void cropMod4Horizontal(ref CropValues crop)
        {
            if (crop.left % 2 != 0 && crop.top % 2 != 0 && crop.bottom % 2 != 0 && crop.right % 2 != 0)
                throw new Exception("Cropping by odd numbers not supported in mod4 horizontal cropping");
            while ((crop.left + crop.right) % 4 > 0)
            {
                if (crop.left > crop.right)
                {
                    if (crop.left > 1)
                    {
                        crop.left -= 2;
                    }
                    else
                    {
                        crop.left = 0;
                    }
                }
                else
                {
                    if (crop.right > 1)
                    {
                        crop.right -= 2;
                    }
                    else
                    {
                        crop.right = 0;
                    }
                }
            }
        }

    }
}
