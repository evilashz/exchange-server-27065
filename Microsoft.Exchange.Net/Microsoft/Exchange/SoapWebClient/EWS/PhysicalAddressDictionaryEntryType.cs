using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000233 RID: 563
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PhysicalAddressDictionaryEntryType
	{
		// Token: 0x04000E95 RID: 3733
		public string Street;

		// Token: 0x04000E96 RID: 3734
		public string City;

		// Token: 0x04000E97 RID: 3735
		public string State;

		// Token: 0x04000E98 RID: 3736
		public string CountryOrRegion;

		// Token: 0x04000E99 RID: 3737
		public string PostalCode;

		// Token: 0x04000E9A RID: 3738
		[XmlAttribute]
		public PhysicalAddressKeyType Key;
	}
}
