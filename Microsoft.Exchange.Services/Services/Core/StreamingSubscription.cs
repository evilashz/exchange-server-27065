using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200023E RID: 574
	internal sealed class StreamingSubscription : SubscriptionBase
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x0004A2AC File Offset: 0x000484AC
		private void InternalEventAvailableHandler(object obj)
		{
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				Action<StreamingSubscription> action = obj as Action<StreamingSubscription>;
				if (action != null)
				{
					action(this);
				}
			});
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0004A318 File Offset: 0x00048518
		private void InternalDisconnectSubscriptionHandler(object obj)
		{
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				Action<StreamingSubscription, LocalizedException> action = obj as Action<StreamingSubscription, LocalizedException>;
				if (action != null)
				{
					action(this, new SubscriptionNewConnectionOpenedException());
				}
			});
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004A384 File Offset: 0x00048584
		private void InternalDisposeSubscriptionHandler(object obj)
		{
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				Action<StreamingSubscription, LocalizedException> action = obj as Action<StreamingSubscription, LocalizedException>;
				if (action != null)
				{
					action(this, new SubscriptionUnsubscribedException());
				}
			});
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004A3B6 File Offset: 0x000485B6
		public StreamingSubscription(StreamingSubscriptionRequest subscriptionRequest, IdAndSession[] folderIds, string owner) : base(subscriptionRequest, folderIds)
		{
			this.owner = owner;
			this.SetExpirationDateTime();
			base.EventQueue.RegisterEventAvailableHandler(new EventQueue.EventAvailableHandler(this.EventAvailableHandler));
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0004A3EF File Offset: 0x000485EF
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0004A3F6 File Offset: 0x000485F6
		internal static int TimeToLiveDefault
		{
			get
			{
				return StreamingSubscription.timeToLiveInSeconds;
			}
			set
			{
				StreamingSubscription.timeToLiveInSeconds = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0004A3FE File Offset: 0x000485FE
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0004A405 File Offset: 0x00048605
		internal static int NewEventQueueSize
		{
			get
			{
				return StreamingSubscription.newEventQueueSize;
			}
			set
			{
				StreamingSubscription.newEventQueueSize = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0004A40D File Offset: 0x0004860D
		internal ExDateTime ExpirationDateTime
		{
			get
			{
				return this.expirationDateTime;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0004A418 File Offset: 0x00048618
		private void SetExpirationDateTime()
		{
			this.expirationDateTime = ExDateTime.UtcNow.AddSeconds((double)StreamingSubscription.timeToLiveInSeconds);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0004A440 File Offset: 0x00048640
		public EwsNotificationType GetEvents(int maxEventCount, out int eventCount)
		{
			EwsNotificationType result;
			try
			{
				lock (this.lockObject)
				{
					ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "StreamingSubscription.GetEvents. Subscription: {0}. Current queue length is {1}{2}.", base.TraceIdentifier, (base.EventQueue == null) ? "<null>" : base.EventQueue.CurrentEventsCount.ToString(), (base.EventQueue == null) ? "<null>" : (base.EventQueue.HasMissedEvents ? " (missed events)" : string.Empty));
					if (this.isDisposed)
					{
						throw new InvalidSubscriptionException();
					}
					if (this.isExpired)
					{
						ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "StreamingSubscription.GetEvents. ExpiredSubscriptionException. Subscription: {0}.", base.TraceIdentifier);
						throw new ExpiredSubscriptionException();
					}
					if (base.EventQueue.HasMissedEvents)
					{
						ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "StreamingSubscription.GetEvents. EventQueueOverflowException. Subscription: {0}.", base.TraceIdentifier);
						this.ResetEventQueue();
						throw new EventQueueOverflowException();
					}
					BudgetKey budgetKey = null;
					IEwsBudget budget = CallContext.Current.Budget;
					if (budget == null)
					{
						ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string>((long)this.GetHashCode(), "StreamingSubscription.GetEvents. Subscription: {0}. currentCallContextBudget == null", base.TraceIdentifier);
					}
					else
					{
						budgetKey = budget.Owner;
					}
					if (budgetKey != null && budgetKey.Equals(base.BudgetKey))
					{
						EwsNotificationType events = base.GetEvents(base.LastWatermarkSent, maxEventCount, out eventCount);
						result = events;
					}
					else
					{
						using (IEwsBudget ewsBudget = EwsBudget.Acquire(base.BudgetKey))
						{
							try
							{
								ewsBudget.StartLocal("StreamingSubscription.GetEvents", default(TimeSpan));
								EwsNotificationType events2 = base.GetEvents(base.LastWatermarkSent, maxEventCount, out eventCount);
								result = events2;
							}
							finally
							{
								ewsBudget.LogEndStateToIIS();
							}
						}
					}
				}
			}
			catch (LocalizedException ex)
			{
				if (!ex.Data.Contains(StreamingConnection.IsNonFatalSubscriptionExceptionKey) || (string)ex.Data[StreamingConnection.IsNonFatalSubscriptionExceptionKey] != bool.TrueString)
				{
					this.encounteredFatalError = true;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0004A694 File Offset: 0x00048894
		private void ResetEventQueue()
		{
			base.EventQueue.ResetQueue();
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0004A6A4 File Offset: 0x000488A4
		public override bool IsExpired
		{
			get
			{
				if (!this.isExpired)
				{
					lock (this.lockObject)
					{
						this.isExpired = (this.expirationDateTime < ExDateTime.UtcNow && this.openConnection == null);
					}
				}
				return this.isExpired;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0004A710 File Offset: 0x00048910
		public override bool UseWatermarks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0004A713 File Offset: 0x00048913
		protected override int EventQueueSize
		{
			get
			{
				return StreamingSubscription.NewEventQueueSize;
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0004A71C File Offset: 0x0004891C
		private void EventAvailableHandler()
		{
			lock (this.connectionLock)
			{
				ExTraceGlobals.SubscriptionBaseTracer.TraceDebug<string, string>((long)this.GetHashCode(), "StreamingSubscription.EventAvailableHandler. Subscription: {0}. New events available {1} active connection.", base.TraceIdentifier, (this.openConnection != null) ? "with" : "without");
				if (this.openConnection != null && !this.openConnection.IsDisposed)
				{
					Action<StreamingSubscription> state = new Action<StreamingSubscription>(this.openConnection.EventsAvailable);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.InternalEventAvailableHandler), state);
				}
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004A7C4 File Offset: 0x000489C4
		public override bool CheckCallerHasRights(CallContext callContext)
		{
			string identifierString = callContext.OriginalCallerContext.IdentifierString;
			if (callContext.IsPartnerUser)
			{
				int num = identifierString.IndexOf('\\');
				if (num > 0)
				{
					string value = identifierString.Substring(num);
					return this.owner.EndsWith(value);
				}
			}
			return this.owner.Equals(identifierString, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0004A814 File Offset: 0x00048A14
		internal void RegisterConnection(ISubscriptionEventHandler connection)
		{
			lock (this.connectionLock)
			{
				if (this.openConnection != connection)
				{
					if (this.openConnection != null && !this.openConnection.IsDisposed)
					{
						Action<StreamingSubscription, LocalizedException> state = new Action<StreamingSubscription, LocalizedException>(this.openConnection.DisconnectSubscription);
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.InternalDisconnectSubscriptionHandler), state);
					}
					this.openConnection = connection;
					if (!base.EventQueue.IsQueueEmptyAndUpToDate())
					{
						Action<StreamingSubscription> state2 = new Action<StreamingSubscription>(this.openConnection.EventsAvailable);
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.InternalEventAvailableHandler), state2);
					}
				}
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0004A8CC File Offset: 0x00048ACC
		internal void UnregisterConnection(ISubscriptionEventHandler connection)
		{
			lock (this.connectionLock)
			{
				if (this.openConnection != connection)
				{
					return;
				}
				this.openConnection = null;
			}
			lock (this.lockObject)
			{
				if (this.encounteredFatalError)
				{
					Subscriptions.Singleton.Delete(base.SubscriptionId);
				}
				else
				{
					this.SetExpirationDateTime();
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0004A964 File Offset: 0x00048B64
		internal bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0004A96C File Offset: 0x00048B6C
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			lock (this.connectionLock)
			{
				if (isDisposing && this.openConnection != null)
				{
					Action<StreamingSubscription, LocalizedException> state = new Action<StreamingSubscription, LocalizedException>(this.openConnection.DisconnectSubscription);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.InternalDisposeSubscriptionHandler), state);
				}
			}
		}

		// Token: 0x04000B8E RID: 2958
		internal const int DefaultEventQueueSize = 500;

		// Token: 0x04000B8F RID: 2959
		internal const int DefaultTimeToLive = 1800;

		// Token: 0x04000B90 RID: 2960
		private readonly string owner;

		// Token: 0x04000B91 RID: 2961
		private static int timeToLiveInSeconds = 1800;

		// Token: 0x04000B92 RID: 2962
		private static int newEventQueueSize = 500;

		// Token: 0x04000B93 RID: 2963
		private bool isExpired;

		// Token: 0x04000B94 RID: 2964
		private bool encounteredFatalError;

		// Token: 0x04000B95 RID: 2965
		private ExDateTime expirationDateTime;

		// Token: 0x04000B96 RID: 2966
		private ISubscriptionEventHandler openConnection;

		// Token: 0x04000B97 RID: 2967
		private object connectionLock = new object();
	}
}
