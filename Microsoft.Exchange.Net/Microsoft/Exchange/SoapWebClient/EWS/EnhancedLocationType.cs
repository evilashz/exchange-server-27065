using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000229 RID: 553
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EnhancedLocationType
	{
		// Token: 0x04000E2C RID: 3628
		public string DisplayName;

		// Token: 0x04000E2D RID: 3629
		public string Annotation;

		// Token: 0x04000E2E RID: 3630
		public PersonaPostalAddressType PostalAddress;
	}
}
