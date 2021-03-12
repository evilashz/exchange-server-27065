using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200046D RID: 1133
	public class GetTextMessagingAccountCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600409D RID: 16541 RVA: 0x0006B9BE File Offset: 0x00069BBE
		private GetTextMessagingAccountCommand() : base("Get-TextMessagingAccount")
		{
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0006B9CB File Offset: 0x00069BCB
		public GetTextMessagingAccountCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0006B9DA File Offset: 0x00069BDA
		public virtual GetTextMessagingAccountCommand SetParameters(GetTextMessagingAccountCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0006B9E4 File Offset: 0x00069BE4
		public virtual GetTextMessagingAccountCommand SetParameters(GetTextMessagingAccountCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200046E RID: 1134
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700237C RID: 9084
			// (set) Token: 0x060040A1 RID: 16545 RVA: 0x0006B9EE File Offset: 0x00069BEE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700237D RID: 9085
			// (set) Token: 0x060040A2 RID: 16546 RVA: 0x0006BA0C File Offset: 0x00069C0C
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700237E RID: 9086
			// (set) Token: 0x060040A3 RID: 16547 RVA: 0x0006BA1F File Offset: 0x00069C1F
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700237F RID: 9087
			// (set) Token: 0x060040A4 RID: 16548 RVA: 0x0006BA37 File Offset: 0x00069C37
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002380 RID: 9088
			// (set) Token: 0x060040A5 RID: 16549 RVA: 0x0006BA4F File Offset: 0x00069C4F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002381 RID: 9089
			// (set) Token: 0x060040A6 RID: 16550 RVA: 0x0006BA62 File Offset: 0x00069C62
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002382 RID: 9090
			// (set) Token: 0x060040A7 RID: 16551 RVA: 0x0006BA7A File Offset: 0x00069C7A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002383 RID: 9091
			// (set) Token: 0x060040A8 RID: 16552 RVA: 0x0006BA92 File Offset: 0x00069C92
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002384 RID: 9092
			// (set) Token: 0x060040A9 RID: 16553 RVA: 0x0006BAAA File Offset: 0x00069CAA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200046F RID: 1135
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002385 RID: 9093
			// (set) Token: 0x060040AB RID: 16555 RVA: 0x0006BACA File Offset: 0x00069CCA
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002386 RID: 9094
			// (set) Token: 0x060040AC RID: 16556 RVA: 0x0006BADD File Offset: 0x00069CDD
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002387 RID: 9095
			// (set) Token: 0x060040AD RID: 16557 RVA: 0x0006BAF5 File Offset: 0x00069CF5
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002388 RID: 9096
			// (set) Token: 0x060040AE RID: 16558 RVA: 0x0006BB0D File Offset: 0x00069D0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002389 RID: 9097
			// (set) Token: 0x060040AF RID: 16559 RVA: 0x0006BB20 File Offset: 0x00069D20
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700238A RID: 9098
			// (set) Token: 0x060040B0 RID: 16560 RVA: 0x0006BB38 File Offset: 0x00069D38
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700238B RID: 9099
			// (set) Token: 0x060040B1 RID: 16561 RVA: 0x0006BB50 File Offset: 0x00069D50
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700238C RID: 9100
			// (set) Token: 0x060040B2 RID: 16562 RVA: 0x0006BB68 File Offset: 0x00069D68
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
