using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001B8 RID: 440
	public class GetMobileDeviceMailboxPolicyCommand : SyntheticCommandWithPipelineInput<MobileMailboxPolicy, MobileMailboxPolicy>
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x000480C2 File Offset: 0x000462C2
		private GetMobileDeviceMailboxPolicyCommand() : base("Get-MobileDeviceMailboxPolicy")
		{
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000480CF File Offset: 0x000462CF
		public GetMobileDeviceMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000480DE File Offset: 0x000462DE
		public virtual GetMobileDeviceMailboxPolicyCommand SetParameters(GetMobileDeviceMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000480E8 File Offset: 0x000462E8
		public virtual GetMobileDeviceMailboxPolicyCommand SetParameters(GetMobileDeviceMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001B9 RID: 441
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D9D RID: 3485
			// (set) Token: 0x06002558 RID: 9560 RVA: 0x000480F2 File Offset: 0x000462F2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D9E RID: 3486
			// (set) Token: 0x06002559 RID: 9561 RVA: 0x00048110 File Offset: 0x00046310
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D9F RID: 3487
			// (set) Token: 0x0600255A RID: 9562 RVA: 0x00048123 File Offset: 0x00046323
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000DA0 RID: 3488
			// (set) Token: 0x0600255B RID: 9563 RVA: 0x0004813B File Offset: 0x0004633B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000DA1 RID: 3489
			// (set) Token: 0x0600255C RID: 9564 RVA: 0x00048153 File Offset: 0x00046353
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000DA2 RID: 3490
			// (set) Token: 0x0600255D RID: 9565 RVA: 0x0004816B File Offset: 0x0004636B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001BA RID: 442
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000DA3 RID: 3491
			// (set) Token: 0x0600255F RID: 9567 RVA: 0x0004818B File Offset: 0x0004638B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000DA4 RID: 3492
			// (set) Token: 0x06002560 RID: 9568 RVA: 0x000481A9 File Offset: 0x000463A9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000DA5 RID: 3493
			// (set) Token: 0x06002561 RID: 9569 RVA: 0x000481C7 File Offset: 0x000463C7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000DA6 RID: 3494
			// (set) Token: 0x06002562 RID: 9570 RVA: 0x000481DA File Offset: 0x000463DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000DA7 RID: 3495
			// (set) Token: 0x06002563 RID: 9571 RVA: 0x000481F2 File Offset: 0x000463F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000DA8 RID: 3496
			// (set) Token: 0x06002564 RID: 9572 RVA: 0x0004820A File Offset: 0x0004640A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000DA9 RID: 3497
			// (set) Token: 0x06002565 RID: 9573 RVA: 0x00048222 File Offset: 0x00046422
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
