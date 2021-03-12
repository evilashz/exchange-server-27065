using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000080 RID: 128
	[Cmdlet("Retry", "Queue", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	[LocDescription(QueueViewerStrings.IDs.RetryQueueTask)]
	public sealed class RetryQueue : QueueAction
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00011091 File Offset: 0x0000F291
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageRetryQueueFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageRetryQueueIdentity(base.Identity.ToString());
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000110C6 File Offset: 0x0000F2C6
		public RetryQueue()
		{
			this.Resubmit = false;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000110D5 File Offset: 0x0000F2D5
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000110EC File Offset: 0x0000F2EC
		[Parameter(Mandatory = false)]
		public bool Resubmit
		{
			get
			{
				return (bool)base.Fields["Resubmit"];
			}
			set
			{
				base.Fields["Resubmit"] = value;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00011104 File Offset: 0x0000F304
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleQueueInfo> queueViewerClient = new QueueViewerClient<ExtensibleQueueInfo>((string)base.Server))
			{
				queueViewerClient.RetryQueue(base.Identity, this.innerFilter, this.Resubmit);
			}
		}
	}
}
