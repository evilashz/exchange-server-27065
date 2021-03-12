using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200011E RID: 286
	public class GetMsoFullSyncOrganizationCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x06001F8C RID: 8076 RVA: 0x000409DC File Offset: 0x0003EBDC
		private GetMsoFullSyncOrganizationCommand() : base("Get-MsoFullSyncOrganization")
		{
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000409E9 File Offset: 0x0003EBE9
		public GetMsoFullSyncOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000409F8 File Offset: 0x0003EBF8
		public virtual GetMsoFullSyncOrganizationCommand SetParameters(GetMsoFullSyncOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00040A02 File Offset: 0x0003EC02
		public virtual GetMsoFullSyncOrganizationCommand SetParameters(GetMsoFullSyncOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200011F RID: 287
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000909 RID: 2313
			// (set) Token: 0x06001F90 RID: 8080 RVA: 0x00040A0C File Offset: 0x0003EC0C
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00040A1F File Offset: 0x0003EC1F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700090B RID: 2315
			// (set) Token: 0x06001F92 RID: 8082 RVA: 0x00040A32 File Offset: 0x0003EC32
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700090C RID: 2316
			// (set) Token: 0x06001F93 RID: 8083 RVA: 0x00040A4A File Offset: 0x0003EC4A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700090D RID: 2317
			// (set) Token: 0x06001F94 RID: 8084 RVA: 0x00040A62 File Offset: 0x0003EC62
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700090E RID: 2318
			// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00040A7A File Offset: 0x0003EC7A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000120 RID: 288
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700090F RID: 2319
			// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00040A9A File Offset: 0x0003EC9A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000910 RID: 2320
			// (set) Token: 0x06001F98 RID: 8088 RVA: 0x00040AB8 File Offset: 0x0003ECB8
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17000911 RID: 2321
			// (set) Token: 0x06001F99 RID: 8089 RVA: 0x00040ACB File Offset: 0x0003ECCB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000912 RID: 2322
			// (set) Token: 0x06001F9A RID: 8090 RVA: 0x00040ADE File Offset: 0x0003ECDE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000913 RID: 2323
			// (set) Token: 0x06001F9B RID: 8091 RVA: 0x00040AF6 File Offset: 0x0003ECF6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000914 RID: 2324
			// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00040B0E File Offset: 0x0003ED0E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000915 RID: 2325
			// (set) Token: 0x06001F9D RID: 8093 RVA: 0x00040B26 File Offset: 0x0003ED26
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
