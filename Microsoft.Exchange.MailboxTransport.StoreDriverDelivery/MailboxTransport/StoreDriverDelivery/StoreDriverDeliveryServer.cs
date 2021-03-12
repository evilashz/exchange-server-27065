using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000019 RID: 25
	internal class StoreDriverDeliveryServer : StoreDriverServer
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00009037 File Offset: 0x00007237
		private StoreDriverDeliveryServer(OrganizationId organizationId) : base(organizationId)
		{
			this.agentLoopChecker = new AgentGeneratedMessageLoopChecker(new AgentGeneratedMessageLoopCheckerTransportConfig(Components.Configuration));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009055 File Offset: 0x00007255
		public new static StoreDriverDeliveryServer GetInstance(OrganizationId organizationId)
		{
			return new StoreDriverDeliveryServer(organizationId);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000905D File Offset: 0x0000725D
		public override void SubmitMessage(EmailMessage message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009064 File Offset: 0x00007264
		public override void SubmitMessage(IReadOnlyMailItem originalMailItem, EmailMessage message, OrganizationId organizationId, Guid externalOrganizationId, bool suppressDSNs)
		{
			try
			{
				TransportMailItem mailItem = SubmitHelper.CreateTransportMailItem(originalMailItem, message, this.Name, this.Version, (base.AssociatedAgent != null) ? base.AssociatedAgent.Name : "StoreDriverServer", null, null, organizationId, externalOrganizationId, false);
				this.SubmitMailItem(mailItem, suppressDSNs);
			}
			catch (Exception innerException)
			{
				throw new StoreDriverAgentTransientException(Strings.StoreDriverAgentTransientExceptionEmail, innerException);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000090D8 File Offset: 0x000072D8
		public override void SubmitMailItem(TransportMailItem mailItem, bool suppressDSNs)
		{
			StoreDriverDeliveryEventArgs storeDriverDeliveryEventArgs = null;
			if (base.AssociatedAgent != null && base.AssociatedAgent.Session != null && base.AssociatedAgent.Session.CurrentEventArgs != null)
			{
				storeDriverDeliveryEventArgs = (base.AssociatedAgent.Session.CurrentEventArgs as StoreDriverDeliveryEventArgs);
			}
			bool flag = this.agentLoopChecker.IsEnabledInSubmission();
			bool flag2 = false;
			if (storeDriverDeliveryEventArgs != null && !string.IsNullOrEmpty(base.AssociatedAgent.Name))
			{
				flag2 = this.agentLoopChecker.CheckAndStampInSubmission(storeDriverDeliveryEventArgs.MailItem.Message.RootPart.Headers, mailItem.RootPart.Headers, base.AssociatedAgent.Name);
				if (flag2)
				{
					MessageTrackingLog.TrackAgentGeneratedMessageRejected(MessageTrackingSource.STOREDRIVER, flag, mailItem);
				}
			}
			if (flag2 && flag)
			{
				using (IEnumerator<MailRecipient> enumerator = mailItem.Recipients.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MailRecipient mailRecipient = enumerator.Current;
						mailRecipient.Ack(AckStatus.Fail, SmtpResponse.AgentGeneratedMessageDepthExceeded);
					}
					return;
				}
			}
			Utils.SubmitMailItem(mailItem, suppressDSNs);
		}

		// Token: 0x040000B1 RID: 177
		private AgentGeneratedMessageLoopChecker agentLoopChecker;
	}
}
