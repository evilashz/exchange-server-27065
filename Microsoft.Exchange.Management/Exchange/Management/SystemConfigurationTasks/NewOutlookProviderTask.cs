using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007FA RID: 2042
	[Cmdlet("New", "OutlookProvider", SupportsShouldProcess = true)]
	public sealed class NewOutlookProviderTask : NewMultitenancySystemConfigurationObjectTask<OutlookProvider>
	{
		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x00124AF6 File Offset: 0x00122CF6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOutlookProvider(base.Name.ToString());
			}
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x00124B08 File Offset: 0x00122D08
		public NewOutlookProviderTask()
		{
			this.DataObject.InitializeDefaults();
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x00124B1B File Offset: 0x00122D1B
		protected override ObjectId RootId
		{
			get
			{
				return OutlookProvider.GetParentContainer(base.DataSession as ITopologyConfigurationSession);
			}
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00124B30 File Offset: 0x00122D30
		protected override IConfigurable PrepareDataObject()
		{
			OutlookProvider outlookProvider = (OutlookProvider)base.PrepareDataObject();
			ADObjectId parentContainer = OutlookProvider.GetParentContainer(base.DataSession as ITopologyConfigurationSession);
			outlookProvider.SetId(parentContainer.GetChildId(base.Name));
			return outlookProvider;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00124B6D File Offset: 0x00122D6D
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<OutlookProvider>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
				return;
			}
			this.WriteWarning(Strings.OutlookProviderAlreadyExists(base.Name.ToString()));
		}
	}
}
