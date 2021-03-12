using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B04 RID: 2820
	[Cmdlet("Install", "ProvisioningReconciliationConfig")]
	public sealed class InstallProvisioningReconciliationConfig : NewFixedNameSystemConfigurationObjectTask<ProvisioningReconciliationConfig>
	{
		// Token: 0x06006445 RID: 25669 RVA: 0x001A2BAC File Offset: 0x001A0DAC
		protected override IConfigurable PrepareDataObject()
		{
			ProvisioningReconciliationConfig provisioningReconciliationConfig = (ProvisioningReconciliationConfig)base.PrepareDataObject();
			provisioningReconciliationConfig.SetId((IConfigurationSession)base.DataSession, ProvisioningReconciliationConfig.CanonicalName);
			return provisioningReconciliationConfig;
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x001A2BDC File Offset: 0x001A0DDC
		protected override void InternalProcessRecord()
		{
			ProvisioningReconciliationConfig[] array = this.ConfigurationSession.Find<ProvisioningReconciliationConfig>(null, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				base.InternalProcessRecord();
			}
		}
	}
}
