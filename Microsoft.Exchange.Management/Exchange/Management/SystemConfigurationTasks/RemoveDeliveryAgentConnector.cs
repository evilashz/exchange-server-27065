using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B43 RID: 2883
	[Cmdlet("Remove", "DeliveryAgentConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveDeliveryAgentConnector : RemoveSystemConfigurationObjectTask<DeliveryAgentConnectorIdParameter, DeliveryAgentConnector>
	{
		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x060068A9 RID: 26793 RVA: 0x001AF642 File Offset: 0x001AD842
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDeliveryAgentConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x001AF654 File Offset: 0x001AD854
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, base.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}
	}
}
