using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000301 RID: 769
	internal class AgentErrorHandlingNdrAction : IErrorHandlingAction
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x0007FA6C File Offset: 0x0007DC6C
		public AgentErrorHandlingNdrAction(SmtpResponse response)
		{
			this.Response = response;
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0007FA7B File Offset: 0x0007DC7B
		public ErrorHandlingActionType ActionType
		{
			get
			{
				return ErrorHandlingActionType.NDR;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0007FA7E File Offset: 0x0007DC7E
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0007FA86 File Offset: 0x0007DC86
		public SmtpResponse Response { get; private set; }

		// Token: 0x060021AC RID: 8620 RVA: 0x0007FA90 File Offset: 0x0007DC90
		public void TakeAction(QueuedMessageEventSource source, MailItem mailItem)
		{
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			if (recipients == null || recipients.Count == 0)
			{
				ExTraceGlobals.ExtensibilityTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Error Handler: Mssage id '{0} has no recipients to NDR", mailItem.Message.MessageId);
				return;
			}
			TransportMailItemWrapper transportMailItemWrapper = mailItem as TransportMailItemWrapper;
			if (transportMailItemWrapper == null)
			{
				throw new ArgumentException("Can't map mailitem to a TransportMailItemWrapper");
			}
			TransportMailItem transportMailItem = transportMailItemWrapper.TransportMailItem;
			transportMailItem.SuppressBodyInDsn = true;
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				ExTraceGlobals.ExtensibilityTracer.TraceInformation<string, string>(0, (long)this.GetHashCode(), "Error Handler: dropping message id '{0} for recipient '{1}'", mailItem.Message.MessageId, recipients[i].Address.ToString());
				mailItem.Recipients.Remove(recipients[i], DsnType.Failure, this.Response, "Error triggered action.");
			}
		}
	}
}
