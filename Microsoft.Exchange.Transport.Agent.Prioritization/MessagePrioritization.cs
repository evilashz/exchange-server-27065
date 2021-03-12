using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.Prioritization
{
	// Token: 0x02000004 RID: 4
	internal class MessagePrioritization
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000233F File Offset: 0x0000053F
		public MessagePrioritization() : this(() => DateTime.UtcNow)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002364 File Offset: 0x00000564
		internal MessagePrioritization(Func<DateTime> currentTimeProvider)
		{
			this.anonymousSenderCostBucket = new MessagePrioritization.SenderAccumulatedCostBucket(currentTimeProvider);
			this.internalSenderCostBucket = new MessagePrioritization.SenderAccumulatedCostBucket(currentTimeProvider);
			this.recipientCostBucket = new MessagePrioritization.RecipientAccumulatedCostBucket(currentTimeProvider);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002390 File Offset: 0x00000590
		internal static long MessageSizeThreshold
		{
			get
			{
				return MessagePrioritization.messageSizeThreshold;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002397 File Offset: 0x00000597
		internal static long AnonymousMessageSizeThreshold
		{
			get
			{
				return MessagePrioritization.anonymousMessageSizeThreshold;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000239E File Offset: 0x0000059E
		internal static long RecipientCostLevel1Threshold
		{
			get
			{
				return MessagePrioritization.recipientCostLevel1Threshold;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023A5 File Offset: 0x000005A5
		internal static long RecipientCostLevel2Threshold
		{
			get
			{
				return MessagePrioritization.recipientCostLevel2Threshold;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023AC File Offset: 0x000005AC
		internal static long AnonymousRecipientCostLevel1Threshold
		{
			get
			{
				return MessagePrioritization.anonymousRecipientCostLevel1Threshold;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023B3 File Offset: 0x000005B3
		internal static long AnonymousRecipientCostLevel2Threshold
		{
			get
			{
				return MessagePrioritization.anonymousRecipientCostLevel2Threshold;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023BA File Offset: 0x000005BA
		internal static TimeSpan SlidingCounterWindowLength
		{
			get
			{
				return MessagePrioritization.slidingCounterWindowLength;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023C1 File Offset: 0x000005C1
		internal static TimeSpan SlidingCounterBucketLength
		{
			get
			{
				return MessagePrioritization.slidingCounterBucketLength;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023C8 File Offset: 0x000005C8
		public void PrioritizeMessage(bool isInternalSender, string fromAddress, long messageSize, int recipientCost, MailItem mailItem, Trace tracer, out DeliveryPriority priority, out string reason)
		{
			this.PrioritizeSenderMessage(isInternalSender, fromAddress, mailItem.CachedMimeStreamLength, recipientCost, out priority, out reason);
			if (DeliveryPriority.Low == priority || DeliveryPriority.None == priority)
			{
				tracer.TraceDebug((long)this.GetHashCode(), "Skip recipient prioritization since message is already low or none priority.");
				return;
			}
			this.PrioritizeRecipientMessage(mailItem, tracer, out priority, out reason);
			if (DeliveryPriority.Low != priority || DeliveryPriority.None != priority)
			{
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					if (!RoutingAddress.NullReversePath.Equals(envelopeRecipient.Address) && !RoutingAddress.Empty.Equals(envelopeRecipient.Address) && !envelopeRecipient.IsPublicFolderRecipient())
					{
						this.UpdateRecipientCounter(envelopeRecipient.Address.ToString());
					}
				}
			}
			this.recipientCostBucket.ClearStaleEntries();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024BC File Offset: 0x000006BC
		public void PrioritizeSenderMessage(bool isInternal, string sender, long messageSize, int recipientCost, out DeliveryPriority priority, out string reason)
		{
			if (string.IsNullOrEmpty(sender))
			{
				throw new ArgumentException("Sender is null or empty.");
			}
			long totalMessageSize;
			long totalRecipientCost;
			long num;
			long num2;
			long num3;
			if (isInternal)
			{
				this.internalSenderCostBucket.CalculateCost(sender, messageSize, recipientCost, out totalMessageSize, out totalRecipientCost);
				num = MessagePrioritization.messageSizeThreshold;
				num2 = MessagePrioritization.recipientCostLevel1Threshold;
				num3 = MessagePrioritization.recipientCostLevel2Threshold;
			}
			else
			{
				this.anonymousSenderCostBucket.CalculateCost(sender, messageSize, recipientCost, out totalMessageSize, out totalRecipientCost);
				num = MessagePrioritization.anonymousMessageSizeThreshold;
				num2 = MessagePrioritization.anonymousRecipientCostLevel1Threshold;
				num3 = MessagePrioritization.anonymousRecipientCostLevel2Threshold;
			}
			priority = MessagePrioritization.GetSenderPriority(totalMessageSize, totalRecipientCost, num, num2, num3);
			reason = MessagePrioritization.GetSenderPrioritizationReason(totalMessageSize, totalRecipientCost, num, num2, num3);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000254C File Offset: 0x0000074C
		public void PrioritizeRecipientMessage(MailItem mailItem, Trace tracer, out DeliveryPriority priority, out string reason)
		{
			int hashCode = this.GetHashCode();
			int count = mailItem.Recipients.Count;
			priority = DeliveryPriority.Normal;
			reason = null;
			if (count == 0)
			{
				tracer.TraceDebug((long)hashCode, "Skip recipient prioritization since there are no P1 recipients on the message");
				return;
			}
			string inboxRulePropertyValue = MessagePrioritization.TryGetStringProperty(mailItem.Properties, "Microsoft.Exchange.Transport.GeneratedByMailboxRule");
			HeaderList headers = mailItem.MimeDocument.RootPart.Headers;
			Header approvalInitiatorHeader = headers.FindFirst("X-MS-Exchange-Organization-Approval-Initiator");
			string approvalInitiatorTransportRuleName = MessagePrioritization.TryGetStringProperty(mailItem.Properties, "Microsoft.Exchange.Transport.ModeratedByTransportRule");
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				if (RoutingAddress.NullReversePath.Equals(envelopeRecipient.Address) || RoutingAddress.Empty.Equals(envelopeRecipient.Address))
				{
					tracer.TraceDebug<RoutingAddress>((long)hashCode, "Skip recipient since its address is empty or null reverse path. Address={0}", envelopeRecipient.Address);
				}
				else if (envelopeRecipient.IsPublicFolderRecipient())
				{
					tracer.TraceDebug<RoutingAddress>((long)hashCode, "Skip public folder recipient {0}", envelopeRecipient.Address);
				}
				else
				{
					long totalRecipients = this.recipientCostBucket.CalculateCost(envelopeRecipient.Address.ToString());
					string transportRulePropertyValue = MessagePrioritization.TryGetStringProperty(envelopeRecipient.Properties, "Microsoft.Exchange.Transport.AddedByTransportRule");
					priority = MessagePrioritization.GetRecipientPriority(envelopeRecipient.Address, totalRecipients, MessagePrioritization.RecipientCountLevel1Threshold, MessagePrioritization.RecipientCountLevel2Threshold, count, approvalInitiatorHeader, approvalInitiatorTransportRuleName, inboxRulePropertyValue, transportRulePropertyValue, tracer, hashCode, out reason);
					if (DeliveryPriority.Low == priority || DeliveryPriority.None == priority)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026D8 File Offset: 0x000008D8
		internal static string GetSenderPrioritizationReason(long totalMessageSize, long totalRecipientCost, long messageSizeThreshold, long recipientCostLevel1Threshold, long recipientCostLevel2Threshold)
		{
			return string.Format(CultureInfo.InvariantCulture, "AMS:{0}/{1}|ARC:{2}/{3};{4}", new object[]
			{
				totalMessageSize,
				messageSizeThreshold,
				totalRecipientCost,
				recipientCostLevel1Threshold,
				recipientCostLevel2Threshold
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000272C File Offset: 0x0000092C
		internal static DeliveryPriority GetRecipientPriority(RoutingAddress recipientAddress, long totalRecipients, long totalRecipientsLevel1Threshold, long totalRecipientsLevel2Threshold, int recipientCountOnMailItem, Header approvalInitiatorHeader, string approvalInitiatorTransportRuleName, string inboxRulePropertyValue, string transportRulePropertyValue, Trace tracer, int hashCode, out string reason)
		{
			reason = null;
			DeliveryPriority deliveryPriority = DeliveryPriority.Normal;
			bool flag = totalRecipients > totalRecipientsLevel1Threshold;
			if (flag)
			{
				DeliveryPriority deliveryPriority2 = DeliveryPriority.Low;
				string text = null;
				if (1 == recipientCountOnMailItem)
				{
					tracer.TraceDebug((long)hashCode, "Set priority: {0} due to single recipient on message. Recipient={1}, RecipientCount={2}, ThresholdLevel1={3}, ThresholdLevel2={4}.", new object[]
					{
						deliveryPriority2,
						recipientAddress,
						totalRecipients,
						totalRecipientsLevel1Threshold,
						totalRecipientsLevel2Threshold
					});
					text = "|SR";
					deliveryPriority = deliveryPriority2;
				}
				string text2 = null;
				if (approvalInitiatorHeader != null)
				{
					tracer.TraceDebug((long)hashCode, "Set priority: {0} due to arbitration initiator message. Recipient={1}, RecipientCount={2}, ThresholdLevel1={3}, ThresholdLevel2={4}.", new object[]
					{
						deliveryPriority2,
						recipientAddress,
						totalRecipients,
						totalRecipientsLevel1Threshold,
						totalRecipientsLevel2Threshold
					});
					text2 = (string.IsNullOrEmpty(approvalInitiatorTransportRuleName) ? "|AI" : "|AI:");
					deliveryPriority = deliveryPriority2;
				}
				string text3 = null;
				if (!string.IsNullOrEmpty(inboxRulePropertyValue))
				{
					tracer.TraceDebug((long)hashCode, "Set priority: {0} due to inbox rule generated message. Recipient={1}, RecipientCount={2}, ThresholdLevel1={3}, ThresholdLevel2={4}, InboxRule={5}.", new object[]
					{
						deliveryPriority2,
						recipientAddress,
						totalRecipients,
						totalRecipientsLevel1Threshold,
						totalRecipientsLevel2Threshold,
						inboxRulePropertyValue
					});
					text3 = "|IR:";
					deliveryPriority = deliveryPriority2;
				}
				string text4 = null;
				if (!string.IsNullOrEmpty(transportRulePropertyValue))
				{
					tracer.TraceDebug((long)hashCode, "Set priority: {0} due to recipient added by transport rule. Recipient={1}, RecipientCount={2}, ThresholdLevel1={3}, ThresholdLevel2={4}, TransportRule={5}.", new object[]
					{
						deliveryPriority2,
						recipientAddress,
						totalRecipients,
						totalRecipientsLevel1Threshold,
						totalRecipientsLevel2Threshold,
						transportRulePropertyValue
					});
					text4 = "|TR:";
					deliveryPriority = deliveryPriority2;
				}
				if (deliveryPriority2 == deliveryPriority)
				{
					reason = string.Format(CultureInfo.InvariantCulture, "RC:{0}:{1}/{2};{3}{4}{5}{6}{7}{8}{9}{10}", new object[]
					{
						SuppressingPiiData.Redact(recipientAddress),
						totalRecipients,
						totalRecipientsLevel1Threshold,
						totalRecipientsLevel2Threshold,
						text,
						text2,
						approvalInitiatorTransportRuleName,
						text3,
						inboxRulePropertyValue,
						text4,
						transportRulePropertyValue
					});
				}
			}
			return deliveryPriority;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002957 File Offset: 0x00000B57
		internal void UpdateRecipientCounter(string recipient)
		{
			this.recipientCostBucket.UpdateCounter(recipient);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002968 File Offset: 0x00000B68
		private static string TryGetStringProperty(IDictionary<string, object> propertyMap, string key)
		{
			string result = null;
			object obj;
			if (propertyMap.TryGetValue(key, out obj))
			{
				result = (string)obj;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000298A File Offset: 0x00000B8A
		private static DeliveryPriority GetSenderPriority(long totalMessageSize, long totalRecipientCost, long messageSizeThreshold, long recipientCostLevel1Threshold, long recipientCostLevel2Threshold)
		{
			if (totalRecipientCost > recipientCostLevel2Threshold)
			{
				return DeliveryPriority.Low;
			}
			if (totalMessageSize > messageSizeThreshold || totalRecipientCost > recipientCostLevel1Threshold)
			{
				return DeliveryPriority.Low;
			}
			return DeliveryPriority.Normal;
		}

		// Token: 0x04000004 RID: 4
		internal static readonly long RecipientCountLevel1Threshold = Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AccumulatedRecipientCountLevel1Threshold;

		// Token: 0x04000005 RID: 5
		internal static readonly long RecipientCountLevel2Threshold = Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AccumulatedRecipientCountLevel2Threshold;

		// Token: 0x04000006 RID: 6
		private static long messageSizeThreshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AccumulatedMessageSizeThreshold.ToBytes();

		// Token: 0x04000007 RID: 7
		private static long anonymousMessageSizeThreshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AnonymousAccumulatedMessageSizeThreshold.ToBytes();

		// Token: 0x04000008 RID: 8
		private static long recipientCostLevel1Threshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AccumulatedRecipientCostLevel1Threshold;

		// Token: 0x04000009 RID: 9
		private static long recipientCostLevel2Threshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AccumulatedRecipientCostLevel2Threshold;

		// Token: 0x0400000A RID: 10
		private static long anonymousRecipientCostLevel1Threshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AnonymousAccumulatedRecipientCostLevel1Threshold;

		// Token: 0x0400000B RID: 11
		private static long anonymousRecipientCostLevel2Threshold = (long)Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration.AnonymousAccumulatedRecipientCostLevel2Threshold;

		// Token: 0x0400000C RID: 12
		private static TimeSpan slidingCounterWindowLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400000D RID: 13
		private static TimeSpan slidingCounterBucketLength = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400000E RID: 14
		private MessagePrioritization.SenderAccumulatedCostBucket anonymousSenderCostBucket;

		// Token: 0x0400000F RID: 15
		private MessagePrioritization.SenderAccumulatedCostBucket internalSenderCostBucket;

		// Token: 0x04000010 RID: 16
		private MessagePrioritization.RecipientAccumulatedCostBucket recipientCostBucket;

		// Token: 0x02000005 RID: 5
		private interface IAccumulatedCosts
		{
			// Token: 0x0600001C RID: 28
			bool IsStale();
		}

		// Token: 0x02000006 RID: 6
		private struct SenderAccumulatedCosts : MessagePrioritization.IAccumulatedCosts
		{
			// Token: 0x0600001D RID: 29 RVA: 0x00002A87 File Offset: 0x00000C87
			internal SenderAccumulatedCosts(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider)
			{
				this.messageSizeCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, currentTimeProvider);
				this.recipientCostCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, currentTimeProvider);
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002AA5 File Offset: 0x00000CA5
			// (set) Token: 0x0600001F RID: 31 RVA: 0x00002AAD File Offset: 0x00000CAD
			internal SlidingTotalCounter MessageSizeCounter
			{
				get
				{
					return this.messageSizeCounter;
				}
				set
				{
					this.messageSizeCounter = value;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002AB6 File Offset: 0x00000CB6
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002ABE File Offset: 0x00000CBE
			internal SlidingTotalCounter RecipientCostCounter
			{
				get
				{
					return this.recipientCostCounter;
				}
				set
				{
					this.recipientCostCounter = value;
				}
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002AC7 File Offset: 0x00000CC7
			public bool IsStale()
			{
				return 0L == this.recipientCostCounter.Sum;
			}

			// Token: 0x04000012 RID: 18
			private SlidingTotalCounter messageSizeCounter;

			// Token: 0x04000013 RID: 19
			private SlidingTotalCounter recipientCostCounter;
		}

		// Token: 0x02000007 RID: 7
		private struct RecipientAccumulatedCosts : MessagePrioritization.IAccumulatedCosts
		{
			// Token: 0x06000023 RID: 35 RVA: 0x00002AD8 File Offset: 0x00000CD8
			internal RecipientAccumulatedCosts(TimeSpan slidingWindowLength, TimeSpan bucketLength, Func<DateTime> currentTimeProvider)
			{
				this.recipientCounter = new SlidingTotalCounter(MessagePrioritization.slidingCounterWindowLength, bucketLength, currentTimeProvider);
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002AEC File Offset: 0x00000CEC
			// (set) Token: 0x06000025 RID: 37 RVA: 0x00002AF4 File Offset: 0x00000CF4
			internal SlidingTotalCounter RecipientCounter
			{
				get
				{
					return this.recipientCounter;
				}
				set
				{
					this.recipientCounter = value;
				}
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002AFD File Offset: 0x00000CFD
			public bool IsStale()
			{
				return 0L == this.recipientCounter.Sum;
			}

			// Token: 0x04000014 RID: 20
			private SlidingTotalCounter recipientCounter;
		}

		// Token: 0x02000008 RID: 8
		private class AccumulatedCostBucket
		{
			// Token: 0x06000027 RID: 39 RVA: 0x00002B0E File Offset: 0x00000D0E
			public AccumulatedCostBucket(Func<DateTime> currentTimeProvider)
			{
				this.AccumulatedCosts = new Dictionary<string, MessagePrioritization.IAccumulatedCosts>();
				this.CurrentTimeProvider = currentTimeProvider;
				this.lastClearingStaleEntriesTime = DateTime.MinValue;
				this.SyncObject = new object();
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002B3E File Offset: 0x00000D3E
			// (set) Token: 0x06000029 RID: 41 RVA: 0x00002B46 File Offset: 0x00000D46
			protected Dictionary<string, MessagePrioritization.IAccumulatedCosts> AccumulatedCosts { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B4F File Offset: 0x00000D4F
			// (set) Token: 0x0600002B RID: 43 RVA: 0x00002B57 File Offset: 0x00000D57
			protected object SyncObject { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002B60 File Offset: 0x00000D60
			// (set) Token: 0x0600002D RID: 45 RVA: 0x00002B68 File Offset: 0x00000D68
			protected Func<DateTime> CurrentTimeProvider { get; set; }

			// Token: 0x0600002E RID: 46 RVA: 0x00002B74 File Offset: 0x00000D74
			protected void ClearStaleEntriesIfNecessary()
			{
				if (this.AccumulatedCosts.Count < 20000 && this.CurrentTimeProvider() - this.lastClearingStaleEntriesTime < TimeSpan.FromMinutes(1.0))
				{
					return;
				}
				List<string> list = new List<string>();
				this.lastClearingStaleEntriesTime = this.CurrentTimeProvider();
				foreach (KeyValuePair<string, MessagePrioritization.IAccumulatedCosts> keyValuePair in this.AccumulatedCosts)
				{
					if (keyValuePair.Value.IsStale())
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (string key in list)
				{
					this.AccumulatedCosts.Remove(key);
				}
			}

			// Token: 0x04000015 RID: 21
			private DateTime lastClearingStaleEntriesTime;
		}

		// Token: 0x02000009 RID: 9
		private class SenderAccumulatedCostBucket : MessagePrioritization.AccumulatedCostBucket
		{
			// Token: 0x0600002F RID: 47 RVA: 0x00002C74 File Offset: 0x00000E74
			public SenderAccumulatedCostBucket(Func<DateTime> currentTimeProvider) : base(currentTimeProvider)
			{
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002C80 File Offset: 0x00000E80
			public void CalculateCost(string sender, long messageSize, int recipientCost, out long totalMessageSize, out long totalRecipientCost)
			{
				lock (base.SyncObject)
				{
					if (!base.AccumulatedCosts.ContainsKey(sender))
					{
						base.AccumulatedCosts.Add(sender, new MessagePrioritization.SenderAccumulatedCosts(MessagePrioritization.SlidingCounterWindowLength, MessagePrioritization.SlidingCounterBucketLength, base.CurrentTimeProvider));
					}
					MessagePrioritization.SenderAccumulatedCosts senderAccumulatedCosts = (MessagePrioritization.SenderAccumulatedCosts)base.AccumulatedCosts[sender];
					totalMessageSize = senderAccumulatedCosts.MessageSizeCounter.AddValue(messageSize);
					totalRecipientCost = senderAccumulatedCosts.RecipientCostCounter.AddValue((long)recipientCost);
					base.ClearStaleEntriesIfNecessary();
				}
			}
		}

		// Token: 0x0200000A RID: 10
		private class RecipientAccumulatedCostBucket : MessagePrioritization.AccumulatedCostBucket
		{
			// Token: 0x06000031 RID: 49 RVA: 0x00002D28 File Offset: 0x00000F28
			public RecipientAccumulatedCostBucket(Func<DateTime> currentTimeProvider) : base(currentTimeProvider)
			{
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002D34 File Offset: 0x00000F34
			public long CalculateCost(string recipient)
			{
				long result;
				lock (base.SyncObject)
				{
					result = this.GetOrCreateAccumulatedCosts(recipient).RecipientCounter.Sum + 1L;
				}
				return result;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00002D88 File Offset: 0x00000F88
			public void UpdateCounter(string recipient)
			{
				lock (base.SyncObject)
				{
					this.GetOrCreateAccumulatedCosts(recipient).RecipientCounter.AddValue(1L);
				}
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00002DDC File Offset: 0x00000FDC
			public void ClearStaleEntries()
			{
				lock (base.SyncObject)
				{
					base.ClearStaleEntriesIfNecessary();
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002E1C File Offset: 0x0000101C
			private MessagePrioritization.RecipientAccumulatedCosts GetOrCreateAccumulatedCosts(string recipient)
			{
				if (!base.AccumulatedCosts.ContainsKey(recipient))
				{
					MessagePrioritization.RecipientAccumulatedCosts recipientAccumulatedCosts = new MessagePrioritization.RecipientAccumulatedCosts(MessagePrioritization.SlidingCounterWindowLength, MessagePrioritization.SlidingCounterBucketLength, base.CurrentTimeProvider);
					base.AccumulatedCosts.Add(recipient, recipientAccumulatedCosts);
					return recipientAccumulatedCosts;
				}
				return (MessagePrioritization.RecipientAccumulatedCosts)base.AccumulatedCosts[recipient];
			}
		}
	}
}
