using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200000B RID: 11
	internal abstract class BasePerfGcSuccessAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003050 File Offset: 0x00001250
		protected BasePerfGcSuccessAuxiliaryBlock(byte blockVersion, AuxiliaryBlockTypes blockType, ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(blockVersion, blockType)
		{
			if (blockType != AuxiliaryBlockTypes.PerfGcSuccess && blockType != AuxiliaryBlockTypes.PerfBgGcSuccess && blockType != AuxiliaryBlockTypes.PerfFgGcSuccess)
			{
				throw new ArgumentException("Type must be either PerfGcSuccess, PerfBgGcSuccess or PerfFgGcSuccess", "blockType");
			}
			if (blockVersion == 2)
			{
				this.blockProcessId = blockProcessId;
			}
			this.blockClientId = blockClientId;
			this.blockServerId = blockServerId;
			this.blockSessionId = blockSessionId;
			this.blockTimeSinceRequest = blockTimeSinceRequest;
			this.blockTimeToCompleteRequest = blockTimeToCompleteRequest;
			this.blockRequestOperation = blockRequestOperation;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030C0 File Offset: 0x000012C0
		protected BasePerfGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
			if (base.Version == 2)
			{
				this.blockProcessId = reader.ReadUInt16();
			}
			this.blockClientId = reader.ReadUInt16();
			this.blockServerId = reader.ReadUInt16();
			this.blockSessionId = reader.ReadUInt16();
			if (base.Version == 1)
			{
				reader.ReadInt16();
			}
			this.blockTimeSinceRequest = reader.ReadUInt32();
			this.blockTimeToCompleteRequest = reader.ReadUInt32();
			this.blockRequestOperation = reader.ReadByte();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003141 File Offset: 0x00001341
		public ushort BlockProcessId
		{
			get
			{
				return this.blockProcessId;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003149 File Offset: 0x00001349
		public ushort BlockClientId
		{
			get
			{
				return this.blockClientId;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003151 File Offset: 0x00001351
		public ushort BlockServerId
		{
			get
			{
				return this.blockServerId;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003159 File Offset: 0x00001359
		public ushort BlockSessionId
		{
			get
			{
				return this.blockSessionId;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003161 File Offset: 0x00001361
		public uint BlockTimeSinceRequest
		{
			get
			{
				return this.blockTimeSinceRequest;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003169 File Offset: 0x00001369
		public uint BlockTimeToCompleteRequest
		{
			get
			{
				return this.blockTimeToCompleteRequest;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003171 File Offset: 0x00001371
		public byte BlockRequestOperation
		{
			get
			{
				return this.blockRequestOperation;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000317C File Offset: 0x0000137C
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
			if (base.Version == 1)
			{
				writer.WriteInt16(0);
			}
			writer.WriteUInt32(this.blockTimeSinceRequest);
			writer.WriteUInt32(this.blockTimeToCompleteRequest);
			writer.WriteByte(this.blockRequestOperation);
			writer.WriteByte(0);
			writer.WriteInt16(0);
		}

		// Token: 0x0400004C RID: 76
		private readonly ushort blockProcessId;

		// Token: 0x0400004D RID: 77
		private readonly ushort blockClientId;

		// Token: 0x0400004E RID: 78
		private readonly ushort blockServerId;

		// Token: 0x0400004F RID: 79
		private readonly ushort blockSessionId;

		// Token: 0x04000050 RID: 80
		private readonly uint blockTimeSinceRequest;

		// Token: 0x04000051 RID: 81
		private readonly uint blockTimeToCompleteRequest;

		// Token: 0x04000052 RID: 82
		private readonly byte blockRequestOperation;
	}
}
