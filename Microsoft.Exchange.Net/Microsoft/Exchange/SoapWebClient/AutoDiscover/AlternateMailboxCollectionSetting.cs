using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200011B RID: 283
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class AlternateMailboxCollectionSetting : UserSetting
	{
		// Token: 0x040005C8 RID: 1480
		[XmlArray(IsNullable = true)]
		public AlternateMailbox[] AlternateMailboxes;
	}
}
