using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B97 RID: 2967
	public class SetUMAutoAttendantCommand : SyntheticCommandWithPipelineInputNoOutput<UMAutoAttendant>
	{
		// Token: 0x06008F9F RID: 36767 RVA: 0x000D21EF File Offset: 0x000D03EF
		private SetUMAutoAttendantCommand() : base("Set-UMAutoAttendant")
		{
		}

		// Token: 0x06008FA0 RID: 36768 RVA: 0x000D21FC File Offset: 0x000D03FC
		public SetUMAutoAttendantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008FA1 RID: 36769 RVA: 0x000D220B File Offset: 0x000D040B
		public virtual SetUMAutoAttendantCommand SetParameters(SetUMAutoAttendantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x000D2215 File Offset: 0x000D0415
		public virtual SetUMAutoAttendantCommand SetParameters(SetUMAutoAttendantCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B98 RID: 2968
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700642A RID: 25642
			// (set) Token: 0x06008FA3 RID: 36771 RVA: 0x000D221F File Offset: 0x000D041F
			public virtual string DTMFFallbackAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["DTMFFallbackAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700642B RID: 25643
			// (set) Token: 0x06008FA4 RID: 36772 RVA: 0x000D223D File Offset: 0x000D043D
			public virtual AddressListIdParameter ContactAddressList
			{
				set
				{
					base.PowerSharpParameters["ContactAddressList"] = value;
				}
			}

			// Token: 0x1700642C RID: 25644
			// (set) Token: 0x06008FA5 RID: 36773 RVA: 0x000D2250 File Offset: 0x000D0450
			public virtual string ContactRecipientContainer
			{
				set
				{
					base.PowerSharpParameters["ContactRecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700642D RID: 25645
			// (set) Token: 0x06008FA6 RID: 36774 RVA: 0x000D226E File Offset: 0x000D046E
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x1700642E RID: 25646
			// (set) Token: 0x06008FA7 RID: 36775 RVA: 0x000D2286 File Offset: 0x000D0486
			public virtual string TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x1700642F RID: 25647
			// (set) Token: 0x06008FA8 RID: 36776 RVA: 0x000D2299 File Offset: 0x000D0499
			public virtual UMTimeZone TimeZoneName
			{
				set
				{
					base.PowerSharpParameters["TimeZoneName"] = value;
				}
			}

			// Token: 0x17006430 RID: 25648
			// (set) Token: 0x06008FA9 RID: 36777 RVA: 0x000D22AC File Offset: 0x000D04AC
			public virtual string DefaultMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006431 RID: 25649
			// (set) Token: 0x06008FAA RID: 36778 RVA: 0x000D22CA File Offset: 0x000D04CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006432 RID: 25650
			// (set) Token: 0x06008FAB RID: 36779 RVA: 0x000D22DD File Offset: 0x000D04DD
			public virtual bool SpeechEnabled
			{
				set
				{
					base.PowerSharpParameters["SpeechEnabled"] = value;
				}
			}

			// Token: 0x17006433 RID: 25651
			// (set) Token: 0x06008FAC RID: 36780 RVA: 0x000D22F5 File Offset: 0x000D04F5
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x17006434 RID: 25652
			// (set) Token: 0x06008FAD RID: 36781 RVA: 0x000D230D File Offset: 0x000D050D
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x17006435 RID: 25653
			// (set) Token: 0x06008FAE RID: 36782 RVA: 0x000D2325 File Offset: 0x000D0525
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x17006436 RID: 25654
			// (set) Token: 0x06008FAF RID: 36783 RVA: 0x000D2338 File Offset: 0x000D0538
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x17006437 RID: 25655
			// (set) Token: 0x06008FB0 RID: 36784 RVA: 0x000D234B File Offset: 0x000D054B
			public virtual bool CallSomeoneEnabled
			{
				set
				{
					base.PowerSharpParameters["CallSomeoneEnabled"] = value;
				}
			}

			// Token: 0x17006438 RID: 25656
			// (set) Token: 0x06008FB1 RID: 36785 RVA: 0x000D2363 File Offset: 0x000D0563
			public virtual DialScopeEnum ContactScope
			{
				set
				{
					base.PowerSharpParameters["ContactScope"] = value;
				}
			}

			// Token: 0x17006439 RID: 25657
			// (set) Token: 0x06008FB2 RID: 36786 RVA: 0x000D237B File Offset: 0x000D057B
			public virtual bool SendVoiceMsgEnabled
			{
				set
				{
					base.PowerSharpParameters["SendVoiceMsgEnabled"] = value;
				}
			}

			// Token: 0x1700643A RID: 25658
			// (set) Token: 0x06008FB3 RID: 36787 RVA: 0x000D2393 File Offset: 0x000D0593
			public virtual ScheduleInterval BusinessHoursSchedule
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursSchedule"] = value;
				}
			}

			// Token: 0x1700643B RID: 25659
			// (set) Token: 0x06008FB4 RID: 36788 RVA: 0x000D23AB File Offset: 0x000D05AB
			public virtual MultiValuedProperty<string> PilotIdentifierList
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifierList"] = value;
				}
			}

			// Token: 0x1700643C RID: 25660
			// (set) Token: 0x06008FB5 RID: 36789 RVA: 0x000D23BE File Offset: 0x000D05BE
			public virtual MultiValuedProperty<HolidaySchedule> HolidaySchedule
			{
				set
				{
					base.PowerSharpParameters["HolidaySchedule"] = value;
				}
			}

			// Token: 0x1700643D RID: 25661
			// (set) Token: 0x06008FB6 RID: 36790 RVA: 0x000D23D1 File Offset: 0x000D05D1
			public virtual AutoAttendantDisambiguationFieldEnum MatchedNameSelectionMethod
			{
				set
				{
					base.PowerSharpParameters["MatchedNameSelectionMethod"] = value;
				}
			}

			// Token: 0x1700643E RID: 25662
			// (set) Token: 0x06008FB7 RID: 36791 RVA: 0x000D23E9 File Offset: 0x000D05E9
			public virtual string BusinessLocation
			{
				set
				{
					base.PowerSharpParameters["BusinessLocation"] = value;
				}
			}

			// Token: 0x1700643F RID: 25663
			// (set) Token: 0x06008FB8 RID: 36792 RVA: 0x000D23FC File Offset: 0x000D05FC
			public virtual DayOfWeek WeekStartDay
			{
				set
				{
					base.PowerSharpParameters["WeekStartDay"] = value;
				}
			}

			// Token: 0x17006440 RID: 25664
			// (set) Token: 0x06008FB9 RID: 36793 RVA: 0x000D2414 File Offset: 0x000D0614
			public virtual UMLanguage Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17006441 RID: 25665
			// (set) Token: 0x06008FBA RID: 36794 RVA: 0x000D2427 File Offset: 0x000D0627
			public virtual string OperatorExtension
			{
				set
				{
					base.PowerSharpParameters["OperatorExtension"] = value;
				}
			}

			// Token: 0x17006442 RID: 25666
			// (set) Token: 0x06008FBB RID: 36795 RVA: 0x000D243A File Offset: 0x000D063A
			public virtual string InfoAnnouncementFilename
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementFilename"] = value;
				}
			}

			// Token: 0x17006443 RID: 25667
			// (set) Token: 0x06008FBC RID: 36796 RVA: 0x000D244D File Offset: 0x000D064D
			public virtual InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementEnabled"] = value;
				}
			}

			// Token: 0x17006444 RID: 25668
			// (set) Token: 0x06008FBD RID: 36797 RVA: 0x000D2465 File Offset: 0x000D0665
			public virtual bool NameLookupEnabled
			{
				set
				{
					base.PowerSharpParameters["NameLookupEnabled"] = value;
				}
			}

			// Token: 0x17006445 RID: 25669
			// (set) Token: 0x06008FBE RID: 36798 RVA: 0x000D247D File Offset: 0x000D067D
			public virtual bool StarOutToDialPlanEnabled
			{
				set
				{
					base.PowerSharpParameters["StarOutToDialPlanEnabled"] = value;
				}
			}

			// Token: 0x17006446 RID: 25670
			// (set) Token: 0x06008FBF RID: 36799 RVA: 0x000D2495 File Offset: 0x000D0695
			public virtual bool ForwardCallsToDefaultMailbox
			{
				set
				{
					base.PowerSharpParameters["ForwardCallsToDefaultMailbox"] = value;
				}
			}

			// Token: 0x17006447 RID: 25671
			// (set) Token: 0x06008FC0 RID: 36800 RVA: 0x000D24AD File Offset: 0x000D06AD
			public virtual string BusinessName
			{
				set
				{
					base.PowerSharpParameters["BusinessName"] = value;
				}
			}

			// Token: 0x17006448 RID: 25672
			// (set) Token: 0x06008FC1 RID: 36801 RVA: 0x000D24C0 File Offset: 0x000D06C0
			public virtual string BusinessHoursWelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x17006449 RID: 25673
			// (set) Token: 0x06008FC2 RID: 36802 RVA: 0x000D24D3 File Offset: 0x000D06D3
			public virtual bool BusinessHoursWelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x1700644A RID: 25674
			// (set) Token: 0x06008FC3 RID: 36803 RVA: 0x000D24EB File Offset: 0x000D06EB
			public virtual string BusinessHoursMainMenuCustomPromptFilename
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursMainMenuCustomPromptFilename"] = value;
				}
			}

			// Token: 0x1700644B RID: 25675
			// (set) Token: 0x06008FC4 RID: 36804 RVA: 0x000D24FE File Offset: 0x000D06FE
			public virtual bool BusinessHoursMainMenuCustomPromptEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursMainMenuCustomPromptEnabled"] = value;
				}
			}

			// Token: 0x1700644C RID: 25676
			// (set) Token: 0x06008FC5 RID: 36805 RVA: 0x000D2516 File Offset: 0x000D0716
			public virtual bool BusinessHoursTransferToOperatorEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursTransferToOperatorEnabled"] = value;
				}
			}

			// Token: 0x1700644D RID: 25677
			// (set) Token: 0x06008FC6 RID: 36806 RVA: 0x000D252E File Offset: 0x000D072E
			public virtual MultiValuedProperty<CustomMenuKeyMapping> BusinessHoursKeyMapping
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursKeyMapping"] = value;
				}
			}

			// Token: 0x1700644E RID: 25678
			// (set) Token: 0x06008FC7 RID: 36807 RVA: 0x000D2541 File Offset: 0x000D0741
			public virtual bool BusinessHoursKeyMappingEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursKeyMappingEnabled"] = value;
				}
			}

			// Token: 0x1700644F RID: 25679
			// (set) Token: 0x06008FC8 RID: 36808 RVA: 0x000D2559 File Offset: 0x000D0759
			public virtual string AfterHoursWelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x17006450 RID: 25680
			// (set) Token: 0x06008FC9 RID: 36809 RVA: 0x000D256C File Offset: 0x000D076C
			public virtual bool AfterHoursWelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x17006451 RID: 25681
			// (set) Token: 0x06008FCA RID: 36810 RVA: 0x000D2584 File Offset: 0x000D0784
			public virtual string AfterHoursMainMenuCustomPromptFilename
			{
				set
				{
					base.PowerSharpParameters["AfterHoursMainMenuCustomPromptFilename"] = value;
				}
			}

			// Token: 0x17006452 RID: 25682
			// (set) Token: 0x06008FCB RID: 36811 RVA: 0x000D2597 File Offset: 0x000D0797
			public virtual bool AfterHoursMainMenuCustomPromptEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursMainMenuCustomPromptEnabled"] = value;
				}
			}

			// Token: 0x17006453 RID: 25683
			// (set) Token: 0x06008FCC RID: 36812 RVA: 0x000D25AF File Offset: 0x000D07AF
			public virtual bool AfterHoursTransferToOperatorEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursTransferToOperatorEnabled"] = value;
				}
			}

			// Token: 0x17006454 RID: 25684
			// (set) Token: 0x06008FCD RID: 36813 RVA: 0x000D25C7 File Offset: 0x000D07C7
			public virtual MultiValuedProperty<CustomMenuKeyMapping> AfterHoursKeyMapping
			{
				set
				{
					base.PowerSharpParameters["AfterHoursKeyMapping"] = value;
				}
			}

			// Token: 0x17006455 RID: 25685
			// (set) Token: 0x06008FCE RID: 36814 RVA: 0x000D25DA File Offset: 0x000D07DA
			public virtual bool AfterHoursKeyMappingEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursKeyMappingEnabled"] = value;
				}
			}

			// Token: 0x17006456 RID: 25686
			// (set) Token: 0x06008FCF RID: 36815 RVA: 0x000D25F2 File Offset: 0x000D07F2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006457 RID: 25687
			// (set) Token: 0x06008FD0 RID: 36816 RVA: 0x000D2605 File Offset: 0x000D0805
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006458 RID: 25688
			// (set) Token: 0x06008FD1 RID: 36817 RVA: 0x000D261D File Offset: 0x000D081D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006459 RID: 25689
			// (set) Token: 0x06008FD2 RID: 36818 RVA: 0x000D2635 File Offset: 0x000D0835
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700645A RID: 25690
			// (set) Token: 0x06008FD3 RID: 36819 RVA: 0x000D264D File Offset: 0x000D084D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700645B RID: 25691
			// (set) Token: 0x06008FD4 RID: 36820 RVA: 0x000D2665 File Offset: 0x000D0865
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B99 RID: 2969
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700645C RID: 25692
			// (set) Token: 0x06008FD6 RID: 36822 RVA: 0x000D2685 File Offset: 0x000D0885
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700645D RID: 25693
			// (set) Token: 0x06008FD7 RID: 36823 RVA: 0x000D26A3 File Offset: 0x000D08A3
			public virtual string DTMFFallbackAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["DTMFFallbackAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700645E RID: 25694
			// (set) Token: 0x06008FD8 RID: 36824 RVA: 0x000D26C1 File Offset: 0x000D08C1
			public virtual AddressListIdParameter ContactAddressList
			{
				set
				{
					base.PowerSharpParameters["ContactAddressList"] = value;
				}
			}

			// Token: 0x1700645F RID: 25695
			// (set) Token: 0x06008FD9 RID: 36825 RVA: 0x000D26D4 File Offset: 0x000D08D4
			public virtual string ContactRecipientContainer
			{
				set
				{
					base.PowerSharpParameters["ContactRecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006460 RID: 25696
			// (set) Token: 0x06008FDA RID: 36826 RVA: 0x000D26F2 File Offset: 0x000D08F2
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17006461 RID: 25697
			// (set) Token: 0x06008FDB RID: 36827 RVA: 0x000D270A File Offset: 0x000D090A
			public virtual string TimeZone
			{
				set
				{
					base.PowerSharpParameters["TimeZone"] = value;
				}
			}

			// Token: 0x17006462 RID: 25698
			// (set) Token: 0x06008FDC RID: 36828 RVA: 0x000D271D File Offset: 0x000D091D
			public virtual UMTimeZone TimeZoneName
			{
				set
				{
					base.PowerSharpParameters["TimeZoneName"] = value;
				}
			}

			// Token: 0x17006463 RID: 25699
			// (set) Token: 0x06008FDD RID: 36829 RVA: 0x000D2730 File Offset: 0x000D0930
			public virtual string DefaultMailbox
			{
				set
				{
					base.PowerSharpParameters["DefaultMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006464 RID: 25700
			// (set) Token: 0x06008FDE RID: 36830 RVA: 0x000D274E File Offset: 0x000D094E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006465 RID: 25701
			// (set) Token: 0x06008FDF RID: 36831 RVA: 0x000D2761 File Offset: 0x000D0961
			public virtual bool SpeechEnabled
			{
				set
				{
					base.PowerSharpParameters["SpeechEnabled"] = value;
				}
			}

			// Token: 0x17006466 RID: 25702
			// (set) Token: 0x06008FE0 RID: 36832 RVA: 0x000D2779 File Offset: 0x000D0979
			public virtual bool AllowDialPlanSubscribers
			{
				set
				{
					base.PowerSharpParameters["AllowDialPlanSubscribers"] = value;
				}
			}

			// Token: 0x17006467 RID: 25703
			// (set) Token: 0x06008FE1 RID: 36833 RVA: 0x000D2791 File Offset: 0x000D0991
			public virtual bool AllowExtensions
			{
				set
				{
					base.PowerSharpParameters["AllowExtensions"] = value;
				}
			}

			// Token: 0x17006468 RID: 25704
			// (set) Token: 0x06008FE2 RID: 36834 RVA: 0x000D27A9 File Offset: 0x000D09A9
			public virtual MultiValuedProperty<string> AllowedInCountryOrRegionGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInCountryOrRegionGroups"] = value;
				}
			}

			// Token: 0x17006469 RID: 25705
			// (set) Token: 0x06008FE3 RID: 36835 RVA: 0x000D27BC File Offset: 0x000D09BC
			public virtual MultiValuedProperty<string> AllowedInternationalGroups
			{
				set
				{
					base.PowerSharpParameters["AllowedInternationalGroups"] = value;
				}
			}

			// Token: 0x1700646A RID: 25706
			// (set) Token: 0x06008FE4 RID: 36836 RVA: 0x000D27CF File Offset: 0x000D09CF
			public virtual bool CallSomeoneEnabled
			{
				set
				{
					base.PowerSharpParameters["CallSomeoneEnabled"] = value;
				}
			}

			// Token: 0x1700646B RID: 25707
			// (set) Token: 0x06008FE5 RID: 36837 RVA: 0x000D27E7 File Offset: 0x000D09E7
			public virtual DialScopeEnum ContactScope
			{
				set
				{
					base.PowerSharpParameters["ContactScope"] = value;
				}
			}

			// Token: 0x1700646C RID: 25708
			// (set) Token: 0x06008FE6 RID: 36838 RVA: 0x000D27FF File Offset: 0x000D09FF
			public virtual bool SendVoiceMsgEnabled
			{
				set
				{
					base.PowerSharpParameters["SendVoiceMsgEnabled"] = value;
				}
			}

			// Token: 0x1700646D RID: 25709
			// (set) Token: 0x06008FE7 RID: 36839 RVA: 0x000D2817 File Offset: 0x000D0A17
			public virtual ScheduleInterval BusinessHoursSchedule
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursSchedule"] = value;
				}
			}

			// Token: 0x1700646E RID: 25710
			// (set) Token: 0x06008FE8 RID: 36840 RVA: 0x000D282F File Offset: 0x000D0A2F
			public virtual MultiValuedProperty<string> PilotIdentifierList
			{
				set
				{
					base.PowerSharpParameters["PilotIdentifierList"] = value;
				}
			}

			// Token: 0x1700646F RID: 25711
			// (set) Token: 0x06008FE9 RID: 36841 RVA: 0x000D2842 File Offset: 0x000D0A42
			public virtual MultiValuedProperty<HolidaySchedule> HolidaySchedule
			{
				set
				{
					base.PowerSharpParameters["HolidaySchedule"] = value;
				}
			}

			// Token: 0x17006470 RID: 25712
			// (set) Token: 0x06008FEA RID: 36842 RVA: 0x000D2855 File Offset: 0x000D0A55
			public virtual AutoAttendantDisambiguationFieldEnum MatchedNameSelectionMethod
			{
				set
				{
					base.PowerSharpParameters["MatchedNameSelectionMethod"] = value;
				}
			}

			// Token: 0x17006471 RID: 25713
			// (set) Token: 0x06008FEB RID: 36843 RVA: 0x000D286D File Offset: 0x000D0A6D
			public virtual string BusinessLocation
			{
				set
				{
					base.PowerSharpParameters["BusinessLocation"] = value;
				}
			}

			// Token: 0x17006472 RID: 25714
			// (set) Token: 0x06008FEC RID: 36844 RVA: 0x000D2880 File Offset: 0x000D0A80
			public virtual DayOfWeek WeekStartDay
			{
				set
				{
					base.PowerSharpParameters["WeekStartDay"] = value;
				}
			}

			// Token: 0x17006473 RID: 25715
			// (set) Token: 0x06008FED RID: 36845 RVA: 0x000D2898 File Offset: 0x000D0A98
			public virtual UMLanguage Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17006474 RID: 25716
			// (set) Token: 0x06008FEE RID: 36846 RVA: 0x000D28AB File Offset: 0x000D0AAB
			public virtual string OperatorExtension
			{
				set
				{
					base.PowerSharpParameters["OperatorExtension"] = value;
				}
			}

			// Token: 0x17006475 RID: 25717
			// (set) Token: 0x06008FEF RID: 36847 RVA: 0x000D28BE File Offset: 0x000D0ABE
			public virtual string InfoAnnouncementFilename
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementFilename"] = value;
				}
			}

			// Token: 0x17006476 RID: 25718
			// (set) Token: 0x06008FF0 RID: 36848 RVA: 0x000D28D1 File Offset: 0x000D0AD1
			public virtual InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
			{
				set
				{
					base.PowerSharpParameters["InfoAnnouncementEnabled"] = value;
				}
			}

			// Token: 0x17006477 RID: 25719
			// (set) Token: 0x06008FF1 RID: 36849 RVA: 0x000D28E9 File Offset: 0x000D0AE9
			public virtual bool NameLookupEnabled
			{
				set
				{
					base.PowerSharpParameters["NameLookupEnabled"] = value;
				}
			}

			// Token: 0x17006478 RID: 25720
			// (set) Token: 0x06008FF2 RID: 36850 RVA: 0x000D2901 File Offset: 0x000D0B01
			public virtual bool StarOutToDialPlanEnabled
			{
				set
				{
					base.PowerSharpParameters["StarOutToDialPlanEnabled"] = value;
				}
			}

			// Token: 0x17006479 RID: 25721
			// (set) Token: 0x06008FF3 RID: 36851 RVA: 0x000D2919 File Offset: 0x000D0B19
			public virtual bool ForwardCallsToDefaultMailbox
			{
				set
				{
					base.PowerSharpParameters["ForwardCallsToDefaultMailbox"] = value;
				}
			}

			// Token: 0x1700647A RID: 25722
			// (set) Token: 0x06008FF4 RID: 36852 RVA: 0x000D2931 File Offset: 0x000D0B31
			public virtual string BusinessName
			{
				set
				{
					base.PowerSharpParameters["BusinessName"] = value;
				}
			}

			// Token: 0x1700647B RID: 25723
			// (set) Token: 0x06008FF5 RID: 36853 RVA: 0x000D2944 File Offset: 0x000D0B44
			public virtual string BusinessHoursWelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x1700647C RID: 25724
			// (set) Token: 0x06008FF6 RID: 36854 RVA: 0x000D2957 File Offset: 0x000D0B57
			public virtual bool BusinessHoursWelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursWelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x1700647D RID: 25725
			// (set) Token: 0x06008FF7 RID: 36855 RVA: 0x000D296F File Offset: 0x000D0B6F
			public virtual string BusinessHoursMainMenuCustomPromptFilename
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursMainMenuCustomPromptFilename"] = value;
				}
			}

			// Token: 0x1700647E RID: 25726
			// (set) Token: 0x06008FF8 RID: 36856 RVA: 0x000D2982 File Offset: 0x000D0B82
			public virtual bool BusinessHoursMainMenuCustomPromptEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursMainMenuCustomPromptEnabled"] = value;
				}
			}

			// Token: 0x1700647F RID: 25727
			// (set) Token: 0x06008FF9 RID: 36857 RVA: 0x000D299A File Offset: 0x000D0B9A
			public virtual bool BusinessHoursTransferToOperatorEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursTransferToOperatorEnabled"] = value;
				}
			}

			// Token: 0x17006480 RID: 25728
			// (set) Token: 0x06008FFA RID: 36858 RVA: 0x000D29B2 File Offset: 0x000D0BB2
			public virtual MultiValuedProperty<CustomMenuKeyMapping> BusinessHoursKeyMapping
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursKeyMapping"] = value;
				}
			}

			// Token: 0x17006481 RID: 25729
			// (set) Token: 0x06008FFB RID: 36859 RVA: 0x000D29C5 File Offset: 0x000D0BC5
			public virtual bool BusinessHoursKeyMappingEnabled
			{
				set
				{
					base.PowerSharpParameters["BusinessHoursKeyMappingEnabled"] = value;
				}
			}

			// Token: 0x17006482 RID: 25730
			// (set) Token: 0x06008FFC RID: 36860 RVA: 0x000D29DD File Offset: 0x000D0BDD
			public virtual string AfterHoursWelcomeGreetingFilename
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreetingFilename"] = value;
				}
			}

			// Token: 0x17006483 RID: 25731
			// (set) Token: 0x06008FFD RID: 36861 RVA: 0x000D29F0 File Offset: 0x000D0BF0
			public virtual bool AfterHoursWelcomeGreetingEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursWelcomeGreetingEnabled"] = value;
				}
			}

			// Token: 0x17006484 RID: 25732
			// (set) Token: 0x06008FFE RID: 36862 RVA: 0x000D2A08 File Offset: 0x000D0C08
			public virtual string AfterHoursMainMenuCustomPromptFilename
			{
				set
				{
					base.PowerSharpParameters["AfterHoursMainMenuCustomPromptFilename"] = value;
				}
			}

			// Token: 0x17006485 RID: 25733
			// (set) Token: 0x06008FFF RID: 36863 RVA: 0x000D2A1B File Offset: 0x000D0C1B
			public virtual bool AfterHoursMainMenuCustomPromptEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursMainMenuCustomPromptEnabled"] = value;
				}
			}

			// Token: 0x17006486 RID: 25734
			// (set) Token: 0x06009000 RID: 36864 RVA: 0x000D2A33 File Offset: 0x000D0C33
			public virtual bool AfterHoursTransferToOperatorEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursTransferToOperatorEnabled"] = value;
				}
			}

			// Token: 0x17006487 RID: 25735
			// (set) Token: 0x06009001 RID: 36865 RVA: 0x000D2A4B File Offset: 0x000D0C4B
			public virtual MultiValuedProperty<CustomMenuKeyMapping> AfterHoursKeyMapping
			{
				set
				{
					base.PowerSharpParameters["AfterHoursKeyMapping"] = value;
				}
			}

			// Token: 0x17006488 RID: 25736
			// (set) Token: 0x06009002 RID: 36866 RVA: 0x000D2A5E File Offset: 0x000D0C5E
			public virtual bool AfterHoursKeyMappingEnabled
			{
				set
				{
					base.PowerSharpParameters["AfterHoursKeyMappingEnabled"] = value;
				}
			}

			// Token: 0x17006489 RID: 25737
			// (set) Token: 0x06009003 RID: 36867 RVA: 0x000D2A76 File Offset: 0x000D0C76
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700648A RID: 25738
			// (set) Token: 0x06009004 RID: 36868 RVA: 0x000D2A89 File Offset: 0x000D0C89
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700648B RID: 25739
			// (set) Token: 0x06009005 RID: 36869 RVA: 0x000D2AA1 File Offset: 0x000D0CA1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700648C RID: 25740
			// (set) Token: 0x06009006 RID: 36870 RVA: 0x000D2AB9 File Offset: 0x000D0CB9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700648D RID: 25741
			// (set) Token: 0x06009007 RID: 36871 RVA: 0x000D2AD1 File Offset: 0x000D0CD1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700648E RID: 25742
			// (set) Token: 0x06009008 RID: 36872 RVA: 0x000D2AE9 File Offset: 0x000D0CE9
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
