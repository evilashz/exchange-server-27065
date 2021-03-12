using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200030A RID: 778
	internal class OnLoadedMessageEventSource : StorageEventSource
	{
		// Token: 0x060021DF RID: 8671 RVA: 0x0008019C File Offset: 0x0007E39C
		public OnLoadedMessageEventSource(TransportMailItem mailItem)
		{
			this.mailItem = mailItem;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000801AC File Offset: 0x0007E3AC
		public override void Delete(string sourceContext)
		{
			LatencyFormatter latencyFormatter = new LatencyFormatter(this.mailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, true);
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients)
			{
				MessageTrackingLog.TrackRecipientFail(MessageTrackingSource.AGENT, this.mailItem, mailRecipient.Email, mailRecipient.SmtpResponse, sourceContext, latencyFormatter);
			}
			this.mailItem.ReleaseFromActive();
			this.mailItem.CommitLazy();
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00080248 File Offset: 0x0007E448
		public override void DeleteWithNdr(SmtpResponse response, string sourceContext)
		{
			if (this.mailItem.ADRecipientCache == null)
			{
				ADOperationResult adoperationResult = MultiTenantTransport.TryCreateADRecipientCache(this.mailItem);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError(string.Format("Error {0} when creating recipient cache for message {1}. Falling back to first org", adoperationResult.Exception, MultiTenantTransport.ToString(this.mailItem)), new object[0]);
					MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(this.mailItem, OrganizationId.ForestWideOrgId);
				}
			}
			TransportMailItemWrapper transportMailItemWrapper = new TransportMailItemWrapper(this.mailItem, true);
			EnvelopeRecipientCollection recipients = transportMailItemWrapper.Recipients;
			if (recipients == null || recipients.Count == 0)
			{
				return;
			}
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				transportMailItemWrapper.Recipients.Remove(recipients[i], DsnType.Failure, response, sourceContext);
			}
			Components.DsnGenerator.GenerateDSNs(this.mailItem);
			this.mailItem.ReleaseFromActive();
			this.mailItem.CommitLazy();
		}

		// Token: 0x040011C2 RID: 4546
		private TransportMailItem mailItem;
	}
}
