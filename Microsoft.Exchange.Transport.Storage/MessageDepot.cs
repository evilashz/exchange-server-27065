using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000002 RID: 2
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IMessageDepot))]
	internal sealed class MessageDepot : IMessageDepot, IMessageDepotQueueViewer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MessageDepot() : this(null, null)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		public MessageDepot(Func<DateTime> timeProvider, TimeSpan? delayNotificationTimeout)
		{
			this.timeProvider = timeProvider;
			this.CreateMessageTransitionMap();
			if (delayNotificationTimeout == null)
			{
				this.delayNotificationTimeout = MessageDepot.DelayNotificationTimeout;
			}
			else
			{
				this.delayNotificationTimeout = delayNotificationTimeout.Value;
			}
			Type typeFromHandle = typeof(MessageDepotItemStage);
			foreach (object obj in Enum.GetValues(typeFromHandle))
			{
				int num = (int)obj;
				this.messageAddedHandlers[num] = delegate(MessageEventArgs param0)
				{
				};
				this.messageActivatedHandlers[num] = delegate(MessageActivatedEventArgs param0)
				{
				};
				this.messageDeactivatedHandlers[num] = delegate(MessageDeactivatedEventArgs param0)
				{
				};
				this.messageRemovedHandlers[num] = delegate(MessageRemovedEventArgs param0)
				{
				};
				this.messageExpiredHandlers[num] = delegate(MessageEventArgs param0)
				{
				};
				this.messageDelayedHandlers[num] = delegate(MessageEventArgs param0)
				{
				};
				string name = Enum.GetName(typeFromHandle, num);
				MessageDepotPerfCountersInstance instance = MessageDepotPerfCounters.GetInstance(name);
				this.messageCounterWrappers[num] = new MessageDepot.CounterWrapper[MessageDepot.StateCount];
				this.messageCounterWrappers[num][0] = new MessageDepot.CounterWrapper(instance.ReadyMessages);
				this.messageCounterWrappers[num][1] = new MessageDepot.CounterWrapper(instance.DeferredMessages);
				this.messageCounterWrappers[num][5] = new MessageDepot.CounterWrapper(instance.ExpiringMessages);
				this.messageCounterWrappers[num][2] = new MessageDepot.CounterWrapper(instance.PoisonMessages);
				this.messageCounterWrappers[num][4] = new MessageDepot.CounterWrapper(instance.ProcessingMessages);
				this.messageCounterWrappers[num][3] = new MessageDepot.CounterWrapper(instance.SuspendedMessages);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000023D4 File Offset: 0x000005D4
		public void SubscribeToAddEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageAddedHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023FC File Offset: 0x000005FC
		public void UnsubscribeFromAddEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageAddedHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002424 File Offset: 0x00000624
		public void SubscribeToActivatedEvent(MessageDepotItemStage targetStage, MessageActivatedEventHandler eventHandler)
		{
			MessageActivatedEventHandler[] array;
			(array = this.messageActivatedHandlers)[(int)targetStage] = (MessageActivatedEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000244C File Offset: 0x0000064C
		public void UnsubscribeFromActivatedEvent(MessageDepotItemStage targetStage, MessageActivatedEventHandler eventHandler)
		{
			MessageActivatedEventHandler[] array;
			(array = this.messageActivatedHandlers)[(int)targetStage] = (MessageActivatedEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002474 File Offset: 0x00000674
		public void SubscribeToDeactivatedEvent(MessageDepotItemStage targetStage, MessageDeactivatedEventHandler eventHandler)
		{
			MessageDeactivatedEventHandler[] array;
			(array = this.messageDeactivatedHandlers)[(int)targetStage] = (MessageDeactivatedEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000249C File Offset: 0x0000069C
		public void UnsubscribeFromDeactivatedEvent(MessageDepotItemStage targetStage, MessageDeactivatedEventHandler eventHandler)
		{
			MessageDeactivatedEventHandler[] array;
			(array = this.messageDeactivatedHandlers)[(int)targetStage] = (MessageDeactivatedEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024C4 File Offset: 0x000006C4
		public void SubscribeToRemovedEvent(MessageDepotItemStage targetStage, MessageRemovedEventHandler eventHandler)
		{
			MessageRemovedEventHandler[] array;
			(array = this.messageRemovedHandlers)[(int)targetStage] = (MessageRemovedEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024EC File Offset: 0x000006EC
		public void UnsubscribeFromRemovedEvent(MessageDepotItemStage targetStage, MessageRemovedEventHandler eventHandler)
		{
			MessageRemovedEventHandler[] array;
			(array = this.messageRemovedHandlers)[(int)targetStage] = (MessageRemovedEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002514 File Offset: 0x00000714
		public void SubscribeToExpiredEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageExpiredHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000253C File Offset: 0x0000073C
		public void UnsubscribeFromExpiredEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageExpiredHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002564 File Offset: 0x00000764
		public void SubscribeToDelayedEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageDelayedHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Combine(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000258C File Offset: 0x0000078C
		public void UnsubscribeFromDelayedEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler)
		{
			MessageEventHandler[] array;
			(array = this.messageDelayedHandlers)[(int)targetStage] = (MessageEventHandler)Delegate.Remove(array[(int)targetStage], eventHandler);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025B4 File Offset: 0x000007B4
		public void Add(IMessageDepotItem item)
		{
			this.ValidateAddArguments(item);
			MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper = new MessageDepot.MessageDepotItemWrapper(item, MessageDepotItemState.Ready);
			lock (messageDepotItemWrapper)
			{
				this.SetNewMessageState(messageDepotItemWrapper);
				if (!this.allMessages.TryAdd(item.Id, messageDepotItemWrapper))
				{
					throw new DuplicateItemException(item.Id, messageDepotItemWrapper.State, Strings.DuplicateItemFound(item.Id), null);
				}
			}
			this.AddToNearLists(messageDepotItemWrapper);
			this.IncrementMessageCount(messageDepotItemWrapper);
			this.RaiseEventsAfterAddItem(messageDepotItemWrapper);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002648 File Offset: 0x00000848
		public void DeferMessage(TransportMessageId messageId, TimeSpan deferTimeSpan, AcquireToken acquireToken)
		{
			ArgumentValidator.ThrowIfNull("acquireToken", acquireToken);
			if (deferTimeSpan < TimeSpan.Zero)
			{
				throw new ArgumentException("Defer time span cannot be negative");
			}
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId);
			if (acquireToken != item.AcquireToken)
			{
				throw new MessageDepotPermanentException(Strings.AcquireTokenMatchFail(item.Item.Id), null);
			}
			lock (item)
			{
				this.ChangeMessageState(item, MessageDepotItemState.Deferred);
				item.Item.DeferUntil = this.GetCurrentTime().Add(deferTimeSpan);
				this.messageDeactivatedHandlers[(int)item.Item.Stage](new MessageDeactivatedEventArgs(item, MessageDeactivationReason.Deferred));
				this.AddToNearLists(item);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002714 File Offset: 0x00000914
		public AcquireResult Acquire(TransportMessageId messageId)
		{
			Exception ex;
			AcquireResult result = this.Acquire(messageId, out ex);
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002734 File Offset: 0x00000934
		public bool TryAcquire(TransportMessageId messageId, out AcquireResult result)
		{
			Exception ex;
			result = this.Acquire(messageId, out ex);
			return ex == null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002750 File Offset: 0x00000950
		public void Release(TransportMessageId messageId, AcquireToken acquireToken)
		{
			ArgumentValidator.ThrowIfNull("acquireToken", acquireToken);
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId);
			if (acquireToken != item.AcquireToken)
			{
				throw new MessageDepotPermanentException(Strings.AcquireTokenMatchFail(item.Item.Id), null);
			}
			lock (item)
			{
				if (item.State != MessageDepotItemState.Processing)
				{
					throw new MessageDepotPermanentException(Strings.InvalidMessageStateTransition(messageId, item.State, MessageDepotItemState.Processing), null);
				}
				this.RemoveItemFromMessageDepot(item, true);
				MessageRemovedEventArgs args = new MessageRemovedEventArgs(item, MessageRemovalReason.Deleted, false);
				this.messageRemovedHandlers[(int)item.Item.Stage](args);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002804 File Offset: 0x00000A04
		public IMessageDepotItemWrapper Get(TransportMessageId messageId)
		{
			return this.GetItem(messageId);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002810 File Offset: 0x00000A10
		public bool TryGet(TransportMessageId messageId, out IMessageDepotItemWrapper item)
		{
			ArgumentValidator.ThrowIfNull("messageId", messageId);
			MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper;
			if (this.allMessages.TryGetValue(messageId, out messageDepotItemWrapper))
			{
				item = messageDepotItemWrapper;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002844 File Offset: 0x00000A44
		public void Remove(TransportMessageId messageId, bool withNdr)
		{
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId);
			lock (item)
			{
				this.RemoveItemFromMessageDepot(item, false);
				this.messageRemovedHandlers[(int)item.Item.Stage](new MessageRemovedEventArgs(item, MessageRemovalReason.Deleted, withNdr));
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void Suspend(TransportMessageId messageId)
		{
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId);
			lock (item)
			{
				this.ChangeMessageState(item, MessageDepotItemState.Suspended);
				this.messageDeactivatedHandlers[(int)item.Item.Stage](new MessageDeactivatedEventArgs(item, MessageDeactivationReason.Suspended));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000290C File Offset: 0x00000B0C
		public void Resume(TransportMessageId messageId)
		{
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId);
			lock (item)
			{
				if (item.State != MessageDepotItemState.Suspended)
				{
					throw new MessageDepotPermanentException(Strings.InvalidMessageStateTransition(messageId, item.State, MessageDepotItemState.Suspended), null);
				}
				this.ChangeMessageState(item, MessageDepotItemState.Ready);
				this.messageActivatedHandlers[(int)item.Item.Stage](new MessageActivatedEventArgs(item, MessageActivationReason.Resumed));
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void DehydrateAll()
		{
			Parallel.ForEach<MessageDepot.MessageDepotItemWrapper>(from itemWrapper in this.allMessages
			select itemWrapper.Value, delegate(MessageDepot.MessageDepotItemWrapper itemWrapper)
			{
				itemWrapper.Item.Dehydrate();
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A08 File Offset: 0x00000C08
		public void VisitMailItems(Func<IMessageDepotItemWrapper, bool> visitor)
		{
			foreach (MessageDepot.MessageDepotItemWrapper arg in from item in this.allMessages
			select item.Value)
			{
				if (!visitor(arg))
				{
					break;
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A7C File Offset: 0x00000C7C
		public long GetCount(MessageDepotItemStage stage, MessageDepotItemState state)
		{
			return this.messageCounterWrappers[(int)stage][(int)state].Count;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A8D File Offset: 0x00000C8D
		public void TimedUpdate()
		{
			this.UpdateNearLists();
			this.MoveDeferToReady();
			this.DelayMessages();
			this.ExpireMessages();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AB0 File Offset: 0x00000CB0
		private void ExpireMessages()
		{
			DateTime currentTime = this.GetCurrentTime();
			foreach (MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper in from item in this.nearExpiryMessages
			select item.Value)
			{
				lock (messageDepotItemWrapper)
				{
					if (messageDepotItemWrapper.State != MessageDepotItemState.Expiring && messageDepotItemWrapper.Item.ExpirationTime <= currentTime)
					{
						Exception ex;
						this.ChangeMessageState(messageDepotItemWrapper, MessageDepotItemState.Expiring, out ex);
						this.messageExpiredHandlers[(int)messageDepotItemWrapper.Item.Stage](new MessageEventArgs(messageDepotItemWrapper));
					}
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B9C File Offset: 0x00000D9C
		private void DelayMessages()
		{
			DateTime currentTime = this.GetCurrentTime();
			foreach (MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper in from item in this.nearDelayMessages
			select item.Value)
			{
				lock (messageDepotItemWrapper)
				{
					if ((messageDepotItemWrapper.State == MessageDepotItemState.Ready || messageDepotItemWrapper.State == MessageDepotItemState.Deferred) && !messageDepotItemWrapper.Item.IsDelayDsnGenerated && messageDepotItemWrapper.Item.ArrivalTime.Add(this.delayNotificationTimeout) <= currentTime)
					{
						this.messageDelayedHandlers[(int)messageDepotItemWrapper.Item.Stage](new MessageEventArgs(messageDepotItemWrapper));
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CA0 File Offset: 0x00000EA0
		private void MoveDeferToReady()
		{
			DateTime currentTime = this.GetCurrentTime();
			foreach (MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper in from item in this.nearDeferralOverMessages
			select item.Value)
			{
				lock (messageDepotItemWrapper)
				{
					if (messageDepotItemWrapper.State == MessageDepotItemState.Deferred && messageDepotItemWrapper.Item.DeferUntil <= currentTime)
					{
						Exception ex;
						this.ChangeMessageState(messageDepotItemWrapper, MessageDepotItemState.Ready, out ex);
						if (ex == null)
						{
							this.messageActivatedHandlers[(int)messageDepotItemWrapper.Item.Stage](new MessageActivatedEventArgs(messageDepotItemWrapper, MessageActivationReason.DeferralOver));
						}
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D84 File Offset: 0x00000F84
		private MessageDepot.MessageDepotItemWrapper GetItem(TransportMessageId messageId)
		{
			Exception ex;
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId, out ex);
			if (ex != null)
			{
				throw ex;
			}
			return item;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private MessageDepot.MessageDepotItemWrapper GetItem(TransportMessageId messageId, out Exception exception)
		{
			exception = null;
			ArgumentValidator.ThrowIfNull("messageId", messageId);
			MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper;
			if (!this.allMessages.TryGetValue(messageId, out messageDepotItemWrapper) || messageDepotItemWrapper == null)
			{
				exception = new ItemNotFoundException(messageId, Strings.ItemNotFound(messageId), null);
			}
			return messageDepotItemWrapper;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private void RaiseEventsAfterAddItem(MessageDepot.MessageDepotItemWrapper itemWrapper)
		{
			switch (itemWrapper.State)
			{
			case MessageDepotItemState.Ready:
				this.messageAddedHandlers[(int)itemWrapper.Item.Stage](new MessageEventArgs(itemWrapper));
				if (itemWrapper.Item.ExpirationTime > this.GetCurrentTime())
				{
					this.messageActivatedHandlers[(int)itemWrapper.Item.Stage](new MessageActivatedEventArgs(itemWrapper, MessageActivationReason.New));
					return;
				}
				break;
			case MessageDepotItemState.Deferred:
			case MessageDepotItemState.Poisoned:
			case MessageDepotItemState.Suspended:
			{
				MessageDeactivationReason reason = MessageDeactivationReason.Deferred;
				if (itemWrapper.State == MessageDepotItemState.Poisoned)
				{
					reason = MessageDeactivationReason.Poison;
				}
				else if (itemWrapper.State == MessageDepotItemState.Suspended)
				{
					reason = MessageDeactivationReason.Suspended;
				}
				this.messageAddedHandlers[(int)itemWrapper.Item.Stage](new MessageEventArgs(itemWrapper));
				this.messageDeactivatedHandlers[(int)itemWrapper.Item.Stage](new MessageDeactivatedEventArgs(itemWrapper, reason));
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EB8 File Offset: 0x000010B8
		private DateTime GetCurrentTime()
		{
			if (this.timeProvider == null)
			{
				return DateTime.UtcNow;
			}
			return this.timeProvider();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002ED4 File Offset: 0x000010D4
		private AcquireResult Acquire(TransportMessageId messageId, out Exception exception)
		{
			MessageDepot.MessageDepotItemWrapper item = this.GetItem(messageId, out exception);
			if (exception != null)
			{
				return null;
			}
			lock (item)
			{
				this.ChangeMessageState(item, MessageDepotItemState.Processing, out exception);
				if (exception != null)
				{
					return null;
				}
				item.AcquireToken = new AcquireToken();
			}
			return new AcquireResult(item, item.AcquireToken);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F44 File Offset: 0x00001144
		private void RemoveItemFromMessageDepot(MessageDepot.MessageDepotItemWrapper itemWrapper, bool forceRemove)
		{
			if (!forceRemove && !this.IsStateTransitionAllowed(this.statesAllowedForRemoveApi, itemWrapper.State))
			{
				throw new MessageDepotPermanentException(Strings.InvalidMessageStateForRemove(itemWrapper.Item.Id, itemWrapper.State), null);
			}
			MessageDepot.MessageDepotItemWrapper messageDepotItemWrapper;
			if (!this.allMessages.TryRemove(itemWrapper.Item.Id, out messageDepotItemWrapper))
			{
				throw new MessageDepotPermanentException(Strings.FailedToRemove(itemWrapper.Item.Id), null);
			}
			this.nearDeferralOverMessages.TryRemove(itemWrapper.Item.Id, out messageDepotItemWrapper);
			this.nearExpiryMessages.TryRemove(itemWrapper.Item.Id, out messageDepotItemWrapper);
			this.nearDelayMessages.TryRemove(itemWrapper.Item.Id, out messageDepotItemWrapper);
			this.DecrementMessageCount(itemWrapper);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003008 File Offset: 0x00001208
		private void AddToNearLists(MessageDepot.MessageDepotItemWrapper itemWrapper)
		{
			DateTime t = this.GetCurrentTime().Add(MessageDepot.DefaultNearTimeSpan);
			if (itemWrapper.Item.ExpirationTime < t)
			{
				this.nearExpiryMessages.TryAdd(itemWrapper.Item.Id, itemWrapper);
			}
			if (itemWrapper.Item.DeferUntil > DateTime.MinValue && itemWrapper.Item.DeferUntil < t)
			{
				this.nearDeferralOverMessages.TryAdd(itemWrapper.Item.Id, itemWrapper);
			}
			if (itemWrapper.Item.ArrivalTime.Add(this.delayNotificationTimeout) < t)
			{
				this.nearDelayMessages.TryAdd(itemWrapper.Item.Id, itemWrapper);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000030D8 File Offset: 0x000012D8
		private void UpdateNearLists()
		{
			if (this.lastNearListRefreshTime > DateTime.MinValue && this.lastNearListRefreshTime < this.GetCurrentTime().Add(MessageDepot.DefaultNearTimeSpan))
			{
				return;
			}
			this.nearDeferralOverMessages.Clear();
			this.nearDelayMessages.Clear();
			this.nearExpiryMessages.Clear();
			foreach (MessageDepot.MessageDepotItemWrapper itemWrapper in from item in this.allMessages
			select item.Value)
			{
				this.AddToNearLists(itemWrapper);
			}
			this.lastNearListRefreshTime = this.GetCurrentTime();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000031A8 File Offset: 0x000013A8
		private void IncrementMessageCount(MessageDepot.MessageDepotItemWrapper itemWrapper)
		{
			int stage = (int)itemWrapper.Item.Stage;
			int state = (int)itemWrapper.State;
			this.messageCounterWrappers[stage][state].Increment();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000031D8 File Offset: 0x000013D8
		private void DecrementMessageCount(MessageDepot.MessageDepotItemWrapper itemWrapper)
		{
			int stage = (int)itemWrapper.Item.Stage;
			int state = (int)itemWrapper.State;
			this.messageCounterWrappers[stage][state].Decrement();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003208 File Offset: 0x00001408
		private void SetNewMessageState(MessageDepot.MessageDepotItemWrapper itemWrapper)
		{
			if (itemWrapper.Item.IsPoison)
			{
				itemWrapper.SetState(MessageDepotItemState.Poisoned);
				return;
			}
			if (itemWrapper.Item.IsSuspended)
			{
				itemWrapper.SetState(MessageDepotItemState.Suspended);
				return;
			}
			if (itemWrapper.Item.DeferUntil > this.GetCurrentTime())
			{
				itemWrapper.SetState(MessageDepotItemState.Deferred);
				return;
			}
			itemWrapper.SetState(MessageDepotItemState.Ready);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003268 File Offset: 0x00001468
		private void ValidateAddArguments(IMessageDepotItem item)
		{
			ArgumentValidator.ThrowIfNull("item", item);
			if (item.Id == null)
			{
				throw new ArgumentException("Message Id cannot be null");
			}
			if (item.ArrivalTime > this.GetCurrentTime())
			{
				throw new ArgumentException("Message arrival time cannot be in future");
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032B8 File Offset: 0x000014B8
		private void CreateMessageTransitionMap()
		{
			this.messageTransitionMap = new BitVector32[MessageDepot.StateCount];
			this.messageTransitionMap[0] = this.GetAllowedStateMap(new MessageDepotItemState[]
			{
				MessageDepotItemState.Suspended,
				MessageDepotItemState.Suspended,
				MessageDepotItemState.Processing,
				MessageDepotItemState.Expiring
			});
			this.messageTransitionMap[1] = this.GetAllowedStateMap(new MessageDepotItemState[]
			{
				MessageDepotItemState.Ready,
				MessageDepotItemState.Suspended,
				MessageDepotItemState.Expiring
			});
			BitVector32[] array = this.messageTransitionMap;
			int num = 2;
			MessageDepotItemState[] allowedStates = new MessageDepotItemState[1];
			array[num] = this.GetAllowedStateMap(allowedStates);
			BitVector32[] array2 = this.messageTransitionMap;
			int num2 = 3;
			MessageDepotItemState[] allowedStates2 = new MessageDepotItemState[1];
			array2[num2] = this.GetAllowedStateMap(allowedStates2);
			this.messageTransitionMap[4] = this.GetAllowedStateMap(new MessageDepotItemState[]
			{
				MessageDepotItemState.Deferred
			});
			this.messageTransitionMap[5] = this.GetAllowedStateMap(new MessageDepotItemState[]
			{
				MessageDepotItemState.Processing
			});
			this.statesAllowedForRemoveApi = this.GetAllowedStateMap(new MessageDepotItemState[]
			{
				MessageDepotItemState.Ready,
				MessageDepotItemState.Deferred,
				MessageDepotItemState.Poisoned,
				MessageDepotItemState.Suspended
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033DC File Offset: 0x000015DC
		private void ChangeMessageState(MessageDepot.MessageDepotItemWrapper itemWrapper, MessageDepotItemState nextState)
		{
			Exception ex;
			this.ChangeMessageState(itemWrapper, nextState, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000033F8 File Offset: 0x000015F8
		private void ChangeMessageState(MessageDepot.MessageDepotItemWrapper itemWrapper, MessageDepotItemState nextState, out Exception exception)
		{
			exception = null;
			if (!this.IsStateTransitionAllowed(this.messageTransitionMap[(int)itemWrapper.State], nextState))
			{
				exception = new MessageDepotPermanentException(Strings.InvalidMessageStateTransition(itemWrapper.Item.Id, itemWrapper.State, nextState), null);
				return;
			}
			this.DecrementMessageCount(itemWrapper);
			itemWrapper.SetState(nextState);
			this.IncrementMessageCount(itemWrapper);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000345E File Offset: 0x0000165E
		private bool IsStateTransitionAllowed(BitVector32 bitVector, MessageDepotItemState nextState)
		{
			return bitVector[1 << (int)nextState];
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003470 File Offset: 0x00001670
		private BitVector32 GetAllowedStateMap(params MessageDepotItemState[] allowedStates)
		{
			BitVector32 result = new BitVector32(0);
			foreach (MessageDepotItemState messageDepotItemState in allowedStates)
			{
				result[1 << (int)messageDepotItemState] = true;
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static readonly TimeSpan DefaultNearTimeSpan = TimeSpan.FromHours(1.0);

		// Token: 0x04000002 RID: 2
		private static readonly TimeSpan DelayNotificationTimeout = TimeSpan.FromHours(4.0);

		// Token: 0x04000003 RID: 3
		private static readonly int StageCount = Enum.GetValues(typeof(MessageDepotItemStage)).Length;

		// Token: 0x04000004 RID: 4
		private static readonly int StateCount = Enum.GetValues(typeof(MessageDepotItemState)).Length;

		// Token: 0x04000005 RID: 5
		private readonly MessageEventHandler[] messageAddedHandlers = new MessageEventHandler[MessageDepot.StageCount];

		// Token: 0x04000006 RID: 6
		private readonly MessageActivatedEventHandler[] messageActivatedHandlers = new MessageActivatedEventHandler[MessageDepot.StageCount];

		// Token: 0x04000007 RID: 7
		private readonly MessageDeactivatedEventHandler[] messageDeactivatedHandlers = new MessageDeactivatedEventHandler[MessageDepot.StageCount];

		// Token: 0x04000008 RID: 8
		private readonly MessageRemovedEventHandler[] messageRemovedHandlers = new MessageRemovedEventHandler[MessageDepot.StageCount];

		// Token: 0x04000009 RID: 9
		private readonly MessageEventHandler[] messageExpiredHandlers = new MessageEventHandler[MessageDepot.StageCount];

		// Token: 0x0400000A RID: 10
		private readonly MessageEventHandler[] messageDelayedHandlers = new MessageEventHandler[MessageDepot.StageCount];

		// Token: 0x0400000B RID: 11
		private readonly ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper> allMessages = new ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper>();

		// Token: 0x0400000C RID: 12
		private readonly ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper> nearDeferralOverMessages = new ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper>();

		// Token: 0x0400000D RID: 13
		private readonly ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper> nearDelayMessages = new ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper>();

		// Token: 0x0400000E RID: 14
		private readonly ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper> nearExpiryMessages = new ConcurrentDictionary<TransportMessageId, MessageDepot.MessageDepotItemWrapper>();

		// Token: 0x0400000F RID: 15
		private readonly Func<DateTime> timeProvider;

		// Token: 0x04000010 RID: 16
		private readonly MessageDepot.CounterWrapper[][] messageCounterWrappers = new MessageDepot.CounterWrapper[MessageDepot.StageCount][];

		// Token: 0x04000011 RID: 17
		private readonly TimeSpan delayNotificationTimeout;

		// Token: 0x04000012 RID: 18
		private BitVector32[] messageTransitionMap;

		// Token: 0x04000013 RID: 19
		private BitVector32 statesAllowedForRemoveApi;

		// Token: 0x04000014 RID: 20
		private DateTime lastNearListRefreshTime = DateTime.MinValue;

		// Token: 0x02000003 RID: 3
		private class CounterWrapper
		{
			// Token: 0x0600003F RID: 63 RVA: 0x0000350D File Offset: 0x0000170D
			public CounterWrapper(ExPerformanceCounter performanceCounter)
			{
				ArgumentValidator.ThrowIfNull("performanceCounter", performanceCounter);
				this.performanceCounter = performanceCounter;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000040 RID: 64 RVA: 0x00003527 File Offset: 0x00001727
			public long Count
			{
				get
				{
					return this.messageCount;
				}
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00003530 File Offset: 0x00001730
			public void Increment()
			{
				long rawValue = Interlocked.Increment(ref this.messageCount);
				this.performanceCounter.RawValue = rawValue;
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00003558 File Offset: 0x00001758
			public void Decrement()
			{
				long rawValue = Interlocked.Decrement(ref this.messageCount);
				this.performanceCounter.RawValue = rawValue;
			}

			// Token: 0x04000022 RID: 34
			private readonly ExPerformanceCounter performanceCounter;

			// Token: 0x04000023 RID: 35
			private long messageCount;
		}

		// Token: 0x02000004 RID: 4
		private class MessageDepotItemWrapper : IMessageDepotItemWrapper
		{
			// Token: 0x06000043 RID: 67 RVA: 0x0000357D File Offset: 0x0000177D
			public MessageDepotItemWrapper(IMessageDepotItem item, MessageDepotItemState state)
			{
				ArgumentValidator.ThrowIfNull("item", item);
				this.item = item;
				this.state = state;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000044 RID: 68 RVA: 0x0000359E File Offset: 0x0000179E
			// (set) Token: 0x06000045 RID: 69 RVA: 0x000035A6 File Offset: 0x000017A6
			public AcquireToken AcquireToken
			{
				get
				{
					return this.acquireToken;
				}
				set
				{
					this.acquireToken = value;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000046 RID: 70 RVA: 0x000035AF File Offset: 0x000017AF
			public MessageDepotItemState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000047 RID: 71 RVA: 0x000035B7 File Offset: 0x000017B7
			public IMessageDepotItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000035BF File Offset: 0x000017BF
			public void SetState(MessageDepotItemState newState)
			{
				this.state = newState;
			}

			// Token: 0x04000024 RID: 36
			private readonly IMessageDepotItem item;

			// Token: 0x04000025 RID: 37
			private MessageDepotItemState state;

			// Token: 0x04000026 RID: 38
			private AcquireToken acquireToken;
		}
	}
}
