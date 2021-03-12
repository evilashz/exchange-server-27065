using System;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E4 RID: 484
	internal interface IADMailStorage
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001655 RID: 5717
		// (set) Token: 0x06001656 RID: 5718
		ADObjectId Database { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001657 RID: 5719
		// (set) Token: 0x06001658 RID: 5720
		DeletedItemRetention DeletedItemFlags { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001659 RID: 5721
		// (set) Token: 0x0600165A RID: 5722
		bool DeliverToMailboxAndForward { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600165B RID: 5723
		// (set) Token: 0x0600165C RID: 5724
		Guid ExchangeGuid { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165E RID: 5726
		RawSecurityDescriptor ExchangeSecurityDescriptor { get; set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x06001660 RID: 5728
		ExternalOofOptions ExternalOofOptions { get; set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001661 RID: 5729
		// (set) Token: 0x06001662 RID: 5730
		EnhancedTimeSpan RetainDeletedItemsFor { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001663 RID: 5731
		bool IsMailboxEnabled { get; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001664 RID: 5732
		// (set) Token: 0x06001665 RID: 5733
		ADObjectId OfflineAddressBook { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001666 RID: 5734
		// (set) Token: 0x06001667 RID: 5735
		Unlimited<ByteQuantifiedSize> ProhibitSendQuota { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001668 RID: 5736
		// (set) Token: 0x06001669 RID: 5737
		Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600166A RID: 5738
		// (set) Token: 0x0600166B RID: 5739
		string ServerLegacyDN { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600166C RID: 5740
		string ServerName { get; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600166D RID: 5741
		// (set) Token: 0x0600166E RID: 5742
		bool? UseDatabaseQuotaDefaults { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600166F RID: 5743
		// (set) Token: 0x06001670 RID: 5744
		Unlimited<ByteQuantifiedSize> IssueWarningQuota { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001671 RID: 5745
		// (set) Token: 0x06001672 RID: 5746
		ByteQuantifiedSize RulesQuota { get; set; }
	}
}
