using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E12 RID: 3602
	public class SetUMMailboxPlanCommand : SyntheticCommandWithPipelineInputNoOutput<UMMailboxPlan>
	{
		// Token: 0x0600D64E RID: 54862 RVA: 0x00130866 File Offset: 0x0012EA66
		private SetUMMailboxPlanCommand() : base("Set-UMMailboxPlan")
		{
		}

		// Token: 0x0600D64F RID: 54863 RVA: 0x00130873 File Offset: 0x0012EA73
		public SetUMMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D650 RID: 54864 RVA: 0x00130882 File Offset: 0x0012EA82
		public virtual SetUMMailboxPlanCommand SetParameters(SetUMMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D651 RID: 54865 RVA: 0x0013088C File Offset: 0x0012EA8C
		public virtual SetUMMailboxPlanCommand SetParameters(SetUMMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E13 RID: 3603
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A5E3 RID: 42467
			// (set) Token: 0x0600D652 RID: 54866 RVA: 0x00130896 File Offset: 0x0012EA96
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700A5E4 RID: 42468
			// (set) Token: 0x0600D653 RID: 54867 RVA: 0x001308B4 File Offset: 0x0012EAB4
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A5E5 RID: 42469
			// (set) Token: 0x0600D654 RID: 54868 RVA: 0x001308CC File Offset: 0x0012EACC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A5E6 RID: 42470
			// (set) Token: 0x0600D655 RID: 54869 RVA: 0x001308DF File Offset: 0x0012EADF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A5E7 RID: 42471
			// (set) Token: 0x0600D656 RID: 54870 RVA: 0x001308F2 File Offset: 0x0012EAF2
			public virtual bool TUIAccessToCalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToCalendarEnabled"] = value;
				}
			}

			// Token: 0x1700A5E8 RID: 42472
			// (set) Token: 0x0600D657 RID: 54871 RVA: 0x0013090A File Offset: 0x0012EB0A
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x1700A5E9 RID: 42473
			// (set) Token: 0x0600D658 RID: 54872 RVA: 0x00130922 File Offset: 0x0012EB22
			public virtual bool TUIAccessToEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToEmailEnabled"] = value;
				}
			}

			// Token: 0x1700A5EA RID: 42474
			// (set) Token: 0x0600D659 RID: 54873 RVA: 0x0013093A File Offset: 0x0012EB3A
			public virtual bool SubscriberAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["SubscriberAccessEnabled"] = value;
				}
			}

			// Token: 0x1700A5EB RID: 42475
			// (set) Token: 0x0600D65A RID: 54874 RVA: 0x00130952 File Offset: 0x0012EB52
			public virtual bool PinlessAccessToVoiceMailEnabled
			{
				set
				{
					base.PowerSharpParameters["PinlessAccessToVoiceMailEnabled"] = value;
				}
			}

			// Token: 0x1700A5EC RID: 42476
			// (set) Token: 0x0600D65B RID: 54875 RVA: 0x0013096A File Offset: 0x0012EB6A
			public virtual string PhoneProviderId
			{
				set
				{
					base.PowerSharpParameters["PhoneProviderId"] = value;
				}
			}

			// Token: 0x1700A5ED RID: 42477
			// (set) Token: 0x0600D65C RID: 54876 RVA: 0x0013097D File Offset: 0x0012EB7D
			public virtual bool MissedCallNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MissedCallNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700A5EE RID: 42478
			// (set) Token: 0x0600D65D RID: 54877 RVA: 0x00130995 File Offset: 0x0012EB95
			public virtual UMSMSNotificationOptions UMSMSNotificationOption
			{
				set
				{
					base.PowerSharpParameters["UMSMSNotificationOption"] = value;
				}
			}

			// Token: 0x1700A5EF RID: 42479
			// (set) Token: 0x0600D65E RID: 54878 RVA: 0x001309AD File Offset: 0x0012EBAD
			public virtual bool AnonymousCallersCanLeaveMessages
			{
				set
				{
					base.PowerSharpParameters["AnonymousCallersCanLeaveMessages"] = value;
				}
			}

			// Token: 0x1700A5F0 RID: 42480
			// (set) Token: 0x0600D65F RID: 54879 RVA: 0x001309C5 File Offset: 0x0012EBC5
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x1700A5F1 RID: 42481
			// (set) Token: 0x0600D660 RID: 54880 RVA: 0x001309DD File Offset: 0x0012EBDD
			public virtual bool VoiceMailAnalysisEnabled
			{
				set
				{
					base.PowerSharpParameters["VoiceMailAnalysisEnabled"] = value;
				}
			}

			// Token: 0x1700A5F2 RID: 42482
			// (set) Token: 0x0600D661 RID: 54881 RVA: 0x001309F5 File Offset: 0x0012EBF5
			public virtual bool PlayOnPhoneEnabled
			{
				set
				{
					base.PowerSharpParameters["PlayOnPhoneEnabled"] = value;
				}
			}

			// Token: 0x1700A5F3 RID: 42483
			// (set) Token: 0x0600D662 RID: 54882 RVA: 0x00130A0D File Offset: 0x0012EC0D
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x1700A5F4 RID: 42484
			// (set) Token: 0x0600D663 RID: 54883 RVA: 0x00130A25 File Offset: 0x0012EC25
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A5F5 RID: 42485
			// (set) Token: 0x0600D664 RID: 54884 RVA: 0x00130A3D File Offset: 0x0012EC3D
			public virtual string OperatorNumber
			{
				set
				{
					base.PowerSharpParameters["OperatorNumber"] = value;
				}
			}

			// Token: 0x1700A5F6 RID: 42486
			// (set) Token: 0x0600D665 RID: 54885 RVA: 0x00130A50 File Offset: 0x0012EC50
			public virtual AudioCodecEnum? CallAnsweringAudioCodec
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringAudioCodec"] = value;
				}
			}

			// Token: 0x1700A5F7 RID: 42487
			// (set) Token: 0x0600D666 RID: 54886 RVA: 0x00130A68 File Offset: 0x0012EC68
			public virtual bool UMProvisioningRequested
			{
				set
				{
					base.PowerSharpParameters["UMProvisioningRequested"] = value;
				}
			}

			// Token: 0x1700A5F8 RID: 42488
			// (set) Token: 0x0600D667 RID: 54887 RVA: 0x00130A80 File Offset: 0x0012EC80
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A5F9 RID: 42489
			// (set) Token: 0x0600D668 RID: 54888 RVA: 0x00130A93 File Offset: 0x0012EC93
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A5FA RID: 42490
			// (set) Token: 0x0600D669 RID: 54889 RVA: 0x00130AAB File Offset: 0x0012ECAB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A5FB RID: 42491
			// (set) Token: 0x0600D66A RID: 54890 RVA: 0x00130AC3 File Offset: 0x0012ECC3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A5FC RID: 42492
			// (set) Token: 0x0600D66B RID: 54891 RVA: 0x00130ADB File Offset: 0x0012ECDB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A5FD RID: 42493
			// (set) Token: 0x0600D66C RID: 54892 RVA: 0x00130AF3 File Offset: 0x0012ECF3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E14 RID: 3604
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A5FE RID: 42494
			// (set) Token: 0x0600D66E RID: 54894 RVA: 0x00130B13 File Offset: 0x0012ED13
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A5FF RID: 42495
			// (set) Token: 0x0600D66F RID: 54895 RVA: 0x00130B31 File Offset: 0x0012ED31
			public virtual string UMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["UMMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700A600 RID: 42496
			// (set) Token: 0x0600D670 RID: 54896 RVA: 0x00130B4F File Offset: 0x0012ED4F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A601 RID: 42497
			// (set) Token: 0x0600D671 RID: 54897 RVA: 0x00130B67 File Offset: 0x0012ED67
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A602 RID: 42498
			// (set) Token: 0x0600D672 RID: 54898 RVA: 0x00130B7A File Offset: 0x0012ED7A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A603 RID: 42499
			// (set) Token: 0x0600D673 RID: 54899 RVA: 0x00130B8D File Offset: 0x0012ED8D
			public virtual bool TUIAccessToCalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToCalendarEnabled"] = value;
				}
			}

			// Token: 0x1700A604 RID: 42500
			// (set) Token: 0x0600D674 RID: 54900 RVA: 0x00130BA5 File Offset: 0x0012EDA5
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x1700A605 RID: 42501
			// (set) Token: 0x0600D675 RID: 54901 RVA: 0x00130BBD File Offset: 0x0012EDBD
			public virtual bool TUIAccessToEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIAccessToEmailEnabled"] = value;
				}
			}

			// Token: 0x1700A606 RID: 42502
			// (set) Token: 0x0600D676 RID: 54902 RVA: 0x00130BD5 File Offset: 0x0012EDD5
			public virtual bool SubscriberAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["SubscriberAccessEnabled"] = value;
				}
			}

			// Token: 0x1700A607 RID: 42503
			// (set) Token: 0x0600D677 RID: 54903 RVA: 0x00130BED File Offset: 0x0012EDED
			public virtual bool PinlessAccessToVoiceMailEnabled
			{
				set
				{
					base.PowerSharpParameters["PinlessAccessToVoiceMailEnabled"] = value;
				}
			}

			// Token: 0x1700A608 RID: 42504
			// (set) Token: 0x0600D678 RID: 54904 RVA: 0x00130C05 File Offset: 0x0012EE05
			public virtual string PhoneProviderId
			{
				set
				{
					base.PowerSharpParameters["PhoneProviderId"] = value;
				}
			}

			// Token: 0x1700A609 RID: 42505
			// (set) Token: 0x0600D679 RID: 54905 RVA: 0x00130C18 File Offset: 0x0012EE18
			public virtual bool MissedCallNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MissedCallNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700A60A RID: 42506
			// (set) Token: 0x0600D67A RID: 54906 RVA: 0x00130C30 File Offset: 0x0012EE30
			public virtual UMSMSNotificationOptions UMSMSNotificationOption
			{
				set
				{
					base.PowerSharpParameters["UMSMSNotificationOption"] = value;
				}
			}

			// Token: 0x1700A60B RID: 42507
			// (set) Token: 0x0600D67B RID: 54907 RVA: 0x00130C48 File Offset: 0x0012EE48
			public virtual bool AnonymousCallersCanLeaveMessages
			{
				set
				{
					base.PowerSharpParameters["AnonymousCallersCanLeaveMessages"] = value;
				}
			}

			// Token: 0x1700A60C RID: 42508
			// (set) Token: 0x0600D67C RID: 54908 RVA: 0x00130C60 File Offset: 0x0012EE60
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x1700A60D RID: 42509
			// (set) Token: 0x0600D67D RID: 54909 RVA: 0x00130C78 File Offset: 0x0012EE78
			public virtual bool VoiceMailAnalysisEnabled
			{
				set
				{
					base.PowerSharpParameters["VoiceMailAnalysisEnabled"] = value;
				}
			}

			// Token: 0x1700A60E RID: 42510
			// (set) Token: 0x0600D67E RID: 54910 RVA: 0x00130C90 File Offset: 0x0012EE90
			public virtual bool PlayOnPhoneEnabled
			{
				set
				{
					base.PowerSharpParameters["PlayOnPhoneEnabled"] = value;
				}
			}

			// Token: 0x1700A60F RID: 42511
			// (set) Token: 0x0600D67F RID: 54911 RVA: 0x00130CA8 File Offset: 0x0012EEA8
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x1700A610 RID: 42512
			// (set) Token: 0x0600D680 RID: 54912 RVA: 0x00130CC0 File Offset: 0x0012EEC0
			public virtual AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
			{
				set
				{
					base.PowerSharpParameters["AllowUMCallsFromNonUsers"] = value;
				}
			}

			// Token: 0x1700A611 RID: 42513
			// (set) Token: 0x0600D681 RID: 54913 RVA: 0x00130CD8 File Offset: 0x0012EED8
			public virtual string OperatorNumber
			{
				set
				{
					base.PowerSharpParameters["OperatorNumber"] = value;
				}
			}

			// Token: 0x1700A612 RID: 42514
			// (set) Token: 0x0600D682 RID: 54914 RVA: 0x00130CEB File Offset: 0x0012EEEB
			public virtual AudioCodecEnum? CallAnsweringAudioCodec
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringAudioCodec"] = value;
				}
			}

			// Token: 0x1700A613 RID: 42515
			// (set) Token: 0x0600D683 RID: 54915 RVA: 0x00130D03 File Offset: 0x0012EF03
			public virtual bool UMProvisioningRequested
			{
				set
				{
					base.PowerSharpParameters["UMProvisioningRequested"] = value;
				}
			}

			// Token: 0x1700A614 RID: 42516
			// (set) Token: 0x0600D684 RID: 54916 RVA: 0x00130D1B File Offset: 0x0012EF1B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A615 RID: 42517
			// (set) Token: 0x0600D685 RID: 54917 RVA: 0x00130D2E File Offset: 0x0012EF2E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A616 RID: 42518
			// (set) Token: 0x0600D686 RID: 54918 RVA: 0x00130D46 File Offset: 0x0012EF46
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A617 RID: 42519
			// (set) Token: 0x0600D687 RID: 54919 RVA: 0x00130D5E File Offset: 0x0012EF5E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A618 RID: 42520
			// (set) Token: 0x0600D688 RID: 54920 RVA: 0x00130D76 File Offset: 0x0012EF76
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A619 RID: 42521
			// (set) Token: 0x0600D689 RID: 54921 RVA: 0x00130D8E File Offset: 0x0012EF8E
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
