using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.Logging
{
	// Token: 0x02000769 RID: 1897
	internal sealed class ProtocolLog : IDisposable
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x0004EA68 File Offset: 0x0004CC68
		public ProtocolLog(ProtocolLogConfiguration protocolLogConfiguration)
		{
			ArgumentValidator.ThrowIfNull("protocolLogConfiguration", protocolLogConfiguration);
			this.isEnabled = protocolLogConfiguration.IsEnabled;
			this.schema = new LogSchema(protocolLogConfiguration.SoftwareName, protocolLogConfiguration.SoftwareVersion.ToString(), protocolLogConfiguration.LogTypeName, ProtocolLog.Fields);
			this.log = new Log(protocolLogConfiguration.LogFilePrefix, new LogHeaderFormatter(this.schema), protocolLogConfiguration.LogComponent);
			this.ConfigureLog(protocolLogConfiguration.LogFilePath, protocolLogConfiguration.AgeQuota, protocolLogConfiguration.DirectorySizeQuota, protocolLogConfiguration.PerFileSizeQuota, protocolLogConfiguration.ProtocolLoggingLevel);
			ExTraceGlobals.ProtocolLogTracer.TraceDebug((long)this.GetHashCode(), "Protocol Log Created and Configured");
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x0004EB15 File Offset: 0x0004CD15
		internal ProtocolLoggingLevel LoggingLevel
		{
			get
			{
				return this.loggingLevel;
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0004EB1D File Offset: 0x0004CD1D
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0004EB28 File Offset: 0x0004CD28
		public void ConfigureLog(string path, long ageQuota, long directorySizeQuota, long perFileSizeQuota, ProtocolLoggingLevel loggingLevel)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("path", path);
			ProtocolLog.ThrowIfArgumentLessThanZero("ageQuota", ageQuota);
			ProtocolLog.ThrowIfArgumentLessThanZero("directorySizeQuota", directorySizeQuota);
			ProtocolLog.ThrowIfArgumentLessThanZero("perFileSizeQuota", perFileSizeQuota);
			ProtocolLog.ThrowIfArg1LessThenArg2("directorySizeQuota", directorySizeQuota, "perFileSizeQuota", perFileSizeQuota);
			this.log.Configure(path, TimeSpan.FromHours((double)ageQuota), directorySizeQuota * 1024L, perFileSizeQuota * 1024L);
			this.loggingLevel = loggingLevel;
			ExTraceGlobals.ProtocolLogTracer.TraceDebug((long)this.GetHashCode(), "Protocol Logs Configured");
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0004EBB8 File Offset: 0x0004CDB8
		public ProtocolLogSession OpenSession(ulong sessionId, string remoteServer, string localServer)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[1] = sessionId.ToString("X16", NumberFormatInfo.InvariantInfo);
			logRowFormatter[3] = remoteServer;
			logRowFormatter[2] = localServer;
			return new ProtocolLogSession(this, logRowFormatter);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0004EC00 File Offset: 0x0004CE00
		public void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
			this.isEnabled = false;
			ExTraceGlobals.ProtocolLogTracer.TraceDebug((long)this.GetHashCode(), "Protocol Logs Closed");
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0004EC32 File Offset: 0x0004CE32
		internal void Append(LogRowFormatter row)
		{
			if (!this.isEnabled)
			{
				return;
			}
			this.log.Append(row, 0);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0004EC4A File Offset: 0x0004CE4A
		private static void ThrowIfArgumentLessThanZero(string name, long arg)
		{
			if (arg < 0L)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is set to less than 0.");
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0004EC64 File Offset: 0x0004CE64
		private static void ThrowIfArg1LessThenArg2(string name1, long arg1, string name2, long arg2)
		{
			if (arg1 < arg2)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The value of {0} is set to less than {1}.", new object[]
				{
					name1,
					name2
				}));
			}
		}

		// Token: 0x040022C9 RID: 8905
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"session-id",
			"local-server",
			"remote-server",
			"event",
			"data",
			"context"
		};

		// Token: 0x040022CA RID: 8906
		private Log log;

		// Token: 0x040022CB RID: 8907
		private LogSchema schema;

		// Token: 0x040022CC RID: 8908
		private bool isEnabled;

		// Token: 0x040022CD RID: 8909
		private ProtocolLoggingLevel loggingLevel;

		// Token: 0x0200076A RID: 1898
		internal enum Field
		{
			// Token: 0x040022CF RID: 8911
			Time,
			// Token: 0x040022D0 RID: 8912
			SessionId,
			// Token: 0x040022D1 RID: 8913
			LocalServer,
			// Token: 0x040022D2 RID: 8914
			RemoteServer,
			// Token: 0x040022D3 RID: 8915
			Event,
			// Token: 0x040022D4 RID: 8916
			Data,
			// Token: 0x040022D5 RID: 8917
			Context
		}
	}
}
