using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors
{
	// Token: 0x020000D4 RID: 212
	internal class DeletePartitionProcessor : AbstractActionProcessor
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x00023C31 File Offset: 0x00021E31
		public DeletePartitionProcessor(ActionProcessorFactory actionProcessorFactory) : base(actionProcessorFactory)
		{
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00023C3C File Offset: 0x00021E3C
		public override void Process(StoreDriverDeliveryEventArgsImpl argsImpl, Action action, int traceId)
		{
			AbstractActionProcessor.Tracer.TraceDebug((long)traceId, "WorkingSetAgent.DeletePartitionProcessor.Process: entering");
			MailboxSession mailboxSession = argsImpl.MailboxSession;
			string workingSetSourcePartitionInternal = WorkingSetUtils.GetWorkingSetSourcePartitionInternal(action.Item);
			AbstractActionProcessor.Tracer.TraceDebug<string>((long)traceId, "WorkingSetAgent.DeletePartitionProcessor.Process: partition to delete - {0}", workingSetSourcePartitionInternal);
			WorkingSetUtils.DeleteWorkingSetPartition(mailboxSession, workingSetSourcePartitionInternal);
			AbstractActionProcessor.Tracer.TraceDebug<string>((long)traceId, "WorkingSetAgent.DeletePartitionProcessor.Process: partition is deleted - {0}", workingSetSourcePartitionInternal);
			argsImpl.MailRecipient.DsnRequested = DsnRequestedFlags.Never;
			throw new SmtpResponseException(DeletePartitionProcessor.processedDeletePartitionSignalMailResponse, this.actionProcessorFactory.WorkingSetAgent.Name);
		}

		// Token: 0x04000380 RID: 896
		private static readonly SmtpResponse processedDeletePartitionSignalMailResponse = new SmtpResponse("250", "2.1.6", new string[]
		{
			"WorkingSetAgent; Suppressing delivery of delete partition Signal mail"
		});
	}
}
