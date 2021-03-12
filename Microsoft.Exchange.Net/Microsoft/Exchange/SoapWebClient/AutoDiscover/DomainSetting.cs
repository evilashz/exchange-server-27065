using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000111 RID: 273
	[XmlInclude(typeof(DomainStringSetting))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DomainSetting
	{
		// Token: 0x040005A2 RID: 1442
		[XmlElement(IsNullable = true)]
		public string Name;
	}
}
