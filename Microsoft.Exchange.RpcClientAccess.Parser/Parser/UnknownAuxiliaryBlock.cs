using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000039 RID: 57
	internal sealed class UnknownAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000049C8 File Offset: 0x00002BC8
		internal UnknownAuxiliaryBlock(byte blockVersion, AuxiliaryBlockTypes blockType, ArraySegment<byte> blockData) : base(blockVersion, blockType)
		{
			this.data = blockData;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000049D9 File Offset: 0x00002BD9
		internal UnknownAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.data = reader.ReadArraySegment((uint)(reader.Length - reader.Position));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000049FC File Offset: 0x00002BFC
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBytesSegment(this.data);
		}

		// Token: 0x040000B9 RID: 185
		private readonly ArraySegment<byte> data;
	}
}
