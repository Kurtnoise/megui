﻿// ****************************************************************************
// 
// Copyright (C) 2005-2013 Doom9 & al
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using MeGUI.core.util;

namespace MeGUI
{
    public class DGAIndexer : CommandlineJobProcessor<DGAIndexJob>
    {
public static readonly JobProcessorFactory Factory =
            new JobProcessorFactory(new ProcessorFactory(init), "DGAIndexer");

        private static IJobProcessor init(MainForm mf, Job j)
        {
            if (j is DGAIndexJob) return new DGAIndexer(mf.Settings.DGAVCIndex.Path);
            return null;
        }

        public DGAIndexer(string executableName)
        {
            UpdateCacher.CheckPackage("dgavcindex");
            executable = executableName;
        }

        public override void ProcessLine(string line, StreamType stream, ImageType oType)
        {
            if (Regex.IsMatch(line, "^[0-9]{1,3}$", RegexOptions.Compiled))
                su.PercentageDoneExact = Int32.Parse(line);
            else
                base.ProcessLine(line, stream, oType);
        }

        protected override void checkJobIO()
        {
            try
            {
                if (!String.IsNullOrEmpty(job.Output))
                    FileUtil.ensureDirectoryExists(Path.GetDirectoryName(job.Output));
            }
            finally
            {
                base.checkJobIO();
            }
            su.Status = "Creating DGA...";
        }

        protected override string Commandline
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (job.DemuxVideo)
                     sb.Append("-i \"" + job.Input + "\" -od \"" + job.Output + "\" -e -h");
                else sb.Append("-i \"" + job.Input + "\" -o \"" + job.Output + "\" -e -h");
                if (job.DemuxMode == 2)
                    sb.Append(" -a"); // demux everything
                return sb.ToString();
            }
        }
    }
}
