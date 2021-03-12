using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200011C RID: 284
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class WebClientUrlCollectionSetting : UserSetting
	{
		// Token: 0x040005C9 RID: 1481
		[XmlArray(IsNullable = true)]
		public WebClientUrl[] WebClientUrls;
	}
}
