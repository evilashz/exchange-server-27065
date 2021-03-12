using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ProvisioningReconciliation;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B03 RID: 2819
	[Cmdlet("Get", "ProvisioningReconciliationConfig")]
	public sealed class GetProvisioningReconciliationConfig : GetSingletonSystemConfigurationObjectTask<ProvisioningReconciliationConfig>
	{
		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x001A2B2A File Offset: 0x001A0D2A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x001A2B2D File Offset: 0x001A0D2D
		// (set) Token: 0x06006442 RID: 25666 RVA: 0x001A2B35 File Offset: 0x001A0D35
		internal new Fqdn DomainController { get; set; }

		// Token: 0x06006443 RID: 25667 RVA: 0x001A2B40 File Offset: 0x001A0D40
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (dataObject != null)
			{
				ProvisioningReconciliationConfig provisioningReconciliationConfig = (ProvisioningReconciliationConfig)dataObject;
				if (provisioningReconciliationConfig != null)
				{
					provisioningReconciliationConfig.ReconciliationCookieForCurrentCycle = ProvisioningReconciliationHelper.GetReconciliationCookie(provisioningReconciliationConfig, new Task.TaskErrorLoggingDelegate(base.WriteError));
					provisioningReconciliationConfig.ReconciliationCookiesForNextCycle = ProvisioningReconciliationHelper.GetReconciliationCookiesForNextCycle(provisioningReconciliationConfig.ReconciliationCookieForCurrentCycle.DCHostName, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				base.WriteResult(provisioningReconciliationConfig);
				return;
			}
			base.WriteResult(dataObject);
		}
	}
}
