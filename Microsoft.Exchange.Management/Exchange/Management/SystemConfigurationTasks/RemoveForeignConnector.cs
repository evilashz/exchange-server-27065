using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B44 RID: 2884
	[Cmdlet("Remove", "ForeignConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveForeignConnector : RemoveSystemConfigurationObjectTask<ForeignConnectorIdParameter, ForeignConnector>
	{
		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x060068AC RID: 26796 RVA: 0x001AF68B File Offset: 0x001AD88B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveForeignConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x001AF6A0 File Offset: 0x001AD8A0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			try
			{
				ForeignConnectorTaskUtil.CheckTopology();
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, base.DataObject);
			}
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x001AF6DC File Offset: 0x001AD8DC
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			ManageSendConnectors.UpdateGwartLastModified((ITopologyConfigurationSession)base.DataSession, base.DataObject.SourceRoutingGroup, new ManageSendConnectors.ThrowTerminatingErrorDelegate(base.WriteError));
		}
	}
}
