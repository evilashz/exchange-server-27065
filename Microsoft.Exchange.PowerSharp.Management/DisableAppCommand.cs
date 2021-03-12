using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E3A RID: 3642
	public class DisableAppCommand : SyntheticCommandWithPipelineInputNoOutput<AppIdParameter>
	{
		// Token: 0x0600D866 RID: 55398 RVA: 0x0013345D File Offset: 0x0013165D
		private DisableAppCommand() : base("Disable-App")
		{
		}

		// Token: 0x0600D867 RID: 55399 RVA: 0x0013346A File Offset: 0x0013166A
		public DisableAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D868 RID: 55400 RVA: 0x00133479 File Offset: 0x00131679
		public virtual DisableAppCommand SetParameters(DisableAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D869 RID: 55401 RVA: 0x00133483 File Offset: 0x00131683
		public virtual DisableAppCommand SetParameters(DisableAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E3B RID: 3643
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A7AB RID: 42923
			// (set) Token: 0x0600D86A RID: 55402 RVA: 0x0013348D File Offset: 0x0013168D
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7AC RID: 42924
			// (set) Token: 0x0600D86B RID: 55403 RVA: 0x001334AB File Offset: 0x001316AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7AD RID: 42925
			// (set) Token: 0x0600D86C RID: 55404 RVA: 0x001334BE File Offset: 0x001316BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7AE RID: 42926
			// (set) Token: 0x0600D86D RID: 55405 RVA: 0x001334D6 File Offset: 0x001316D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7AF RID: 42927
			// (set) Token: 0x0600D86E RID: 55406 RVA: 0x001334EE File Offset: 0x001316EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7B0 RID: 42928
			// (set) Token: 0x0600D86F RID: 55407 RVA: 0x00133506 File Offset: 0x00131706
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7B1 RID: 42929
			// (set) Token: 0x0600D870 RID: 55408 RVA: 0x0013351E File Offset: 0x0013171E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A7B2 RID: 42930
			// (set) Token: 0x0600D871 RID: 55409 RVA: 0x00133536 File Offset: 0x00131736
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E3C RID: 3644
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A7B3 RID: 42931
			// (set) Token: 0x0600D873 RID: 55411 RVA: 0x00133556 File Offset: 0x00131756
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AppIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7B4 RID: 42932
			// (set) Token: 0x0600D874 RID: 55412 RVA: 0x00133574 File Offset: 0x00131774
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7B5 RID: 42933
			// (set) Token: 0x0600D875 RID: 55413 RVA: 0x00133592 File Offset: 0x00131792
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7B6 RID: 42934
			// (set) Token: 0x0600D876 RID: 55414 RVA: 0x001335A5 File Offset: 0x001317A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7B7 RID: 42935
			// (set) Token: 0x0600D877 RID: 55415 RVA: 0x001335BD File Offset: 0x001317BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7B8 RID: 42936
			// (set) Token: 0x0600D878 RID: 55416 RVA: 0x001335D5 File Offset: 0x001317D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7B9 RID: 42937
			// (set) Token: 0x0600D879 RID: 55417 RVA: 0x001335ED File Offset: 0x001317ED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7BA RID: 42938
			// (set) Token: 0x0600D87A RID: 55418 RVA: 0x00133605 File Offset: 0x00131805
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A7BB RID: 42939
			// (set) Token: 0x0600D87B RID: 55419 RVA: 0x0013361D File Offset: 0x0013181D
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
