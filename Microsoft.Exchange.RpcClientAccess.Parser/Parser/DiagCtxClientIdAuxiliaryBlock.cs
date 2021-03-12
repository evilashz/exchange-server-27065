using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000013 RID: 19
	internal sealed class DiagCtxClientIdAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000373B File Offset: 0x0000193B
		public DiagCtxClientIdAuxiliaryBlock(string clientId) : base(1, AuxiliaryBlockTypes.DiagCtxClientId)
		{
			this.clientId = clientId;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000374D File Offset: 0x0000194D
		internal DiagCtxClientIdAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.clientId = reader.ReadString8(CTSGlobals.AsciiEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003768 File Offset: 0x00001968
		public string ClientId
		{
			get
			{
				return this.clientId;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003770 File Offset: 0x00001970
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteString8(this.clientId, CTSGlobals.AsciiEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x04000070 RID: 112
		private readonly string clientId;
	}
}
