using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200043B RID: 1083
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class InferenceDiagnosticsLog
	{
		// Token: 0x0600306B RID: 12395 RVA: 0x000C70C8 File Offset: 0x000C52C8
		static InferenceDiagnosticsLog()
		{
			InferenceDiagnosticsLog.Schema = new LogSchema("Microsoft Exchange", InferenceDiagnosticsLog.Version, "InferenceDiagnosticsLog", InferenceDiagnosticsLog.Columns);
			string fileNamePrefix = string.Format("{0}_{1}_{2}_", "InferenceDiagnosticsLog", InferenceDiagnosticsLog.ProcessName, InferenceDiagnosticsLog.ProcessId.ToString());
			InferenceDiagnosticsLog.Logger = new Log(fileNamePrefix, new LogHeaderFormatter(InferenceDiagnosticsLog.Schema), "InferenceDiagnosticsLog");
			InferenceDiagnosticsLog.Logger.Configure(InferenceDiagnosticsLog.LogPath, TimeSpan.FromDays(7.0), (long)ByteQuantifiedSize.FromGB(1UL).ToBytes(), (long)ByteQuantifiedSize.FromMB(10UL).ToBytes());
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000C71D4 File Offset: 0x000C53D4
		public static void Log(string source, object detail)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(InferenceDiagnosticsLog.Schema);
			logRowFormatter[0] = DateTime.UtcNow;
			logRowFormatter[1] = InferenceDiagnosticsLog.ProcessName;
			logRowFormatter[2] = source;
			logRowFormatter[3] = detail;
			InferenceDiagnosticsLog.Logger.Append(logRowFormatter, -1);
		}

		// Token: 0x04001A53 RID: 6739
		private const string SoftwareName = "Microsoft Exchange";

		// Token: 0x04001A54 RID: 6740
		private const string LogType = "InferenceDiagnosticsLog";

		// Token: 0x04001A55 RID: 6741
		private static readonly string LogPath = Path.Combine(ExchangeSetupContext.LoggingPath, "InferenceDiagnosticsLog");

		// Token: 0x04001A56 RID: 6742
		private static readonly Log Logger;

		// Token: 0x04001A57 RID: 6743
		private static readonly LogSchema Schema;

		// Token: 0x04001A58 RID: 6744
		private static readonly string Version = "15.00.1497.010";

		// Token: 0x04001A59 RID: 6745
		private static readonly string[] Columns = new string[]
		{
			"Timestamp",
			"ProcessName",
			"Source",
			"Detail"
		};

		// Token: 0x04001A5A RID: 6746
		private static readonly int ProcessId = Process.GetCurrentProcess().Id;

		// Token: 0x04001A5B RID: 6747
		private static readonly string ProcessName = Process.GetCurrentProcess().ProcessName;
	}
}
