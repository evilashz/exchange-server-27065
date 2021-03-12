using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B45 RID: 2885
	[Cmdlet("Remove", "InboundConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveInboundConnector : RemoveSystemConfigurationObjectTask<InboundConnectorIdParameter, TenantInboundConnector>
	{
		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x060068B0 RID: 26800 RVA: 0x001AF713 File Offset: 0x001AD913
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveInboundConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x001AF725 File Offset: 0x001AD925
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<TenantInboundConnector>(this, base.DataObject);
			TaskLogger.LogExit();
		}
	}
}
