using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000116 RID: 278
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AlternateMailbox
	{
		// Token: 0x040005BB RID: 1467
		[XmlElement(IsNullable = true)]
		public string Type;

		// Token: 0x040005BC RID: 1468
		[XmlElement(IsNullable = true)]
		public string DisplayName;

		// Token: 0x040005BD RID: 1469
		[XmlElement(IsNullable = true)]
		public string LegacyDN;

		// Token: 0x040005BE RID: 1470
		[XmlElement(IsNullable = true)]
		public string Server;

		// Token: 0x040005BF RID: 1471
		[XmlElement(IsNullable = true)]
		public string SmtpAddress;

		// Token: 0x040005C0 RID: 1472
		[XmlElement(IsNullable = true)]
		public string OwnerSmtpAddress;
	}
}
