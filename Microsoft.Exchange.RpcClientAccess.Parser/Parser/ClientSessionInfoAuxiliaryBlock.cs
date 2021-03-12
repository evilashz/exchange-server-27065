using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000011 RID: 17
	internal sealed class ClientSessionInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000036A8 File Offset: 0x000018A8
		public ClientSessionInfoAuxiliaryBlock(byte[] blockInfoBlob) : base(1, AuxiliaryBlockTypes.ClientSessionInfo)
		{
			this.infoBlob = blockInfoBlob;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036BA File Offset: 0x000018BA
		internal ClientSessionInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.infoBlob = reader.ReadBytes((uint)(reader.Length - reader.Position));
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036DD File Offset: 0x000018DD
		public byte[] InfoBlob
		{
			get
			{
				return this.infoBlob;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000036E5 File Offset: 0x000018E5
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBytes(this.InfoBlob);
		}

		// Token: 0x0400006E RID: 110
		private readonly byte[] infoBlob;
	}
}
