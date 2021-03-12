using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002CF RID: 719
	public class GetAuthConfigCommand : SyntheticCommandWithPipelineInput<AuthConfig, AuthConfig>
	{
		// Token: 0x0600319B RID: 12699 RVA: 0x00058464 File Offset: 0x00056664
		private GetAuthConfigCommand() : base("Get-AuthConfig")
		{
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00058471 File Offset: 0x00056671
		public GetAuthConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x00058480 File Offset: 0x00056680
		public virtual GetAuthConfigCommand SetParameters(GetAuthConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002D0 RID: 720
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170017B6 RID: 6070
			// (set) Token: 0x0600319E RID: 12702 RVA: 0x0005848A File Offset: 0x0005668A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170017B7 RID: 6071
			// (set) Token: 0x0600319F RID: 12703 RVA: 0x0005849D File Offset: 0x0005669D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017B8 RID: 6072
			// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000584B5 File Offset: 0x000566B5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017B9 RID: 6073
			// (set) Token: 0x060031A1 RID: 12705 RVA: 0x000584CD File Offset: 0x000566CD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017BA RID: 6074
			// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000584E5 File Offset: 0x000566E5
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
