using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E3D RID: 3645
	public class EnableAppCommand : SyntheticCommandWithPipelineInputNoOutput<AppIdParameter>
	{
		// Token: 0x0600D87D RID: 55421 RVA: 0x0013363D File Offset: 0x0013183D
		private EnableAppCommand() : base("Enable-App")
		{
		}

		// Token: 0x0600D87E RID: 55422 RVA: 0x0013364A File Offset: 0x0013184A
		public EnableAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D87F RID: 55423 RVA: 0x00133659 File Offset: 0x00131859
		public virtual EnableAppCommand SetParameters(EnableAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D880 RID: 55424 RVA: 0x00133663 File Offset: 0x00131863
		public virtual EnableAppCommand SetParameters(EnableAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E3E RID: 3646
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A7BC RID: 42940
			// (set) Token: 0x0600D881 RID: 55425 RVA: 0x0013366D File Offset: 0x0013186D
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7BD RID: 42941
			// (set) Token: 0x0600D882 RID: 55426 RVA: 0x0013368B File Offset: 0x0013188B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7BE RID: 42942
			// (set) Token: 0x0600D883 RID: 55427 RVA: 0x0013369E File Offset: 0x0013189E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7BF RID: 42943
			// (set) Token: 0x0600D884 RID: 55428 RVA: 0x001336B6 File Offset: 0x001318B6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7C0 RID: 42944
			// (set) Token: 0x0600D885 RID: 55429 RVA: 0x001336CE File Offset: 0x001318CE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7C1 RID: 42945
			// (set) Token: 0x0600D886 RID: 55430 RVA: 0x001336E6 File Offset: 0x001318E6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7C2 RID: 42946
			// (set) Token: 0x0600D887 RID: 55431 RVA: 0x001336FE File Offset: 0x001318FE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E3F RID: 3647
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A7C3 RID: 42947
			// (set) Token: 0x0600D889 RID: 55433 RVA: 0x0013371E File Offset: 0x0013191E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AppIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7C4 RID: 42948
			// (set) Token: 0x0600D88A RID: 55434 RVA: 0x0013373C File Offset: 0x0013193C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7C5 RID: 42949
			// (set) Token: 0x0600D88B RID: 55435 RVA: 0x0013375A File Offset: 0x0013195A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7C6 RID: 42950
			// (set) Token: 0x0600D88C RID: 55436 RVA: 0x0013376D File Offset: 0x0013196D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7C7 RID: 42951
			// (set) Token: 0x0600D88D RID: 55437 RVA: 0x00133785 File Offset: 0x00131985
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7C8 RID: 42952
			// (set) Token: 0x0600D88E RID: 55438 RVA: 0x0013379D File Offset: 0x0013199D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7C9 RID: 42953
			// (set) Token: 0x0600D88F RID: 55439 RVA: 0x001337B5 File Offset: 0x001319B5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7CA RID: 42954
			// (set) Token: 0x0600D890 RID: 55440 RVA: 0x001337CD File Offset: 0x001319CD
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
