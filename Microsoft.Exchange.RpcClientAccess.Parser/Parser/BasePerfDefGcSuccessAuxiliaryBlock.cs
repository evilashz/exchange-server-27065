using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000008 RID: 8
	internal abstract class BasePerfDefGcSuccessAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002BF0 File Offset: 0x00000DF0
		protected BasePerfDefGcSuccessAuxiliaryBlock(AuxiliaryBlockTypes blockType, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(1, blockType)
		{
			if (blockType != AuxiliaryBlockTypes.PerfDefGcSuccess && blockType != AuxiliaryBlockTypes.PerfBgDefGcSuccess && blockType != AuxiliaryBlockTypes.PerfFgDefGcSuccess)
			{
				throw new ArgumentException("Type must be either PerfDefGcSuccess, PerfBgDefGcSuccess or PerfFgDefGcSuccess", "blockType");
			}
			this.blockServerId = blockServerId;
			this.blockSessionId = blockSessionId;
			this.blockTimeSinceRequest = blockTimeSinceRequest;
			this.blockTimeToCompleteRequest = blockTimeToCompleteRequest;
			this.blockRequestOperation = blockRequestOperation;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal BasePerfDefGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.blockServerId = reader.ReadUInt16();
			this.blockSessionId = reader.ReadUInt16();
			this.blockTimeSinceRequest = reader.ReadUInt32();
			this.blockTimeToCompleteRequest = reader.ReadUInt32();
			this.blockRequestOperation = reader.ReadByte();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C9C File Offset: 0x00000E9C
		public ushort BlockServerId
		{
			get
			{
				return this.blockServerId;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public ushort BlockSessionId
		{
			get
			{
				return this.blockSessionId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002CAC File Offset: 0x00000EAC
		public uint BlockTimeSinceRequest
		{
			get
			{
				return this.blockTimeSinceRequest;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public uint BlockTimeToCompleteRequest
		{
			get
			{
				return this.blockTimeToCompleteRequest;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002CBC File Offset: 0x00000EBC
		public byte BlockRequestOperation
		{
			get
			{
				return this.blockRequestOperation;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CC4 File Offset: 0x00000EC4
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.blockServerId);
			writer.WriteUInt16(this.blockSessionId);
			writer.WriteUInt32(this.blockTimeSinceRequest);
			writer.WriteUInt32(this.blockTimeToCompleteRequest);
			writer.WriteByte(this.blockRequestOperation);
			writer.WriteByte(0);
			writer.WriteInt16(0);
		}

		// Token: 0x0400003B RID: 59
		private readonly ushort blockServerId;

		// Token: 0x0400003C RID: 60
		private readonly ushort blockSessionId;

		// Token: 0x0400003D RID: 61
		private readonly uint blockTimeSinceRequest;

		// Token: 0x0400003E RID: 62
		private readonly uint blockTimeToCompleteRequest;

		// Token: 0x0400003F RID: 63
		private readonly byte blockRequestOperation;
	}
}
