using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003A2 RID: 930
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetClientExtensionUserParametersType
	{
		// Token: 0x040014AD RID: 5293
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserEnabledExtensions;

		// Token: 0x040014AE RID: 5294
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserDisabledExtensions;

		// Token: 0x040014AF RID: 5295
		[XmlAttribute]
		public string UserId;

		// Token: 0x040014B0 RID: 5296
		[XmlAttribute]
		public bool EnabledOnly;

		// Token: 0x040014B1 RID: 5297
		[XmlIgnore]
		public bool EnabledOnlySpecified;
	}
}
