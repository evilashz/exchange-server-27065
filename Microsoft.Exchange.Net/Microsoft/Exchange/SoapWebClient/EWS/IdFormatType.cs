using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002D7 RID: 727
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum IdFormatType
	{
		// Token: 0x0400124D RID: 4685
		EwsLegacyId,
		// Token: 0x0400124E RID: 4686
		EwsId,
		// Token: 0x0400124F RID: 4687
		EntryId,
		// Token: 0x04001250 RID: 4688
		HexEntryId,
		// Token: 0x04001251 RID: 4689
		StoreId,
		// Token: 0x04001252 RID: 4690
		OwaId
	}
}
