using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Agents
{
	// Token: 0x02000002 RID: 2
	internal class ApprovalSubmitterAgent : SubmissionAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ApprovalSubmitterAgent()
		{
			base.OnDemotedMessage += this.OnDemotedMessageHandler;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		public void OnDemotedMessageHandler(StoreDriverEventSource source, StoreDriverSubmissionEventArgs args)
		{
			MailItem mailItem = args.MailItem;
			StoreDriverSubmissionEventArgsImpl storeDriverSubmissionEventArgsImpl = (StoreDriverSubmissionEventArgsImpl)args;
			MessageItem item = storeDriverSubmissionEventArgsImpl.SubmissionItem.Item;
			if (ApprovalInitiation.IsArbitrationMailbox((ADRecipientCache<TransportMiniRecipient>)mailItem.RecipientCache, mailItem.FromAddress))
			{
				item.Load(ApprovalSubmitterAgent.ModeratedTransportProperties);
				ApprovalStatus? valueAsNullable = item.GetValueAsNullable<ApprovalStatus>(MessageItemSchema.ApprovalStatus);
				string valueOrDefault = item.GetValueOrDefault<string>(MessageItemSchema.ApprovalDecisionMaker, string.Empty);
				if (valueAsNullable != null && (valueAsNullable.Value & ApprovalStatus.Approved) == ApprovalStatus.Approved)
				{
					TransportMailItem transportMailItem = ((TransportMailItemWrapper)mailItem).TransportMailItem;
					ModeratedTransportHandling.ResubmitApprovedMessage(item, transportMailItem, valueOrDefault);
					return;
				}
				if (ObjectClass.IsOfClass(item.ClassName, "IPM.Microsoft.Approval.Initiation") || ObjectClass.IsOfClass(item.ClassName, "IPM.Note.Microsoft.Approval.Request.Recall"))
				{
					Header header = (TextHeader)Header.Create("X-MS-Exchange-Organization-Do-Not-Journal");
					header.Value = "ArbitrationMailboxSubmission";
					mailItem.MimeDocument.RootPart.Headers.AppendChild(header);
					Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Approval-Initiator", "mapi");
					mailItem.MimeDocument.RootPart.Headers.AppendChild(newChild);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string DoNotJournalIdentifier = "ArbitrationMailboxSubmission";

		// Token: 0x04000002 RID: 2
		private const string MapiApprovalInitiator = "mapi";

		// Token: 0x04000003 RID: 3
		internal static readonly PropertyDefinition[] ModeratedTransportProperties = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalDecisionMaker,
			MessageItemSchema.ApprovalStatus
		};
	}
}
