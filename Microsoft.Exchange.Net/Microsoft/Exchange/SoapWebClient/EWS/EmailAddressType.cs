using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001CE RID: 462
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailAddressType : BaseEmailAddressType
	{
		// Token: 0x04000C3F RID: 3135
		public string Name;

		// Token: 0x04000C40 RID: 3136
		public string EmailAddress;

		// Token: 0x04000C41 RID: 3137
		public string RoutingType;

		// Token: 0x04000C42 RID: 3138
		public MailboxTypeType MailboxType;

		// Token: 0x04000C43 RID: 3139
		[XmlIgnore]
		public bool MailboxTypeSpecified;

		// Token: 0x04000C44 RID: 3140
		public ItemIdType ItemId;

		// Token: 0x04000C45 RID: 3141
		public string OriginalDisplayName;
	}
}
