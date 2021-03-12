using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000036 RID: 54
	internal sealed class ServerInformationAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000104 RID: 260 RVA: 0x000048E5 File Offset: 0x00002AE5
		public ServerInformationAuxiliaryBlock(string serverInformation) : base(1, AuxiliaryBlockTypes.ServerInformation)
		{
			if (serverInformation == null)
			{
				throw new ArgumentNullException("serverInformation");
			}
			this.serverInformation = serverInformation;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004905 File Offset: 0x00002B05
		internal ServerInformationAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.serverInformation = reader.ReadUnicodeString(StringFlags.IncludeNull);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000491B File Offset: 0x00002B1B
		public string ServerInformation
		{
			get
			{
				return this.serverInformation;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004923 File Offset: 0x00002B23
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUnicodeString(this.ServerInformation, StringFlags.IncludeNull);
		}

		// Token: 0x040000B7 RID: 183
		private readonly string serverInformation;
	}
}
