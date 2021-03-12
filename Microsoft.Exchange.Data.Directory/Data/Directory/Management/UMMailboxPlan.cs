using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077D RID: 1917
	[Serializable]
	public class UMMailboxPlan : ADPresentationObject
	{
		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x001461ED File Offset: 0x001443ED
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return UMMailboxPlan.schema;
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x001461F4 File Offset: 0x001443F4
		public UMMailboxPlan()
		{
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x001461FC File Offset: 0x001443FC
		public UMMailboxPlan(ADRecipient dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x00146205 File Offset: 0x00144405
		internal static UMMailboxPlan FromDataObject(ADRecipient dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new UMMailboxPlan(dataObject);
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x00146212 File Offset: 0x00144412
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x00146219 File Offset: 0x00144419
		// (set) Token: 0x06005F83 RID: 24451 RVA: 0x0014622B File Offset: 0x0014442B
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[UMMailboxPlanSchema.DisplayName];
			}
			set
			{
				this[UMMailboxPlanSchema.DisplayName] = value;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x00146239 File Offset: 0x00144439
		// (set) Token: 0x06005F85 RID: 24453 RVA: 0x0014624B File Offset: 0x0014444B
		public bool UMEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.UMEnabled];
			}
			internal set
			{
				this[UMMailboxPlanSchema.UMEnabled] = value;
			}
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x0014625E File Offset: 0x0014445E
		// (set) Token: 0x06005F87 RID: 24455 RVA: 0x00146270 File Offset: 0x00144470
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[UMMailboxPlanSchema.UMRecipientDialPlanId];
			}
			set
			{
				this[UMMailboxPlanSchema.UMRecipientDialPlanId] = value;
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x0014627E File Offset: 0x0014447E
		// (set) Token: 0x06005F89 RID: 24457 RVA: 0x00146290 File Offset: 0x00144490
		[Parameter(Mandatory = false)]
		public bool TUIAccessToCalendarEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.TUIAccessToCalendarEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.TUIAccessToCalendarEnabled] = value;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06005F8A RID: 24458 RVA: 0x001462A3 File Offset: 0x001444A3
		// (set) Token: 0x06005F8B RID: 24459 RVA: 0x001462B5 File Offset: 0x001444B5
		[Parameter(Mandatory = false)]
		public bool FaxEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.FaxEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.FaxEnabled] = value;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x001462C8 File Offset: 0x001444C8
		// (set) Token: 0x06005F8D RID: 24461 RVA: 0x001462DA File Offset: 0x001444DA
		[Parameter(Mandatory = false)]
		public bool TUIAccessToEmailEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.TUIAccessToEmailEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.TUIAccessToEmailEnabled] = value;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x06005F8E RID: 24462 RVA: 0x001462ED File Offset: 0x001444ED
		// (set) Token: 0x06005F8F RID: 24463 RVA: 0x001462FF File Offset: 0x001444FF
		[Parameter(Mandatory = false)]
		public bool SubscriberAccessEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.SubscriberAccessEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.SubscriberAccessEnabled] = value;
			}
		}

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x06005F90 RID: 24464 RVA: 0x00146312 File Offset: 0x00144512
		// (set) Token: 0x06005F91 RID: 24465 RVA: 0x00146324 File Offset: 0x00144524
		[Parameter(Mandatory = false)]
		public bool PinlessAccessToVoiceMailEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.PinlessAccessToVoiceMailEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.PinlessAccessToVoiceMailEnabled] = value;
			}
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x06005F92 RID: 24466 RVA: 0x00146337 File Offset: 0x00144537
		// (set) Token: 0x06005F93 RID: 24467 RVA: 0x00146349 File Offset: 0x00144549
		[Parameter(Mandatory = false)]
		public string PhoneProviderId
		{
			get
			{
				return (string)this[UMMailboxPlanSchema.PhoneProviderId];
			}
			set
			{
				this[UMMailboxPlanSchema.PhoneProviderId] = value;
			}
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x00146357 File Offset: 0x00144557
		// (set) Token: 0x06005F95 RID: 24469 RVA: 0x00146369 File Offset: 0x00144569
		[Parameter(Mandatory = false)]
		public bool MissedCallNotificationEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.MissedCallNotificationEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.MissedCallNotificationEnabled] = value;
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x0014637C File Offset: 0x0014457C
		// (set) Token: 0x06005F97 RID: 24471 RVA: 0x0014638E File Offset: 0x0014458E
		[Parameter(Mandatory = false)]
		public UMSMSNotificationOptions UMSMSNotificationOption
		{
			get
			{
				return (UMSMSNotificationOptions)this[UMMailboxPlanSchema.UMSMSNotificationOption];
			}
			set
			{
				this[UMMailboxPlanSchema.UMSMSNotificationOption] = value;
			}
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x06005F98 RID: 24472 RVA: 0x001463A1 File Offset: 0x001445A1
		// (set) Token: 0x06005F99 RID: 24473 RVA: 0x001463B3 File Offset: 0x001445B3
		[Parameter(Mandatory = false)]
		public bool AnonymousCallersCanLeaveMessages
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.AnonymousCallersCanLeaveMessages];
			}
			set
			{
				this[UMMailboxPlanSchema.AnonymousCallersCanLeaveMessages] = value;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06005F9A RID: 24474 RVA: 0x001463C6 File Offset: 0x001445C6
		// (set) Token: 0x06005F9B RID: 24475 RVA: 0x001463D8 File Offset: 0x001445D8
		[Parameter(Mandatory = false)]
		public bool AutomaticSpeechRecognitionEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.ASREnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.ASREnabled] = value;
			}
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x06005F9C RID: 24476 RVA: 0x001463EB File Offset: 0x001445EB
		// (set) Token: 0x06005F9D RID: 24477 RVA: 0x001463FD File Offset: 0x001445FD
		[Parameter(Mandatory = false)]
		public bool VoiceMailAnalysisEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.VoiceMailAnalysisEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.VoiceMailAnalysisEnabled] = value;
			}
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x06005F9E RID: 24478 RVA: 0x00146410 File Offset: 0x00144610
		// (set) Token: 0x06005F9F RID: 24479 RVA: 0x00146422 File Offset: 0x00144622
		[Parameter(Mandatory = false)]
		public bool PlayOnPhoneEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.PlayOnPhoneEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.PlayOnPhoneEnabled] = value;
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x00146435 File Offset: 0x00144635
		// (set) Token: 0x06005FA1 RID: 24481 RVA: 0x00146447 File Offset: 0x00144647
		[Parameter(Mandatory = false)]
		public bool CallAnsweringRulesEnabled
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.CallAnsweringRulesEnabled];
			}
			set
			{
				this[UMMailboxPlanSchema.CallAnsweringRulesEnabled] = value;
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x06005FA2 RID: 24482 RVA: 0x0014645A File Offset: 0x0014465A
		// (set) Token: 0x06005FA3 RID: 24483 RVA: 0x0014646C File Offset: 0x0014466C
		[Parameter(Mandatory = false)]
		public AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
		{
			get
			{
				return (AllowUMCallsFromNonUsersFlags)this[UMMailboxPlanSchema.AllowUMCallsFromNonUsers];
			}
			set
			{
				this[UMMailboxPlanSchema.AllowUMCallsFromNonUsers] = value;
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x06005FA4 RID: 24484 RVA: 0x0014647F File Offset: 0x0014467F
		// (set) Token: 0x06005FA5 RID: 24485 RVA: 0x00146491 File Offset: 0x00144691
		[Parameter(Mandatory = false)]
		public string OperatorNumber
		{
			get
			{
				return (string)this[UMMailboxPlanSchema.OperatorNumber];
			}
			set
			{
				this[UMMailboxPlanSchema.OperatorNumber] = value;
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x0014649F File Offset: 0x0014469F
		// (set) Token: 0x06005FA7 RID: 24487 RVA: 0x001464B1 File Offset: 0x001446B1
		public ADObjectId UMMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[UMMailboxPlanSchema.UMMailboxPolicy];
			}
			set
			{
				this[UMMailboxPlanSchema.UMMailboxPolicy] = value;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x06005FA8 RID: 24488 RVA: 0x001464BF File Offset: 0x001446BF
		// (set) Token: 0x06005FA9 RID: 24489 RVA: 0x001464D1 File Offset: 0x001446D1
		[Parameter(Mandatory = false)]
		public AudioCodecEnum? CallAnsweringAudioCodec
		{
			get
			{
				return (AudioCodecEnum?)this[UMMailboxPlanSchema.CallAnsweringAudioCodec];
			}
			set
			{
				this[UMMailboxPlanSchema.CallAnsweringAudioCodec] = value;
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x001464E4 File Offset: 0x001446E4
		// (set) Token: 0x06005FAB RID: 24491 RVA: 0x001464F6 File Offset: 0x001446F6
		[Parameter(Mandatory = false)]
		public bool UMProvisioningRequested
		{
			get
			{
				return (bool)this[UMMailboxPlanSchema.UMProvisioningRequested];
			}
			set
			{
				this[UMMailboxPlanSchema.UMProvisioningRequested] = value;
			}
		}

		// Token: 0x0400403D RID: 16445
		private static UMMailboxPlanSchema schema = ObjectSchema.GetInstance<UMMailboxPlanSchema>();
	}
}
