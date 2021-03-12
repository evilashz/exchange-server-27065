using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000015 RID: 21
	internal class SenderDBObject : Database
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003B28 File Offset: 0x00001D28
		public SenderDBObject(IPAddress address, Trace traceTag)
		{
			if (traceTag == null)
			{
				throw new LocalizedException(DbStrings.InvalidTraceObject);
			}
			this.loaded = false;
			this.senderAddress = address;
			this.dataBlob = null;
			this.detectionTime = DateTime.MinValue;
			this.detectionPending = false;
			this.status = OPDetectionResult.Unknown;
			this.reverseDns = string.Empty;
			this.queryTime = DateTime.MinValue;
			this.queryPending = false;
			this.srl = 0;
			this.extOpenProxy = false;
			this.expirationTime = DateTime.MinValue;
			this.traceTag = traceTag;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public byte[] ProtocolAnalysisDataBlob
		{
			get
			{
				return this.dataBlob;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003BBC File Offset: 0x00001DBC
		public DateTime OpenProxyDetectionTime
		{
			get
			{
				return this.detectionTime;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public bool OpenProxyDetectionPending
		{
			get
			{
				return this.detectionPending;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003BCC File Offset: 0x00001DCC
		public OPDetectionResult OpenProxyStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public string ReverseDns
		{
			get
			{
				return this.reverseDns;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003BDC File Offset: 0x00001DDC
		public DateTime ReverseDnsQueryTime
		{
			get
			{
				return this.queryTime;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public bool ReverseDnsQueryPending
		{
			get
			{
				return this.queryPending;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003BEC File Offset: 0x00001DEC
		public int SenderReputationLevel
		{
			get
			{
				return this.srl;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public bool SenderReputationIsOpenProxy
		{
			get
			{
				return this.extOpenProxy;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003BFC File Offset: 0x00001DFC
		public DateTime SenderReputationExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003C04 File Offset: 0x00001E04
		public int WorkQueueSize
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003C0B File Offset: 0x00001E0B
		public IPAddress SenderAddress
		{
			get
			{
				return this.senderAddress;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C14 File Offset: 0x00001E14
		public bool Load()
		{
			this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Loading data for sender {0}.", this.senderAddress);
			this.loaded = false;
			try
			{
				lock (Database.syncObject)
				{
					if (Database.IsDbClosed)
					{
						return false;
					}
					this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Loading protocol analysis data for sender {0}.", this.senderAddress);
					ProtocolAnalysisRowData protocolAnalysisRowData = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.Find(this.senderAddress.ToString());
					if (protocolAnalysisRowData == null)
					{
						this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Failed to find {0}. Creating a new record", this.senderAddress);
						protocolAnalysisRowData = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.NewData(this.senderAddress.ToString());
					}
					else
					{
						this.dataBlob = protocolAnalysisRowData.DataBlob;
						this.reverseDns = protocolAnalysisRowData.ReverseDns;
						this.queryTime = protocolAnalysisRowData.LastQueryTime;
						this.queryPending = protocolAnalysisRowData.Processing;
					}
					protocolAnalysisRowData.LastUpdateTime = DateTime.UtcNow;
					protocolAnalysisRowData.Commit();
					this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Loading open proxy status data for sender {0}.", this.senderAddress);
					OpenProxyStatusRowData openProxyStatusRowData = DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>.Find(this.senderAddress.ToString());
					if (openProxyStatusRowData != null)
					{
						this.status = (OPDetectionResult)openProxyStatusRowData.OpenProxyStatus;
						this.detectionTime = openProxyStatusRowData.LastDetectionTime;
						this.detectionPending = openProxyStatusRowData.Processing;
					}
					if (this.senderAddress.AddressFamily == AddressFamily.InterNetwork)
					{
						this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Loading sender reputation data for sender {0}.", this.senderAddress);
						SenderReputationRowData senderReputationRowData = DataRowAccessBase<SenderReputationTable, SenderReputationRowData>.Find(CMD5.GetHash(this.senderAddress.GetAddressBytes()));
						if (senderReputationRowData != null)
						{
							this.srl = senderReputationRowData.Srl;
							this.extOpenProxy = senderReputationRowData.OpenProxy;
							this.expirationTime = senderReputationRowData.ExpirationTime;
						}
					}
					this.loaded = true;
				}
			}
			catch
			{
				this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Failed to load sender data for {0}. Exception thrown.", this.senderAddress);
				throw;
			}
			return this.loaded;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003E38 File Offset: 0x00002038
		public bool Update(byte[] senderData, bool openProxyDetection, bool reverseDnsQuery)
		{
			if (!this.loaded)
			{
				throw new LocalizedException(DbStrings.PaRecordNotLoaded);
			}
			this.dataBlob = senderData;
			try
			{
				lock (Database.syncObject)
				{
					if (Database.IsDbClosed)
					{
						return false;
					}
					this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Updating protocol analysis data for sender {0}.", this.senderAddress);
					ProtocolAnalysisRowData protocolAnalysisRowData = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.Find(this.senderAddress.ToString());
					if (protocolAnalysisRowData != null)
					{
						protocolAnalysisRowData.DataBlob = this.dataBlob;
						if (openProxyDetection)
						{
							protocolAnalysisRowData.LastUpdateTime = DateTime.UtcNow;
						}
						else
						{
							protocolAnalysisRowData.Processing = true;
						}
						protocolAnalysisRowData.Commit();
					}
					else if (reverseDnsQuery)
					{
						protocolAnalysisRowData = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.NewData(this.senderAddress.ToString());
						protocolAnalysisRowData.DataBlob = this.dataBlob;
						protocolAnalysisRowData.Processing = true;
						protocolAnalysisRowData.LastUpdateTime = DateTime.UtcNow;
						protocolAnalysisRowData.Commit();
					}
				}
				if (!openProxyDetection)
				{
					return true;
				}
				this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Updating open proxy data for sender {0}.", this.senderAddress);
				lock (Database.syncObject)
				{
					if (Database.IsDbClosed)
					{
						return false;
					}
					OpenProxyStatusRowData openProxyStatusRowData = DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>.Find(this.senderAddress.ToString());
					if (openProxyStatusRowData == null)
					{
						openProxyStatusRowData = DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>.NewData(this.senderAddress.ToString());
						openProxyStatusRowData.LastAccessTime = DateTime.UtcNow;
					}
					openProxyStatusRowData.OpenProxyStatus = 0;
					openProxyStatusRowData.Processing = true;
					openProxyStatusRowData.Commit();
				}
			}
			catch
			{
				this.traceTag.TraceDebug<IPAddress>((long)this.GetHashCode(), "Failed to update protocol analysis data for {0}. Exception thrown.", this.senderAddress);
				throw;
			}
			return true;
		}

		// Token: 0x04000034 RID: 52
		private const int WorkQueueSizeValue = 200;

		// Token: 0x04000035 RID: 53
		private IPAddress senderAddress;

		// Token: 0x04000036 RID: 54
		private bool loaded;

		// Token: 0x04000037 RID: 55
		private byte[] dataBlob;

		// Token: 0x04000038 RID: 56
		private DateTime detectionTime;

		// Token: 0x04000039 RID: 57
		private bool detectionPending;

		// Token: 0x0400003A RID: 58
		private OPDetectionResult status;

		// Token: 0x0400003B RID: 59
		private string reverseDns;

		// Token: 0x0400003C RID: 60
		private DateTime queryTime;

		// Token: 0x0400003D RID: 61
		private bool queryPending;

		// Token: 0x0400003E RID: 62
		private int srl;

		// Token: 0x0400003F RID: 63
		private bool extOpenProxy;

		// Token: 0x04000040 RID: 64
		private DateTime expirationTime;

		// Token: 0x04000041 RID: 65
		private Trace traceTag;
	}
}
