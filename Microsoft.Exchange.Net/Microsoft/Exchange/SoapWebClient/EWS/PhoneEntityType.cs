using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000261 RID: 609
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PhoneEntityType : EntityType
	{
		// Token: 0x04000FAA RID: 4010
		public string OriginalPhoneString;

		// Token: 0x04000FAB RID: 4011
		public string PhoneString;

		// Token: 0x04000FAC RID: 4012
		public string Type;
	}
}
