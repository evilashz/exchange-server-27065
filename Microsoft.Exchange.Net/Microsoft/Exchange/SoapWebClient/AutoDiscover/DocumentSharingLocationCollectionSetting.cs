using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200011A RID: 282
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DocumentSharingLocationCollectionSetting : UserSetting
	{
		// Token: 0x040005C7 RID: 1479
		[XmlArrayItem(IsNullable = false)]
		public DocumentSharingLocation[] DocumentSharingLocations;
	}
}
