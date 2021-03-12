using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000C RID: 12
	internal abstract class BasePerfMdbSuccessAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000320C File Offset: 0x0000140C
		protected BasePerfMdbSuccessAuxiliaryBlock(byte blockVersion, AuxiliaryBlockTypes blockType, ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(blockVersion, blockType)
		{
			if (blockType != AuxiliaryBlockTypes.PerfMdbSuccess && blockType != AuxiliaryBlockTypes.PerfBgMdbSuccess && blockType != AuxiliaryBlockTypes.PerfFgMdbSuccess)
			{
				throw new ArgumentException("Type must be either PerfMdbSuccess, PerfBgMdbSuccess or PerfFgMdbSuccess", "blockType");
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
			this.blockTimeToCompleteRequest = blockTimeToCompleteRequest;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000327C File Offset: 0x0000147C
		protected BasePerfMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
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
			this.blockTimeToCompleteRequest = reader.ReadUInt32();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000032FD File Offset: 0x000014FD
		public ushort BlockProcessId
		{
			get
			{
				return this.blockProcessId;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003305 File Offset: 0x00001505
		public ushort BlockClientId
		{
			get
			{
				return this.blockClientId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000330D File Offset: 0x0000150D
		public ushort BlockServerId
		{
			get
			{
				return this.blockServerId;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003315 File Offset: 0x00001515
		public ushort BlockSessionId
		{
			get
			{
				return this.blockSessionId;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000331D File Offset: 0x0000151D
		public ushort BlockRequestId
		{
			get
			{
				return this.blockRequestId;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003325 File Offset: 0x00001525
		public uint BlockTimeSinceRequest
		{
			get
			{
				return this.blockTimeSinceRequest;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000332D File Offset: 0x0000152D
		public uint BlockTimeToCompleteRequest
		{
			get
			{
				return this.blockTimeToCompleteRequest;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003335 File Offset: 0x00001535
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientTimeStampedEventArgs(this.blockTimeSinceRequest, ClientPerformanceEventType.RpcAttempted));
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.RpcSucceeded));
			sink.ReportLatency(TimeSpan.FromMilliseconds(this.blockTimeToCompleteRequest));
			base.ReportClientPerformance(sink);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003370 File Offset: 0x00001570
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
			writer.WriteUInt32(this.blockTimeToCompleteRequest);
		}

		// Token: 0x04000053 RID: 83
		private readonly ushort blockProcessId;

		// Token: 0x04000054 RID: 84
		private readonly ushort blockClientId;

		// Token: 0x04000055 RID: 85
		private readonly ushort blockServerId;

		// Token: 0x04000056 RID: 86
		private readonly ushort blockSessionId;

		// Token: 0x04000057 RID: 87
		private readonly ushort blockRequestId;

		// Token: 0x04000058 RID: 88
		private readonly uint blockTimeSinceRequest;

		// Token: 0x04000059 RID: 89
		private readonly uint blockTimeToCompleteRequest;
	}
}
