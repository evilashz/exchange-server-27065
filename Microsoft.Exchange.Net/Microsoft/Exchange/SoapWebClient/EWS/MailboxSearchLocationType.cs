using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000281 RID: 641
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MailboxSearchLocationType
	{
		// Token: 0x0400106E RID: 4206
		PrimaryOnly,
		// Token: 0x0400106F RID: 4207
		ArchiveOnly,
		// Token: 0x04001070 RID: 4208
		All
	}
}
