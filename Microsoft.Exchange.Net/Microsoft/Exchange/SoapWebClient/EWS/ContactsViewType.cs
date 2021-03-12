using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E5 RID: 997
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ContactsViewType : BasePagingType
	{
		// Token: 0x04001595 RID: 5525
		[XmlAttribute]
		public string InitialName;

		// Token: 0x04001596 RID: 5526
		[XmlAttribute]
		public string FinalName;
	}
}
