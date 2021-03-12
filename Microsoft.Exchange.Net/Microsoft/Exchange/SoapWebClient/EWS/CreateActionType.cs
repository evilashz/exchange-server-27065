using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003EC RID: 1004
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CreateActionType
	{
		// Token: 0x040015A9 RID: 5545
		CreateNew,
		// Token: 0x040015AA RID: 5546
		Update,
		// Token: 0x040015AB RID: 5547
		UpdateOrCreate
	}
}
