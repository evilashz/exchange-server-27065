using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200031D RID: 797
	internal class MessageQueue : IQueueVisitor
	{
		// Token: 0x06002246 RID: 8774 RVA: 0x00081240 File Offset: 0x0007F440
		static MessageQueue()
		{
			MessageQueue.expiryCheckPeriodForFixedPriorityBehaviour = MessageQueue.GetExpiryCheckPeriod(MessageQueue.GetSmallestExpirationInterval());
			Components.Configuration.LocalServerChanged += MessageQueue.TransportServerConfigUpdate;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000812B5 File Offset: 0x0007F4B5
		public MessageQueue(PriorityBehaviour behaviour) : this(behaviour, Components.TransportAppConfig.RemoteDelivery.DeliveryPriorityQuotas)
		{
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000812E0 File Offset: 0x0007F4E0
		internal MessageQueue(PriorityBehaviour behaviour, int[] deliveryPriorityQuotas)
		{
			if (behaviour == PriorityBehaviour.QueuePriority)
			{
				if (deliveryPriorityQuotas == null)
				{
					throw new ArgumentNullException("deliveryPriorityQuotas");
				}
				if (deliveryPriorityQuotas.Length < 1)
				{
					throw new ArgumentException("deliveryPriorityQuotas");
				}
				this.subQueues = new FifoQueue[deliveryPriorityQuotas.Length];
				this.deliveryPriorityQuotas = deliveryPriorityQuotas;
				this.remainingDeliveryPriorityQuotas = new int[this.deliveryPriorityQuotas.Length];
				this.remainingActivationPriorityQuotas = new int[this.deliveryPriorityQuotas.Length];
				for (int i = 0; i < this.deliveryPriorityQuotas.Length; i++)
				{
					if (this.deliveryPriorityQuotas[i] < 0)
					{
						throw new ArgumentOutOfRangeException(string.Format("deliveryPriorityQuotas[{0}]", i));
					}
				}
				for (int j = 0; j < deliveryPriorityQuotas.Length; j++)
				{
					this.subQueues[j] = new FifoQueue();
					this.remainingDeliveryPriorityQuotas[j] = this.deliveryPriorityQuotas[j];
					this.remainingActivationPriorityQuotas[j] = this.deliveryPriorityQuotas[j];
				}
			}
			else
			{
				this.subQueues = new FifoQueue[this.numberOfDeliveryPriorities];
				for (int k = 0; k < this.numberOfDeliveryPriorities; k++)
				{
					this.subQueues[k] = new FifoQueue();
				}
				this.remainingActivationPriorityQuotas = new int[]
				{
					40,
					20,
					4,
					1
				};
			}
			this.filledCountsPerPriority = new int[this.numberOfDeliveryPriorities];
			this.behaviour = behaviour;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00081480 File Offset: 0x0007F680
		public int ActiveCount
		{
			get
			{
				int result;
				lock (this)
				{
					int num = this.filled;
					for (int i = 0; i < this.subQueues.Length; i++)
					{
						num += this.subQueues[i].ActiveCount;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000814E4 File Offset: 0x0007F6E4
		public int ActiveCountExcludingPriorityNone
		{
			get
			{
				int result;
				lock (this)
				{
					int num = this.filled - this.filledCountsPerPriority[this.numberOfDeliveryPriorities - 1];
					for (int i = 0; i < this.subQueues.Length - 1; i++)
					{
						num += this.subQueues[i].ActiveCount;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0008155C File Offset: 0x0007F75C
		public int DeferredCount
		{
			get
			{
				return this.deferredQueue.TotalCount;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0008156C File Offset: 0x0007F76C
		public int LockedCount
		{
			get
			{
				int result;
				lock (this)
				{
					int num = 0;
					for (int i = 0; i < this.subQueues.Length; i++)
					{
						num += this.subQueues[i].LockedCount;
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000815CC File Offset: 0x0007F7CC
		public int TotalCount
		{
			get
			{
				int result;
				lock (this)
				{
					int num = this.filled;
					for (int i = 0; i < this.subQueues.Length; i++)
					{
						num += this.subQueues[i].TotalCount;
					}
					result = num + this.deferredQueue.TotalCount;
				}
				return result;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0008163C File Offset: 0x0007F83C
		public bool SupportsFixedPriority
		{
			get
			{
				return this.behaviour == PriorityBehaviour.Fixed;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00081647 File Offset: 0x0007F847
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x00081676 File Offset: 0x0007F876
		public TimeSpan ExpiryDuration
		{
			get
			{
				if (this.expiryCheckPeriod != null)
				{
					return this.expiryCheckPeriod.Value;
				}
				if (this.behaviour != PriorityBehaviour.Fixed)
				{
					return MessageQueue.expiryCheckPeriodForOtherPriorityBehaviours;
				}
				return MessageQueue.expiryCheckPeriodForFixedPriorityBehaviour;
			}
			set
			{
				if (value < MessageQueue.minExpiryCheckPeriod)
				{
					throw new ArgumentOutOfRangeException("ExpiryDuration", string.Format("Values less than '{0}' second(s) are invalid.", MessageQueue.minExpiryCheckPeriod.TotalSeconds));
				}
				this.expiryCheckPeriod = new TimeSpan?(value);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000816B5 File Offset: 0x0007F8B5
		public long LastDequeueTime
		{
			get
			{
				return Interlocked.Read(ref this.lastDequeue);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000816C2 File Offset: 0x0007F8C2
		private bool ShouldUseFastArray
		{
			get
			{
				return this.behaviour != PriorityBehaviour.Fixed && this.behaviour != PriorityBehaviour.QueuePriority;
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000816DC File Offset: 0x0007F8DC
		public virtual void Enqueue(IQueueItem item)
		{
			if (item.Expiry < DateTime.UtcNow)
			{
				this.ItemExpired(item, false);
				return;
			}
			bool flag = false;
			if (item.DeferUntil != DateTime.MinValue)
			{
				if (item.DeferUntil > item.Expiry && item.DeferUntil != DateTime.MaxValue)
				{
					this.ItemExpired(item, false);
				}
				else
				{
					flag = this.ItemDeferred(item);
				}
				if (!flag)
				{
					return;
				}
			}
			lock (this)
			{
				if (flag)
				{
					this.deferredQueue.Enqueue(item);
					return;
				}
				if (this.ShouldUseFastArray && this.filled < this.items.Length && this.filled == this.ActiveCount)
				{
					this.items[this.tailIndex] = item;
					this.tailIndex = this.Advance(this.tailIndex);
					this.IncrementFilled(item.Priority);
				}
				else
				{
					DeliveryPriority itemPriority = this.GetItemPriority(item);
					this.subQueues[(int)itemPriority].Enqueue(item);
					this.extendedData = true;
				}
				this.ItemEnqueued(item);
			}
			this.DataAvailable();
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00081810 File Offset: 0x0007FA10
		public IQueueItem Dequeue()
		{
			return this.DequeueInternal(DeliveryPriority.Normal);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00081819 File Offset: 0x0007FA19
		public virtual IQueueItem Dequeue(DeliveryPriority priority)
		{
			return this.DequeueInternal(priority);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00081824 File Offset: 0x0007FA24
		public void ForEach(Action<IQueueItem> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			lock (this)
			{
				this.InternalForEach(action);
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00081870 File Offset: 0x0007FA70
		public void ForEach(Action<IQueueItem> action, bool includeDeferred)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			lock (this)
			{
				this.InternalForEach(action);
				if (includeDeferred)
				{
					this.deferredQueue.ForEach(action);
				}
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000818CC File Offset: 0x0007FACC
		public void ForEach<T>(Action<IQueueItem, T> action, T state, bool includeDeferred)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			lock (this)
			{
				this.InternalForEach<T>(action, state);
				if (includeDeferred)
				{
					this.deferredQueue.ForEach<T>(action, state);
				}
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00081A00 File Offset: 0x0007FC00
		public void TimedUpdate()
		{
			DateTime now = DateTime.UtcNow;
			QueueItemList queueItemList = null;
			QueueItemList queueItemList2 = null;
			QueueItemList queueItemList3 = null;
			lock (this)
			{
				if (now.Ticks >= this.deferredQueue.NextActivationTime)
				{
					queueItemList = this.deferredQueue.DequeueAll((IQueueItem item) => now.Ticks >= Math.Min(item.DeferUntil.Ticks, item.Expiry.Ticks));
				}
				if (now - this.lastExpiryCheck >= this.ExpiryDuration || (this.checkForExpiriesOnNextTimedUpdate && now - this.lastExpiryCheck >= MessageQueue.minExpiryCheckPeriod))
				{
					this.checkForExpiriesOnNextTimedUpdate = false;
					queueItemList2 = this.DequeueAllInternal((IQueueItem item) => now.Ticks >= item.Expiry.Ticks, false);
					this.lastExpiryCheck = now;
				}
				if (Components.TransportAppConfig.ThrottlingConfig.LockExpirationInterval > TimeSpan.Zero && now - this.lastLockExpiryCheck > Components.TransportAppConfig.ThrottlingConfig.LockExpirationCheckPeriod)
				{
					queueItemList3 = this.DequeueAllLocked(delegate(IQueueItem item)
					{
						ILockableItem lockableItem = item as ILockableItem;
						if (lockableItem == null)
						{
							return false;
						}
						bool flag2 = now.Ticks >= lockableItem.LockExpirationTime.Ticks;
						if (flag2)
						{
							lockableItem.LockExpirationTime = DateTimeOffset.MinValue;
						}
						return flag2;
					});
					this.lastLockExpiryCheck = now;
				}
			}
			if (queueItemList != null)
			{
				queueItemList.ForEach(new Action<IQueueItem>(this.ReactivateItem));
			}
			if (queueItemList2 != null)
			{
				queueItemList2.ForEach(delegate(IQueueItem item)
				{
					this.ItemExpired(item, true);
				});
			}
			if (queueItemList3 != null)
			{
				queueItemList3.ForEach(delegate(IQueueItem item)
				{
					this.ItemLockExpired(item);
					this.Enqueue(item);
				});
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00081BE0 File Offset: 0x0007FDE0
		public QueueItemList DequeueAll(Predicate<IQueueItem> match)
		{
			return this.DequeueAll(match, true);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00081BF4 File Offset: 0x0007FDF4
		public QueueItemList DequeueAll(Predicate<IQueueItem> match, bool checkDeferred)
		{
			QueueItemList queueItemList;
			lock (this)
			{
				queueItemList = this.DequeueAllInternal(match, checkDeferred);
			}
			if (queueItemList != null)
			{
				queueItemList.ForEach(delegate(IQueueItem item)
				{
					this.ItemRemoved(item);
				});
			}
			return queueItemList;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00081C50 File Offset: 0x0007FE50
		public void Lock(IQueueItem item, WaitCondition condition, string lockReason, int dehydrateThreshold)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (item.Expiry < DateTime.UtcNow)
			{
				this.ItemExpired(item, false);
				return;
			}
			if (!this.ItemLocked(item, condition, lockReason))
			{
				return;
			}
			lock (this)
			{
				DeliveryPriority itemPriority = this.GetItemPriority(item);
				this.subQueues[(int)itemPriority].Lock(item, condition, dehydrateThreshold, new Action<IQueueItem>(this.ItemDehydrated));
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00081CE0 File Offset: 0x0007FEE0
		public bool ActivateOne(WaitCondition condition, DeliveryPriority suggestedPriority, AccessToken token)
		{
			bool flag = false;
			lock (this)
			{
				switch (this.behaviour)
				{
				case PriorityBehaviour.IgnorePriority:
				case PriorityBehaviour.RoundRobin:
				case PriorityBehaviour.QueuePriority:
					flag = this.FindItemToActivateByQuotas(condition, token);
					break;
				case PriorityBehaviour.Fixed:
					if (this.subQueues[(int)suggestedPriority].LockedCount > 0)
					{
						if (!this.subQueues[(int)suggestedPriority].ActivateOne(condition, token, new ItemUnlocked(this.ItemUnlocked)))
						{
							return false;
						}
						flag = true;
					}
					break;
				}
			}
			if (flag)
			{
				this.DataAvailable();
				return true;
			}
			return false;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00081D88 File Offset: 0x0007FF88
		public void UpdateNextActivationTime(DateTime activationTime)
		{
			lock (this)
			{
				this.deferredQueue.UpdateNextActivationTime(activationTime.Ticks);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		internal static void RunUnderPoisonContext(IQueueItem item, Action<IQueueItem> action)
		{
			PoisonContext context = PoisonMessage.Context;
			PoisonMessage.Context = item.GetMessageContext(MessageProcessingSource.Queue);
			action(item);
			PoisonMessage.Context = context;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00081E00 File Offset: 0x00080000
		internal static void RunUnderPoisonContext<T>(IQueueItem item, T state, Action<IQueueItem, T> action)
		{
			PoisonContext context = PoisonMessage.Context;
			PoisonMessage.Context = item.GetMessageContext(MessageProcessingSource.Queue);
			action(item, state);
			PoisonMessage.Context = context;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x00081E2E File Offset: 0x0008002E
		internal int GetDeferredCount(DeferReason reason)
		{
			return this.deferredQueue.GetDeferredCount(reason);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00081E3C File Offset: 0x0008003C
		internal XElement GetDiagnosticInfo(bool verbose, bool conditionalQueuing)
		{
			XElement result;
			lock (this)
			{
				XElement xelement = new XElement("queue");
				XElement xelement2 = new XElement("counts");
				xelement2.Add(new XElement("TotalCount", this.TotalCount));
				xelement2.Add(new XElement("DeferredCount", this.DeferredCount));
				if (this.ShouldUseFastArray)
				{
					XElement content = new XElement("FastArrayCount", this.filled);
					xelement2.Add(content);
				}
				if (conditionalQueuing)
				{
					xelement2.Add(new XElement("LockedCount", this.LockedCount));
				}
				xelement.Add(xelement2);
				if (verbose)
				{
					for (int i = 0; i < this.subQueues.Length; i++)
					{
						XElement xelement3 = new XElement("priorityQueue");
						xelement3.Add(new XElement("priority", i));
						xelement.Add(this.subQueues[i].GetDiagnosticInfo(xelement3, conditionalQueuing));
					}
				}
				result = xelement;
			}
			return result;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00081F94 File Offset: 0x00080194
		protected void RelockAll(string lockReason, Predicate<IQueueItem> isUnlocked)
		{
			lock (this)
			{
				List<IQueueItem>[] array = new List<IQueueItem>[this.subQueues.Length];
				if (this.ShouldUseFastArray)
				{
					int num = this.headIndex;
					int i = 0;
					while (i < this.filled)
					{
						if (isUnlocked(this.items[num]))
						{
							IQueueItem queueItem = this.DequeueArrayItem(num);
							if (array[(int)queueItem.Priority] == null)
							{
								array[(int)queueItem.Priority] = new List<IQueueItem>();
							}
							array[(int)queueItem.Priority].Add(queueItem);
						}
						else
						{
							num = this.Advance(num);
							i++;
						}
					}
				}
				for (int j = 0; j < this.subQueues.Length; j++)
				{
					this.subQueues[j].RelockAll(array[j], lockReason, new ItemRelocked(this.ItemRelocked));
				}
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0008207C File Offset: 0x0008027C
		protected IQueueItem DequeueItem(DequeueMatch match, bool deferredQueueFirst)
		{
			IQueueItem queueItem = null;
			bool flag = false;
			lock (this)
			{
				if (deferredQueueFirst)
				{
					queueItem = this.deferredQueue.DequeueItem(match, out flag);
				}
				if (!flag && this.ShouldUseFastArray)
				{
					int num = this.headIndex;
					int num2 = 0;
					while (num2 < this.filled && !flag)
					{
						switch (match(this.items[num]))
						{
						case DequeueMatchResult.Break:
							flag = true;
							break;
						case DequeueMatchResult.DequeueAndBreak:
							queueItem = this.DequeueArrayItem(num);
							flag = true;
							if (this.extendedData && this.filled < 35)
							{
								this.ScheduleItems();
							}
							break;
						case DequeueMatchResult.Continue:
							num = this.Advance(num);
							num2++;
							break;
						default:
							throw new InvalidOperationException("Invalid return value from match()");
						}
					}
				}
				int num3 = 0;
				while (num3 < this.subQueues.Length && !flag)
				{
					queueItem = this.subQueues[num3].DequeueItem(match, out flag);
					num3++;
				}
				if (!deferredQueueFirst && !flag)
				{
					queueItem = this.deferredQueue.DequeueItem(match, out flag);
				}
			}
			if (queueItem != null)
			{
				this.ItemRemoved(queueItem);
			}
			return queueItem;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000821A4 File Offset: 0x000803A4
		protected void ScheduleCheckForExpiredItems()
		{
			this.checkForExpiriesOnNextTimedUpdate = true;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000821AD File Offset: 0x000803AD
		protected virtual void DataAvailable()
		{
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000821AF File Offset: 0x000803AF
		protected virtual void ItemRemoved(IQueueItem item)
		{
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000821B1 File Offset: 0x000803B1
		protected virtual void ItemExpired(IQueueItem item, bool wasEnqueued)
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000821B3 File Offset: 0x000803B3
		protected virtual void ItemLockExpired(IQueueItem item)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000821B5 File Offset: 0x000803B5
		protected virtual bool ItemDeferred(IQueueItem item)
		{
			return true;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000821B8 File Offset: 0x000803B8
		protected virtual bool ItemActivated(IQueueItem item)
		{
			return true;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000821BB File Offset: 0x000803BB
		protected virtual void ItemEnqueued(IQueueItem item)
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000821BD File Offset: 0x000803BD
		protected virtual bool ItemLocked(IQueueItem item, WaitCondition condition, string lockReason)
		{
			return true;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000821C0 File Offset: 0x000803C0
		protected virtual bool ItemUnlocked(IQueueItem item, AccessToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000821C7 File Offset: 0x000803C7
		protected virtual void ItemRelocked(IQueueItem item, string lockReason, out WaitCondition condition)
		{
			condition = null;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000821CC File Offset: 0x000803CC
		protected virtual void ItemDehydrated(IQueueItem item)
		{
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000821CE File Offset: 0x000803CE
		protected int Advance(int index)
		{
			if (++index >= this.items.Length)
			{
				return 0;
			}
			return index;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000821E3 File Offset: 0x000803E3
		private static void TransportServerConfigUpdate(TransportServerConfiguration args)
		{
			MessageQueue.expiryCheckPeriodForOtherPriorityBehaviours = MessageQueue.GetExpiryCheckPeriod(Components.Configuration.LocalServer.TransportServer.MessageExpirationTimeout);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00082208 File Offset: 0x00080408
		private static TimeSpan GetSmallestExpirationInterval()
		{
			TimeSpan timeSpan = Components.TransportAppConfig.RemoteDelivery.MessageExpirationTimeout(DeliveryPriority.High);
			if (timeSpan > Components.TransportAppConfig.RemoteDelivery.MessageExpirationTimeout(DeliveryPriority.Normal))
			{
				timeSpan = Components.TransportAppConfig.RemoteDelivery.MessageExpirationTimeout(DeliveryPriority.Normal);
			}
			if (timeSpan > Components.TransportAppConfig.RemoteDelivery.MessageExpirationTimeout(DeliveryPriority.Low))
			{
				timeSpan = Components.TransportAppConfig.RemoteDelivery.MessageExpirationTimeout(DeliveryPriority.Low);
			}
			return timeSpan;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0008227C File Offset: 0x0008047C
		private static TimeSpan GetExpiryCheckPeriod(TimeSpan expirationInterval)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(expirationInterval.TotalSeconds * 0.1);
			if (timeSpan < BackgroundProcessingThread.SlowScanInterval)
			{
				timeSpan = MessageQueue.MinExpiryCheckInterval;
			}
			else if (timeSpan > MessageQueue.MaxExpiryCheckInterval)
			{
				timeSpan = MessageQueue.MaxExpiryCheckInterval;
			}
			return timeSpan;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000822C9 File Offset: 0x000804C9
		private void IncrementFilled(DeliveryPriority priority)
		{
			this.filled++;
			this.filledCountsPerPriority[QueueManager.PriorityToInstanceIndexMap[priority]]++;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000822FC File Offset: 0x000804FC
		private void DecrementFilled(DeliveryPriority priority)
		{
			this.filled--;
			this.filledCountsPerPriority[QueueManager.PriorityToInstanceIndexMap[priority]]--;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0008232F File Offset: 0x0008052F
		private bool IsEmpty(DeliveryPriority priority)
		{
			if (this.behaviour != PriorityBehaviour.Fixed)
			{
				return this.ActiveCount == 0;
			}
			return this.subQueues[(int)priority].ActiveCount == 0;
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00082360 File Offset: 0x00080560
		private IQueueItem DequeueInternal(DeliveryPriority priority)
		{
			IQueueItem queueItem = null;
			DateTime utcNow = DateTime.UtcNow;
			List<IQueueItem> list = null;
			lock (this)
			{
				while (!this.IsEmpty(priority))
				{
					Interlocked.Exchange(ref this.lastDequeue, utcNow.Ticks);
					if (this.behaviour == PriorityBehaviour.QueuePriority)
					{
						queueItem = this.PriorityBasedDequeue();
					}
					else if (this.behaviour != PriorityBehaviour.Fixed)
					{
						if (this.filled < 1)
						{
							throw new InvalidOperationException();
						}
						queueItem = this.items[this.headIndex];
						this.items[this.headIndex] = null;
						this.headIndex = this.Advance(this.headIndex);
						this.DecrementFilled(queueItem.Priority);
						if (this.extendedData && this.filled < 35)
						{
							this.ScheduleItems();
						}
					}
					else
					{
						queueItem = this.subQueues[(int)priority].Dequeue();
					}
					if (utcNow <= queueItem.Expiry)
					{
						break;
					}
					if (list == null)
					{
						list = new List<IQueueItem>();
					}
					list.Add(queueItem);
					queueItem = null;
				}
			}
			if (list != null)
			{
				list.ForEach(delegate(IQueueItem item)
				{
					this.ItemExpired(item, true);
				});
			}
			return queueItem;
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00082494 File Offset: 0x00080694
		private IQueueItem PriorityBasedDequeue()
		{
			this.RefillDequeueQuotasIfNeccessary();
			long num = long.MaxValue;
			int num2 = -1;
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				if (this.subQueues[i].ActiveCount != 0)
				{
					long oldestItem = this.subQueues[i].OldestItem;
					if (num > oldestItem)
					{
						num = oldestItem;
						num2 = i;
					}
					if (this.remainingDeliveryPriorityQuotas[i] > 0)
					{
						break;
					}
				}
			}
			if (num2 == -1)
			{
				throw new InvalidOperationException("The queue was not empty, but we couldn't figure out the sub queue to dequeue from.");
			}
			if (this.remainingDeliveryPriorityQuotas[num2] > 0)
			{
				this.remainingDeliveryPriorityQuotas[num2]--;
			}
			return this.subQueues[num2].Dequeue();
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00082538 File Offset: 0x00080738
		private void RefillDequeueQuotasIfNeccessary()
		{
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				if (this.subQueues[i].ActiveCount != 0 && this.remainingDeliveryPriorityQuotas[i] > 0)
				{
					return;
				}
			}
			for (int j = 0; j < this.deliveryPriorityQuotas.Length; j++)
			{
				this.remainingDeliveryPriorityQuotas[j] = this.deliveryPriorityQuotas[j];
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00082598 File Offset: 0x00080798
		private bool FindItemToActivateByQuotas(WaitCondition condition, AccessToken token)
		{
			bool flag = false;
			this.RefillActivationQuotasIfNeccessary();
			int num = 0;
			while (num < this.subQueues.Length && !flag)
			{
				if (this.subQueues[num].LockedCount > 0 && this.remainingActivationPriorityQuotas[num] > 0 && this.subQueues[num].ActivateOne(condition, token, new ItemUnlocked(this.ItemUnlocked)))
				{
					this.remainingActivationPriorityQuotas[num]--;
					flag = true;
				}
				num++;
			}
			int num2 = 0;
			while (num2 < this.subQueues.Length && !flag)
			{
				if (this.subQueues[num2].LockedCount > 0 && this.remainingActivationPriorityQuotas[num2] <= 0 && this.subQueues[num2].ActivateOne(condition, token, new ItemUnlocked(this.ItemUnlocked)))
				{
					this.remainingActivationPriorityQuotas[num2]--;
					flag = true;
				}
				num2++;
			}
			this.extendedData = true;
			if (this.ShouldUseFastArray && this.filled < 35)
			{
				this.ScheduleItems();
			}
			return flag;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000826A4 File Offset: 0x000808A4
		private void RefillActivationQuotasIfNeccessary()
		{
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				if (this.subQueues[i].LockedCount > 0 && this.remainingActivationPriorityQuotas[i] > 0)
				{
					return;
				}
			}
			if (this.behaviour == PriorityBehaviour.QueuePriority)
			{
				for (int j = 0; j < this.deliveryPriorityQuotas.Length; j++)
				{
					this.remainingActivationPriorityQuotas[j] = this.deliveryPriorityQuotas[j];
				}
				return;
			}
			this.remainingActivationPriorityQuotas[0] = 40;
			this.remainingActivationPriorityQuotas[1] = 20;
			this.remainingActivationPriorityQuotas[2] = 4;
			this.remainingActivationPriorityQuotas[3] = 1;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00082733 File Offset: 0x00080933
		private void ReactivateItem(IQueueItem item)
		{
			item.DeferUntil = DateTime.MinValue;
			if (this.ItemActivated(item))
			{
				this.Enqueue(item);
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00082750 File Offset: 0x00080950
		private IQueueItem DequeueArrayItem(int index)
		{
			IQueueItem queueItem = this.items[index];
			this.DecrementFilled(queueItem.Priority);
			this.tailIndex = this.Rewind(this.tailIndex);
			if (index != this.tailIndex)
			{
				this.items[index] = this.items[this.tailIndex];
				this.items[this.tailIndex] = null;
			}
			else
			{
				this.items[index] = null;
			}
			return queueItem;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000827BC File Offset: 0x000809BC
		private QueueItemList DequeueAllInternal(Predicate<IQueueItem> match, bool checkDeferred)
		{
			QueueItemList queueItemList = new QueueItemList();
			if (this.ShouldUseFastArray)
			{
				int num = this.headIndex;
				int i = 0;
				while (i < this.filled)
				{
					if (match(this.items[num]))
					{
						queueItemList.Add(this.DequeueArrayItem(num));
					}
					else
					{
						num = this.Advance(num);
						i++;
					}
				}
			}
			for (int j = 0; j < this.subQueues.Length; j++)
			{
				QueueItemList list = this.subQueues[j].DequeueAll(match);
				queueItemList.Concat(list);
			}
			if (this.ShouldUseFastArray && this.extendedData && this.filled < 35)
			{
				this.ScheduleItems();
			}
			if (checkDeferred)
			{
				queueItemList.Concat(this.deferredQueue.DequeueAll(match));
			}
			return queueItemList;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00082878 File Offset: 0x00080A78
		private QueueItemList DequeueAllLocked(Predicate<IQueueItem> match)
		{
			QueueItemList queueItemList = new QueueItemList();
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				QueueItemList list = this.subQueues[i].DequeueAllLocked(match);
				queueItemList.Concat(list);
			}
			return queueItemList;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000828B8 File Offset: 0x00080AB8
		private long ShuffleFrom(DeliveryPriority index)
		{
			long oldestItem = this.subQueues[(int)index].OldestItem;
			IQueueItem queueItem = this.subQueues[(int)index].Dequeue();
			this.items[this.tailIndex] = queueItem;
			this.tailIndex = this.Advance(this.tailIndex);
			this.IncrementFilled(queueItem.Priority);
			return oldestItem;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00082910 File Offset: 0x00080B10
		private void ScheduleItems()
		{
			int i = 0;
			int j = 40;
			int num = this.subQueues[0].ActiveCount;
			if (j > num)
			{
				i = j - num;
				j = num;
			}
			while (j > 0)
			{
				this.ShuffleFrom(DeliveryPriority.High);
				j--;
			}
			j = 20;
			int num2 = 0;
			while (i > 20)
			{
				num2++;
				j += 20;
				i -= 21;
			}
			j += i;
			i = num2;
			num = this.subQueues[1].ActiveCount;
			if (j > num)
			{
				i += j - num;
				j = num;
			}
			long num3 = DateTime.UtcNow.Ticks;
			while (j > 0)
			{
				num3 = this.ShuffleFrom(DeliveryPriority.Normal);
				j--;
			}
			j = 5 + i;
			long num4 = this.subQueues[2].OldestItem;
			num = this.subQueues[2].ActiveCount + this.subQueues[3].ActiveCount;
			while (j > 0 && num > 0)
			{
				if ((num3 <= this.subQueues[2].OldestItem || num3 <= this.subQueues[3].OldestItem) && 0 < this.subQueues[1].ActiveCount)
				{
					num3 = this.ShuffleFrom(DeliveryPriority.Normal);
				}
				else if (num4 <= this.subQueues[3].OldestItem && 0 < this.subQueues[2].ActiveCount)
				{
					num4 = this.ShuffleFrom(DeliveryPriority.Low);
					num--;
				}
				else
				{
					this.ShuffleFrom(DeliveryPriority.None);
					num--;
				}
				j--;
			}
			for (int k = 1; k >= 0; k--)
			{
				while (j > 0 && this.subQueues[k].ActiveCount > 0)
				{
					this.ShuffleFrom((DeliveryPriority)k);
					j--;
				}
			}
			if (j > 0)
			{
				this.extendedData = false;
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00082A9F File Offset: 0x00080C9F
		private int Rewind(int index)
		{
			if (--index < 0)
			{
				return this.items.Length - 1;
			}
			return index;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00082AB8 File Offset: 0x00080CB8
		private void InternalForEach(Action<IQueueItem> action)
		{
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				this.subQueues[i].ForEach(action);
			}
			if (this.ShouldUseFastArray)
			{
				int num = this.headIndex;
				for (int j = 0; j < this.filled; j++)
				{
					MessageQueue.RunUnderPoisonContext(this.items[num], action);
					num = this.Advance(num);
				}
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00082B1C File Offset: 0x00080D1C
		private void InternalForEach<T>(Action<IQueueItem, T> action, T state)
		{
			for (int i = 0; i < this.subQueues.Length; i++)
			{
				this.subQueues[i].ForEach<T>(action, state);
			}
			if (this.behaviour != PriorityBehaviour.Fixed)
			{
				int num = this.headIndex;
				for (int j = 0; j < this.filled; j++)
				{
					MessageQueue.RunUnderPoisonContext<T>(this.items[num], state, action);
					num = this.Advance(num);
				}
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00082B83 File Offset: 0x00080D83
		private DeliveryPriority GetItemPriority(IQueueItem item)
		{
			if (this.behaviour == PriorityBehaviour.IgnorePriority)
			{
				return DeliveryPriority.Normal;
			}
			return item.Priority;
		}

		// Token: 0x040011E7 RID: 4583
		private const int ItemLength = 100;

		// Token: 0x040011E8 RID: 4584
		private const int NoneRatio = 1;

		// Token: 0x040011E9 RID: 4585
		private const int LowRatio = 4;

		// Token: 0x040011EA RID: 4586
		private const int PriorityRatioValue = 10;

		// Token: 0x040011EB RID: 4587
		private const int HighRatio = 40;

		// Token: 0x040011EC RID: 4588
		private const int NormalRatio = 20;

		// Token: 0x040011ED RID: 4589
		private const int LowThreshold = 35;

		// Token: 0x040011EE RID: 4590
		protected FifoQueue[] subQueues;

		// Token: 0x040011EF RID: 4591
		private static readonly TimeSpan MinExpiryCheckInterval = BackgroundProcessingThread.SlowScanInterval;

		// Token: 0x040011F0 RID: 4592
		private static readonly TimeSpan MaxExpiryCheckInterval = TimeSpan.FromMinutes(30.0);

		// Token: 0x040011F1 RID: 4593
		private static readonly TimeSpan expiryCheckPeriodForFixedPriorityBehaviour;

		// Token: 0x040011F2 RID: 4594
		private static TimeSpan minExpiryCheckPeriod = TimeSpan.FromSeconds(1.0);

		// Token: 0x040011F3 RID: 4595
		private static TimeSpan expiryCheckPeriodForOtherPriorityBehaviours = TimeSpan.FromMinutes(2.0);

		// Token: 0x040011F4 RID: 4596
		private readonly PriorityBehaviour behaviour;

		// Token: 0x040011F5 RID: 4597
		private readonly int numberOfDeliveryPriorities = QueueManager.PriorityToInstanceIndexMap.Count;

		// Token: 0x040011F6 RID: 4598
		private TimeSpan? expiryCheckPeriod = null;

		// Token: 0x040011F7 RID: 4599
		private IQueueItem[] items = new IQueueItem[100];

		// Token: 0x040011F8 RID: 4600
		private int headIndex;

		// Token: 0x040011F9 RID: 4601
		private int tailIndex;

		// Token: 0x040011FA RID: 4602
		private int filled;

		// Token: 0x040011FB RID: 4603
		private int[] filledCountsPerPriority;

		// Token: 0x040011FC RID: 4604
		private DeferredQueue deferredQueue = new DeferredQueue();

		// Token: 0x040011FD RID: 4605
		private bool extendedData;

		// Token: 0x040011FE RID: 4606
		private long lastDequeue = DateTime.UtcNow.Ticks;

		// Token: 0x040011FF RID: 4607
		private DateTime lastExpiryCheck = DateTime.MinValue;

		// Token: 0x04001200 RID: 4608
		private DateTime lastLockExpiryCheck = DateTime.MinValue;

		// Token: 0x04001201 RID: 4609
		private bool checkForExpiriesOnNextTimedUpdate;

		// Token: 0x04001202 RID: 4610
		private int[] remainingDeliveryPriorityQuotas;

		// Token: 0x04001203 RID: 4611
		private int[] remainingActivationPriorityQuotas;

		// Token: 0x04001204 RID: 4612
		private int[] deliveryPriorityQuotas;
	}
}
