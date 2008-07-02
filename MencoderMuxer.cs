using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MeGUI.core.util;
using MeGUI.core.details;

namespace MeGUI
{
    class MencoderMuxer : CommandlineMuxer
    {
        public MencoderMuxer(string executablePath)
        {
            this.executable = executablePath;
        }

        /// <summary>
        /// gets the framenumber from an mencoder status update line
        /// </summary>
        /// <param name="line">mencoder stdout line</param>
        /// <returns>the framenumber included in the line</returns>
        public ulong? getFrameNumber(string line)
        {
            try
            {
                int frameNumberStart = line.IndexOf("s", 4) + 1;
                int frameNumberEnd = line.IndexOf("f");
                string frameNumber = line.Substring(frameNumberStart, frameNumberEnd - frameNumberStart).Trim();
                return ulong.Parse(frameNumber);
            }
            catch (Exception e)
            {
                log.LogValue("Exception in getFrameNumber(" + line + ")", e, ImageType.Warning);
                return null;
            }
        }

        public override void ProcessLine(string line, StreamType stream)
        {
            if (line.StartsWith("Pos:")) // status update
            {
                su.NbFramesDone = getFrameNumber(line);
                return;
            }
            
            if (line.IndexOf("error") != -1)
            {
                log.LogValue("An error occurred", line, ImageType.Error);
                su.HasError = true;
            }
            else if (line.IndexOf("not an MEncoder option") != -1)
            {
                log.LogValue("Unrecognized commandline parameter", line, ImageType.Error);
                su.HasError = true;
            }

            base.ProcessLine(line, stream);
        }

        protected override string Commandline
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                MuxSettings settings = job.Settings;

                sb.Append("-ovc copy -oac copy ");
                if (settings.MuxedInput.Length > 0)
                {
                    sb.Append("\"" + settings.MuxedInput + "\" ");
                }
                if (settings.VideoInput.Length > 0)
                {
                    sb.Append("\"" + settings.VideoInput + "\" ");
                }
                if (settings.AudioStreams.Count > 0)
                {
                    MuxStream stream = (MuxStream)settings.AudioStreams[0];
                    sb.Append("-audiofile \"" + stream.path + "\" ");
                }
                sb.Append(" -mc 0 -noskip -o \"" + settings.VideoInput + "\"");
                return sb.ToString();
            }
        }
    }
}
