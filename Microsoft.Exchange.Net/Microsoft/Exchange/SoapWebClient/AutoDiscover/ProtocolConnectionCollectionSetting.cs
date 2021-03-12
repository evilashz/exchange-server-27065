using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200011E RID: 286
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtocolConnectionCollectionSetting : UserSetting
	{
		// Token: 0x040005CB RID: 1483
		[XmlArray(IsNullable = true)]
		public ProtocolConnection[] ProtocolConnections;
	}
}
