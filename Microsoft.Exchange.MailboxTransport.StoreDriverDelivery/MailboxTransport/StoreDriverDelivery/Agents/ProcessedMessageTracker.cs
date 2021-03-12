using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000075 RID: 117
	internal class ProcessedMessageTracker
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x00014DDF File Offset: 0x00012FDF
		public ProcessedMessageTracker()
		{
			this.processedMessages = new ConcurrentDictionary<string, DeliveryStage>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014E08 File Offset: 0x00013008
		public void AddMessageToProcessedList(string messageId, ExDateTime sentTime, Guid mailboxGuid, DeliveryStage stage)
		{
			string key2 = this.GenerateUniqueId(messageId, sentTime, mailboxGuid);
			this.processedMessages.AddOrUpdate(key2, stage, (string key, DeliveryStage oldValue) => stage);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014E4C File Offset: 0x0001304C
		public DeliveryStage ClearMessageFromProcessedList(string messageId, ExDateTime sentTime, Guid mailboxGuid)
		{
			string key = this.GenerateUniqueId(messageId, sentTime, mailboxGuid);
			DeliveryStage result = DeliveryStage.None;
			this.processedMessages.TryRemove(key, out result);
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014E78 File Offset: 0x00013078
		public bool IsAlreadyProcessedForStage(string messageId, ExDateTime sentTime, Guid mailboxGuid, DeliveryStage checkStage)
		{
			string key = this.GenerateUniqueId(messageId, sentTime, mailboxGuid);
			DeliveryStage deliveryStage = DeliveryStage.None;
			this.processedMessages.TryGetValue(key, out deliveryStage);
			return deliveryStage >= checkStage;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014EA8 File Offset: 0x000130A8
		private string GenerateUniqueId(string messageId, ExDateTime sentTime, Guid mailboxGuid)
		{
			return string.Format("{0}:{1}:{2}", messageId ?? "null", sentTime.ToISOString(), mailboxGuid);
		}

		// Token: 0x04000261 RID: 609
		private ConcurrentDictionary<string, DeliveryStage> processedMessages;
	}
}
