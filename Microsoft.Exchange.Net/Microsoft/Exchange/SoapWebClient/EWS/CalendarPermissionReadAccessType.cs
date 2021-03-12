using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E6 RID: 742
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarPermissionReadAccessType
	{
		// Token: 0x04001282 RID: 4738
		None,
		// Token: 0x04001283 RID: 4739
		TimeOnly,
		// Token: 0x04001284 RID: 4740
		TimeAndSubjectAndLocation,
		// Token: 0x04001285 RID: 4741
		FullDetails
	}
}
