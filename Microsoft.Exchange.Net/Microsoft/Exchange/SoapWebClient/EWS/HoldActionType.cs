using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200041F RID: 1055
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum HoldActionType
	{
		// Token: 0x04001637 RID: 5687
		Create,
		// Token: 0x04001638 RID: 5688
		Update,
		// Token: 0x04001639 RID: 5689
		Remove
	}
}
