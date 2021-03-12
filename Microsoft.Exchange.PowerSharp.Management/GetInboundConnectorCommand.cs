using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200088A RID: 2186
	public class GetInboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantInboundConnector, TenantInboundConnector>
	{
		// Token: 0x06006CDD RID: 27869 RVA: 0x000A4DC3 File Offset: 0x000A2FC3
		private GetInboundConnectorCommand() : base("Get-InboundConnector")
		{
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x000A4DD0 File Offset: 0x000A2FD0
		public GetInboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x000A4DDF File Offset: 0x000A2FDF
		public virtual GetInboundConnectorCommand SetParameters(GetInboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x000A4DE9 File Offset: 0x000A2FE9
		public virtual GetInboundConnectorCommand SetParameters(GetInboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200088B RID: 2187
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004782 RID: 18306
			// (set) Token: 0x06006CE1 RID: 27873 RVA: 0x000A4DF3 File Offset: 0x000A2FF3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004783 RID: 18307
			// (set) Token: 0x06006CE2 RID: 27874 RVA: 0x000A4E11 File Offset: 0x000A3011
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004784 RID: 18308
			// (set) Token: 0x06006CE3 RID: 27875 RVA: 0x000A4E24 File Offset: 0x000A3024
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004785 RID: 18309
			// (set) Token: 0x06006CE4 RID: 27876 RVA: 0x000A4E3C File Offset: 0x000A303C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004786 RID: 18310
			// (set) Token: 0x06006CE5 RID: 27877 RVA: 0x000A4E54 File Offset: 0x000A3054
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004787 RID: 18311
			// (set) Token: 0x06006CE6 RID: 27878 RVA: 0x000A4E6C File Offset: 0x000A306C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200088C RID: 2188
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004788 RID: 18312
			// (set) Token: 0x06006CE8 RID: 27880 RVA: 0x000A4E8C File Offset: 0x000A308C
			public virtual InboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004789 RID: 18313
			// (set) Token: 0x06006CE9 RID: 27881 RVA: 0x000A4E9F File Offset: 0x000A309F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700478A RID: 18314
			// (set) Token: 0x06006CEA RID: 27882 RVA: 0x000A4EBD File Offset: 0x000A30BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700478B RID: 18315
			// (set) Token: 0x06006CEB RID: 27883 RVA: 0x000A4ED0 File Offset: 0x000A30D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700478C RID: 18316
			// (set) Token: 0x06006CEC RID: 27884 RVA: 0x000A4EE8 File Offset: 0x000A30E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700478D RID: 18317
			// (set) Token: 0x06006CED RID: 27885 RVA: 0x000A4F00 File Offset: 0x000A3100
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700478E RID: 18318
			// (set) Token: 0x06006CEE RID: 27886 RVA: 0x000A4F18 File Offset: 0x000A3118
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
