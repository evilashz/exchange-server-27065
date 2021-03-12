using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000890 RID: 2192
	public class GetNetworkConnectionInfoCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06006D03 RID: 27907 RVA: 0x000A50AC File Offset: 0x000A32AC
		private GetNetworkConnectionInfoCommand() : base("Get-NetworkConnectionInfo")
		{
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x000A50B9 File Offset: 0x000A32B9
		public GetNetworkConnectionInfoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x000A50C8 File Offset: 0x000A32C8
		public virtual GetNetworkConnectionInfoCommand SetParameters(GetNetworkConnectionInfoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x000A50D2 File Offset: 0x000A32D2
		public virtual GetNetworkConnectionInfoCommand SetParameters(GetNetworkConnectionInfoCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000891 RID: 2193
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700479C RID: 18332
			// (set) Token: 0x06006D07 RID: 27911 RVA: 0x000A50DC File Offset: 0x000A32DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700479D RID: 18333
			// (set) Token: 0x06006D08 RID: 27912 RVA: 0x000A50EF File Offset: 0x000A32EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700479E RID: 18334
			// (set) Token: 0x06006D09 RID: 27913 RVA: 0x000A5107 File Offset: 0x000A3307
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700479F RID: 18335
			// (set) Token: 0x06006D0A RID: 27914 RVA: 0x000A511F File Offset: 0x000A331F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047A0 RID: 18336
			// (set) Token: 0x06006D0B RID: 27915 RVA: 0x000A5137 File Offset: 0x000A3337
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000892 RID: 2194
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170047A1 RID: 18337
			// (set) Token: 0x06006D0D RID: 27917 RVA: 0x000A5157 File Offset: 0x000A3357
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170047A2 RID: 18338
			// (set) Token: 0x06006D0E RID: 27918 RVA: 0x000A516A File Offset: 0x000A336A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047A3 RID: 18339
			// (set) Token: 0x06006D0F RID: 27919 RVA: 0x000A517D File Offset: 0x000A337D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047A4 RID: 18340
			// (set) Token: 0x06006D10 RID: 27920 RVA: 0x000A5195 File Offset: 0x000A3395
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047A5 RID: 18341
			// (set) Token: 0x06006D11 RID: 27921 RVA: 0x000A51AD File Offset: 0x000A33AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047A6 RID: 18342
			// (set) Token: 0x06006D12 RID: 27922 RVA: 0x000A51C5 File Offset: 0x000A33C5
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
