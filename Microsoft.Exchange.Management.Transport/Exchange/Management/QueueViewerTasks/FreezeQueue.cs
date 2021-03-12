using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007E RID: 126
	[LocDescription(QueueViewerStrings.IDs.FreezeQueueTask)]
	[Cmdlet("Suspend", "Queue", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class FreezeQueue : QueueAction
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x00010F7C File Offset: 0x0000F17C
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleQueueInfo> queueViewerClient = new QueueViewerClient<ExtensibleQueueInfo>((string)base.Server))
			{
				queueViewerClient.FreezeQueue(base.Identity, this.innerFilter);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageSuspendQueueFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageSuspendQueueIdentity(base.Identity.ToString());
			}
		}
	}
}
