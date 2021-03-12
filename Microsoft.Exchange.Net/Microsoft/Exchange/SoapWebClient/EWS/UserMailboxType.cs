using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200028C RID: 652
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserMailboxType
	{
		// Token: 0x040010A4 RID: 4260
		[XmlAttribute]
		public string Id;

		// Token: 0x040010A5 RID: 4261
		[XmlAttribute]
		public bool IsArchive;
	}
}
