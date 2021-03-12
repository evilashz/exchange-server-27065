using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200087B RID: 2171
	public class SetThrottlingPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<ThrottlingPolicy>
	{
		// Token: 0x06006BB6 RID: 27574 RVA: 0x000A33BD File Offset: 0x000A15BD
		private SetThrottlingPolicyCommand() : base("Set-ThrottlingPolicy")
		{
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x000A33CA File Offset: 0x000A15CA
		public SetThrottlingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006BB8 RID: 27576 RVA: 0x000A33D9 File Offset: 0x000A15D9
		public virtual SetThrottlingPolicyCommand SetParameters(SetThrottlingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x000A33E3 File Offset: 0x000A15E3
		public virtual SetThrottlingPolicyCommand SetParameters(SetThrottlingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200087C RID: 2172
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004679 RID: 18041
			// (set) Token: 0x06006BBA RID: 27578 RVA: 0x000A33ED File Offset: 0x000A15ED
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700467A RID: 18042
			// (set) Token: 0x06006BBB RID: 27579 RVA: 0x000A3405 File Offset: 0x000A1605
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x1700467B RID: 18043
			// (set) Token: 0x06006BBC RID: 27580 RVA: 0x000A341D File Offset: 0x000A161D
			public virtual SwitchParameter ForceSettingGlobal
			{
				set
				{
					base.PowerSharpParameters["ForceSettingGlobal"] = value;
				}
			}

			// Token: 0x1700467C RID: 18044
			// (set) Token: 0x06006BBD RID: 27581 RVA: 0x000A3435 File Offset: 0x000A1635
			public virtual Unlimited<uint>? AnonymousMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700467D RID: 18045
			// (set) Token: 0x06006BBE RID: 27582 RVA: 0x000A344D File Offset: 0x000A164D
			public virtual Unlimited<uint>? AnonymousMaxBurst
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxBurst"] = value;
				}
			}

			// Token: 0x1700467E RID: 18046
			// (set) Token: 0x06006BBF RID: 27583 RVA: 0x000A3465 File Offset: 0x000A1665
			public virtual Unlimited<uint>? AnonymousRechargeRate
			{
				set
				{
					base.PowerSharpParameters["AnonymousRechargeRate"] = value;
				}
			}

			// Token: 0x1700467F RID: 18047
			// (set) Token: 0x06006BC0 RID: 27584 RVA: 0x000A347D File Offset: 0x000A167D
			public virtual Unlimited<uint>? AnonymousCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["AnonymousCutoffBalance"] = value;
				}
			}

			// Token: 0x17004680 RID: 18048
			// (set) Token: 0x06006BC1 RID: 27585 RVA: 0x000A3495 File Offset: 0x000A1695
			public virtual Unlimited<uint>? EasMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EasMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004681 RID: 18049
			// (set) Token: 0x06006BC2 RID: 27586 RVA: 0x000A34AD File Offset: 0x000A16AD
			public virtual Unlimited<uint>? EasMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EasMaxBurst"] = value;
				}
			}

			// Token: 0x17004682 RID: 18050
			// (set) Token: 0x06006BC3 RID: 27587 RVA: 0x000A34C5 File Offset: 0x000A16C5
			public virtual Unlimited<uint>? EasRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EasRechargeRate"] = value;
				}
			}

			// Token: 0x17004683 RID: 18051
			// (set) Token: 0x06006BC4 RID: 27588 RVA: 0x000A34DD File Offset: 0x000A16DD
			public virtual Unlimited<uint>? EasCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EasCutoffBalance"] = value;
				}
			}

			// Token: 0x17004684 RID: 18052
			// (set) Token: 0x06006BC5 RID: 27589 RVA: 0x000A34F5 File Offset: 0x000A16F5
			public virtual Unlimited<uint>? EasMaxDevices
			{
				set
				{
					base.PowerSharpParameters["EasMaxDevices"] = value;
				}
			}

			// Token: 0x17004685 RID: 18053
			// (set) Token: 0x06006BC6 RID: 27590 RVA: 0x000A350D File Offset: 0x000A170D
			public virtual Unlimited<uint>? EasMaxDeviceDeletesPerMonth
			{
				set
				{
					base.PowerSharpParameters["EasMaxDeviceDeletesPerMonth"] = value;
				}
			}

			// Token: 0x17004686 RID: 18054
			// (set) Token: 0x06006BC7 RID: 27591 RVA: 0x000A3525 File Offset: 0x000A1725
			public virtual Unlimited<uint>? EasMaxInactivityForDeviceCleanup
			{
				set
				{
					base.PowerSharpParameters["EasMaxInactivityForDeviceCleanup"] = value;
				}
			}

			// Token: 0x17004687 RID: 18055
			// (set) Token: 0x06006BC8 RID: 27592 RVA: 0x000A353D File Offset: 0x000A173D
			public virtual Unlimited<uint>? EwsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EwsMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004688 RID: 18056
			// (set) Token: 0x06006BC9 RID: 27593 RVA: 0x000A3555 File Offset: 0x000A1755
			public virtual Unlimited<uint>? EwsMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EwsMaxBurst"] = value;
				}
			}

			// Token: 0x17004689 RID: 18057
			// (set) Token: 0x06006BCA RID: 27594 RVA: 0x000A356D File Offset: 0x000A176D
			public virtual Unlimited<uint>? EwsRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EwsRechargeRate"] = value;
				}
			}

			// Token: 0x1700468A RID: 18058
			// (set) Token: 0x06006BCB RID: 27595 RVA: 0x000A3585 File Offset: 0x000A1785
			public virtual Unlimited<uint>? EwsCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EwsCutoffBalance"] = value;
				}
			}

			// Token: 0x1700468B RID: 18059
			// (set) Token: 0x06006BCC RID: 27596 RVA: 0x000A359D File Offset: 0x000A179D
			public virtual Unlimited<uint>? EwsMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["EwsMaxSubscriptions"] = value;
				}
			}

			// Token: 0x1700468C RID: 18060
			// (set) Token: 0x06006BCD RID: 27597 RVA: 0x000A35B5 File Offset: 0x000A17B5
			public virtual Unlimited<uint>? ImapMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["ImapMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700468D RID: 18061
			// (set) Token: 0x06006BCE RID: 27598 RVA: 0x000A35CD File Offset: 0x000A17CD
			public virtual Unlimited<uint>? ImapMaxBurst
			{
				set
				{
					base.PowerSharpParameters["ImapMaxBurst"] = value;
				}
			}

			// Token: 0x1700468E RID: 18062
			// (set) Token: 0x06006BCF RID: 27599 RVA: 0x000A35E5 File Offset: 0x000A17E5
			public virtual Unlimited<uint>? ImapRechargeRate
			{
				set
				{
					base.PowerSharpParameters["ImapRechargeRate"] = value;
				}
			}

			// Token: 0x1700468F RID: 18063
			// (set) Token: 0x06006BD0 RID: 27600 RVA: 0x000A35FD File Offset: 0x000A17FD
			public virtual Unlimited<uint>? ImapCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["ImapCutoffBalance"] = value;
				}
			}

			// Token: 0x17004690 RID: 18064
			// (set) Token: 0x06006BD1 RID: 27601 RVA: 0x000A3615 File Offset: 0x000A1815
			public virtual Unlimited<uint>? OutlookServiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004691 RID: 18065
			// (set) Token: 0x06006BD2 RID: 27602 RVA: 0x000A362D File Offset: 0x000A182D
			public virtual Unlimited<uint>? OutlookServiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxBurst"] = value;
				}
			}

			// Token: 0x17004692 RID: 18066
			// (set) Token: 0x06006BD3 RID: 27603 RVA: 0x000A3645 File Offset: 0x000A1845
			public virtual Unlimited<uint>? OutlookServiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceRechargeRate"] = value;
				}
			}

			// Token: 0x17004693 RID: 18067
			// (set) Token: 0x06006BD4 RID: 27604 RVA: 0x000A365D File Offset: 0x000A185D
			public virtual Unlimited<uint>? OutlookServiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceCutoffBalance"] = value;
				}
			}

			// Token: 0x17004694 RID: 18068
			// (set) Token: 0x06006BD5 RID: 27605 RVA: 0x000A3675 File Offset: 0x000A1875
			public virtual Unlimited<uint>? OutlookServiceMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSubscriptions"] = value;
				}
			}

			// Token: 0x17004695 RID: 18069
			// (set) Token: 0x06006BD6 RID: 27606 RVA: 0x000A368D File Offset: 0x000A188D
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerDevice"] = value;
				}
			}

			// Token: 0x17004696 RID: 18070
			// (set) Token: 0x06006BD7 RID: 27607 RVA: 0x000A36A5 File Offset: 0x000A18A5
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerUser"] = value;
				}
			}

			// Token: 0x17004697 RID: 18071
			// (set) Token: 0x06006BD8 RID: 27608 RVA: 0x000A36BD File Offset: 0x000A18BD
			public virtual Unlimited<uint>? OwaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004698 RID: 18072
			// (set) Token: 0x06006BD9 RID: 27609 RVA: 0x000A36D5 File Offset: 0x000A18D5
			public virtual Unlimited<uint>? OwaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaMaxBurst"] = value;
				}
			}

			// Token: 0x17004699 RID: 18073
			// (set) Token: 0x06006BDA RID: 27610 RVA: 0x000A36ED File Offset: 0x000A18ED
			public virtual Unlimited<uint>? OwaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaRechargeRate"] = value;
				}
			}

			// Token: 0x1700469A RID: 18074
			// (set) Token: 0x06006BDB RID: 27611 RVA: 0x000A3705 File Offset: 0x000A1905
			public virtual Unlimited<uint>? OwaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaCutoffBalance"] = value;
				}
			}

			// Token: 0x1700469B RID: 18075
			// (set) Token: 0x06006BDC RID: 27612 RVA: 0x000A371D File Offset: 0x000A191D
			public virtual Unlimited<uint>? OwaVoiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700469C RID: 18076
			// (set) Token: 0x06006BDD RID: 27613 RVA: 0x000A3735 File Offset: 0x000A1935
			public virtual Unlimited<uint>? OwaVoiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxBurst"] = value;
				}
			}

			// Token: 0x1700469D RID: 18077
			// (set) Token: 0x06006BDE RID: 27614 RVA: 0x000A374D File Offset: 0x000A194D
			public virtual Unlimited<uint>? OwaVoiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceRechargeRate"] = value;
				}
			}

			// Token: 0x1700469E RID: 18078
			// (set) Token: 0x06006BDF RID: 27615 RVA: 0x000A3765 File Offset: 0x000A1965
			public virtual Unlimited<uint>? OwaVoiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceCutoffBalance"] = value;
				}
			}

			// Token: 0x1700469F RID: 18079
			// (set) Token: 0x06006BE0 RID: 27616 RVA: 0x000A377D File Offset: 0x000A197D
			public virtual Unlimited<uint>? EncryptionSenderMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046A0 RID: 18080
			// (set) Token: 0x06006BE1 RID: 27617 RVA: 0x000A3795 File Offset: 0x000A1995
			public virtual Unlimited<uint>? EncryptionSenderMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxBurst"] = value;
				}
			}

			// Token: 0x170046A1 RID: 18081
			// (set) Token: 0x06006BE2 RID: 27618 RVA: 0x000A37AD File Offset: 0x000A19AD
			public virtual Unlimited<uint>? EncryptionSenderRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderRechargeRate"] = value;
				}
			}

			// Token: 0x170046A2 RID: 18082
			// (set) Token: 0x06006BE3 RID: 27619 RVA: 0x000A37C5 File Offset: 0x000A19C5
			public virtual Unlimited<uint>? EncryptionSenderCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderCutoffBalance"] = value;
				}
			}

			// Token: 0x170046A3 RID: 18083
			// (set) Token: 0x06006BE4 RID: 27620 RVA: 0x000A37DD File Offset: 0x000A19DD
			public virtual Unlimited<uint>? EncryptionRecipientMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046A4 RID: 18084
			// (set) Token: 0x06006BE5 RID: 27621 RVA: 0x000A37F5 File Offset: 0x000A19F5
			public virtual Unlimited<uint>? EncryptionRecipientMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxBurst"] = value;
				}
			}

			// Token: 0x170046A5 RID: 18085
			// (set) Token: 0x06006BE6 RID: 27622 RVA: 0x000A380D File Offset: 0x000A1A0D
			public virtual Unlimited<uint>? EncryptionRecipientRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientRechargeRate"] = value;
				}
			}

			// Token: 0x170046A6 RID: 18086
			// (set) Token: 0x06006BE7 RID: 27623 RVA: 0x000A3825 File Offset: 0x000A1A25
			public virtual Unlimited<uint>? EncryptionRecipientCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientCutoffBalance"] = value;
				}
			}

			// Token: 0x170046A7 RID: 18087
			// (set) Token: 0x06006BE8 RID: 27624 RVA: 0x000A383D File Offset: 0x000A1A3D
			public virtual Unlimited<uint>? PopMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PopMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046A8 RID: 18088
			// (set) Token: 0x06006BE9 RID: 27625 RVA: 0x000A3855 File Offset: 0x000A1A55
			public virtual Unlimited<uint>? PopMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PopMaxBurst"] = value;
				}
			}

			// Token: 0x170046A9 RID: 18089
			// (set) Token: 0x06006BEA RID: 27626 RVA: 0x000A386D File Offset: 0x000A1A6D
			public virtual Unlimited<uint>? PopRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PopRechargeRate"] = value;
				}
			}

			// Token: 0x170046AA RID: 18090
			// (set) Token: 0x06006BEB RID: 27627 RVA: 0x000A3885 File Offset: 0x000A1A85
			public virtual Unlimited<uint>? PopCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PopCutoffBalance"] = value;
				}
			}

			// Token: 0x170046AB RID: 18091
			// (set) Token: 0x06006BEC RID: 27628 RVA: 0x000A389D File Offset: 0x000A1A9D
			public virtual Unlimited<uint>? PowerShellMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046AC RID: 18092
			// (set) Token: 0x06006BED RID: 27629 RVA: 0x000A38B5 File Offset: 0x000A1AB5
			public virtual Unlimited<uint>? PowerShellMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxBurst"] = value;
				}
			}

			// Token: 0x170046AD RID: 18093
			// (set) Token: 0x06006BEE RID: 27630 RVA: 0x000A38CD File Offset: 0x000A1ACD
			public virtual Unlimited<uint>? PowerShellRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PowerShellRechargeRate"] = value;
				}
			}

			// Token: 0x170046AE RID: 18094
			// (set) Token: 0x06006BEF RID: 27631 RVA: 0x000A38E5 File Offset: 0x000A1AE5
			public virtual Unlimited<uint>? PowerShellCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PowerShellCutoffBalance"] = value;
				}
			}

			// Token: 0x170046AF RID: 18095
			// (set) Token: 0x06006BF0 RID: 27632 RVA: 0x000A38FD File Offset: 0x000A1AFD
			public virtual Unlimited<uint>? PowerShellMaxTenantConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantConcurrency"] = value;
				}
			}

			// Token: 0x170046B0 RID: 18096
			// (set) Token: 0x06006BF1 RID: 27633 RVA: 0x000A3915 File Offset: 0x000A1B15
			public virtual Unlimited<uint>? PowerShellMaxOperations
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxOperations"] = value;
				}
			}

			// Token: 0x170046B1 RID: 18097
			// (set) Token: 0x06006BF2 RID: 27634 RVA: 0x000A392D File Offset: 0x000A1B2D
			public virtual Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x170046B2 RID: 18098
			// (set) Token: 0x06006BF3 RID: 27635 RVA: 0x000A3945 File Offset: 0x000A1B45
			public virtual Unlimited<uint>? ExchangeMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaxCmdlets"] = value;
				}
			}

			// Token: 0x170046B3 RID: 18099
			// (set) Token: 0x06006BF4 RID: 27636 RVA: 0x000A395D File Offset: 0x000A1B5D
			public virtual Unlimited<uint>? PowerShellMaxCmdletQueueDepth
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletQueueDepth"] = value;
				}
			}

			// Token: 0x170046B4 RID: 18100
			// (set) Token: 0x06006BF5 RID: 27637 RVA: 0x000A3975 File Offset: 0x000A1B75
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdlets"] = value;
				}
			}

			// Token: 0x170046B5 RID: 18101
			// (set) Token: 0x06006BF6 RID: 27638 RVA: 0x000A398D File Offset: 0x000A1B8D
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x170046B6 RID: 18102
			// (set) Token: 0x06006BF7 RID: 27639 RVA: 0x000A39A5 File Offset: 0x000A1BA5
			public virtual Unlimited<uint>? PowerShellMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdlets"] = value;
				}
			}

			// Token: 0x170046B7 RID: 18103
			// (set) Token: 0x06006BF8 RID: 27640 RVA: 0x000A39BD File Offset: 0x000A1BBD
			public virtual Unlimited<uint>? PowerShellMaxRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspaces"] = value;
				}
			}

			// Token: 0x170046B8 RID: 18104
			// (set) Token: 0x06006BF9 RID: 27641 RVA: 0x000A39D5 File Offset: 0x000A1BD5
			public virtual Unlimited<uint>? PowerShellMaxTenantRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantRunspaces"] = value;
				}
			}

			// Token: 0x170046B9 RID: 18105
			// (set) Token: 0x06006BFA RID: 27642 RVA: 0x000A39ED File Offset: 0x000A1BED
			public virtual Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspacesTimePeriod"] = value;
				}
			}

			// Token: 0x170046BA RID: 18106
			// (set) Token: 0x06006BFB RID: 27643 RVA: 0x000A3A05 File Offset: 0x000A1C05
			public virtual Unlimited<uint>? PswsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PswsMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046BB RID: 18107
			// (set) Token: 0x06006BFC RID: 27644 RVA: 0x000A3A1D File Offset: 0x000A1C1D
			public virtual Unlimited<uint>? PswsMaxRequest
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequest"] = value;
				}
			}

			// Token: 0x170046BC RID: 18108
			// (set) Token: 0x06006BFD RID: 27645 RVA: 0x000A3A35 File Offset: 0x000A1C35
			public virtual Unlimited<uint>? PswsMaxRequestTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequestTimePeriod"] = value;
				}
			}

			// Token: 0x170046BD RID: 18109
			// (set) Token: 0x06006BFE RID: 27646 RVA: 0x000A3A4D File Offset: 0x000A1C4D
			public virtual Unlimited<uint>? RcaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["RcaMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046BE RID: 18110
			// (set) Token: 0x06006BFF RID: 27647 RVA: 0x000A3A65 File Offset: 0x000A1C65
			public virtual Unlimited<uint>? RcaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["RcaMaxBurst"] = value;
				}
			}

			// Token: 0x170046BF RID: 18111
			// (set) Token: 0x06006C00 RID: 27648 RVA: 0x000A3A7D File Offset: 0x000A1C7D
			public virtual Unlimited<uint>? RcaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["RcaRechargeRate"] = value;
				}
			}

			// Token: 0x170046C0 RID: 18112
			// (set) Token: 0x06006C01 RID: 27649 RVA: 0x000A3A95 File Offset: 0x000A1C95
			public virtual Unlimited<uint>? RcaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["RcaCutoffBalance"] = value;
				}
			}

			// Token: 0x170046C1 RID: 18113
			// (set) Token: 0x06006C02 RID: 27650 RVA: 0x000A3AAD File Offset: 0x000A1CAD
			public virtual Unlimited<uint>? CpaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["CpaMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046C2 RID: 18114
			// (set) Token: 0x06006C03 RID: 27651 RVA: 0x000A3AC5 File Offset: 0x000A1CC5
			public virtual Unlimited<uint>? CpaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["CpaMaxBurst"] = value;
				}
			}

			// Token: 0x170046C3 RID: 18115
			// (set) Token: 0x06006C04 RID: 27652 RVA: 0x000A3ADD File Offset: 0x000A1CDD
			public virtual Unlimited<uint>? CpaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["CpaRechargeRate"] = value;
				}
			}

			// Token: 0x170046C4 RID: 18116
			// (set) Token: 0x06006C05 RID: 27653 RVA: 0x000A3AF5 File Offset: 0x000A1CF5
			public virtual Unlimited<uint>? CpaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["CpaCutoffBalance"] = value;
				}
			}

			// Token: 0x170046C5 RID: 18117
			// (set) Token: 0x06006C06 RID: 27654 RVA: 0x000A3B0D File Offset: 0x000A1D0D
			public virtual Unlimited<uint>? MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x170046C6 RID: 18118
			// (set) Token: 0x06006C07 RID: 27655 RVA: 0x000A3B25 File Offset: 0x000A1D25
			public virtual Unlimited<uint>? RecipientRateLimit
			{
				set
				{
					base.PowerSharpParameters["RecipientRateLimit"] = value;
				}
			}

			// Token: 0x170046C7 RID: 18119
			// (set) Token: 0x06006C08 RID: 27656 RVA: 0x000A3B3D File Offset: 0x000A1D3D
			public virtual Unlimited<uint>? ForwardeeLimit
			{
				set
				{
					base.PowerSharpParameters["ForwardeeLimit"] = value;
				}
			}

			// Token: 0x170046C8 RID: 18120
			// (set) Token: 0x06006C09 RID: 27657 RVA: 0x000A3B55 File Offset: 0x000A1D55
			public virtual Unlimited<uint>? DiscoveryMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046C9 RID: 18121
			// (set) Token: 0x06006C0A RID: 27658 RVA: 0x000A3B6D File Offset: 0x000A1D6D
			public virtual Unlimited<uint>? DiscoveryMaxMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxMailboxes"] = value;
				}
			}

			// Token: 0x170046CA RID: 18122
			// (set) Token: 0x06006C0B RID: 27659 RVA: 0x000A3B85 File Offset: 0x000A1D85
			public virtual Unlimited<uint>? DiscoveryMaxKeywords
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywords"] = value;
				}
			}

			// Token: 0x170046CB RID: 18123
			// (set) Token: 0x06006C0C RID: 27660 RVA: 0x000A3B9D File Offset: 0x000A1D9D
			public virtual Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxPreviewSearchMailboxes"] = value;
				}
			}

			// Token: 0x170046CC RID: 18124
			// (set) Token: 0x06006C0D RID: 27661 RVA: 0x000A3BB5 File Offset: 0x000A1DB5
			public virtual Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxStatsSearchMailboxes"] = value;
				}
			}

			// Token: 0x170046CD RID: 18125
			// (set) Token: 0x06006C0E RID: 27662 RVA: 0x000A3BCD File Offset: 0x000A1DCD
			public virtual Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
			{
				set
				{
					base.PowerSharpParameters["DiscoveryPreviewSearchResultsPageSize"] = value;
				}
			}

			// Token: 0x170046CE RID: 18126
			// (set) Token: 0x06006C0F RID: 27663 RVA: 0x000A3BE5 File Offset: 0x000A1DE5
			public virtual Unlimited<uint>? DiscoveryMaxKeywordsPerPage
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywordsPerPage"] = value;
				}
			}

			// Token: 0x170046CF RID: 18127
			// (set) Token: 0x06006C10 RID: 27664 RVA: 0x000A3BFD File Offset: 0x000A1DFD
			public virtual Unlimited<uint>? DiscoveryMaxRefinerResults
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxRefinerResults"] = value;
				}
			}

			// Token: 0x170046D0 RID: 18128
			// (set) Token: 0x06006C11 RID: 27665 RVA: 0x000A3C15 File Offset: 0x000A1E15
			public virtual Unlimited<uint>? DiscoveryMaxSearchQueueDepth
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxSearchQueueDepth"] = value;
				}
			}

			// Token: 0x170046D1 RID: 18129
			// (set) Token: 0x06006C12 RID: 27666 RVA: 0x000A3C2D File Offset: 0x000A1E2D
			public virtual Unlimited<uint>? DiscoverySearchTimeoutPeriod
			{
				set
				{
					base.PowerSharpParameters["DiscoverySearchTimeoutPeriod"] = value;
				}
			}

			// Token: 0x170046D2 RID: 18130
			// (set) Token: 0x06006C13 RID: 27667 RVA: 0x000A3C45 File Offset: 0x000A1E45
			public virtual Unlimited<uint>? ComplianceMaxExpansionDGRecipients
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionDGRecipients"] = value;
				}
			}

			// Token: 0x170046D3 RID: 18131
			// (set) Token: 0x06006C14 RID: 27668 RVA: 0x000A3C5D File Offset: 0x000A1E5D
			public virtual Unlimited<uint>? ComplianceMaxExpansionNestedDGs
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionNestedDGs"] = value;
				}
			}

			// Token: 0x170046D4 RID: 18132
			// (set) Token: 0x06006C15 RID: 27669 RVA: 0x000A3C75 File Offset: 0x000A1E75
			public virtual Unlimited<uint>? PushNotificationMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046D5 RID: 18133
			// (set) Token: 0x06006C16 RID: 27670 RVA: 0x000A3C8D File Offset: 0x000A1E8D
			public virtual Unlimited<uint>? PushNotificationMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurst"] = value;
				}
			}

			// Token: 0x170046D6 RID: 18134
			// (set) Token: 0x06006C17 RID: 27671 RVA: 0x000A3CA5 File Offset: 0x000A1EA5
			public virtual Unlimited<uint>? PushNotificationRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRate"] = value;
				}
			}

			// Token: 0x170046D7 RID: 18135
			// (set) Token: 0x06006C18 RID: 27672 RVA: 0x000A3CBD File Offset: 0x000A1EBD
			public virtual Unlimited<uint>? PushNotificationCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PushNotificationCutoffBalance"] = value;
				}
			}

			// Token: 0x170046D8 RID: 18136
			// (set) Token: 0x06006C19 RID: 27673 RVA: 0x000A3CD5 File Offset: 0x000A1ED5
			public virtual Unlimited<uint>? PushNotificationMaxBurstPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurstPerDevice"] = value;
				}
			}

			// Token: 0x170046D9 RID: 18137
			// (set) Token: 0x06006C1A RID: 27674 RVA: 0x000A3CED File Offset: 0x000A1EED
			public virtual Unlimited<uint>? PushNotificationRechargeRatePerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRatePerDevice"] = value;
				}
			}

			// Token: 0x170046DA RID: 18138
			// (set) Token: 0x06006C1B RID: 27675 RVA: 0x000A3D05 File Offset: 0x000A1F05
			public virtual Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationSamplingPeriodPerDevice"] = value;
				}
			}

			// Token: 0x170046DB RID: 18139
			// (set) Token: 0x06006C1C RID: 27676 RVA: 0x000A3D1D File Offset: 0x000A1F1D
			public virtual SwitchParameter IsServiceAccount
			{
				set
				{
					base.PowerSharpParameters["IsServiceAccount"] = value;
				}
			}

			// Token: 0x170046DC RID: 18140
			// (set) Token: 0x06006C1D RID: 27677 RVA: 0x000A3D35 File Offset: 0x000A1F35
			public virtual ThrottlingPolicyScopeType ThrottlingPolicyScope
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicyScope"] = value;
				}
			}

			// Token: 0x170046DD RID: 18141
			// (set) Token: 0x06006C1E RID: 27678 RVA: 0x000A3D4D File Offset: 0x000A1F4D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170046DE RID: 18142
			// (set) Token: 0x06006C1F RID: 27679 RVA: 0x000A3D60 File Offset: 0x000A1F60
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170046DF RID: 18143
			// (set) Token: 0x06006C20 RID: 27680 RVA: 0x000A3D73 File Offset: 0x000A1F73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170046E0 RID: 18144
			// (set) Token: 0x06006C21 RID: 27681 RVA: 0x000A3D8B File Offset: 0x000A1F8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170046E1 RID: 18145
			// (set) Token: 0x06006C22 RID: 27682 RVA: 0x000A3DA3 File Offset: 0x000A1FA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170046E2 RID: 18146
			// (set) Token: 0x06006C23 RID: 27683 RVA: 0x000A3DBB File Offset: 0x000A1FBB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170046E3 RID: 18147
			// (set) Token: 0x06006C24 RID: 27684 RVA: 0x000A3DD3 File Offset: 0x000A1FD3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200087D RID: 2173
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170046E4 RID: 18148
			// (set) Token: 0x06006C26 RID: 27686 RVA: 0x000A3DF3 File Offset: 0x000A1FF3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170046E5 RID: 18149
			// (set) Token: 0x06006C27 RID: 27687 RVA: 0x000A3E11 File Offset: 0x000A2011
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170046E6 RID: 18150
			// (set) Token: 0x06006C28 RID: 27688 RVA: 0x000A3E29 File Offset: 0x000A2029
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170046E7 RID: 18151
			// (set) Token: 0x06006C29 RID: 27689 RVA: 0x000A3E41 File Offset: 0x000A2041
			public virtual SwitchParameter ForceSettingGlobal
			{
				set
				{
					base.PowerSharpParameters["ForceSettingGlobal"] = value;
				}
			}

			// Token: 0x170046E8 RID: 18152
			// (set) Token: 0x06006C2A RID: 27690 RVA: 0x000A3E59 File Offset: 0x000A2059
			public virtual Unlimited<uint>? AnonymousMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046E9 RID: 18153
			// (set) Token: 0x06006C2B RID: 27691 RVA: 0x000A3E71 File Offset: 0x000A2071
			public virtual Unlimited<uint>? AnonymousMaxBurst
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxBurst"] = value;
				}
			}

			// Token: 0x170046EA RID: 18154
			// (set) Token: 0x06006C2C RID: 27692 RVA: 0x000A3E89 File Offset: 0x000A2089
			public virtual Unlimited<uint>? AnonymousRechargeRate
			{
				set
				{
					base.PowerSharpParameters["AnonymousRechargeRate"] = value;
				}
			}

			// Token: 0x170046EB RID: 18155
			// (set) Token: 0x06006C2D RID: 27693 RVA: 0x000A3EA1 File Offset: 0x000A20A1
			public virtual Unlimited<uint>? AnonymousCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["AnonymousCutoffBalance"] = value;
				}
			}

			// Token: 0x170046EC RID: 18156
			// (set) Token: 0x06006C2E RID: 27694 RVA: 0x000A3EB9 File Offset: 0x000A20B9
			public virtual Unlimited<uint>? EasMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EasMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046ED RID: 18157
			// (set) Token: 0x06006C2F RID: 27695 RVA: 0x000A3ED1 File Offset: 0x000A20D1
			public virtual Unlimited<uint>? EasMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EasMaxBurst"] = value;
				}
			}

			// Token: 0x170046EE RID: 18158
			// (set) Token: 0x06006C30 RID: 27696 RVA: 0x000A3EE9 File Offset: 0x000A20E9
			public virtual Unlimited<uint>? EasRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EasRechargeRate"] = value;
				}
			}

			// Token: 0x170046EF RID: 18159
			// (set) Token: 0x06006C31 RID: 27697 RVA: 0x000A3F01 File Offset: 0x000A2101
			public virtual Unlimited<uint>? EasCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EasCutoffBalance"] = value;
				}
			}

			// Token: 0x170046F0 RID: 18160
			// (set) Token: 0x06006C32 RID: 27698 RVA: 0x000A3F19 File Offset: 0x000A2119
			public virtual Unlimited<uint>? EasMaxDevices
			{
				set
				{
					base.PowerSharpParameters["EasMaxDevices"] = value;
				}
			}

			// Token: 0x170046F1 RID: 18161
			// (set) Token: 0x06006C33 RID: 27699 RVA: 0x000A3F31 File Offset: 0x000A2131
			public virtual Unlimited<uint>? EasMaxDeviceDeletesPerMonth
			{
				set
				{
					base.PowerSharpParameters["EasMaxDeviceDeletesPerMonth"] = value;
				}
			}

			// Token: 0x170046F2 RID: 18162
			// (set) Token: 0x06006C34 RID: 27700 RVA: 0x000A3F49 File Offset: 0x000A2149
			public virtual Unlimited<uint>? EasMaxInactivityForDeviceCleanup
			{
				set
				{
					base.PowerSharpParameters["EasMaxInactivityForDeviceCleanup"] = value;
				}
			}

			// Token: 0x170046F3 RID: 18163
			// (set) Token: 0x06006C35 RID: 27701 RVA: 0x000A3F61 File Offset: 0x000A2161
			public virtual Unlimited<uint>? EwsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EwsMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046F4 RID: 18164
			// (set) Token: 0x06006C36 RID: 27702 RVA: 0x000A3F79 File Offset: 0x000A2179
			public virtual Unlimited<uint>? EwsMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EwsMaxBurst"] = value;
				}
			}

			// Token: 0x170046F5 RID: 18165
			// (set) Token: 0x06006C37 RID: 27703 RVA: 0x000A3F91 File Offset: 0x000A2191
			public virtual Unlimited<uint>? EwsRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EwsRechargeRate"] = value;
				}
			}

			// Token: 0x170046F6 RID: 18166
			// (set) Token: 0x06006C38 RID: 27704 RVA: 0x000A3FA9 File Offset: 0x000A21A9
			public virtual Unlimited<uint>? EwsCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EwsCutoffBalance"] = value;
				}
			}

			// Token: 0x170046F7 RID: 18167
			// (set) Token: 0x06006C39 RID: 27705 RVA: 0x000A3FC1 File Offset: 0x000A21C1
			public virtual Unlimited<uint>? EwsMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["EwsMaxSubscriptions"] = value;
				}
			}

			// Token: 0x170046F8 RID: 18168
			// (set) Token: 0x06006C3A RID: 27706 RVA: 0x000A3FD9 File Offset: 0x000A21D9
			public virtual Unlimited<uint>? ImapMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["ImapMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046F9 RID: 18169
			// (set) Token: 0x06006C3B RID: 27707 RVA: 0x000A3FF1 File Offset: 0x000A21F1
			public virtual Unlimited<uint>? ImapMaxBurst
			{
				set
				{
					base.PowerSharpParameters["ImapMaxBurst"] = value;
				}
			}

			// Token: 0x170046FA RID: 18170
			// (set) Token: 0x06006C3C RID: 27708 RVA: 0x000A4009 File Offset: 0x000A2209
			public virtual Unlimited<uint>? ImapRechargeRate
			{
				set
				{
					base.PowerSharpParameters["ImapRechargeRate"] = value;
				}
			}

			// Token: 0x170046FB RID: 18171
			// (set) Token: 0x06006C3D RID: 27709 RVA: 0x000A4021 File Offset: 0x000A2221
			public virtual Unlimited<uint>? ImapCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["ImapCutoffBalance"] = value;
				}
			}

			// Token: 0x170046FC RID: 18172
			// (set) Token: 0x06006C3E RID: 27710 RVA: 0x000A4039 File Offset: 0x000A2239
			public virtual Unlimited<uint>? OutlookServiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x170046FD RID: 18173
			// (set) Token: 0x06006C3F RID: 27711 RVA: 0x000A4051 File Offset: 0x000A2251
			public virtual Unlimited<uint>? OutlookServiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxBurst"] = value;
				}
			}

			// Token: 0x170046FE RID: 18174
			// (set) Token: 0x06006C40 RID: 27712 RVA: 0x000A4069 File Offset: 0x000A2269
			public virtual Unlimited<uint>? OutlookServiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceRechargeRate"] = value;
				}
			}

			// Token: 0x170046FF RID: 18175
			// (set) Token: 0x06006C41 RID: 27713 RVA: 0x000A4081 File Offset: 0x000A2281
			public virtual Unlimited<uint>? OutlookServiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceCutoffBalance"] = value;
				}
			}

			// Token: 0x17004700 RID: 18176
			// (set) Token: 0x06006C42 RID: 27714 RVA: 0x000A4099 File Offset: 0x000A2299
			public virtual Unlimited<uint>? OutlookServiceMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSubscriptions"] = value;
				}
			}

			// Token: 0x17004701 RID: 18177
			// (set) Token: 0x06006C43 RID: 27715 RVA: 0x000A40B1 File Offset: 0x000A22B1
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerDevice"] = value;
				}
			}

			// Token: 0x17004702 RID: 18178
			// (set) Token: 0x06006C44 RID: 27716 RVA: 0x000A40C9 File Offset: 0x000A22C9
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerUser"] = value;
				}
			}

			// Token: 0x17004703 RID: 18179
			// (set) Token: 0x06006C45 RID: 27717 RVA: 0x000A40E1 File Offset: 0x000A22E1
			public virtual Unlimited<uint>? OwaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004704 RID: 18180
			// (set) Token: 0x06006C46 RID: 27718 RVA: 0x000A40F9 File Offset: 0x000A22F9
			public virtual Unlimited<uint>? OwaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaMaxBurst"] = value;
				}
			}

			// Token: 0x17004705 RID: 18181
			// (set) Token: 0x06006C47 RID: 27719 RVA: 0x000A4111 File Offset: 0x000A2311
			public virtual Unlimited<uint>? OwaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaRechargeRate"] = value;
				}
			}

			// Token: 0x17004706 RID: 18182
			// (set) Token: 0x06006C48 RID: 27720 RVA: 0x000A4129 File Offset: 0x000A2329
			public virtual Unlimited<uint>? OwaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaCutoffBalance"] = value;
				}
			}

			// Token: 0x17004707 RID: 18183
			// (set) Token: 0x06006C49 RID: 27721 RVA: 0x000A4141 File Offset: 0x000A2341
			public virtual Unlimited<uint>? OwaVoiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004708 RID: 18184
			// (set) Token: 0x06006C4A RID: 27722 RVA: 0x000A4159 File Offset: 0x000A2359
			public virtual Unlimited<uint>? OwaVoiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxBurst"] = value;
				}
			}

			// Token: 0x17004709 RID: 18185
			// (set) Token: 0x06006C4B RID: 27723 RVA: 0x000A4171 File Offset: 0x000A2371
			public virtual Unlimited<uint>? OwaVoiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceRechargeRate"] = value;
				}
			}

			// Token: 0x1700470A RID: 18186
			// (set) Token: 0x06006C4C RID: 27724 RVA: 0x000A4189 File Offset: 0x000A2389
			public virtual Unlimited<uint>? OwaVoiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceCutoffBalance"] = value;
				}
			}

			// Token: 0x1700470B RID: 18187
			// (set) Token: 0x06006C4D RID: 27725 RVA: 0x000A41A1 File Offset: 0x000A23A1
			public virtual Unlimited<uint>? EncryptionSenderMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700470C RID: 18188
			// (set) Token: 0x06006C4E RID: 27726 RVA: 0x000A41B9 File Offset: 0x000A23B9
			public virtual Unlimited<uint>? EncryptionSenderMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxBurst"] = value;
				}
			}

			// Token: 0x1700470D RID: 18189
			// (set) Token: 0x06006C4F RID: 27727 RVA: 0x000A41D1 File Offset: 0x000A23D1
			public virtual Unlimited<uint>? EncryptionSenderRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderRechargeRate"] = value;
				}
			}

			// Token: 0x1700470E RID: 18190
			// (set) Token: 0x06006C50 RID: 27728 RVA: 0x000A41E9 File Offset: 0x000A23E9
			public virtual Unlimited<uint>? EncryptionSenderCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderCutoffBalance"] = value;
				}
			}

			// Token: 0x1700470F RID: 18191
			// (set) Token: 0x06006C51 RID: 27729 RVA: 0x000A4201 File Offset: 0x000A2401
			public virtual Unlimited<uint>? EncryptionRecipientMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004710 RID: 18192
			// (set) Token: 0x06006C52 RID: 27730 RVA: 0x000A4219 File Offset: 0x000A2419
			public virtual Unlimited<uint>? EncryptionRecipientMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxBurst"] = value;
				}
			}

			// Token: 0x17004711 RID: 18193
			// (set) Token: 0x06006C53 RID: 27731 RVA: 0x000A4231 File Offset: 0x000A2431
			public virtual Unlimited<uint>? EncryptionRecipientRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientRechargeRate"] = value;
				}
			}

			// Token: 0x17004712 RID: 18194
			// (set) Token: 0x06006C54 RID: 27732 RVA: 0x000A4249 File Offset: 0x000A2449
			public virtual Unlimited<uint>? EncryptionRecipientCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientCutoffBalance"] = value;
				}
			}

			// Token: 0x17004713 RID: 18195
			// (set) Token: 0x06006C55 RID: 27733 RVA: 0x000A4261 File Offset: 0x000A2461
			public virtual Unlimited<uint>? PopMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PopMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004714 RID: 18196
			// (set) Token: 0x06006C56 RID: 27734 RVA: 0x000A4279 File Offset: 0x000A2479
			public virtual Unlimited<uint>? PopMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PopMaxBurst"] = value;
				}
			}

			// Token: 0x17004715 RID: 18197
			// (set) Token: 0x06006C57 RID: 27735 RVA: 0x000A4291 File Offset: 0x000A2491
			public virtual Unlimited<uint>? PopRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PopRechargeRate"] = value;
				}
			}

			// Token: 0x17004716 RID: 18198
			// (set) Token: 0x06006C58 RID: 27736 RVA: 0x000A42A9 File Offset: 0x000A24A9
			public virtual Unlimited<uint>? PopCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PopCutoffBalance"] = value;
				}
			}

			// Token: 0x17004717 RID: 18199
			// (set) Token: 0x06006C59 RID: 27737 RVA: 0x000A42C1 File Offset: 0x000A24C1
			public virtual Unlimited<uint>? PowerShellMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004718 RID: 18200
			// (set) Token: 0x06006C5A RID: 27738 RVA: 0x000A42D9 File Offset: 0x000A24D9
			public virtual Unlimited<uint>? PowerShellMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxBurst"] = value;
				}
			}

			// Token: 0x17004719 RID: 18201
			// (set) Token: 0x06006C5B RID: 27739 RVA: 0x000A42F1 File Offset: 0x000A24F1
			public virtual Unlimited<uint>? PowerShellRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PowerShellRechargeRate"] = value;
				}
			}

			// Token: 0x1700471A RID: 18202
			// (set) Token: 0x06006C5C RID: 27740 RVA: 0x000A4309 File Offset: 0x000A2509
			public virtual Unlimited<uint>? PowerShellCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PowerShellCutoffBalance"] = value;
				}
			}

			// Token: 0x1700471B RID: 18203
			// (set) Token: 0x06006C5D RID: 27741 RVA: 0x000A4321 File Offset: 0x000A2521
			public virtual Unlimited<uint>? PowerShellMaxTenantConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantConcurrency"] = value;
				}
			}

			// Token: 0x1700471C RID: 18204
			// (set) Token: 0x06006C5E RID: 27742 RVA: 0x000A4339 File Offset: 0x000A2539
			public virtual Unlimited<uint>? PowerShellMaxOperations
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxOperations"] = value;
				}
			}

			// Token: 0x1700471D RID: 18205
			// (set) Token: 0x06006C5F RID: 27743 RVA: 0x000A4351 File Offset: 0x000A2551
			public virtual Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x1700471E RID: 18206
			// (set) Token: 0x06006C60 RID: 27744 RVA: 0x000A4369 File Offset: 0x000A2569
			public virtual Unlimited<uint>? ExchangeMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaxCmdlets"] = value;
				}
			}

			// Token: 0x1700471F RID: 18207
			// (set) Token: 0x06006C61 RID: 27745 RVA: 0x000A4381 File Offset: 0x000A2581
			public virtual Unlimited<uint>? PowerShellMaxCmdletQueueDepth
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletQueueDepth"] = value;
				}
			}

			// Token: 0x17004720 RID: 18208
			// (set) Token: 0x06006C62 RID: 27746 RVA: 0x000A4399 File Offset: 0x000A2599
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdlets"] = value;
				}
			}

			// Token: 0x17004721 RID: 18209
			// (set) Token: 0x06006C63 RID: 27747 RVA: 0x000A43B1 File Offset: 0x000A25B1
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x17004722 RID: 18210
			// (set) Token: 0x06006C64 RID: 27748 RVA: 0x000A43C9 File Offset: 0x000A25C9
			public virtual Unlimited<uint>? PowerShellMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdlets"] = value;
				}
			}

			// Token: 0x17004723 RID: 18211
			// (set) Token: 0x06006C65 RID: 27749 RVA: 0x000A43E1 File Offset: 0x000A25E1
			public virtual Unlimited<uint>? PowerShellMaxRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspaces"] = value;
				}
			}

			// Token: 0x17004724 RID: 18212
			// (set) Token: 0x06006C66 RID: 27750 RVA: 0x000A43F9 File Offset: 0x000A25F9
			public virtual Unlimited<uint>? PowerShellMaxTenantRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantRunspaces"] = value;
				}
			}

			// Token: 0x17004725 RID: 18213
			// (set) Token: 0x06006C67 RID: 27751 RVA: 0x000A4411 File Offset: 0x000A2611
			public virtual Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspacesTimePeriod"] = value;
				}
			}

			// Token: 0x17004726 RID: 18214
			// (set) Token: 0x06006C68 RID: 27752 RVA: 0x000A4429 File Offset: 0x000A2629
			public virtual Unlimited<uint>? PswsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PswsMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004727 RID: 18215
			// (set) Token: 0x06006C69 RID: 27753 RVA: 0x000A4441 File Offset: 0x000A2641
			public virtual Unlimited<uint>? PswsMaxRequest
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequest"] = value;
				}
			}

			// Token: 0x17004728 RID: 18216
			// (set) Token: 0x06006C6A RID: 27754 RVA: 0x000A4459 File Offset: 0x000A2659
			public virtual Unlimited<uint>? PswsMaxRequestTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequestTimePeriod"] = value;
				}
			}

			// Token: 0x17004729 RID: 18217
			// (set) Token: 0x06006C6B RID: 27755 RVA: 0x000A4471 File Offset: 0x000A2671
			public virtual Unlimited<uint>? RcaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["RcaMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700472A RID: 18218
			// (set) Token: 0x06006C6C RID: 27756 RVA: 0x000A4489 File Offset: 0x000A2689
			public virtual Unlimited<uint>? RcaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["RcaMaxBurst"] = value;
				}
			}

			// Token: 0x1700472B RID: 18219
			// (set) Token: 0x06006C6D RID: 27757 RVA: 0x000A44A1 File Offset: 0x000A26A1
			public virtual Unlimited<uint>? RcaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["RcaRechargeRate"] = value;
				}
			}

			// Token: 0x1700472C RID: 18220
			// (set) Token: 0x06006C6E RID: 27758 RVA: 0x000A44B9 File Offset: 0x000A26B9
			public virtual Unlimited<uint>? RcaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["RcaCutoffBalance"] = value;
				}
			}

			// Token: 0x1700472D RID: 18221
			// (set) Token: 0x06006C6F RID: 27759 RVA: 0x000A44D1 File Offset: 0x000A26D1
			public virtual Unlimited<uint>? CpaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["CpaMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700472E RID: 18222
			// (set) Token: 0x06006C70 RID: 27760 RVA: 0x000A44E9 File Offset: 0x000A26E9
			public virtual Unlimited<uint>? CpaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["CpaMaxBurst"] = value;
				}
			}

			// Token: 0x1700472F RID: 18223
			// (set) Token: 0x06006C71 RID: 27761 RVA: 0x000A4501 File Offset: 0x000A2701
			public virtual Unlimited<uint>? CpaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["CpaRechargeRate"] = value;
				}
			}

			// Token: 0x17004730 RID: 18224
			// (set) Token: 0x06006C72 RID: 27762 RVA: 0x000A4519 File Offset: 0x000A2719
			public virtual Unlimited<uint>? CpaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["CpaCutoffBalance"] = value;
				}
			}

			// Token: 0x17004731 RID: 18225
			// (set) Token: 0x06006C73 RID: 27763 RVA: 0x000A4531 File Offset: 0x000A2731
			public virtual Unlimited<uint>? MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004732 RID: 18226
			// (set) Token: 0x06006C74 RID: 27764 RVA: 0x000A4549 File Offset: 0x000A2749
			public virtual Unlimited<uint>? RecipientRateLimit
			{
				set
				{
					base.PowerSharpParameters["RecipientRateLimit"] = value;
				}
			}

			// Token: 0x17004733 RID: 18227
			// (set) Token: 0x06006C75 RID: 27765 RVA: 0x000A4561 File Offset: 0x000A2761
			public virtual Unlimited<uint>? ForwardeeLimit
			{
				set
				{
					base.PowerSharpParameters["ForwardeeLimit"] = value;
				}
			}

			// Token: 0x17004734 RID: 18228
			// (set) Token: 0x06006C76 RID: 27766 RVA: 0x000A4579 File Offset: 0x000A2779
			public virtual Unlimited<uint>? DiscoveryMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004735 RID: 18229
			// (set) Token: 0x06006C77 RID: 27767 RVA: 0x000A4591 File Offset: 0x000A2791
			public virtual Unlimited<uint>? DiscoveryMaxMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxMailboxes"] = value;
				}
			}

			// Token: 0x17004736 RID: 18230
			// (set) Token: 0x06006C78 RID: 27768 RVA: 0x000A45A9 File Offset: 0x000A27A9
			public virtual Unlimited<uint>? DiscoveryMaxKeywords
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywords"] = value;
				}
			}

			// Token: 0x17004737 RID: 18231
			// (set) Token: 0x06006C79 RID: 27769 RVA: 0x000A45C1 File Offset: 0x000A27C1
			public virtual Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxPreviewSearchMailboxes"] = value;
				}
			}

			// Token: 0x17004738 RID: 18232
			// (set) Token: 0x06006C7A RID: 27770 RVA: 0x000A45D9 File Offset: 0x000A27D9
			public virtual Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxStatsSearchMailboxes"] = value;
				}
			}

			// Token: 0x17004739 RID: 18233
			// (set) Token: 0x06006C7B RID: 27771 RVA: 0x000A45F1 File Offset: 0x000A27F1
			public virtual Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
			{
				set
				{
					base.PowerSharpParameters["DiscoveryPreviewSearchResultsPageSize"] = value;
				}
			}

			// Token: 0x1700473A RID: 18234
			// (set) Token: 0x06006C7C RID: 27772 RVA: 0x000A4609 File Offset: 0x000A2809
			public virtual Unlimited<uint>? DiscoveryMaxKeywordsPerPage
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywordsPerPage"] = value;
				}
			}

			// Token: 0x1700473B RID: 18235
			// (set) Token: 0x06006C7D RID: 27773 RVA: 0x000A4621 File Offset: 0x000A2821
			public virtual Unlimited<uint>? DiscoveryMaxRefinerResults
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxRefinerResults"] = value;
				}
			}

			// Token: 0x1700473C RID: 18236
			// (set) Token: 0x06006C7E RID: 27774 RVA: 0x000A4639 File Offset: 0x000A2839
			public virtual Unlimited<uint>? DiscoveryMaxSearchQueueDepth
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxSearchQueueDepth"] = value;
				}
			}

			// Token: 0x1700473D RID: 18237
			// (set) Token: 0x06006C7F RID: 27775 RVA: 0x000A4651 File Offset: 0x000A2851
			public virtual Unlimited<uint>? DiscoverySearchTimeoutPeriod
			{
				set
				{
					base.PowerSharpParameters["DiscoverySearchTimeoutPeriod"] = value;
				}
			}

			// Token: 0x1700473E RID: 18238
			// (set) Token: 0x06006C80 RID: 27776 RVA: 0x000A4669 File Offset: 0x000A2869
			public virtual Unlimited<uint>? ComplianceMaxExpansionDGRecipients
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionDGRecipients"] = value;
				}
			}

			// Token: 0x1700473F RID: 18239
			// (set) Token: 0x06006C81 RID: 27777 RVA: 0x000A4681 File Offset: 0x000A2881
			public virtual Unlimited<uint>? ComplianceMaxExpansionNestedDGs
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionNestedDGs"] = value;
				}
			}

			// Token: 0x17004740 RID: 18240
			// (set) Token: 0x06006C82 RID: 27778 RVA: 0x000A4699 File Offset: 0x000A2899
			public virtual Unlimited<uint>? PushNotificationMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004741 RID: 18241
			// (set) Token: 0x06006C83 RID: 27779 RVA: 0x000A46B1 File Offset: 0x000A28B1
			public virtual Unlimited<uint>? PushNotificationMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurst"] = value;
				}
			}

			// Token: 0x17004742 RID: 18242
			// (set) Token: 0x06006C84 RID: 27780 RVA: 0x000A46C9 File Offset: 0x000A28C9
			public virtual Unlimited<uint>? PushNotificationRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRate"] = value;
				}
			}

			// Token: 0x17004743 RID: 18243
			// (set) Token: 0x06006C85 RID: 27781 RVA: 0x000A46E1 File Offset: 0x000A28E1
			public virtual Unlimited<uint>? PushNotificationCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PushNotificationCutoffBalance"] = value;
				}
			}

			// Token: 0x17004744 RID: 18244
			// (set) Token: 0x06006C86 RID: 27782 RVA: 0x000A46F9 File Offset: 0x000A28F9
			public virtual Unlimited<uint>? PushNotificationMaxBurstPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurstPerDevice"] = value;
				}
			}

			// Token: 0x17004745 RID: 18245
			// (set) Token: 0x06006C87 RID: 27783 RVA: 0x000A4711 File Offset: 0x000A2911
			public virtual Unlimited<uint>? PushNotificationRechargeRatePerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRatePerDevice"] = value;
				}
			}

			// Token: 0x17004746 RID: 18246
			// (set) Token: 0x06006C88 RID: 27784 RVA: 0x000A4729 File Offset: 0x000A2929
			public virtual Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationSamplingPeriodPerDevice"] = value;
				}
			}

			// Token: 0x17004747 RID: 18247
			// (set) Token: 0x06006C89 RID: 27785 RVA: 0x000A4741 File Offset: 0x000A2941
			public virtual SwitchParameter IsServiceAccount
			{
				set
				{
					base.PowerSharpParameters["IsServiceAccount"] = value;
				}
			}

			// Token: 0x17004748 RID: 18248
			// (set) Token: 0x06006C8A RID: 27786 RVA: 0x000A4759 File Offset: 0x000A2959
			public virtual ThrottlingPolicyScopeType ThrottlingPolicyScope
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicyScope"] = value;
				}
			}

			// Token: 0x17004749 RID: 18249
			// (set) Token: 0x06006C8B RID: 27787 RVA: 0x000A4771 File Offset: 0x000A2971
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700474A RID: 18250
			// (set) Token: 0x06006C8C RID: 27788 RVA: 0x000A4784 File Offset: 0x000A2984
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700474B RID: 18251
			// (set) Token: 0x06006C8D RID: 27789 RVA: 0x000A4797 File Offset: 0x000A2997
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700474C RID: 18252
			// (set) Token: 0x06006C8E RID: 27790 RVA: 0x000A47AF File Offset: 0x000A29AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700474D RID: 18253
			// (set) Token: 0x06006C8F RID: 27791 RVA: 0x000A47C7 File Offset: 0x000A29C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700474E RID: 18254
			// (set) Token: 0x06006C90 RID: 27792 RVA: 0x000A47DF File Offset: 0x000A29DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700474F RID: 18255
			// (set) Token: 0x06006C91 RID: 27793 RVA: 0x000A47F7 File Offset: 0x000A29F7
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
