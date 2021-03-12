using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086F RID: 2159
	[XmlType(TypeName = "SearchItemKindType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SearchItemKind
	{
		// Token: 0x0400238C RID: 9100
		Email,
		// Token: 0x0400238D RID: 9101
		Meetings,
		// Token: 0x0400238E RID: 9102
		Tasks,
		// Token: 0x0400238F RID: 9103
		Notes,
		// Token: 0x04002390 RID: 9104
		Docs,
		// Token: 0x04002391 RID: 9105
		Journals,
		// Token: 0x04002392 RID: 9106
		Contacts,
		// Token: 0x04002393 RID: 9107
		Im,
		// Token: 0x04002394 RID: 9108
		Voicemail,
		// Token: 0x04002395 RID: 9109
		Faxes,
		// Token: 0x04002396 RID: 9110
		Posts,
		// Token: 0x04002397 RID: 9111
		Rssfeeds
	}
}
