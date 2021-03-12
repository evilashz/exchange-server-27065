using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200001B RID: 27
	internal class LightWeightLog
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00005C64 File Offset: 0x00003E64
		public LightWeightLog(string softwareName, Version softwareVersion, string logTypeName, string logFilePrefix, string logComponent)
		{
			this.schema = new LogSchema(softwareName, softwareVersion.ToString(), logTypeName, LightWeightLog.Fields);
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.schema, true);
			this.log = new Log(logFilePrefix, headerFormatter, logComponent);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005CAC File Offset: 0x00003EAC
		public void Configure(string path, TimeSpan ageQuota, long sizeQuota, long perFileSizeQuota)
		{
			this.log.Configure(path, ageQuota, sizeQuota, perFileSizeQuota, false);
			this.enabled = true;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005CC6 File Offset: 0x00003EC6
		public void Configure(string path, LogFileRollOver logFileRollOverSettings, TimeSpan maxAge)
		{
			this.log.Configure(path, logFileRollOverSettings, maxAge);
			this.enabled = true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public LightWeightLogSession OpenSession(ulong sessionId, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, ProtocolLoggingLevel loggingLevel)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[1] = sessionId.ToString("X16", NumberFormatInfo.InvariantInfo);
			logRowFormatter[4] = remoteEndPoint.ToString();
			logRowFormatter[3] = localEndPoint.ToString();
			return new LightWeightLogSession(this, logRowFormatter);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005D32 File Offset: 0x00003F32
		public void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005D48 File Offset: 0x00003F48
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
				ProtocolBaseServices.ServerTracer.TraceError<ObjectDisposedException>(0L, "Protocol Log Append failed with ObjectDisposedException\r\n{0}", arg);
			}
		}

		// Token: 0x040000C4 RID: 196
		private static readonly string[] Fields = new string[]
		{
			"dateTime",
			"sessionId",
			"seqNumber",
			"sIp",
			"cIp",
			"user",
			"duration",
			"rqsize",
			"rpsize",
			"command",
			"parameters",
			"context"
		};

		// Token: 0x040000C5 RID: 197
		private Log log;

		// Token: 0x040000C6 RID: 198
		private LogSchema schema;

		// Token: 0x040000C7 RID: 199
		private bool enabled;

		// Token: 0x0200001C RID: 28
		internal enum Field
		{
			// Token: 0x040000C9 RID: 201
			Time,
			// Token: 0x040000CA RID: 202
			SessionId,
			// Token: 0x040000CB RID: 203
			SequenceNumber,
			// Token: 0x040000CC RID: 204
			LocalEndPoint,
			// Token: 0x040000CD RID: 205
			RemoteEndPoint,
			// Token: 0x040000CE RID: 206
			User,
			// Token: 0x040000CF RID: 207
			Duration,
			// Token: 0x040000D0 RID: 208
			RequestSize,
			// Token: 0x040000D1 RID: 209
			ResponseSize,
			// Token: 0x040000D2 RID: 210
			Command,
			// Token: 0x040000D3 RID: 211
			Parameters,
			// Token: 0x040000D4 RID: 212
			Context
		}
	}
}
