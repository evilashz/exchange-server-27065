using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000617 RID: 1559
	public class NewClientAccessArrayCommand : SyntheticCommandWithPipelineInput<ClientAccessArray, ClientAccessArray>
	{
		// Token: 0x06004FE8 RID: 20456 RVA: 0x0007EDC0 File Offset: 0x0007CFC0
		private NewClientAccessArrayCommand() : base("New-ClientAccessArray")
		{
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0007EDCD File Offset: 0x0007CFCD
		public NewClientAccessArrayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		public virtual NewClientAccessArrayCommand SetParameters(NewClientAccessArrayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000618 RID: 1560
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F73 RID: 12147
			// (set) Token: 0x06004FEB RID: 20459 RVA: 0x0007EDE6 File Offset: 0x0007CFE6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002F74 RID: 12148
			// (set) Token: 0x06004FEC RID: 20460 RVA: 0x0007EDF9 File Offset: 0x0007CFF9
			public virtual string ArrayDefinition
			{
				set
				{
					base.PowerSharpParameters["ArrayDefinition"] = value;
				}
			}

			// Token: 0x17002F75 RID: 12149
			// (set) Token: 0x06004FED RID: 20461 RVA: 0x0007EE0C File Offset: 0x0007D00C
			public virtual int ServerCount
			{
				set
				{
					base.PowerSharpParameters["ServerCount"] = value;
				}
			}

			// Token: 0x17002F76 RID: 12150
			// (set) Token: 0x06004FEE RID: 20462 RVA: 0x0007EE24 File Offset: 0x0007D024
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002F77 RID: 12151
			// (set) Token: 0x06004FEF RID: 20463 RVA: 0x0007EE42 File Offset: 0x0007D042
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F78 RID: 12152
			// (set) Token: 0x06004FF0 RID: 20464 RVA: 0x0007EE55 File Offset: 0x0007D055
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F79 RID: 12153
			// (set) Token: 0x06004FF1 RID: 20465 RVA: 0x0007EE6D File Offset: 0x0007D06D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F7A RID: 12154
			// (set) Token: 0x06004FF2 RID: 20466 RVA: 0x0007EE85 File Offset: 0x0007D085
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F7B RID: 12155
			// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x0007EE9D File Offset: 0x0007D09D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F7C RID: 12156
			// (set) Token: 0x06004FF4 RID: 20468 RVA: 0x0007EEB5 File Offset: 0x0007D0B5
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
