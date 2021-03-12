using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000079 RID: 121
	[Cmdlet("Remove", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	[LocDescription(QueueViewerStrings.IDs.RemoveMessageTask)]
	public sealed class RemoveMessage : MessageActionWithFilter
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x000105AC File Offset: 0x0000E7AC
		public RemoveMessage()
		{
			this.WithNDR = true;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000105BB File Offset: 0x0000E7BB
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x000105D2 File Offset: 0x0000E7D2
		[Parameter(Mandatory = false)]
		public bool WithNDR
		{
			get
			{
				return (bool)base.Fields["WithNDR"];
			}
			set
			{
				base.Fields["WithNDR"] = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000105EA File Offset: 0x0000E7EA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageRemoveMessageFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageRemoveMessageIdentity(base.Identity.ToString());
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00010620 File Offset: 0x0000E820
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)base.Server))
			{
				queueViewerClient.DeleteMessage(base.Identity, this.innerFilter, this.WithNDR);
			}
		}
	}
}
