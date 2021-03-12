using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B49 RID: 2889
	[Cmdlet("Remove", "SendConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSendConnector : RemoveSystemConfigurationObjectTask<SendConnectorIdParameter, SmtpSendConnectorConfig>
	{
		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x060068BB RID: 26811 RVA: 0x001AF82E File Offset: 0x001ADA2E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.DataObject.IsInitialSendConnector())
				{
					return Strings.ConfirmationMessageRemoveEdgeSyncSendConnector(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveSendConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x001AF860 File Offset: 0x001ADA60
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			Server server = null;
			try
			{
				server = ((ITopologyConfigurationSession)base.DataSession).FindLocalServer();
			}
			catch (ComputerNameNotCurrentlyAvailableException)
			{
			}
			catch (LocalServerNotFoundException)
			{
			}
			if (server == null || !server.IsEdgeServer)
			{
				ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, base.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.ThrowTerminatingError));
			}
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x001AF8DC File Offset: 0x001ADADC
		protected override void InternalValidate()
		{
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
		}
	}
}
