using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000187 RID: 391
	[XmlRoot("TimeZoneContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TimeZoneContextType : SoapHeader
	{
		// Token: 0x040007AE RID: 1966
		public TimeZoneDefinitionType TimeZoneDefinition;

		// Token: 0x040007AF RID: 1967
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
