using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000014 RID: 20
	internal sealed class DiagCtxCtxDataAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000378B File Offset: 0x0000198B
		public DiagCtxCtxDataAuxiliaryBlock(byte[] contextData) : base(1, AuxiliaryBlockTypes.DiagCtxCtxData)
		{
			this.contextData = contextData;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000379D File Offset: 0x0000199D
		internal DiagCtxCtxDataAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.contextData = reader.ReadBytes((uint)(reader.Length - reader.Position));
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000037C0 File Offset: 0x000019C0
		public byte[] ContextData
		{
			get
			{
				return this.contextData;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000037C8 File Offset: 0x000019C8
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBytes(this.contextData);
		}

		// Token: 0x04000071 RID: 113
		private readonly byte[] contextData;
	}
}
