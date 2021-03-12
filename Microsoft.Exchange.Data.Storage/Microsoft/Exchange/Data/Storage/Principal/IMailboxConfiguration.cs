using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000271 RID: 625
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxConfiguration
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060019D1 RID: 6609
		Uri SharePointUrl { get; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060019D2 RID: 6610
		bool IsMapiEnabled { get; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060019D3 RID: 6611
		bool IsOwaEnabled { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060019D4 RID: 6612
		bool IsMowaEnabled { get; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060019D5 RID: 6613
		ADObjectId ThrottlingPolicy { get; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060019D6 RID: 6614
		ADObjectId OwaMailboxPolicy { get; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060019D7 RID: 6615
		ADObjectId MobileDeviceMailboxPolicy { get; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060019D8 RID: 6616
		ADObjectId AddressBookPolicy { get; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060019D9 RID: 6617
		bool IsPersonToPersonMessagingEnabled { get; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060019DA RID: 6618
		bool IsMachineToPersonMessagingEnabled { get; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060019DB RID: 6619
		Capability? SkuCapability { get; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060019DC RID: 6620
		bool? SkuAssigned { get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060019DD RID: 6621
		bool IsMailboxAuditEnabled { get; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060019DE RID: 6622
		bool BypassAudit { get; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060019DF RID: 6623
		EnhancedTimeSpan MailboxAuditLogAgeLimit { get; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060019E0 RID: 6624
		MailboxAuditOperations AuditAdminOperations { get; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060019E1 RID: 6625
		MailboxAuditOperations AuditDelegateOperations { get; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060019E2 RID: 6626
		MailboxAuditOperations AuditDelegateAdminOperations { get; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060019E3 RID: 6627
		MailboxAuditOperations AuditOwnerOperations { get; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060019E4 RID: 6628
		DateTime? AuditLastAdminAccess { get; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060019E5 RID: 6629
		DateTime? AuditLastDelegateAccess { get; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060019E6 RID: 6630
		DateTime? AuditLastExternalAccess { get; }
	}
}
