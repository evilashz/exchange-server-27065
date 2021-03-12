using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000231 RID: 561
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailAddressDictionaryEntryType
	{
		// Token: 0x04000E8B RID: 3723
		[XmlAttribute]
		public EmailAddressKeyType Key;

		// Token: 0x04000E8C RID: 3724
		[XmlAttribute]
		public string Name;

		// Token: 0x04000E8D RID: 3725
		[XmlAttribute]
		public string RoutingType;

		// Token: 0x04000E8E RID: 3726
		[XmlAttribute]
		public MailboxTypeType MailboxType;

		// Token: 0x04000E8F RID: 3727
		[XmlIgnore]
		public bool MailboxTypeSpecified;

		// Token: 0x04000E90 RID: 3728
		[XmlText]
		public string Value;
	}
}
