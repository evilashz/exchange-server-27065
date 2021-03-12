using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003F5 RID: 1013
	public class SetRMSTemplateCommand : SyntheticCommandWithPipelineInputNoOutput<RmsTemplatePresentation>
	{
		// Token: 0x06003C24 RID: 15396 RVA: 0x00065D51 File Offset: 0x00063F51
		private SetRMSTemplateCommand() : base("Set-RMSTemplate")
		{
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x00065D5E File Offset: 0x00063F5E
		public SetRMSTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x00065D6D File Offset: 0x00063F6D
		public virtual SetRMSTemplateCommand SetParameters(SetRMSTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x00065D77 File Offset: 0x00063F77
		public virtual SetRMSTemplateCommand SetParameters(SetRMSTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003F6 RID: 1014
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001FF3 RID: 8179
			// (set) Token: 0x06003C28 RID: 15400 RVA: 0x00065D81 File Offset: 0x00063F81
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001FF4 RID: 8180
			// (set) Token: 0x06003C29 RID: 15401 RVA: 0x00065D9F File Offset: 0x00063F9F
			public virtual RmsTemplateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001FF5 RID: 8181
			// (set) Token: 0x06003C2A RID: 15402 RVA: 0x00065DB7 File Offset: 0x00063FB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FF6 RID: 8182
			// (set) Token: 0x06003C2B RID: 15403 RVA: 0x00065DCA File Offset: 0x00063FCA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FF7 RID: 8183
			// (set) Token: 0x06003C2C RID: 15404 RVA: 0x00065DE2 File Offset: 0x00063FE2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FF8 RID: 8184
			// (set) Token: 0x06003C2D RID: 15405 RVA: 0x00065DFA File Offset: 0x00063FFA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FF9 RID: 8185
			// (set) Token: 0x06003C2E RID: 15406 RVA: 0x00065E12 File Offset: 0x00064012
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FFA RID: 8186
			// (set) Token: 0x06003C2F RID: 15407 RVA: 0x00065E2A File Offset: 0x0006402A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020003F7 RID: 1015
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001FFB RID: 8187
			// (set) Token: 0x06003C31 RID: 15409 RVA: 0x00065E4A File Offset: 0x0006404A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17001FFC RID: 8188
			// (set) Token: 0x06003C32 RID: 15410 RVA: 0x00065E68 File Offset: 0x00064068
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001FFD RID: 8189
			// (set) Token: 0x06003C33 RID: 15411 RVA: 0x00065E86 File Offset: 0x00064086
			public virtual RmsTemplateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001FFE RID: 8190
			// (set) Token: 0x06003C34 RID: 15412 RVA: 0x00065E9E File Offset: 0x0006409E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FFF RID: 8191
			// (set) Token: 0x06003C35 RID: 15413 RVA: 0x00065EB1 File Offset: 0x000640B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002000 RID: 8192
			// (set) Token: 0x06003C36 RID: 15414 RVA: 0x00065EC9 File Offset: 0x000640C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002001 RID: 8193
			// (set) Token: 0x06003C37 RID: 15415 RVA: 0x00065EE1 File Offset: 0x000640E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002002 RID: 8194
			// (set) Token: 0x06003C38 RID: 15416 RVA: 0x00065EF9 File Offset: 0x000640F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002003 RID: 8195
			// (set) Token: 0x06003C39 RID: 15417 RVA: 0x00065F11 File Offset: 0x00064111
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
