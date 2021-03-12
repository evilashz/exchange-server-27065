using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000593 RID: 1427
	[Cmdlet("Get", "MonitoringItemIdentity")]
	public sealed class GetMonitoringItemIdentity : Task
	{
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000CC89C File Offset: 0x000CAA9C
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000CC8B3 File Offset: 0x000CAAB3
		[ValidateNotNullOrEmpty]
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000CC8C6 File Offset: 0x000CAAC6
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000CC8CE File Offset: 0x000CAACE
		[Parameter(Mandatory = true)]
		public ServerIdParameter Server
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000CC8D8 File Offset: 0x000CAAD8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> list = null;
				LocalizedException ex = null;
				try
				{
					list = RpcGetMonitoringItemIdentity.Invoke(this.Server.Fqdn, this.Identity, 900000);
				}
				catch (ActiveMonitoringServerException ex2)
				{
					ex = ex2;
				}
				catch (ActiveMonitoringServerTransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.WriteWarning(ex.LocalizedString);
				}
				if (list != null)
				{
					foreach (RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity rpcIdentity in list)
					{
						MonitoringItemIdentity sendToPipeline = new MonitoringItemIdentity(this.Server.Fqdn, rpcIdentity);
						base.WriteObject(sendToPipeline);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04002352 RID: 9042
		private ServerIdParameter serverId;
	}
}
