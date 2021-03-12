using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001E9 RID: 489
	public class RemoveUMMailboxPolicyCommand : SyntheticCommandWithPipelineInput<UMMailboxPolicy, UMMailboxPolicy>
	{
		// Token: 0x060027E8 RID: 10216 RVA: 0x0004B896 File Offset: 0x00049A96
		private RemoveUMMailboxPolicyCommand() : base("Remove-UMMailboxPolicy")
		{
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0004B8A3 File Offset: 0x00049AA3
		public RemoveUMMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0004B8B2 File Offset: 0x00049AB2
		public virtual RemoveUMMailboxPolicyCommand SetParameters(RemoveUMMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0004B8BC File Offset: 0x00049ABC
		public virtual RemoveUMMailboxPolicyCommand SetParameters(RemoveUMMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001EA RID: 490
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000FCF RID: 4047
			// (set) Token: 0x060027EC RID: 10220 RVA: 0x0004B8C6 File Offset: 0x00049AC6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (set) Token: 0x060027ED RID: 10221 RVA: 0x0004B8D9 File Offset: 0x00049AD9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FD1 RID: 4049
			// (set) Token: 0x060027EE RID: 10222 RVA: 0x0004B8F1 File Offset: 0x00049AF1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FD2 RID: 4050
			// (set) Token: 0x060027EF RID: 10223 RVA: 0x0004B909 File Offset: 0x00049B09
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FD3 RID: 4051
			// (set) Token: 0x060027F0 RID: 10224 RVA: 0x0004B921 File Offset: 0x00049B21
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000FD4 RID: 4052
			// (set) Token: 0x060027F1 RID: 10225 RVA: 0x0004B939 File Offset: 0x00049B39
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000FD5 RID: 4053
			// (set) Token: 0x060027F2 RID: 10226 RVA: 0x0004B951 File Offset: 0x00049B51
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001EB RID: 491
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000FD6 RID: 4054
			// (set) Token: 0x060027F4 RID: 10228 RVA: 0x0004B971 File Offset: 0x00049B71
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000FD7 RID: 4055
			// (set) Token: 0x060027F5 RID: 10229 RVA: 0x0004B98F File Offset: 0x00049B8F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000FD8 RID: 4056
			// (set) Token: 0x060027F6 RID: 10230 RVA: 0x0004B9A2 File Offset: 0x00049BA2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000FD9 RID: 4057
			// (set) Token: 0x060027F7 RID: 10231 RVA: 0x0004B9BA File Offset: 0x00049BBA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000FDA RID: 4058
			// (set) Token: 0x060027F8 RID: 10232 RVA: 0x0004B9D2 File Offset: 0x00049BD2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000FDB RID: 4059
			// (set) Token: 0x060027F9 RID: 10233 RVA: 0x0004B9EA File Offset: 0x00049BEA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (set) Token: 0x060027FA RID: 10234 RVA: 0x0004BA02 File Offset: 0x00049C02
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000FDD RID: 4061
			// (set) Token: 0x060027FB RID: 10235 RVA: 0x0004BA1A File Offset: 0x00049C1A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
