using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200001F RID: 31
	internal class LrsLog
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00006A54 File Offset: 0x00004C54
		public LrsLog(string softwareName, Version softwareVersion, string logTypeName, string logFilePrefix, string logComponent)
		{
			this.schema = new LogSchema(softwareName, softwareVersion.ToString(), logTypeName, LrsLog.Fields);
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.schema, true);
			this.log = new Log(logFilePrefix, headerFormatter, logComponent);
			this.EventID = ProtocolBaseServices.ServiceName;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006AA7 File Offset: 0x00004CA7
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006AAF File Offset: 0x00004CAF
		public string EventID { get; private set; }

		// Token: 0x060001B7 RID: 439 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public void Configure(string path, TimeSpan ageQuota, long sizeQuota, long perFileSizeQuota)
		{
			this.log.Configure(path, ageQuota, sizeQuota, perFileSizeQuota, false);
			this.enabled = true;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006AD2 File Offset: 0x00004CD2
		public void Configure(string path, LogFileRollOver logFileRollOverSettings, TimeSpan maxAge)
		{
			this.log.Configure(path, logFileRollOverSettings, maxAge);
			this.enabled = true;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006AEC File Offset: 0x00004CEC
		public LrsSession OpenSession(string primarySmtpAddress, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[4] = this.EventID;
			logRowFormatter[6] = primarySmtpAddress;
			logRowFormatter[10] = "NA";
			LrsSession lrsSession = new LrsSession(this, logRowFormatter);
			lrsSession.SetEndpoints(remoteEndPoint, localEndPoint);
			return lrsSession;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006B38 File Offset: 0x00004D38
		public void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006B50 File Offset: 0x00004D50
		internal void Append(LogRowFormatter row)
		{
			if (!this.enabled)
			{
				return;
			}
			try
			{
				this.log.Append(row, 0);
			}
			catch (ObjectDisposedException arg)
			{
				ProtocolBaseServices.ServerTracer.TraceError<ObjectDisposedException>(0L, "LRS Log Append failed with ObjectDisposedException\r\n{0}", arg);
			}
		}

		// Token: 0x040000F8 RID: 248
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"client-ip",
			"server-ip",
			"message-id",
			"event-id",
			"sender-address",
			"recipient-address",
			"total-bytes",
			"recipient-count",
			"message-subject",
			"report-status"
		};

		// Token: 0x040000F9 RID: 249
		private Log log;

		// Token: 0x040000FA RID: 250
		private LogSchema schema;

		// Token: 0x040000FB RID: 251
		private bool enabled;

		// Token: 0x02000020 RID: 32
		internal enum Field
		{
			// Token: 0x040000FE RID: 254
			Time,
			// Token: 0x040000FF RID: 255
			ClientIP,
			// Token: 0x04000100 RID: 256
			ServerIP,
			// Token: 0x04000101 RID: 257
			MessageID,
			// Token: 0x04000102 RID: 258
			EventID,
			// Token: 0x04000103 RID: 259
			Sender,
			// Token: 0x04000104 RID: 260
			RecipientAddresses,
			// Token: 0x04000105 RID: 261
			TotalBytes,
			// Token: 0x04000106 RID: 262
			RecipientCount,
			// Token: 0x04000107 RID: 263
			Subject,
			// Token: 0x04000108 RID: 264
			ReportStatus
		}
	}
}
