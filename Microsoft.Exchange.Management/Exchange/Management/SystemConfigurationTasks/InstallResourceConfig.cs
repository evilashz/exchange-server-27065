using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0B RID: 2827
	[Cmdlet("Install", "ResourceConfig")]
	public sealed class InstallResourceConfig : NewMultitenancyFixedNameSystemConfigurationObjectTask<ResourceBookingConfig>
	{
		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x001A43EC File Offset: 0x001A25EC
		protected override ObjectId RootId
		{
			get
			{
				return ResourceBookingConfig.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x001A43FC File Offset: 0x001A25FC
		protected override IConfigurable PrepareDataObject()
		{
			ResourceBookingConfig resourceBookingConfig = (ResourceBookingConfig)base.PrepareDataObject();
			resourceBookingConfig.SetId(ResourceBookingConfig.GetWellKnownLocation(base.CurrentOrgContainerId));
			return resourceBookingConfig;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x001A4427 File Offset: 0x001A2627
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<ResourceBookingConfig>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
		}
	}
}
