using System;
using System.IO;
using System.Xml;

namespace DataContractSerializerProject
{
	// Token: 0x02000019 RID: 25
	internal class EntityXmlTextWriter : XmlTextWriter
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003D73 File Offset: 0x00001F73
		public EntityXmlTextWriter(StringWriter textWriter) : base(textWriter)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D7C File Offset: 0x00001F7C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			base.WriteStartElement(string.Empty, localName, string.Empty);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D8F File Offset: 0x00001F8F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			base.WriteStartAttribute(string.Empty, localName, string.Empty);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003DA2 File Offset: 0x00001FA2
		public override void WriteQualifiedName(string localName, string ns)
		{
			base.WriteQualifiedName(localName, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DAC File Offset: 0x00001FAC
		public override string LookupPrefix(string ns)
		{
			return string.Empty;
		}
	}
}
