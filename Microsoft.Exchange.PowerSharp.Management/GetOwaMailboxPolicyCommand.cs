using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001B0 RID: 432
	public class GetOwaMailboxPolicyCommand : SyntheticCommandWithPipelineInput<OwaMailboxPolicy, OwaMailboxPolicy>
	{
		// Token: 0x060024E7 RID: 9447 RVA: 0x00047766 File Offset: 0x00045966
		private GetOwaMailboxPolicyCommand() : base("Get-OwaMailboxPolicy")
		{
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00047773 File Offset: 0x00045973
		public GetOwaMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00047782 File Offset: 0x00045982
		public virtual GetOwaMailboxPolicyCommand SetParameters(GetOwaMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0004778C File Offset: 0x0004598C
		public virtual GetOwaMailboxPolicyCommand SetParameters(GetOwaMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001B1 RID: 433
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D40 RID: 3392
			// (set) Token: 0x060024EB RID: 9451 RVA: 0x00047796 File Offset: 0x00045996
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D41 RID: 3393
			// (set) Token: 0x060024EC RID: 9452 RVA: 0x000477B4 File Offset: 0x000459B4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D42 RID: 3394
			// (set) Token: 0x060024ED RID: 9453 RVA: 0x000477C7 File Offset: 0x000459C7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D43 RID: 3395
			// (set) Token: 0x060024EE RID: 9454 RVA: 0x000477DF File Offset: 0x000459DF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D44 RID: 3396
			// (set) Token: 0x060024EF RID: 9455 RVA: 0x000477F7 File Offset: 0x000459F7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D45 RID: 3397
			// (set) Token: 0x060024F0 RID: 9456 RVA: 0x0004780F File Offset: 0x00045A0F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001B2 RID: 434
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000D46 RID: 3398
			// (set) Token: 0x060024F2 RID: 9458 RVA: 0x0004782F File Offset: 0x00045A2F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000D47 RID: 3399
			// (set) Token: 0x060024F3 RID: 9459 RVA: 0x0004784D File Offset: 0x00045A4D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D48 RID: 3400
			// (set) Token: 0x060024F4 RID: 9460 RVA: 0x0004786B File Offset: 0x00045A6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D49 RID: 3401
			// (set) Token: 0x060024F5 RID: 9461 RVA: 0x0004787E File Offset: 0x00045A7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D4A RID: 3402
			// (set) Token: 0x060024F6 RID: 9462 RVA: 0x00047896 File Offset: 0x00045A96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D4B RID: 3403
			// (set) Token: 0x060024F7 RID: 9463 RVA: 0x000478AE File Offset: 0x00045AAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D4C RID: 3404
			// (set) Token: 0x060024F8 RID: 9464 RVA: 0x000478C6 File Offset: 0x00045AC6
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
