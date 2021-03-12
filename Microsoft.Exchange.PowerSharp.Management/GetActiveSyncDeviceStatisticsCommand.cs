using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000037 RID: 55
	public class GetActiveSyncDeviceStatisticsCommand : SyntheticCommandWithPipelineInput<ActiveSyncDevice, ActiveSyncDevice>
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x0003431B File Offset: 0x0003251B
		private GetActiveSyncDeviceStatisticsCommand() : base("Get-ActiveSyncDeviceStatistics")
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00034328 File Offset: 0x00032528
		public GetActiveSyncDeviceStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00034337 File Offset: 0x00032537
		public virtual GetActiveSyncDeviceStatisticsCommand SetParameters(GetActiveSyncDeviceStatisticsCommand.MailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00034341 File Offset: 0x00032541
		public virtual GetActiveSyncDeviceStatisticsCommand SetParameters(GetActiveSyncDeviceStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0003434B File Offset: 0x0003254B
		public virtual GetActiveSyncDeviceStatisticsCommand SetParameters(GetActiveSyncDeviceStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000038 RID: 56
		public class MailboxParameters : ParametersBase
		{
			// Token: 0x1700013B RID: 315
			// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00034355 File Offset: 0x00032555
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700013C RID: 316
			// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00034373 File Offset: 0x00032573
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x1700013D RID: 317
			// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0003438B File Offset: 0x0003258B
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x1700013E RID: 318
			// (set) Token: 0x060015F8 RID: 5624 RVA: 0x0003439E File Offset: 0x0003259E
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x1700013F RID: 319
			// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000343B6 File Offset: 0x000325B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000140 RID: 320
			// (set) Token: 0x060015FA RID: 5626 RVA: 0x000343C9 File Offset: 0x000325C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000141 RID: 321
			// (set) Token: 0x060015FB RID: 5627 RVA: 0x000343E1 File Offset: 0x000325E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000142 RID: 322
			// (set) Token: 0x060015FC RID: 5628 RVA: 0x000343F9 File Offset: 0x000325F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000143 RID: 323
			// (set) Token: 0x060015FD RID: 5629 RVA: 0x00034411 File Offset: 0x00032611
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000039 RID: 57
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000144 RID: 324
			// (set) Token: 0x060015FF RID: 5631 RVA: 0x00034431 File Offset: 0x00032631
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x17000145 RID: 325
			// (set) Token: 0x06001600 RID: 5632 RVA: 0x00034449 File Offset: 0x00032649
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x17000146 RID: 326
			// (set) Token: 0x06001601 RID: 5633 RVA: 0x0003445C File Offset: 0x0003265C
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x17000147 RID: 327
			// (set) Token: 0x06001602 RID: 5634 RVA: 0x00034474 File Offset: 0x00032674
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000148 RID: 328
			// (set) Token: 0x06001603 RID: 5635 RVA: 0x00034487 File Offset: 0x00032687
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000149 RID: 329
			// (set) Token: 0x06001604 RID: 5636 RVA: 0x0003449F File Offset: 0x0003269F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700014A RID: 330
			// (set) Token: 0x06001605 RID: 5637 RVA: 0x000344B7 File Offset: 0x000326B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700014B RID: 331
			// (set) Token: 0x06001606 RID: 5638 RVA: 0x000344CF File Offset: 0x000326CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200003A RID: 58
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700014C RID: 332
			// (set) Token: 0x06001608 RID: 5640 RVA: 0x000344EF File Offset: 0x000326EF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceIdParameter(value) : null);
				}
			}

			// Token: 0x1700014D RID: 333
			// (set) Token: 0x06001609 RID: 5641 RVA: 0x0003450D File Offset: 0x0003270D
			public virtual SwitchParameter GetMailboxLog
			{
				set
				{
					base.PowerSharpParameters["GetMailboxLog"] = value;
				}
			}

			// Token: 0x1700014E RID: 334
			// (set) Token: 0x0600160A RID: 5642 RVA: 0x00034525 File Offset: 0x00032725
			public virtual MultiValuedProperty<string> NotificationEmailAddresses
			{
				set
				{
					base.PowerSharpParameters["NotificationEmailAddresses"] = value;
				}
			}

			// Token: 0x1700014F RID: 335
			// (set) Token: 0x0600160B RID: 5643 RVA: 0x00034538 File Offset: 0x00032738
			public virtual SwitchParameter ShowRecoveryPassword
			{
				set
				{
					base.PowerSharpParameters["ShowRecoveryPassword"] = value;
				}
			}

			// Token: 0x17000150 RID: 336
			// (set) Token: 0x0600160C RID: 5644 RVA: 0x00034550 File Offset: 0x00032750
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000151 RID: 337
			// (set) Token: 0x0600160D RID: 5645 RVA: 0x00034563 File Offset: 0x00032763
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000152 RID: 338
			// (set) Token: 0x0600160E RID: 5646 RVA: 0x0003457B File Offset: 0x0003277B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000153 RID: 339
			// (set) Token: 0x0600160F RID: 5647 RVA: 0x00034593 File Offset: 0x00032793
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000154 RID: 340
			// (set) Token: 0x06001610 RID: 5648 RVA: 0x000345AB File Offset: 0x000327AB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
