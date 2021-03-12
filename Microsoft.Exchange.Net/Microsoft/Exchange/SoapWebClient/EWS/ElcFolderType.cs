using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001B2 RID: 434
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ElcFolderType
	{
		// Token: 0x04000A12 RID: 2578
		Calendar,
		// Token: 0x04000A13 RID: 2579
		Contacts,
		// Token: 0x04000A14 RID: 2580
		DeletedItems,
		// Token: 0x04000A15 RID: 2581
		Drafts,
		// Token: 0x04000A16 RID: 2582
		Inbox,
		// Token: 0x04000A17 RID: 2583
		JunkEmail,
		// Token: 0x04000A18 RID: 2584
		Journal,
		// Token: 0x04000A19 RID: 2585
		Notes,
		// Token: 0x04000A1A RID: 2586
		Outbox,
		// Token: 0x04000A1B RID: 2587
		SentItems,
		// Token: 0x04000A1C RID: 2588
		Tasks,
		// Token: 0x04000A1D RID: 2589
		All,
		// Token: 0x04000A1E RID: 2590
		ManagedCustomFolder,
		// Token: 0x04000A1F RID: 2591
		RssSubscriptions,
		// Token: 0x04000A20 RID: 2592
		SyncIssues,
		// Token: 0x04000A21 RID: 2593
		ConversationHistory,
		// Token: 0x04000A22 RID: 2594
		Personal,
		// Token: 0x04000A23 RID: 2595
		RecoverableItems,
		// Token: 0x04000A24 RID: 2596
		NonIpmRoot
	}
}
