using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E0F RID: 3599
	public class SetUMMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<UMMailbox>
	{
		// Token: 0x0600D60D RID: 54797 RVA: 0x001302C8 File Offset: 0x0012E4C8
		private SetUMMailboxCommand() : base("Set-UMMailbox")
		{
		}

		// Token: 0x0600D60E RID: 54798 RVA: 0x001302D5 File Offset: 0x0012E4D5
		public SetUMMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D60F RID: 54799 RVA: 0x001302E4 File Offset: 0x0012E4E4
		public virtual SetUMMailboxCommand SetParameters(SetUMMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D610 RID: 54800 RVA: 0x001302EE File Offset: 0x0012E4EE
		public virtual SetUMMailboxCommand SetParameters(SetUMMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E10 RID: 3600
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A5A8 RID: 42408
			// (set) Token: 0x0600D611 RID: 54801 RVA: 0x001302F8 File Offset: 0x0012E4F8
			public virtual string PhoneNumber
			{
				set
				{
					base.PowerSharpParameters["PhoneNumber"] = value;
				}
			}

			// Token: 0x1700A5A9 RID: 42409
			// (set) Token: 0x0600D612 RID: 54802 RVA: 0x0013030B File Offset: 0x0012E50B
			public virtual MultiValuedProperty<string> AirSyncNumbers
			{
				set
				{
					base.PowerSharpParameters["AirSyncNumbers"] = value;
				}
			}

			// Token: 0x1700A5AA RID: 42410
			// (set) Token: 0x0600D613 RID: 54803 RVA: 0x0013031E File Offset: 0x0012E51E
			public virtual SwitchParameter VerifyGlobalRoutingEntry
			{
				set
				{
					base.PowerSharpParameters["VerifyGlobalRoutingEntry"] = value;
				}
			}

			// Token: 0x1700A5AB RID: 42411
			// (set) Token: 0x0600D614 RID: 54804 RVA: 0x00130336 File Offset: 0x0012E536
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700A5AC RID: 42412
			// (set) Token: 0x0600D615 RID: 54805 RVA: 0x00130354 File Offset: 0x0012E554
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A5AD RID: 42413
			// (set) Token: 0x0600D616 RID: 54806 RVA: 0x0013036C File Offset: 0x0012E56C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A5AE RID: 42414
			// (set) Token: 0x0600D617 RID: 54807 RVA: 0x0013037F File Offset: 0x0012E57F
			public virtual bool TUIAccessToCalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToCalendarEnabled"] = value;
				}
			}

			// Token: 0x1700A5AF RID: 42415
			// (set) Token: 0x0600D618 RID: 54808 RVA: 0x00130397 File Offset: 0x0012E597
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x1700A5B0 RID: 42416
			// (set) Token: 0x0600D619 RID: 54809 RVA: 0x001303AF File Offset: 0x0012E5AF
			public virtual bool TUIAccessToEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToEmailEnabled"] = value;
				}
			}

			// Token: 0x1700A5B1 RID: 42417
			// (set) Token: 0x0600D61A RID: 54810 RVA: 0x001303C7 File Offset: 0x0012E5C7
			public virtual bool SubscriberAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["SubscriberAccessEnabled"] = value;
				}
			}

			// Token: 0x1700A5B2 RID: 42418
			// (set) Token: 0x0600D61B RID: 54811 RVA: 0x001303DF File Offset: 0x0012E5DF
			public virtual bool MissedCallNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MissedCallNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700A5B3 RID: 42419
			// (set) Token: 0x0600D61C RID: 54812 RVA: 0x001303F7 File Offset: 0x0012E5F7
			public virtual UMSMSNotificationOptions UMSMSNotificationOption
			{
				set
				{
					base.PowerSharpParameters["UMSMSNotificationOption"] = value;
				}
			}

			// Token: 0x1700A5B4 RID: 42420
			// (set) Token: 0x0600D61D RID: 54813 RVA: 0x0013040F File Offset: 0x0012E60F
			public virtual bool PinlessAccessToVoiceMailEnabled
			{
				set
				{
					base.PowerSharpParameters["PinlessAccessToVoiceMailEnabled"] = value;
				}
			}

			// Token: 0x1700A5B5 RID: 42421
			// (set) Token: 0x0600D61E RID: 54814 RVA: 0x00130427 File Offset: 0x0012E627
			public virtual bool AnonymousCallersCanLeaveMessages
			{
				set
				{
					base.PowerSharpParameters["AnonymousCallersCanLeaveMessages"] = value;
				}
			}

			// Token: 0x1700A5B6 RID: 42422
			// (set) Token: 0x0600D61F RID: 54815 RVA: 0x0013043F File Offset: 0x0012E63F
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x1700A5B7 RID: 42423
			// (set) Token: 0x0600D620 RID: 54816 RVA: 0x00130457 File Offset: 0x0012E657
			public virtual bool VoiceMailAnalysisEnabled
			{
				set
				{
					base.PowerSharpParameters["VoiceMailAnalysisEnabled"] = value;
				}
			}

			// Token: 0x1700A5B8 RID: 42424
			// (set) Token: 0x0600D621 RID: 54817 RVA: 0x0013046F File Offset: 0x0012E66F
			public virtual bool PlayOnPhoneEnabled
			{
				set
				{
					base.PowerSharpParameters["PlayOnPhoneEnabled"] = value;
				}
			}

			// Token: 0x1700A5B9 RID: 42425
			// (set) Token: 0x0600D622 RID: 54818 RVA: 0x00130487 File Offset: 0x0012E687
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x1700A5BA RID: 42426
			// (set) Token: 0x0600D623 RID: 54819 RVA: 0x0013049F File Offset: 0x0012E69F
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A5BB RID: 42427
			// (set) Token: 0x0600D624 RID: 54820 RVA: 0x001304B7 File Offset: 0x0012E6B7
			public virtual string OperatorNumber
			{
				set
				{
					base.PowerSharpParameters["OperatorNumber"] = value;
				}
			}

			// Token: 0x1700A5BC RID: 42428
			// (set) Token: 0x0600D625 RID: 54821 RVA: 0x001304CA File Offset: 0x0012E6CA
			public virtual string PhoneProviderId
			{
				set
				{
					base.PowerSharpParameters["PhoneProviderId"] = value;
				}
			}

			// Token: 0x1700A5BD RID: 42429
			// (set) Token: 0x0600D626 RID: 54822 RVA: 0x001304DD File Offset: 0x0012E6DD
			public virtual AudioCodecEnum? CallAnsweringAudioCodec
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringAudioCodec"] = value;
				}
			}

			// Token: 0x1700A5BE RID: 42430
			// (set) Token: 0x0600D627 RID: 54823 RVA: 0x001304F5 File Offset: 0x0012E6F5
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x1700A5BF RID: 42431
			// (set) Token: 0x0600D628 RID: 54824 RVA: 0x0013050D File Offset: 0x0012E70D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A5C0 RID: 42432
			// (set) Token: 0x0600D629 RID: 54825 RVA: 0x00130520 File Offset: 0x0012E720
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A5C1 RID: 42433
			// (set) Token: 0x0600D62A RID: 54826 RVA: 0x00130538 File Offset: 0x0012E738
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A5C2 RID: 42434
			// (set) Token: 0x0600D62B RID: 54827 RVA: 0x00130550 File Offset: 0x0012E750
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A5C3 RID: 42435
			// (set) Token: 0x0600D62C RID: 54828 RVA: 0x00130568 File Offset: 0x0012E768
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A5C4 RID: 42436
			// (set) Token: 0x0600D62D RID: 54829 RVA: 0x00130580 File Offset: 0x0012E780
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E11 RID: 3601
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A5C5 RID: 42437
			// (set) Token: 0x0600D62F RID: 54831 RVA: 0x001305A0 File Offset: 0x0012E7A0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A5C6 RID: 42438
			// (set) Token: 0x0600D630 RID: 54832 RVA: 0x001305BE File Offset: 0x0012E7BE
			public virtual string PhoneNumber
			{
				set
				{
					base.PowerSharpParameters["PhoneNumber"] = value;
				}
			}

			// Token: 0x1700A5C7 RID: 42439
			// (set) Token: 0x0600D631 RID: 54833 RVA: 0x001305D1 File Offset: 0x0012E7D1
			public virtual MultiValuedProperty<string> AirSyncNumbers
			{
				set
				{
					base.PowerSharpParameters["AirSyncNumbers"] = value;
				}
			}

			// Token: 0x1700A5C8 RID: 42440
			// (set) Token: 0x0600D632 RID: 54834 RVA: 0x001305E4 File Offset: 0x0012E7E4
			public virtual SwitchParameter VerifyGlobalRoutingEntry
			{
				set
				{
					base.PowerSharpParameters["VerifyGlobalRoutingEntry"] = value;
				}
			}

			// Token: 0x1700A5C9 RID: 42441
			// (set) Token: 0x0600D633 RID: 54835 RVA: 0x001305FC File Offset: 0x0012E7FC
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700A5CA RID: 42442
			// (set) Token: 0x0600D634 RID: 54836 RVA: 0x0013061A File Offset: 0x0012E81A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A5CB RID: 42443
			// (set) Token: 0x0600D635 RID: 54837 RVA: 0x00130632 File Offset: 0x0012E832
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A5CC RID: 42444
			// (set) Token: 0x0600D636 RID: 54838 RVA: 0x00130645 File Offset: 0x0012E845
			public virtual bool TUIAccessToCalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToCalendarEnabled"] = value;
				}
			}

			// Token: 0x1700A5CD RID: 42445
			// (set) Token: 0x0600D637 RID: 54839 RVA: 0x0013065D File Offset: 0x0012E85D
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x1700A5CE RID: 42446
			// (set) Token: 0x0600D638 RID: 54840 RVA: 0x00130675 File Offset: 0x0012E875
			public virtual bool TUIAccessToEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToEmailEnabled"] = value;
				}
			}

			// Token: 0x1700A5CF RID: 42447
			// (set) Token: 0x0600D639 RID: 54841 RVA: 0x0013068D File Offset: 0x0012E88D
			public virtual bool SubscriberAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["SubscriberAccessEnabled"] = value;
				}
			}

			// Token: 0x1700A5D0 RID: 42448
			// (set) Token: 0x0600D63A RID: 54842 RVA: 0x001306A5 File Offset: 0x0012E8A5
			public virtual bool MissedCallNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MissedCallNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700A5D1 RID: 42449
			// (set) Token: 0x0600D63B RID: 54843 RVA: 0x001306BD File Offset: 0x0012E8BD
			public virtual UMSMSNotificationOptions UMSMSNotificationOption
			{
				set
				{
					base.PowerSharpParameters["UMSMSNotificationOption"] = value;
				}
			}

			// Token: 0x1700A5D2 RID: 42450
			// (set) Token: 0x0600D63C RID: 54844 RVA: 0x001306D5 File Offset: 0x0012E8D5
			public virtual bool PinlessAccessToVoiceMailEnabled
			{
				set
				{
					base.PowerSharpParameters["PinlessAccessToVoiceMailEnabled"] = value;
				}
			}

			// Token: 0x1700A5D3 RID: 42451
			// (set) Token: 0x0600D63D RID: 54845 RVA: 0x001306ED File Offset: 0x0012E8ED
			public virtual bool AnonymousCallersCanLeaveMessages
			{
				set
				{
					base.PowerSharpParameters["AnonymousCallersCanLeaveMessages"] = value;
				}
			}

			// Token: 0x1700A5D4 RID: 42452
			// (set) Token: 0x0600D63E RID: 54846 RVA: 0x00130705 File Offset: 0x0012E905
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x1700A5D5 RID: 42453
			// (set) Token: 0x0600D63F RID: 54847 RVA: 0x0013071D File Offset: 0x0012E91D
			public virtual bool VoiceMailAnalysisEnabled
			{
				set
				{
					base.PowerSharpParameters["VoiceMailAnalysisEnabled"] = value;
				}
			}

			// Token: 0x1700A5D6 RID: 42454
			// (set) Token: 0x0600D640 RID: 54848 RVA: 0x00130735 File Offset: 0x0012E935
			public virtual bool PlayOnPhoneEnabled
			{
				set
				{
					base.PowerSharpParameters["PlayOnPhoneEnabled"] = value;
				}
			}

			// Token: 0x1700A5D7 RID: 42455
			// (set) Token: 0x0600D641 RID: 54849 RVA: 0x0013074D File Offset: 0x0012E94D
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x1700A5D8 RID: 42456
			// (set) Token: 0x0600D642 RID: 54850 RVA: 0x00130765 File Offset: 0x0012E965
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A5D9 RID: 42457
			// (set) Token: 0x0600D643 RID: 54851 RVA: 0x0013077D File Offset: 0x0012E97D
			public virtual string OperatorNumber
			{
				set
				{
					base.PowerSharpParameters["OperatorNumber"] = value;
				}
			}

			// Token: 0x1700A5DA RID: 42458
			// (set) Token: 0x0600D644 RID: 54852 RVA: 0x00130790 File Offset: 0x0012E990
			public virtual string PhoneProviderId
			{
				set
				{
					base.PowerSharpParameters["PhoneProviderId"] = value;
				}
			}

			// Token: 0x1700A5DB RID: 42459
			// (set) Token: 0x0600D645 RID: 54853 RVA: 0x001307A3 File Offset: 0x0012E9A3
			public virtual AudioCodecEnum? CallAnsweringAudioCodec
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringAudioCodec"] = value;
				}
			}

			// Token: 0x1700A5DC RID: 42460
			// (set) Token: 0x0600D646 RID: 54854 RVA: 0x001307BB File Offset: 0x0012E9BB
			public virtual bool ImListMigrationCompleted
			{
				set
				{
					base.PowerSharpParameters["ImListMigrationCompleted"] = value;
				}
			}

			// Token: 0x1700A5DD RID: 42461
			// (set) Token: 0x0600D647 RID: 54855 RVA: 0x001307D3 File Offset: 0x0012E9D3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A5DE RID: 42462
			// (set) Token: 0x0600D648 RID: 54856 RVA: 0x001307E6 File Offset: 0x0012E9E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A5DF RID: 42463
			// (set) Token: 0x0600D649 RID: 54857 RVA: 0x001307FE File Offset: 0x0012E9FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A5E0 RID: 42464
			// (set) Token: 0x0600D64A RID: 54858 RVA: 0x00130816 File Offset: 0x0012EA16
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A5E1 RID: 42465
			// (set) Token: 0x0600D64B RID: 54859 RVA: 0x0013082E File Offset: 0x0012EA2E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A5E2 RID: 42466
			// (set) Token: 0x0600D64C RID: 54860 RVA: 0x00130846 File Offset: 0x0012EA46
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
