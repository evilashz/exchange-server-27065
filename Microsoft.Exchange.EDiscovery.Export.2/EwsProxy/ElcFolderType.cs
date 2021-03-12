using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000D1 RID: 209
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ElcFolderType
	{
		// Token: 0x040005C0 RID: 1472
		Calendar,
		// Token: 0x040005C1 RID: 1473
		Contacts,
		// Token: 0x040005C2 RID: 1474
		DeletedItems,
		// Token: 0x040005C3 RID: 1475
		Drafts,
		// Token: 0x040005C4 RID: 1476
		Inbox,
		// Token: 0x040005C5 RID: 1477
		JunkEmail,
		// Token: 0x040005C6 RID: 1478
		Journal,
		// Token: 0x040005C7 RID: 1479
		Notes,
		// Token: 0x040005C8 RID: 1480
		Outbox,
		// Token: 0x040005C9 RID: 1481
		SentItems,
		// Token: 0x040005CA RID: 1482
		Tasks,
		// Token: 0x040005CB RID: 1483
		All,
		// Token: 0x040005CC RID: 1484
		ManagedCustomFolder,
		// Token: 0x040005CD RID: 1485
		RssSubscriptions,
		// Token: 0x040005CE RID: 1486
		SyncIssues,
		// Token: 0x040005CF RID: 1487
		ConversationHistory,
		// Token: 0x040005D0 RID: 1488
		Personal,
		// Token: 0x040005D1 RID: 1489
		RecoverableItems,
		// Token: 0x040005D2 RID: 1490
		NonIpmRoot
	}
}
