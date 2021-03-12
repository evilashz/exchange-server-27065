using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000078 RID: 120
	[LocDescription(QueueViewerStrings.IDs.UnfreezeMessageTask)]
	[Cmdlet("Resume", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class UnfreezeMessage : MessageActionWithFilter
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00010520 File Offset: 0x0000E720
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageResumeMessageFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageResumeMessageIdentity(base.Identity.ToString());
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010558 File Offset: 0x0000E758
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)base.Server))
			{
				queueViewerClient.UnfreezeMessage(base.Identity, this.innerFilter);
			}
		}
	}
}
