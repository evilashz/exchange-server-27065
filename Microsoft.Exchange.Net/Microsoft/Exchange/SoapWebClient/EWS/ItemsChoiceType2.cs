using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E0 RID: 736
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[Serializable]
	public enum ItemsChoiceType2
	{
		// Token: 0x04001264 RID: 4708
		Create,
		// Token: 0x04001265 RID: 4709
		Delete,
		// Token: 0x04001266 RID: 4710
		ReadFlagChange,
		// Token: 0x04001267 RID: 4711
		Update
	}
}
