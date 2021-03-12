using System;
using System.IO;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000006 RID: 6
	internal sealed class SentItemWrapperCreator : ISentItemWrapperCreator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		internal SentItemWrapperCreator(ITracer tracer)
		{
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			this.tracer = tracer;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		public Exception CreateAndSubmit(MailItem mailItem, int traceId)
		{
			this.tracer.TraceDebug((long)traceId, "Create the wrapper message with the original message as an attachment.");
			ITransportMailItemFacade sentItemWrapperMessage = this.CreateWrapperMessageForTheSentItem(mailItem);
			this.tracer.TraceDebug((long)traceId, "Submit the wrapper message to transport.");
			return this.SubmitWrapperMessage(sentItemWrapperMessage, mailItem);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		private static void WriteBody(EmailMessage sentItemWrapperMessage)
		{
			Stream contentWriteStream = sentItemWrapperMessage.RootPart.GetContentWriteStream(ContentTransferEncoding.Base64);
			using (TextWriter textWriter = new StreamWriter(contentWriteStream))
			{
				textWriter.Write(AgentStrings.WrapperMessageBody);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		private ITransportMailItemFacade CreateWrapperMessageForTheSentItem(MailItem transportMailItem)
		{
			EmailMessage message = transportMailItem.Message;
			ITransportMailItemFacade transportMailItemFacade = TransportFacades.NewMailItem(((ITransportMailItemWrapperFacade)transportMailItem).TransportMailItem);
			transportMailItemFacade.Recipients.AddWithoutDsnRequested(message.From.SmtpAddress);
			transportMailItemFacade.From = new RoutingAddress(message.From.SmtpAddress);
			transportMailItemFacade.Message.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-SharedMailbox-SentItem-Message", "True"));
			transportMailItemFacade.Message.Subject = AgentStrings.WrapperMessageSubjectFormat(message.Subject);
			SentItemWrapperCreator.WriteBody(transportMailItemFacade.Message);
			Attachment attachment = transportMailItemFacade.Message.Attachments.Add(null, "message/rfc822");
			attachment.EmbeddedMessage = message;
			TransportFacades.EnsureSecurityAttributes(transportMailItemFacade);
			return transportMailItemFacade;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002274 File Offset: 0x00000474
		private Exception SubmitWrapperMessage(ITransportMailItemFacade sentItemWrapperMessage, MailItem originalMailItem)
		{
			IAsyncResult asyncResult = sentItemWrapperMessage.BeginCommitForReceive(new AsyncCallback(this.OnMailSubmitted), this);
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			Exception ex;
			sentItemWrapperMessage.EndCommitForReceive(asyncResult, out ex);
			if (ex == null)
			{
				TransportFacades.CategorizerComponent.EnqueueSideEffectMessage(((ITransportMailItemWrapperFacade)originalMailItem).TransportMailItem, sentItemWrapperMessage, "SharedMailboxSentItemsRoutingAgent");
			}
			return ex;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022D2 File Offset: 0x000004D2
		private void OnMailSubmitted(IAsyncResult ar)
		{
		}

		// Token: 0x04000002 RID: 2
		private readonly ITracer tracer;
	}
}
