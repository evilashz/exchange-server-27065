using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000121 RID: 289
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetOrganizationRelationshipSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x040005D2 RID: 1490
		[XmlArray(IsNullable = true)]
		public OrganizationRelationshipSettings[] OrganizationRelationshipSettingsCollection;
	}
}
