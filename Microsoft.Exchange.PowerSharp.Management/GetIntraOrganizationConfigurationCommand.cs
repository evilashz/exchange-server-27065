using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006D2 RID: 1746
	public class GetIntraOrganizationConfigurationCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06005B5E RID: 23390 RVA: 0x0008E39C File Offset: 0x0008C59C
		private GetIntraOrganizationConfigurationCommand() : base("Get-IntraOrganizationConfiguration")
		{
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x0008E3A9 File Offset: 0x0008C5A9
		public GetIntraOrganizationConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0008E3B8 File Offset: 0x0008C5B8
		public virtual GetIntraOrganizationConfigurationCommand SetParameters(GetIntraOrganizationConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006D3 RID: 1747
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003973 RID: 14707
			// (set) Token: 0x06005B61 RID: 23393 RVA: 0x0008E3C2 File Offset: 0x0008C5C2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003974 RID: 14708
			// (set) Token: 0x06005B62 RID: 23394 RVA: 0x0008E3E0 File Offset: 0x0008C5E0
			public virtual OnPremisesOrganizationIdParameter OrganizationGuid
			{
				set
				{
					base.PowerSharpParameters["OrganizationGuid"] = value;
				}
			}

			// Token: 0x17003975 RID: 14709
			// (set) Token: 0x06005B63 RID: 23395 RVA: 0x0008E3F3 File Offset: 0x0008C5F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003976 RID: 14710
			// (set) Token: 0x06005B64 RID: 23396 RVA: 0x0008E40B File Offset: 0x0008C60B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003977 RID: 14711
			// (set) Token: 0x06005B65 RID: 23397 RVA: 0x0008E423 File Offset: 0x0008C623
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003978 RID: 14712
			// (set) Token: 0x06005B66 RID: 23398 RVA: 0x0008E43B File Offset: 0x0008C63B
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
