using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B24 RID: 2852
	[Cmdlet("Set", "ThrottlingPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetThrottlingPolicy : SetSystemConfigurationObjectTask<ThrottlingPolicyIdParameter, ThrottlingPolicy>
	{
		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x060065F6 RID: 26102 RVA: 0x001A71D5 File Offset: 0x001A53D5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetThrottlingPolicy(this.Identity.ToString(), this.DataObject.ThrottlingPolicyScope.ToString());
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x001A71FC File Offset: 0x001A53FC
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x060065F8 RID: 26104 RVA: 0x001A720E File Offset: 0x001A540E
		// (set) Token: 0x060065F9 RID: 26105 RVA: 0x001A7216 File Offset: 0x001A5416
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return base.InternalForce;
			}
			set
			{
				base.InternalForce = value;
			}
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x060065FA RID: 26106 RVA: 0x001A721F File Offset: 0x001A541F
		// (set) Token: 0x060065FB RID: 26107 RVA: 0x001A7227 File Offset: 0x001A5427
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x060065FC RID: 26108 RVA: 0x001A7230 File Offset: 0x001A5430
		// (set) Token: 0x060065FD RID: 26109 RVA: 0x001A7256 File Offset: 0x001A5456
		[Parameter(Mandatory = false)]
		public SwitchParameter ForceSettingGlobal
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceSettingGlobal"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceSettingGlobal"] = value;
			}
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x060065FE RID: 26110 RVA: 0x001A726E File Offset: 0x001A546E
		// (set) Token: 0x060065FF RID: 26111 RVA: 0x001A727B File Offset: 0x001A547B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.AnonymousMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.AnonymousMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x06006600 RID: 26112 RVA: 0x001A7293 File Offset: 0x001A5493
		// (set) Token: 0x06006601 RID: 26113 RVA: 0x001A72A0 File Offset: 0x001A54A0
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.AnonymousMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.AnonymousMaxBurst] = value;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x06006602 RID: 26114 RVA: 0x001A72B8 File Offset: 0x001A54B8
		// (set) Token: 0x06006603 RID: 26115 RVA: 0x001A72C5 File Offset: 0x001A54C5
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.AnonymousRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.AnonymousRechargeRate] = value;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x06006604 RID: 26116 RVA: 0x001A72DD File Offset: 0x001A54DD
		// (set) Token: 0x06006605 RID: 26117 RVA: 0x001A72EA File Offset: 0x001A54EA
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.AnonymousCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.AnonymousCutoffBalance] = value;
			}
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06006606 RID: 26118 RVA: 0x001A7302 File Offset: 0x001A5502
		// (set) Token: 0x06006607 RID: 26119 RVA: 0x001A730F File Offset: 0x001A550F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06006608 RID: 26120 RVA: 0x001A7327 File Offset: 0x001A5527
		// (set) Token: 0x06006609 RID: 26121 RVA: 0x001A7334 File Offset: 0x001A5534
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasMaxBurst] = value;
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x0600660A RID: 26122 RVA: 0x001A734C File Offset: 0x001A554C
		// (set) Token: 0x0600660B RID: 26123 RVA: 0x001A7359 File Offset: 0x001A5559
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasRechargeRate] = value;
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x001A7371 File Offset: 0x001A5571
		// (set) Token: 0x0600660D RID: 26125 RVA: 0x001A737E File Offset: 0x001A557E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasCutoffBalance] = value;
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x0600660E RID: 26126 RVA: 0x001A7396 File Offset: 0x001A5596
		// (set) Token: 0x0600660F RID: 26127 RVA: 0x001A73A3 File Offset: 0x001A55A3
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxDevices
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasMaxDevices);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasMaxDevices] = value;
			}
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x06006610 RID: 26128 RVA: 0x001A73BB File Offset: 0x001A55BB
		// (set) Token: 0x06006611 RID: 26129 RVA: 0x001A73C8 File Offset: 0x001A55C8
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth] = value;
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x001A73E0 File Offset: 0x001A55E0
		// (set) Token: 0x06006613 RID: 26131 RVA: 0x001A73ED File Offset: 0x001A55ED
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EasMaxInactivityForDeviceCleanup);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EasMaxInactivityForDeviceCleanup] = value;
			}
		}

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x001A7405 File Offset: 0x001A5605
		// (set) Token: 0x06006615 RID: 26133 RVA: 0x001A7412 File Offset: 0x001A5612
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EwsMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EwsMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x001A742A File Offset: 0x001A562A
		// (set) Token: 0x06006617 RID: 26135 RVA: 0x001A7437 File Offset: 0x001A5637
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EwsMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EwsMaxBurst] = value;
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x001A744F File Offset: 0x001A564F
		// (set) Token: 0x06006619 RID: 26137 RVA: 0x001A745C File Offset: 0x001A565C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EwsRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EwsRechargeRate] = value;
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x001A7474 File Offset: 0x001A5674
		// (set) Token: 0x0600661B RID: 26139 RVA: 0x001A7481 File Offset: 0x001A5681
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EwsCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EwsCutoffBalance] = value;
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x001A7499 File Offset: 0x001A5699
		// (set) Token: 0x0600661D RID: 26141 RVA: 0x001A74A6 File Offset: 0x001A56A6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxSubscriptions
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EwsMaxSubscriptions);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EwsMaxSubscriptions] = value;
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x001A74BE File Offset: 0x001A56BE
		// (set) Token: 0x0600661F RID: 26143 RVA: 0x001A74CB File Offset: 0x001A56CB
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ImapMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ImapMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x001A74E3 File Offset: 0x001A56E3
		// (set) Token: 0x06006621 RID: 26145 RVA: 0x001A74F0 File Offset: 0x001A56F0
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ImapMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ImapMaxBurst] = value;
			}
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06006622 RID: 26146 RVA: 0x001A7508 File Offset: 0x001A5708
		// (set) Token: 0x06006623 RID: 26147 RVA: 0x001A7515 File Offset: 0x001A5715
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ImapRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ImapRechargeRate] = value;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06006624 RID: 26148 RVA: 0x001A752D File Offset: 0x001A572D
		// (set) Token: 0x06006625 RID: 26149 RVA: 0x001A753A File Offset: 0x001A573A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ImapCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ImapCutoffBalance] = value;
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06006626 RID: 26150 RVA: 0x001A7552 File Offset: 0x001A5752
		// (set) Token: 0x06006627 RID: 26151 RVA: 0x001A755F File Offset: 0x001A575F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06006628 RID: 26152 RVA: 0x001A7577 File Offset: 0x001A5777
		// (set) Token: 0x06006629 RID: 26153 RVA: 0x001A7584 File Offset: 0x001A5784
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxBurst] = value;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x001A759C File Offset: 0x001A579C
		// (set) Token: 0x0600662B RID: 26155 RVA: 0x001A75A9 File Offset: 0x001A57A9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceRechargeRate] = value;
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x0600662C RID: 26156 RVA: 0x001A75C1 File Offset: 0x001A57C1
		// (set) Token: 0x0600662D RID: 26157 RVA: 0x001A75CE File Offset: 0x001A57CE
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceCutoffBalance] = value;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x0600662E RID: 26158 RVA: 0x001A75E6 File Offset: 0x001A57E6
		// (set) Token: 0x0600662F RID: 26159 RVA: 0x001A75F3 File Offset: 0x001A57F3
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSubscriptions
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceMaxSubscriptions);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxSubscriptions] = value;
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x06006630 RID: 26160 RVA: 0x001A760B File Offset: 0x001A580B
		// (set) Token: 0x06006631 RID: 26161 RVA: 0x001A7618 File Offset: 0x001A5818
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerDevice);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerDevice] = value;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x06006632 RID: 26162 RVA: 0x001A7630 File Offset: 0x001A5830
		// (set) Token: 0x06006633 RID: 26163 RVA: 0x001A763D File Offset: 0x001A583D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerUser);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerUser] = value;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06006634 RID: 26164 RVA: 0x001A7655 File Offset: 0x001A5855
		// (set) Token: 0x06006635 RID: 26165 RVA: 0x001A7662 File Offset: 0x001A5862
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06006636 RID: 26166 RVA: 0x001A767A File Offset: 0x001A587A
		// (set) Token: 0x06006637 RID: 26167 RVA: 0x001A7687 File Offset: 0x001A5887
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaMaxBurst] = value;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x001A769F File Offset: 0x001A589F
		// (set) Token: 0x06006639 RID: 26169 RVA: 0x001A76AC File Offset: 0x001A58AC
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaRechargeRate] = value;
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x0600663A RID: 26170 RVA: 0x001A76C4 File Offset: 0x001A58C4
		// (set) Token: 0x0600663B RID: 26171 RVA: 0x001A76D1 File Offset: 0x001A58D1
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaCutoffBalance] = value;
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x0600663C RID: 26172 RVA: 0x001A76E9 File Offset: 0x001A58E9
		// (set) Token: 0x0600663D RID: 26173 RVA: 0x001A76F6 File Offset: 0x001A58F6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaVoiceMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaVoiceMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x0600663E RID: 26174 RVA: 0x001A770E File Offset: 0x001A590E
		// (set) Token: 0x0600663F RID: 26175 RVA: 0x001A771B File Offset: 0x001A591B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaVoiceMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaVoiceMaxBurst] = value;
			}
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06006640 RID: 26176 RVA: 0x001A7733 File Offset: 0x001A5933
		// (set) Token: 0x06006641 RID: 26177 RVA: 0x001A7740 File Offset: 0x001A5940
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaVoiceRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaVoiceRechargeRate] = value;
			}
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06006642 RID: 26178 RVA: 0x001A7758 File Offset: 0x001A5958
		// (set) Token: 0x06006643 RID: 26179 RVA: 0x001A7765 File Offset: 0x001A5965
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.OwaVoiceCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.OwaVoiceCutoffBalance] = value;
			}
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06006644 RID: 26180 RVA: 0x001A777D File Offset: 0x001A597D
		// (set) Token: 0x06006645 RID: 26181 RVA: 0x001A778A File Offset: 0x001A598A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionSenderMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionSenderMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x001A77A2 File Offset: 0x001A59A2
		// (set) Token: 0x06006647 RID: 26183 RVA: 0x001A77AF File Offset: 0x001A59AF
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionSenderMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionSenderMaxBurst] = value;
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06006648 RID: 26184 RVA: 0x001A77C7 File Offset: 0x001A59C7
		// (set) Token: 0x06006649 RID: 26185 RVA: 0x001A77D4 File Offset: 0x001A59D4
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionSenderRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionSenderRechargeRate] = value;
			}
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x001A77EC File Offset: 0x001A59EC
		// (set) Token: 0x0600664B RID: 26187 RVA: 0x001A77F9 File Offset: 0x001A59F9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionSenderCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionSenderCutoffBalance] = value;
			}
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x001A7811 File Offset: 0x001A5A11
		// (set) Token: 0x0600664D RID: 26189 RVA: 0x001A781E File Offset: 0x001A5A1E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionRecipientMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionRecipientMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x001A7836 File Offset: 0x001A5A36
		// (set) Token: 0x0600664F RID: 26191 RVA: 0x001A7843 File Offset: 0x001A5A43
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionRecipientMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionRecipientMaxBurst] = value;
			}
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06006650 RID: 26192 RVA: 0x001A785B File Offset: 0x001A5A5B
		// (set) Token: 0x06006651 RID: 26193 RVA: 0x001A7868 File Offset: 0x001A5A68
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionRecipientRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionRecipientRechargeRate] = value;
			}
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06006652 RID: 26194 RVA: 0x001A7880 File Offset: 0x001A5A80
		// (set) Token: 0x06006653 RID: 26195 RVA: 0x001A788D File Offset: 0x001A5A8D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.EncryptionRecipientCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.EncryptionRecipientCutoffBalance] = value;
			}
		}

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06006654 RID: 26196 RVA: 0x001A78A5 File Offset: 0x001A5AA5
		// (set) Token: 0x06006655 RID: 26197 RVA: 0x001A78B2 File Offset: 0x001A5AB2
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PopMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PopMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06006656 RID: 26198 RVA: 0x001A78CA File Offset: 0x001A5ACA
		// (set) Token: 0x06006657 RID: 26199 RVA: 0x001A78D7 File Offset: 0x001A5AD7
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PopMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PopMaxBurst] = value;
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x001A78EF File Offset: 0x001A5AEF
		// (set) Token: 0x06006659 RID: 26201 RVA: 0x001A78FC File Offset: 0x001A5AFC
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PopRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PopRechargeRate] = value;
			}
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x0600665A RID: 26202 RVA: 0x001A7914 File Offset: 0x001A5B14
		// (set) Token: 0x0600665B RID: 26203 RVA: 0x001A7921 File Offset: 0x001A5B21
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PopCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PopCutoffBalance] = value;
			}
		}

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x0600665C RID: 26204 RVA: 0x001A7939 File Offset: 0x001A5B39
		// (set) Token: 0x0600665D RID: 26205 RVA: 0x001A7946 File Offset: 0x001A5B46
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x0600665E RID: 26206 RVA: 0x001A795E File Offset: 0x001A5B5E
		// (set) Token: 0x0600665F RID: 26207 RVA: 0x001A796B File Offset: 0x001A5B6B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxBurst] = value;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06006660 RID: 26208 RVA: 0x001A7983 File Offset: 0x001A5B83
		// (set) Token: 0x06006661 RID: 26209 RVA: 0x001A7990 File Offset: 0x001A5B90
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellRechargeRate] = value;
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x001A79A8 File Offset: 0x001A5BA8
		// (set) Token: 0x06006663 RID: 26211 RVA: 0x001A79B5 File Offset: 0x001A5BB5
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellCutoffBalance] = value;
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x06006664 RID: 26212 RVA: 0x001A79CD File Offset: 0x001A5BCD
		// (set) Token: 0x06006665 RID: 26213 RVA: 0x001A79DA File Offset: 0x001A5BDA
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxTenantConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxTenantConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxTenantConcurrency] = value;
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x001A79F2 File Offset: 0x001A5BF2
		// (set) Token: 0x06006667 RID: 26215 RVA: 0x001A79FF File Offset: 0x001A5BFF
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxOperations
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxOperations);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxOperations] = value;
			}
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x001A7A17 File Offset: 0x001A5C17
		// (set) Token: 0x06006669 RID: 26217 RVA: 0x001A7A24 File Offset: 0x001A5C24
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod] = value;
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x001A7A3C File Offset: 0x001A5C3C
		// (set) Token: 0x0600666B RID: 26219 RVA: 0x001A7A49 File Offset: 0x001A5C49
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ExchangeMaxCmdlets
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ExchangeMaxCmdlets);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ExchangeMaxCmdlets] = value;
			}
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x0600666C RID: 26220 RVA: 0x001A7A61 File Offset: 0x001A5C61
		// (set) Token: 0x0600666D RID: 26221 RVA: 0x001A7A6E File Offset: 0x001A5C6E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth] = value;
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x001A7A86 File Offset: 0x001A5C86
		// (set) Token: 0x0600666F RID: 26223 RVA: 0x001A7A93 File Offset: 0x001A5C93
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets] = value;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x001A7AAB File Offset: 0x001A5CAB
		// (set) Token: 0x06006671 RID: 26225 RVA: 0x001A7AB8 File Offset: 0x001A5CB8
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod] = value;
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x001A7AD0 File Offset: 0x001A5CD0
		// (set) Token: 0x06006673 RID: 26227 RVA: 0x001A7ADD File Offset: 0x001A5CDD
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdlets
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxCmdlets);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxCmdlets] = value;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x001A7AF5 File Offset: 0x001A5CF5
		// (set) Token: 0x06006675 RID: 26229 RVA: 0x001A7B02 File Offset: 0x001A5D02
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxRunspaces
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxRunspaces);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxRunspaces] = value;
			}
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06006676 RID: 26230 RVA: 0x001A7B1A File Offset: 0x001A5D1A
		// (set) Token: 0x06006677 RID: 26231 RVA: 0x001A7B27 File Offset: 0x001A5D27
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxTenantRunspaces
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxTenantRunspaces);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxTenantRunspaces] = value;
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06006678 RID: 26232 RVA: 0x001A7B3F File Offset: 0x001A5D3F
		// (set) Token: 0x06006679 RID: 26233 RVA: 0x001A7B4C File Offset: 0x001A5D4C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PowerShellMaxRunspacesTimePeriod);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PowerShellMaxRunspacesTimePeriod] = value;
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x001A7B64 File Offset: 0x001A5D64
		// (set) Token: 0x0600667B RID: 26235 RVA: 0x001A7B71 File Offset: 0x001A5D71
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PswsMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PswsMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x0600667C RID: 26236 RVA: 0x001A7B89 File Offset: 0x001A5D89
		// (set) Token: 0x0600667D RID: 26237 RVA: 0x001A7B96 File Offset: 0x001A5D96
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxRequest
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PswsMaxRequest);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PswsMaxRequest] = value;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x0600667E RID: 26238 RVA: 0x001A7BAE File Offset: 0x001A5DAE
		// (set) Token: 0x0600667F RID: 26239 RVA: 0x001A7BBB File Offset: 0x001A5DBB
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxRequestTimePeriod
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PswsMaxRequestTimePeriod);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PswsMaxRequestTimePeriod] = value;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06006680 RID: 26240 RVA: 0x001A7BD3 File Offset: 0x001A5DD3
		// (set) Token: 0x06006681 RID: 26241 RVA: 0x001A7BE0 File Offset: 0x001A5DE0
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.RcaMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.RcaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x001A7BF8 File Offset: 0x001A5DF8
		// (set) Token: 0x06006683 RID: 26243 RVA: 0x001A7C05 File Offset: 0x001A5E05
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.RcaMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.RcaMaxBurst] = value;
			}
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x001A7C1D File Offset: 0x001A5E1D
		// (set) Token: 0x06006685 RID: 26245 RVA: 0x001A7C2A File Offset: 0x001A5E2A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.RcaRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.RcaRechargeRate] = value;
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x001A7C42 File Offset: 0x001A5E42
		// (set) Token: 0x06006687 RID: 26247 RVA: 0x001A7C4F File Offset: 0x001A5E4F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.RcaCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.RcaCutoffBalance] = value;
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x001A7C67 File Offset: 0x001A5E67
		// (set) Token: 0x06006689 RID: 26249 RVA: 0x001A7C74 File Offset: 0x001A5E74
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.CpaMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.CpaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x001A7C8C File Offset: 0x001A5E8C
		// (set) Token: 0x0600668B RID: 26251 RVA: 0x001A7C99 File Offset: 0x001A5E99
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.CpaMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.CpaMaxBurst] = value;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x0600668C RID: 26252 RVA: 0x001A7CB1 File Offset: 0x001A5EB1
		// (set) Token: 0x0600668D RID: 26253 RVA: 0x001A7CBE File Offset: 0x001A5EBE
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.CpaRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.CpaRechargeRate] = value;
			}
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600668E RID: 26254 RVA: 0x001A7CD6 File Offset: 0x001A5ED6
		// (set) Token: 0x0600668F RID: 26255 RVA: 0x001A7CE3 File Offset: 0x001A5EE3
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.CpaCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.CpaCutoffBalance] = value;
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x06006690 RID: 26256 RVA: 0x001A7CFB File Offset: 0x001A5EFB
		// (set) Token: 0x06006691 RID: 26257 RVA: 0x001A7D08 File Offset: 0x001A5F08
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? MessageRateLimit
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.MessageRateLimit);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.MessageRateLimit] = value;
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x001A7D20 File Offset: 0x001A5F20
		// (set) Token: 0x06006693 RID: 26259 RVA: 0x001A7D2D File Offset: 0x001A5F2D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RecipientRateLimit
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.RecipientRateLimit);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.RecipientRateLimit] = value;
			}
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06006694 RID: 26260 RVA: 0x001A7D45 File Offset: 0x001A5F45
		// (set) Token: 0x06006695 RID: 26261 RVA: 0x001A7D52 File Offset: 0x001A5F52
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ForwardeeLimit
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ForwardeeLimit);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ForwardeeLimit] = value;
			}
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06006696 RID: 26262 RVA: 0x001A7D6A File Offset: 0x001A5F6A
		// (set) Token: 0x06006697 RID: 26263 RVA: 0x001A7D77 File Offset: 0x001A5F77
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x001A7D8F File Offset: 0x001A5F8F
		// (set) Token: 0x06006699 RID: 26265 RVA: 0x001A7D9C File Offset: 0x001A5F9C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxMailboxes
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxMailboxes);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxMailboxes] = value;
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x001A7DB4 File Offset: 0x001A5FB4
		// (set) Token: 0x0600669B RID: 26267 RVA: 0x001A7DC1 File Offset: 0x001A5FC1
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxKeywords
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxKeywords);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxKeywords] = value;
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x001A7DD9 File Offset: 0x001A5FD9
		// (set) Token: 0x0600669D RID: 26269 RVA: 0x001A7DE6 File Offset: 0x001A5FE6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxPreviewSearchMailboxes);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxPreviewSearchMailboxes] = value;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x001A7DFE File Offset: 0x001A5FFE
		// (set) Token: 0x0600669F RID: 26271 RVA: 0x001A7E0B File Offset: 0x001A600B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxStatsSearchMailboxes);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxStatsSearchMailboxes] = value;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x001A7E23 File Offset: 0x001A6023
		// (set) Token: 0x060066A1 RID: 26273 RVA: 0x001A7E30 File Offset: 0x001A6030
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryPreviewSearchResultsPageSize);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryPreviewSearchResultsPageSize] = value;
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x001A7E48 File Offset: 0x001A6048
		// (set) Token: 0x060066A3 RID: 26275 RVA: 0x001A7E55 File Offset: 0x001A6055
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxKeywordsPerPage);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxKeywordsPerPage] = value;
			}
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x001A7E6D File Offset: 0x001A606D
		// (set) Token: 0x060066A5 RID: 26277 RVA: 0x001A7E7A File Offset: 0x001A607A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxRefinerResults
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxRefinerResults);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxRefinerResults] = value;
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x001A7E92 File Offset: 0x001A6092
		// (set) Token: 0x060066A7 RID: 26279 RVA: 0x001A7E9F File Offset: 0x001A609F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoveryMaxSearchQueueDepth);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoveryMaxSearchQueueDepth] = value;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x001A7EB7 File Offset: 0x001A60B7
		// (set) Token: 0x060066A9 RID: 26281 RVA: 0x001A7EC4 File Offset: 0x001A60C4
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoverySearchTimeoutPeriod
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod] = value;
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x060066AA RID: 26282 RVA: 0x001A7EDC File Offset: 0x001A60DC
		// (set) Token: 0x060066AB RID: 26283 RVA: 0x001A7EE9 File Offset: 0x001A60E9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ComplianceMaxExpansionDGRecipients);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ComplianceMaxExpansionDGRecipients] = value;
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x001A7F01 File Offset: 0x001A6101
		// (set) Token: 0x060066AD RID: 26285 RVA: 0x001A7F0E File Offset: 0x001A610E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.ComplianceMaxExpansionNestedDGs);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.ComplianceMaxExpansionNestedDGs] = value;
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x001A7F26 File Offset: 0x001A6126
		// (set) Token: 0x060066AF RID: 26287 RVA: 0x001A7F33 File Offset: 0x001A6133
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxConcurrency
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationMaxConcurrency);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x001A7F4B File Offset: 0x001A614B
		// (set) Token: 0x060066B1 RID: 26289 RVA: 0x001A7F58 File Offset: 0x001A6158
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxBurst
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationMaxBurst);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationMaxBurst] = value;
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x001A7F70 File Offset: 0x001A6170
		// (set) Token: 0x060066B3 RID: 26291 RVA: 0x001A7F7D File Offset: 0x001A617D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationRechargeRate
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationRechargeRate);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationRechargeRate] = value;
			}
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x001A7F95 File Offset: 0x001A6195
		// (set) Token: 0x060066B5 RID: 26293 RVA: 0x001A7FA2 File Offset: 0x001A61A2
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationCutoffBalance
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationCutoffBalance);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationCutoffBalance] = value;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x001A7FBA File Offset: 0x001A61BA
		// (set) Token: 0x060066B7 RID: 26295 RVA: 0x001A7FC7 File Offset: 0x001A61C7
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxBurstPerDevice
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationMaxBurstPerDevice);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationMaxBurstPerDevice] = value;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x001A7FDF File Offset: 0x001A61DF
		// (set) Token: 0x060066B9 RID: 26297 RVA: 0x001A7FEC File Offset: 0x001A61EC
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationRechargeRatePerDevice
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationRechargeRatePerDevice);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationRechargeRatePerDevice] = value;
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x001A8004 File Offset: 0x001A6204
		// (set) Token: 0x060066BB RID: 26299 RVA: 0x001A8011 File Offset: 0x001A6211
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return this.SafeGetField(ThrottlingPolicySchema.PushNotificationSamplingPeriodPerDevice);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.PushNotificationSamplingPeriodPerDevice] = value;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x001A8029 File Offset: 0x001A6229
		// (set) Token: 0x060066BD RID: 26301 RVA: 0x001A804F File Offset: 0x001A624F
		[Parameter(Mandatory = false)]
		public SwitchParameter IsServiceAccount
		{
			get
			{
				return (SwitchParameter)(base.Fields[ThrottlingPolicySchema.IsServiceAccount] ?? false);
			}
			set
			{
				base.Fields[ThrottlingPolicySchema.IsServiceAccount] = value;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x001A8067 File Offset: 0x001A6267
		// (set) Token: 0x060066BF RID: 26303 RVA: 0x001A807E File Offset: 0x001A627E
		[Parameter(Mandatory = false)]
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return (ThrottlingPolicyScopeType)base.Fields[ThrottlingPolicySchema.ThrottlingPolicyScope];
			}
			set
			{
				base.VerifyValues<ThrottlingPolicyScopeType>(SetThrottlingPolicy.AllowedThrottlingPolicyScopeTypes, value);
				base.Fields[ThrottlingPolicySchema.ThrottlingPolicyScope] = value;
			}
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x001A80A2 File Offset: 0x001A62A2
		internal static Unlimited<uint> ConvertToUnlimitedNull(Unlimited<uint>? value)
		{
			if (value != null)
			{
				return value.Value;
			}
			return Unlimited<uint>.UnlimitedValue;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x001A80BC File Offset: 0x001A62BC
		internal static void VerifyMaxCmdlets(Unlimited<uint> powershellMaxOperations, Unlimited<uint> powershellMaxCmdlets, Unlimited<uint> exchangeMaxCmdlets, out bool haveWarning, out bool haveError)
		{
			haveWarning = false;
			if (powershellMaxCmdlets < exchangeMaxCmdlets || powershellMaxOperations < powershellMaxCmdlets)
			{
				haveError = true;
				return;
			}
			if (!powershellMaxCmdlets.IsUnlimited)
			{
				uint value = powershellMaxCmdlets.Value;
				uint value2 = exchangeMaxCmdlets.Value;
				haveWarning = (value < value2 * 1.2);
			}
			haveError = false;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x001A8114 File Offset: 0x001A6314
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Global)
			{
				if (!this.ForceSettingGlobal)
				{
					base.WriteError(new LocalizedException(Strings.ErrorCannotSetGlobalThrottlingPolicyWithoutForceSettingGlobalParameter), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				this.WriteWarning(Strings.ChangingGlobalPolicy(this.DataObject.Name));
			}
			if (base.Fields.IsModified(ThrottlingPolicySchema.ThrottlingPolicyScope) && this.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Organization)
			{
				this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
				ThrottlingPolicy[] array = this.ConfigurationSession.FindOrganizationThrottlingPolicies(this.DataObject.OrganizationId);
				if (array != null && array.Length > 0 && !array[0].Identity.Equals(this.DataObject.Identity))
				{
					base.WriteError(new LocalizedException(Strings.ErrorOrganizationThrottlingPolicyAlreadyExists(this.DataObject.OrganizationId.ToString())), (ErrorCategory)1000, null);
				}
			}
			if (this.ExchangeMaxCmdlets != null || this.PowerShellMaxCmdlets != null || this.PowerShellMaxOperations != null)
			{
				IThrottlingPolicy effectiveThrottlingPolicy = this.DataObject.GetEffectiveThrottlingPolicy(false);
				Unlimited<uint> powershellMaxOperations = SetThrottlingPolicy.ConvertToUnlimitedNull((this.PowerShellMaxOperations == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.PowerShellMaxOperations) : this.PowerShellMaxOperations);
				Unlimited<uint> powershellMaxCmdlets = SetThrottlingPolicy.ConvertToUnlimitedNull((this.PowerShellMaxCmdlets == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.PowerShellMaxCmdlets) : this.PowerShellMaxCmdlets);
				Unlimited<uint> exchangeMaxCmdlets = SetThrottlingPolicy.ConvertToUnlimitedNull((this.ExchangeMaxCmdlets == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.ExchangeMaxCmdlets) : this.ExchangeMaxCmdlets);
				bool flag;
				bool flag2;
				SetThrottlingPolicy.VerifyMaxCmdlets(powershellMaxOperations, powershellMaxCmdlets, exchangeMaxCmdlets, out flag, out flag2);
				if (flag2)
				{
					base.WriteError(new LocalizedException(Strings.ErrorMaxCmdletsNotSupported(powershellMaxOperations.ToString(), powershellMaxCmdlets.ToString(), exchangeMaxCmdlets.ToString())), (ErrorCategory)1000, null);
					return;
				}
				if (flag)
				{
					this.WriteWarning(Strings.WarningMaxCmdletsRatioNotSupported(powershellMaxCmdlets.ToString(), exchangeMaxCmdlets.ToString()));
				}
			}
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001A833C File Offset: 0x001A653C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			base.StampChangesOn(dataObject);
			ThrottlingPolicy throttlingPolicy = dataObject as ThrottlingPolicy;
			if (base.Fields.Contains(ThrottlingPolicySchema.IsServiceAccount))
			{
				throttlingPolicy.IsServiceAccount = this.IsServiceAccount;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ThrottlingPolicyScope))
			{
				throttlingPolicy.ThrottlingPolicyScope = this.ThrottlingPolicyScope;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.AnonymousMaxConcurrency))
			{
				throttlingPolicy.AnonymousMaxConcurrency = this.AnonymousMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.AnonymousMaxBurst))
			{
				throttlingPolicy.AnonymousMaxBurst = this.AnonymousMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.AnonymousRechargeRate))
			{
				throttlingPolicy.AnonymousRechargeRate = this.AnonymousRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.AnonymousCutoffBalance))
			{
				throttlingPolicy.AnonymousCutoffBalance = this.AnonymousCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasMaxConcurrency))
			{
				throttlingPolicy.EasMaxConcurrency = this.EasMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasMaxBurst))
			{
				throttlingPolicy.EasMaxBurst = this.EasMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasRechargeRate))
			{
				throttlingPolicy.EasRechargeRate = this.EasRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasCutoffBalance))
			{
				throttlingPolicy.EasCutoffBalance = this.EasCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasMaxDevices))
			{
				throttlingPolicy.EasMaxDevices = this.EasMaxDevices;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth))
			{
				throttlingPolicy.EasMaxDeviceDeletesPerMonth = this.EasMaxDeviceDeletesPerMonth;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EasMaxInactivityForDeviceCleanup))
			{
				throttlingPolicy.EasMaxInactivityForDeviceCleanup = this.EasMaxInactivityForDeviceCleanup;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EwsMaxConcurrency))
			{
				throttlingPolicy.EwsMaxConcurrency = this.EwsMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EwsMaxBurst))
			{
				throttlingPolicy.EwsMaxBurst = this.EwsMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EwsRechargeRate))
			{
				throttlingPolicy.EwsRechargeRate = this.EwsRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EwsCutoffBalance))
			{
				throttlingPolicy.EwsCutoffBalance = this.EwsCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EwsMaxSubscriptions))
			{
				throttlingPolicy.EwsMaxSubscriptions = this.EwsMaxSubscriptions;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ImapMaxConcurrency))
			{
				throttlingPolicy.ImapMaxConcurrency = this.ImapMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ImapMaxBurst))
			{
				throttlingPolicy.ImapMaxBurst = this.ImapMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ImapRechargeRate))
			{
				throttlingPolicy.ImapRechargeRate = this.ImapRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ImapCutoffBalance))
			{
				throttlingPolicy.ImapCutoffBalance = this.ImapCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceMaxConcurrency))
			{
				throttlingPolicy.OutlookServiceMaxConcurrency = this.OutlookServiceMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceMaxBurst))
			{
				throttlingPolicy.OutlookServiceMaxBurst = this.OutlookServiceMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceRechargeRate))
			{
				throttlingPolicy.OutlookServiceRechargeRate = this.OutlookServiceRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceCutoffBalance))
			{
				throttlingPolicy.OutlookServiceCutoffBalance = this.OutlookServiceCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceMaxSubscriptions))
			{
				throttlingPolicy.OutlookServiceMaxSubscriptions = this.OutlookServiceMaxSubscriptions;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerDevice))
			{
				throttlingPolicy.OutlookServiceMaxSocketConnectionsPerDevice = this.OutlookServiceMaxSocketConnectionsPerDevice;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerUser))
			{
				throttlingPolicy.OutlookServiceMaxSocketConnectionsPerUser = this.OutlookServiceMaxSocketConnectionsPerUser;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaMaxConcurrency))
			{
				throttlingPolicy.OwaMaxConcurrency = this.OwaMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaMaxBurst))
			{
				throttlingPolicy.OwaMaxBurst = this.OwaMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaRechargeRate))
			{
				throttlingPolicy.OwaRechargeRate = this.OwaRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaCutoffBalance))
			{
				throttlingPolicy.OwaCutoffBalance = this.OwaCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaVoiceMaxConcurrency))
			{
				throttlingPolicy.OwaVoiceMaxConcurrency = this.OwaVoiceMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaVoiceMaxBurst))
			{
				throttlingPolicy.OwaVoiceMaxBurst = this.OwaVoiceMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaVoiceRechargeRate))
			{
				throttlingPolicy.OwaVoiceRechargeRate = this.OwaVoiceRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.OwaVoiceCutoffBalance))
			{
				throttlingPolicy.OwaVoiceCutoffBalance = this.OwaVoiceCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionSenderMaxConcurrency))
			{
				throttlingPolicy.EncryptionSenderMaxConcurrency = this.EncryptionSenderMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionSenderMaxBurst))
			{
				throttlingPolicy.EncryptionSenderMaxBurst = this.EncryptionSenderMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionSenderRechargeRate))
			{
				throttlingPolicy.EncryptionSenderRechargeRate = this.EncryptionSenderRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionSenderCutoffBalance))
			{
				throttlingPolicy.EncryptionSenderCutoffBalance = this.EncryptionSenderCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionRecipientMaxConcurrency))
			{
				throttlingPolicy.EncryptionRecipientMaxConcurrency = this.EncryptionRecipientMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionRecipientMaxBurst))
			{
				throttlingPolicy.EncryptionRecipientMaxBurst = this.EncryptionRecipientMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionRecipientRechargeRate))
			{
				throttlingPolicy.EncryptionRecipientRechargeRate = this.EncryptionRecipientRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.EncryptionRecipientCutoffBalance))
			{
				throttlingPolicy.EncryptionRecipientCutoffBalance = this.EncryptionRecipientCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PopMaxConcurrency))
			{
				throttlingPolicy.PopMaxConcurrency = this.PopMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PopMaxBurst))
			{
				throttlingPolicy.PopMaxBurst = this.PopMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PopRechargeRate))
			{
				throttlingPolicy.PopRechargeRate = this.PopRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PopCutoffBalance))
			{
				throttlingPolicy.PopCutoffBalance = this.PopCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxConcurrency))
			{
				throttlingPolicy.PowerShellMaxConcurrency = this.PowerShellMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxTenantConcurrency))
			{
				if (throttlingPolicy.ThrottlingPolicyScope != ThrottlingPolicyScopeType.Regular)
				{
					throttlingPolicy.PowerShellMaxTenantConcurrency = this.PowerShellMaxTenantConcurrency;
				}
				else
				{
					base.WriteError(new LocalizedException(Strings.ErrorCannotSetPowerShellMaxTenantConcurrency), (ErrorCategory)1000, null);
				}
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxBurst))
			{
				throttlingPolicy.PowerShellMaxBurst = this.PowerShellMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellRechargeRate))
			{
				throttlingPolicy.PowerShellRechargeRate = this.PowerShellRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellCutoffBalance))
			{
				throttlingPolicy.PowerShellCutoffBalance = this.PowerShellCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxOperations))
			{
				throttlingPolicy.PowerShellMaxOperations = this.PowerShellMaxOperations;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod))
			{
				throttlingPolicy.PowerShellMaxCmdletsTimePeriod = this.PowerShellMaxCmdletsTimePeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ExchangeMaxCmdlets))
			{
				throttlingPolicy.ExchangeMaxCmdlets = this.ExchangeMaxCmdlets;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth))
			{
				throttlingPolicy.PowerShellMaxCmdletQueueDepth = this.PowerShellMaxCmdletQueueDepth;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets))
			{
				throttlingPolicy.PowerShellMaxDestructiveCmdlets = this.PowerShellMaxDestructiveCmdlets;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod))
			{
				throttlingPolicy.PowerShellMaxDestructiveCmdletsTimePeriod = this.PowerShellMaxDestructiveCmdletsTimePeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxCmdlets))
			{
				throttlingPolicy.PowerShellMaxCmdlets = this.PowerShellMaxCmdlets;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxRunspaces))
			{
				throttlingPolicy.PowerShellMaxRunspaces = this.PowerShellMaxRunspaces;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxTenantRunspaces))
			{
				if (throttlingPolicy.ThrottlingPolicyScope != ThrottlingPolicyScopeType.Regular)
				{
					throttlingPolicy.PowerShellMaxTenantRunspaces = this.PowerShellMaxTenantRunspaces;
				}
				else
				{
					base.WriteError(new LocalizedException(Strings.ErrorCannotSetPowerShellMaxTenantRunspaces), (ErrorCategory)1000, null);
				}
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxRunspacesTimePeriod))
			{
				throttlingPolicy.PowerShellMaxRunspacesTimePeriod = this.PowerShellMaxRunspacesTimePeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PswsMaxConcurrency))
			{
				throttlingPolicy.PswsMaxConcurrency = this.PswsMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PswsMaxRequest))
			{
				throttlingPolicy.PswsMaxRequest = this.PswsMaxRequest;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PswsMaxRequestTimePeriod))
			{
				throttlingPolicy.PswsMaxRequestTimePeriod = this.PswsMaxRequestTimePeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.RcaMaxConcurrency))
			{
				throttlingPolicy.RcaMaxConcurrency = this.RcaMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.RcaMaxBurst))
			{
				throttlingPolicy.RcaMaxBurst = this.RcaMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.RcaRechargeRate))
			{
				throttlingPolicy.RcaRechargeRate = this.RcaRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.RcaCutoffBalance))
			{
				throttlingPolicy.RcaCutoffBalance = this.RcaCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.CpaMaxConcurrency))
			{
				throttlingPolicy.CpaMaxConcurrency = this.CpaMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.CpaMaxBurst))
			{
				throttlingPolicy.CpaMaxBurst = this.CpaMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.CpaRechargeRate))
			{
				throttlingPolicy.CpaRechargeRate = this.CpaRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.CpaCutoffBalance))
			{
				throttlingPolicy.CpaCutoffBalance = this.CpaCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.MessageRateLimit))
			{
				throttlingPolicy.MessageRateLimit = this.MessageRateLimit;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.RecipientRateLimit))
			{
				throttlingPolicy.RecipientRateLimit = this.RecipientRateLimit;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ForwardeeLimit))
			{
				throttlingPolicy.ForwardeeLimit = this.ForwardeeLimit;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxConcurrency))
			{
				throttlingPolicy.DiscoveryMaxConcurrency = this.DiscoveryMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxMailboxes))
			{
				throttlingPolicy.DiscoveryMaxMailboxes = this.DiscoveryMaxMailboxes;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxKeywords))
			{
				throttlingPolicy.DiscoveryMaxKeywords = this.DiscoveryMaxKeywords;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxPreviewSearchMailboxes))
			{
				throttlingPolicy.DiscoveryMaxPreviewSearchMailboxes = this.DiscoveryMaxPreviewSearchMailboxes;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxStatsSearchMailboxes))
			{
				throttlingPolicy.DiscoveryMaxStatsSearchMailboxes = this.DiscoveryMaxStatsSearchMailboxes;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryPreviewSearchResultsPageSize))
			{
				throttlingPolicy.DiscoveryPreviewSearchResultsPageSize = this.DiscoveryPreviewSearchResultsPageSize;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxKeywordsPerPage))
			{
				throttlingPolicy.DiscoveryMaxKeywordsPerPage = this.DiscoveryMaxKeywordsPerPage;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxRefinerResults))
			{
				throttlingPolicy.DiscoveryMaxRefinerResults = this.DiscoveryMaxRefinerResults;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoveryMaxSearchQueueDepth))
			{
				throttlingPolicy.DiscoveryMaxSearchQueueDepth = this.DiscoveryMaxSearchQueueDepth;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod))
			{
				throttlingPolicy.DiscoverySearchTimeoutPeriod = this.DiscoverySearchTimeoutPeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod))
			{
				throttlingPolicy.DiscoverySearchTimeoutPeriod = this.DiscoverySearchTimeoutPeriod;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationMaxConcurrency))
			{
				throttlingPolicy.PushNotificationMaxConcurrency = this.PushNotificationMaxConcurrency;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationMaxBurst))
			{
				throttlingPolicy.PushNotificationMaxBurst = this.PushNotificationMaxBurst;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationRechargeRate))
			{
				throttlingPolicy.PushNotificationRechargeRate = this.PushNotificationRechargeRate;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationCutoffBalance))
			{
				throttlingPolicy.PushNotificationCutoffBalance = this.PushNotificationCutoffBalance;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationMaxBurstPerDevice))
			{
				throttlingPolicy.PushNotificationMaxBurstPerDevice = this.PushNotificationMaxBurstPerDevice;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationRechargeRatePerDevice))
			{
				throttlingPolicy.PushNotificationRechargeRatePerDevice = this.PushNotificationRechargeRatePerDevice;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.PushNotificationSamplingPeriodPerDevice))
			{
				throttlingPolicy.PushNotificationSamplingPeriodPerDevice = this.PushNotificationSamplingPeriodPerDevice;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ComplianceMaxExpansionDGRecipients))
			{
				throttlingPolicy.ComplianceMaxExpansionDGRecipients = this.ComplianceMaxExpansionDGRecipients;
			}
			if (base.Fields.Contains(ThrottlingPolicySchema.ComplianceMaxExpansionNestedDGs))
			{
				throttlingPolicy.ComplianceMaxExpansionNestedDGs = this.ComplianceMaxExpansionNestedDGs;
			}
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x001A8F18 File Offset: 0x001A7118
		private Unlimited<uint>? SafeGetField(ADPropertyDefinition key)
		{
			if (!base.Fields.Contains(key))
			{
				return null;
			}
			return (Unlimited<uint>?)base.Fields[key];
		}

		// Token: 0x04003647 RID: 13895
		internal static readonly ThrottlingPolicyScopeType[] AllowedThrottlingPolicyScopeTypes = new ThrottlingPolicyScopeType[]
		{
			ThrottlingPolicyScopeType.Regular,
			ThrottlingPolicyScopeType.Organization
		};
	}
}
