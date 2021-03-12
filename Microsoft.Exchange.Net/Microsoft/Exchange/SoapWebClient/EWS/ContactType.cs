using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000262 RID: 610
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ContactType : EntityType
	{
		// Token: 0x04000FAD RID: 4013
		public string PersonName;

		// Token: 0x04000FAE RID: 4014
		public string BusinessName;

		// Token: 0x04000FAF RID: 4015
		[XmlArrayItem("Phone", IsNullable = false)]
		public PhoneType[] PhoneNumbers;

		// Token: 0x04000FB0 RID: 4016
		[XmlArrayItem("Url", IsNullable = false)]
		public string[] Urls;

		// Token: 0x04000FB1 RID: 4017
		[XmlArrayItem("EmailAddress", IsNullable = false)]
		public string[] EmailAddresses;

		// Token: 0x04000FB2 RID: 4018
		[XmlArrayItem("Address", IsNullable = false)]
		public string[] Addresses;

		// Token: 0x04000FB3 RID: 4019
		public string ContactString;
	}
}
