using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000077 RID: 119
	[LocDescription(QueueViewerStrings.IDs.FreezeMessageTask)]
	[Cmdlet("Suspend", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class FreezeMessage : MessageActionWithFilter
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00010494 File Offset: 0x0000E694
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageSuspendMessageFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageSuspendMessageIdentity(base.Identity.ToString());
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000104CC File Offset: 0x0000E6CC
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)base.Server))
			{
				queueViewerClient.FreezeMessage(base.Identity, this.innerFilter);
			}
		}
	}
}
