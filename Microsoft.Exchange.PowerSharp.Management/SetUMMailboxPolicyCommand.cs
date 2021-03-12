using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001EF RID: 495
	public class SetUMMailboxPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<UMMailboxPolicy>
	{
		// Token: 0x06002812 RID: 10258 RVA: 0x0004BBF6 File Offset: 0x00049DF6
		private SetUMMailboxPolicyCommand() : base("Set-UMMailboxPolicy")
		{
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0004BC03 File Offset: 0x00049E03
		public SetUMMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0004BC12 File Offset: 0x00049E12
		public virtual SetUMMailboxPolicyCommand SetParameters(SetUMMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0004BC1C File Offset: 0x00049E1C
		public virtual SetUMMailboxPolicyCommand SetParameters(SetUMMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001F0 RID: 496
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000FED RID: 4077
			// (set) Token: 0x06002816 RID: 10262 RVA: 0x0004BC26 File Offset: 0x00049E26
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17000FEE RID: 4078
			// (set) Token: 0x06002817 RID: 10263 RVA: 0x0004BC44 File Offset: 0x00049E44
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17000FEF RID: 4079
			// (set) Token: 0x06002818 RID: 10264 RVA: 0x0004BC5C File Offset: 0x00049E5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FF0 RID: 4080
			// (set) Token: 0x06002819 RID: 10265 RVA: 0x0004BC6F File Offset: 0x00049E6F
			public virtual int MaxGreetingDuration
			{
				set
				{
					base.PowerSharpParameters["MaxGreetingDuration"] = value;
				}
			}

			// Token: 0x17000FF1 RID: 4081
			// (set) Token: 0x0600281A RID: 10266 RVA: 0x0004BC87 File Offset: 0x00049E87
			public virtual Unlimited<int> MaxLogonAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxLogonAttempts"] = value;
				}
			}

			// Token: 0x17000FF2 RID: 4082
			// (set) Token: 0x0600281B RID: 10267 RVA: 0x0004BC9F File Offset: 0x00049E9F
			public virtual bool AllowCommonPatterns
			{
				set
				{
					base.PowerSharpParameters["AllowCommonPatterns"] = value;
				}
			}

			// Token: 0x17000FF3 RID: 4083
			// (set) Token: 0x0600281C RID: 10268 RVA: 0x0004BCB7 File Offset: 0x00049EB7
			public virtual Unlimited<EnhancedTimeSpan> PINLifetime
			{
				set
				{
					base.PowerSharpParameters["PINLifetime"] = value;
				}
			}

			// Token: 0x17000FF4 RID: 4084
			// (set) Token: 0x0600281D RID: 10269 RVA: 0x0004BCCF File Offset: 0x00049ECF
			public virtual int PINHistoryCount
			{
				set
				{
					base.PowerSharpParameters["PINHistoryCount"] = value;
				}
			}

			// Token: 0x17000FF5 RID: 4085
			// (set) Token: 0x0600281E RID: 10270 RVA: 0x0004BCE7 File Offset: 0x00049EE7
			public virtual bool AllowSMSNotification
			{
				set
				{
					base.PowerSharpParameters["AllowSMSNotification"] = value;
				}
			}

			// Token: 0x17000FF6 RID: 4086
			// (set) Token: 0x0600281F RID: 10271 RVA: 0x0004BCFF File Offset: 0x00049EFF
			public virtual DRMProtectionOptions ProtectUnauthenticatedVoiceMail
			{
				set
				{
					base.PowerSharpParameters["ProtectUnauthenticatedVoiceMail"] = value;
				}
			}

			// Token: 0x17000FF7 RID: 4087
			// (set) Token: 0x06002820 RID: 10272 RVA: 0x0004BD17 File Offset: 0x00049F17
			public virtual DRMProtectionOptions ProtectAuthenticatedVoiceMail
			{
				set
				{
					base.PowerSharpParameters["ProtectAuthenticatedVoiceMail"] = value;
				}
			}

			// Token: 0x17000FF8 RID: 4088
			// (set) Token: 0x06002821 RID: 10273 RVA: 0x0004BD2F File Offset: 0x00049F2F
			public virtual string ProtectedVoiceMailText
			{
				set
				{
					base.PowerSharpParameters["ProtectedVoiceMailText"] = value;
				}
			}

			// Token: 0x17000FF9 RID: 4089
			// (set) Token: 0x06002822 RID: 10274 RVA: 0x0004BD42 File Offset: 0x00049F42
			public virtual bool RequireProtectedPlayOnPhone
			{
				set
				{
					base.PowerSharpParameters["RequireProtectedPlayOnPhone"] = value;
				}
			}

			// Token: 0x17000FFA RID: 4090
			// (set) Token: 0x06002823 RID: 10275 RVA: 0x0004BD5A File Offset: 0x00049F5A
			public virtual int MinPINLength
			{
				set
				{
					base.PowerSharpParameters["MinPINLength"] = value;
				}
			}

			// Token: 0x17000FFB RID: 4091
			// (set) Token: 0x06002824 RID: 10276 RVA: 0x0004BD72 File Offset: 0x00049F72
			public virtual string FaxMessageText
			{
				set
				{
					base.PowerSharpParameters["FaxMessageText"] = value;
				}
			}

			// Token: 0x17000FFC RID: 4092
			// (set) Token: 0x06002825 RID: 10277 RVA: 0x0004BD85 File Offset: 0x00049F85
			public virtual string UMEnabledText
			{
				set
				{
					base.PowerSharpParameters["UMEnabledText"] = value;
				}
			}

			// Token: 0x17000FFD RID: 4093
			// (set) Token: 0x06002826 RID: 10278 RVA: 0x0004BD98 File Offset: 0x00049F98
			public virtual string ResetPINText
			{
				set
				{
					base.PowerSharpParameters["ResetPINText"] = value;
				}
			}

			// Token: 0x17000FFE RID: 4094
			// (set) Token: 0x06002827 RID: 10279 RVA: 0x0004BDAB File Offset: 0x00049FAB
			public virtual MultiValuedProperty<string> SourceForestPolicyNames
			{
				set
				{
					base.PowerSharpParameters["SourceForestPolicyNames"] = value;
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (set) Token: 0x06002828 RID: 10280 RVA: 0x0004BDBE File Offset: 0x00049FBE
			public virtual string VoiceMailText
			{
				set
				{
					base.PowerSharpParameters["VoiceMailText"] = value;
				}
			}

			// Token: 0x17001000 RID: 4096
			// (set) Token: 0x06002829 RID: 10281 RVA: 0x0004BDD1 File Offset: 0x00049FD1
			public virtual string FaxServerURI
			{
				set
				{
					base.PowerSharpParameters["FaxServerURI"] = value;
				}
			}

			// Token: 0x17001001 RID: 4097
			// (set) Token: 0x0600282A RID: 10282 RVA: 0x0004BDE4 File Offset: 0x00049FE4
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x17001002 RID: 4098
			// (set) Token: 0x0600282B RID: 10283 RVA: 0x0004BDF7 File Offset: 0x00049FF7
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x17001003 RID: 4099
			// (set) Token: 0x0600282C RID: 10284 RVA: 0x0004BE0A File Offset: 0x0004A00A
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x17001004 RID: 4100
			// (set) Token: 0x0600282D RID: 10285 RVA: 0x0004BE22 File Offset: 0x0004A022
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x17001005 RID: 4101
			// (set) Token: 0x0600282E RID: 10286 RVA: 0x0004BE3A File Offset: 0x0004A03A
			public virtual Unlimited<int> LogonFailuresBeforePINReset
			{
				set
				{
					base.PowerSharpParameters["LogonFailuresBeforePINReset"] = value;
				}
			}

			// Token: 0x17001006 RID: 4102
			// (set) Token: 0x0600282F RID: 10287 RVA: 0x0004BE52 File Offset: 0x0004A052
			public virtual bool AllowMissedCallNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowMissedCallNotifications"] = value;
				}
			}

			// Token: 0x17001007 RID: 4103
			// (set) Token: 0x06002830 RID: 10288 RVA: 0x0004BE6A File Offset: 0x0004A06A
			public virtual bool AllowFax
			{
				set
				{
					base.PowerSharpParameters["AllowFax"] = value;
				}
			}

			// Token: 0x17001008 RID: 4104
			// (set) Token: 0x06002831 RID: 10289 RVA: 0x0004BE82 File Offset: 0x0004A082
			public virtual bool AllowTUIAccessToCalendar
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToCalendar"] = value;
				}
			}

			// Token: 0x17001009 RID: 4105
			// (set) Token: 0x06002832 RID: 10290 RVA: 0x0004BE9A File Offset: 0x0004A09A
			public virtual bool AllowTUIAccessToEmail
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToEmail"] = value;
				}
			}

			// Token: 0x1700100A RID: 4106
			// (set) Token: 0x06002833 RID: 10291 RVA: 0x0004BEB2 File Offset: 0x0004A0B2
			public virtual bool AllowSubscriberAccess
			{
				set
				{
					base.PowerSharpParameters["AllowSubscriberAccess"] = value;
				}
			}

			// Token: 0x1700100B RID: 4107
			// (set) Token: 0x06002834 RID: 10292 RVA: 0x0004BECA File Offset: 0x0004A0CA
			public virtual bool AllowTUIAccessToDirectory
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToDirectory"] = value;
				}
			}

			// Token: 0x1700100C RID: 4108
			// (set) Token: 0x06002835 RID: 10293 RVA: 0x0004BEE2 File Offset: 0x0004A0E2
			public virtual bool AllowTUIAccessToPersonalContacts
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToPersonalContacts"] = value;
				}
			}

			// Token: 0x1700100D RID: 4109
			// (set) Token: 0x06002836 RID: 10294 RVA: 0x0004BEFA File Offset: 0x0004A0FA
			public virtual bool AllowAutomaticSpeechRecognition
			{
				set
				{
					base.PowerSharpParameters["AllowAutomaticSpeechRecognition"] = value;
				}
			}

			// Token: 0x1700100E RID: 4110
			// (set) Token: 0x06002837 RID: 10295 RVA: 0x0004BF12 File Offset: 0x0004A112
			public virtual bool AllowPlayOnPhone
			{
				set
				{
					base.PowerSharpParameters["AllowPlayOnPhone"] = value;
				}
			}

			// Token: 0x1700100F RID: 4111
			// (set) Token: 0x06002838 RID: 10296 RVA: 0x0004BF2A File Offset: 0x0004A12A
			public virtual bool AllowVoiceMailPreview
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceMailPreview"] = value;
				}
			}

			// Token: 0x17001010 RID: 4112
			// (set) Token: 0x06002839 RID: 10297 RVA: 0x0004BF42 File Offset: 0x0004A142
			public virtual bool AllowCallAnsweringRules
			{
				set
				{
					base.PowerSharpParameters["AllowCallAnsweringRules"] = value;
				}
			}

			// Token: 0x17001011 RID: 4113
			// (set) Token: 0x0600283A RID: 10298 RVA: 0x0004BF5A File Offset: 0x0004A15A
			public virtual bool AllowMessageWaitingIndicator
			{
				set
				{
					base.PowerSharpParameters["AllowMessageWaitingIndicator"] = value;
				}
			}

			// Token: 0x17001012 RID: 4114
			// (set) Token: 0x0600283B RID: 10299 RVA: 0x0004BF72 File Offset: 0x0004A172
			public virtual bool AllowPinlessVoiceMailAccess
			{
				set
				{
					base.PowerSharpParameters["AllowPinlessVoiceMailAccess"] = value;
				}
			}

			// Token: 0x17001013 RID: 4115
			// (set) Token: 0x0600283C RID: 10300 RVA: 0x0004BF8A File Offset: 0x0004A18A
			public virtual bool AllowVoiceResponseToOtherMessageTypes
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceResponseToOtherMessageTypes"] = value;
				}
			}

			// Token: 0x17001014 RID: 4116
			// (set) Token: 0x0600283D RID: 10301 RVA: 0x0004BFA2 File Offset: 0x0004A1A2
			public virtual bool AllowVoiceMailAnalysis
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceMailAnalysis"] = value;
				}
			}

			// Token: 0x17001015 RID: 4117
			// (set) Token: 0x0600283E RID: 10302 RVA: 0x0004BFBA File Offset: 0x0004A1BA
			public virtual bool AllowVoiceNotification
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceNotification"] = value;
				}
			}

			// Token: 0x17001016 RID: 4118
			// (set) Token: 0x0600283F RID: 10303 RVA: 0x0004BFD2 File Offset: 0x0004A1D2
			public virtual bool InformCallerOfVoiceMailAnalysis
			{
				set
				{
					base.PowerSharpParameters["InformCallerOfVoiceMailAnalysis"] = value;
				}
			}

			// Token: 0x17001017 RID: 4119
			// (set) Token: 0x06002840 RID: 10304 RVA: 0x0004BFEA File Offset: 0x0004A1EA
			public virtual SmtpAddress? VoiceMailPreviewPartnerAddress
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerAddress"] = value;
				}
			}

			// Token: 0x17001018 RID: 4120
			// (set) Token: 0x06002841 RID: 10305 RVA: 0x0004C002 File Offset: 0x0004A202
			public virtual string VoiceMailPreviewPartnerAssignedID
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerAssignedID"] = value;
				}
			}

			// Token: 0x17001019 RID: 4121
			// (set) Token: 0x06002842 RID: 10306 RVA: 0x0004C015 File Offset: 0x0004A215
			public virtual int VoiceMailPreviewPartnerMaxMessageDuration
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerMaxMessageDuration"] = value;
				}
			}

			// Token: 0x1700101A RID: 4122
			// (set) Token: 0x06002843 RID: 10307 RVA: 0x0004C02D File Offset: 0x0004A22D
			public virtual int VoiceMailPreviewPartnerMaxDeliveryDelay
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerMaxDeliveryDelay"] = value;
				}
			}

			// Token: 0x1700101B RID: 4123
			// (set) Token: 0x06002844 RID: 10308 RVA: 0x0004C045 File Offset: 0x0004A245
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700101C RID: 4124
			// (set) Token: 0x06002845 RID: 10309 RVA: 0x0004C058 File Offset: 0x0004A258
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700101D RID: 4125
			// (set) Token: 0x06002846 RID: 10310 RVA: 0x0004C070 File Offset: 0x0004A270
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700101E RID: 4126
			// (set) Token: 0x06002847 RID: 10311 RVA: 0x0004C088 File Offset: 0x0004A288
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700101F RID: 4127
			// (set) Token: 0x06002848 RID: 10312 RVA: 0x0004C0A0 File Offset: 0x0004A2A0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001020 RID: 4128
			// (set) Token: 0x06002849 RID: 10313 RVA: 0x0004C0B8 File Offset: 0x0004A2B8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001F1 RID: 497
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001021 RID: 4129
			// (set) Token: 0x0600284B RID: 10315 RVA: 0x0004C0D8 File Offset: 0x0004A2D8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17001022 RID: 4130
			// (set) Token: 0x0600284C RID: 10316 RVA: 0x0004C0F6 File Offset: 0x0004A2F6
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17001023 RID: 4131
			// (set) Token: 0x0600284D RID: 10317 RVA: 0x0004C114 File Offset: 0x0004A314
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17001024 RID: 4132
			// (set) Token: 0x0600284E RID: 10318 RVA: 0x0004C12C File Offset: 0x0004A32C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001025 RID: 4133
			// (set) Token: 0x0600284F RID: 10319 RVA: 0x0004C13F File Offset: 0x0004A33F
			public virtual int MaxGreetingDuration
			{
				set
				{
					base.PowerSharpParameters["MaxGreetingDuration"] = value;
				}
			}

			// Token: 0x17001026 RID: 4134
			// (set) Token: 0x06002850 RID: 10320 RVA: 0x0004C157 File Offset: 0x0004A357
			public virtual Unlimited<int> MaxLogonAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxLogonAttempts"] = value;
				}
			}

			// Token: 0x17001027 RID: 4135
			// (set) Token: 0x06002851 RID: 10321 RVA: 0x0004C16F File Offset: 0x0004A36F
			public virtual bool AllowCommonPatterns
			{
				set
				{
					base.PowerSharpParameters["AllowCommonPatterns"] = value;
				}
			}

			// Token: 0x17001028 RID: 4136
			// (set) Token: 0x06002852 RID: 10322 RVA: 0x0004C187 File Offset: 0x0004A387
			public virtual Unlimited<EnhancedTimeSpan> PINLifetime
			{
				set
				{
					base.PowerSharpParameters["PINLifetime"] = value;
				}
			}

			// Token: 0x17001029 RID: 4137
			// (set) Token: 0x06002853 RID: 10323 RVA: 0x0004C19F File Offset: 0x0004A39F
			public virtual int PINHistoryCount
			{
				set
				{
					base.PowerSharpParameters["PINHistoryCount"] = value;
				}
			}

			// Token: 0x1700102A RID: 4138
			// (set) Token: 0x06002854 RID: 10324 RVA: 0x0004C1B7 File Offset: 0x0004A3B7
			public virtual bool AllowSMSNotification
			{
				set
				{
					base.PowerSharpParameters["AllowSMSNotification"] = value;
				}
			}

			// Token: 0x1700102B RID: 4139
			// (set) Token: 0x06002855 RID: 10325 RVA: 0x0004C1CF File Offset: 0x0004A3CF
			public virtual DRMProtectionOptions ProtectUnauthenticatedVoiceMail
			{
				set
				{
					base.PowerSharpParameters["ProtectUnauthenticatedVoiceMail"] = value;
				}
			}

			// Token: 0x1700102C RID: 4140
			// (set) Token: 0x06002856 RID: 10326 RVA: 0x0004C1E7 File Offset: 0x0004A3E7
			public virtual DRMProtectionOptions ProtectAuthenticatedVoiceMail
			{
				set
				{
					base.PowerSharpParameters["ProtectAuthenticatedVoiceMail"] = value;
				}
			}

			// Token: 0x1700102D RID: 4141
			// (set) Token: 0x06002857 RID: 10327 RVA: 0x0004C1FF File Offset: 0x0004A3FF
			public virtual string ProtectedVoiceMailText
			{
				set
				{
					base.PowerSharpParameters["ProtectedVoiceMailText"] = value;
				}
			}

			// Token: 0x1700102E RID: 4142
			// (set) Token: 0x06002858 RID: 10328 RVA: 0x0004C212 File Offset: 0x0004A412
			public virtual bool RequireProtectedPlayOnPhone
			{
				set
				{
					base.PowerSharpParameters["RequireProtectedPlayOnPhone"] = value;
				}
			}

			// Token: 0x1700102F RID: 4143
			// (set) Token: 0x06002859 RID: 10329 RVA: 0x0004C22A File Offset: 0x0004A42A
			public virtual int MinPINLength
			{
				set
				{
					base.PowerSharpParameters["MinPINLength"] = value;
				}
			}

			// Token: 0x17001030 RID: 4144
			// (set) Token: 0x0600285A RID: 10330 RVA: 0x0004C242 File Offset: 0x0004A442
			public virtual string FaxMessageText
			{
				set
				{
					base.PowerSharpParameters["FaxMessageText"] = value;
				}
			}

			// Token: 0x17001031 RID: 4145
			// (set) Token: 0x0600285B RID: 10331 RVA: 0x0004C255 File Offset: 0x0004A455
			public virtual string UMEnabledText
			{
				set
				{
					base.PowerSharpParameters["UMEnabledText"] = value;
				}
			}

			// Token: 0x17001032 RID: 4146
			// (set) Token: 0x0600285C RID: 10332 RVA: 0x0004C268 File Offset: 0x0004A468
			public virtual string ResetPINText
			{
				set
				{
					base.PowerSharpParameters["ResetPINText"] = value;
				}
			}

			// Token: 0x17001033 RID: 4147
			// (set) Token: 0x0600285D RID: 10333 RVA: 0x0004C27B File Offset: 0x0004A47B
			public virtual MultiValuedProperty<string> SourceForestPolicyNames
			{
				set
				{
					base.PowerSharpParameters["SourceForestPolicyNames"] = value;
				}
			}

			// Token: 0x17001034 RID: 4148
			// (set) Token: 0x0600285E RID: 10334 RVA: 0x0004C28E File Offset: 0x0004A48E
			public virtual string VoiceMailText
			{
				set
				{
					base.PowerSharpParameters["VoiceMailText"] = value;
				}
			}

			// Token: 0x17001035 RID: 4149
			// (set) Token: 0x0600285F RID: 10335 RVA: 0x0004C2A1 File Offset: 0x0004A4A1
			public virtual string FaxServerURI
			{
				set
				{
					base.PowerSharpParameters["FaxServerURI"] = value;
				}
			}

			// Token: 0x17001036 RID: 4150
			// (set) Token: 0x06002860 RID: 10336 RVA: 0x0004C2B4 File Offset: 0x0004A4B4
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x17001037 RID: 4151
			// (set) Token: 0x06002861 RID: 10337 RVA: 0x0004C2C7 File Offset: 0x0004A4C7
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x17001038 RID: 4152
			// (set) Token: 0x06002862 RID: 10338 RVA: 0x0004C2DA File Offset: 0x0004A4DA
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x17001039 RID: 4153
			// (set) Token: 0x06002863 RID: 10339 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x1700103A RID: 4154
			// (set) Token: 0x06002864 RID: 10340 RVA: 0x0004C30A File Offset: 0x0004A50A
			public virtual Unlimited<int> LogonFailuresBeforePINReset
			{
				set
				{
					base.PowerSharpParameters["LogonFailuresBeforePINReset"] = value;
				}
			}

			// Token: 0x1700103B RID: 4155
			// (set) Token: 0x06002865 RID: 10341 RVA: 0x0004C322 File Offset: 0x0004A522
			public virtual bool AllowMissedCallNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowMissedCallNotifications"] = value;
				}
			}

			// Token: 0x1700103C RID: 4156
			// (set) Token: 0x06002866 RID: 10342 RVA: 0x0004C33A File Offset: 0x0004A53A
			public virtual bool AllowFax
			{
				set
				{
					base.PowerSharpParameters["AllowFax"] = value;
				}
			}

			// Token: 0x1700103D RID: 4157
			// (set) Token: 0x06002867 RID: 10343 RVA: 0x0004C352 File Offset: 0x0004A552
			public virtual bool AllowTUIAccessToCalendar
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToCalendar"] = value;
				}
			}

			// Token: 0x1700103E RID: 4158
			// (set) Token: 0x06002868 RID: 10344 RVA: 0x0004C36A File Offset: 0x0004A56A
			public virtual bool AllowTUIAccessToEmail
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToEmail"] = value;
				}
			}

			// Token: 0x1700103F RID: 4159
			// (set) Token: 0x06002869 RID: 10345 RVA: 0x0004C382 File Offset: 0x0004A582
			public virtual bool AllowSubscriberAccess
			{
				set
				{
					base.PowerSharpParameters["AllowSubscriberAccess"] = value;
				}
			}

			// Token: 0x17001040 RID: 4160
			// (set) Token: 0x0600286A RID: 10346 RVA: 0x0004C39A File Offset: 0x0004A59A
			public virtual bool AllowTUIAccessToDirectory
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToDirectory"] = value;
				}
			}

			// Token: 0x17001041 RID: 4161
			// (set) Token: 0x0600286B RID: 10347 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
			public virtual bool AllowTUIAccessToPersonalContacts
			{
				set
				{
					base.PowerSharpParameters["AllowTUIAccessToPersonalContacts"] = value;
				}
			}

			// Token: 0x17001042 RID: 4162
			// (set) Token: 0x0600286C RID: 10348 RVA: 0x0004C3CA File Offset: 0x0004A5CA
			public virtual bool AllowAutomaticSpeechRecognition
			{
				set
				{
					base.PowerSharpParameters["AllowAutomaticSpeechRecognition"] = value;
				}
			}

			// Token: 0x17001043 RID: 4163
			// (set) Token: 0x0600286D RID: 10349 RVA: 0x0004C3E2 File Offset: 0x0004A5E2
			public virtual bool AllowPlayOnPhone
			{
				set
				{
					base.PowerSharpParameters["AllowPlayOnPhone"] = value;
				}
			}

			// Token: 0x17001044 RID: 4164
			// (set) Token: 0x0600286E RID: 10350 RVA: 0x0004C3FA File Offset: 0x0004A5FA
			public virtual bool AllowVoiceMailPreview
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceMailPreview"] = value;
				}
			}

			// Token: 0x17001045 RID: 4165
			// (set) Token: 0x0600286F RID: 10351 RVA: 0x0004C412 File Offset: 0x0004A612
			public virtual bool AllowCallAnsweringRules
			{
				set
				{
					base.PowerSharpParameters["AllowCallAnsweringRules"] = value;
				}
			}

			// Token: 0x17001046 RID: 4166
			// (set) Token: 0x06002870 RID: 10352 RVA: 0x0004C42A File Offset: 0x0004A62A
			public virtual bool AllowMessageWaitingIndicator
			{
				set
				{
					base.PowerSharpParameters["AllowMessageWaitingIndicator"] = value;
				}
			}

			// Token: 0x17001047 RID: 4167
			// (set) Token: 0x06002871 RID: 10353 RVA: 0x0004C442 File Offset: 0x0004A642
			public virtual bool AllowPinlessVoiceMailAccess
			{
				set
				{
					base.PowerSharpParameters["AllowPinlessVoiceMailAccess"] = value;
				}
			}

			// Token: 0x17001048 RID: 4168
			// (set) Token: 0x06002872 RID: 10354 RVA: 0x0004C45A File Offset: 0x0004A65A
			public virtual bool AllowVoiceResponseToOtherMessageTypes
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceResponseToOtherMessageTypes"] = value;
				}
			}

			// Token: 0x17001049 RID: 4169
			// (set) Token: 0x06002873 RID: 10355 RVA: 0x0004C472 File Offset: 0x0004A672
			public virtual bool AllowVoiceMailAnalysis
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceMailAnalysis"] = value;
				}
			}

			// Token: 0x1700104A RID: 4170
			// (set) Token: 0x06002874 RID: 10356 RVA: 0x0004C48A File Offset: 0x0004A68A
			public virtual bool AllowVoiceNotification
			{
				set
				{
					base.PowerSharpParameters["AllowVoiceNotification"] = value;
				}
			}

			// Token: 0x1700104B RID: 4171
			// (set) Token: 0x06002875 RID: 10357 RVA: 0x0004C4A2 File Offset: 0x0004A6A2
			public virtual bool InformCallerOfVoiceMailAnalysis
			{
				set
				{
					base.PowerSharpParameters["InformCallerOfVoiceMailAnalysis"] = value;
				}
			}

			// Token: 0x1700104C RID: 4172
			// (set) Token: 0x06002876 RID: 10358 RVA: 0x0004C4BA File Offset: 0x0004A6BA
			public virtual SmtpAddress? VoiceMailPreviewPartnerAddress
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerAddress"] = value;
				}
			}

			// Token: 0x1700104D RID: 4173
			// (set) Token: 0x06002877 RID: 10359 RVA: 0x0004C4D2 File Offset: 0x0004A6D2
			public virtual string VoiceMailPreviewPartnerAssignedID
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerAssignedID"] = value;
				}
			}

			// Token: 0x1700104E RID: 4174
			// (set) Token: 0x06002878 RID: 10360 RVA: 0x0004C4E5 File Offset: 0x0004A6E5
			public virtual int VoiceMailPreviewPartnerMaxMessageDuration
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerMaxMessageDuration"] = value;
				}
			}

			// Token: 0x1700104F RID: 4175
			// (set) Token: 0x06002879 RID: 10361 RVA: 0x0004C4FD File Offset: 0x0004A6FD
			public virtual int VoiceMailPreviewPartnerMaxDeliveryDelay
			{
				set
				{
					base.PowerSharpParameters["VoiceMailPreviewPartnerMaxDeliveryDelay"] = value;
				}
			}

			// Token: 0x17001050 RID: 4176
			// (set) Token: 0x0600287A RID: 10362 RVA: 0x0004C515 File Offset: 0x0004A715
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001051 RID: 4177
			// (set) Token: 0x0600287B RID: 10363 RVA: 0x0004C528 File Offset: 0x0004A728
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001052 RID: 4178
			// (set) Token: 0x0600287C RID: 10364 RVA: 0x0004C540 File Offset: 0x0004A740
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001053 RID: 4179
			// (set) Token: 0x0600287D RID: 10365 RVA: 0x0004C558 File Offset: 0x0004A758
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001054 RID: 4180
			// (set) Token: 0x0600287E RID: 10366 RVA: 0x0004C570 File Offset: 0x0004A770
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001055 RID: 4181
			// (set) Token: 0x0600287F RID: 10367 RVA: 0x0004C588 File Offset: 0x0004A788
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
