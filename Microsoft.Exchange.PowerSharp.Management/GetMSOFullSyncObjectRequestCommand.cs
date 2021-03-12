using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200011B RID: 283
	public class GetMSOFullSyncObjectRequestCommand : SyntheticCommandWithPipelineInput<FullSyncObjectRequest, FullSyncObjectRequest>
	{
		// Token: 0x06001F7C RID: 8060 RVA: 0x000408B6 File Offset: 0x0003EAB6
		private GetMSOFullSyncObjectRequestCommand() : base("Get-MSOFullSyncObjectRequest")
		{
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000408C3 File Offset: 0x0003EAC3
		public GetMSOFullSyncObjectRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000408D2 File Offset: 0x0003EAD2
		public virtual GetMSOFullSyncObjectRequestCommand SetParameters(GetMSOFullSyncObjectRequestCommand.IdentityParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000408DC File Offset: 0x0003EADC
		public virtual GetMSOFullSyncObjectRequestCommand SetParameters(GetMSOFullSyncObjectRequestCommand.ServiceInstanceIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200011C RID: 284
		public class IdentityParameterSetParameters : ParametersBase
		{
			// Token: 0x170008FF RID: 2303
			// (set) Token: 0x06001F80 RID: 8064 RVA: 0x000408E6 File Offset: 0x0003EAE6
			public virtual SyncObjectId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17000900 RID: 2304
			// (set) Token: 0x06001F81 RID: 8065 RVA: 0x000408F9 File Offset: 0x0003EAF9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00040911 File Offset: 0x0003EB11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000902 RID: 2306
			// (set) Token: 0x06001F83 RID: 8067 RVA: 0x00040929 File Offset: 0x0003EB29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000903 RID: 2307
			// (set) Token: 0x06001F84 RID: 8068 RVA: 0x00040941 File Offset: 0x0003EB41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200011D RID: 285
		public class ServiceInstanceIdParameterSetParameters : ParametersBase
		{
			// Token: 0x17000904 RID: 2308
			// (set) Token: 0x06001F86 RID: 8070 RVA: 0x00040961 File Offset: 0x0003EB61
			public virtual ServiceInstanceId ServiceInstanceId
			{
				set
				{
					base.PowerSharpParameters["ServiceInstanceId"] = value;
				}
			}

			// Token: 0x17000905 RID: 2309
			// (set) Token: 0x06001F87 RID: 8071 RVA: 0x00040974 File Offset: 0x0003EB74
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000906 RID: 2310
			// (set) Token: 0x06001F88 RID: 8072 RVA: 0x0004098C File Offset: 0x0003EB8C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000907 RID: 2311
			// (set) Token: 0x06001F89 RID: 8073 RVA: 0x000409A4 File Offset: 0x0003EBA4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000908 RID: 2312
			// (set) Token: 0x06001F8A RID: 8074 RVA: 0x000409BC File Offset: 0x0003EBBC
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
