using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000119 RID: 281
	[XmlInclude(typeof(AlternateMailboxCollectionSetting))]
	[XmlInclude(typeof(DocumentSharingLocationCollectionSetting))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlInclude(typeof(WebClientUrlCollectionSetting))]
	[XmlInclude(typeof(StringSetting))]
	[XmlInclude(typeof(ProtocolConnectionCollectionSetting))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UserSetting
	{
		// Token: 0x040005C6 RID: 1478
		[XmlElement(IsNullable = true)]
		public string Name;
	}
}
