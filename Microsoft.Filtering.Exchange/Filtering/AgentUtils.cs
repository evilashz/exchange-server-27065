using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Filtering
{
	// Token: 0x02000002 RID: 2
	public static class AgentUtils
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void NdrMessage(MailItem mailItem, SmtpResponse response)
		{
			TransportMailItem transportMailItem = AgentUtils.GetTransportMailItem(mailItem);
			transportMailItem.SuppressBodyInDsn = true;
			foreach (EnvelopeRecipient recipient in mailItem.Recipients)
			{
				mailItem.Recipients.Remove(recipient, DsnType.Failure, response);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000213C File Offset: 0x0000033C
		private static TransportMailItem GetTransportMailItem(MailItem mailItem)
		{
			return (TransportMailItem)((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
		}
	}
}
