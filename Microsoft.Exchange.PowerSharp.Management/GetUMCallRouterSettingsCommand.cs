using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B47 RID: 2887
	public class GetUMCallRouterSettingsCommand : SyntheticCommandWithPipelineInput<SIPFEServerConfiguration, SIPFEServerConfiguration>
	{
		// Token: 0x06008CAD RID: 36013 RVA: 0x000CE546 File Offset: 0x000CC746
		private GetUMCallRouterSettingsCommand() : base("Get-UMCallRouterSettings")
		{
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x000CE553 File Offset: 0x000CC753
		public GetUMCallRouterSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x000CE562 File Offset: 0x000CC762
		public virtual GetUMCallRouterSettingsCommand SetParameters(GetUMCallRouterSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B48 RID: 2888
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061D8 RID: 25048
			// (set) Token: 0x06008CB0 RID: 36016 RVA: 0x000CE56C File Offset: 0x000CC76C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170061D9 RID: 25049
			// (set) Token: 0x06008CB1 RID: 36017 RVA: 0x000CE57F File Offset: 0x000CC77F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061DA RID: 25050
			// (set) Token: 0x06008CB2 RID: 36018 RVA: 0x000CE592 File Offset: 0x000CC792
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061DB RID: 25051
			// (set) Token: 0x06008CB3 RID: 36019 RVA: 0x000CE5AA File Offset: 0x000CC7AA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061DC RID: 25052
			// (set) Token: 0x06008CB4 RID: 36020 RVA: 0x000CE5C2 File Offset: 0x000CC7C2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061DD RID: 25053
			// (set) Token: 0x06008CB5 RID: 36021 RVA: 0x000CE5DA File Offset: 0x000CC7DA
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
