using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007DE RID: 2014
	public class SetRemoteDomainCommand : SyntheticCommandWithPipelineInputNoOutput<DomainContentConfig>
	{
		// Token: 0x06006454 RID: 25684 RVA: 0x000998DB File Offset: 0x00097ADB
		private SetRemoteDomainCommand() : base("Set-RemoteDomain")
		{
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x000998E8 File Offset: 0x00097AE8
		public SetRemoteDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x000998F7 File Offset: 0x00097AF7
		public virtual SetRemoteDomainCommand SetParameters(SetRemoteDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x00099901 File Offset: 0x00097B01
		public virtual SetRemoteDomainCommand SetParameters(SetRemoteDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007DF RID: 2015
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004051 RID: 16465
			// (set) Token: 0x06006458 RID: 25688 RVA: 0x0009990B File Offset: 0x00097B0B
			public virtual bool TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x17004052 RID: 16466
			// (set) Token: 0x06006459 RID: 25689 RVA: 0x00099923 File Offset: 0x00097B23
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004053 RID: 16467
			// (set) Token: 0x0600645A RID: 25690 RVA: 0x00099936 File Offset: 0x00097B36
			public virtual bool IsInternal
			{
				set
				{
					base.PowerSharpParameters["IsInternal"] = value;
				}
			}

			// Token: 0x17004054 RID: 16468
			// (set) Token: 0x0600645B RID: 25691 RVA: 0x0009994E File Offset: 0x00097B4E
			public virtual ByteEncoderTypeFor7BitCharsetsEnum ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x17004055 RID: 16469
			// (set) Token: 0x0600645C RID: 25692 RVA: 0x00099966 File Offset: 0x00097B66
			public virtual string CharacterSet
			{
				set
				{
					base.PowerSharpParameters["CharacterSet"] = value;
				}
			}

			// Token: 0x17004056 RID: 16470
			// (set) Token: 0x0600645D RID: 25693 RVA: 0x00099979 File Offset: 0x00097B79
			public virtual string NonMimeCharacterSet
			{
				set
				{
					base.PowerSharpParameters["NonMimeCharacterSet"] = value;
				}
			}

			// Token: 0x17004057 RID: 16471
			// (set) Token: 0x0600645E RID: 25694 RVA: 0x0009998C File Offset: 0x00097B8C
			public virtual AllowedOOFType AllowedOOFType
			{
				set
				{
					base.PowerSharpParameters["AllowedOOFType"] = value;
				}
			}

			// Token: 0x17004058 RID: 16472
			// (set) Token: 0x0600645F RID: 25695 RVA: 0x000999A4 File Offset: 0x00097BA4
			public virtual bool AutoReplyEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoReplyEnabled"] = value;
				}
			}

			// Token: 0x17004059 RID: 16473
			// (set) Token: 0x06006460 RID: 25696 RVA: 0x000999BC File Offset: 0x00097BBC
			public virtual bool AutoForwardEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoForwardEnabled"] = value;
				}
			}

			// Token: 0x1700405A RID: 16474
			// (set) Token: 0x06006461 RID: 25697 RVA: 0x000999D4 File Offset: 0x00097BD4
			public virtual bool DeliveryReportEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryReportEnabled"] = value;
				}
			}

			// Token: 0x1700405B RID: 16475
			// (set) Token: 0x06006462 RID: 25698 RVA: 0x000999EC File Offset: 0x00097BEC
			public virtual bool NDREnabled
			{
				set
				{
					base.PowerSharpParameters["NDREnabled"] = value;
				}
			}

			// Token: 0x1700405C RID: 16476
			// (set) Token: 0x06006463 RID: 25699 RVA: 0x00099A04 File Offset: 0x00097C04
			public virtual bool MeetingForwardNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MeetingForwardNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700405D RID: 16477
			// (set) Token: 0x06006464 RID: 25700 RVA: 0x00099A1C File Offset: 0x00097C1C
			public virtual ContentType ContentType
			{
				set
				{
					base.PowerSharpParameters["ContentType"] = value;
				}
			}

			// Token: 0x1700405E RID: 16478
			// (set) Token: 0x06006465 RID: 25701 RVA: 0x00099A34 File Offset: 0x00097C34
			public virtual bool DisplaySenderName
			{
				set
				{
					base.PowerSharpParameters["DisplaySenderName"] = value;
				}
			}

			// Token: 0x1700405F RID: 16479
			// (set) Token: 0x06006466 RID: 25702 RVA: 0x00099A4C File Offset: 0x00097C4C
			public virtual PreferredInternetCodePageForShiftJisEnum PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x17004060 RID: 16480
			// (set) Token: 0x06006467 RID: 25703 RVA: 0x00099A64 File Offset: 0x00097C64
			public virtual int? RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x17004061 RID: 16481
			// (set) Token: 0x06006468 RID: 25704 RVA: 0x00099A7C File Offset: 0x00097C7C
			public virtual bool? TNEFEnabled
			{
				set
				{
					base.PowerSharpParameters["TNEFEnabled"] = value;
				}
			}

			// Token: 0x17004062 RID: 16482
			// (set) Token: 0x06006469 RID: 25705 RVA: 0x00099A94 File Offset: 0x00097C94
			public virtual Unlimited<int> LineWrapSize
			{
				set
				{
					base.PowerSharpParameters["LineWrapSize"] = value;
				}
			}

			// Token: 0x17004063 RID: 16483
			// (set) Token: 0x0600646A RID: 25706 RVA: 0x00099AAC File Offset: 0x00097CAC
			public virtual bool TrustedMailOutboundEnabled
			{
				set
				{
					base.PowerSharpParameters["TrustedMailOutboundEnabled"] = value;
				}
			}

			// Token: 0x17004064 RID: 16484
			// (set) Token: 0x0600646B RID: 25707 RVA: 0x00099AC4 File Offset: 0x00097CC4
			public virtual bool TrustedMailInboundEnabled
			{
				set
				{
					base.PowerSharpParameters["TrustedMailInboundEnabled"] = value;
				}
			}

			// Token: 0x17004065 RID: 16485
			// (set) Token: 0x0600646C RID: 25708 RVA: 0x00099ADC File Offset: 0x00097CDC
			public virtual bool UseSimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["UseSimpleDisplayName"] = value;
				}
			}

			// Token: 0x17004066 RID: 16486
			// (set) Token: 0x0600646D RID: 25709 RVA: 0x00099AF4 File Offset: 0x00097CF4
			public virtual bool NDRDiagnosticInfoEnabled
			{
				set
				{
					base.PowerSharpParameters["NDRDiagnosticInfoEnabled"] = value;
				}
			}

			// Token: 0x17004067 RID: 16487
			// (set) Token: 0x0600646E RID: 25710 RVA: 0x00099B0C File Offset: 0x00097D0C
			public virtual int MessageCountThreshold
			{
				set
				{
					base.PowerSharpParameters["MessageCountThreshold"] = value;
				}
			}

			// Token: 0x17004068 RID: 16488
			// (set) Token: 0x0600646F RID: 25711 RVA: 0x00099B24 File Offset: 0x00097D24
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004069 RID: 16489
			// (set) Token: 0x06006470 RID: 25712 RVA: 0x00099B37 File Offset: 0x00097D37
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700406A RID: 16490
			// (set) Token: 0x06006471 RID: 25713 RVA: 0x00099B4F File Offset: 0x00097D4F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700406B RID: 16491
			// (set) Token: 0x06006472 RID: 25714 RVA: 0x00099B67 File Offset: 0x00097D67
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700406C RID: 16492
			// (set) Token: 0x06006473 RID: 25715 RVA: 0x00099B7F File Offset: 0x00097D7F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700406D RID: 16493
			// (set) Token: 0x06006474 RID: 25716 RVA: 0x00099B97 File Offset: 0x00097D97
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007E0 RID: 2016
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700406E RID: 16494
			// (set) Token: 0x06006476 RID: 25718 RVA: 0x00099BB7 File Offset: 0x00097DB7
			public virtual RemoteDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700406F RID: 16495
			// (set) Token: 0x06006477 RID: 25719 RVA: 0x00099BCA File Offset: 0x00097DCA
			public virtual bool TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x17004070 RID: 16496
			// (set) Token: 0x06006478 RID: 25720 RVA: 0x00099BE2 File Offset: 0x00097DE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004071 RID: 16497
			// (set) Token: 0x06006479 RID: 25721 RVA: 0x00099BF5 File Offset: 0x00097DF5
			public virtual bool IsInternal
			{
				set
				{
					base.PowerSharpParameters["IsInternal"] = value;
				}
			}

			// Token: 0x17004072 RID: 16498
			// (set) Token: 0x0600647A RID: 25722 RVA: 0x00099C0D File Offset: 0x00097E0D
			public virtual ByteEncoderTypeFor7BitCharsetsEnum ByteEncoderTypeFor7BitCharsets
			{
				set
				{
					base.PowerSharpParameters["ByteEncoderTypeFor7BitCharsets"] = value;
				}
			}

			// Token: 0x17004073 RID: 16499
			// (set) Token: 0x0600647B RID: 25723 RVA: 0x00099C25 File Offset: 0x00097E25
			public virtual string CharacterSet
			{
				set
				{
					base.PowerSharpParameters["CharacterSet"] = value;
				}
			}

			// Token: 0x17004074 RID: 16500
			// (set) Token: 0x0600647C RID: 25724 RVA: 0x00099C38 File Offset: 0x00097E38
			public virtual string NonMimeCharacterSet
			{
				set
				{
					base.PowerSharpParameters["NonMimeCharacterSet"] = value;
				}
			}

			// Token: 0x17004075 RID: 16501
			// (set) Token: 0x0600647D RID: 25725 RVA: 0x00099C4B File Offset: 0x00097E4B
			public virtual AllowedOOFType AllowedOOFType
			{
				set
				{
					base.PowerSharpParameters["AllowedOOFType"] = value;
				}
			}

			// Token: 0x17004076 RID: 16502
			// (set) Token: 0x0600647E RID: 25726 RVA: 0x00099C63 File Offset: 0x00097E63
			public virtual bool AutoReplyEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoReplyEnabled"] = value;
				}
			}

			// Token: 0x17004077 RID: 16503
			// (set) Token: 0x0600647F RID: 25727 RVA: 0x00099C7B File Offset: 0x00097E7B
			public virtual bool AutoForwardEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoForwardEnabled"] = value;
				}
			}

			// Token: 0x17004078 RID: 16504
			// (set) Token: 0x06006480 RID: 25728 RVA: 0x00099C93 File Offset: 0x00097E93
			public virtual bool DeliveryReportEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryReportEnabled"] = value;
				}
			}

			// Token: 0x17004079 RID: 16505
			// (set) Token: 0x06006481 RID: 25729 RVA: 0x00099CAB File Offset: 0x00097EAB
			public virtual bool NDREnabled
			{
				set
				{
					base.PowerSharpParameters["NDREnabled"] = value;
				}
			}

			// Token: 0x1700407A RID: 16506
			// (set) Token: 0x06006482 RID: 25730 RVA: 0x00099CC3 File Offset: 0x00097EC3
			public virtual bool MeetingForwardNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["MeetingForwardNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700407B RID: 16507
			// (set) Token: 0x06006483 RID: 25731 RVA: 0x00099CDB File Offset: 0x00097EDB
			public virtual ContentType ContentType
			{
				set
				{
					base.PowerSharpParameters["ContentType"] = value;
				}
			}

			// Token: 0x1700407C RID: 16508
			// (set) Token: 0x06006484 RID: 25732 RVA: 0x00099CF3 File Offset: 0x00097EF3
			public virtual bool DisplaySenderName
			{
				set
				{
					base.PowerSharpParameters["DisplaySenderName"] = value;
				}
			}

			// Token: 0x1700407D RID: 16509
			// (set) Token: 0x06006485 RID: 25733 RVA: 0x00099D0B File Offset: 0x00097F0B
			public virtual PreferredInternetCodePageForShiftJisEnum PreferredInternetCodePageForShiftJis
			{
				set
				{
					base.PowerSharpParameters["PreferredInternetCodePageForShiftJis"] = value;
				}
			}

			// Token: 0x1700407E RID: 16510
			// (set) Token: 0x06006486 RID: 25734 RVA: 0x00099D23 File Offset: 0x00097F23
			public virtual int? RequiredCharsetCoverage
			{
				set
				{
					base.PowerSharpParameters["RequiredCharsetCoverage"] = value;
				}
			}

			// Token: 0x1700407F RID: 16511
			// (set) Token: 0x06006487 RID: 25735 RVA: 0x00099D3B File Offset: 0x00097F3B
			public virtual bool? TNEFEnabled
			{
				set
				{
					base.PowerSharpParameters["TNEFEnabled"] = value;
				}
			}

			// Token: 0x17004080 RID: 16512
			// (set) Token: 0x06006488 RID: 25736 RVA: 0x00099D53 File Offset: 0x00097F53
			public virtual Unlimited<int> LineWrapSize
			{
				set
				{
					base.PowerSharpParameters["LineWrapSize"] = value;
				}
			}

			// Token: 0x17004081 RID: 16513
			// (set) Token: 0x06006489 RID: 25737 RVA: 0x00099D6B File Offset: 0x00097F6B
			public virtual bool TrustedMailOutboundEnabled
			{
				set
				{
					base.PowerSharpParameters["TrustedMailOutboundEnabled"] = value;
				}
			}

			// Token: 0x17004082 RID: 16514
			// (set) Token: 0x0600648A RID: 25738 RVA: 0x00099D83 File Offset: 0x00097F83
			public virtual bool TrustedMailInboundEnabled
			{
				set
				{
					base.PowerSharpParameters["TrustedMailInboundEnabled"] = value;
				}
			}

			// Token: 0x17004083 RID: 16515
			// (set) Token: 0x0600648B RID: 25739 RVA: 0x00099D9B File Offset: 0x00097F9B
			public virtual bool UseSimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["UseSimpleDisplayName"] = value;
				}
			}

			// Token: 0x17004084 RID: 16516
			// (set) Token: 0x0600648C RID: 25740 RVA: 0x00099DB3 File Offset: 0x00097FB3
			public virtual bool NDRDiagnosticInfoEnabled
			{
				set
				{
					base.PowerSharpParameters["NDRDiagnosticInfoEnabled"] = value;
				}
			}

			// Token: 0x17004085 RID: 16517
			// (set) Token: 0x0600648D RID: 25741 RVA: 0x00099DCB File Offset: 0x00097FCB
			public virtual int MessageCountThreshold
			{
				set
				{
					base.PowerSharpParameters["MessageCountThreshold"] = value;
				}
			}

			// Token: 0x17004086 RID: 16518
			// (set) Token: 0x0600648E RID: 25742 RVA: 0x00099DE3 File Offset: 0x00097FE3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004087 RID: 16519
			// (set) Token: 0x0600648F RID: 25743 RVA: 0x00099DF6 File Offset: 0x00097FF6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004088 RID: 16520
			// (set) Token: 0x06006490 RID: 25744 RVA: 0x00099E0E File Offset: 0x0009800E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004089 RID: 16521
			// (set) Token: 0x06006491 RID: 25745 RVA: 0x00099E26 File Offset: 0x00098026
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700408A RID: 16522
			// (set) Token: 0x06006492 RID: 25746 RVA: 0x00099E3E File Offset: 0x0009803E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700408B RID: 16523
			// (set) Token: 0x06006493 RID: 25747 RVA: 0x00099E56 File Offset: 0x00098056
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
