using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D4 RID: 468
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class DistinguishedFolderIdType : BaseFolderIdType
	{
		// Token: 0x04000C54 RID: 3156
		public EmailAddressType Mailbox;

		// Token: 0x04000C55 RID: 3157
		[XmlAttribute]
		public DistinguishedFolderIdNameType Id;

		// Token: 0x04000C56 RID: 3158
		[XmlAttribute]
		public string ChangeKey;
	}
}
