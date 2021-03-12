using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000A RID: 10
	internal abstract class BasePerfFailureAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002E14 File Offset: 0x00001014
		protected BasePerfFailureAuxiliaryBlock(byte blockVersion, AuxiliaryBlockTypes blockType, ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(blockVersion, blockType)
		{
			if (blockType != AuxiliaryBlockTypes.PerfFailure && blockType != AuxiliaryBlockTypes.PerfBgFailure && blockType != AuxiliaryBlockTypes.PerfFgFailure)
			{
				throw new ArgumentException("Type must be PerfFailure, PerfBgFailure or PerfFgFailure", "blockType");
			}
			if (blockVersion == 2)
			{
				this.blockProcessId = blockProcessId;
			}
			this.blockClientId = blockClientId;
			this.blockServerId = blockServerId;
			this.blockSessionId = blockSessionId;
			this.blockRequestId = blockRequestId;
			this.blockTimeSinceRequest = blockTimeSinceRequest;
			this.blockTimeToFailRequest = blockTimeToFailRequest;
			this.blockResultCode = blockResultCode;
			this.blockRequestOperation = blockRequestOperation;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E94 File Offset: 0x00001094
		internal BasePerfFailureAuxiliaryBlock(Reader reader) : base(reader)
		{
			if (base.Version == 2)
			{
				this.blockProcessId = reader.ReadUInt16();
			}
			this.blockClientId = reader.ReadUInt16();
			this.blockServerId = reader.ReadUInt16();
			this.blockSessionId = reader.ReadUInt16();
			this.blockRequestId = reader.ReadUInt16();
			if (base.Version == 2)
			{
				reader.ReadInt16();
			}
			this.blockTimeSinceRequest = reader.ReadUInt32();
			this.blockTimeToFailRequest = reader.ReadUInt32();
			this.blockResultCode = reader.ReadUInt32();
			this.blockRequestOperation = reader.ReadByte();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F2D File Offset: 0x0000112D
		public ushort BlockProcessId
		{
			get
			{
				return this.blockProcessId;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002F35 File Offset: 0x00001135
		public ushort BlockClientId
		{
			get
			{
				return this.blockClientId;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F3D File Offset: 0x0000113D
		public ushort BlockServerId
		{
			get
			{
				return this.blockServerId;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002F45 File Offset: 0x00001145
		public ushort BlockSessionId
		{
			get
			{
				return this.blockSessionId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F4D File Offset: 0x0000114D
		public ushort BlockRequestId
		{
			get
			{
				return this.blockRequestId;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002F55 File Offset: 0x00001155
		public uint BlockTimeSinceRequest
		{
			get
			{
				return this.blockTimeSinceRequest;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F5D File Offset: 0x0000115D
		public uint BlockTimeToFailRequest
		{
			get
			{
				return this.blockTimeToFailRequest;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002F65 File Offset: 0x00001165
		public uint BlockResultCode
		{
			get
			{
				return this.blockResultCode;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F6D File Offset: 0x0000116D
		public byte BlockRequestOperation
		{
			get
			{
				return this.blockRequestOperation;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F75 File Offset: 0x00001175
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientTimeStampedEventArgs(this.blockTimeSinceRequest, ClientPerformanceEventType.RpcAttempted));
			sink.ReportEvent(new ClientFailureEventArgs(this.blockTimeSinceRequest, ClientPerformanceEventType.RpcFailed, this.blockResultCode));
			base.ReportClientPerformance(sink);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002FA8 File Offset: 0x000011A8
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			if (base.Version == 2)
			{
				writer.WriteUInt16(this.blockProcessId);
			}
			writer.WriteUInt16(this.blockClientId);
			writer.WriteUInt16(this.blockServerId);
			writer.WriteUInt16(this.blockSessionId);
			writer.WriteUInt16(this.blockRequestId);
			if (base.Version == 2)
			{
				writer.WriteInt16(0);
			}
			writer.WriteUInt32(this.blockTimeSinceRequest);
			writer.WriteUInt32(this.blockTimeToFailRequest);
			writer.WriteUInt32(this.blockResultCode);
			writer.WriteByte(this.blockRequestOperation);
			writer.WriteByte(0);
			writer.WriteInt16(0);
		}

		// Token: 0x04000043 RID: 67
		private readonly ushort blockProcessId;

		// Token: 0x04000044 RID: 68
		private readonly ushort blockClientId;

		// Token: 0x04000045 RID: 69
		private readonly ushort blockServerId;

		// Token: 0x04000046 RID: 70
		private readonly ushort blockSessionId;

		// Token: 0x04000047 RID: 71
		private readonly ushort blockRequestId;

		// Token: 0x04000048 RID: 72
		private readonly uint blockTimeSinceRequest;

		// Token: 0x04000049 RID: 73
		private readonly uint blockTimeToFailRequest;

		// Token: 0x0400004A RID: 74
		private readonly uint blockResultCode;

		// Token: 0x0400004B RID: 75
		private readonly byte blockRequestOperation;
	}
}
