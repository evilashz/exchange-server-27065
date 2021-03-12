using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000EF RID: 239
	internal class NotificationManager : IEventHandler, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x00047F40 File Offset: 0x00046140
		private NotificationManager(IAsyncCommand command, string uniqueId, int policyHashCode, uint policyKey)
		{
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Created);
			this.createdUTC = DateTime.UtcNow;
			this.command = command;
			this.uniqueId = uniqueId;
			this.policyHashCode = policyHashCode;
			this.policyKey = policyKey;
			this.disposeTracker = this.GetDisposeTracker();
			this.budgetKey = this.command.Context.BudgetKey;
			this.emailAddress = this.command.Context.SmtpAddress;
			this.deviceIdentity = this.command.Context.DeviceIdentity;
			this.accountValidationContext = this.command.Context.AccountValidationContext;
			Guid mdbGuid = this.command.Context.MdbGuid;
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "NotificationManager created for {0}", this.uniqueId);
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0004804D File Offset: 0x0004624D
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x00048055 File Offset: 0x00046255
		public object Information { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0004805E File Offset: 0x0004625E
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x00048066 File Offset: 0x00046266
		public uint RequestedWaitTime { get; private set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0004806F File Offset: 0x0004626F
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x00048077 File Offset: 0x00046277
		public bool MailboxLoggingEnabled { get; set; }

		// Token: 0x06000D39 RID: 3385 RVA: 0x00048080 File Offset: 0x00046280
		public static NotificationManagerResult GetDiagnosticInfo(CallType callType, string argument)
		{
			NotificationManagerResult notificationManagerResult = new NotificationManagerResult();
			notificationManagerResult.CacheCount = NotificationManager.notificationManagerCache.Count;
			notificationManagerResult.RemovedCount = NotificationManager.removedInstances.Count;
			notificationManagerResult.CreatesPerMinute = (int)NotificationManager.createsPerMinute.GetValue();
			notificationManagerResult.HitsPerMinute = (int)NotificationManager.hitsPerMinute.GetValue();
			notificationManagerResult.ContentionsPerMinute = (int)NotificationManager.cacheContentionsPerMinute.GetValue();
			notificationManagerResult.StealsPerMinute = (int)NotificationManager.stealsPerMinute.GetValue();
			bool flag = callType != CallType.Metadata;
			if (flag)
			{
				notificationManagerResult.RemovedInstances = new List<NotificationManagerResultItem>();
				foreach (KeyValuePair<string, NotificationManager> keyValuePair in NotificationManager.removedInstances)
				{
					if (keyValuePair.Value.Matches(callType, argument))
					{
						notificationManagerResult.RemovedInstances.Add(keyValuePair.Value.GetInstanceDiagnosticInformation());
					}
				}
			}
			lock (NotificationManager.notificationManagerCache)
			{
				List<NotificationManagerResultItem> list = flag ? new List<NotificationManagerResultItem>() : null;
				List<NotificationManagerResultItem> list2 = flag ? new List<NotificationManagerResultItem>() : null;
				int num = 0;
				int num2 = 0;
				foreach (KeyValuePair<string, NotificationManager> keyValuePair2 in NotificationManager.notificationManagerCache)
				{
					if (keyValuePair2.Value.command != null)
					{
						num++;
						if (flag && keyValuePair2.Value.Matches(callType, argument))
						{
							list.Add(keyValuePair2.Value.GetInstanceDiagnosticInformation());
						}
					}
					else
					{
						num2++;
						if (flag && keyValuePair2.Value.Matches(callType, argument))
						{
							list2.Add(keyValuePair2.Value.GetInstanceDiagnosticInformation());
						}
					}
				}
				notificationManagerResult.ActiveInstances = list;
				notificationManagerResult.ActiveCount = num;
				notificationManagerResult.InactiveInstances = list2;
				notificationManagerResult.InactiveCount = num2;
			}
			return notificationManagerResult;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0004827C File Offset: 0x0004647C
		public static void ClearCache()
		{
			lock (NotificationManager.notificationManagerCache)
			{
				NotificationManager.notificationManagerCache.Clear();
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000482C0 File Offset: 0x000464C0
		public static NotificationManager CreateNotificationManager(INotificationManagerContext context, IAsyncCommand command)
		{
			string text = NotificationManager.GetUniqueId(context);
			uint num = context.PolicyKey;
			int mailboxPolicyHash = context.MailboxPolicyHash;
			NotificationManager notificationManager = new NotificationManager(command, text, mailboxPolicyHash, num);
			NotificationManager notificationManager2;
			lock (NotificationManager.notificationManagerCache)
			{
				if (NotificationManager.notificationManagerCache.TryGetValue(text, out notificationManager2))
				{
					NotificationManager.cacheContentionsPerMinute.Add(1U);
					notificationManager2.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Removed);
					NotificationManager.notificationManagerCache.Remove(text);
					NotificationManager.removedInstances[text] = notificationManager2;
				}
				NotificationManager.createsPerMinute.Add(1U);
				notificationManager.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Cached);
				NotificationManager.notificationManagerCache.Add(text, notificationManager);
				AirSyncCounters.NumberOfNotificationManagerInCache.RawValue = (long)NotificationManager.notificationManagerCache.Count;
			}
			if (notificationManager2 != null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, notificationManager, "Disposing existing NotificationManager for {0}", text);
				notificationManager2.EnqueueEvent(new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Acquire, null));
				notificationManager2.EnqueueDispose();
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, notificationManager, "Created notification manager for {0}", text);
			return notificationManager;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000483C8 File Offset: 0x000465C8
		public static NotificationManager GetOrCreateNotificationManager(INotificationManagerContext context, IAsyncCommand command, out bool wasTakenOver)
		{
			string text = NotificationManager.GetUniqueId(context);
			uint num = context.PolicyKey;
			int mailboxPolicyHash = context.MailboxPolicyHash;
			wasTakenOver = false;
			NotificationManager notificationManager = null;
			NotificationManager notificationManager2 = null;
			lock (NotificationManager.notificationManagerCache)
			{
				bool flag2 = NotificationManager.notificationManagerCache.TryGetValue(text, out notificationManager2);
				if (flag2)
				{
					NotificationManager.hitsPerMinute.Add(1U);
				}
				if (flag2 && notificationManager2.subscriptionCanBeTaken && !notificationManager2.MailboxLoggingEnabled && mailboxPolicyHash == notificationManager2.policyHashCode && num == notificationManager2.policyKey)
				{
					wasTakenOver = true;
				}
				else
				{
					if (flag2)
					{
						NotificationManager.cacheContentionsPerMinute.Add(1U);
						notificationManager2.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Removed);
						NotificationManager.notificationManagerCache.Remove(text);
						NotificationManager.removedInstances[text] = notificationManager2;
						notificationManager = notificationManager2;
					}
					notificationManager2 = new NotificationManager(command, text, mailboxPolicyHash, num);
					NotificationManager.createsPerMinute.Add(1U);
					notificationManager2.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Cached);
					NotificationManager.notificationManagerCache.Add(text, notificationManager2);
					AirSyncCounters.NumberOfNotificationManagerInCache.RawValue = (long)NotificationManager.notificationManagerCache.Count;
				}
			}
			if (notificationManager != null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, notificationManager2, "Disposing existing NotificationManager for {0}", text);
				notificationManager.EnqueueEvent(new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Acquire, null));
				notificationManager.EnqueueDispose();
				notificationManager = null;
			}
			if (wasTakenOver)
			{
				NotificationManager.AsyncEvent evt = new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Acquire, command);
				if (!notificationManager2.EnqueueEvent(evt))
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, notificationManager2, "A NotificationManager was attempted to be taken over but failed for {0}", text);
					wasTakenOver = false;
					notificationManager2 = NotificationManager.CreateNotificationManager(context, command);
				}
				else
				{
					notificationManager2.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Stolen);
					NotificationManager.stealsPerMinute.Add(1U);
				}
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, notificationManager2, "Got or created notification manager for {0}", text);
			return notificationManager2;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00048570 File Offset: 0x00046770
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationManager>(this);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00048578 File Offset: 0x00046778
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0004858D File Offset: 0x0004678D
		public void Dispose()
		{
			throw new InvalidOperationException("DO NOT CALL NotificationManager.Dispose()! Use EnqueueDispose() instead!");
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00048599 File Offset: 0x00046799
		public void EnqueueDispose()
		{
			if (this.disposed)
			{
				return;
			}
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Enqueue Dispose: Enqueue a kill event.");
			this.EnqueueEvent(new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Kill));
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000485C1 File Offset: 0x000467C1
		internal Queue<NotificationManager.AsyncEvent> QueuedEventsForTest
		{
			get
			{
				return this.eventQueue;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000485CC File Offset: 0x000467CC
		public void Consume(Event evt)
		{
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Consume Event called {0}.", this.uniqueId);
			AccountState accountState = AccountState.AccountEnabled;
			if (this.accountValidationContext != null)
			{
				accountState = this.accountValidationContext.CheckAccount();
			}
			if (accountState != AccountState.AccountEnabled)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Account Terminated {0}.", this.uniqueId);
				this.EnqueueEvent(new NotificationManager.AsyncEvent(accountState));
				return;
			}
			this.EnqueueEvent(new NotificationManager.AsyncEvent(evt));
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00048639 File Offset: 0x00046839
		public void HandleException(Exception ex)
		{
			AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "HandleException called. uniqueID: {0}, Exception:{1}.", this.uniqueId, ex.ToString());
			this.EnqueueEvent(new NotificationManager.AsyncEvent(ex));
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00048664 File Offset: 0x00046864
		public void ProcessQueuedEvents(IAsyncCommand callingCommand)
		{
			NotificationManager.AsyncEvent asyncEvent = null;
			if (this.currentlyExecuting)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "ProcessQueuedEvents.UniqueId:{0}.  Already processing event.  Exiting.", this.uniqueId);
				return;
			}
			lock (this.eventQueue)
			{
				if (this.currentlyExecuting)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "ProcessQueuedEvents.UniqueId:{0}.  Already processing event.  Exiting.", this.uniqueId);
					return;
				}
				this.currentlyExecuting = true;
			}
			IBudget budget = null;
			try
			{
				this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.RunStart);
				budget = StandardBudget.Acquire(this.budgetKey);
				lock (this.eventQueue)
				{
					if (callingCommand != null)
					{
						callingCommand.ProcessingEventsEnabled = true;
					}
					AirSyncDiagnostics.TraceDebug<string, string, int>(ExTraceGlobals.RequestsTracer, this, "ProcessQueuedEvents.UniqueId:{0}, ProcessingEnabled: {1}, eventQueue count:{2}.", this.uniqueId, (callingCommand == null) ? "<null command>" : "true", this.eventQueue.Count);
					asyncEvent = this.GetNextEvent();
				}
				IAsyncCommand cmd = null;
				bool flag3 = asyncEvent != null;
				while (flag3)
				{
					try
					{
						cmd = this.StartCommandContext();
						AirSyncDiagnostics.TraceDebug<NotificationManager.AsyncEventType, string, bool>(ExTraceGlobals.ThreadingTracer, this, "Processing event {0} for {1}. Command is null? {2}", asyncEvent.Type, this.uniqueId, this.command == null);
						switch (asyncEvent.Type)
						{
						case NotificationManager.AsyncEventType.XsoEventAvailable:
							this.ProcessXsoEventAvailable(asyncEvent, budget);
							break;
						case NotificationManager.AsyncEventType.XsoException:
							this.ProcessXsoException(asyncEvent);
							break;
						case NotificationManager.AsyncEventType.Timeout:
							this.ProcessTimeout(budget);
							break;
						case NotificationManager.AsyncEventType.Acquire:
							this.ProcessAcquire(asyncEvent);
							break;
						case NotificationManager.AsyncEventType.Release:
							this.ProcessRelease(asyncEvent);
							break;
						case NotificationManager.AsyncEventType.Kill:
							this.Kill();
							break;
						case NotificationManager.AsyncEventType.AccountTerminated:
							this.ProcessAccountTerminated(asyncEvent);
							break;
						}
					}
					catch (Exception ex)
					{
						AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.ThreadingTracer, this, "Exception in processQueuedEvents for {0}. Exception:{1}", this.uniqueId, ex.ToString());
						if (!this.InternalHandleException(ex))
						{
							throw;
						}
					}
					finally
					{
						this.EndCommandContext(cmd);
						asyncEvent = this.GetNextEvent();
						flag3 = (asyncEvent != null);
					}
				}
			}
			finally
			{
				this.currentlyExecuting = false;
				this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.RunEnd);
				if (budget != null)
				{
					try
					{
						budget.Dispose();
					}
					catch (FailFastException arg)
					{
						AirSyncDiagnostics.TraceError<FailFastException>(ExTraceGlobals.RequestsTracer, this, "Budget.Dispose failed with exception: {0}", arg);
					}
				}
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00048910 File Offset: 0x00046B10
		public void ReleaseCommand(IAsyncCommand command)
		{
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Release Command for {0}", this.uniqueId);
			NotificationManager.AsyncEvent evt = new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Release, command);
			this.EnqueueEvent(evt);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00048943 File Offset: 0x00046B43
		public void StartTimer(uint timeout, ExDateTime requestTime, ExDateTime policyExpirationTime)
		{
			if (this.timer != null)
			{
				throw new InvalidOperationException();
			}
			if (timeout == 0U)
			{
				throw new ArgumentException("Timeout cannot be 0!");
			}
			this.RequestedWaitTime = timeout;
			this.policyExpirationTime = policyExpirationTime;
			this.InternalStartTimer(requestTime, timeout);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00048977 File Offset: 0x00046B77
		public void Add(EventSubscription subscription)
		{
			this.eventSubscriptions.Add(subscription);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00048985 File Offset: 0x00046B85
		public void SubscriptionsCannotBeTaken()
		{
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Locked);
			this.subscriptionCanBeTaken = false;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00048998 File Offset: 0x00046B98
		public NotificationManagerResultItem GetInstanceDiagnosticInformation()
		{
			NotificationManagerResultItem notificationManagerResultItem = new NotificationManagerResultItem();
			notificationManagerResultItem.UniqueId = this.uniqueId;
			IAsyncCommand asyncCommand = this.command;
			notificationManagerResultItem.Command = ((asyncCommand == null) ? string.Empty : asyncCommand.ToString());
			notificationManagerResultItem.EmailAddress = this.emailAddress;
			notificationManagerResultItem.DeviceId = this.deviceIdentity.DeviceId;
			notificationManagerResultItem.TotalAcquires = this.totalAcquires;
			notificationManagerResultItem.TotalKills = this.totalKills;
			notificationManagerResultItem.TotalReleases = this.totalReleases;
			notificationManagerResultItem.TotalTimeouts = this.totalTimeouts;
			notificationManagerResultItem.TotalXsoEvents = this.totalXsoEvents;
			notificationManagerResultItem.TotalXsoExceptions = this.totalXsoExceptions;
			notificationManagerResultItem.IsExecuting = this.currentlyExecuting;
			notificationManagerResultItem.QueueCount = this.eventQueue.Count;
			notificationManagerResultItem.PolicyKey = (long)((ulong)this.policyKey);
			notificationManagerResultItem.LiveTime = (DateTime.UtcNow - this.createdUTC).ToString();
			notificationManagerResultItem.RequestedWaitTime = TimeSpan.FromSeconds(this.RequestedWaitTime).ToString();
			notificationManagerResultItem.Actions = new List<InstanceAction>(this.instanceEvents.Count);
			foreach (InstanceAction item in this.instanceEvents)
			{
				notificationManagerResultItem.Actions.Add(item);
			}
			lock (this.eventQueue)
			{
				notificationManagerResultItem.QueuedEvents = new List<string>(this.eventQueue.Count);
				foreach (NotificationManager.AsyncEvent asyncEvent in this.eventQueue)
				{
					notificationManagerResultItem.QueuedEvents.Add(asyncEvent.Type.ToString());
				}
			}
			return notificationManagerResultItem;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00048BA8 File Offset: 0x00046DA8
		private static string GetUniqueId(INotificationManagerContext context)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", new object[]
			{
				context.MailboxGuid.ToString(),
				context.DeviceIdentity,
				context.CommandType.ToString()
			});
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00048C00 File Offset: 0x00046E00
		private static void TimerExpired(object state)
		{
			NotificationManager notificationManager = (NotificationManager)state;
			AccountState accountState = AccountState.AccountEnabled;
			if (notificationManager.accountValidationContext != null)
			{
				accountState = notificationManager.accountValidationContext.CheckAccount();
			}
			if (accountState != AccountState.AccountEnabled)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, notificationManager, "Account Terminated {0}.", notificationManager.uniqueId);
				notificationManager.EnqueueEvent(new NotificationManager.AsyncEvent(accountState));
				return;
			}
			notificationManager.EnqueueEvent(new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Timeout));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00048C60 File Offset: 0x00046E60
		private void ProcessXsoEventAvailable(NotificationManager.AsyncEvent evt, IBudget budget)
		{
			Interlocked.Increment(ref this.totalXsoEvents);
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.XsoEvent, new EventType?(evt.Event.EventType), new EventObjectType?(evt.Event.ObjectType), null);
			if (this.command == null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "XSO event available for {0}, but command is not available. Calling Kill", this.uniqueId);
				this.Kill();
				return;
			}
			budget.CheckOverBudget();
			this.command.Consume(evt.Event);
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "XSO event available for {0}. calling Command.Consume", this.uniqueId);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00048CF4 File Offset: 0x00046EF4
		private void ProcessXsoException(NotificationManager.AsyncEvent evt)
		{
			Interlocked.Increment(ref this.totalXsoExceptions);
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.XsoException, null, null, evt.Exception);
			if (this.command == null)
			{
				if (!this.InternalHandleException(evt.Exception))
				{
					throw evt.Exception;
				}
			}
			else
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "XSO exception event available for {0}. calling Command.HandleException", this.uniqueId);
				this.command.HandleException(evt.Exception);
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00048D70 File Offset: 0x00046F70
		private void ProcessTimeout(IBudget budget)
		{
			Interlocked.Increment(ref this.totalTimeouts);
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.HBTimeout);
			if (ExDateTime.UtcNow.CompareTo(this.lastTargetTime) >= 0)
			{
				if (this.command != null)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Timed out for {0}. calling command.HeartbeatCallback", this.uniqueId);
					budget.CheckOverBudget();
					this.command.HeartbeatCallback();
					return;
				}
				this.Kill();
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Timed out and killing {0}", this.uniqueId);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00048DF4 File Offset: 0x00046FF4
		private void ProcessAcquire(NotificationManager.AsyncEvent evt)
		{
			Interlocked.Increment(ref this.totalAcquires);
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Acquired);
			if (this.command != null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Release notificationManager for {0}", this.uniqueId);
				this.command.ReleaseNotificationManager(true);
			}
			this.command = evt.Command;
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Acquiring notification manager for {0}", this.uniqueId);
			if (this.RequestedWaitTime > 0U && this.command != null)
			{
				this.InternalStartTimer(this.command.Context.RequestTime, this.RequestedWaitTime);
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00048E90 File Offset: 0x00047090
		private void ProcessRelease(NotificationManager.AsyncEvent evt)
		{
			Interlocked.Increment(ref this.totalReleases);
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Released);
			evt.Command.ReleaseNotificationManager(false);
			if (this.command == evt.Command)
			{
				this.command = null;
				if (this.RequestedWaitTime > 0U && !this.disposed)
				{
					this.InternalStartTimer(ExDateTime.UtcNow, 120U);
					return;
				}
				this.Kill();
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Release & killing {0}", this.uniqueId);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00048F10 File Offset: 0x00047110
		private void ProcessAccountTerminated(NotificationManager.AsyncEvent evt)
		{
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.AccountTerminated);
			if (this.command == null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Account terminated after XSO event callback for {0}, but command is not available. Calling Kill", this.uniqueId);
				this.Kill();
				return;
			}
			this.command.HandleAccountTerminated(evt);
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Account terminated after XSO event callback for {0}. calling Command.HandleAccountTerminated", this.uniqueId);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00048F6C File Offset: 0x0004716C
		private IAsyncCommand StartCommandContext()
		{
			IAsyncCommand asyncCommand = this.command;
			if (asyncCommand != null)
			{
				asyncCommand.SetContextDataInTls();
				if (asyncCommand.PerUserTracingEnabled)
				{
					AirSyncDiagnostics.SetThreadTracing();
				}
			}
			return asyncCommand;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00048F97 File Offset: 0x00047197
		private void EndCommandContext(IAsyncCommand cmd)
		{
			if (cmd != null)
			{
				Command.ClearContextDataInTls();
				if (cmd.PerUserTracingEnabled)
				{
					AirSyncDiagnostics.ClearThreadTracing();
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00048FB0 File Offset: 0x000471B0
		private NotificationManager.AsyncEvent GetNextEvent()
		{
			IAsyncCommand asyncCommand = this.command;
			NotificationManager.AsyncEvent result;
			lock (this.eventQueue)
			{
				if (this.eventQueue.Count > 0)
				{
					NotificationManager.AsyncEvent asyncEvent = this.eventQueue.Peek();
					if ((asyncCommand != null && !asyncCommand.ProcessingEventsEnabled) || (asyncEvent.Command != null && !asyncEvent.Command.ProcessingEventsEnabled))
					{
						result = null;
					}
					else
					{
						result = this.eventQueue.Dequeue();
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00049048 File Offset: 0x00047248
		private void Kill()
		{
			Interlocked.Increment(ref this.totalKills);
			if (this.disposed)
			{
				return;
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Processing Kill event for {0}", this.uniqueId);
			lock (NotificationManager.notificationManagerCache)
			{
				NotificationManager notificationManager;
				if (NotificationManager.notificationManagerCache.TryGetValue(this.uniqueId, out notificationManager) && notificationManager == this)
				{
					notificationManager.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Removed);
					NotificationManager.notificationManagerCache.Remove(this.uniqueId);
					AirSyncCounters.NumberOfNotificationManagerInCache.RawValue = (long)NotificationManager.notificationManagerCache.Count;
				}
			}
			NotificationManager notificationManager2 = null;
			if (NotificationManager.removedInstances.TryGetValue(this.uniqueId, out notificationManager2) && notificationManager2 == this)
			{
				NotificationManager.removedInstances.TryRemove(this.uniqueId, out notificationManager2);
			}
			this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Killed);
			lock (this.eventQueue)
			{
				GC.SuppressFinalize(this);
				this.subscriptionCanBeTaken = false;
				this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Locked);
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.eventSubscriptions.Count > 0)
				{
					foreach (EventSubscription eventSubscription in this.eventSubscriptions)
					{
						eventSubscription.Dispose();
					}
					this.eventSubscriptions.Clear();
				}
				List<NotificationManager.AsyncEvent> list = new List<NotificationManager.AsyncEvent>(this.eventSubscriptions.Count + 1);
				if (this.command != null)
				{
					NotificationManager.AsyncEvent item = new NotificationManager.AsyncEvent(NotificationManager.AsyncEventType.Release, this.command);
					list.Add(item);
				}
				foreach (NotificationManager.AsyncEvent asyncEvent in this.eventQueue)
				{
					if (asyncEvent.Type == NotificationManager.AsyncEventType.Acquire)
					{
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ThreadingTracer, this, "Changing Acquire into a Release for {0}", this.uniqueId);
						asyncEvent.Type = NotificationManager.AsyncEventType.Release;
						list.Add(asyncEvent);
					}
					else if (asyncEvent.Type == NotificationManager.AsyncEventType.Release)
					{
						list.Add(asyncEvent);
					}
					else
					{
						AirSyncDiagnostics.TraceDebug<NotificationManager.AsyncEventType, string>(ExTraceGlobals.ThreadingTracer, this, "Ignoring event {0} after a Kill for {1}", asyncEvent.Type, this.uniqueId);
					}
				}
				this.eventQueue.Clear();
				foreach (NotificationManager.AsyncEvent item2 in list)
				{
					this.eventQueue.Enqueue(item2);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00049360 File Offset: 0x00047560
		private void InternalStartTimer(ExDateTime startTime, uint timeout)
		{
			AirSyncDiagnostics.TraceDebug<uint, string>(ExTraceGlobals.ThreadingTracer, this, "Starting timer with {0} seconds for {1}.", timeout, this.uniqueId);
			uint num = (timeout > (uint)GlobalSettings.EarlyWakeupBufferTime) ? (timeout - (uint)GlobalSettings.EarlyWakeupBufferTime) : timeout;
			ExDateTime exDateTime = startTime.AddSeconds(num);
			ExDateTime utcNow = ExDateTime.UtcNow;
			int num2 = (int)exDateTime.Subtract(utcNow).TotalMilliseconds + 1;
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.lastTargetTime = exDateTime.AddSeconds(-2.0);
			if (this.timer == null)
			{
				this.timer = new Timer(NotificationManager.timerCallback, this, num2, -1);
				return;
			}
			this.timer.Change(num2, -1);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00049403 File Offset: 0x00047603
		private bool InternalHandleException(Exception ex)
		{
			if (this.command != null)
			{
				this.command.HandleException(ex);
				return true;
			}
			if (AirSyncUtility.HandleNonCriticalException(ex, true))
			{
				this.Kill();
				return true;
			}
			return false;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00049430 File Offset: 0x00047630
		private bool EnqueueEvent(NotificationManager.AsyncEvent evt)
		{
			bool result;
			lock (this.eventQueue)
			{
				if (this.disposed && evt.Type != NotificationManager.AsyncEventType.Release)
				{
					result = false;
				}
				else if (evt.Type == NotificationManager.AsyncEventType.Acquire && (!this.subscriptionCanBeTaken || ExDateTime.UtcNow > this.policyExpirationTime))
				{
					result = false;
				}
				else
				{
					if (evt.Type != NotificationManager.AsyncEventType.Timeout && evt.Type != NotificationManager.AsyncEventType.Acquire && evt.Type != NotificationManager.AsyncEventType.Release)
					{
						this.subscriptionCanBeTaken = false;
						this.EnqueueDiagOperation(NotificationManager.DiagnosticEvent.Locked);
					}
					this.eventQueue.Enqueue(evt);
					AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.ThreadingTracer, this, "Enqueue event for {0}. processingEnabled:{1}", this.uniqueId, (this.command == null) ? "<null command>" : this.command.ProcessingEventsEnabled.ToString());
					if (this.command == null || this.command.ProcessingEventsEnabled)
					{
						this.ProcessQueuedEvents(null);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0004953C File Offset: 0x0004773C
		private void EnqueueDiagOperation(NotificationManager.DiagnosticEvent evt)
		{
			this.EnqueueDiagOperation(evt, null, null, null);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00049564 File Offset: 0x00047764
		private void EnqueueDiagOperation(NotificationManager.DiagnosticEvent evt, EventType? xsoEventType, EventObjectType? xsoEventObjectType, Exception xsoException)
		{
			if (this.instanceEvents.Count < 100)
			{
				this.instanceEvents.Enqueue(new InstanceAction
				{
					Action = evt.ToString(),
					Time = DateTime.UtcNow,
					ThreadId = Thread.CurrentThread.ManagedThreadId,
					XsoEventType = ((xsoEventType != null) ? xsoEventType.Value.ToString() : null),
					XsoObjectType = ((xsoEventObjectType != null) ? xsoEventObjectType.Value.ToString() : null),
					XsoException = ((xsoException == null) ? null : xsoException.ToString())
				});
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00049624 File Offset: 0x00047824
		private bool Matches(CallType callType, string argument)
		{
			switch (callType)
			{
			case CallType.EmailAddress:
				return string.Compare(this.emailAddress, argument, true) == 0;
			case CallType.DeviceId:
				return this.deviceIdentity.IsDeviceId(argument);
			default:
				return true;
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00049664 File Offset: 0x00047864
		private void RunWithThreadContext(Action action)
		{
			bool flag = false;
			IAsyncCommand asyncCommand = this.command;
			if (asyncCommand != null)
			{
				asyncCommand.SetContextDataInTls();
				if (asyncCommand.PerUserTracingEnabled)
				{
					AirSyncDiagnostics.SetThreadTracing();
				}
				flag = true;
			}
			try
			{
				action();
			}
			finally
			{
				if (flag)
				{
					Command.ClearContextDataInTls();
					if (asyncCommand.PerUserTracingEnabled)
					{
						AirSyncDiagnostics.ClearThreadTracing();
					}
				}
			}
		}

		// Token: 0x0400082F RID: 2095
		private const int InstanceEventMaxCount = 100;

		// Token: 0x04000830 RID: 2096
		private static readonly TimerCallback timerCallback = new TimerCallback(NotificationManager.TimerExpired);

		// Token: 0x04000831 RID: 2097
		private static Dictionary<string, NotificationManager> notificationManagerCache = new Dictionary<string, NotificationManager>(500);

		// Token: 0x04000832 RID: 2098
		private static ConcurrentDictionary<string, NotificationManager> removedInstances = new ConcurrentDictionary<string, NotificationManager>();

		// Token: 0x04000833 RID: 2099
		private static FixedTimeSum createsPerMinute = new FixedTimeSum(6000, 10);

		// Token: 0x04000834 RID: 2100
		private static FixedTimeSum hitsPerMinute = new FixedTimeSum(6000, 10);

		// Token: 0x04000835 RID: 2101
		private static FixedTimeSum cacheContentionsPerMinute = new FixedTimeSum(6000, 10);

		// Token: 0x04000836 RID: 2102
		private static FixedTimeSum stealsPerMinute = new FixedTimeSum(6000, 10);

		// Token: 0x04000837 RID: 2103
		private int totalXsoEvents;

		// Token: 0x04000838 RID: 2104
		private int totalXsoExceptions;

		// Token: 0x04000839 RID: 2105
		private int totalTimeouts;

		// Token: 0x0400083A RID: 2106
		private int totalKills;

		// Token: 0x0400083B RID: 2107
		private int totalAcquires;

		// Token: 0x0400083C RID: 2108
		private int totalReleases;

		// Token: 0x0400083D RID: 2109
		private ConcurrentQueue<InstanceAction> instanceEvents = new ConcurrentQueue<InstanceAction>();

		// Token: 0x0400083E RID: 2110
		private readonly DateTime createdUTC;

		// Token: 0x0400083F RID: 2111
		private readonly string emailAddress;

		// Token: 0x04000840 RID: 2112
		private readonly DeviceIdentity deviceIdentity;

		// Token: 0x04000841 RID: 2113
		private Timer timer;

		// Token: 0x04000842 RID: 2114
		private string uniqueId;

		// Token: 0x04000843 RID: 2115
		private IAsyncCommand command;

		// Token: 0x04000844 RID: 2116
		private bool subscriptionCanBeTaken = true;

		// Token: 0x04000845 RID: 2117
		private List<EventSubscription> eventSubscriptions = new List<EventSubscription>(5);

		// Token: 0x04000846 RID: 2118
		private Queue<NotificationManager.AsyncEvent> eventQueue = new Queue<NotificationManager.AsyncEvent>(4);

		// Token: 0x04000847 RID: 2119
		private bool disposed;

		// Token: 0x04000848 RID: 2120
		private ExDateTime lastTargetTime = ExDateTime.MinValue;

		// Token: 0x04000849 RID: 2121
		private int policyHashCode;

		// Token: 0x0400084A RID: 2122
		private uint policyKey;

		// Token: 0x0400084B RID: 2123
		private BudgetKey budgetKey;

		// Token: 0x0400084C RID: 2124
		private bool currentlyExecuting;

		// Token: 0x0400084D RID: 2125
		private DisposeTracker disposeTracker;

		// Token: 0x0400084E RID: 2126
		private ExDateTime policyExpirationTime = ExDateTime.MaxValue;

		// Token: 0x0400084F RID: 2127
		private IAccountValidationContext accountValidationContext;

		// Token: 0x020000F0 RID: 240
		private enum DiagnosticEvent
		{
			// Token: 0x04000854 RID: 2132
			Created,
			// Token: 0x04000855 RID: 2133
			XsoEvent,
			// Token: 0x04000856 RID: 2134
			XsoException,
			// Token: 0x04000857 RID: 2135
			Cached,
			// Token: 0x04000858 RID: 2136
			Removed,
			// Token: 0x04000859 RID: 2137
			Stolen,
			// Token: 0x0400085A RID: 2138
			Killed,
			// Token: 0x0400085B RID: 2139
			HBTimeout,
			// Token: 0x0400085C RID: 2140
			Acquired,
			// Token: 0x0400085D RID: 2141
			Released,
			// Token: 0x0400085E RID: 2142
			Locked,
			// Token: 0x0400085F RID: 2143
			RunStart,
			// Token: 0x04000860 RID: 2144
			RunEnd,
			// Token: 0x04000861 RID: 2145
			AccountTerminated
		}

		// Token: 0x020000F1 RID: 241
		internal enum AsyncEventType
		{
			// Token: 0x04000863 RID: 2147
			XsoEventAvailable,
			// Token: 0x04000864 RID: 2148
			XsoException,
			// Token: 0x04000865 RID: 2149
			Timeout,
			// Token: 0x04000866 RID: 2150
			Acquire,
			// Token: 0x04000867 RID: 2151
			Release,
			// Token: 0x04000868 RID: 2152
			Kill,
			// Token: 0x04000869 RID: 2153
			AccountTerminated
		}

		// Token: 0x020000F2 RID: 242
		internal abstract class HbiMonitor
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x0004973B File Offset: 0x0004793B
			public void Initialize(int heartbeatSampleSize, int heartbeatAlertThreshold)
			{
				this.hbiSamples = new uint[heartbeatSampleSize];
				this.heartbeatAlertThreshold = heartbeatAlertThreshold;
				AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.ProtocolTracer, this, "Heartbeat monitor has sample size {0} and alert threshold {1}.", this.hbiSamples.Length, this.heartbeatAlertThreshold);
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00049770 File Offset: 0x00047970
			public void RegisterSample(uint heartbeatInterval, INotificationManagerContext context)
			{
				if (context.DeviceIdentity.DeviceType.StartsWith("TestActiveSyncConnectivity"))
				{
					return;
				}
				lock (this.hbiSamples)
				{
					uint num = this.hbiSamples[this.insertionIndex];
					this.hbiSamples[this.insertionIndex] = heartbeatInterval;
					this.insertionIndex = (this.insertionIndex + 1) % this.hbiSamples.Length;
					this.hbiSum += heartbeatInterval;
					this.hbiSum -= num;
					if ((ulong)this.numSamples == (ulong)((long)this.hbiSamples.Length))
					{
						uint num2 = this.hbiSum / this.numSamples;
						if ((ulong)num2 <= (ulong)((long)this.heartbeatAlertThreshold))
						{
							string text = context.CommandType.ToString();
							AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_AverageHbiTooLow, text, new string[]
							{
								num2.ToString(CultureInfo.InvariantCulture),
								text,
								this.heartbeatAlertThreshold.ToString(CultureInfo.InvariantCulture)
							});
						}
					}
					else
					{
						this.numSamples += 1U;
					}
				}
			}

			// Token: 0x0400086A RID: 2154
			private uint[] hbiSamples;

			// Token: 0x0400086B RID: 2155
			private int insertionIndex;

			// Token: 0x0400086C RID: 2156
			private uint hbiSum;

			// Token: 0x0400086D RID: 2157
			private uint numSamples;

			// Token: 0x0400086E RID: 2158
			private int heartbeatAlertThreshold;
		}

		// Token: 0x020000F3 RID: 243
		internal class AsyncEvent
		{
			// Token: 0x06000D61 RID: 3425 RVA: 0x000498A8 File Offset: 0x00047AA8
			internal AsyncEvent(Event xsoEvent) : this(NotificationManager.AsyncEventType.XsoEventAvailable)
			{
				this.xsoEvent = xsoEvent;
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000498B8 File Offset: 0x00047AB8
			internal AsyncEvent(NotificationManager.AsyncEventType type, IAsyncCommand command) : this(type)
			{
				this.command = command;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x000498C8 File Offset: 0x00047AC8
			internal AsyncEvent(Exception exception) : this(NotificationManager.AsyncEventType.XsoException)
			{
				this.exception = exception;
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x000498D8 File Offset: 0x00047AD8
			internal AsyncEvent(NotificationManager.AsyncEventType type)
			{
				this.CreationDate = DateTime.UtcNow;
				this.type = type;
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x000498F2 File Offset: 0x00047AF2
			internal AsyncEvent(AccountState accountState) : this(NotificationManager.AsyncEventType.AccountTerminated)
			{
				this.accountState = accountState;
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00049902 File Offset: 0x00047B02
			// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0004990A File Offset: 0x00047B0A
			internal DateTime CreationDate { get; private set; }

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00049913 File Offset: 0x00047B13
			internal Event Event
			{
				get
				{
					return this.xsoEvent;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0004991B File Offset: 0x00047B1B
			internal Exception Exception
			{
				get
				{
					return this.exception;
				}
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00049923 File Offset: 0x00047B23
			// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0004992B File Offset: 0x00047B2B
			internal NotificationManager.AsyncEventType Type
			{
				get
				{
					return this.type;
				}
				set
				{
					this.type = value;
				}
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00049934 File Offset: 0x00047B34
			internal IAsyncCommand Command
			{
				get
				{
					return this.command;
				}
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0004993C File Offset: 0x00047B3C
			internal AccountState AccountState
			{
				get
				{
					return this.accountState;
				}
			}

			// Token: 0x0400086F RID: 2159
			private NotificationManager.AsyncEventType type;

			// Token: 0x04000870 RID: 2160
			private Event xsoEvent;

			// Token: 0x04000871 RID: 2161
			private Exception exception;

			// Token: 0x04000872 RID: 2162
			private IAsyncCommand command;

			// Token: 0x04000873 RID: 2163
			private AccountState accountState;
		}
	}
}
