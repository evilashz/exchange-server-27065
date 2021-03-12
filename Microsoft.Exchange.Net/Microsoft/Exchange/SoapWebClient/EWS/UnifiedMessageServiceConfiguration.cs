using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B1 RID: 689
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class UnifiedMessageServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x040011E6 RID: 4582
		public bool UmEnabled;

		// Token: 0x040011E7 RID: 4583
		public string PlayOnPhoneDialString;

		// Token: 0x040011E8 RID: 4584
		public bool PlayOnPhoneEnabled;
	}
}
