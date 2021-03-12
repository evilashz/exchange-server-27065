using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000123 RID: 291
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class DomainResponse : AutodiscoverResponse
	{
		// Token: 0x040005D6 RID: 1494
		[XmlArray(IsNullable = true)]
		public DomainSettingError[] DomainSettingErrors;

		// Token: 0x040005D7 RID: 1495
		[XmlArray(IsNullable = true)]
		public DomainSetting[] DomainSettings;

		// Token: 0x040005D8 RID: 1496
		[XmlElement(IsNullable = true)]
		public string RedirectTarget;
	}
}
