using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007F RID: 127
	[LocDescription(QueueViewerStrings.IDs.UnfreezeQueueTask)]
	[Cmdlet("Resume", "Queue", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class UnfreezeQueue : QueueAction
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00011008 File Offset: 0x0000F208
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleQueueInfo> queueViewerClient = new QueueViewerClient<ExtensibleQueueInfo>((string)base.Server))
			{
				queueViewerClient.UnfreezeQueue(base.Identity, this.innerFilter);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00011054 File Offset: 0x0000F254
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageResumeQueueFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageResumeQueueIdentity(base.Identity.ToString());
			}
		}
	}
}
