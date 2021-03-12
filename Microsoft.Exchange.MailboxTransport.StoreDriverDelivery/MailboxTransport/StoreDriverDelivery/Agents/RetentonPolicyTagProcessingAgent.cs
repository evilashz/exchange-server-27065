using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000089 RID: 137
	internal class RetentonPolicyTagProcessingAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00018376 File Offset: 0x00016576
		public RetentonPolicyTagProcessingAgent()
		{
			base.OnCreatedMessage += this.OnCreatedMessageHandler;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00018390 File Offset: 0x00016590
		public void OnCreatedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			if (RetentonPolicyTagProcessingAgent.IsRetentionPolicyEnabled(storeDriverDeliveryEventArgsImpl.ADRecipientCache, storeDriverDeliveryEventArgsImpl.MailRecipient.Email))
			{
				RetentionTagHelper.ApplyPolicy(storeDriverDeliveryEventArgsImpl.MailboxSession, storeDriverDeliveryEventArgsImpl.RetentionPolicyTag, storeDriverDeliveryEventArgsImpl.RetentionFlags, storeDriverDeliveryEventArgsImpl.RetentionPeriod, storeDriverDeliveryEventArgsImpl.ArchiveTag, storeDriverDeliveryEventArgsImpl.ArchivePeriod, storeDriverDeliveryEventArgsImpl.CompactDefaultRetentionPolicy, storeDriverDeliveryEventArgsImpl.MessageItem);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000183F4 File Offset: 0x000165F4
		internal static bool IsRetentionPolicyEnabled(ADRecipientCache<TransportMiniRecipient> cache, RoutingAddress address)
		{
			ProxyAddress proxyAddress = new SmtpProxyAddress((string)address, true);
			TransportMiniRecipient data = cache.FindAndCacheRecipient(proxyAddress).Data;
			if (data == null)
			{
				return false;
			}
			ElcMailboxFlags elcMailboxFlags = data.ElcMailboxFlags;
			ADObjectId elcPolicyTemplate = data.ElcPolicyTemplate;
			return ((elcMailboxFlags & ElcMailboxFlags.ElcV2) != ElcMailboxFlags.None && elcPolicyTemplate != null) || ((elcMailboxFlags & ElcMailboxFlags.ShouldUseDefaultRetentionPolicy) != ElcMailboxFlags.None && elcPolicyTemplate == null);
		}
	}
}
