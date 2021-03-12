using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200020C RID: 524
	public class PendingRequestManager : DisposeTrackableBase
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060011AC RID: 4524 RVA: 0x0006AAFC File Offset: 0x00068CFC
		// (remove) Token: 0x060011AD RID: 4525 RVA: 0x0006AB34 File Offset: 0x00068D34
		public event EventHandler<EventArgs> ClientDisconnected;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060011AE RID: 4526 RVA: 0x0006AB6C File Offset: 0x00068D6C
		// (remove) Token: 0x060011AF RID: 4527 RVA: 0x0006ABA4 File Offset: 0x00068DA4
		public event EventHandler<EventArgs> KeepAlive;

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0006ABD9 File Offset: 0x00068DD9
		internal ChunkedHttpResponse ChunkedHttpResponse
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0006ABE1 File Offset: 0x00068DE1
		internal PendingRequestManager(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.notifiersStateLock = new OwaRWLockWrapper();
			this.notifierDataAvaiableState = new Dictionary<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState>();
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0006AC1F File Offset: 0x00068E1F
		public static bool IsPendingGetRequired(UserContext userContext)
		{
			return userContext.IsInstantMessageEnabled() || PerformanceConsole.IsPerformanceConsoleEnabled(userContext) || userContext.IsPushNotificationsEnabled;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0006AC39 File Offset: 0x00068E39
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PendingRequestManager>(this);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0006AC44 File Offset: 0x00068E44
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed)
			{
				if (isDisposing)
				{
					this.HandleFinishRequestFromClient();
					if (this.pendingRequestAliveTimer != null)
					{
						this.pendingRequestAliveTimer.Dispose();
						this.pendingRequestAliveTimer = null;
					}
					if (this.pendingRequestEventHandler != null && !this.pendingRequestEventHandler.IsDisposed && this.lockTracker.TryReleaseAllLocks(new PendingNotifierLockTracker.ReleaseAllLocksCallback(this.pendingRequestEventHandler.Dispose)))
					{
						this.pendingRequestEventHandler.Dispose();
					}
					this.pendingRequestEventHandler = null;
					LockCookie? lockCookie = null;
					try
					{
						try
						{
							this.notifiersStateLock.LockWriterElastic(5000);
						}
						catch (OwaLockTimeoutException)
						{
							ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Dispose was called but the writer lock is not available:");
							return;
						}
						foreach (KeyValuePair<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> keyValuePair in this.notifierDataAvaiableState)
						{
							keyValuePair.Value.Dispose();
						}
					}
					finally
					{
						if (this.notifiersStateLock.IsWriterLockHeld)
						{
							if (lockCookie != null)
							{
								LockCookie value = lockCookie.Value;
								this.notifiersStateLock.DowngradeFromWriterLock(ref value);
							}
							else
							{
								this.notifiersStateLock.ReleaseWriterLock();
							}
						}
						if (lockCookie != null && !this.notifiersStateLock.IsReaderLockHeld)
						{
							ExAssert.RetailAssert(true, "Lost readerwriterlock that was acquired before entering the method");
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0006ADC8 File Offset: 0x00068FC8
		internal void AddPendingRequestNotifier(IPendingRequestNotifier notifier)
		{
			LockCookie? lockCookie = null;
			try
			{
				if (notifier == null)
				{
					throw new ArgumentNullException("notifier");
				}
				this.notifiersStateLock.LockWriterElastic(5000);
				this.notifierDataAvaiableState.Add(notifier, new PendingRequestManager.PendingNotifierState());
				notifier.DataAvailable += this.OnNotifierDataAvailable;
			}
			finally
			{
				if (this.notifiersStateLock.IsWriterLockHeld)
				{
					if (lockCookie != null)
					{
						LockCookie value = lockCookie.Value;
						this.notifiersStateLock.DowngradeFromWriterLock(ref value);
					}
					else
					{
						this.notifiersStateLock.ReleaseWriterLock();
					}
				}
				if (lockCookie != null && !this.notifiersStateLock.IsReaderLockHeld)
				{
					ExAssert.RetailAssert(true, "Lost readerwriterlock that was acquired before entering the method");
				}
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0006AE8C File Offset: 0x0006908C
		internal void OnNotifierDataAvailable(object sender, EventArgs args)
		{
			bool flag = false;
			try
			{
				this.notifiersStateLock.LockReaderElastic(5000);
				flag = true;
				if (sender == null)
				{
					throw new ArgumentNullException("sender");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "OnNotifierDataAvailable called by a notifier.Notifier:{0}", sender.GetHashCode());
				IPendingRequestNotifier key = (IPendingRequestNotifier)sender;
				PendingRequestManager.PendingNotifierState pendingNotifierState = null;
				if (!this.notifierDataAvaiableState.TryGetValue(key, out pendingNotifierState))
				{
					throw new ArgumentException("The sender object is not registered in the manager class");
				}
				int num = Interlocked.CompareExchange(ref pendingNotifierState.State, 1, 0);
				if (num != 0)
				{
					throw new OwaInvalidOperationException("OnNotifierDataAvailable should not be called if the manager did not consume the notifier's data yet. Notifier:" + sender.ToString());
				}
				if (this.lockTracker.TryAcquireLock())
				{
					this.WriteNotification(false);
				}
			}
			catch (Exception e)
			{
				this.HandleException(e, false);
			}
			finally
			{
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0006AF74 File Offset: 0x00069174
		internal IAsyncResult BeginSendNotification(AsyncCallback callback, object extraData, bool isUserContextFullyInitialized, PendingRequestEventHandler pendingRequestHandler)
		{
			bool flag = this.lockTracker.SetPipeAvailable(false);
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Setting the pipe to AVAILABLE");
			try
			{
				this.pendingRequestEventHandler = pendingRequestHandler;
				this.asyncResult = new OwaAsyncResult(callback, extraData);
				try
				{
					this.response = (ChunkedHttpResponse)extraData;
					this.response.WriteIsRequestAlive(true);
					if (!isUserContextFullyInitialized)
					{
						this.response.WriteReInitializeOWA();
					}
					this.lastWriteTime = DateTime.UtcNow.Ticks;
					this.disposePendingRequest = false;
				}
				finally
				{
					flag = !this.lockTracker.TryReleaseLock();
				}
				if (flag)
				{
					this.WriteNotification(true);
				}
				this.startPendingRequestTime = DateTime.UtcNow.Ticks;
				this.lastDisconnectedTime = 0L;
				if (this.pendingRequestAliveTimer == null)
				{
					this.pendingRequestAliveTimer = new Timer(new TimerCallback(this.ElapsedConnectionAliveTimeout), null, 40000, 40000);
				}
			}
			catch (Exception e)
			{
				this.HandleException(e, true);
			}
			return this.asyncResult;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0006B08C File Offset: 0x0006928C
		internal void EndSendNotification(IAsyncResult async)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!", owaAsyncResult.Exception, this);
			}
			this.disposePendingRequest = false;
			this.response.WriteIsRequestAlive(false);
			if (owaAsyncResult.Exception != null)
			{
				throw new OwaNotificationPipeException("An exception happened while handling the pending connection asynchronously", owaAsyncResult.Exception);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0006B0ED File Offset: 0x000692ED
		internal void RecordFinishPendingRequest()
		{
			this.lockTracker.SetPipeUnavailable();
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Setting the pipe to UNAVAILABLE");
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0006B110 File Offset: 0x00069310
		internal bool HandleFinishRequestFromClient()
		{
			return this.HandleFinishRequestFromClient(false);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0006B11C File Offset: 0x0006931C
		internal bool HandleFinishRequestFromClient(bool requestRestart)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "The client requested the end of the current notification pipe.Should a restart request be sent ? {0}", requestRestart);
			this.disposePendingRequest = true;
			if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
			{
				try
				{
					this.CloseCurrentPendingRequest(false, requestRestart);
				}
				finally
				{
					this.lockTracker.TryReleaseLock();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0006B180 File Offset: 0x00069380
		private void HandleException(Exception e, bool finishSync)
		{
			try
			{
				this.asyncResult.Exception = e;
			}
			catch (OwaInvalidOperationException)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception not reported on pending get request. Exception:{0};", e);
				return;
			}
			if (this.lockTracker.IsLockOwner())
			{
				this.asyncResult.CompleteRequest(finishSync);
				return;
			}
			if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
			{
				try
				{
					this.asyncResult.CompleteRequest(finishSync);
					return;
				}
				finally
				{
					this.lockTracker.TryReleaseLock();
				}
			}
			this.disposePendingRequest = true;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0006B21C File Offset: 0x0006941C
		private void WriteNotification(bool asyncOperation)
		{
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!");
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				while (!flag)
				{
					if (this.disposePendingRequest)
					{
						this.CloseCurrentPendingRequest(asyncOperation, true);
						break;
					}
					this.notifiersStateLock.LockReaderElastic(5000);
					flag2 = true;
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
					foreach (KeyValuePair<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> keyValuePair in this.notifierDataAvaiableState)
					{
						bool flag3 = false;
						int num2 = Interlocked.CompareExchange(ref keyValuePair.Value.State, 0, 1);
						if (num2 == 1)
						{
							if (keyValuePair.Key.ShouldThrottle)
							{
								int num3 = Interlocked.Increment(ref keyValuePair.Value.OnDataAvailableThrottleCount);
								if (num3 > 100)
								{
									flag3 = true;
								}
								else if (num3 == 100 && num <= 10.0)
								{
									ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Start throttling mechanism - timer was started and from now on notifier {0} will be on throttling mode ", keyValuePair.Key.GetHashCode());
									flag3 = true;
									if (keyValuePair.Value.ThrottleTimer == null)
									{
										keyValuePair.Value.ThrottleTimer = new Timer(new TimerCallback(this.ThrottleTimeout), keyValuePair.Key, 20000, -1);
									}
									else
									{
										keyValuePair.Value.ThrottleTimer.Change(20000, -1);
									}
								}
								if (num3 <= 100 && num > 10.0 && num3 != 1)
								{
									Interlocked.Exchange(ref keyValuePair.Value.OnDataAvailableThrottleCount, 1);
								}
							}
							ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "PendingRequestManager.WriteNotification is reading data from the notifier. Notifier:{0}", keyValuePair.Key.GetHashCode());
							if (!flag3)
							{
								try
								{
									string text = keyValuePair.Key.ReadDataAndResetState();
									if (!string.IsNullOrEmpty(text))
									{
										this.response.Write(text);
										this.lastWriteTime = DateTime.UtcNow.Ticks;
									}
								}
								catch (Exception ex)
								{
									this.lockTracker.TryReleaseLock();
									ExTraceGlobals.NotificationsCallTracer.TraceError<string>((long)this.GetHashCode(), "Exception when writing the notifications to the client. Exception message:{0};", (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
									throw;
								}
							}
							flag = this.lockTracker.TryReleaseLock();
							if (flag)
							{
								break;
							}
						}
					}
					this.notifiersStateLock.ReleaseReaderLock();
					flag2 = false;
				}
			}
			finally
			{
				if (flag2)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0006B548 File Offset: 0x00069748
		private void ThrottleTimeout(object state)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Throttle Timeout method was called - throttle timeout elapsed");
			IPendingRequestNotifier pendingRequestNotifier = state as IPendingRequestNotifier;
			bool flag = false;
			try
			{
				if (pendingRequestNotifier == null)
				{
					throw new ArgumentException("State paramenter is invalid");
				}
				this.notifiersStateLock.LockReaderElastic(5000);
				flag = true;
				PendingRequestManager.PendingNotifierState pendingNotifierState = null;
				if (!this.notifierDataAvaiableState.TryGetValue(pendingRequestNotifier, out pendingNotifierState))
				{
					throw new ArgumentException("The sender object is not registered in the manager class");
				}
				Interlocked.Exchange(ref pendingNotifierState.OnDataAvailableThrottleCount, 0);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Notifier {0} is not on throttle period anymore", pendingRequestNotifier.GetHashCode());
				this.OnNotifierDataAvailable(pendingRequestNotifier, null);
			}
			catch (Exception e)
			{
				this.HandleException(e, false);
			}
			finally
			{
				if (flag)
				{
					this.notifiersStateLock.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0006B61C File Offset: 0x0006981C
		private void ElapsedConnectionAliveTimeout(object state)
		{
			bool requestRestart = false;
			OwaAsyncResult owaAsyncResult = this.asyncResult;
			EventHandler<EventArgs> keepAlive = this.KeepAlive;
			if (keepAlive != null)
			{
				keepAlive(this, new EventArgs());
			}
			if (DateTime.UtcNow.Ticks - this.startPendingRequestTime > PendingRequestManager.MaxPendingRequestOpenTimeIn100NanoSec)
			{
				this.disposePendingRequest = true;
				requestRestart = true;
			}
			try
			{
				if (this.lockTracker.TryAcquireLockOnlyIfSucceed())
				{
					try
					{
						if (this.lastWriteTime == 0L)
						{
							this.lastWriteTime = DateTime.UtcNow.Ticks;
						}
						bool flag = false;
						try
						{
							this.notifiersStateLock.LockReaderElastic(5000);
							flag = true;
							IPendingRequestNotifier[] array = this.notifierDataAvaiableState.Keys.ToArray<IPendingRequestNotifier>();
							this.notifiersStateLock.ReleaseReaderLock();
							flag = false;
							if (array != null)
							{
								for (int i = 0; i < array.Length; i++)
								{
									array[i].ConnectionAliveTimer();
								}
							}
						}
						finally
						{
							if (flag)
							{
								this.notifiersStateLock.ReleaseReaderLock();
							}
						}
						if (DateTime.UtcNow.Ticks - this.lastWriteTime >= 100000000L)
						{
							if (this.disposePendingRequest)
							{
								this.CloseCurrentPendingRequest(false, requestRestart);
								this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted);
								return;
							}
							this.response.WriteEmptyNotification();
							this.lastWriteTime = DateTime.UtcNow.Ticks;
						}
					}
					catch (Exception e)
					{
						this.HandleException(e, false);
						this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted);
						return;
					}
					if (!this.lockTracker.TryReleaseLock(owaAsyncResult.IsCompleted))
					{
						try
						{
							this.WriteNotification(false);
						}
						catch (Exception e2)
						{
							this.HandleException(e2, false);
						}
					}
				}
			}
			finally
			{
				if (!this.ChunkedHttpResponse.IsClientConnected)
				{
					if (this.lastDisconnectedTime == 0L)
					{
						this.lastDisconnectedTime = DateTime.UtcNow.Ticks;
						goto IL_23D;
					}
					if (DateTime.UtcNow.Ticks - this.lastDisconnectedTime <= 700000000L || this.userContext.IsUserRequestLockHeld)
					{
						goto IL_23D;
					}
					this.lastDisconnectedTime = 0L;
					try
					{
						EventArgs e3 = new EventArgs();
						EventHandler<EventArgs> clientDisconnected = this.ClientDisconnected;
						if (clientDisconnected != null)
						{
							clientDisconnected(this, e3);
						}
						goto IL_23D;
					}
					catch (Exception ex)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Exception during ClientDisconnected event: {0}", ex.ToString());
						goto IL_23D;
					}
				}
				this.lastDisconnectedTime = 0L;
				IL_23D:;
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0006B8E4 File Offset: 0x00069AE4
		private void CloseCurrentPendingRequest(bool completedSynchronously, bool requestRestart)
		{
			if (!this.lockTracker.IsLockOwner())
			{
				throw new OwaInvalidOperationException("A thread that is not the owner of the lock can't call WriteNotification!");
			}
			if (requestRestart)
			{
				try
				{
					this.response.RestartRequest();
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Send code to the client that will restart the notification pipe");
				}
				catch (OwaNotificationPipeWriteException)
				{
				}
			}
			this.asyncResult.CompleteRequest(completedSynchronously);
		}

		// Token: 0x04000BEA RID: 3050
		private const int LockTimeout = 5000;

		// Token: 0x04000BEB RID: 3051
		private const int EmptyNotificationIntervalInMilliSeconds = 40000;

		// Token: 0x04000BEC RID: 3052
		private const int MinEmptyNotificationIntervalInMilliSeconds = 10000;

		// Token: 0x04000BED RID: 3053
		private const long TicksPerSecond = 10000000L;

		// Token: 0x04000BEE RID: 3054
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x04000BEF RID: 3055
		private const int ThrottleIntervalInSec = 10;

		// Token: 0x04000BF0 RID: 3056
		private const int MinimumWaitTimeForClientActivityInSeconds = 70;

		// Token: 0x04000BF1 RID: 3057
		private const int ThrottleTimerCallbackInMilliSeconds = 20000;

		// Token: 0x04000BF2 RID: 3058
		private const int ThrottlingThreshold = 100;

		// Token: 0x04000BF3 RID: 3059
		private static readonly long MaxPendingRequestOpenTimeIn100NanoSec = (long)Globals.MaxPendingRequestLifeInSeconds * 10000000L;

		// Token: 0x04000BF4 RID: 3060
		private readonly UserContext userContext;

		// Token: 0x04000BF5 RID: 3061
		private TimeSpan? startThrottleTime;

		// Token: 0x04000BF6 RID: 3062
		private Dictionary<IPendingRequestNotifier, PendingRequestManager.PendingNotifierState> notifierDataAvaiableState;

		// Token: 0x04000BF7 RID: 3063
		private OwaRWLockWrapper notifiersStateLock;

		// Token: 0x04000BF8 RID: 3064
		private volatile bool disposePendingRequest;

		// Token: 0x04000BF9 RID: 3065
		private OwaAsyncResult asyncResult;

		// Token: 0x04000BFA RID: 3066
		private ChunkedHttpResponse response;

		// Token: 0x04000BFB RID: 3067
		private long lastWriteTime;

		// Token: 0x04000BFC RID: 3068
		private long lastDisconnectedTime;

		// Token: 0x04000BFD RID: 3069
		private long startPendingRequestTime;

		// Token: 0x04000BFE RID: 3070
		private Timer pendingRequestAliveTimer;

		// Token: 0x04000BFF RID: 3071
		private PendingNotifierLockTracker lockTracker = new PendingNotifierLockTracker();

		// Token: 0x04000C00 RID: 3072
		private bool disposed;

		// Token: 0x04000C01 RID: 3073
		private PendingRequestEventHandler pendingRequestEventHandler;

		// Token: 0x0200020D RID: 525
		private class PendingNotifierState : DisposeTrackableBase
		{
			// Token: 0x060011C2 RID: 4546 RVA: 0x0006B964 File Offset: 0x00069B64
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

			// Token: 0x060011C3 RID: 4547 RVA: 0x0006B992 File Offset: 0x00069B92
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<PendingRequestManager.PendingNotifierState>(this);
			}

			// Token: 0x04000C04 RID: 3076
			internal int State;

			// Token: 0x04000C05 RID: 3077
			internal int OnDataAvailableThrottleCount;

			// Token: 0x04000C06 RID: 3078
			internal Timer ThrottleTimer;

			// Token: 0x04000C07 RID: 3079
			private bool disposed;
		}
	}
}
