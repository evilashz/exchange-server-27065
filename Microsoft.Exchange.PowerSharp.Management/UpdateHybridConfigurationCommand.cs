using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000593 RID: 1427
	public class UpdateHybridConfigurationCommand : SyntheticCommandWithPipelineInput<HybridConfiguration, HybridConfiguration>
	{
		// Token: 0x06004AAE RID: 19118 RVA: 0x00078351 File Offset: 0x00076551
		private UpdateHybridConfigurationCommand() : base("Update-HybridConfiguration")
		{
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0007835E File Offset: 0x0007655E
		public UpdateHybridConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0007836D File Offset: 0x0007656D
		public virtual UpdateHybridConfigurationCommand SetParameters(UpdateHybridConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000594 RID: 1428
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B41 RID: 11073
			// (set) Token: 0x06004AB1 RID: 19121 RVA: 0x00078377 File Offset: 0x00076577
			public virtual PSCredential OnPremisesCredentials
			{
				set
				{
					base.PowerSharpParameters["OnPremisesCredentials"] = value;
				}
			}

			// Token: 0x17002B42 RID: 11074
			// (set) Token: 0x06004AB2 RID: 19122 RVA: 0x0007838A File Offset: 0x0007658A
			public virtual PSCredential TenantCredentials
			{
				set
				{
					base.PowerSharpParameters["TenantCredentials"] = value;
				}
			}

			// Token: 0x17002B43 RID: 11075
			// (set) Token: 0x06004AB3 RID: 19123 RVA: 0x0007839D File Offset: 0x0007659D
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17002B44 RID: 11076
			// (set) Token: 0x06004AB4 RID: 19124 RVA: 0x000783B5 File Offset: 0x000765B5
			public virtual SwitchParameter SuppressOAuthWarning
			{
				set
				{
					base.PowerSharpParameters["SuppressOAuthWarning"] = value;
				}
			}

			// Token: 0x17002B45 RID: 11077
			// (set) Token: 0x06004AB5 RID: 19125 RVA: 0x000783CD File Offset: 0x000765CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B46 RID: 11078
			// (set) Token: 0x06004AB6 RID: 19126 RVA: 0x000783E0 File Offset: 0x000765E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B47 RID: 11079
			// (set) Token: 0x06004AB7 RID: 19127 RVA: 0x000783F8 File Offset: 0x000765F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B48 RID: 11080
			// (set) Token: 0x06004AB8 RID: 19128 RVA: 0x00078410 File Offset: 0x00076610
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B49 RID: 11081
			// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x00078428 File Offset: 0x00076628
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B4A RID: 11082
			// (set) Token: 0x06004ABA RID: 19130 RVA: 0x00078440 File Offset: 0x00076640
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
