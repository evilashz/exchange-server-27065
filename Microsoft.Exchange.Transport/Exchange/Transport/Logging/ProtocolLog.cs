using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Logging
{
	// Token: 0x02000082 RID: 130
	internal class ProtocolLog : IProtocolLog
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x000103D8 File Offset: 0x0000E5D8
		public ProtocolLog(string softwareName, Version softwareVersion, string logTypeName, string logFilePrefix, string logComponent)
		{
			this.schema = new LogSchema(softwareName, softwareVersion.ToString(), logTypeName, ProtocolLog.Fields);
			this.log = new AsyncLog(logFilePrefix, new LogHeaderFormatter(this.schema), logComponent);
			this.logMode = logFilePrefix;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00010428 File Offset: 0x0000E628
		public void Configure(LocalLongFullPath path, TimeSpan ageQuota, Unlimited<ByteQuantifiedSize> sizeQuota, Unlimited<ByteQuantifiedSize> perFileSizeQuota, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval)
		{
			this.Configure((path != null) ? path.PathName : null, ageQuota, (long)(sizeQuota.IsUnlimited ? 0UL : sizeQuota.Value.ToBytes()), (long)(perFileSizeQuota.IsUnlimited ? 0UL : perFileSizeQuota.Value.ToBytes()), bufferSize, streamFlushInterval, backgroundWriteInterval);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001048C File Offset: 0x0000E68C
		public void Configure(string path, TimeSpan ageQuota, long sizeQuota, long perFileSizeQuota, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval)
		{
			if (string.IsNullOrEmpty(path))
			{
				if (this.enabled)
				{
					if (string.CompareOrdinal(this.logMode, "SEND") == 0)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "SendProtocol Log output path was set to null, using old output path");
						Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SendProtocolLogPathIsNullUsingOld, null, new object[0]);
					}
					if (string.CompareOrdinal(this.logMode, "RECV") == 0)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ReceiveProtocol Log output path was set to null using old output path");
						Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ReceiveProtocolLogPathIsNullUsingOld, null, new object[0]);
						return;
					}
				}
				else
				{
					if (string.CompareOrdinal(this.logMode, "SEND") == 0)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "SendProtocol Log output path was set to null, SendProtocol Log is disabled");
						Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SendProtocolLogPathIsNull, null, new object[0]);
					}
					if (string.CompareOrdinal(this.logMode, "RECV") == 0)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "ReceiveProtocol Log output path was set to null, ReceiveProtocol Log is disabled");
						Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ReceiveProtocolLogPathIsNull, null, new object[0]);
						return;
					}
				}
			}
			else
			{
				this.log.Configure(path, ageQuota, sizeQuota, perFileSizeQuota, bufferSize, streamFlushInterval, backgroundWriteInterval);
				this.enabled = true;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public IProtocolLogSession OpenSession(string connectorId, ulong sessionId, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, ProtocolLoggingLevel loggingLevel)
		{
			ProtocolLogRowFormatter protocolLogRowFormatter = new ProtocolLogRowFormatter(this.schema);
			protocolLogRowFormatter[1] = connectorId;
			protocolLogRowFormatter[2] = sessionId.ToString("X16", NumberFormatInfo.InvariantInfo);
			protocolLogRowFormatter[5] = remoteEndPoint;
			protocolLogRowFormatter[4] = localEndPoint;
			return new ProtocolLogSession(this, protocolLogRowFormatter, loggingLevel);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010623 File Offset: 0x0000E823
		public void Flush()
		{
			if (!this.enabled || this.closed)
			{
				return;
			}
			if (this.log != null)
			{
				this.log.Flush();
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010649 File Offset: 0x0000E849
		public void Close()
		{
			this.closed = true;
			if (this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010668 File Offset: 0x0000E868
		internal void Append(ProtocolLogRowFormatter row)
		{
			if (!this.enabled || this.closed)
			{
				return;
			}
			try
			{
				this.log.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Protocol Log Append failed with ObjectDisposedException");
			}
		}

		// Token: 0x04000226 RID: 550
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"connector-id",
			"session-id",
			"sequence-number",
			"local-endpoint",
			"remote-endpoint",
			"event",
			"data",
			"context"
		};

		// Token: 0x04000227 RID: 551
		private readonly AsyncLog log;

		// Token: 0x04000228 RID: 552
		private readonly LogSchema schema;

		// Token: 0x04000229 RID: 553
		private readonly string logMode;

		// Token: 0x0400022A RID: 554
		private bool enabled;

		// Token: 0x0400022B RID: 555
		private bool closed;

		// Token: 0x02000083 RID: 131
		internal enum Field
		{
			// Token: 0x0400022D RID: 557
			Time,
			// Token: 0x0400022E RID: 558
			ConnectorId,
			// Token: 0x0400022F RID: 559
			SessionId,
			// Token: 0x04000230 RID: 560
			SequenceNumber,
			// Token: 0x04000231 RID: 561
			LocalEndPoint,
			// Token: 0x04000232 RID: 562
			RemoteEndPoint,
			// Token: 0x04000233 RID: 563
			Event,
			// Token: 0x04000234 RID: 564
			Data,
			// Token: 0x04000235 RID: 565
			Context
		}
	}
}
