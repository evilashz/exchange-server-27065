using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000009 RID: 9
	internal abstract class BasePerfDefMdbSuccessAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002D22 File Offset: 0x00000F22
		protected BasePerfDefMdbSuccessAuxiliaryBlock(AuxiliaryBlockTypes blockType, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, ushort blockRequestId) : base(1, blockType)
		{
			if (blockType != AuxiliaryBlockTypes.PerfDefMdbSuccess && blockType != AuxiliaryBlockTypes.PerfBgDefMdbSuccess && blockType != AuxiliaryBlockTypes.PerfFgDefMdbSuccess)
			{
				throw new ArgumentException("Type must be PerfDefMdbSuccess, PerfBgDefMdbSuccess or PerfFgDefMdbSuccess", "blockType");
			}
			this.blockTimeSinceRequest = blockTimeSinceRequest;
			this.blockTimeToCompleteRequest = blockTimeToCompleteRequest;
			this.blockRequestId = blockRequestId;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D60 File Offset: 0x00000F60
		internal BasePerfDefMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.blockTimeSinceRequest = reader.ReadUInt32();
			this.blockTimeToCompleteRequest = reader.ReadUInt32();
			this.blockRequestId = reader.ReadUInt16();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002D8D File Offset: 0x00000F8D
		public uint BlockTimeSinceRequest
		{
			get
			{
				return this.blockTimeSinceRequest;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002D95 File Offset: 0x00000F95
		public uint BlockTimeToCompleteRequest
		{
			get
			{
				return this.blockTimeToCompleteRequest;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D9D File Offset: 0x00000F9D
		public ushort BlockRequestId
		{
			get
			{
				return this.blockRequestId;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DA5 File Offset: 0x00000FA5
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientTimeStampedEventArgs(this.blockTimeSinceRequest, ClientPerformanceEventType.RpcAttempted));
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.RpcSucceeded));
			sink.ReportLatency(TimeSpan.FromMilliseconds(this.blockTimeToCompleteRequest));
			base.ReportClientPerformance(sink);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DDF File Offset: 0x00000FDF
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.blockTimeSinceRequest);
			writer.WriteUInt32(this.blockTimeToCompleteRequest);
			writer.WriteUInt16(this.blockRequestId);
			writer.WriteInt16(0);
		}

		// Token: 0x04000040 RID: 64
		private readonly uint blockTimeSinceRequest;

		// Token: 0x04000041 RID: 65
		private readonly uint blockTimeToCompleteRequest;

		// Token: 0x04000042 RID: 66
		private readonly ushort blockRequestId;
	}
}
