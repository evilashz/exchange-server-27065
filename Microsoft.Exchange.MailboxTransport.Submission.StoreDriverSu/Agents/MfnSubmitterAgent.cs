using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Agents
{
	// Token: 0x02000006 RID: 6
	internal class MfnSubmitterAgent : SubmissionAgent
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003660 File Offset: 0x00001860
		public MfnSubmitterAgent()
		{
			base.OnDemotedMessage += this.OnDemotedMessageHandler;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000367C File Offset: 0x0000187C
		public void OnDemotedMessageHandler(StoreDriverEventSource source, StoreDriverSubmissionEventArgs args)
		{
			StoreDriverSubmissionEventArgsImpl storeDriverSubmissionEventArgsImpl = (StoreDriverSubmissionEventArgsImpl)args;
			if (MfnSubmitterAgent.ShouldGenerateMfn(storeDriverSubmissionEventArgsImpl.SubmissionItem.MessageClass))
			{
				using (MfnSubmitter mfnSubmitter = new MfnSubmitter(storeDriverSubmissionEventArgsImpl.SubmissionItem, storeDriverSubmissionEventArgsImpl.MailItemSubmitter))
				{
					TransportMailItem originalMailItem = null;
					TransportMailItemWrapper transportMailItemWrapper = args.MailItem as TransportMailItemWrapper;
					if (transportMailItemWrapper != null)
					{
						originalMailItem = transportMailItemWrapper.TransportMailItem;
					}
					mfnSubmitter.CheckAndSubmitMfn(originalMailItem);
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000036F0 File Offset: 0x000018F0
		private static bool ShouldGenerateMfn(string messageClass)
		{
			return ObjectClass.IsMeetingRequest(messageClass) || (ObjectClass.IsMeetingRequestSeries(messageClass) && SubmissionConfiguration.Instance.App.EnableSeriesMessageProcessing);
		}
	}
}
