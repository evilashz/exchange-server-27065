using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000764 RID: 1892
	internal class SyncMailUserSchema : MailUserSchema
	{
		// Token: 0x04003EAE RID: 16046
		public static readonly ADPropertyDefinition AssistantName = ADRecipientSchema.AssistantName;

		// Token: 0x04003EAF RID: 16047
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x04003EB0 RID: 16048
		public static readonly ADPropertyDefinition Certificate = ADRecipientSchema.Certificate;

		// Token: 0x04003EB1 RID: 16049
		public static readonly ADPropertyDefinition MasterAccountSid = ADRecipientSchema.MasterAccountSid;

		// Token: 0x04003EB2 RID: 16050
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003EB3 RID: 16051
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04003EB4 RID: 16052
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04003EB5 RID: 16053
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04003EB6 RID: 16054
		public static readonly ADPropertyDefinition SMimeCertificate = ADRecipientSchema.SMimeCertificate;

		// Token: 0x04003EB7 RID: 16055
		public static readonly ADPropertyDefinition ThumbnailPhoto = ADRecipientSchema.ThumbnailPhoto;

		// Token: 0x04003EB8 RID: 16056
		public static readonly ADPropertyDefinition DirSyncId = ADRecipientSchema.DirSyncId;

		// Token: 0x04003EB9 RID: 16057
		public static readonly ADPropertyDefinition ReleaseTrack = ADRecipientSchema.ReleaseTrack;

		// Token: 0x04003EBA RID: 16058
		public static readonly ADPropertyDefinition City = ADOrgPersonSchema.City;

		// Token: 0x04003EBB RID: 16059
		public static readonly ADPropertyDefinition Company = ADOrgPersonSchema.Company;

		// Token: 0x04003EBC RID: 16060
		public static readonly ADPropertyDefinition CountryOrRegion = ADOrgPersonSchema.CountryOrRegion;

		// Token: 0x04003EBD RID: 16061
		public static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x04003EBE RID: 16062
		public static readonly ADPropertyDefinition Co = ADOrgPersonSchema.Co;

		// Token: 0x04003EBF RID: 16063
		public static readonly ADPropertyDefinition CountryCode = ADOrgPersonSchema.CountryCode;

		// Token: 0x04003EC0 RID: 16064
		public static readonly ADPropertyDefinition Department = ADOrgPersonSchema.Department;

		// Token: 0x04003EC1 RID: 16065
		public static readonly ADPropertyDefinition Fax = ADOrgPersonSchema.Fax;

		// Token: 0x04003EC2 RID: 16066
		public static readonly ADPropertyDefinition FirstName = ADOrgPersonSchema.FirstName;

		// Token: 0x04003EC3 RID: 16067
		public static readonly ADPropertyDefinition HomePhone = ADOrgPersonSchema.HomePhone;

		// Token: 0x04003EC4 RID: 16068
		public static readonly ADPropertyDefinition Initials = ADOrgPersonSchema.Initials;

		// Token: 0x04003EC5 RID: 16069
		public static readonly ADPropertyDefinition LastName = ADOrgPersonSchema.LastName;

		// Token: 0x04003EC6 RID: 16070
		public static readonly ADPropertyDefinition Manager = ADOrgPersonSchema.Manager;

		// Token: 0x04003EC7 RID: 16071
		public static readonly ADPropertyDefinition MobilePhone = ADOrgPersonSchema.MobilePhone;

		// Token: 0x04003EC8 RID: 16072
		public static readonly ADPropertyDefinition Office = ADOrgPersonSchema.Office;

		// Token: 0x04003EC9 RID: 16073
		public static readonly ADPropertyDefinition OtherFax = ADOrgPersonSchema.OtherFax;

		// Token: 0x04003ECA RID: 16074
		public static readonly ADPropertyDefinition OtherHomePhone = ADOrgPersonSchema.OtherHomePhone;

		// Token: 0x04003ECB RID: 16075
		public static readonly ADPropertyDefinition OtherTelephone = ADOrgPersonSchema.OtherTelephone;

		// Token: 0x04003ECC RID: 16076
		public static readonly ADPropertyDefinition Pager = ADOrgPersonSchema.Pager;

		// Token: 0x04003ECD RID: 16077
		public static readonly ADPropertyDefinition Phone = ADOrgPersonSchema.Phone;

		// Token: 0x04003ECE RID: 16078
		public static readonly ADPropertyDefinition PostalCode = ADOrgPersonSchema.PostalCode;

		// Token: 0x04003ECF RID: 16079
		public static readonly ADPropertyDefinition ResourceCapacity = ADRecipientSchema.ResourceCapacity;

		// Token: 0x04003ED0 RID: 16080
		public static readonly ADPropertyDefinition ResourceCustom = ADRecipientSchema.ResourceCustom;

		// Token: 0x04003ED1 RID: 16081
		public static readonly ADPropertyDefinition ResourceMetaData = ADRecipientSchema.ResourceMetaData;

		// Token: 0x04003ED2 RID: 16082
		public static readonly ADPropertyDefinition ResourcePropertiesDisplay = ADRecipientSchema.ResourcePropertiesDisplay;

		// Token: 0x04003ED3 RID: 16083
		public static readonly ADPropertyDefinition ResourceSearchProperties = ADRecipientSchema.ResourceSearchProperties;

		// Token: 0x04003ED4 RID: 16084
		public static readonly ADPropertyDefinition StateOrProvince = ADOrgPersonSchema.StateOrProvince;

		// Token: 0x04003ED5 RID: 16085
		public static readonly ADPropertyDefinition StreetAddress = ADOrgPersonSchema.StreetAddress;

		// Token: 0x04003ED6 RID: 16086
		public static readonly ADPropertyDefinition TelephoneAssistant = ADOrgPersonSchema.TelephoneAssistant;

		// Token: 0x04003ED7 RID: 16087
		public static readonly ADPropertyDefinition Title = ADOrgPersonSchema.Title;

		// Token: 0x04003ED8 RID: 16088
		public static readonly ADPropertyDefinition WebPage = ADRecipientSchema.WebPage;

		// Token: 0x04003ED9 RID: 16089
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x04003EDA RID: 16090
		public static readonly ADPropertyDefinition SidHistory = IADSecurityPrincipalSchema.SidHistory;

		// Token: 0x04003EDB RID: 16091
		public static readonly ADPropertyDefinition EndOfList = SyncMailboxSchema.EndOfList;

		// Token: 0x04003EDC RID: 16092
		public static readonly ADPropertyDefinition Cookie = SyncMailboxSchema.Cookie;

		// Token: 0x04003EDD RID: 16093
		public static readonly ADPropertyDefinition Languages = ADOrgPersonSchema.Languages;

		// Token: 0x04003EDE RID: 16094
		public static readonly ADPropertyDefinition IntendedMailboxPlan = ADUserSchema.IntendedMailboxPlan;

		// Token: 0x04003EDF RID: 16095
		public static readonly ADPropertyDefinition IntendedMailboxPlanName = ADUserSchema.IntendedMailboxPlanName;

		// Token: 0x04003EE0 RID: 16096
		public static readonly ADPropertyDefinition SeniorityIndex = ADRecipientSchema.HABSeniorityIndex;

		// Token: 0x04003EE1 RID: 16097
		public static readonly ADPropertyDefinition IsCalculatedTargetAddress = ADRecipientSchema.IsCalculatedTargetAddress;

		// Token: 0x04003EE2 RID: 16098
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003EE3 RID: 16099
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003EE4 RID: 16100
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003EE5 RID: 16101
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003EE6 RID: 16102
		public static readonly ADPropertyDefinition RemoteRecipientType = ADUserSchema.RemoteRecipientType;

		// Token: 0x04003EE7 RID: 16103
		public static readonly ADPropertyDefinition ExcludedFromBackSync = ADRecipientSchema.ExcludedFromBackSync;

		// Token: 0x04003EE8 RID: 16104
		public static readonly ADPropertyDefinition AuditBypassEnabled = ADRecipientSchema.AuditBypassEnabled;

		// Token: 0x04003EE9 RID: 16105
		public static readonly ADPropertyDefinition AuditAdminFlags = ADRecipientSchema.AuditAdminFlags;

		// Token: 0x04003EEA RID: 16106
		public static readonly ADPropertyDefinition AuditDelegateAdminFlags = ADRecipientSchema.AuditDelegateAdminFlags;

		// Token: 0x04003EEB RID: 16107
		public static readonly ADPropertyDefinition AuditDelegateFlags = ADRecipientSchema.AuditDelegateFlags;

		// Token: 0x04003EEC RID: 16108
		public static readonly ADPropertyDefinition AuditOwnerFlags = ADRecipientSchema.AuditOwnerFlags;

		// Token: 0x04003EED RID: 16109
		public static readonly ADPropertyDefinition ElcMailboxFlags = ADUserSchema.ElcMailboxFlags;

		// Token: 0x04003EEE RID: 16110
		public static readonly ADPropertyDefinition InPlaceHoldsRaw = ADRecipientSchema.InPlaceHoldsRaw;

		// Token: 0x04003EEF RID: 16111
		public static readonly ADPropertyDefinition AuditEnabled = ADRecipientSchema.AuditEnabled;

		// Token: 0x04003EF0 RID: 16112
		public static readonly ADPropertyDefinition AuditLogAgeLimit = ADRecipientSchema.AuditLogAgeLimit;

		// Token: 0x04003EF1 RID: 16113
		public static readonly ADPropertyDefinition SiteMailboxOwners = ADUserSchema.Owners;

		// Token: 0x04003EF2 RID: 16114
		public static readonly ADPropertyDefinition SiteMailboxUsers = ADMailboxRecipientSchema.DelegateListLink;

		// Token: 0x04003EF3 RID: 16115
		public static readonly ADPropertyDefinition SiteMailboxClosedTime = ADUserSchema.TeamMailboxClosedTime;

		// Token: 0x04003EF4 RID: 16116
		public static readonly ADPropertyDefinition SharePointUrl = ADMailboxRecipientSchema.SharePointUrl;

		// Token: 0x04003EF5 RID: 16117
		public static readonly ADPropertyDefinition AccountDisabled = ADUserSchema.AccountDisabled;

		// Token: 0x04003EF6 RID: 16118
		public static readonly ADPropertyDefinition StsRefreshTokensValidFrom = ADUserSchema.StsRefreshTokensValidFrom;
	}
}
