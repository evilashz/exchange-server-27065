using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001D RID: 29
	internal static class Utils
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00009814 File Offset: 0x00007A14
		public static void SubmitMailItem(TransportMailItem mailItem, bool suppressDSNs)
		{
			try
			{
				TraceHelper.StoreDriverDeliveryTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Start sending email generated by delivery agent.");
				if (suppressDSNs)
				{
					foreach (MailRecipient mailRecipient in mailItem.Recipients)
					{
						mailRecipient.DsnRequested = DsnRequestedFlags.Never;
					}
				}
				SmtpMailItemResult smtpMailItemResult = SmtpMailItemSender.Instance.Send(new MbxTransportMailItem(mailItem));
				if (smtpMailItemResult.ConnectionResponse.AckStatus != AckStatus.Success || smtpMailItemResult.MessageResponse.AckStatus != AckStatus.Success)
				{
					TraceHelper.StoreDriverDeliveryTracer.TraceFail<AckStatus, AckStatus>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Failure sending email generated by delivery agent. ConnectionResponse {0} MessageResponse {1}", smtpMailItemResult.ConnectionResponse.AckStatus, smtpMailItemResult.MessageResponse.AckStatus);
					throw new StoreDriverAgentTransientException(Strings.StoreDriverAgentTransientExceptionEmail);
				}
				SubmitHelper.AgentMessageCounter.AddValue(1L);
			}
			catch (Exception ex)
			{
				TraceHelper.StoreDriverDeliveryTracer.TraceFail<string>(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Failure sending email generated by delivery agent. Error {0}", ex.ToString());
				throw new StoreDriverAgentTransientException(Strings.StoreDriverAgentTransientExceptionEmail, ex);
			}
			TraceHelper.StoreDriverDeliveryTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "Delivery: Completed sending email generated by delivery agent.");
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009938 File Offset: 0x00007B38
		public static string RedactRoutingAddressIfNecessary(RoutingAddress address, bool isNecessary)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("address.ToString()", address.ToString());
			if (isNecessary)
			{
				return SuppressingPiiData.Redact(address).ToString();
			}
			return address.ToString();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009981 File Offset: 0x00007B81
		public static bool IsRedactionNecessary()
		{
			return MultiTenantTransport.MultiTenancyEnabled;
		}
	}
}
