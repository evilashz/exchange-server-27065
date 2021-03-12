using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200031A RID: 794
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfTimeZoneDefinitionType
	{
		// Token: 0x04001325 RID: 4901
		[XmlElement("TimeZoneDefinition")]
		public TimeZoneDefinitionType[] TimeZoneDefinition;
	}
}
