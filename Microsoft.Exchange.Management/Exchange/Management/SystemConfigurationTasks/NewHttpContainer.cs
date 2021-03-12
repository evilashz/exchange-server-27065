using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C2 RID: 2498
	[Cmdlet("New", "HttpContainer")]
	public sealed class NewHttpContainer : NewFixedNameSystemConfigurationObjectTask<HttpContainer>
	{
		// Token: 0x060058F6 RID: 22774 RVA: 0x00174F2B File Offset: 0x0017312B
		public NewHttpContainer()
		{
			this.serverName = Environment.MachineName;
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x00174F40 File Offset: 0x00173140
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.DataObject.Name = NewHttpContainer.httpContainer;
			ADObjectId orgContainerId = ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId(NewHttpContainer.adminGroupContainer).GetChildId(NewHttpContainer.adminGroup).GetChildId(NewHttpContainer.serversContainer).GetChildId(this.serverName).GetChildId(NewHttpContainer.protocolsContainer).GetChildId(NewHttpContainer.httpContainer);
			this.DataObject.SetId(childId);
			return this.DataObject;
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x00174FC8 File Offset: 0x001731C8
		protected override void InternalProcessRecord()
		{
			ADObjectId orgContainerId = ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId(NewHttpContainer.adminGroupContainer).GetChildId(NewHttpContainer.adminGroup).GetChildId(NewHttpContainer.serversContainer).GetChildId(this.serverName);
			ProtocolsContainer protocolsContainer = new ProtocolsContainer();
			ADObjectId childId2 = childId.GetChildId(NewHttpContainer.protocolsContainer);
			protocolsContainer.SetId(childId2);
			try
			{
				base.DataSession.Save(protocolsContainer);
			}
			catch (ADObjectAlreadyExistsException)
			{
			}
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADOperationException)
			{
			}
		}

		// Token: 0x04003301 RID: 13057
		private static readonly string adminGroupContainer = "Administrative Groups";

		// Token: 0x04003302 RID: 13058
		private static readonly string serversContainer = "Servers";

		// Token: 0x04003303 RID: 13059
		private static readonly string protocolsContainer = "Protocols";

		// Token: 0x04003304 RID: 13060
		private static readonly string httpContainer = "HTTP";

		// Token: 0x04003305 RID: 13061
		private static readonly string adminGroup = AdministrativeGroup.DefaultName;

		// Token: 0x04003306 RID: 13062
		private readonly string serverName;
	}
}
