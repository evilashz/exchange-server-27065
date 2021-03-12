using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000031 RID: 49
	internal sealed class PerfRequestIdAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004235 File Offset: 0x00002435
		public PerfRequestIdAuxiliaryBlock(ushort blockSessionId, ushort blockRequestId) : base(1, AuxiliaryBlockTypes.PerfRequestId)
		{
			this.blockSessionId = blockSessionId;
			this.blockRequestId = blockRequestId;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000424D File Offset: 0x0000244D
		internal PerfRequestIdAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.blockSessionId = reader.ReadUInt16();
			this.blockRequestId = reader.ReadUInt16();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000426E File Offset: 0x0000246E
		public ushort BlockSessionId
		{
			get
			{
				return this.blockSessionId;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004276 File Offset: 0x00002476
		public ushort BlockRequestId
		{
			get
			{
				return this.blockRequestId;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000427E File Offset: 0x0000247E
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.blockSessionId);
			writer.WriteUInt16(this.blockRequestId);
		}

		// Token: 0x04000099 RID: 153
		private readonly ushort blockSessionId;

		// Token: 0x0400009A RID: 154
		private readonly ushort blockRequestId;
	}
}
