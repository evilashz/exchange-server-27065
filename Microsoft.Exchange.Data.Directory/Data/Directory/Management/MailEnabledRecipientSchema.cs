using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F2 RID: 1778
	internal abstract class MailEnabledRecipientSchema : ADPresentationSchema
	{
		// Token: 0x04003833 RID: 14387
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFrom = ADRecipientSchema.AcceptMessagesOnlyFrom;

		// Token: 0x04003834 RID: 14388
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromDLMembers = ADRecipientSchema.AcceptMessagesOnlyFromDLMembers;

		// Token: 0x04003835 RID: 14389
		public static readonly ADPropertyDefinition AcceptMessagesOnlyFromSendersOrMembers = ADRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers;

		// Token: 0x04003836 RID: 14390
		public static readonly ADPropertyDefinition AddressListMembership = ADRecipientSchema.ReadOnlyAddressListMembership;

		// Token: 0x04003837 RID: 14391
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x04003838 RID: 14392
		public static readonly ADPropertyDefinition ArbitrationMailbox = ADRecipientSchema.ArbitrationMailbox;

		// Token: 0x04003839 RID: 14393
		public static readonly ADPropertyDefinition BypassModerationFrom = ADRecipientSchema.BypassModerationFrom;

		// Token: 0x0400383A RID: 14394
		public static readonly ADPropertyDefinition BypassModerationFromDLMembers = ADRecipientSchema.BypassModerationFromDLMembers;

		// Token: 0x0400383B RID: 14395
		public static readonly ADPropertyDefinition BypassModerationFromSendersOrMembers = ADRecipientSchema.BypassModerationFromSendersOrMembers;

		// Token: 0x0400383C RID: 14396
		public static readonly ADPropertyDefinition CustomAttribute1 = ADRecipientSchema.CustomAttribute1;

		// Token: 0x0400383D RID: 14397
		public static readonly ADPropertyDefinition CustomAttribute10 = ADRecipientSchema.CustomAttribute10;

		// Token: 0x0400383E RID: 14398
		public static readonly ADPropertyDefinition CustomAttribute11 = ADRecipientSchema.CustomAttribute11;

		// Token: 0x0400383F RID: 14399
		public static readonly ADPropertyDefinition CustomAttribute12 = ADRecipientSchema.CustomAttribute12;

		// Token: 0x04003840 RID: 14400
		public static readonly ADPropertyDefinition CustomAttribute13 = ADRecipientSchema.CustomAttribute13;

		// Token: 0x04003841 RID: 14401
		public static readonly ADPropertyDefinition CustomAttribute14 = ADRecipientSchema.CustomAttribute14;

		// Token: 0x04003842 RID: 14402
		public static readonly ADPropertyDefinition CustomAttribute15 = ADRecipientSchema.CustomAttribute15;

		// Token: 0x04003843 RID: 14403
		public static readonly ADPropertyDefinition CustomAttribute2 = ADRecipientSchema.CustomAttribute2;

		// Token: 0x04003844 RID: 14404
		public static readonly ADPropertyDefinition CustomAttribute3 = ADRecipientSchema.CustomAttribute3;

		// Token: 0x04003845 RID: 14405
		public static readonly ADPropertyDefinition CustomAttribute4 = ADRecipientSchema.CustomAttribute4;

		// Token: 0x04003846 RID: 14406
		public static readonly ADPropertyDefinition CustomAttribute5 = ADRecipientSchema.CustomAttribute5;

		// Token: 0x04003847 RID: 14407
		public static readonly ADPropertyDefinition CustomAttribute6 = ADRecipientSchema.CustomAttribute6;

		// Token: 0x04003848 RID: 14408
		public static readonly ADPropertyDefinition CustomAttribute7 = ADRecipientSchema.CustomAttribute7;

		// Token: 0x04003849 RID: 14409
		public static readonly ADPropertyDefinition CustomAttribute8 = ADRecipientSchema.CustomAttribute8;

		// Token: 0x0400384A RID: 14410
		public static readonly ADPropertyDefinition CustomAttribute9 = ADRecipientSchema.CustomAttribute9;

		// Token: 0x0400384B RID: 14411
		public static readonly ADPropertyDefinition ExtensionCustomAttribute1 = ADRecipientSchema.ExtensionCustomAttribute1;

		// Token: 0x0400384C RID: 14412
		public static readonly ADPropertyDefinition ExtensionCustomAttribute2 = ADRecipientSchema.ExtensionCustomAttribute2;

		// Token: 0x0400384D RID: 14413
		public static readonly ADPropertyDefinition ExtensionCustomAttribute3 = ADRecipientSchema.ExtensionCustomAttribute3;

		// Token: 0x0400384E RID: 14414
		public static readonly ADPropertyDefinition ExtensionCustomAttribute4 = ADRecipientSchema.ExtensionCustomAttribute4;

		// Token: 0x0400384F RID: 14415
		public static readonly ADPropertyDefinition ExtensionCustomAttribute5 = ADRecipientSchema.ExtensionCustomAttribute5;

		// Token: 0x04003850 RID: 14416
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04003851 RID: 14417
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04003852 RID: 14418
		public static readonly ADPropertyDefinition GrantSendOnBehalfTo = ADRecipientSchema.GrantSendOnBehalfTo;

		// Token: 0x04003853 RID: 14419
		public static readonly ADPropertyDefinition HiddenFromAddressListsEnabled = ADRecipientSchema.HiddenFromAddressListsEnabled;

		// Token: 0x04003854 RID: 14420
		public static readonly ADPropertyDefinition HiddenFromAddressListsValue = ADRecipientSchema.HiddenFromAddressListsValue;

		// Token: 0x04003855 RID: 14421
		public static readonly ADPropertyDefinition LastExchangeChangedTime = ADRecipientSchema.LastExchangeChangedTime;

		// Token: 0x04003856 RID: 14422
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x04003857 RID: 14423
		public static readonly ADPropertyDefinition MaxSendSize = ADRecipientSchema.MaxSendSize;

		// Token: 0x04003858 RID: 14424
		public static readonly ADPropertyDefinition MaxReceiveSize = ADRecipientSchema.MaxReceiveSize;

		// Token: 0x04003859 RID: 14425
		public static readonly ADPropertyDefinition ModerationEnabled = ADRecipientSchema.ModerationEnabled;

		// Token: 0x0400385A RID: 14426
		public static readonly ADPropertyDefinition ModerationFlags = ADRecipientSchema.ModerationFlags;

		// Token: 0x0400385B RID: 14427
		public static readonly ADPropertyDefinition ModeratedBy = ADRecipientSchema.ModeratedBy;

		// Token: 0x0400385C RID: 14428
		public static readonly ADPropertyDefinition PoliciesIncluded = ADRecipientSchema.ReadOnlyPoliciesIncluded;

		// Token: 0x0400385D RID: 14429
		public static readonly ADPropertyDefinition PoliciesExcluded = ADRecipientSchema.ReadOnlyPoliciesExcluded;

		// Token: 0x0400385E RID: 14430
		public static readonly ADPropertyDefinition EmailAddressPolicyEnabled = ADRecipientSchema.EmailAddressPolicyEnabled;

		// Token: 0x0400385F RID: 14431
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x04003860 RID: 14432
		public static readonly ADPropertyDefinition RecipientType = ADRecipientSchema.RecipientType;

		// Token: 0x04003861 RID: 14433
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x04003862 RID: 14434
		public static readonly ADPropertyDefinition RejectMessagesFrom = ADRecipientSchema.RejectMessagesFrom;

		// Token: 0x04003863 RID: 14435
		public static readonly ADPropertyDefinition RejectMessagesFromDLMembers = ADRecipientSchema.RejectMessagesFromDLMembers;

		// Token: 0x04003864 RID: 14436
		public static readonly ADPropertyDefinition RejectMessagesFromSendersOrMembers = ADRecipientSchema.RejectMessagesFromSendersOrMembers;

		// Token: 0x04003865 RID: 14437
		public static readonly ADPropertyDefinition RequireSenderAuthenticationEnabled = ADRecipientSchema.RequireAllSendersAreAuthenticated;

		// Token: 0x04003866 RID: 14438
		public static readonly ADPropertyDefinition SimpleDisplayName = ADRecipientSchema.SimpleDisplayName;

		// Token: 0x04003867 RID: 14439
		public static readonly ADPropertyDefinition SendModerationNotifications = ADRecipientSchema.SendModerationNotifications;

		// Token: 0x04003868 RID: 14440
		public static readonly ADPropertyDefinition UMDtmfMap = ADRecipientSchema.UMDtmfMap;

		// Token: 0x04003869 RID: 14441
		public static readonly ADPropertyDefinition WindowsEmailAddress = ADRecipientSchema.WindowsEmailAddress;

		// Token: 0x0400386A RID: 14442
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x0400386B RID: 14443
		public static readonly ADPropertyDefinition OrganizationalUnit = ADRecipientSchema.OrganizationalUnit;

		// Token: 0x0400386C RID: 14444
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x0400386D RID: 14445
		public static readonly ADPropertyDefinition MailTip = ADRecipientSchema.DefaultMailTip;

		// Token: 0x0400386E RID: 14446
		public static readonly ADPropertyDefinition MailTipTranslations = ADRecipientSchema.MailTipTranslations;

		// Token: 0x0400386F RID: 14447
		public static readonly ADPropertyDefinition UsnCreated = ADRecipientSchema.UsnCreated;
	}
}
