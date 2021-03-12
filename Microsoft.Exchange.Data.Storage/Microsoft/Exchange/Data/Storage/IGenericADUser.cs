using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E9 RID: 745
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IGenericADUser : IFederatedIdentityParameters
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001FB7 RID: 8119
		string DisplayName { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001FB8 RID: 8120
		string UserPrincipalName { get; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001FB9 RID: 8121
		string LegacyDn { get; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001FBA RID: 8122
		string Alias { get; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001FBB RID: 8123
		ADObjectId DefaultPublicFolderMailbox { get; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001FBC RID: 8124
		SecurityIdentifier Sid { get; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001FBD RID: 8125
		SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001FBE RID: 8126
		IEnumerable<SecurityIdentifier> SidHistory { get; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001FBF RID: 8127
		IEnumerable<ADObjectId> GrantSendOnBehalfTo { get; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001FC0 RID: 8128
		IEnumerable<CultureInfo> Languages { get; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001FC1 RID: 8129
		RecipientType RecipientType { get; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001FC2 RID: 8130
		RecipientTypeDetails RecipientTypeDetails { get; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001FC3 RID: 8131
		bool? IsResource { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001FC4 RID: 8132
		SmtpAddress PrimarySmtpAddress { get; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001FC5 RID: 8133
		ProxyAddress ExternalEmailAddress { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001FC6 RID: 8134
		ProxyAddressCollection EmailAddresses { get; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001FC7 RID: 8135
		Guid MailboxGuid { get; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001FC8 RID: 8136
		ADObjectId MailboxDatabase { get; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001FC9 RID: 8137
		DateTime? WhenMailboxCreated { get; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001FCA RID: 8138
		NetID NetId { get; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001FCB RID: 8139
		ModernGroupObjectType ModernGroupType { get; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001FCC RID: 8140
		IEnumerable<SecurityIdentifier> PublicToGroupSids { get; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001FCD RID: 8141
		string ExternalDirectoryObjectId { get; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001FCE RID: 8142
		ADObjectId ArchiveDatabase { get; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001FCF RID: 8143
		Guid ArchiveGuid { get; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001FD0 RID: 8144
		IEnumerable<string> ArchiveName { get; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001FD1 RID: 8145
		ArchiveState ArchiveState { get; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001FD2 RID: 8146
		ArchiveStatusFlags ArchiveStatus { get; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001FD3 RID: 8147
		SmtpDomain ArchiveDomain { get; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001FD4 RID: 8148
		IEnumerable<Guid> AggregatedMailboxGuids { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001FD5 RID: 8149
		IEnumerable<IMailboxLocationInfo> MailboxLocations { get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001FD6 RID: 8150
		Uri SharePointUrl { get; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001FD7 RID: 8151
		bool IsMapiEnabled { get; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001FD8 RID: 8152
		bool IsOwaEnabled { get; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001FD9 RID: 8153
		bool IsMowaEnabled { get; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001FDA RID: 8154
		ADObjectId ThrottlingPolicy { get; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001FDB RID: 8155
		ADObjectId OwaMailboxPolicy { get; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001FDC RID: 8156
		ADObjectId MobileDeviceMailboxPolicy { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001FDD RID: 8157
		ADObjectId AddressBookPolicy { get; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001FDE RID: 8158
		bool IsPersonToPersonMessagingEnabled { get; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001FDF RID: 8159
		bool IsMachineToPersonMessagingEnabled { get; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001FE0 RID: 8160
		Capability? SkuCapability { get; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001FE1 RID: 8161
		bool? SkuAssigned { get; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001FE2 RID: 8162
		bool IsMailboxAuditEnabled { get; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001FE3 RID: 8163
		bool BypassAudit { get; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001FE4 RID: 8164
		EnhancedTimeSpan MailboxAuditLogAgeLimit { get; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001FE5 RID: 8165
		MailboxAuditOperations AuditAdminOperations { get; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001FE6 RID: 8166
		MailboxAuditOperations AuditDelegateOperations { get; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001FE7 RID: 8167
		MailboxAuditOperations AuditDelegateAdminOperations { get; }

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001FE8 RID: 8168
		MailboxAuditOperations AuditOwnerOperations { get; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001FE9 RID: 8169
		DateTime? AuditLastAdminAccess { get; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001FEA RID: 8170
		DateTime? AuditLastDelegateAccess { get; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001FEB RID: 8171
		DateTime? AuditLastExternalAccess { get; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001FEC RID: 8172
		ADObjectId QueryBaseDN { get; }

		// Token: 0x06001FED RID: 8173
		SmtpAddress GetFederatedSmtpAddress();

		// Token: 0x06001FEE RID: 8174
		FederatedIdentity GetFederatedIdentity();
	}
}
