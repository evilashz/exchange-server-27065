using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200019C RID: 412
	internal class PendingRequestManager : DisposeTrackableBase
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00039174 File Offset: 0x00037374
		internal PendingRequestManager() : this(null, ListenerChannelsManager.Instance)
		{
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00039184 File Offset: 0x00037384
		internal PendingRequestManager(IMailboxContext userContext, ListenerChannelsManager listenerChannelsManager)
		{
			this.notifiersStateLock = new OwaRWLockWrapper();
			this.notifierDataAvailableState = new Dictionary<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState>();
			this.userContext = userContext;
			this.listenerChannelsManager = listenerChannelsManager;
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000ED6 RID: 3798 RVA: 0x000391DC File Offset: 0x000373DC
		// (remove) Token: 0x06000ED7 RID: 3799 RVA: 0x00039214 File Offset: 0x00037414
		public event EventHandler<EventArgs> ClientDisconnected;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000ED8 RID: 3800 RVA: 0x0003924C File Offset: 0x0003744C
		// (remove) Token: 0x06000ED9 RID: 3801 RVA: 0x00039284 File Offset: 0x00037484
		public event EventHandler<EventArgs> KeepAlive;

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000392B9 File Offset: 0x000374B9
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x000392C1 File Offset: 0x000374C1
		internal bool ShouldDispose { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x000392CC File Offset: 0x000374CC
		internal RemoteNotifier GetRemoteNotifier
		{
			get
			{
				if (this.remoteNotifier == null)
				{
					lock (this.syncRoot)
					{
						if (this.remoteNotifier == null)
						{
							this.remoteNotifier = this.CreateRemoteNotifier();
							this.AddPendingRequestNotifier(this.remoteNotifier);
						}
					}
				}
				return this.remoteNotifier;
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00039334 File Offset: 0x00037534
		internal void AddPendingRequestNotifier(IPendingRequestNotifier notifier)
		{
			try
			{
				if (notifier == null)
				{
					throw new ArgumentNullException("notifier");
				}
				if (this.notifiersStateLock.LockWriterElastic(5000))
				{
					this.notifierDataAvailableState.Add(notifier, new PendingRequestManager.PendingNotifierState());
					notifier.DataAvailable += this.OnNotifierDataAvailable;
				}
			}
			finally
			{
				if (this.notifiersStateLock.IsWriterLockHeld)
				{
					this.notifiersStateLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000393B0 File Offset: 0x000375B0
		internal void RemovePendingRequestNotifier(IPendingRequestNotifier notifier)
		{
			try
			{
				if (notifier == null)
				{
					throw new ArgumentNullException("notifier");
				}
				if (this.notifiersStateLock.LockWriterElastic(5000))
				{
					this.notifierDataAvailableState.Remove(notifier);
					notifier.DataAvailable -= this.OnNotifierDataAvailable;
				}
			}
			finally
			{
				if (this.notifiersStateLock.IsWriterLockHeld)
				{
					this.notifiersStateLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00039428 File Offset: 0x00037628
		internal PendingRequestChannel AddPendingGetChannel(string channelId)
		{
			if (this.pendingRequestChannels != null)
			{
				lock (this.pendingRequestChannels)
				{
					if (this.pendingRequestChannels != null && this.pendingRequestChannels.Count <= 10 && !this.pendingRequestChannels.ContainsKey(channelId))
					{
						PendingRequestChannel pendingRequestChannel = new PendingRequestChannel(this, channelId);
						this.pendingRequestChannels.Add(channelId, pendingRequestChannel);
						this.listenerChannelsManager.AddPendingGetChannel(channelId, this);
						if (this.userContext.ExchangePrincipal != null && !string.IsNullOrEmpty(this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()))
						{
							OwaServerLogger.AppendToLog(new PendingRequestChannelLogEvent(this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), channelId));
						}
						return pendingRequestChannel;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003952C File Offset: 0x0003772C
		internal PendingRequestChannel GetPendingGetChannel(string channelId)
		{
			PendingRequestChannel result = null;
			if (this.pendingRequestChannels != null)
			{
				this.pendingRequestChannels.TryGetValue(channelId, out result);
			}
			return result;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003958C File Offset: 0x0003778C
		internal void RemovePendingGetChannel(string channelId)
		{
			if (this.pendingRequestChannels != null)
			{
				lock (this.pendingRequestChannels)
				{
					PendingRequestChannel pendingGetChannel = this.GetPendingGetChannel(channelId);
					if (pendingGetChannel != null)
					{
						this.pendingRequestChannels.Remove(channelId);
						pendingGetChannel.Dispose();
					}
					this.listenerChannelsManager.RemovePendingGetChannel(channelId);
				}
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[PendingRequestManager::RemovePendingGetChannel] ChannelId: {0}", channelId);
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					if (this.userContext.NotificationManager != null)
					{
						this.userContext.NotificationManager.ReleaseSubscriptionsForChannelId(channelId);
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.DisposeXSOObjects Unable to dispose object.  exception {0}", ex.Message);
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00039684 File Offset: 0x00037884
		internal bool HasAnyActivePendingGetChannel()
		{
			if (this.pendingRequestChannels != null)
			{
				lock (this.pendingRequestChannels)
				{
					if (this.pendingRequestChannels != null)
					{
						foreach (KeyValuePair<string, PendingRequestChannel> keyValuePair in this.pendingRequestChannels)
						{
							if (keyValuePair.Value.IsActive)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00039720 File Offset: 0x00037920
		internal int GetChannelCount()
		{
			if (this.pendingRequestChannels != null)
			{
				lock (this.pendingRequestChannels)
				{
					if (this.pendingRequestChannels != null)
					{
						return this.pendingRequestChannels.Count;
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003977C File Offset: 0x0003797C
		internal long GetMarkAndReset(string channelId)
		{
			long result = 0L;
			if (this.channelNotificationMarks != null && this.channelNotificationMarks.ContainsKey(channelId))
			{
				result = this.channelNotificationMarks[channelId];
			}
			this.channelNotificationMarks[channelId] = 0L;
			return result;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000397C0 File Offset: 0x000379C0
		internal void FireKeepAlive()
		{
			EventHandler<EventArgs> keepAlive = this.KeepAlive;
			if (keepAlive != null)
			{
				keepAlive(this, new EventArgs());
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000397E4 File Offset: 0x000379E4
		internal void FireClientDisconnect()
		{
			EventArgs e = new EventArgs();
			EventHandler<EventArgs> clientDisconnected = this.ClientDisconnected;
			if (clientDisconnected != null)
			{
				clientDisconnected(this, e);
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003980C File Offset: 0x00037A0C
		internal void OnNotifierDataAvailable(object sender, EventArgs args)
		{
			bool flag = false;
			try
			{
				if (!this.notifiersStateLock.IsReaderLockHeld)
				{
					this.notifiersStateLock.LockReaderElastic(5000);
					flag = true;
				}
				if (sender == null)
				{
					throw new ArgumentNullException("sender");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "OnNotifierDataAvailable called by {0} / {1}", sender.GetType().Name, sender.GetHashCode());
				IPendingRequestNotifier key = (IPendingRequestNotifier)sender;
				PendingRequestManager.PendingNotifierState pendingNotifierState = null;
				if (!this.notifierDataAvailableState.TryGetValue(key, out pendingNotifierState))
				{
					throw new ArgumentException("The sender object is not registered in the manager class");
				}
				int num = pendingNotifierState.CompareExchangeState(1, 0);
				if (num != 0)
				{
					throw new OwaInvalidOperationException("OnNotifierDataAvailable should not be called if the manager did not consume the notifier's data yet. Notifier:" + sender.ToString());
				}
				this.WriteNotification(false);
			}
			catch (Exception e)
			{
				this.HandleException(sender.GetType().Name + ".Error", e);
			}
			finally
			{
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00039910 File Offset: 0x00037B10
		protected virtual RemoteNotifier CreateRemoteNotifier()
		{
			return new RemoteNotifier(this.userContext);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003991D File Offset: 0x00037B1D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PendingRequestManager>(this);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00039ACC File Offset: 0x00037CCC
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed && isDisposing)
			{
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						try
						{
							bool flag = false;
							try
							{
								flag = this.notifiersStateLock.LockWriterElastic(5000);
							}
							catch (OwaLockTimeoutException)
							{
								ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Dispose was called but the writer lock is not available:");
								return;
							}
							if (!flag)
							{
								ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Dispose was called but the writer lock is not available:");
								return;
							}
							foreach (KeyValuePair<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> keyValuePair in this.notifierDataAvailableState)
							{
								keyValuePair.Value.Dispose();
							}
							this.notifierDataAvailableState = null;
						}
						finally
						{
							if (this.notifiersStateLock.IsWriterLockHeld)
							{
								this.notifiersStateLock.ReleaseWriterLock();
							}
						}
						lock (this.pendingRequestChannels)
						{
							if (this.pendingRequestChannels != null)
							{
								foreach (string channelId in this.pendingRequestChannels.Keys)
								{
									PendingRequestChannel pendingGetChannel = this.GetPendingGetChannel(channelId);
									if (pendingGetChannel != null)
									{
										pendingGetChannel.Dispose();
									}
									this.listenerChannelsManager.RemovePendingGetChannel(channelId);
								}
							}
							this.pendingRequestChannels.Clear();
							this.pendingRequestChannels = null;
						}
						if (this.budget != null)
						{
							this.budget.Dispose();
							this.budget = null;
						}
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<string>(0L, "MapiNotificationHandlerBase.DisposeXSOObjects Unable to dispose object.  exception {0}", ex.Message);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00039B30 File Offset: 0x00037D30
		private void HandleException(string eventId, Exception e)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "There was an exception on the notification thread: {0}", e.ToString());
			OwaServerLogger.AppendToLog(new ExceptionLogEvent(eventId, this.userContext, e));
			if (this.pendingRequestChannels != null)
			{
				lock (this.pendingRequestChannels)
				{
					if (this.pendingRequestChannels != null)
					{
						foreach (KeyValuePair<string, PendingRequestChannel> keyValuePair in this.pendingRequestChannels)
						{
							keyValuePair.Value.HandleException(e, false);
						}
					}
				}
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00039BF4 File Offset: 0x00037DF4
		private void WriteNotification(bool asyncOperation)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "Writing notifications for {0}.", this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress);
			bool flag = false;
			try
			{
				if (!this.notifiersStateLock.IsReaderLockHeld)
				{
					this.notifiersStateLock.LockReaderElastic(5000);
					flag = true;
				}
				TimeSpan value = new TimeSpan(DateTime.UtcNow.Ticks);
				double num = 0.0;
				if (this.startThrottleTime != null)
				{
					num = value.Subtract(this.startThrottleTime.Value).TotalSeconds;
				}
				if (this.startThrottleTime == null || num > 10.0)
				{
					this.startThrottleTime = new TimeSpan?(value);
				}
				foreach (KeyValuePair<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> keyValuePair in this.notifierDataAvailableState)
				{
					IPendingRequestNotifier key = keyValuePair.Key;
					PendingRequestManager.PendingNotifierState value2 = keyValuePair.Value;
					int num2 = value2.CompareExchangeState(0, 1);
					if (num2 != 1)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "PendingRequestManager.WriteNotification is skipping notifier {0} / {1} with state {2}.", key.GetType().Name, key.GetHashCode(), num2);
					}
					else
					{
						this.WriteNotification(asyncOperation, num, key, value2);
					}
				}
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00039E88 File Offset: 0x00038088
		private void WriteNotification(bool asyncOperation, double throttleInterval, IPendingRequestNotifier notifier, PendingRequestManager.PendingNotifierState notifierState)
		{
			bool flag = false;
			if (notifier.ShouldThrottle)
			{
				int num = notifierState.IncrementOnDataAvailableThrottleCount();
				if (num > 100)
				{
					flag = true;
				}
				else if (num == 100 && throttleInterval <= 10.0)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Start throttling mechanism - timer was started and from now on notifier {0} / {1} will be on throttling mode ", notifier.GetType().Name, notifier.GetHashCode());
					flag = true;
					if (notifierState.ThrottleTimer == null)
					{
						notifierState.ThrottleTimer = new Timer(new TimerCallback(this.ThrottleTimeout), notifier, 20000, -1);
					}
					else
					{
						notifierState.ThrottleTimer.Change(20000, -1);
					}
				}
				if (num <= 100 && throttleInterval > 10.0 && num != 1)
				{
					notifierState.ExchangeOnDataAvailableThrottleCount(1);
				}
			}
			if (flag)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "PendingRequestManager.WriteNotification throttled notifier: {0} / {1}", notifier.GetType().Name, notifier.GetHashCode());
				return;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int>((long)this.GetHashCode(), "PendingRequestManager.WriteNotification is reading data from the notifier. Notifier: {0} / {1}", notifier.GetType().Name, notifier.GetHashCode());
			try
			{
				List<NotificationPayloadBase> payloadList = (List<NotificationPayloadBase>)notifier.ReadDataAndResetState();
				if (notifier.SubscriptionId != null)
				{
					Pusher.Instance.Distribute(payloadList, notifier.ContextKey, notifier.SubscriptionId);
				}
				if (this.pendingRequestChannels != null)
				{
					lock (this.pendingRequestChannels)
					{
						if (this.pendingRequestChannels != null)
						{
							if (this.budget == null)
							{
								this.budget = StandardBudget.Acquire(this.userContext.ExchangePrincipal.Sid, BudgetType.Owa, this.userContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings());
							}
							this.budget.CheckOverBudget();
							this.budget.StartLocal("PendingRequestManager.WriteNotification", default(TimeSpan));
							try
							{
								using (Dictionary<string, PendingRequestChannel>.Enumerator enumerator = this.pendingRequestChannels.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										KeyValuePair<string, PendingRequestChannel> channel = enumerator.Current;
										try
										{
											OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
											{
												KeyValuePair<string, PendingRequestChannel> channel = channel;
												channel.Value.WritePayload(asyncOperation, payloadList);
												Dictionary<string, long> dictionary = this.channelNotificationMarks;
												KeyValuePair<string, PendingRequestChannel> channel2 = channel;
												if (dictionary.ContainsKey(channel2.Key))
												{
													Dictionary<string, long> dictionary3;
													Dictionary<string, long> dictionary2 = dictionary3 = this.channelNotificationMarks;
													KeyValuePair<string, PendingRequestChannel> channel3 = channel;
													string key;
													dictionary2[key = channel3.Key] = dictionary3[key] + (long)payloadList.Count;
													return;
												}
												Dictionary<string, long> dictionary4 = this.channelNotificationMarks;
												KeyValuePair<string, PendingRequestChannel> channel4 = channel;
												dictionary4.Add(channel4.Key, (long)payloadList.Count);
											});
										}
										catch (GrayException ex)
										{
											Exception ex2 = (ex.InnerException != null) ? ex.InnerException : ex;
											ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "Exception when writing the notifications to the client. Notifier {0} / {1}, exception message: {2}, stack: {3};", new object[]
											{
												notifier.GetType().Name,
												notifier.GetHashCode(),
												ex2.Message,
												ex2.StackTrace
											});
										}
									}
								}
							}
							finally
							{
								this.budget.EndLocal();
							}
						}
					}
				}
			}
			catch (Exception ex3)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<string, int, string>((long)this.GetHashCode(), "Exception when writing the notifications to the client. Notifier {0} / {1}, exception message: {2};", notifier.GetType().Name, notifier.GetHashCode(), (ex3.InnerException != null) ? ex3.InnerException.Message : ex3.Message);
				throw;
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003A23C File Offset: 0x0003843C
		private void ThrottleTimeout(object state)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Throttle Timeout method was called - throttle timeout elapsed");
			IPendingRequestNotifier pendingRequestNotifier = state as IPendingRequestNotifier;
			bool flag = false;
			try
			{
				if (pendingRequestNotifier == null)
				{
					throw new ArgumentException("State parameter is invalid");
				}
				if (!this.notifiersStateLock.IsReaderLockHeld)
				{
					this.notifiersStateLock.LockReaderElastic(5000);
					flag = true;
				}
				PendingRequestManager.PendingNotifierState pendingNotifierState = null;
				if (!this.notifierDataAvailableState.TryGetValue(pendingRequestNotifier, out pendingNotifierState))
				{
					throw new ArgumentException("The sender object is not registered in the manager class");
				}
				pendingNotifierState.ExchangeOnDataAvailableThrottleCount(0);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Notifier {0} is not on throttle period anymore", pendingRequestNotifier.GetHashCode());
				this.OnNotifierDataAvailable(pendingRequestNotifier, null);
			}
			catch (Exception e)
			{
				this.HandleException("NotificationThrottleTimeout", e);
			}
			finally
			{
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x04000901 RID: 2305
		private const int LockTimeout = 5000;

		// Token: 0x04000902 RID: 2306
		private const int ThrottleIntervalInSec = 10;

		// Token: 0x04000903 RID: 2307
		private const int ThrottleTimerCallbackInMilliSeconds = 20000;

		// Token: 0x04000904 RID: 2308
		private const int ThrottlingThreshold = 100;

		// Token: 0x04000905 RID: 2309
		private const int MaxChannels = 10;

		// Token: 0x04000906 RID: 2310
		private readonly object syncRoot = new object();

		// Token: 0x04000907 RID: 2311
		private IStandardBudget budget;

		// Token: 0x04000908 RID: 2312
		private TimeSpan? startThrottleTime;

		// Token: 0x04000909 RID: 2313
		private Dictionary<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> notifierDataAvailableState;

		// Token: 0x0400090A RID: 2314
		private OwaRWLockWrapper notifiersStateLock;

		// Token: 0x0400090B RID: 2315
		private Dictionary<string, PendingRequestChannel> pendingRequestChannels = new Dictionary<string, PendingRequestChannel>();

		// Token: 0x0400090C RID: 2316
		private Dictionary<string, long> channelNotificationMarks = new Dictionary<string, long>();

		// Token: 0x0400090D RID: 2317
		private bool disposed;

		// Token: 0x0400090E RID: 2318
		private IMailboxContext userContext;

		// Token: 0x0400090F RID: 2319
		private ListenerChannelsManager listenerChannelsManager;

		// Token: 0x04000910 RID: 2320
		private RemoteNotifier remoteNotifier;

		// Token: 0x0200019D RID: 413
		private class PendingNotifierState : DisposeTrackableBase
		{
			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003A320 File Offset: 0x00038520
			// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0003A328 File Offset: 0x00038528
			public Timer ThrottleTimer
			{
				get
				{
					return this.throttleTimer;
				}
				set
				{
					this.throttleTimer = value;
				}
			}

			// Token: 0x06000EF2 RID: 3826 RVA: 0x0003A331 File Offset: 0x00038531
			internal int CompareExchangeState(int value, int comparand)
			{
				return Interlocked.CompareExchange(ref this.state, value, comparand);
			}

			// Token: 0x06000EF3 RID: 3827 RVA: 0x0003A340 File Offset: 0x00038540
			internal int ExchangeOnDataAvailableThrottleCount(int value)
			{
				return Interlocked.Exchange(ref this.onDataAvailableThrottleCount, value);
			}

			// Token: 0x06000EF4 RID: 3828 RVA: 0x0003A34E File Offset: 0x0003854E
			internal int IncrementOnDataAvailableThrottleCount()
			{
				return Interlocked.Increment(ref this.onDataAvailableThrottleCount);
			}

			// Token: 0x06000EF5 RID: 3829 RVA: 0x0003A35B File Offset: 0x0003855B
			protected override void InternalDispose(bool isDisposing)
			{
				if (!this.disposed)
				{
					if (isDisposing && this.ThrottleTimer != null)
					{
						this.ThrottleTimer.Dispose();
						this.ThrottleTimer = null;
					}
					this.disposed = true;
				}
			}

			// Token: 0x06000EF6 RID: 3830 RVA: 0x0003A389 File Offset: 0x00038589
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<PendingRequestManager.PendingNotifierState>(this);
			}

			// Token: 0x04000914 RID: 2324
			private int state;

			// Token: 0x04000915 RID: 2325
			private int onDataAvailableThrottleCount;

			// Token: 0x04000916 RID: 2326
			private Timer throttleTimer;

			// Token: 0x04000917 RID: 2327
			private bool disposed;
		}
	}
}
