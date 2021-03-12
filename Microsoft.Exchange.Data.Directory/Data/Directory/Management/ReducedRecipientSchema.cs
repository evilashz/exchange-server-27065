using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000783 RID: 1923
	internal class ReducedRecipientSchema : ADObjectSchema
	{
		// Token: 0x04004062 RID: 16482
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x04004063 RID: 16483
		public static readonly ADPropertyDefinition ArchiveGuid = ADUserSchema.ArchiveGuid;

		// Token: 0x04004064 RID: 16484
		public static readonly ADPropertyDefinition AuthenticationType = ADRecipientSchema.AuthenticationType;

		// Token: 0x04004065 RID: 16485
		public static readonly ADPropertyDefinition City = ADUserSchema.City;

		// Token: 0x04004066 RID: 16486
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04004067 RID: 16487
		public static readonly ADPropertyDefinition Company = ADUserSchema.Company;

		// Token: 0x04004068 RID: 16488
		public static readonly ADPropertyDefinition CountryOrRegion = ADUserSchema.CountryOrRegion;

		// Token: 0x04004069 RID: 16489
		public static readonly ADPropertyDefinition PostalCode = ADUserSchema.PostalCode;

		// Token: 0x0400406A RID: 16490
		public static readonly ADPropertyDefinition CustomAttribute1 = ADRecipientSchema.CustomAttribute1;

		// Token: 0x0400406B RID: 16491
		public static readonly ADPropertyDefinition CustomAttribute2 = ADRecipientSchema.CustomAttribute2;

		// Token: 0x0400406C RID: 16492
		public static readonly ADPropertyDefinition CustomAttribute3 = ADRecipientSchema.CustomAttribute3;

		// Token: 0x0400406D RID: 16493
		public static readonly ADPropertyDefinition CustomAttribute4 = ADRecipientSchema.CustomAttribute4;

		// Token: 0x0400406E RID: 16494
		public static readonly ADPropertyDefinition CustomAttribute5 = ADRecipientSchema.CustomAttribute5;

		// Token: 0x0400406F RID: 16495
		public static readonly ADPropertyDefinition CustomAttribute6 = ADRecipientSchema.CustomAttribute6;

		// Token: 0x04004070 RID: 16496
		public static readonly ADPropertyDefinition CustomAttribute7 = ADRecipientSchema.CustomAttribute7;

		// Token: 0x04004071 RID: 16497
		public static readonly ADPropertyDefinition CustomAttribute8 = ADRecipientSchema.CustomAttribute8;

		// Token: 0x04004072 RID: 16498
		public static readonly ADPropertyDefinition CustomAttribute9 = ADRecipientSchema.CustomAttribute9;

		// Token: 0x04004073 RID: 16499
		public static readonly ADPropertyDefinition CustomAttribute10 = ADRecipientSchema.CustomAttribute10;

		// Token: 0x04004074 RID: 16500
		public static readonly ADPropertyDefinition CustomAttribute11 = ADRecipientSchema.CustomAttribute11;

		// Token: 0x04004075 RID: 16501
		public static readonly ADPropertyDefinition CustomAttribute12 = ADRecipientSchema.CustomAttribute12;

		// Token: 0x04004076 RID: 16502
		public static readonly ADPropertyDefinition CustomAttribute13 = ADRecipientSchema.CustomAttribute13;

		// Token: 0x04004077 RID: 16503
		public static readonly ADPropertyDefinition CustomAttribute14 = ADRecipientSchema.CustomAttribute14;

		// Token: 0x04004078 RID: 16504
		public static readonly ADPropertyDefinition CustomAttribute15 = ADRecipientSchema.CustomAttribute15;

		// Token: 0x04004079 RID: 16505
		public static readonly ADPropertyDefinition ExtensionCustomAttribute1 = ADRecipientSchema.ExtensionCustomAttribute1;

		// Token: 0x0400407A RID: 16506
		public static readonly ADPropertyDefinition ExtensionCustomAttribute2 = ADRecipientSchema.ExtensionCustomAttribute2;

		// Token: 0x0400407B RID: 16507
		public static readonly ADPropertyDefinition ExtensionCustomAttribute3 = ADRecipientSchema.ExtensionCustomAttribute3;

		// Token: 0x0400407C RID: 16508
		public static readonly ADPropertyDefinition ExtensionCustomAttribute4 = ADRecipientSchema.ExtensionCustomAttribute4;

		// Token: 0x0400407D RID: 16509
		public static readonly ADPropertyDefinition ExtensionCustomAttribute5 = ADRecipientSchema.ExtensionCustomAttribute5;

		// Token: 0x0400407E RID: 16510
		public static readonly ADPropertyDefinition Database = ADMailboxRecipientSchema.Database;

		// Token: 0x0400407F RID: 16511
		public static readonly ADPropertyDefinition ArchiveDatabase = ADUserSchema.ArchiveDatabase;

		// Token: 0x04004080 RID: 16512
		public static readonly ADPropertyDefinition Department = ADUserSchema.Department;

		// Token: 0x04004081 RID: 16513
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04004082 RID: 16514
		public static readonly ADPropertyDefinition ManagedFolderMailboxPolicy = ADUserSchema.ManagedFolderMailboxPolicy;

		// Token: 0x04004083 RID: 16515
		public static readonly ADPropertyDefinition AddressListMembership = ADRecipientSchema.AddressListMembership;

		// Token: 0x04004084 RID: 16516
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04004085 RID: 16517
		public static readonly ADPropertyDefinition ExpansionServer = ADGroupSchema.ExpansionServer;

		// Token: 0x04004086 RID: 16518
		public static readonly ADPropertyDefinition ExternalEmailAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04004087 RID: 16519
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04004088 RID: 16520
		public static readonly ADPropertyDefinition FirstName = ADUserSchema.FirstName;

		// Token: 0x04004089 RID: 16521
		public static readonly ADPropertyDefinition HiddenFromAddressListsEnabled = ADRecipientSchema.HiddenFromAddressListsEnabled;

		// Token: 0x0400408A RID: 16522
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x0400408B RID: 16523
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x0400408C RID: 16524
		public static readonly ADPropertyDefinition LastName = ADUserSchema.LastName;

		// Token: 0x0400408D RID: 16525
		public static readonly ADPropertyDefinition ResourceType = MailboxSchema.ResourceType;

		// Token: 0x0400408E RID: 16526
		public static readonly ADPropertyDefinition ManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x0400408F RID: 16527
		public static readonly ADPropertyDefinition Manager = ADUserSchema.Manager;

		// Token: 0x04004090 RID: 16528
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicy = ADUserSchema.ActiveSyncMailboxPolicy;

		// Token: 0x04004091 RID: 16529
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicyIsDefaulted = ADUserSchema.ActiveSyncMailboxPolicyIsDefaulted;

		// Token: 0x04004092 RID: 16530
		public static readonly ADPropertyDefinition OwaMailboxPolicy = ADUserSchema.OwaMailboxPolicy;

		// Token: 0x04004093 RID: 16531
		public static readonly ADPropertyDefinition AddressBookPolicy = ADRecipientSchema.AddressBookPolicy;

		// Token: 0x04004094 RID: 16532
		public static readonly ADPropertyDefinition SharingPolicy = ADUserSchema.SharingPolicy;

		// Token: 0x04004095 RID: 16533
		public static readonly ADPropertyDefinition Office = ADUserSchema.Office;

		// Token: 0x04004096 RID: 16534
		public static readonly ADPropertyDefinition Phone = ADUserSchema.Phone;

		// Token: 0x04004097 RID: 16535
		public static readonly ADPropertyDefinition PoliciesIncluded = ADRecipientSchema.PoliciesIncluded;

		// Token: 0x04004098 RID: 16536
		public static readonly ADPropertyDefinition PoliciesExcluded = ADRecipientSchema.PoliciesExcluded;

		// Token: 0x04004099 RID: 16537
		public static readonly ADPropertyDefinition UserPrincipalName = ADUserSchema.UserPrincipalName;

		// Token: 0x0400409A RID: 16538
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x0400409B RID: 16539
		public static readonly ADPropertyDefinition ServerLegacyDN = ADMailboxRecipientSchema.ServerLegacyDN;

		// Token: 0x0400409C RID: 16540
		public static readonly ADPropertyDefinition StateOrProvince = ADUserSchema.StateOrProvince;

		// Token: 0x0400409D RID: 16541
		public static readonly ADPropertyDefinition Title = ADUserSchema.Title;

		// Token: 0x0400409E RID: 16542
		public static readonly ADPropertyDefinition UMMailboxPolicy = ADUserSchema.UMMailboxPolicy;

		// Token: 0x0400409F RID: 16543
		public static readonly ADPropertyDefinition UMRecipientDialPlanId = ADRecipientSchema.UMRecipientDialPlanId;

		// Token: 0x040040A0 RID: 16544
		public static readonly ADPropertyDefinition DatabaseName = IADMailStorageSchema.DatabaseName;

		// Token: 0x040040A1 RID: 16545
		public static readonly ADPropertyDefinition EmailAddressPolicyEnabled = ADRecipientSchema.EmailAddressPolicyEnabled;

		// Token: 0x040040A2 RID: 16546
		public static readonly ADPropertyDefinition OrganizationalUnit = ADRecipientSchema.OrganizationalUnit;

		// Token: 0x040040A3 RID: 16547
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x040040A4 RID: 16548
		public static readonly ADPropertyDefinition RecipientType = ADRecipientSchema.RecipientType;

		// Token: 0x040040A5 RID: 16549
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x040040A6 RID: 16550
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x040040A7 RID: 16551
		public static readonly ADPropertyDefinition StorageGroupName = ADUserSchema.StorageGroupName;

		// Token: 0x040040A8 RID: 16552
		public static readonly ADPropertyDefinition UMEnabled = ADUserSchema.UMEnabled;

		// Token: 0x040040A9 RID: 16553
		public static readonly ADPropertyDefinition HasActiveSyncDevicePartnership = ADUserSchema.HasActiveSyncDevicePartnership;

		// Token: 0x040040AA RID: 16554
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x040040AB RID: 16555
		public static readonly ADPropertyDefinition Members = new ADPropertyDefinition("Members", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "member", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040AC RID: 16556
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = ADUserSchema.MailboxMoveTargetMDB;

		// Token: 0x040040AD RID: 16557
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = ADUserSchema.MailboxMoveSourceMDB;

		// Token: 0x040040AE RID: 16558
		public static readonly ADPropertyDefinition MailboxMoveTargetArchiveMDB = ADUserSchema.MailboxMoveTargetArchiveMDB;

		// Token: 0x040040AF RID: 16559
		public static readonly ADPropertyDefinition MailboxMoveSourceArchiveMDB = ADUserSchema.MailboxMoveSourceArchiveMDB;

		// Token: 0x040040B0 RID: 16560
		public static readonly ADPropertyDefinition MailboxMoveFlags = ADUserSchema.MailboxMoveFlags;

		// Token: 0x040040B1 RID: 16561
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = ADUserSchema.MailboxMoveRemoteHostName;

		// Token: 0x040040B2 RID: 16562
		public static readonly ADPropertyDefinition MailboxMoveBatchName = ADUserSchema.MailboxMoveBatchName;

		// Token: 0x040040B3 RID: 16563
		public static readonly ADPropertyDefinition MailboxMoveStatus = ADUserSchema.MailboxMoveStatus;

		// Token: 0x040040B4 RID: 16564
		public static readonly ADPropertyDefinition MailboxRelease = ADUserSchema.MailboxRelease;

		// Token: 0x040040B5 RID: 16565
		public static readonly ADPropertyDefinition ArchiveRelease = ADUserSchema.ArchiveRelease;

		// Token: 0x040040B6 RID: 16566
		public static readonly ADPropertyDefinition IsValidSecurityPrincipal = ADRecipientSchema.IsValidSecurityPrincipal;

		// Token: 0x040040B7 RID: 16567
		public static readonly ADPropertyDefinition RetentionPolicy = IADMailStorageSchema.RetentionPolicy;

		// Token: 0x040040B8 RID: 16568
		public static readonly ADPropertyDefinition ShouldUseDefaultRetentionPolicy = IADMailStorageSchema.ShouldUseDefaultRetentionPolicy;

		// Token: 0x040040B9 RID: 16569
		public static readonly ADPropertyDefinition LitigationHoldEnabled = IADMailStorageSchema.LitigationHoldEnabled;

		// Token: 0x040040BA RID: 16570
		public static readonly ADPropertyDefinition ArchiveState = IADMailStorageSchema.ArchiveState;

		// Token: 0x040040BB RID: 16571
		public static readonly ADPropertyDefinition RawCapabilities = SharedPropertyDefinitions.RawCapabilities;

		// Token: 0x040040BC RID: 16572
		public static readonly ADPropertyDefinition Capabilities = SharedPropertyDefinitions.Capabilities;

		// Token: 0x040040BD RID: 16573
		public static readonly ADPropertyDefinition SKUAssigned = ADRecipientSchema.SKUAssigned;

		// Token: 0x040040BE RID: 16574
		public static readonly ADPropertyDefinition WhenMailboxCreated = ADMailboxRecipientSchema.WhenMailboxCreated;

		// Token: 0x040040BF RID: 16575
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x040040C0 RID: 16576
		public static readonly ADPropertyDefinition UsageLocation = ADRecipientSchema.UsageLocation;

		// Token: 0x040040C1 RID: 16577
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x040040C2 RID: 16578
		public static readonly ADPropertyDefinition ArchiveStatus = ADUserSchema.ArchiveStatus;
	}
}
