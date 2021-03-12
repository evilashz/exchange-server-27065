using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200040B RID: 1035
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AddNewTelUriContactToGroupType : BaseRequestType
	{
		// Token: 0x040015EE RID: 5614
		public string TelUriAddress;

		// Token: 0x040015EF RID: 5615
		public string ImContactSipUriAddress;

		// Token: 0x040015F0 RID: 5616
		public string ImTelephoneNumber;

		// Token: 0x040015F1 RID: 5617
		public ItemIdType GroupId;
	}
}
