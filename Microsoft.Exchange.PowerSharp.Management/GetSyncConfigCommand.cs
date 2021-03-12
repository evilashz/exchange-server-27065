using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000185 RID: 389
	public class GetSyncConfigCommand : SyntheticCommandWithPipelineInput<SyncOrganization, SyncOrganization>
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x00044E78 File Offset: 0x00043078
		private GetSyncConfigCommand() : base("Get-SyncConfig")
		{
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00044E85 File Offset: 0x00043085
		public GetSyncConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00044E94 File Offset: 0x00043094
		public virtual GetSyncConfigCommand SetParameters(GetSyncConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00044E9E File Offset: 0x0004309E
		public virtual GetSyncConfigCommand SetParameters(GetSyncConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000186 RID: 390
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000BAC RID: 2988
			// (set) Token: 0x06002301 RID: 8961 RVA: 0x00044EA8 File Offset: 0x000430A8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000BAD RID: 2989
			// (set) Token: 0x06002302 RID: 8962 RVA: 0x00044EC6 File Offset: 0x000430C6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BAE RID: 2990
			// (set) Token: 0x06002303 RID: 8963 RVA: 0x00044ED9 File Offset: 0x000430D9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BAF RID: 2991
			// (set) Token: 0x06002304 RID: 8964 RVA: 0x00044EF1 File Offset: 0x000430F1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BB0 RID: 2992
			// (set) Token: 0x06002305 RID: 8965 RVA: 0x00044F09 File Offset: 0x00043109
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BB1 RID: 2993
			// (set) Token: 0x06002306 RID: 8966 RVA: 0x00044F21 File Offset: 0x00043121
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000187 RID: 391
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000BB2 RID: 2994
			// (set) Token: 0x06002308 RID: 8968 RVA: 0x00044F41 File Offset: 0x00043141
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000BB3 RID: 2995
			// (set) Token: 0x06002309 RID: 8969 RVA: 0x00044F54 File Offset: 0x00043154
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000BB4 RID: 2996
			// (set) Token: 0x0600230A RID: 8970 RVA: 0x00044F6C File Offset: 0x0004316C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000BB5 RID: 2997
			// (set) Token: 0x0600230B RID: 8971 RVA: 0x00044F84 File Offset: 0x00043184
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000BB6 RID: 2998
			// (set) Token: 0x0600230C RID: 8972 RVA: 0x00044F9C File Offset: 0x0004319C
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
