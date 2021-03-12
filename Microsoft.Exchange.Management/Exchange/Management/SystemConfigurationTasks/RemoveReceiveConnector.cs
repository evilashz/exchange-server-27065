using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B48 RID: 2888
	[Cmdlet("Remove", "ReceiveConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveReceiveConnector : RemoveSystemConfigurationObjectTask<ReceiveConnectorIdParameter, ReceiveConnector>
	{
		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x060068B9 RID: 26809 RVA: 0x001AF814 File Offset: 0x001ADA14
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveReceiveConnector(this.Identity.ToString());
			}
		}
	}
}
