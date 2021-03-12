using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200005E RID: 94
	public class SetActiveSyncOrganizationSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<ActiveSyncOrganizationSettings>
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00035CBA File Offset: 0x00033EBA
		private SetActiveSyncOrganizationSettingsCommand() : base("Set-ActiveSyncOrganizationSettings")
		{
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00035CC7 File Offset: 0x00033EC7
		public SetActiveSyncOrganizationSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00035CD6 File Offset: 0x00033ED6
		public virtual SetActiveSyncOrganizationSettingsCommand SetParameters(SetActiveSyncOrganizationSettingsCommand.ParameterSetRemoveDeviceFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00035CE0 File Offset: 0x00033EE0
		public virtual SetActiveSyncOrganizationSettingsCommand SetParameters(SetActiveSyncOrganizationSettingsCommand.ParameterSetRemoveDeviceFilterRuleParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00035CEA File Offset: 0x00033EEA
		public virtual SetActiveSyncOrganizationSettingsCommand SetParameters(SetActiveSyncOrganizationSettingsCommand.ParameterSetAddDeviceFilterRuleParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00035CF4 File Offset: 0x00033EF4
		public virtual SetActiveSyncOrganizationSettingsCommand SetParameters(SetActiveSyncOrganizationSettingsCommand.ParameterSetAddDeviceFilterRuleForAllDevicesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00035CFE File Offset: 0x00033EFE
		public virtual SetActiveSyncOrganizationSettingsCommand SetParameters(SetActiveSyncOrganizationSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200005F RID: 95
		public class ParameterSetRemoveDeviceFilterParameters : ParametersBase
		{
			// Token: 0x1700022D RID: 557
			// (set) Token: 0x06001737 RID: 5943 RVA: 0x00035D08 File Offset: 0x00033F08
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x1700022E RID: 558
			// (set) Token: 0x06001738 RID: 5944 RVA: 0x00035D26 File Offset: 0x00033F26
			public virtual SwitchParameter RemoveDeviceFilter
			{
				set
				{
					base.PowerSharpParameters["RemoveDeviceFilter"] = value;
				}
			}

			// Token: 0x1700022F RID: 559
			// (set) Token: 0x06001739 RID: 5945 RVA: 0x00035D3E File Offset: 0x00033F3E
			public virtual string DeviceFilterName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterName"] = value;
				}
			}

			// Token: 0x17000230 RID: 560
			// (set) Token: 0x0600173A RID: 5946 RVA: 0x00035D51 File Offset: 0x00033F51
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000231 RID: 561
			// (set) Token: 0x0600173B RID: 5947 RVA: 0x00035D64 File Offset: 0x00033F64
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x17000232 RID: 562
			// (set) Token: 0x0600173C RID: 5948 RVA: 0x00035D7C File Offset: 0x00033F7C
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x17000233 RID: 563
			// (set) Token: 0x0600173D RID: 5949 RVA: 0x00035D8F File Offset: 0x00033F8F
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x17000234 RID: 564
			// (set) Token: 0x0600173E RID: 5950 RVA: 0x00035DA7 File Offset: 0x00033FA7
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x17000235 RID: 565
			// (set) Token: 0x0600173F RID: 5951 RVA: 0x00035DBA File Offset: 0x00033FBA
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x17000236 RID: 566
			// (set) Token: 0x06001740 RID: 5952 RVA: 0x00035DCD File Offset: 0x00033FCD
			public virtual ActiveSyncDeviceFilterArray DeviceFiltering
			{
				set
				{
					base.PowerSharpParameters["DeviceFiltering"] = value;
				}
			}

			// Token: 0x17000237 RID: 567
			// (set) Token: 0x06001741 RID: 5953 RVA: 0x00035DE0 File Offset: 0x00033FE0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000238 RID: 568
			// (set) Token: 0x06001742 RID: 5954 RVA: 0x00035DF8 File Offset: 0x00033FF8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000239 RID: 569
			// (set) Token: 0x06001743 RID: 5955 RVA: 0x00035E10 File Offset: 0x00034010
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700023A RID: 570
			// (set) Token: 0x06001744 RID: 5956 RVA: 0x00035E28 File Offset: 0x00034028
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700023B RID: 571
			// (set) Token: 0x06001745 RID: 5957 RVA: 0x00035E40 File Offset: 0x00034040
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000060 RID: 96
		public class ParameterSetRemoveDeviceFilterRuleParameters : ParametersBase
		{
			// Token: 0x1700023C RID: 572
			// (set) Token: 0x06001747 RID: 5959 RVA: 0x00035E60 File Offset: 0x00034060
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x1700023D RID: 573
			// (set) Token: 0x06001748 RID: 5960 RVA: 0x00035E7E File Offset: 0x0003407E
			public virtual SwitchParameter RemoveDeviceFilterRule
			{
				set
				{
					base.PowerSharpParameters["RemoveDeviceFilterRule"] = value;
				}
			}

			// Token: 0x1700023E RID: 574
			// (set) Token: 0x06001749 RID: 5961 RVA: 0x00035E96 File Offset: 0x00034096
			public virtual string DeviceFilterName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterName"] = value;
				}
			}

			// Token: 0x1700023F RID: 575
			// (set) Token: 0x0600174A RID: 5962 RVA: 0x00035EA9 File Offset: 0x000340A9
			public virtual string DeviceFilterRuleName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterRuleName"] = value;
				}
			}

			// Token: 0x17000240 RID: 576
			// (set) Token: 0x0600174B RID: 5963 RVA: 0x00035EBC File Offset: 0x000340BC
			public virtual DeviceAccessCharacteristic DeviceFilterCharacteristic
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterCharacteristic"] = value;
				}
			}

			// Token: 0x17000241 RID: 577
			// (set) Token: 0x0600174C RID: 5964 RVA: 0x00035ED4 File Offset: 0x000340D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000242 RID: 578
			// (set) Token: 0x0600174D RID: 5965 RVA: 0x00035EE7 File Offset: 0x000340E7
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x17000243 RID: 579
			// (set) Token: 0x0600174E RID: 5966 RVA: 0x00035EFF File Offset: 0x000340FF
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x17000244 RID: 580
			// (set) Token: 0x0600174F RID: 5967 RVA: 0x00035F12 File Offset: 0x00034112
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x17000245 RID: 581
			// (set) Token: 0x06001750 RID: 5968 RVA: 0x00035F2A File Offset: 0x0003412A
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x17000246 RID: 582
			// (set) Token: 0x06001751 RID: 5969 RVA: 0x00035F3D File Offset: 0x0003413D
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x17000247 RID: 583
			// (set) Token: 0x06001752 RID: 5970 RVA: 0x00035F50 File Offset: 0x00034150
			public virtual ActiveSyncDeviceFilterArray DeviceFiltering
			{
				set
				{
					base.PowerSharpParameters["DeviceFiltering"] = value;
				}
			}

			// Token: 0x17000248 RID: 584
			// (set) Token: 0x06001753 RID: 5971 RVA: 0x00035F63 File Offset: 0x00034163
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000249 RID: 585
			// (set) Token: 0x06001754 RID: 5972 RVA: 0x00035F7B File Offset: 0x0003417B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700024A RID: 586
			// (set) Token: 0x06001755 RID: 5973 RVA: 0x00035F93 File Offset: 0x00034193
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700024B RID: 587
			// (set) Token: 0x06001756 RID: 5974 RVA: 0x00035FAB File Offset: 0x000341AB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700024C RID: 588
			// (set) Token: 0x06001757 RID: 5975 RVA: 0x00035FC3 File Offset: 0x000341C3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000061 RID: 97
		public class ParameterSetAddDeviceFilterRuleParameters : ParametersBase
		{
			// Token: 0x1700024D RID: 589
			// (set) Token: 0x06001759 RID: 5977 RVA: 0x00035FE3 File Offset: 0x000341E3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x1700024E RID: 590
			// (set) Token: 0x0600175A RID: 5978 RVA: 0x00036001 File Offset: 0x00034201
			public virtual SwitchParameter AddDeviceFilterRule
			{
				set
				{
					base.PowerSharpParameters["AddDeviceFilterRule"] = value;
				}
			}

			// Token: 0x1700024F RID: 591
			// (set) Token: 0x0600175B RID: 5979 RVA: 0x00036019 File Offset: 0x00034219
			public virtual string DeviceFilterName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterName"] = value;
				}
			}

			// Token: 0x17000250 RID: 592
			// (set) Token: 0x0600175C RID: 5980 RVA: 0x0003602C File Offset: 0x0003422C
			public virtual string DeviceFilterRuleName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterRuleName"] = value;
				}
			}

			// Token: 0x17000251 RID: 593
			// (set) Token: 0x0600175D RID: 5981 RVA: 0x0003603F File Offset: 0x0003423F
			public virtual DeviceAccessCharacteristic DeviceFilterCharacteristic
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterCharacteristic"] = value;
				}
			}

			// Token: 0x17000252 RID: 594
			// (set) Token: 0x0600175E RID: 5982 RVA: 0x00036057 File Offset: 0x00034257
			public virtual DeviceFilterOperator DeviceFilterOperator
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterOperator"] = value;
				}
			}

			// Token: 0x17000253 RID: 595
			// (set) Token: 0x0600175F RID: 5983 RVA: 0x0003606F File Offset: 0x0003426F
			public virtual string DeviceFilterValue
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterValue"] = value;
				}
			}

			// Token: 0x17000254 RID: 596
			// (set) Token: 0x06001760 RID: 5984 RVA: 0x00036082 File Offset: 0x00034282
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000255 RID: 597
			// (set) Token: 0x06001761 RID: 5985 RVA: 0x00036095 File Offset: 0x00034295
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x17000256 RID: 598
			// (set) Token: 0x06001762 RID: 5986 RVA: 0x000360AD File Offset: 0x000342AD
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x17000257 RID: 599
			// (set) Token: 0x06001763 RID: 5987 RVA: 0x000360C0 File Offset: 0x000342C0
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x17000258 RID: 600
			// (set) Token: 0x06001764 RID: 5988 RVA: 0x000360D8 File Offset: 0x000342D8
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x17000259 RID: 601
			// (set) Token: 0x06001765 RID: 5989 RVA: 0x000360EB File Offset: 0x000342EB
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x1700025A RID: 602
			// (set) Token: 0x06001766 RID: 5990 RVA: 0x000360FE File Offset: 0x000342FE
			public virtual ActiveSyncDeviceFilterArray DeviceFiltering
			{
				set
				{
					base.PowerSharpParameters["DeviceFiltering"] = value;
				}
			}

			// Token: 0x1700025B RID: 603
			// (set) Token: 0x06001767 RID: 5991 RVA: 0x00036111 File Offset: 0x00034311
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700025C RID: 604
			// (set) Token: 0x06001768 RID: 5992 RVA: 0x00036129 File Offset: 0x00034329
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700025D RID: 605
			// (set) Token: 0x06001769 RID: 5993 RVA: 0x00036141 File Offset: 0x00034341
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700025E RID: 606
			// (set) Token: 0x0600176A RID: 5994 RVA: 0x00036159 File Offset: 0x00034359
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700025F RID: 607
			// (set) Token: 0x0600176B RID: 5995 RVA: 0x00036171 File Offset: 0x00034371
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000062 RID: 98
		public class ParameterSetAddDeviceFilterRuleForAllDevicesParameters : ParametersBase
		{
			// Token: 0x17000260 RID: 608
			// (set) Token: 0x0600176D RID: 5997 RVA: 0x00036191 File Offset: 0x00034391
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17000261 RID: 609
			// (set) Token: 0x0600176E RID: 5998 RVA: 0x000361AF File Offset: 0x000343AF
			public virtual SwitchParameter AddDeviceFilterRuleForAllDevices
			{
				set
				{
					base.PowerSharpParameters["AddDeviceFilterRuleForAllDevices"] = value;
				}
			}

			// Token: 0x17000262 RID: 610
			// (set) Token: 0x0600176F RID: 5999 RVA: 0x000361C7 File Offset: 0x000343C7
			public virtual string DeviceFilterName
			{
				set
				{
					base.PowerSharpParameters["DeviceFilterName"] = value;
				}
			}

			// Token: 0x17000263 RID: 611
			// (set) Token: 0x06001770 RID: 6000 RVA: 0x000361DA File Offset: 0x000343DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000264 RID: 612
			// (set) Token: 0x06001771 RID: 6001 RVA: 0x000361ED File Offset: 0x000343ED
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x17000265 RID: 613
			// (set) Token: 0x06001772 RID: 6002 RVA: 0x00036205 File Offset: 0x00034405
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x17000266 RID: 614
			// (set) Token: 0x06001773 RID: 6003 RVA: 0x00036218 File Offset: 0x00034418
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x17000267 RID: 615
			// (set) Token: 0x06001774 RID: 6004 RVA: 0x00036230 File Offset: 0x00034430
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x17000268 RID: 616
			// (set) Token: 0x06001775 RID: 6005 RVA: 0x00036243 File Offset: 0x00034443
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x17000269 RID: 617
			// (set) Token: 0x06001776 RID: 6006 RVA: 0x00036256 File Offset: 0x00034456
			public virtual ActiveSyncDeviceFilterArray DeviceFiltering
			{
				set
				{
					base.PowerSharpParameters["DeviceFiltering"] = value;
				}
			}

			// Token: 0x1700026A RID: 618
			// (set) Token: 0x06001777 RID: 6007 RVA: 0x00036269 File Offset: 0x00034469
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700026B RID: 619
			// (set) Token: 0x06001778 RID: 6008 RVA: 0x00036281 File Offset: 0x00034481
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700026C RID: 620
			// (set) Token: 0x06001779 RID: 6009 RVA: 0x00036299 File Offset: 0x00034499
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700026D RID: 621
			// (set) Token: 0x0600177A RID: 6010 RVA: 0x000362B1 File Offset: 0x000344B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700026E RID: 622
			// (set) Token: 0x0600177B RID: 6011 RVA: 0x000362C9 File Offset: 0x000344C9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000063 RID: 99
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700026F RID: 623
			// (set) Token: 0x0600177D RID: 6013 RVA: 0x000362E9 File Offset: 0x000344E9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncOrganizationSettingsIdParameter(value) : null);
				}
			}

			// Token: 0x17000270 RID: 624
			// (set) Token: 0x0600177E RID: 6014 RVA: 0x00036307 File Offset: 0x00034507
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000271 RID: 625
			// (set) Token: 0x0600177F RID: 6015 RVA: 0x0003631A File Offset: 0x0003451A
			public virtual DeviceAccessLevel DefaultAccessLevel
			{
				set
				{
					base.PowerSharpParameters["DefaultAccessLevel"] = value;
				}
			}

			// Token: 0x17000272 RID: 626
			// (set) Token: 0x06001780 RID: 6016 RVA: 0x00036332 File Offset: 0x00034532
			public virtual string UserMailInsert
			{
				set
				{
					base.PowerSharpParameters["UserMailInsert"] = value;
				}
			}

			// Token: 0x17000273 RID: 627
			// (set) Token: 0x06001781 RID: 6017 RVA: 0x00036345 File Offset: 0x00034545
			public virtual bool AllowAccessForUnSupportedPlatform
			{
				set
				{
					base.PowerSharpParameters["AllowAccessForUnSupportedPlatform"] = value;
				}
			}

			// Token: 0x17000274 RID: 628
			// (set) Token: 0x06001782 RID: 6018 RVA: 0x0003635D File Offset: 0x0003455D
			public virtual MultiValuedProperty<SmtpAddress> AdminMailRecipients
			{
				set
				{
					base.PowerSharpParameters["AdminMailRecipients"] = value;
				}
			}

			// Token: 0x17000275 RID: 629
			// (set) Token: 0x06001783 RID: 6019 RVA: 0x00036370 File Offset: 0x00034570
			public virtual string OtaNotificationMailInsert
			{
				set
				{
					base.PowerSharpParameters["OtaNotificationMailInsert"] = value;
				}
			}

			// Token: 0x17000276 RID: 630
			// (set) Token: 0x06001784 RID: 6020 RVA: 0x00036383 File Offset: 0x00034583
			public virtual ActiveSyncDeviceFilterArray DeviceFiltering
			{
				set
				{
					base.PowerSharpParameters["DeviceFiltering"] = value;
				}
			}

			// Token: 0x17000277 RID: 631
			// (set) Token: 0x06001785 RID: 6021 RVA: 0x00036396 File Offset: 0x00034596
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000278 RID: 632
			// (set) Token: 0x06001786 RID: 6022 RVA: 0x000363AE File Offset: 0x000345AE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000279 RID: 633
			// (set) Token: 0x06001787 RID: 6023 RVA: 0x000363C6 File Offset: 0x000345C6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700027A RID: 634
			// (set) Token: 0x06001788 RID: 6024 RVA: 0x000363DE File Offset: 0x000345DE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700027B RID: 635
			// (set) Token: 0x06001789 RID: 6025 RVA: 0x000363F6 File Offset: 0x000345F6
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
