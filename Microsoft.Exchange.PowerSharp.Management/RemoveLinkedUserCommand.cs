using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C62 RID: 3170
	public class RemoveLinkedUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009B7F RID: 39807 RVA: 0x000E1AAA File Offset: 0x000DFCAA
		private RemoveLinkedUserCommand() : base("Remove-LinkedUser")
		{
		}

		// Token: 0x06009B80 RID: 39808 RVA: 0x000E1AB7 File Offset: 0x000DFCB7
		public RemoveLinkedUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x000E1AC6 File Offset: 0x000DFCC6
		public virtual RemoveLinkedUserCommand SetParameters(RemoveLinkedUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009B82 RID: 39810 RVA: 0x000E1AD0 File Offset: 0x000DFCD0
		public virtual RemoveLinkedUserCommand SetParameters(RemoveLinkedUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C63 RID: 3171
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006E74 RID: 28276
			// (set) Token: 0x06009B83 RID: 39811 RVA: 0x000E1ADA File Offset: 0x000DFCDA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E75 RID: 28277
			// (set) Token: 0x06009B84 RID: 39812 RVA: 0x000E1AF2 File Offset: 0x000DFCF2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E76 RID: 28278
			// (set) Token: 0x06009B85 RID: 39813 RVA: 0x000E1B05 File Offset: 0x000DFD05
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E77 RID: 28279
			// (set) Token: 0x06009B86 RID: 39814 RVA: 0x000E1B1D File Offset: 0x000DFD1D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E78 RID: 28280
			// (set) Token: 0x06009B87 RID: 39815 RVA: 0x000E1B35 File Offset: 0x000DFD35
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E79 RID: 28281
			// (set) Token: 0x06009B88 RID: 39816 RVA: 0x000E1B4D File Offset: 0x000DFD4D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E7A RID: 28282
			// (set) Token: 0x06009B89 RID: 39817 RVA: 0x000E1B65 File Offset: 0x000DFD65
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006E7B RID: 28283
			// (set) Token: 0x06009B8A RID: 39818 RVA: 0x000E1B7D File Offset: 0x000DFD7D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000C64 RID: 3172
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006E7C RID: 28284
			// (set) Token: 0x06009B8C RID: 39820 RVA: 0x000E1B9D File Offset: 0x000DFD9D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006E7D RID: 28285
			// (set) Token: 0x06009B8D RID: 39821 RVA: 0x000E1BBB File Offset: 0x000DFDBB
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006E7E RID: 28286
			// (set) Token: 0x06009B8E RID: 39822 RVA: 0x000E1BD3 File Offset: 0x000DFDD3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E7F RID: 28287
			// (set) Token: 0x06009B8F RID: 39823 RVA: 0x000E1BE6 File Offset: 0x000DFDE6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E80 RID: 28288
			// (set) Token: 0x06009B90 RID: 39824 RVA: 0x000E1BFE File Offset: 0x000DFDFE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E81 RID: 28289
			// (set) Token: 0x06009B91 RID: 39825 RVA: 0x000E1C16 File Offset: 0x000DFE16
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E82 RID: 28290
			// (set) Token: 0x06009B92 RID: 39826 RVA: 0x000E1C2E File Offset: 0x000DFE2E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E83 RID: 28291
			// (set) Token: 0x06009B93 RID: 39827 RVA: 0x000E1C46 File Offset: 0x000DFE46
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006E84 RID: 28292
			// (set) Token: 0x06009B94 RID: 39828 RVA: 0x000E1C5E File Offset: 0x000DFE5E
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
