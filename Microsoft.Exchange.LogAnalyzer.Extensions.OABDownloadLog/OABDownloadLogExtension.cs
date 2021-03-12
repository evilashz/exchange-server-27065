using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog
{
	// Token: 0x02000003 RID: 3
	public sealed class OABDownloadLogExtension : LogExtension
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000002DC
		public OABDownloadLogExtension(Job job) : base(job)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E5 File Offset: 0x000002E5
		public override string GetName()
		{
			return "OABDownloadLog";
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		public override void Initialize()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EE File Offset: 0x000002EE
		public override Type GetLogAnalyzerBaseType()
		{
			return typeof(OABDownloadLogAnalyzer);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public override void ProcessLine(LogSourceLine line, List<LogLine> logLinesProcessed)
		{
			if (line.LogSource.Schema.IsHeader(line))
			{
				List<string> list;
				if (!StringUtils.TryGetColumns(line.Text, ',', ref list))
				{
					Log.LogErrorMessage(string.Format("Format Exception: Unable to parse OABDownload log header from line - '{0}'", line.Text), new object[0]);
					return;
				}
				if (string.IsNullOrEmpty(list[0]) || !list[0].Equals("DateTime", StringComparison.OrdinalIgnoreCase))
				{
					Log.LogErrorMessage(string.Format("Format Exception: OABDownload log Header is in an incorrect format. The first parsed column is not equal to DateTime: - '{0}'", line.Text), new object[0]);
					return;
				}
				this.logHeader = list;
				return;
			}
			else
			{
				if (line.LogSource.Schema.IsComment(line))
				{
					return;
				}
				if (this.logHeader == null || this.logHeader.Count == 0)
				{
					Log.LogErrorMessage("Format Exception: OABDownload log line processing skipped since we have not yet parsed a valid header.", new object[0]);
					return;
				}
				try
				{
					OABDownloadLogLine item = new OABDownloadLogLine(this.logHeader, line);
					logLinesProcessed.Add(item);
				}
				catch (InvalidDataException ex)
				{
					Log.LogErrorMessage("Skipped corrupted log line. Exception - {0}", new object[]
					{
						ex
					});
				}
				return;
			}
		}

		// Token: 0x04000001 RID: 1
		public const string Name = "OABDownloadLog";

		// Token: 0x04000002 RID: 2
		private List<string> logHeader;
	}
}
