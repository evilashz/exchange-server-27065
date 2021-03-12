using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004CD RID: 1229
	public class GetAddressBookPolicyCommand : SyntheticCommandWithPipelineInput<AddressBookMailboxPolicy, AddressBookMailboxPolicy>
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x00070462 File Offset: 0x0006E662
		private GetAddressBookPolicyCommand() : base("Get-AddressBookPolicy")
		{
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x0007046F File Offset: 0x0006E66F
		public GetAddressBookPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0007047E File Offset: 0x0006E67E
		public virtual GetAddressBookPolicyCommand SetParameters(GetAddressBookPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00070488 File Offset: 0x0006E688
		public virtual GetAddressBookPolicyCommand SetParameters(GetAddressBookPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004CE RID: 1230
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700266E RID: 9838
			// (set) Token: 0x06004453 RID: 17491 RVA: 0x00070492 File Offset: 0x0006E692
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700266F RID: 9839
			// (set) Token: 0x06004454 RID: 17492 RVA: 0x000704B0 File Offset: 0x0006E6B0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002670 RID: 9840
			// (set) Token: 0x06004455 RID: 17493 RVA: 0x000704C3 File Offset: 0x0006E6C3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002671 RID: 9841
			// (set) Token: 0x06004456 RID: 17494 RVA: 0x000704DB File Offset: 0x0006E6DB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002672 RID: 9842
			// (set) Token: 0x06004457 RID: 17495 RVA: 0x000704F3 File Offset: 0x0006E6F3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002673 RID: 9843
			// (set) Token: 0x06004458 RID: 17496 RVA: 0x0007050B File Offset: 0x0006E70B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004CF RID: 1231
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002674 RID: 9844
			// (set) Token: 0x0600445A RID: 17498 RVA: 0x0007052B File Offset: 0x0006E72B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002675 RID: 9845
			// (set) Token: 0x0600445B RID: 17499 RVA: 0x00070549 File Offset: 0x0006E749
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002676 RID: 9846
			// (set) Token: 0x0600445C RID: 17500 RVA: 0x00070567 File Offset: 0x0006E767
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002677 RID: 9847
			// (set) Token: 0x0600445D RID: 17501 RVA: 0x0007057A File Offset: 0x0006E77A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002678 RID: 9848
			// (set) Token: 0x0600445E RID: 17502 RVA: 0x00070592 File Offset: 0x0006E792
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002679 RID: 9849
			// (set) Token: 0x0600445F RID: 17503 RVA: 0x000705AA File Offset: 0x0006E7AA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700267A RID: 9850
			// (set) Token: 0x06004460 RID: 17504 RVA: 0x000705C2 File Offset: 0x0006E7C2
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
