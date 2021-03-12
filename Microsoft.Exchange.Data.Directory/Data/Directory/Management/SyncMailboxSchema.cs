using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000760 RID: 1888
	internal class SyncMailboxSchema : MailboxSchema
	{
		// Token: 0x04003E4E RID: 15950
		public static readonly ADPropertyDefinition AssistantName = ADRecipientSchema.AssistantName;

		// Token: 0x04003E4F RID: 15951
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x04003E50 RID: 15952
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003E51 RID: 15953
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04003E52 RID: 15954
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04003E53 RID: 15955
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04003E54 RID: 15956
		public static readonly ADPropertyDefinition ThumbnailPhoto = ADRecipientSchema.ThumbnailPhoto;

		// Token: 0x04003E55 RID: 15957
		public static readonly ADPropertyDefinition UMSpokenName = ADRecipientSchema.UMSpokenName;

		// Token: 0x04003E56 RID: 15958
		public static readonly ADPropertyDefinition ReleaseTrack = ADRecipientSchema.ReleaseTrack;

		// Token: 0x04003E57 RID: 15959
		public static readonly ADPropertyDefinition DirSyncId = ADRecipientSchema.DirSyncId;

		// Token: 0x04003E58 RID: 15960
		public static readonly ADPropertyDefinition City = ADOrgPersonSchema.City;

		// Token: 0x04003E59 RID: 15961
		public static readonly ADPropertyDefinition Company = ADOrgPersonSchema.Company;

		// Token: 0x04003E5A RID: 15962
		public static readonly ADPropertyDefinition CountryOrRegion = ADOrgPersonSchema.CountryOrRegion;

		// Token: 0x04003E5B RID: 15963
		public static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x04003E5C RID: 15964
		public static readonly ADPropertyDefinition Co = ADOrgPersonSchema.Co;

		// Token: 0x04003E5D RID: 15965
		public static readonly ADPropertyDefinition CountryCode = ADOrgPersonSchema.CountryCode;

		// Token: 0x04003E5E RID: 15966
		public static readonly ADPropertyDefinition Department = ADOrgPersonSchema.Department;

		// Token: 0x04003E5F RID: 15967
		public static readonly ADPropertyDefinition Fax = ADOrgPersonSchema.Fax;

		// Token: 0x04003E60 RID: 15968
		public static readonly ADPropertyDefinition FirstName = ADOrgPersonSchema.FirstName;

		// Token: 0x04003E61 RID: 15969
		public static readonly ADPropertyDefinition HomePhone = ADOrgPersonSchema.HomePhone;

		// Token: 0x04003E62 RID: 15970
		public static readonly ADPropertyDefinition Initials = ADOrgPersonSchema.Initials;

		// Token: 0x04003E63 RID: 15971
		public static readonly ADPropertyDefinition LastName = ADOrgPersonSchema.LastName;

		// Token: 0x04003E64 RID: 15972
		public static readonly ADPropertyDefinition Manager = ADOrgPersonSchema.Manager;

		// Token: 0x04003E65 RID: 15973
		public static readonly ADPropertyDefinition MobilePhone = ADOrgPersonSchema.MobilePhone;

		// Token: 0x04003E66 RID: 15974
		public static readonly ADPropertyDefinition OtherFax = ADOrgPersonSchema.OtherFax;

		// Token: 0x04003E67 RID: 15975
		public static readonly ADPropertyDefinition OtherHomePhone = ADOrgPersonSchema.OtherHomePhone;

		// Token: 0x04003E68 RID: 15976
		public static readonly ADPropertyDefinition OtherTelephone = ADOrgPersonSchema.OtherTelephone;

		// Token: 0x04003E69 RID: 15977
		public static readonly ADPropertyDefinition Pager = ADOrgPersonSchema.Pager;

		// Token: 0x04003E6A RID: 15978
		public static readonly ADPropertyDefinition Phone = ADOrgPersonSchema.Phone;

		// Token: 0x04003E6B RID: 15979
		public static readonly ADPropertyDefinition PostalCode = ADOrgPersonSchema.PostalCode;

		// Token: 0x04003E6C RID: 15980
		public static readonly ADPropertyDefinition StateOrProvince = ADOrgPersonSchema.StateOrProvince;

		// Token: 0x04003E6D RID: 15981
		public static readonly ADPropertyDefinition StreetAddress = ADOrgPersonSchema.StreetAddress;

		// Token: 0x04003E6E RID: 15982
		public static readonly ADPropertyDefinition TelephoneAssistant = ADOrgPersonSchema.TelephoneAssistant;

		// Token: 0x04003E6F RID: 15983
		public static readonly ADPropertyDefinition Title = ADOrgPersonSchema.Title;

		// Token: 0x04003E70 RID: 15984
		public static readonly ADPropertyDefinition WebPage = ADRecipientSchema.WebPage;

		// Token: 0x04003E71 RID: 15985
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x04003E72 RID: 15986
		public static readonly ADPropertyDefinition SidHistory = IADSecurityPrincipalSchema.SidHistory;

		// Token: 0x04003E73 RID: 15987
		public static readonly ADPropertyDefinition UsnChanged = ADRecipientSchema.UsnChanged;

		// Token: 0x04003E74 RID: 15988
		public static readonly ADPropertyDefinition EndOfList = ADRecipientSchema.EndOfList;

		// Token: 0x04003E75 RID: 15989
		public static readonly ADPropertyDefinition Cookie = ADRecipientSchema.Cookie;

		// Token: 0x04003E76 RID: 15990
		public static readonly ADPropertyDefinition MailboxPlanName = ADRecipientSchema.MailboxPlanName;

		// Token: 0x04003E77 RID: 15991
		public static readonly ADPropertyDefinition SeniorityIndex = ADRecipientSchema.HABSeniorityIndex;

		// Token: 0x04003E78 RID: 15992
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003E79 RID: 15993
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003E7A RID: 15994
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003E7B RID: 15995
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003E7C RID: 15996
		public static readonly ADPropertyDefinition ExcludedFromBackSync = ADRecipientSchema.ExcludedFromBackSync;

		// Token: 0x04003E7D RID: 15997
		public static readonly ADPropertyDefinition InPlaceHoldsRaw = ADRecipientSchema.InPlaceHoldsRaw;

		// Token: 0x04003E7E RID: 15998
		public static readonly ADPropertyDefinition LEOEnabled = ADRecipientSchema.LEOEnabled;

		// Token: 0x04003E7F RID: 15999
		public static readonly ADPropertyDefinition AccountDisabled = ADUserSchema.AccountDisabled;

		// Token: 0x04003E80 RID: 16000
		public static readonly ADPropertyDefinition StsRefreshTokensValidFrom = ADUserSchema.StsRefreshTokensValidFrom;
	}
}
