using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200058B RID: 1419
	public class GetHybridConfigurationCommand : SyntheticCommandWithPipelineInput<HybridConfiguration, HybridConfiguration>
	{
		// Token: 0x06004A71 RID: 19057 RVA: 0x00077ED4 File Offset: 0x000760D4
		private GetHybridConfigurationCommand() : base("Get-HybridConfiguration")
		{
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00077EE1 File Offset: 0x000760E1
		public GetHybridConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00077EF0 File Offset: 0x000760F0
		public virtual GetHybridConfigurationCommand SetParameters(GetHybridConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200058C RID: 1420
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B14 RID: 11028
			// (set) Token: 0x06004A74 RID: 19060 RVA: 0x00077EFA File Offset: 0x000760FA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B15 RID: 11029
			// (set) Token: 0x06004A75 RID: 19061 RVA: 0x00077F0D File Offset: 0x0007610D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B16 RID: 11030
			// (set) Token: 0x06004A76 RID: 19062 RVA: 0x00077F25 File Offset: 0x00076125
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B17 RID: 11031
			// (set) Token: 0x06004A77 RID: 19063 RVA: 0x00077F3D File Offset: 0x0007613D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B18 RID: 11032
			// (set) Token: 0x06004A78 RID: 19064 RVA: 0x00077F55 File Offset: 0x00076155
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
