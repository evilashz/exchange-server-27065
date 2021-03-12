using System;
using System.IO;
using System.Reflection;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003E5 RID: 997
	internal class E4eLog : IE4eLogger
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000B5E14 File Offset: 0x000B4014
		internal static E4eLog Instance
		{
			get
			{
				return E4eLog.instance;
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000B5E1C File Offset: 0x000B401C
		private E4eLog()
		{
			this.enabled = false;
			try
			{
				string[] array = new string[4];
				for (int i = 0; i < 4; i++)
				{
					array[i] = ((E4eLogField)i).ToString();
				}
				this.logSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "E4E Agent Log", array);
				this.log = new Log("E4EAgent", new LogHeaderFormatter(this.logSchema), "E4EAgentLogs");
				string installPath = ExchangeSetupContext.InstallPath;
				if (string.IsNullOrEmpty(installPath))
				{
					throw new Exception("Install path is empty");
				}
				this.log.Configure(Path.Combine(installPath, E4eLog.DefaultLogPath), E4eLog.DefaultMaxAge, (long)E4eLog.DefaultMaxDirectorySize.ToBytes(), (long)E4eLog.DefaultMaxLogFileSize.ToBytes(), E4eLog.DefaultBufferSize, E4eLog.DefaultStreamFlushInterval);
				this.enabled = true;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "E4e Agent Log is disabled - {0}", ex.ToString());
				this.enabled = false;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000B5F38 File Offset: 0x000B4138
		internal void LogInfo(string messageID, string formatString, params object[] args)
		{
			if (this.enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
				logRowFormatter[1] = E4eLogType.Info;
				logRowFormatter[2] = messageID;
				logRowFormatter[3] = string.Format(formatString, args);
				this.log.Append(logRowFormatter, 0);
				return;
			}
			ExTraceGlobals.HostedEncryptionTracer.TraceInformation(0, 0L, formatString, args);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000B5F9C File Offset: 0x000B419C
		internal void LogError(string messageID, string formatString, params object[] args)
		{
			if (this.enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
				logRowFormatter[1] = E4eLogType.Error;
				logRowFormatter[2] = messageID;
				logRowFormatter[3] = string.Format(formatString, args);
				this.log.Append(logRowFormatter, 0);
				return;
			}
			ExTraceGlobals.HostedEncryptionTracer.TraceInformation(0, 0L, formatString, args);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000B5FFD File Offset: 0x000B41FD
		public void LogInfo(HttpContext context, string methodName, string messageID, string messageFormat, params object[] args)
		{
			this.LogInfo(messageID, messageFormat, args);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000B600A File Offset: 0x000B420A
		public void LogError(HttpContext context, string methodName, string messageID, string messageFormat, params object[] args)
		{
			this.LogError(messageID, messageFormat, args);
		}

		// Token: 0x0400168C RID: 5772
		private static readonly string DefaultLogPath = "Logging\\E4E\\Agent";

		// Token: 0x0400168D RID: 5773
		private static readonly TimeSpan DefaultMaxAge = TimeSpan.FromDays(30.0);

		// Token: 0x0400168E RID: 5774
		private static readonly ByteQuantifiedSize DefaultMaxDirectorySize = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x0400168F RID: 5775
		private static readonly ByteQuantifiedSize DefaultMaxLogFileSize = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x04001690 RID: 5776
		private static readonly int DefaultBufferSize = 1048576;

		// Token: 0x04001691 RID: 5777
		private static readonly TimeSpan DefaultStreamFlushInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04001692 RID: 5778
		private static E4eLog instance = new E4eLog();

		// Token: 0x04001693 RID: 5779
		private LogSchema logSchema;

		// Token: 0x04001694 RID: 5780
		private Log log;

		// Token: 0x04001695 RID: 5781
		private readonly bool enabled;
	}
}
