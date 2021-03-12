using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000887 RID: 2183
	public class GetForeignConnectorCommand : SyntheticCommandWithPipelineInput<ForeignConnector, ForeignConnector>
	{
		// Token: 0x06006CCC RID: 27852 RVA: 0x000A4C7F File Offset: 0x000A2E7F
		private GetForeignConnectorCommand() : base("Get-ForeignConnector")
		{
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x000A4C8C File Offset: 0x000A2E8C
		public GetForeignConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x000A4C9B File Offset: 0x000A2E9B
		public virtual GetForeignConnectorCommand SetParameters(GetForeignConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x000A4CA5 File Offset: 0x000A2EA5
		public virtual GetForeignConnectorCommand SetParameters(GetForeignConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000888 RID: 2184
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004777 RID: 18295
			// (set) Token: 0x06006CD0 RID: 27856 RVA: 0x000A4CAF File Offset: 0x000A2EAF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004778 RID: 18296
			// (set) Token: 0x06006CD1 RID: 27857 RVA: 0x000A4CC2 File Offset: 0x000A2EC2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004779 RID: 18297
			// (set) Token: 0x06006CD2 RID: 27858 RVA: 0x000A4CDA File Offset: 0x000A2EDA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700477A RID: 18298
			// (set) Token: 0x06006CD3 RID: 27859 RVA: 0x000A4CF2 File Offset: 0x000A2EF2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700477B RID: 18299
			// (set) Token: 0x06006CD4 RID: 27860 RVA: 0x000A4D0A File Offset: 0x000A2F0A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000889 RID: 2185
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700477C RID: 18300
			// (set) Token: 0x06006CD6 RID: 27862 RVA: 0x000A4D2A File Offset: 0x000A2F2A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ForeignConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x1700477D RID: 18301
			// (set) Token: 0x06006CD7 RID: 27863 RVA: 0x000A4D48 File Offset: 0x000A2F48
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700477E RID: 18302
			// (set) Token: 0x06006CD8 RID: 27864 RVA: 0x000A4D5B File Offset: 0x000A2F5B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700477F RID: 18303
			// (set) Token: 0x06006CD9 RID: 27865 RVA: 0x000A4D73 File Offset: 0x000A2F73
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004780 RID: 18304
			// (set) Token: 0x06006CDA RID: 27866 RVA: 0x000A4D8B File Offset: 0x000A2F8B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004781 RID: 18305
			// (set) Token: 0x06006CDB RID: 27867 RVA: 0x000A4DA3 File Offset: 0x000A2FA3
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
