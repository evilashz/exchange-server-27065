using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200026C RID: 620
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MemberType
	{
		// Token: 0x0400101A RID: 4122
		public EmailAddressType Mailbox;

		// Token: 0x0400101B RID: 4123
		public MemberStatusType Status;

		// Token: 0x0400101C RID: 4124
		[XmlIgnore]
		public bool StatusSpecified;

		// Token: 0x0400101D RID: 4125
		[XmlAttribute]
		public string Key;
	}
}
