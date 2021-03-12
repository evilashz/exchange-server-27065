using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200040C RID: 1036
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class AddNewImContactToGroupType : BaseRequestType
	{
		// Token: 0x040015F2 RID: 5618
		public string ImAddress;

		// Token: 0x040015F3 RID: 5619
		public string DisplayName;

		// Token: 0x040015F4 RID: 5620
		public ItemIdType GroupId;
	}
}
