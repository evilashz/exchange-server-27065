using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B88 RID: 2952
	public class SetUMDialPlanCommand : SyntheticCommandWithPipelineInputNoOutput<UMDialPlan>
	{
		// Token: 0x06008E98 RID: 36504 RVA: 0x000D0C31 File Offset: 0x000CEE31
		private SetUMDialPlanCommand() : base("Set-UMDialPlan")
		{
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x000D0C3E File Offset: 0x000CEE3E
		public SetUMDialPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x000D0C4D File Offset: 0x000CEE4D
		public virtual SetUMDialPlanCommand SetParameters(SetUMDialPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x000D0C57 File Offset: 0x000CEE57
		public virtual SetUMDialPlanCommand SetParameters(SetUMDialPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B89 RID: 2953
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006341 RID: 25409
			// (set) Token: 0x06008E9C RID: 36508 RVA: 0x000D0C61 File Offset: 0x000CEE61
			public virtual AddressListIdParameter ContactAddressList
			{
				set
				{
					base.PowerSharpParameters["ContactAddressList"] = value;
				}
			}

			// Token: 0x17006342 RID: 25410
			// (set) Token: 0x06008E9D RID: 36509 RVA: 0x000D0C74 File Offset: 0x000CEE74
			public virtual string ContactRecipientContainer
			{
				set
				{
					base.PowerSharpParameters["ContactRecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006343 RID: 25411
			// (set) Token: 0x06008E9E RID: 36510 RVA: 0x000D0C92 File Offset: 0x000CEE92
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006344 RID: 25412
			// (set) Token: 0x06008E9F RID: 36511 RVA: 0x000D0CB0 File Offset: 0x000CEEB0
			public virtual string CountryOrRegionCode
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegionCode"] = value;
				}
			}

			// Token: 0x17006345 RID: 25413
			// (set) Token: 0x06008EA0 RID: 36512 RVA: 0x000D0CC3 File Offset: 0x000CEEC3
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17006346 RID: 25414
			// (set) Token: 0x06008EA1 RID: 36513 RVA: 0x000D0CDB File Offset: 0x000CEEDB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006347 RID: 25415
			// (set) Token: 0x06008EA2 RID: 36514 RVA: 0x000D0CEE File Offset: 0x000CEEEE
			public virtual int LogonFailuresBeforeDisconnect
			{
				set
				{
					base.PowerSharpParameters["LogonFailuresBeforeDisconnect"] = value;
				}
			}

			// Token: 0x17006348 RID: 25416
			// (set) Token: 0x06008EA3 RID: 36515 RVA: 0x000D0D06 File Offset: 0x000CEF06
			public virtual MultiValuedProperty<string> AccessTelephoneNumbers
			{
				set
				{
					base.PowerSharpParameters["AccessTelephoneNumbers"] = value;
				}
			}

			// Token: 0x17006349 RID: 25417
			// (set) Token: 0x06008EA4 RID: 36516 RVA: 0x000D0D19 File Offset: 0x000CEF19
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x1700634A RID: 25418
			// (set) Token: 0x06008EA5 RID: 36517 RVA: 0x000D0D31 File Offset: 0x000CEF31
			public virtual int InputFailuresBeforeDisconnect
			{
				set
				{
					base.PowerSharpParameters["InputFailuresBeforeDisconnect"] = value;
				}
			}

			// Token: 0x1700634B RID: 25419
			// (set) Token: 0x06008EA6 RID: 36518 RVA: 0x000D0D49 File Offset: 0x000CEF49
			public virtual string OutsideLineAccessCode
			{
				set
				{
					base.PowerSharpParameters["OutsideLineAccessCode"] = value;
				}
			}

			// Token: 0x1700634C RID: 25420
			// (set) Token: 0x06008EA7 RID: 36519 RVA: 0x000D0D5C File Offset: 0x000CEF5C
			public virtual DialByNamePrimaryEnum DialByNamePrimary
			{
				set
				{
					base.PowerSharpParameters["DialByNamePrimary"] = value;
				}
			}

			// Token: 0x1700634D RID: 25421
			// (set) Token: 0x06008EA8 RID: 36520 RVA: 0x000D0D74 File Offset: 0x000CEF74
			public virtual DialByNameSecondaryEnum DialByNameSecondary
			{
				set
				{
					base.PowerSharpParameters["DialByNameSecondary"] = value;
				}
			}

			// Token: 0x1700634E RID: 25422
			// (set) Token: 0x06008EA9 RID: 36521 RVA: 0x000D0D8C File Offset: 0x000CEF8C
			public virtual AudioCodecEnum AudioCodec
			{
				set
				{
					base.PowerSharpParameters["AudioCodec"] = value;
				}
			}

			// Token: 0x1700634F RID: 25423
			// (set) Token: 0x06008EAA RID: 36522 RVA: 0x000D0DA4 File Offset: 0x000CEFA4
			public virtual UMLanguage DefaultLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultLanguage"] = value;
				}
			}

			// Token: 0x17006350 RID: 25424
			// (set) Token: 0x06008EAB RID: 36523 RVA: 0x000D0DB7 File Offset: 0x000CEFB7
			public virtual UMVoIPSecurityType VoIPSecurity
			{
				set
				{
					base.PowerSharpParameters["VoIPSecurity"] = value;
				}
			}

			// Token: 0x17006351 RID: 25425
			// (set) Token: 0x06008EAC RID: 36524 RVA: 0x000D0DCF File Offset: 0x000CEFCF
			public virtual int MaxCallDuration
			{
				set
				{
					base.PowerSharpParameters["MaxCallDuration"] = value;
				}
			}

			// Token: 0x17006352 RID: 25426
			// (set) Token: 0x06008EAD RID: 36525 RVA: 0x000D0DE7 File Offset: 0x000CEFE7
			public virtual int MaxRecordingDuration
			{
				set
				{
					base.PowerSharpParameters["MaxRecordingDuration"] = value;
				}
			}

			// Token: 0x17006353 RID: 25427
			// (set) Token: 0x06008EAE RID: 36526 RVA: 0x000D0DFF File Offset: 0x000CEFFF
			public virtual int RecordingIdleTimeout
			{
				set
				{
					base.PowerSharpParameters["RecordingIdleTimeout"] = value;
				}
			}

			// Token: 0x17006354 RID: 25428
			// (set) Token: 0x06008EAF RID: 36527 RVA: 0x000D0E17 File Offset: 0x000CF017
			public virtual MultiValuedProperty<string> PilotIdentifierList
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifierList"] = value;
				}
			}

			// Token: 0x17006355 RID: 25429
			// (set) Token: 0x06008EB0 RID: 36528 RVA: 0x000D0E2A File Offset: 0x000CF02A
			public virtual bool WelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["WelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x17006356 RID: 25430
			// (set) Token: 0x06008EB1 RID: 36529 RVA: 0x000D0E42 File Offset: 0x000CF042
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x17006357 RID: 25431
			// (set) Token: 0x06008EB2 RID: 36530 RVA: 0x000D0E5A File Offset: 0x000CF05A
			public virtual string WelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["WelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x17006358 RID: 25432
			// (set) Token: 0x06008EB3 RID: 36531 RVA: 0x000D0E6D File Offset: 0x000CF06D
			public virtual string InfoAnnouncementFilename
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementFilename"] = value;
				}
			}

			// Token: 0x17006359 RID: 25433
			// (set) Token: 0x06008EB4 RID: 36532 RVA: 0x000D0E80 File Offset: 0x000CF080
			public virtual string OperatorExtension
			{
				set
				{
					base.PowerSharpParameters["OperatorExtension"] = value;
				}
			}

			// Token: 0x1700635A RID: 25434
			// (set) Token: 0x06008EB5 RID: 36533 RVA: 0x000D0E93 File Offset: 0x000CF093
			public virtual string DefaultOutboundCallingLineId
			{
				set
				{
					base.PowerSharpParameters["DefaultOutboundCallingLineId"] = value;
				}
			}

			// Token: 0x1700635B RID: 25435
			// (set) Token: 0x06008EB6 RID: 36534 RVA: 0x000D0EA6 File Offset: 0x000CF0A6
			public virtual string Extension
			{
				set
				{
					base.PowerSharpParameters["Extension"] = value;
				}
			}

			// Token: 0x1700635C RID: 25436
			// (set) Token: 0x06008EB7 RID: 36535 RVA: 0x000D0EB9 File Offset: 0x000CF0B9
			public virtual DisambiguationFieldEnum MatchedNameSelectionMethod
			{
				set
				{
					base.PowerSharpParameters["MatchedNameSelectionMethod"] = value;
				}
			}

			// Token: 0x1700635D RID: 25437
			// (set) Token: 0x06008EB8 RID: 36536 RVA: 0x000D0ED1 File Offset: 0x000CF0D1
			public virtual InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementEnabled"] = value;
				}
			}

			// Token: 0x1700635E RID: 25438
			// (set) Token: 0x06008EB9 RID: 36537 RVA: 0x000D0EE9 File Offset: 0x000CF0E9
			public virtual string InternationalAccessCode
			{
				set
				{
					base.PowerSharpParameters["InternationalAccessCode"] = value;
				}
			}

			// Token: 0x1700635F RID: 25439
			// (set) Token: 0x06008EBA RID: 36538 RVA: 0x000D0EFC File Offset: 0x000CF0FC
			public virtual string NationalNumberPrefix
			{
				set
				{
					base.PowerSharpParameters["NationalNumberPrefix"] = value;
				}
			}

			// Token: 0x17006360 RID: 25440
			// (set) Token: 0x06008EBB RID: 36539 RVA: 0x000D0F0F File Offset: 0x000CF10F
			public virtual NumberFormat InCountryOrRegionNumberFormat
			{
				set
				{
					base.PowerSharpParameters["InCountryOrRegionNumberFormat"] = value;
				}
			}

			// Token: 0x17006361 RID: 25441
			// (set) Token: 0x06008EBC RID: 36540 RVA: 0x000D0F22 File Offset: 0x000CF122
			public virtual NumberFormat InternationalNumberFormat
			{
				set
				{
					base.PowerSharpParameters["InternationalNumberFormat"] = value;
				}
			}

			// Token: 0x17006362 RID: 25442
			// (set) Token: 0x06008EBD RID: 36541 RVA: 0x000D0F35 File Offset: 0x000CF135
			public virtual bool CallSomeoneEnabled
			{
				set
				{
					base.PowerSharpParameters["CallSomeoneEnabled"] = value;
				}
			}

			// Token: 0x17006363 RID: 25443
			// (set) Token: 0x06008EBE RID: 36542 RVA: 0x000D0F4D File Offset: 0x000CF14D
			public virtual CallSomeoneScopeEnum ContactScope
			{
				set
				{
					base.PowerSharpParameters["ContactScope"] = value;
				}
			}

			// Token: 0x17006364 RID: 25444
			// (set) Token: 0x06008EBF RID: 36543 RVA: 0x000D0F65 File Offset: 0x000CF165
			public virtual bool SendVoiceMsgEnabled
			{
				set
				{
					base.PowerSharpParameters["SendVoiceMsgEnabled"] = value;
				}
			}

			// Token: 0x17006365 RID: 25445
			// (set) Token: 0x06008EC0 RID: 36544 RVA: 0x000D0F7D File Offset: 0x000CF17D
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x17006366 RID: 25446
			// (set) Token: 0x06008EC1 RID: 36545 RVA: 0x000D0F95 File Offset: 0x000CF195
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x17006367 RID: 25447
			// (set) Token: 0x06008EC2 RID: 36546 RVA: 0x000D0FAD File Offset: 0x000CF1AD
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x17006368 RID: 25448
			// (set) Token: 0x06008EC3 RID: 36547 RVA: 0x000D0FC0 File Offset: 0x000CF1C0
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x17006369 RID: 25449
			// (set) Token: 0x06008EC4 RID: 36548 RVA: 0x000D0FD3 File Offset: 0x000CF1D3
			public virtual MultiValuedProperty<DialGroupEntry> ConfiguredInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["ConfiguredInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x1700636A RID: 25450
			// (set) Token: 0x06008EC5 RID: 36549 RVA: 0x000D0FE6 File Offset: 0x000CF1E6
			public virtual string LegacyPromptPublishingPoint
			{
				set
				{
					base.PowerSharpParameters["LegacyPromptPublishingPoint"] = value;
				}
			}

			// Token: 0x1700636B RID: 25451
			// (set) Token: 0x06008EC6 RID: 36550 RVA: 0x000D0FF9 File Offset: 0x000CF1F9
			public virtual MultiValuedProperty<DialGroupEntry> ConfiguredInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["ConfiguredInternationalGroups"] = value;
				}
			}

			// Token: 0x1700636C RID: 25452
			// (set) Token: 0x06008EC7 RID: 36551 RVA: 0x000D100C File Offset: 0x000CF20C
			public virtual bool TUIPromptEditingEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIPromptEditingEnabled"] = value;
				}
			}

			// Token: 0x1700636D RID: 25453
			// (set) Token: 0x06008EC8 RID: 36552 RVA: 0x000D1024 File Offset: 0x000CF224
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x1700636E RID: 25454
			// (set) Token: 0x06008EC9 RID: 36553 RVA: 0x000D103C File Offset: 0x000CF23C
			public virtual MultiValuedProperty<string> EquivalentDialPlanPhoneContexts
			{
				set
				{
					base.PowerSharpParameters["EquivalentDialPlanPhoneContexts"] = value;
				}
			}

			// Token: 0x1700636F RID: 25455
			// (set) Token: 0x06008ECA RID: 36554 RVA: 0x000D104F File Offset: 0x000CF24F
			public virtual MultiValuedProperty<UMNumberingPlanFormat> NumberingPlanFormats
			{
				set
				{
					base.PowerSharpParameters["NumberingPlanFormats"] = value;
				}
			}

			// Token: 0x17006370 RID: 25456
			// (set) Token: 0x06008ECB RID: 36555 RVA: 0x000D1062 File Offset: 0x000CF262
			public virtual bool AllowHeuristicADCallingLineIdResolution
			{
				set
				{
					base.PowerSharpParameters["AllowHeuristicADCallingLineIdResolution"] = value;
				}
			}

			// Token: 0x17006371 RID: 25457
			// (set) Token: 0x06008ECC RID: 36556 RVA: 0x000D107A File Offset: 0x000CF27A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006372 RID: 25458
			// (set) Token: 0x06008ECD RID: 36557 RVA: 0x000D108D File Offset: 0x000CF28D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006373 RID: 25459
			// (set) Token: 0x06008ECE RID: 36558 RVA: 0x000D10A5 File Offset: 0x000CF2A5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006374 RID: 25460
			// (set) Token: 0x06008ECF RID: 36559 RVA: 0x000D10BD File Offset: 0x000CF2BD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006375 RID: 25461
			// (set) Token: 0x06008ED0 RID: 36560 RVA: 0x000D10D5 File Offset: 0x000CF2D5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006376 RID: 25462
			// (set) Token: 0x06008ED1 RID: 36561 RVA: 0x000D10ED File Offset: 0x000CF2ED
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B8A RID: 2954
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006377 RID: 25463
			// (set) Token: 0x06008ED3 RID: 36563 RVA: 0x000D110D File Offset: 0x000CF30D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17006378 RID: 25464
			// (set) Token: 0x06008ED4 RID: 36564 RVA: 0x000D112B File Offset: 0x000CF32B
			public virtual AddressListIdParameter ContactAddressList
			{
				set
				{
					base.PowerSharpParameters["ContactAddressList"] = value;
				}
			}

			// Token: 0x17006379 RID: 25465
			// (set) Token: 0x06008ED5 RID: 36565 RVA: 0x000D113E File Offset: 0x000CF33E
			public virtual string ContactRecipientContainer
			{
				set
				{
					base.PowerSharpParameters["ContactRecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700637A RID: 25466
			// (set) Token: 0x06008ED6 RID: 36566 RVA: 0x000D115C File Offset: 0x000CF35C
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700637B RID: 25467
			// (set) Token: 0x06008ED7 RID: 36567 RVA: 0x000D117A File Offset: 0x000CF37A
			public virtual string CountryOrRegionCode
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegionCode"] = value;
				}
			}

			// Token: 0x1700637C RID: 25468
			// (set) Token: 0x06008ED8 RID: 36568 RVA: 0x000D118D File Offset: 0x000CF38D
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x1700637D RID: 25469
			// (set) Token: 0x06008ED9 RID: 36569 RVA: 0x000D11A5 File Offset: 0x000CF3A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700637E RID: 25470
			// (set) Token: 0x06008EDA RID: 36570 RVA: 0x000D11B8 File Offset: 0x000CF3B8
			public virtual int LogonFailuresBeforeDisconnect
			{
				set
				{
					base.PowerSharpParameters["LogonFailuresBeforeDisconnect"] = value;
				}
			}

			// Token: 0x1700637F RID: 25471
			// (set) Token: 0x06008EDB RID: 36571 RVA: 0x000D11D0 File Offset: 0x000CF3D0
			public virtual MultiValuedProperty<string> AccessTelephoneNumbers
			{
				set
				{
					base.PowerSharpParameters["AccessTelephoneNumbers"] = value;
				}
			}

			// Token: 0x17006380 RID: 25472
			// (set) Token: 0x06008EDC RID: 36572 RVA: 0x000D11E3 File Offset: 0x000CF3E3
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x17006381 RID: 25473
			// (set) Token: 0x06008EDD RID: 36573 RVA: 0x000D11FB File Offset: 0x000CF3FB
			public virtual int InputFailuresBeforeDisconnect
			{
				set
				{
					base.PowerSharpParameters["InputFailuresBeforeDisconnect"] = value;
				}
			}

			// Token: 0x17006382 RID: 25474
			// (set) Token: 0x06008EDE RID: 36574 RVA: 0x000D1213 File Offset: 0x000CF413
			public virtual string OutsideLineAccessCode
			{
				set
				{
					base.PowerSharpParameters["OutsideLineAccessCode"] = value;
				}
			}

			// Token: 0x17006383 RID: 25475
			// (set) Token: 0x06008EDF RID: 36575 RVA: 0x000D1226 File Offset: 0x000CF426
			public virtual DialByNamePrimaryEnum DialByNamePrimary
			{
				set
				{
					base.PowerSharpParameters["DialByNamePrimary"] = value;
				}
			}

			// Token: 0x17006384 RID: 25476
			// (set) Token: 0x06008EE0 RID: 36576 RVA: 0x000D123E File Offset: 0x000CF43E
			public virtual DialByNameSecondaryEnum DialByNameSecondary
			{
				set
				{
					base.PowerSharpParameters["DialByNameSecondary"] = value;
				}
			}

			// Token: 0x17006385 RID: 25477
			// (set) Token: 0x06008EE1 RID: 36577 RVA: 0x000D1256 File Offset: 0x000CF456
			public virtual AudioCodecEnum AudioCodec
			{
				set
				{
					base.PowerSharpParameters["AudioCodec"] = value;
				}
			}

			// Token: 0x17006386 RID: 25478
			// (set) Token: 0x06008EE2 RID: 36578 RVA: 0x000D126E File Offset: 0x000CF46E
			public virtual UMLanguage DefaultLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultLanguage"] = value;
				}
			}

			// Token: 0x17006387 RID: 25479
			// (set) Token: 0x06008EE3 RID: 36579 RVA: 0x000D1281 File Offset: 0x000CF481
			public virtual UMVoIPSecurityType VoIPSecurity
			{
				set
				{
					base.PowerSharpParameters["VoIPSecurity"] = value;
				}
			}

			// Token: 0x17006388 RID: 25480
			// (set) Token: 0x06008EE4 RID: 36580 RVA: 0x000D1299 File Offset: 0x000CF499
			public virtual int MaxCallDuration
			{
				set
				{
					base.PowerSharpParameters["MaxCallDuration"] = value;
				}
			}

			// Token: 0x17006389 RID: 25481
			// (set) Token: 0x06008EE5 RID: 36581 RVA: 0x000D12B1 File Offset: 0x000CF4B1
			public virtual int MaxRecordingDuration
			{
				set
				{
					base.PowerSharpParameters["MaxRecordingDuration"] = value;
				}
			}

			// Token: 0x1700638A RID: 25482
			// (set) Token: 0x06008EE6 RID: 36582 RVA: 0x000D12C9 File Offset: 0x000CF4C9
			public virtual int RecordingIdleTimeout
			{
				set
				{
					base.PowerSharpParameters["RecordingIdleTimeout"] = value;
				}
			}

			// Token: 0x1700638B RID: 25483
			// (set) Token: 0x06008EE7 RID: 36583 RVA: 0x000D12E1 File Offset: 0x000CF4E1
			public virtual MultiValuedProperty<string> PilotIdentifierList
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifierList"] = value;
				}
			}

			// Token: 0x1700638C RID: 25484
			// (set) Token: 0x06008EE8 RID: 36584 RVA: 0x000D12F4 File Offset: 0x000CF4F4
			public virtual bool WelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["WelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x1700638D RID: 25485
			// (set) Token: 0x06008EE9 RID: 36585 RVA: 0x000D130C File Offset: 0x000CF50C
			public virtual bool AutomaticSpeechRecognitionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutomaticSpeechRecognitionEnabled"] = value;
				}
			}

			// Token: 0x1700638E RID: 25486
			// (set) Token: 0x06008EEA RID: 36586 RVA: 0x000D1324 File Offset: 0x000CF524
			public virtual string WelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["WelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x1700638F RID: 25487
			// (set) Token: 0x06008EEB RID: 36587 RVA: 0x000D1337 File Offset: 0x000CF537
			public virtual string InfoAnnouncementFilename
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementFilename"] = value;
				}
			}

			// Token: 0x17006390 RID: 25488
			// (set) Token: 0x06008EEC RID: 36588 RVA: 0x000D134A File Offset: 0x000CF54A
			public virtual string OperatorExtension
			{
				set
				{
					base.PowerSharpParameters["OperatorExtension"] = value;
				}
			}

			// Token: 0x17006391 RID: 25489
			// (set) Token: 0x06008EED RID: 36589 RVA: 0x000D135D File Offset: 0x000CF55D
			public virtual string DefaultOutboundCallingLineId
			{
				set
				{
					base.PowerSharpParameters["DefaultOutboundCallingLineId"] = value;
				}
			}

			// Token: 0x17006392 RID: 25490
			// (set) Token: 0x06008EEE RID: 36590 RVA: 0x000D1370 File Offset: 0x000CF570
			public virtual string Extension
			{
				set
				{
					base.PowerSharpParameters["Extension"] = value;
				}
			}

			// Token: 0x17006393 RID: 25491
			// (set) Token: 0x06008EEF RID: 36591 RVA: 0x000D1383 File Offset: 0x000CF583
			public virtual DisambiguationFieldEnum MatchedNameSelectionMethod
			{
				set
				{
					base.PowerSharpParameters["MatchedNameSelectionMethod"] = value;
				}
			}

			// Token: 0x17006394 RID: 25492
			// (set) Token: 0x06008EF0 RID: 36592 RVA: 0x000D139B File Offset: 0x000CF59B
			public virtual InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementEnabled"] = value;
				}
			}

			// Token: 0x17006395 RID: 25493
			// (set) Token: 0x06008EF1 RID: 36593 RVA: 0x000D13B3 File Offset: 0x000CF5B3
			public virtual string InternationalAccessCode
			{
				set
				{
					base.PowerSharpParameters["InternationalAccessCode"] = value;
				}
			}

			// Token: 0x17006396 RID: 25494
			// (set) Token: 0x06008EF2 RID: 36594 RVA: 0x000D13C6 File Offset: 0x000CF5C6
			public virtual string NationalNumberPrefix
			{
				set
				{
					base.PowerSharpParameters["NationalNumberPrefix"] = value;
				}
			}

			// Token: 0x17006397 RID: 25495
			// (set) Token: 0x06008EF3 RID: 36595 RVA: 0x000D13D9 File Offset: 0x000CF5D9
			public virtual NumberFormat InCountryOrRegionNumberFormat
			{
				set
				{
					base.PowerSharpParameters["InCountryOrRegionNumberFormat"] = value;
				}
			}

			// Token: 0x17006398 RID: 25496
			// (set) Token: 0x06008EF4 RID: 36596 RVA: 0x000D13EC File Offset: 0x000CF5EC
			public virtual NumberFormat InternationalNumberFormat
			{
				set
				{
					base.PowerSharpParameters["InternationalNumberFormat"] = value;
				}
			}

			// Token: 0x17006399 RID: 25497
			// (set) Token: 0x06008EF5 RID: 36597 RVA: 0x000D13FF File Offset: 0x000CF5FF
			public virtual bool CallSomeoneEnabled
			{
				set
				{
					base.PowerSharpParameters["CallSomeoneEnabled"] = value;
				}
			}

			// Token: 0x1700639A RID: 25498
			// (set) Token: 0x06008EF6 RID: 36598 RVA: 0x000D1417 File Offset: 0x000CF617
			public virtual CallSomeoneScopeEnum ContactScope
			{
				set
				{
					base.PowerSharpParameters["ContactScope"] = value;
				}
			}

			// Token: 0x1700639B RID: 25499
			// (set) Token: 0x06008EF7 RID: 36599 RVA: 0x000D142F File Offset: 0x000CF62F
			public virtual bool SendVoiceMsgEnabled
			{
				set
				{
					base.PowerSharpParameters["SendVoiceMsgEnabled"] = value;
				}
			}

			// Token: 0x1700639C RID: 25500
			// (set) Token: 0x06008EF8 RID: 36600 RVA: 0x000D1447 File Offset: 0x000CF647
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x1700639D RID: 25501
			// (set) Token: 0x06008EF9 RID: 36601 RVA: 0x000D145F File Offset: 0x000CF65F
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x1700639E RID: 25502
			// (set) Token: 0x06008EFA RID: 36602 RVA: 0x000D1477 File Offset: 0x000CF677
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x1700639F RID: 25503
			// (set) Token: 0x06008EFB RID: 36603 RVA: 0x000D148A File Offset: 0x000CF68A
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x170063A0 RID: 25504
			// (set) Token: 0x06008EFC RID: 36604 RVA: 0x000D149D File Offset: 0x000CF69D
			public virtual MultiValuedProperty<DialGroupEntry> ConfiguredInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["ConfiguredInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x170063A1 RID: 25505
			// (set) Token: 0x06008EFD RID: 36605 RVA: 0x000D14B0 File Offset: 0x000CF6B0
			public virtual string LegacyPromptPublishingPoint
			{
				set
				{
					base.PowerSharpParameters["LegacyPromptPublishingPoint"] = value;
				}
			}

			// Token: 0x170063A2 RID: 25506
			// (set) Token: 0x06008EFE RID: 36606 RVA: 0x000D14C3 File Offset: 0x000CF6C3
			public virtual MultiValuedProperty<DialGroupEntry> ConfiguredInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["ConfiguredInternationalGroups"] = value;
				}
			}

			// Token: 0x170063A3 RID: 25507
			// (set) Token: 0x06008EFF RID: 36607 RVA: 0x000D14D6 File Offset: 0x000CF6D6
			public virtual bool TUIPromptEditingEnabled
			{
				set
				{
					base.PowerSharpParameters["TUIPromptEditingEnabled"] = value;
				}
			}

			// Token: 0x170063A4 RID: 25508
			// (set) Token: 0x06008F00 RID: 36608 RVA: 0x000D14EE File Offset: 0x000CF6EE
			public virtual bool CallAnsweringRulesEnabled
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRulesEnabled"] = value;
				}
			}

			// Token: 0x170063A5 RID: 25509
			// (set) Token: 0x06008F01 RID: 36609 RVA: 0x000D1506 File Offset: 0x000CF706
			public virtual MultiValuedProperty<string> EquivalentDialPlanPhoneContexts
			{
				set
				{
					base.PowerSharpParameters["EquivalentDialPlanPhoneContexts"] = value;
				}
			}

			// Token: 0x170063A6 RID: 25510
			// (set) Token: 0x06008F02 RID: 36610 RVA: 0x000D1519 File Offset: 0x000CF719
			public virtual MultiValuedProperty<UMNumberingPlanFormat> NumberingPlanFormats
			{
				set
				{
					base.PowerSharpParameters["NumberingPlanFormats"] = value;
				}
			}

			// Token: 0x170063A7 RID: 25511
			// (set) Token: 0x06008F03 RID: 36611 RVA: 0x000D152C File Offset: 0x000CF72C
			public virtual bool AllowHeuristicADCallingLineIdResolution
			{
				set
				{
					base.PowerSharpParameters["AllowHeuristicADCallingLineIdResolution"] = value;
				}
			}

			// Token: 0x170063A8 RID: 25512
			// (set) Token: 0x06008F04 RID: 36612 RVA: 0x000D1544 File Offset: 0x000CF744
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170063A9 RID: 25513
			// (set) Token: 0x06008F05 RID: 36613 RVA: 0x000D1557 File Offset: 0x000CF757
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063AA RID: 25514
			// (set) Token: 0x06008F06 RID: 36614 RVA: 0x000D156F File Offset: 0x000CF76F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063AB RID: 25515
			// (set) Token: 0x06008F07 RID: 36615 RVA: 0x000D1587 File Offset: 0x000CF787
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063AC RID: 25516
			// (set) Token: 0x06008F08 RID: 36616 RVA: 0x000D159F File Offset: 0x000CF79F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170063AD RID: 25517
			// (set) Token: 0x06008F09 RID: 36617 RVA: 0x000D15B7 File Offset: 0x000CF7B7
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
