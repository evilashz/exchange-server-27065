using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001BE RID: 446
	public class NewOwaMailboxPolicyCommand : SyntheticCommandWithPipelineInput<OwaMailboxPolicy, OwaMailboxPolicy>
	{
		// Token: 0x060025EA RID: 9706 RVA: 0x00048E18 File Offset: 0x00047018
		private NewOwaMailboxPolicyCommand() : base("New-OwaMailboxPolicy")
		{
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x00048E25 File Offset: 0x00047025
		public NewOwaMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00048E34 File Offset: 0x00047034
		public virtual NewOwaMailboxPolicyCommand SetParameters(NewOwaMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001BF RID: 447
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E27 RID: 3623
			// (set) Token: 0x060025ED RID: 9709 RVA: 0x00048E3E File Offset: 0x0004703E
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000E28 RID: 3624
			// (set) Token: 0x060025EE RID: 9710 RVA: 0x00048E56 File Offset: 0x00047056
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000E29 RID: 3625
			// (set) Token: 0x060025EF RID: 9711 RVA: 0x00048E74 File Offset: 0x00047074
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E2A RID: 3626
			// (set) Token: 0x060025F0 RID: 9712 RVA: 0x00048E87 File Offset: 0x00047087
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E2B RID: 3627
			// (set) Token: 0x060025F1 RID: 9713 RVA: 0x00048E9A File Offset: 0x0004709A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E2C RID: 3628
			// (set) Token: 0x060025F2 RID: 9714 RVA: 0x00048EB2 File Offset: 0x000470B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E2D RID: 3629
			// (set) Token: 0x060025F3 RID: 9715 RVA: 0x00048ECA File Offset: 0x000470CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E2E RID: 3630
			// (set) Token: 0x060025F4 RID: 9716 RVA: 0x00048EE2 File Offset: 0x000470E2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E2F RID: 3631
			// (set) Token: 0x060025F5 RID: 9717 RVA: 0x00048EFA File Offset: 0x000470FA
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
