using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000339 RID: 825
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class GroupedItemsType
	{
		// Token: 0x040013A3 RID: 5027
		public string GroupIndex;

		// Token: 0x040013A4 RID: 5028
		public ArrayOfRealItemsType Items;
	}
}
