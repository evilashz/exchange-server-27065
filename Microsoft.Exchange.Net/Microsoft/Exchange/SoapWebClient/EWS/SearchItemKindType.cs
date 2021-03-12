using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000425 RID: 1061
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SearchItemKindType
	{
		// Token: 0x04001660 RID: 5728
		Email,
		// Token: 0x04001661 RID: 5729
		Meetings,
		// Token: 0x04001662 RID: 5730
		Tasks,
		// Token: 0x04001663 RID: 5731
		Notes,
		// Token: 0x04001664 RID: 5732
		Docs,
		// Token: 0x04001665 RID: 5733
		Journals,
		// Token: 0x04001666 RID: 5734
		Contacts,
		// Token: 0x04001667 RID: 5735
		Im,
		// Token: 0x04001668 RID: 5736
		Voicemail,
		// Token: 0x04001669 RID: 5737
		Faxes,
		// Token: 0x0400166A RID: 5738
		Posts,
		// Token: 0x0400166B RID: 5739
		Rssfeeds
	}
}
