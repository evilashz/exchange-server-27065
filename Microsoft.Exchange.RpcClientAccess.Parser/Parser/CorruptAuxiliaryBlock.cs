using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000012 RID: 18
	internal sealed class CorruptAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000036FA File Offset: 0x000018FA
		internal CorruptAuxiliaryBlock(ArraySegment<byte> blockData) : base(0, (AuxiliaryBlockTypes)0)
		{
			this.data = blockData;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000370B File Offset: 0x0000190B
		internal CorruptAuxiliaryBlock(Reader reader) : base(0, (AuxiliaryBlockTypes)0)
		{
			this.data = reader.ReadArraySegment((uint)(reader.Length - reader.Position));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000372F File Offset: 0x0000192F
		protected override void Serialize(Writer writer)
		{
			throw new InvalidOperationException("CorruptAuxiliaryBlocks cannot be serialized");
		}

		// Token: 0x0400006F RID: 111
		private readonly ArraySegment<byte> data;
	}
}
