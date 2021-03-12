using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DF RID: 479
	internal class MiniRecipientSchema : ADObjectSchema
	{
		// Token: 0x04000AEA RID: 2794
		public static readonly ADPropertyDefinition ArchiveDatabase = ADUserSchema.ArchiveDatabase;

		// Token: 0x04000AEB RID: 2795
		public static readonly ADPropertyDefinition ArchiveGuid = ADUserSchema.ArchiveGuid;

		// Token: 0x04000AEC RID: 2796
		public static readonly ADPropertyDefinition ArchiveName = ADUserSchema.ArchiveName;

		// Token: 0x04000AED RID: 2797
		public static readonly ADPropertyDefinition ArchiveState = ADUserSchema.ArchiveState;

		// Token: 0x04000AEE RID: 2798
		public static readonly ADPropertyDefinition JournalArchiveAddress = ADRecipientSchema.JournalArchiveAddress;

		// Token: 0x04000AEF RID: 2799
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x04000AF0 RID: 2800
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04000AF1 RID: 2801
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x04000AF2 RID: 2802
		public static readonly ADPropertyDefinition MailboxContainerGuid = IADMailStorageSchema.MailboxContainerGuid;

		// Token: 0x04000AF3 RID: 2803
		public static readonly ADPropertyDefinition AggregatedMailboxGuids = IADMailStorageSchema.AggregatedMailboxGuids;

		// Token: 0x04000AF4 RID: 2804
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = IADMailStorageSchema.ExchangeSecurityDescriptor;

		// Token: 0x04000AF5 RID: 2805
		public static readonly ADPropertyDefinition ExternalEmailAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04000AF6 RID: 2806
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04000AF7 RID: 2807
		public static readonly ADPropertyDefinition GrantSendOnBehalfTo = ADRecipientSchema.GrantSendOnBehalfTo;

		// Token: 0x04000AF8 RID: 2808
		public static readonly ADPropertyDefinition Languages = ADOrgPersonSchema.Languages;

		// Token: 0x04000AF9 RID: 2809
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x04000AFA RID: 2810
		public static readonly ADPropertyDefinition MasterAccountSid = ADRecipientSchema.MasterAccountSid;

		// Token: 0x04000AFB RID: 2811
		public static readonly ADPropertyDefinition MAPIEnabled = ADRecipientSchema.MAPIEnabled;

		// Token: 0x04000AFC RID: 2812
		public static readonly ADPropertyDefinition OWAEnabled = ADRecipientSchema.OWAEnabled;

		// Token: 0x04000AFD RID: 2813
		public static readonly ADPropertyDefinition MOWAEnabled = ADUserSchema.OWAforDevicesEnabled;

		// Token: 0x04000AFE RID: 2814
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x04000AFF RID: 2815
		public static readonly ADPropertyDefinition QueryBaseDN = ADUserSchema.QueryBaseDN;

		// Token: 0x04000B00 RID: 2816
		public static readonly ADPropertyDefinition RecipientType = ADRecipientSchema.RecipientType;

		// Token: 0x04000B01 RID: 2817
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x04000B02 RID: 2818
		public static readonly ADPropertyDefinition IsResource = ADRecipientSchema.IsResource;

		// Token: 0x04000B03 RID: 2819
		public static readonly ADPropertyDefinition DefaultPublicFolderMailbox = ADRecipientSchema.DefaultPublicFolderMailbox;

		// Token: 0x04000B04 RID: 2820
		public static readonly ADPropertyDefinition ServerLegacyDN = IADMailStorageSchema.ServerLegacyDN;

		// Token: 0x04000B05 RID: 2821
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x04000B06 RID: 2822
		public static readonly ADPropertyDefinition SidHistory = IADSecurityPrincipalSchema.SidHistory;

		// Token: 0x04000B07 RID: 2823
		public static readonly ADPropertyDefinition IsPersonToPersonTextMessagingEnabled = ADRecipientSchema.IsPersonToPersonTextMessagingEnabled;

		// Token: 0x04000B08 RID: 2824
		public static readonly ADPropertyDefinition IsMachineToPersonTextMessagingEnabled = ADRecipientSchema.IsMachineToPersonTextMessagingEnabled;

		// Token: 0x04000B09 RID: 2825
		public static readonly ADPropertyDefinition OWAMailboxPolicy = ADUserSchema.OwaMailboxPolicy;

		// Token: 0x04000B0A RID: 2826
		public static readonly ADPropertyDefinition MobileDeviceMailboxPolicy = ADUserSchema.ActiveSyncMailboxPolicy;

		// Token: 0x04000B0B RID: 2827
		public static readonly ADPropertyDefinition AddressBookPolicy = ADRecipientSchema.AddressBookPolicy;

		// Token: 0x04000B0C RID: 2828
		public static readonly ADPropertyDefinition ThrottlingPolicy = ADRecipientSchema.ThrottlingPolicy;

		// Token: 0x04000B0D RID: 2829
		public static readonly ADPropertyDefinition UserPrincipalName = ADUserSchema.UserPrincipalName;

		// Token: 0x04000B0E RID: 2830
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x04000B0F RID: 2831
		public static readonly ADPropertyDefinition NetID = ADUserSchema.NetID;

		// Token: 0x04000B10 RID: 2832
		public static readonly ADPropertyDefinition PersistedCapabilities = SharedPropertyDefinitions.PersistedCapabilities;

		// Token: 0x04000B11 RID: 2833
		public static readonly ADPropertyDefinition SKUAssigned = ADRecipientSchema.SKUAssigned;

		// Token: 0x04000B12 RID: 2834
		public static readonly ADPropertyDefinition SharePointUrl = IADMailStorageSchema.SharePointUrl;

		// Token: 0x04000B13 RID: 2835
		public static readonly ADPropertyDefinition WhenMailboxCreated = ADMailboxRecipientSchema.WhenMailboxCreated;

		// Token: 0x04000B14 RID: 2836
		public static readonly ADPropertyDefinition AuditEnabled = ADRecipientSchema.AuditEnabled;

		// Token: 0x04000B15 RID: 2837
		public static readonly ADPropertyDefinition AuditLogAgeLimit = ADRecipientSchema.AuditLogAgeLimit;

		// Token: 0x04000B16 RID: 2838
		public static readonly ADPropertyDefinition AuditAdminFlags = ADRecipientSchema.AuditAdminFlags;

		// Token: 0x04000B17 RID: 2839
		public static readonly ADPropertyDefinition AuditDelegateAdminFlags = ADRecipientSchema.AuditDelegateAdminFlags;

		// Token: 0x04000B18 RID: 2840
		public static readonly ADPropertyDefinition AuditDelegateFlags = ADRecipientSchema.AuditDelegateFlags;

		// Token: 0x04000B19 RID: 2841
		public static readonly ADPropertyDefinition AuditOwnerFlags = ADRecipientSchema.AuditOwnerFlags;

		// Token: 0x04000B1A RID: 2842
		public static readonly ADPropertyDefinition AuditBypassEnabled = ADRecipientSchema.AuditBypassEnabled;

		// Token: 0x04000B1B RID: 2843
		public static readonly ADPropertyDefinition AuditLastAdminAccess = ADRecipientSchema.AuditLastAdminAccess;

		// Token: 0x04000B1C RID: 2844
		public static readonly ADPropertyDefinition AuditLastDelegateAccess = ADRecipientSchema.AuditLastDelegateAccess;

		// Token: 0x04000B1D RID: 2845
		public static readonly ADPropertyDefinition AuditLastExternalAccess = ADRecipientSchema.AuditLastExternalAccess;

		// Token: 0x04000B1E RID: 2846
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04000B1F RID: 2847
		public static readonly ADPropertyDefinition ReleaseTrack = ADRecipientSchema.ReleaseTrack;

		// Token: 0x04000B20 RID: 2848
		public static readonly ADPropertyDefinition ConfigurationXML = ADRecipientSchema.ConfigurationXML;

		// Token: 0x04000B21 RID: 2849
		public static readonly ADPropertyDefinition ModernGroupType = ADRecipientSchema.ModernGroupType;

		// Token: 0x04000B22 RID: 2850
		public static readonly ADPropertyDefinition PublicToGroupSids = ADMailboxRecipientSchema.PublicToGroupSids;
	}
}
