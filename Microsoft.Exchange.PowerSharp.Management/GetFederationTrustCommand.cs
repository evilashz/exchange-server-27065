using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200067F RID: 1663
	public class GetFederationTrustCommand : SyntheticCommandWithPipelineInput<FederationTrust, FederationTrust>
	{
		// Token: 0x060058B1 RID: 22705 RVA: 0x0008AE29 File Offset: 0x00089029
		private GetFederationTrustCommand() : base("Get-FederationTrust")
		{
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0008AE36 File Offset: 0x00089036
		public GetFederationTrustCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0008AE45 File Offset: 0x00089045
		public virtual GetFederationTrustCommand SetParameters(GetFederationTrustCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x0008AE4F File Offset: 0x0008904F
		public virtual GetFederationTrustCommand SetParameters(GetFederationTrustCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000680 RID: 1664
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700376C RID: 14188
			// (set) Token: 0x060058B5 RID: 22709 RVA: 0x0008AE59 File Offset: 0x00089059
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700376D RID: 14189
			// (set) Token: 0x060058B6 RID: 22710 RVA: 0x0008AE6C File Offset: 0x0008906C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700376E RID: 14190
			// (set) Token: 0x060058B7 RID: 22711 RVA: 0x0008AE84 File Offset: 0x00089084
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700376F RID: 14191
			// (set) Token: 0x060058B8 RID: 22712 RVA: 0x0008AE9C File Offset: 0x0008909C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003770 RID: 14192
			// (set) Token: 0x060058B9 RID: 22713 RVA: 0x0008AEB4 File Offset: 0x000890B4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000681 RID: 1665
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003771 RID: 14193
			// (set) Token: 0x060058BB RID: 22715 RVA: 0x0008AED4 File Offset: 0x000890D4
			public virtual FederationTrustIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003772 RID: 14194
			// (set) Token: 0x060058BC RID: 22716 RVA: 0x0008AEE7 File Offset: 0x000890E7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003773 RID: 14195
			// (set) Token: 0x060058BD RID: 22717 RVA: 0x0008AEFA File Offset: 0x000890FA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003774 RID: 14196
			// (set) Token: 0x060058BE RID: 22718 RVA: 0x0008AF12 File Offset: 0x00089112
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003775 RID: 14197
			// (set) Token: 0x060058BF RID: 22719 RVA: 0x0008AF2A File Offset: 0x0008912A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003776 RID: 14198
			// (set) Token: 0x060058C0 RID: 22720 RVA: 0x0008AF42 File Offset: 0x00089142
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
