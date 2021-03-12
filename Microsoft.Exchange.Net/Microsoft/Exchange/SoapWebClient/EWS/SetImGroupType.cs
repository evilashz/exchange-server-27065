using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000401 RID: 1025
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetImGroupType : BaseRequestType
	{
		// Token: 0x040015DE RID: 5598
		public ItemIdType GroupId;

		// Token: 0x040015DF RID: 5599
		public string NewDisplayName;
	}
}
