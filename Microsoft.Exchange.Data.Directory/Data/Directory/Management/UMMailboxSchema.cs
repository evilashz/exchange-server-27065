using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077A RID: 1914
	internal class UMMailboxSchema : ADPresentationSchema
	{
		// Token: 0x06005F15 RID: 24341 RVA: 0x00145045 File Offset: 0x00143245
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003FFC RID: 16380
		public static readonly ADPropertyDefinition AllowUMCallsFromNonUsers = ADRecipientSchema.AllowUMCallsFromNonUsers;

		// Token: 0x04003FFD RID: 16381
		public static readonly ADPropertyDefinition CallAnsweringAudioCodec = ADUserSchema.CallAnsweringAudioCodec;

		// Token: 0x04003FFE RID: 16382
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04003FFF RID: 16383
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04004000 RID: 16384
		public static readonly ADPropertyDefinition UMAddresses = ADRecipientSchema.UMAddresses;

		// Token: 0x04004001 RID: 16385
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x04004002 RID: 16386
		public static readonly ADPropertyDefinition LinkedMasterAccount = ADRecipientSchema.LinkedMasterAccount;

		// Token: 0x04004003 RID: 16387
		public static readonly ADPropertyDefinition OperatorNumber = ADUserSchema.OperatorNumber;

		// Token: 0x04004004 RID: 16388
		public static readonly ADPropertyDefinition PhoneProviderId = ADUserSchema.PhoneProviderId;

		// Token: 0x04004005 RID: 16389
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x04004006 RID: 16390
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04004007 RID: 16391
		public static readonly ADPropertyDefinition ServerLegacyDN = ADMailboxRecipientSchema.ServerLegacyDN;

		// Token: 0x04004008 RID: 16392
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x04004009 RID: 16393
		public static readonly ADPropertyDefinition UMDtmfMap = ADRecipientSchema.UMDtmfMap;

		// Token: 0x0400400A RID: 16394
		public static readonly ADPropertyDefinition UMMailboxPolicy = ADUserSchema.UMMailboxPolicy;

		// Token: 0x0400400B RID: 16395
		public static readonly ADPropertyDefinition UMRecipientDialPlanId = ADRecipientSchema.UMRecipientDialPlanId;

		// Token: 0x0400400C RID: 16396
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x0400400D RID: 16397
		public static readonly ADPropertyDefinition AnonymousCallersCanLeaveMessages = ADUserSchema.AnonymousCallersCanLeaveMessages;

		// Token: 0x0400400E RID: 16398
		public static readonly ADPropertyDefinition ASREnabled = ADUserSchema.ASREnabled;

		// Token: 0x0400400F RID: 16399
		public static readonly ADPropertyDefinition VoiceMailAnalysisEnabled = ADUserSchema.VoiceMailAnalysisEnabled;

		// Token: 0x04004010 RID: 16400
		public static readonly ADPropertyDefinition Extensions = ADRecipientSchema.Extensions;

		// Token: 0x04004011 RID: 16401
		public static readonly ADPropertyDefinition FaxEnabled = ADUserSchema.FaxEnabled;

		// Token: 0x04004012 RID: 16402
		public static readonly ADPropertyDefinition MissedCallNotificationEnabled = ADUserSchema.MissedCallNotificationEnabled;

		// Token: 0x04004013 RID: 16403
		public static readonly ADPropertyDefinition UMSMSNotificationOption = ADUserSchema.UMSMSNotificationOption;

		// Token: 0x04004014 RID: 16404
		public static readonly ADPropertyDefinition PinlessAccessToVoiceMailEnabled = ADUserSchema.PinlessAccessToVoiceMailEnabled;

		// Token: 0x04004015 RID: 16405
		public static readonly ADPropertyDefinition SIPResourceIdentifier = ADUserSchema.SIPResourceIdentifier;

		// Token: 0x04004016 RID: 16406
		public static readonly ADPropertyDefinition PhoneNumber = ADUserSchema.PhoneNumber;

		// Token: 0x04004017 RID: 16407
		public static readonly ADPropertyDefinition SubscriberAccessEnabled = ADUserSchema.SubscriberAccessEnabled;

		// Token: 0x04004018 RID: 16408
		public static readonly ADPropertyDefinition TUIAccessToCalendarEnabled = ADUserSchema.TUIAccessToCalendarEnabled;

		// Token: 0x04004019 RID: 16409
		public static readonly ADPropertyDefinition TUIAccessToEmailEnabled = ADUserSchema.TUIAccessToEmailEnabled;

		// Token: 0x0400401A RID: 16410
		public static readonly ADPropertyDefinition PlayOnPhoneEnabled = ADUserSchema.PlayOnPhoneEnabled;

		// Token: 0x0400401B RID: 16411
		public static readonly ADPropertyDefinition CallAnsweringRulesEnabled = ADUserSchema.CallAnsweringRulesEnabled;

		// Token: 0x0400401C RID: 16412
		public static readonly ADPropertyDefinition UMEnabled = ADUserSchema.UMEnabled;

		// Token: 0x0400401D RID: 16413
		public static readonly ADPropertyDefinition UMProvisioningRequested = ADRecipientSchema.UMProvisioningRequested;

		// Token: 0x0400401E RID: 16414
		public static readonly ADPropertyDefinition AccessTelephoneNumbers = ADUserSchema.AccessTelephoneNumbers;

		// Token: 0x0400401F RID: 16415
		public static readonly ADPropertyDefinition CallAnsweringRulesExtensions = ADUserSchema.CallAnsweringRulesExtensions;

		// Token: 0x04004020 RID: 16416
		public static readonly ADPropertyDefinition UCSImListMigrationCompleted = ADRecipientSchema.UCSImListMigrationCompleted;
	}
}
