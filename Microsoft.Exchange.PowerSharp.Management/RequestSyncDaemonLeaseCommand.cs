using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200013A RID: 314
	public class RequestSyncDaemonLeaseCommand : SyntheticCommandWithPipelineInputNoOutput<ServiceInstanceId>
	{
		// Token: 0x06002051 RID: 8273 RVA: 0x0004190B File Offset: 0x0003FB0B
		private RequestSyncDaemonLeaseCommand() : base("Request-SyncDaemonLease")
		{
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00041918 File Offset: 0x0003FB18
		public RequestSyncDaemonLeaseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00041927 File Offset: 0x0003FB27
		public virtual RequestSyncDaemonLeaseCommand SetParameters(RequestSyncDaemonLeaseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200013B RID: 315
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000996 RID: 2454
			// (set) Token: 0x06002054 RID: 8276 RVA: 0x00041931 File Offset: 0x0003FB31
			public virtual ServiceInstanceId ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x17000997 RID: 2455
			// (set) Token: 0x06002055 RID: 8277 RVA: 0x00041944 File Offset: 0x0003FB44
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000998 RID: 2456
			// (set) Token: 0x06002056 RID: 8278 RVA: 0x0004195C File Offset: 0x0003FB5C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000999 RID: 2457
			// (set) Token: 0x06002057 RID: 8279 RVA: 0x00041974 File Offset: 0x0003FB74
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700099A RID: 2458
			// (set) Token: 0x06002058 RID: 8280 RVA: 0x0004198C File Offset: 0x0003FB8C
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
