using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000592 RID: 1426
	[Cmdlet("Get", "MonitoringItemHelp")]
	public sealed class GetMonitoringItemHelp : Task
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000CC72C File Offset: 0x000CA92C
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x000CC743 File Offset: 0x000CA943
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

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000CC756 File Offset: 0x000CA956
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x000CC75E File Offset: 0x000CA95E
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

		// Token: 0x0600323E RID: 12862 RVA: 0x000CC768 File Offset: 0x000CA968
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(this.Identity))
				{
					base.WriteError(new ArgumentException(Strings.InvalidMonitorIdentity(this.Identity)), (ErrorCategory)1000, null);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000CC7C0 File Offset: 0x000CA9C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				List<PropertyInformation> list = null;
				LocalizedException ex = null;
				try
				{
					list = RpcGetMonitoringItemHelp.Invoke(this.Server.Fqdn, this.Identity, 900000);
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
				foreach (PropertyInformation propertyInfo in list)
				{
					MonitorPropertyInformation sendToPipeline = new MonitorPropertyInformation(this.Server.Fqdn, propertyInfo);
					base.WriteObject(sendToPipeline);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04002351 RID: 9041
		private ServerIdParameter serverId;
	}
}
