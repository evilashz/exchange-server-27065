using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000009 RID: 9
	internal class MapiExMonLogger : ExMonLogger
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002760 File Offset: 0x00000960
		internal MapiExMonLogger(bool enableTestMode, int sessionId, string accessingPrincipalLegacyDN, string clientAddress, MapiVersion clientVersion, string serviceName) : base(enableTestMode, clientAddress, serviceName, new ExMonLogger.CreateExmonRpcInstanceId(ETWTrace.CreateExmonMapiRpcInstanceId), new ExMonLogger.ExmonRpcTraceEventInstance(ETWTrace.ExmonMapiRpcTraceEventInstance))
		{
			this.sessionId = sessionId;
			this.clientVersion = clientVersion;
			this.AccessedMailboxLegacyDn = accessingPrincipalLegacyDN;
			this.end = new MapiExMonLogger.MapiRpcEnd(this);
			this.start = new MapiExMonLogger.MapiRpcStart(this);
			this.operation = new MapiExMonLogger.MapiRpcOperation(this);
		}

		// Token: 0x1700001C RID: 28
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000027CC File Offset: 0x000009CC
		public string AccessedMailboxLegacyDn
		{
			set
			{
				if (!object.ReferenceEquals(this.mailboxDn, value) && this.mailboxDn != value)
				{
					this.mailboxDn = value;
					LegacyDN legacyDN;
					if (LegacyDN.TryParse(value, out legacyDN))
					{
						string text;
						string userName;
						legacyDN.GetParentLegacyDN(out text, out userName);
						base.UserName = userName;
					}
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002818 File Offset: 0x00000A18
		internal MapiExMonLogger.MapiRpcStart RpcStart
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002820 File Offset: 0x00000A20
		internal MapiExMonLogger.MapiRpcOperation RpcOperation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002828 File Offset: 0x00000A28
		internal MapiExMonLogger.MapiRpcEnd RpcEnd
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002830 File Offset: 0x00000A30
		public void BeginRopProcessing(JET_THREADSTATS threadStats)
		{
			if (base.IsTracingEnabled)
			{
				base.GetNewInstanceId();
				this.start.Clear();
				this.start.ThreadStats = threadStats;
				this.operationNumber = 0U;
				base.Submit();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002865 File Offset: 0x00000A65
		public void LogInputRops(IEnumerable<RopId> rops)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002867 File Offset: 0x00000A67
		public void LogPrepareForRop(RopId ropId)
		{
			if (base.IsTracingEnabled)
			{
				this.operation.Clear();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000287C File Offset: 0x00000A7C
		public void OnFid(StoreId folderId)
		{
			if (base.IsTracingEnabled)
			{
				this.operation.Fid = folderId;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002892 File Offset: 0x00000A92
		public void OnMid(StoreId messageId)
		{
			if (base.IsTracingEnabled)
			{
				this.operation.Mid = messageId;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void SetClientActivityInfo(string activityId, string component, string protocol, string action)
		{
			if (base.IsTracingEnabled)
			{
				this.activityid = activityId;
				this.component = component;
				this.protocol = protocol;
				this.action = action;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void LogCompletedRop(RopId ropId, ErrorCode errorCode, JET_THREADSTATS threadStats)
		{
			if (base.IsTracingEnabled)
			{
				this.operation.Rop = ropId;
				this.operation.RopNumber = this.operationNumber++;
				this.operation.ErrorCode = (uint)errorCode;
				this.operation.ThreadStats = threadStats;
				base.Submit();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000292C File Offset: 0x00000B2C
		public void EndRopProcessing()
		{
			if (base.IsTracingEnabled)
			{
				this.end.Clear();
				this.end.ClientVersion = this.clientVersion;
				this.end.ConnectionHandle = (ulong)this.sessionId;
				this.end.SetStrings(base.UserName, base.ClientAddress, base.ServiceName, this.activityid, this.component, this.protocol, this.action);
				base.Submit();
			}
			base.ReleaseBuffer();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029B4 File Offset: 0x00000BB4
		protected void LogCompletedRop(RopId ropId, ErrorCode errorCode)
		{
			JET_THREADSTATS threadStats = JET_THREADSTATS.Create(0, 0, 0, 0, 0, 0, 0);
			this.LogCompletedRop(ropId, errorCode, threadStats);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029D8 File Offset: 0x00000BD8
		private void WriteMapiVersion(int offset, MapiVersion version)
		{
			ushort[] array = version.ToTriplet();
			int num = 0;
			num += ExBitConverter.Write(array[0], base.Buffer, offset);
			num += ExBitConverter.Write(array[1], base.Buffer, offset + 2);
			num += ExBitConverter.Write(array[2], base.Buffer, offset + 4);
			base.UpdateBytesWritten(offset, num);
		}

		// Token: 0x04000038 RID: 56
		private const byte ExMonStart = 199;

		// Token: 0x04000039 RID: 57
		private const byte ExMonEnd = 200;

		// Token: 0x0400003A RID: 58
		private readonly int sessionId;

		// Token: 0x0400003B RID: 59
		private readonly MapiVersion clientVersion;

		// Token: 0x0400003C RID: 60
		private uint operationNumber;

		// Token: 0x0400003D RID: 61
		private MapiExMonLogger.MapiRpcEnd end;

		// Token: 0x0400003E RID: 62
		private MapiExMonLogger.MapiRpcOperation operation;

		// Token: 0x0400003F RID: 63
		private MapiExMonLogger.MapiRpcStart start;

		// Token: 0x04000040 RID: 64
		private string mailboxDn;

		// Token: 0x04000041 RID: 65
		private string activityid;

		// Token: 0x04000042 RID: 66
		private string component;

		// Token: 0x04000043 RID: 67
		private string protocol;

		// Token: 0x04000044 RID: 68
		private string action;

		// Token: 0x0200000A RID: 10
		internal struct MapiRpcEnd
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00002A31 File Offset: 0x00000C31
			internal MapiRpcEnd(MapiExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
			}

			// Token: 0x17000020 RID: 32
			// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A3A File Offset: 0x00000C3A
			public ulong ConnectionHandle
			{
				set
				{
					this.exmonLogger.WriteUInt64(56, value);
				}
			}

			// Token: 0x17000021 RID: 33
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00002A4A File Offset: 0x00000C4A
			public uint ErrorCode
			{
				set
				{
					this.exmonLogger.WriteUInt32(64, value);
				}
			}

			// Token: 0x17000022 RID: 34
			// (set) Token: 0x06000052 RID: 82 RVA: 0x00002A5A File Offset: 0x00000C5A
			public uint BytesIn
			{
				set
				{
					this.exmonLogger.WriteUInt32(68, value);
				}
			}

			// Token: 0x17000023 RID: 35
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A6A File Offset: 0x00000C6A
			public uint BytesOut
			{
				set
				{
					this.exmonLogger.WriteUInt32(72, value);
				}
			}

			// Token: 0x17000024 RID: 36
			// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A7A File Offset: 0x00000C7A
			public ulong PLogon
			{
				set
				{
					this.exmonLogger.WriteUInt64(80, value);
				}
			}

			// Token: 0x17000025 RID: 37
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00002A8A File Offset: 0x00000C8A
			public MapiVersion ClientVersion
			{
				set
				{
					this.exmonLogger.WriteMapiVersion(88, value);
				}
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00002A9A File Offset: 0x00000C9A
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(99);
				this.exmonLogger.SetClassType(200);
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002ABC File Offset: 0x00000CBC
			public void SetStrings(string user, string address, string application, string activityId, string component, string protocol, string action)
			{
				this.exmonLogger.WriteUserAddressApplication(94, user, address, application, activityId, component, protocol, action);
			}

			// Token: 0x04000045 RID: 69
			internal const int StructSize = 99;

			// Token: 0x04000046 RID: 70
			private MapiExMonLogger exmonLogger;

			// Token: 0x0200000B RID: 11
			private enum Offsets
			{
				// Token: 0x04000048 RID: 72
				ConnectionHandle = 56,
				// Token: 0x04000049 RID: 73
				Ec = 64,
				// Token: 0x0400004A RID: 74
				BytesIn = 68,
				// Token: 0x0400004B RID: 75
				BytesOut = 72,
				// Token: 0x0400004C RID: 76
				LogonPtr = 80,
				// Token: 0x0400004D RID: 77
				ClientVersion = 88,
				// Token: 0x0400004E RID: 78
				StringsBuffer = 94,
				// Token: 0x0400004F RID: 79
				MaxOffset = 99
			}
		}

		// Token: 0x0200000C RID: 12
		internal struct MapiRpcOperation
		{
			// Token: 0x06000058 RID: 88 RVA: 0x00002AE1 File Offset: 0x00000CE1
			internal MapiRpcOperation(MapiExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 88);
			}

			// Token: 0x17000026 RID: 38
			// (set) Token: 0x06000059 RID: 89 RVA: 0x00002AF8 File Offset: 0x00000CF8
			public uint RopNumber
			{
				set
				{
					this.exmonLogger.WriteUInt32(56, value);
				}
			}

			// Token: 0x17000027 RID: 39
			// (set) Token: 0x0600005A RID: 90 RVA: 0x00002B08 File Offset: 0x00000D08
			public uint ErrorCode
			{
				set
				{
					this.exmonLogger.WriteUInt32(60, value);
				}
			}

			// Token: 0x17000028 RID: 40
			// (set) Token: 0x0600005B RID: 91 RVA: 0x00002B18 File Offset: 0x00000D18
			public uint Hsot
			{
				set
				{
					this.exmonLogger.WriteUInt32(64, value);
				}
			}

			// Token: 0x17000029 RID: 41
			// (set) Token: 0x0600005C RID: 92 RVA: 0x00002B28 File Offset: 0x00000D28
			public uint Flags
			{
				set
				{
					this.exmonLogger.WriteUInt32(68, value);
				}
			}

			// Token: 0x1700002A RID: 42
			// (set) Token: 0x0600005D RID: 93 RVA: 0x00002B38 File Offset: 0x00000D38
			public StoreId Fid
			{
				set
				{
					this.exmonLogger.WriteUInt64(72, value);
				}
			}

			// Token: 0x1700002B RID: 43
			// (set) Token: 0x0600005E RID: 94 RVA: 0x00002B4D File Offset: 0x00000D4D
			public StoreId Mid
			{
				set
				{
					this.exmonLogger.WriteUInt64(80, value);
				}
			}

			// Token: 0x1700002C RID: 44
			// (set) Token: 0x0600005F RID: 95 RVA: 0x00002B62 File Offset: 0x00000D62
			public RopId Rop
			{
				set
				{
					this.exmonLogger.WriteByte(4, (byte)value);
				}
			}

			// Token: 0x1700002D RID: 45
			// (set) Token: 0x06000060 RID: 96 RVA: 0x00002B71 File Offset: 0x00000D71
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002B7F File Offset: 0x00000D7F
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(120);
			}

			// Token: 0x04000050 RID: 80
			internal const int StructSize = 120;

			// Token: 0x04000051 RID: 81
			private MapiExMonLogger exmonLogger;

			// Token: 0x04000052 RID: 82
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x0200000D RID: 13
			private enum Offsets
			{
				// Token: 0x04000054 RID: 84
				RopNumber = 56,
				// Token: 0x04000055 RID: 85
				Ec = 60,
				// Token: 0x04000056 RID: 86
				Hsot = 64,
				// Token: 0x04000057 RID: 87
				Flags = 68,
				// Token: 0x04000058 RID: 88
				Fid = 72,
				// Token: 0x04000059 RID: 89
				Mid = 80,
				// Token: 0x0400005A RID: 90
				JetStats = 88,
				// Token: 0x0400005B RID: 91
				MaxOffset = 120
			}
		}

		// Token: 0x0200000E RID: 14
		internal struct MapiRpcStart
		{
			// Token: 0x06000062 RID: 98 RVA: 0x00002B8E File Offset: 0x00000D8E
			internal MapiRpcStart(MapiExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
				this.statsLogger = new ExMonLogger.JetThreadStats(exmonLogger, 56);
			}

			// Token: 0x1700002E RID: 46
			// (set) Token: 0x06000063 RID: 99 RVA: 0x00002BA5 File Offset: 0x00000DA5
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.statsLogger.ThreadStats = value;
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00002BB3 File Offset: 0x00000DB3
			public void Clear()
			{
				this.exmonLogger.SetTraceSize(88);
				this.exmonLogger.SetClassType(199);
			}

			// Token: 0x0400005C RID: 92
			internal const int StructSize = 88;

			// Token: 0x0400005D RID: 93
			private MapiExMonLogger exmonLogger;

			// Token: 0x0400005E RID: 94
			private ExMonLogger.JetThreadStats statsLogger;

			// Token: 0x0200000F RID: 15
			private enum Offsets
			{
				// Token: 0x04000060 RID: 96
				JetStats = 56,
				// Token: 0x04000061 RID: 97
				MaxOffset = 88
			}
		}
	}
}
