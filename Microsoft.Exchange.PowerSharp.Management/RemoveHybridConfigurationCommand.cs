using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200058F RID: 1423
	public class RemoveHybridConfigurationCommand : SyntheticCommandWithPipelineInput<HybridConfiguration, HybridConfiguration>
	{
		// Token: 0x06004A8E RID: 19086 RVA: 0x000780F1 File Offset: 0x000762F1
		private RemoveHybridConfigurationCommand() : base("Remove-HybridConfiguration")
		{
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x000780FE File Offset: 0x000762FE
		public RemoveHybridConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x0007810D File Offset: 0x0007630D
		public virtual RemoveHybridConfigurationCommand SetParameters(RemoveHybridConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000590 RID: 1424
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B29 RID: 11049
			// (set) Token: 0x06004A91 RID: 19089 RVA: 0x00078117 File Offset: 0x00076317
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B2A RID: 11050
			// (set) Token: 0x06004A92 RID: 19090 RVA: 0x0007812A File Offset: 0x0007632A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B2B RID: 11051
			// (set) Token: 0x06004A93 RID: 19091 RVA: 0x00078142 File Offset: 0x00076342
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B2C RID: 11052
			// (set) Token: 0x06004A94 RID: 19092 RVA: 0x0007815A File Offset: 0x0007635A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B2D RID: 11053
			// (set) Token: 0x06004A95 RID: 19093 RVA: 0x00078172 File Offset: 0x00076372
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B2E RID: 11054
			// (set) Token: 0x06004A96 RID: 19094 RVA: 0x0007818A File Offset: 0x0007638A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002B2F RID: 11055
			// (set) Token: 0x06004A97 RID: 19095 RVA: 0x000781A2 File Offset: 0x000763A2
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
