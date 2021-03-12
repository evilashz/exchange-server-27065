using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200001B RID: 27
	internal sealed class IdentityCorrelationAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003980 File Offset: 0x00001B80
		public IdentityCorrelationAuxiliaryBlock(string key, string value) : base(1, AuxiliaryBlockTypes.IdentityCorrelationInfo)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003999 File Offset: 0x00001B99
		internal IdentityCorrelationAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.key = reader.ReadUnicodeString(StringFlags.IncludeNull);
			this.value = reader.ReadUnicodeString(StringFlags.IncludeNull);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000039BC File Offset: 0x00001BBC
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000039C4 File Offset: 0x00001BC4
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000039CC File Offset: 0x00001BCC
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUnicodeString(this.key, StringFlags.IncludeNull);
			writer.WriteUnicodeString(this.value, StringFlags.IncludeNull);
		}

		// Token: 0x0400007E RID: 126
		private readonly string key;

		// Token: 0x0400007F RID: 127
		private readonly string value;
	}
}
