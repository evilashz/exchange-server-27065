using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Logging
{
	// Token: 0x02000051 RID: 81
	internal class EdgeSyncLog
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000A174 File Offset: 0x00008374
		public EdgeSyncLog(string softwareName, Version softwareVersion, string logTypeName, string logFilePrefix, string logComponent)
		{
			this.schema = new LogSchema(softwareName, softwareVersion.ToString(), logTypeName, EdgeSyncLog.Fields);
			this.log = new Log(logFilePrefix, new LogHeaderFormatter(this.schema), logComponent);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A1AE File Offset: 0x000083AE
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A1B6 File Offset: 0x000083B6
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public void Configure(string path, TimeSpan ageQuota, Unlimited<ByteQuantifiedSize> sizeQuota, Unlimited<ByteQuantifiedSize> perFileSizeQuota)
		{
			this.log.Configure(path, ageQuota, (long)(sizeQuota.IsUnlimited ? 262144000UL : sizeQuota.Value.ToBytes()), (long)(perFileSizeQuota.IsUnlimited ? 10485760UL : perFileSizeQuota.Value.ToBytes()));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A21C File Offset: 0x0000841C
		public EdgeSyncLogSession OpenSession(string sessionId, string remoteServerFqdn, int remotePort, string localServerFqdn, EdgeSyncLoggingLevel loggingLevel)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[1] = sessionId;
			logRowFormatter[4] = remoteServerFqdn;
			logRowFormatter[5] = remotePort;
			logRowFormatter[3] = localServerFqdn;
			return new EdgeSyncLogSession(this, logRowFormatter, loggingLevel);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A264 File Offset: 0x00008464
		public void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A27C File Offset: 0x0000847C
		internal void Append(LogRowFormatter row)
		{
			try
			{
				this.log.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.ProcessTracer.TraceDebug((long)this.GetHashCode(), "EdgeSync Log Append failed with ObjectDisposedException");
			}
		}

		// Token: 0x04000163 RID: 355
		private const int DefaultQuota = 262144000;

		// Token: 0x04000164 RID: 356
		private const int DefaultPerFileQuota = 10485760;

		// Token: 0x04000165 RID: 357
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"session-id",
			"sequence-number",
			"local-serverfqdn",
			"remote-serverfqdn",
			"remote-port",
			"event",
			"level",
			"data",
			"context",
			"sync-mode",
			"sync-type",
			"dc"
		};

		// Token: 0x04000166 RID: 358
		private Log log;

		// Token: 0x04000167 RID: 359
		private LogSchema schema;

		// Token: 0x04000168 RID: 360
		private bool enabled;

		// Token: 0x02000052 RID: 82
		internal enum Field
		{
			// Token: 0x0400016A RID: 362
			Time,
			// Token: 0x0400016B RID: 363
			SessionId,
			// Token: 0x0400016C RID: 364
			SequenceNumber,
			// Token: 0x0400016D RID: 365
			LocalServerFqdn,
			// Token: 0x0400016E RID: 366
			RemoteServerFqdn,
			// Token: 0x0400016F RID: 367
			RemotePort,
			// Token: 0x04000170 RID: 368
			Event,
			// Token: 0x04000171 RID: 369
			Level,
			// Token: 0x04000172 RID: 370
			Data,
			// Token: 0x04000173 RID: 371
			Context,
			// Token: 0x04000174 RID: 372
			SyncMode,
			// Token: 0x04000175 RID: 373
			SyncType,
			// Token: 0x04000176 RID: 374
			DC
		}
	}
}
