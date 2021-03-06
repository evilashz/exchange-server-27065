using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000350 RID: 848
	internal class TraceLogger : ILogger
	{
		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006A874 File Offset: 0x00068A74
		public TraceLogger(bool silent = false)
		{
			this.silent = silent;
			if (silent)
			{
				return;
			}
			this.logSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "SharePoint Activity Logger Diagnostics Logs", this.columns);
			this.logger = new Log("SendLinkClickedSignalToSP_" + Process.GetCurrentProcess().Id + "_", new LogHeaderFormatter(this.logSchema), "SendLinkClickedSignalToSP");
			this.logger.Configure(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\SendLinkClickedSignalToSP"), TimeSpan.FromDays(3.0), 262144000L, 10485760L);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006A954 File Offset: 0x00068B54
		void ILogger.LogInfo(string format, params object[] args)
		{
			this.Log(TraceLogger.LoggingLevel.Information, format, args);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0006A95F File Offset: 0x00068B5F
		void ILogger.LogWarning(string format, params object[] args)
		{
			this.Log(TraceLogger.LoggingLevel.Warning, format, args);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006A96C File Offset: 0x00068B6C
		private void Log(TraceLogger.LoggingLevel level, string format, params object[] args)
		{
			if (this.silent)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[0] = DateTime.UtcNow;
			logRowFormatter[1] = level;
			logRowFormatter[2] = ((args.Length != 0) ? new StringBuilder(format.Length).AppendFormat(format, args).ToString() : format);
			this.logger.Append(logRowFormatter, -1);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0006A9DF File Offset: 0x00068BDF
		public void Close()
		{
			if (this.silent)
			{
				return;
			}
			this.logger.Flush();
			this.logger.Close();
		}

		// Token: 0x04000FAB RID: 4011
		private readonly string[] columns = new string[]
		{
			"date-time",
			"level",
			"message"
		};

		// Token: 0x04000FAC RID: 4012
		private Log logger;

		// Token: 0x04000FAD RID: 4013
		private LogSchema logSchema;

		// Token: 0x04000FAE RID: 4014
		private readonly bool silent;

		// Token: 0x02000351 RID: 849
		public enum LoggingLevel
		{
			// Token: 0x04000FB0 RID: 4016
			Information,
			// Token: 0x04000FB1 RID: 4017
			Warning
		}
	}
}
