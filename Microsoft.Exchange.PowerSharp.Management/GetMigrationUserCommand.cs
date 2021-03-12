using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000268 RID: 616
	public class GetMigrationUserCommand : SyntheticCommandWithPipelineInput<MigrationUser, MigrationUser>
	{
		// Token: 0x06002D20 RID: 11552 RVA: 0x0005259A File Offset: 0x0005079A
		private GetMigrationUserCommand() : base("Get-MigrationUser")
		{
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000525A7 File Offset: 0x000507A7
		public GetMigrationUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000525B6 File Offset: 0x000507B6
		public virtual GetMigrationUserCommand SetParameters(GetMigrationUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000525C0 File Offset: 0x000507C0
		public virtual GetMigrationUserCommand SetParameters(GetMigrationUserCommand.MailboxGuidParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000525CA File Offset: 0x000507CA
		public virtual GetMigrationUserCommand SetParameters(GetMigrationUserCommand.StatusAndBatchIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000525D4 File Offset: 0x000507D4
		public virtual GetMigrationUserCommand SetParameters(GetMigrationUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000269 RID: 617
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001409 RID: 5129
			// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000525DE File Offset: 0x000507DE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700140A RID: 5130
			// (set) Token: 0x06002D27 RID: 11559 RVA: 0x000525F6 File Offset: 0x000507F6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700140B RID: 5131
			// (set) Token: 0x06002D28 RID: 11560 RVA: 0x00052614 File Offset: 0x00050814
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700140C RID: 5132
			// (set) Token: 0x06002D29 RID: 11561 RVA: 0x00052632 File Offset: 0x00050832
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700140D RID: 5133
			// (set) Token: 0x06002D2A RID: 11562 RVA: 0x00052645 File Offset: 0x00050845
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700140E RID: 5134
			// (set) Token: 0x06002D2B RID: 11563 RVA: 0x0005265D File Offset: 0x0005085D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700140F RID: 5135
			// (set) Token: 0x06002D2C RID: 11564 RVA: 0x00052675 File Offset: 0x00050875
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001410 RID: 5136
			// (set) Token: 0x06002D2D RID: 11565 RVA: 0x0005268D File Offset: 0x0005088D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200026A RID: 618
		public class MailboxGuidParameters : ParametersBase
		{
			// Token: 0x17001411 RID: 5137
			// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000526AD File Offset: 0x000508AD
			public virtual Guid? MailboxGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxGuid"] = value;
				}
			}

			// Token: 0x17001412 RID: 5138
			// (set) Token: 0x06002D30 RID: 11568 RVA: 0x000526C5 File Offset: 0x000508C5
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001413 RID: 5139
			// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000526DD File Offset: 0x000508DD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001414 RID: 5140
			// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000526FB File Offset: 0x000508FB
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001415 RID: 5141
			// (set) Token: 0x06002D33 RID: 11571 RVA: 0x00052719 File Offset: 0x00050919
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001416 RID: 5142
			// (set) Token: 0x06002D34 RID: 11572 RVA: 0x0005272C File Offset: 0x0005092C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001417 RID: 5143
			// (set) Token: 0x06002D35 RID: 11573 RVA: 0x00052744 File Offset: 0x00050944
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001418 RID: 5144
			// (set) Token: 0x06002D36 RID: 11574 RVA: 0x0005275C File Offset: 0x0005095C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001419 RID: 5145
			// (set) Token: 0x06002D37 RID: 11575 RVA: 0x00052774 File Offset: 0x00050974
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200026B RID: 619
		public class StatusAndBatchIdParameters : ParametersBase
		{
			// Token: 0x1700141A RID: 5146
			// (set) Token: 0x06002D39 RID: 11577 RVA: 0x00052794 File Offset: 0x00050994
			public virtual string BatchId
			{
				set
				{
					base.PowerSharpParameters["BatchId"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x1700141B RID: 5147
			// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000527B2 File Offset: 0x000509B2
			public virtual MigrationUserStatus? Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x1700141C RID: 5148
			// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000527CA File Offset: 0x000509CA
			public virtual MigrationUserStatusSummary? StatusSummary
			{
				set
				{
					base.PowerSharpParameters["StatusSummary"] = value;
				}
			}

			// Token: 0x1700141D RID: 5149
			// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000527E2 File Offset: 0x000509E2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700141E RID: 5150
			// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000527FA File Offset: 0x000509FA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700141F RID: 5151
			// (set) Token: 0x06002D3E RID: 11582 RVA: 0x00052818 File Offset: 0x00050A18
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001420 RID: 5152
			// (set) Token: 0x06002D3F RID: 11583 RVA: 0x00052836 File Offset: 0x00050A36
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001421 RID: 5153
			// (set) Token: 0x06002D40 RID: 11584 RVA: 0x00052849 File Offset: 0x00050A49
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001422 RID: 5154
			// (set) Token: 0x06002D41 RID: 11585 RVA: 0x00052861 File Offset: 0x00050A61
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001423 RID: 5155
			// (set) Token: 0x06002D42 RID: 11586 RVA: 0x00052879 File Offset: 0x00050A79
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001424 RID: 5156
			// (set) Token: 0x06002D43 RID: 11587 RVA: 0x00052891 File Offset: 0x00050A91
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200026C RID: 620
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001425 RID: 5157
			// (set) Token: 0x06002D45 RID: 11589 RVA: 0x000528B1 File Offset: 0x00050AB1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationUserIdParameter(value) : null);
				}
			}

			// Token: 0x17001426 RID: 5158
			// (set) Token: 0x06002D46 RID: 11590 RVA: 0x000528CF File Offset: 0x00050ACF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001427 RID: 5159
			// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000528E7 File Offset: 0x00050AE7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001428 RID: 5160
			// (set) Token: 0x06002D48 RID: 11592 RVA: 0x00052905 File Offset: 0x00050B05
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001429 RID: 5161
			// (set) Token: 0x06002D49 RID: 11593 RVA: 0x00052923 File Offset: 0x00050B23
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700142A RID: 5162
			// (set) Token: 0x06002D4A RID: 11594 RVA: 0x00052936 File Offset: 0x00050B36
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700142B RID: 5163
			// (set) Token: 0x06002D4B RID: 11595 RVA: 0x0005294E File Offset: 0x00050B4E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700142C RID: 5164
			// (set) Token: 0x06002D4C RID: 11596 RVA: 0x00052966 File Offset: 0x00050B66
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700142D RID: 5165
			// (set) Token: 0x06002D4D RID: 11597 RVA: 0x0005297E File Offset: 0x00050B7E
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
