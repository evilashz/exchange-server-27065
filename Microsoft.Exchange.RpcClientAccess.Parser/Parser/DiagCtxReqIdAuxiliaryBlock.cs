using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000015 RID: 21
	internal sealed class DiagCtxReqIdAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000037DD File Offset: 0x000019DD
		public DiagCtxReqIdAuxiliaryBlock(int requestId) : base(1, AuxiliaryBlockTypes.DiagCtxReqId)
		{
			this.requestId = requestId;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037EF File Offset: 0x000019EF
		internal DiagCtxReqIdAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.requestId = reader.ReadInt32();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003804 File Offset: 0x00001A04
		public int RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000380C File Offset: 0x00001A0C
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.requestId);
		}

		// Token: 0x04000072 RID: 114
		private readonly int requestId;
	}
}
