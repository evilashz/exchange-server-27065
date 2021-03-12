using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077C RID: 1916
	internal class UMMailboxPlanSchema : ADPresentationSchema
	{
		// Token: 0x06005F7A RID: 24442 RVA: 0x001460F4 File Offset: 0x001442F4
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04004027 RID: 16423
		public static readonly ADPropertyDefinition AllowUMCallsFromNonUsers = ADRecipientSchema.AllowUMCallsFromNonUsers;

		// Token: 0x04004028 RID: 16424
		public static readonly ADPropertyDefinition CallAnsweringAudioCodec = ADUserSchema.CallAnsweringAudioCodec;

		// Token: 0x04004029 RID: 16425
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x0400402A RID: 16426
		public static readonly ADPropertyDefinition OperatorNumber = ADUserSchema.OperatorNumber;

		// Token: 0x0400402B RID: 16427
		public static readonly ADPropertyDefinition UMMailboxPolicy = ADUserSchema.UMMailboxPolicy;

		// Token: 0x0400402C RID: 16428
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x0400402D RID: 16429
		public static readonly ADPropertyDefinition PhoneProviderId = ADUserSchema.PhoneProviderId;

		// Token: 0x0400402E RID: 16430
		public static readonly ADPropertyDefinition AnonymousCallersCanLeaveMessages = ADUserSchema.AnonymousCallersCanLeaveMessages;

		// Token: 0x0400402F RID: 16431
		public static readonly ADPropertyDefinition ASREnabled = ADUserSchema.ASREnabled;

		// Token: 0x04004030 RID: 16432
		public static readonly ADPropertyDefinition VoiceMailAnalysisEnabled = ADUserSchema.VoiceMailAnalysisEnabled;

		// Token: 0x04004031 RID: 16433
		public static readonly ADPropertyDefinition FaxEnabled = ADUserSchema.FaxEnabled;

		// Token: 0x04004032 RID: 16434
		public static readonly ADPropertyDefinition MissedCallNotificationEnabled = ADUserSchema.MissedCallNotificationEnabled;

		// Token: 0x04004033 RID: 16435
		public static readonly ADPropertyDefinition UMSMSNotificationOption = ADUserSchema.UMSMSNotificationOption;

		// Token: 0x04004034 RID: 16436
		public static readonly ADPropertyDefinition SubscriberAccessEnabled = ADUserSchema.SubscriberAccessEnabled;

		// Token: 0x04004035 RID: 16437
		public static readonly ADPropertyDefinition PinlessAccessToVoiceMailEnabled = ADUserSchema.PinlessAccessToVoiceMailEnabled;

		// Token: 0x04004036 RID: 16438
		public static readonly ADPropertyDefinition TUIAccessToCalendarEnabled = ADUserSchema.TUIAccessToCalendarEnabled;

		// Token: 0x04004037 RID: 16439
		public static readonly ADPropertyDefinition TUIAccessToEmailEnabled = ADUserSchema.TUIAccessToEmailEnabled;

		// Token: 0x04004038 RID: 16440
		public static readonly ADPropertyDefinition PlayOnPhoneEnabled = ADUserSchema.PlayOnPhoneEnabled;

		// Token: 0x04004039 RID: 16441
		public static readonly ADPropertyDefinition CallAnsweringRulesEnabled = ADUserSchema.CallAnsweringRulesEnabled;

		// Token: 0x0400403A RID: 16442
		public static readonly ADPropertyDefinition UMEnabled = ADUserSchema.UMEnabled;

		// Token: 0x0400403B RID: 16443
		public static readonly ADPropertyDefinition UMRecipientDialPlanId = ADRecipientSchema.UMRecipientDialPlanId;

		// Token: 0x0400403C RID: 16444
		public static readonly ADPropertyDefinition UMProvisioningRequested = ADRecipientSchema.UMProvisioningRequested;
	}
}
