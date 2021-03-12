using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000274 RID: 628
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxConfiguration : IMailboxConfiguration
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x0007B6E0 File Offset: 0x000798E0
		public MailboxConfiguration(IGenericADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			this.SharePointUrl = adUser.SharePointUrl;
			this.IsMapiEnabled = adUser.IsMapiEnabled;
			this.IsOwaEnabled = adUser.IsOwaEnabled;
			this.IsMowaEnabled = adUser.IsMowaEnabled;
			this.ThrottlingPolicy = adUser.ThrottlingPolicy;
			this.OwaMailboxPolicy = adUser.OwaMailboxPolicy;
			this.MobileDeviceMailboxPolicy = adUser.MobileDeviceMailboxPolicy;
			this.AddressBookPolicy = adUser.AddressBookPolicy;
			this.IsPersonToPersonMessagingEnabled = adUser.IsPersonToPersonMessagingEnabled;
			this.IsMachineToPersonMessagingEnabled = adUser.IsMachineToPersonMessagingEnabled;
			this.SkuCapability = adUser.SkuCapability;
			this.SkuAssigned = adUser.SkuAssigned;
			this.IsMailboxAuditEnabled = adUser.IsMailboxAuditEnabled;
			this.BypassAudit = adUser.BypassAudit;
			this.MailboxAuditLogAgeLimit = adUser.MailboxAuditLogAgeLimit;
			this.AuditAdminOperations = adUser.AuditAdminOperations;
			this.AuditDelegateOperations = adUser.AuditDelegateOperations;
			this.AuditDelegateAdminOperations = adUser.AuditDelegateAdminOperations;
			this.AuditOwnerOperations = adUser.AuditOwnerOperations;
			this.AuditLastAdminAccess = adUser.AuditLastAdminAccess;
			this.AuditLastDelegateAccess = adUser.AuditLastDelegateAccess;
			this.AuditLastExternalAccess = adUser.AuditLastExternalAccess;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0007B808 File Offset: 0x00079A08
		public MailboxConfiguration()
		{
			this.SharePointUrl = null;
			this.IsMapiEnabled = false;
			this.IsOwaEnabled = false;
			this.IsMowaEnabled = false;
			this.ThrottlingPolicy = null;
			this.OwaMailboxPolicy = null;
			this.MobileDeviceMailboxPolicy = null;
			this.AddressBookPolicy = null;
			this.IsPersonToPersonMessagingEnabled = false;
			this.IsMachineToPersonMessagingEnabled = false;
			this.SkuCapability = null;
			this.SkuAssigned = null;
			this.IsMailboxAuditEnabled = false;
			this.BypassAudit = false;
			this.MailboxAuditLogAgeLimit = default(EnhancedTimeSpan);
			this.AuditAdminOperations = MailboxAuditOperations.None;
			this.AuditDelegateOperations = MailboxAuditOperations.None;
			this.AuditDelegateAdminOperations = MailboxAuditOperations.None;
			this.AuditOwnerOperations = MailboxAuditOperations.None;
			this.AuditLastAdminAccess = null;
			this.AuditLastDelegateAccess = null;
			this.AuditLastExternalAccess = null;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0007B8E7 File Offset: 0x00079AE7
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0007B8EF File Offset: 0x00079AEF
		public Uri SharePointUrl { get; private set; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0007B8F8 File Offset: 0x00079AF8
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0007B900 File Offset: 0x00079B00
		public bool IsMapiEnabled { get; private set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0007B909 File Offset: 0x00079B09
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x0007B911 File Offset: 0x00079B11
		public bool IsOwaEnabled { get; private set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0007B91A File Offset: 0x00079B1A
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0007B922 File Offset: 0x00079B22
		public bool IsMowaEnabled { get; private set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0007B92B File Offset: 0x00079B2B
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0007B933 File Offset: 0x00079B33
		public ADObjectId ThrottlingPolicy { get; private set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0007B93C File Offset: 0x00079B3C
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x0007B944 File Offset: 0x00079B44
		public ADObjectId OwaMailboxPolicy { get; private set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0007B94D File Offset: 0x00079B4D
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x0007B955 File Offset: 0x00079B55
		public ADObjectId MobileDeviceMailboxPolicy { get; private set; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0007B95E File Offset: 0x00079B5E
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0007B966 File Offset: 0x00079B66
		public ADObjectId AddressBookPolicy { get; private set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0007B96F File Offset: 0x00079B6F
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x0007B977 File Offset: 0x00079B77
		public bool IsPersonToPersonMessagingEnabled { get; private set; }

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0007B980 File Offset: 0x00079B80
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x0007B988 File Offset: 0x00079B88
		public bool IsMachineToPersonMessagingEnabled { get; private set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0007B991 File Offset: 0x00079B91
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x0007B999 File Offset: 0x00079B99
		public Capability? SkuCapability { get; private set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0007B9A2 File Offset: 0x00079BA2
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x0007B9AA File Offset: 0x00079BAA
		public bool? SkuAssigned { get; private set; }

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0007B9B3 File Offset: 0x00079BB3
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x0007B9BB File Offset: 0x00079BBB
		public bool IsMailboxAuditEnabled { get; private set; }

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0007B9C4 File Offset: 0x00079BC4
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x0007B9CC File Offset: 0x00079BCC
		public bool BypassAudit { get; private set; }

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0007B9D5 File Offset: 0x00079BD5
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x0007B9DD File Offset: 0x00079BDD
		public EnhancedTimeSpan MailboxAuditLogAgeLimit { get; private set; }

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0007B9E6 File Offset: 0x00079BE6
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x0007B9EE File Offset: 0x00079BEE
		public MailboxAuditOperations AuditAdminOperations { get; private set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0007B9F7 File Offset: 0x00079BF7
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x0007B9FF File Offset: 0x00079BFF
		public MailboxAuditOperations AuditDelegateOperations { get; private set; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x0007BA08 File Offset: 0x00079C08
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x0007BA10 File Offset: 0x00079C10
		public MailboxAuditOperations AuditDelegateAdminOperations { get; private set; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0007BA19 File Offset: 0x00079C19
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x0007BA21 File Offset: 0x00079C21
		public MailboxAuditOperations AuditOwnerOperations { get; private set; }

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0007BA2A File Offset: 0x00079C2A
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x0007BA32 File Offset: 0x00079C32
		public DateTime? AuditLastAdminAccess { get; private set; }

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0007BA3B File Offset: 0x00079C3B
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0007BA43 File Offset: 0x00079C43
		public DateTime? AuditLastDelegateAccess { get; private set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0007BA4C File Offset: 0x00079C4C
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x0007BA54 File Offset: 0x00079C54
		public DateTime? AuditLastExternalAccess { get; private set; }
	}
}
