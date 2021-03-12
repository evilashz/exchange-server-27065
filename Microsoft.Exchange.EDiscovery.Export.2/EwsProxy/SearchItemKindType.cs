using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000344 RID: 836
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SearchItemKindType
	{
		// Token: 0x0400120E RID: 4622
		Email,
		// Token: 0x0400120F RID: 4623
		Meetings,
		// Token: 0x04001210 RID: 4624
		Tasks,
		// Token: 0x04001211 RID: 4625
		Notes,
		// Token: 0x04001212 RID: 4626
		Docs,
		// Token: 0x04001213 RID: 4627
		Journals,
		// Token: 0x04001214 RID: 4628
		Contacts,
		// Token: 0x04001215 RID: 4629
		Im,
		// Token: 0x04001216 RID: 4630
		Voicemail,
		// Token: 0x04001217 RID: 4631
		Faxes,
		// Token: 0x04001218 RID: 4632
		Posts,
		// Token: 0x04001219 RID: 4633
		Rssfeeds
	}
}
