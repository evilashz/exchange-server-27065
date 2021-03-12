using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000762 RID: 1890
	internal class SyncMailContactSchema : MailContactSchema
	{
		// Token: 0x04003E82 RID: 16002
		public static readonly ADPropertyDefinition AssistantName = ADRecipientSchema.AssistantName;

		// Token: 0x04003E83 RID: 16003
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x04003E84 RID: 16004
		public static readonly ADPropertyDefinition ImmutableId = ADRecipientSchema.ImmutableId;

		// Token: 0x04003E85 RID: 16005
		public static readonly ADPropertyDefinition MasterAccountSid = ADRecipientSchema.MasterAccountSid;

		// Token: 0x04003E86 RID: 16006
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003E87 RID: 16007
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04003E88 RID: 16008
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04003E89 RID: 16009
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04003E8A RID: 16010
		public static readonly ADPropertyDefinition DirSyncId = ADRecipientSchema.DirSyncId;

		// Token: 0x04003E8B RID: 16011
		public static readonly ADPropertyDefinition City = ADOrgPersonSchema.City;

		// Token: 0x04003E8C RID: 16012
		public static readonly ADPropertyDefinition Company = ADOrgPersonSchema.Company;

		// Token: 0x04003E8D RID: 16013
		public static readonly ADPropertyDefinition CountryOrRegion = ADOrgPersonSchema.CountryOrRegion;

		// Token: 0x04003E8E RID: 16014
		public static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x04003E8F RID: 16015
		public static readonly ADPropertyDefinition Co = ADOrgPersonSchema.Co;

		// Token: 0x04003E90 RID: 16016
		public static readonly ADPropertyDefinition CountryCode = ADOrgPersonSchema.CountryCode;

		// Token: 0x04003E91 RID: 16017
		public static readonly ADPropertyDefinition Department = ADOrgPersonSchema.Department;

		// Token: 0x04003E92 RID: 16018
		public static readonly ADPropertyDefinition Fax = ADOrgPersonSchema.Fax;

		// Token: 0x04003E93 RID: 16019
		public static readonly ADPropertyDefinition FirstName = ADOrgPersonSchema.FirstName;

		// Token: 0x04003E94 RID: 16020
		public static readonly ADPropertyDefinition HomePhone = ADOrgPersonSchema.HomePhone;

		// Token: 0x04003E95 RID: 16021
		public static readonly ADPropertyDefinition Initials = ADOrgPersonSchema.Initials;

		// Token: 0x04003E96 RID: 16022
		public static readonly ADPropertyDefinition LastName = ADOrgPersonSchema.LastName;

		// Token: 0x04003E97 RID: 16023
		public static readonly ADPropertyDefinition MobilePhone = ADOrgPersonSchema.MobilePhone;

		// Token: 0x04003E98 RID: 16024
		public static readonly ADPropertyDefinition Manager = ADOrgPersonSchema.Manager;

		// Token: 0x04003E99 RID: 16025
		public static readonly ADPropertyDefinition Office = ADOrgPersonSchema.Office;

		// Token: 0x04003E9A RID: 16026
		public static readonly ADPropertyDefinition OtherFax = ADOrgPersonSchema.OtherFax;

		// Token: 0x04003E9B RID: 16027
		public static readonly ADPropertyDefinition OtherHomePhone = ADOrgPersonSchema.OtherHomePhone;

		// Token: 0x04003E9C RID: 16028
		public static readonly ADPropertyDefinition OtherTelephone = ADOrgPersonSchema.OtherTelephone;

		// Token: 0x04003E9D RID: 16029
		public static readonly ADPropertyDefinition Pager = ADOrgPersonSchema.Pager;

		// Token: 0x04003E9E RID: 16030
		public static readonly ADPropertyDefinition Phone = ADOrgPersonSchema.Phone;

		// Token: 0x04003E9F RID: 16031
		public static readonly ADPropertyDefinition PostalCode = ADOrgPersonSchema.PostalCode;

		// Token: 0x04003EA0 RID: 16032
		public static readonly ADPropertyDefinition StateOrProvince = ADOrgPersonSchema.StateOrProvince;

		// Token: 0x04003EA1 RID: 16033
		public static readonly ADPropertyDefinition StreetAddress = ADOrgPersonSchema.StreetAddress;

		// Token: 0x04003EA2 RID: 16034
		public static readonly ADPropertyDefinition TelephoneAssistant = ADOrgPersonSchema.TelephoneAssistant;

		// Token: 0x04003EA3 RID: 16035
		public static readonly ADPropertyDefinition Title = ADOrgPersonSchema.Title;

		// Token: 0x04003EA4 RID: 16036
		public static readonly ADPropertyDefinition WebPage = ADRecipientSchema.WebPage;

		// Token: 0x04003EA5 RID: 16037
		public static readonly ADPropertyDefinition EndOfList = SyncMailboxSchema.EndOfList;

		// Token: 0x04003EA6 RID: 16038
		public static readonly ADPropertyDefinition Cookie = SyncMailboxSchema.Cookie;

		// Token: 0x04003EA7 RID: 16039
		public static readonly ADPropertyDefinition SeniorityIndex = ADRecipientSchema.HABSeniorityIndex;

		// Token: 0x04003EA8 RID: 16040
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003EA9 RID: 16041
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003EAA RID: 16042
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003EAB RID: 16043
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003EAC RID: 16044
		public static readonly ADPropertyDefinition ExcludedFromBackSync = ADRecipientSchema.ExcludedFromBackSync;
	}
}
