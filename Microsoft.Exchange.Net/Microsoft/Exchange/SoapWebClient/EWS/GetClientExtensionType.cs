using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000417 RID: 1047
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetClientExtensionType : BaseRequestType
	{
		// Token: 0x04001608 RID: 5640
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RequestedExtensionIds;

		// Token: 0x04001609 RID: 5641
		public GetClientExtensionUserParametersType UserParameters;

		// Token: 0x0400160A RID: 5642
		public bool IsDebug;

		// Token: 0x0400160B RID: 5643
		[XmlIgnore]
		public bool IsDebugSpecified;
	}
}
