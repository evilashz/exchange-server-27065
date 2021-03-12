using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000129 RID: 297
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetOrganizationRelationshipSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x040005DF RID: 1503
		[XmlArrayItem("Domain")]
		[XmlArray(IsNullable = true)]
		public string[] Domains;
	}
}
