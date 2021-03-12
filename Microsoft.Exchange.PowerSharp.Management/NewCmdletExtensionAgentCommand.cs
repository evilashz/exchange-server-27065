using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AE7 RID: 2791
	public class NewCmdletExtensionAgentCommand : SyntheticCommandWithPipelineInput<CmdletExtensionAgent, CmdletExtensionAgent>
	{
		// Token: 0x060089A7 RID: 35239 RVA: 0x000CA79C File Offset: 0x000C899C
		private NewCmdletExtensionAgentCommand() : base("New-CmdletExtensionAgent")
		{
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x000CA7A9 File Offset: 0x000C89A9
		public NewCmdletExtensionAgentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x000CA7B8 File Offset: 0x000C89B8
		public virtual NewCmdletExtensionAgentCommand SetParameters(NewCmdletExtensionAgentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AE8 RID: 2792
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F92 RID: 24466
			// (set) Token: 0x060089AA RID: 35242 RVA: 0x000CA7C2 File Offset: 0x000C89C2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005F93 RID: 24467
			// (set) Token: 0x060089AB RID: 35243 RVA: 0x000CA7D5 File Offset: 0x000C89D5
			public virtual string Assembly
			{
				set
				{
					base.PowerSharpParameters["Assembly"] = value;
				}
			}

			// Token: 0x17005F94 RID: 24468
			// (set) Token: 0x060089AC RID: 35244 RVA: 0x000CA7E8 File Offset: 0x000C89E8
			public virtual string ClassFactory
			{
				set
				{
					base.PowerSharpParameters["ClassFactory"] = value;
				}
			}

			// Token: 0x17005F95 RID: 24469
			// (set) Token: 0x060089AD RID: 35245 RVA: 0x000CA7FB File Offset: 0x000C89FB
			public virtual byte Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005F96 RID: 24470
			// (set) Token: 0x060089AE RID: 35246 RVA: 0x000CA813 File Offset: 0x000C8A13
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17005F97 RID: 24471
			// (set) Token: 0x060089AF RID: 35247 RVA: 0x000CA82B File Offset: 0x000C8A2B
			public virtual bool IsSystem
			{
				set
				{
					base.PowerSharpParameters["IsSystem"] = value;
				}
			}

			// Token: 0x17005F98 RID: 24472
			// (set) Token: 0x060089B0 RID: 35248 RVA: 0x000CA843 File Offset: 0x000C8A43
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F99 RID: 24473
			// (set) Token: 0x060089B1 RID: 35249 RVA: 0x000CA856 File Offset: 0x000C8A56
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F9A RID: 24474
			// (set) Token: 0x060089B2 RID: 35250 RVA: 0x000CA86E File Offset: 0x000C8A6E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F9B RID: 24475
			// (set) Token: 0x060089B3 RID: 35251 RVA: 0x000CA886 File Offset: 0x000C8A86
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F9C RID: 24476
			// (set) Token: 0x060089B4 RID: 35252 RVA: 0x000CA89E File Offset: 0x000C8A9E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F9D RID: 24477
			// (set) Token: 0x060089B5 RID: 35253 RVA: 0x000CA8B6 File Offset: 0x000C8AB6
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
