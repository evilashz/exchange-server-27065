using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DispatchManager : DisposeTrackableBase, IHealthLogDispatchEntryReporter
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		public DispatchManager(SyncLogSession syncLogSession, bool isDispatchingEnabled, TimeSpan primingDispatchTime, TimeSpan minimumDispatchWaitForFailedSync, TimeSpan dispatchOutageThreshold, Action<EventLogEntry> eventLoggerDelegate, ISyncHealthLog syncHealthLogger, ISyncManagerConfiguration configuration)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
			this.isDispatchingEnabled = isDispatchingEnabled;
			this.syncQueueManager = this.CreateSyncQueueManager(minimumDispatchWaitForFailedSync);
			this.dispatchEntryManager = this.CreateDispatchEntryManager(syncHealthLogger, configuration);
			this.dispatchEntryManager.EntryExpiredEvent += this.OnSyncTimedOut;
			this.dbPicker = this.CreateDatabasePicker(configuration, this.syncLogSession, this.dispatchEntryManager, this, this.syncQueueManager);
			this.dispatchWorkChecker = this.CreateDispatchWorkChecker(configuration);
			this.dispatcher = this.CreateDispatcher();
			this.dispatchDriver = this.CreateDispatchDriver(primingDispatchTime);
			this.dispatchDriver.PrimingEvent += this.OnPrimingEventHandler;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		internal SyncQueueManager SyncQueueManager
		{
			get
			{
				return this.syncQueueManager;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public void OnSubscriptionSyncCompletedHandler(object sender, OnSyncCompletedEventArgs eventArgs)
		{
			SyncUtilities.ThrowIfArgumentNull("eventArgs", eventArgs);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SyncCompletedCallback), eventArgs);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		public virtual void SyncCompleted(SubscriptionCompletionData subscriptionCompletionData)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionCompletionData", subscriptionCompletionData);
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionCompletionData.MailboxGuid, subscriptionCompletionData.SubscriptionGuid);
			DispatchEntry dispatchEntry;
			if (!this.dispatchEntryManager.TryRemoveDispatchedEntry(subscriptionCompletionData.SubscriptionGuid, out dispatchEntry))
			{
				syncLogSession.LogDebugging((TSLID)195UL, "DM.SyncCompleted: Subscription not in list of dispatched subscriptions", new object[0]);
			}
			else
			{
				subscriptionCompletionData.DispatchedWorkType = new WorkType?(dispatchEntry.WorkType);
			}
			this.syncQueueManager.SyncCompleted(subscriptionCompletionData);
			WorkType? dispatchedWorkType = subscriptionCompletionData.DispatchedWorkType;
			if (dispatchedWorkType == null)
			{
				subscriptionCompletionData.TryGetCurrentWorkType(syncLogSession, out dispatchedWorkType);
			}
			Guid databaseGuid;
			IList<WorkType> workTypes;
			if (this.dbPicker.TryGetNextDatabaseToDispatchFrom(DispatchTrigger.Completion, out databaseGuid, out workTypes))
			{
				this.DispatchSubscriptionForDatabase(databaseGuid, workTypes, DispatchTrigger.Completion, dispatchedWorkType);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public void ReportDispatchAttempt(DispatchEntry dispatchEntry, DispatchTrigger dispatchTrigger, WorkType? workType, DispatchResult dispatchResult, ISubscriptionInformation subscriptionInformation, ExDateTime? lastDispatchTime)
		{
			Guid guid = Guid.Empty;
			Guid guid2 = Guid.Empty;
			Guid guid3 = Guid.Empty;
			Guid guid4 = Guid.Empty;
			string incomingServerName = string.Empty;
			string dispatchedTo = string.Empty;
			string subscriptionType = string.Empty;
			string aggregationType = string.Empty;
			int num = 0;
			if (dispatchEntry != null)
			{
				guid2 = dispatchEntry.MiniSubscriptionInformation.MailboxGuid;
				guid3 = dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid;
				guid4 = dispatchEntry.MiniSubscriptionInformation.DatabaseGuid;
				if (subscriptionInformation != null)
				{
					guid = subscriptionInformation.TenantGuid;
					subscriptionType = subscriptionInformation.SubscriptionType.ToString();
					aggregationType = subscriptionInformation.AggregationType.ToString();
					incomingServerName = subscriptionInformation.IncomingServerName;
					dispatchedTo = ((subscriptionInformation.HubServerDispatched == null) ? string.Empty : subscriptionInformation.HubServerDispatched);
					if (lastDispatchTime != null)
					{
						WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(dispatchEntry.WorkType);
						ExDateTime dt;
						if (workTypeDefinition.TimeTillSyncDue.TotalSeconds == 0.0)
						{
							dt = dispatchEntry.DispatchEnqueuedTime;
						}
						else
						{
							dt = lastDispatchTime.Value;
						}
						TimeSpan t = dispatchEntry.DispatchAttemptTime - dt;
						double totalSeconds = (t - workTypeDefinition.TimeTillSyncDue).TotalSeconds;
						if (totalSeconds >= 2147483647.0)
						{
							num = int.MaxValue;
						}
						else if (totalSeconds <= -2147483648.0)
						{
							num = int.MinValue;
						}
						else
						{
							num = Convert.ToInt32(totalSeconds);
						}
					}
				}
			}
			bool beyondSyncPollingFrequency = num > 0;
			this.InternalReportSubscriptionDispatch(Environment.MachineName, guid.ToString(), guid2.ToString(), guid3.ToString(), incomingServerName, subscriptionType, aggregationType, dispatchedTo, DispatchResult.Success == dispatchResult, DispatchResult.Success < (DispatchResult.PermanentFailure & dispatchResult), DispatchResult.Success < (DispatchResult.TransientFailure & dispatchResult), dispatchResult.ToString(), beyondSyncPollingFrequency, num, (workType != null) ? workType.ToString() : string.Empty, dispatchTrigger.ToString(), guid4.ToString());
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
		internal void StopActiveDispatching()
		{
			this.dispatchDriver.PrimingEvent -= this.OnPrimingEventHandler;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		internal XElement GetDiagnosticInfo(SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("DispatchManager");
			xelement.Add(new XElement("lastDequeueAttemptTime", (this.lastDequeueAttempt != null) ? this.lastDequeueAttempt.Value.ToString("o") : string.Empty));
			xelement.Add(new XElement("lastPrimerStartTime", (this.lastPrimerStartTime != null) ? this.lastPrimerStartTime.Value.ToString("o") : string.Empty));
			if (mode == SyncDiagnosticMode.Verbose)
			{
				this.dispatchDriver.AddDiagnosticInfoTo(xelement);
				this.dbPicker.AddDiagnosticInfoTo(xelement);
			}
			xelement.Add(this.syncQueueManager.GetDiagnosticInfo(mode));
			xelement.Add(this.dispatchEntryManager.GetDiagnosticInfo(mode));
			xelement.Add(this.dispatcher.GetDiagnosticInfo(mode));
			return xelement;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000FAC2 File Offset: 0x0000DCC2
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000FACC File Offset: 0x0000DCCC
		protected virtual void InternalReportSubscriptionDispatch(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string incomingServerName, string subscriptionType, string aggregationType, string dispatchedTo, bool successful, bool permanentError, bool transientError, string dispatchError, bool beyondSyncPollingFrequency, int secondsBeyondPollingFrequency, string workType, string dispatchTrigger, string databaseGuid)
		{
			DataAccessLayer.ReportSubscriptionDispatch(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, incomingServerName, subscriptionType, aggregationType, dispatchedTo, successful, permanentError, transientError, dispatchError, beyondSyncPollingFrequency, secondsBeyondPollingFrequency, workType, dispatchTrigger, databaseGuid);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000FAFD File Offset: 0x0000DCFD
		protected virtual SyncQueueManager CreateSyncQueueManager(TimeSpan minimumDispatchWaitForFailedSync)
		{
			return new SyncQueueManager(this.syncLogSession, minimumDispatchWaitForFailedSync);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000FB0B File Offset: 0x0000DD0B
		protected virtual DispatchWorkChecker CreateDispatchWorkChecker(ISyncManagerConfiguration syncManagerConfiguration)
		{
			return new DispatchWorkChecker(this.syncLogSession, this.dispatchEntryManager, syncManagerConfiguration, this);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000FB20 File Offset: 0x0000DD20
		protected virtual IDispatcher CreateDispatcher()
		{
			return new SubscriptionDispatcher();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000FB27 File Offset: 0x0000DD27
		protected virtual IDispatchDriver CreateDispatchDriver(TimeSpan primingDispatchTime)
		{
			return new PrimingDispatchDriver(this.syncLogSession, primingDispatchTime);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000FB35 File Offset: 0x0000DD35
		protected void DisabledExpiration()
		{
			this.dispatchEntryManager.DisabledExpiration();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000FB44 File Offset: 0x0000DD44
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dispatchDriver != null)
				{
					IDisposable disposable = this.dispatchDriver as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.dispatchDriver = null;
				}
				if (this.dispatchEntryManager != null)
				{
					IDisposable disposable2 = this.dispatchEntryManager as IDisposable;
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
					this.dispatchEntryManager = null;
				}
				this.dispatcher.Shutdown();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000FBA7 File Offset: 0x0000DDA7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DispatchManager>(this);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000FBAF File Offset: 0x0000DDAF
		protected virtual IDispatchEntryManager CreateDispatchEntryManager(ISyncHealthLog syncHealthLogger, ISyncManagerConfiguration configuration)
		{
			return new DispatchEntryManager(this.syncLogSession, syncHealthLogger, configuration);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		protected virtual DatabasePicker CreateDatabasePicker(ISyncManagerConfiguration configuration, SyncLogSession syncLogSession, IDispatchEntryManager dispatchEntryManager, IHealthLogDispatchEntryReporter healthLogDispatchEntryReporter, SyncQueueManager syncQueueManager)
		{
			return new DatabasePicker(configuration, syncLogSession, dispatchEntryManager, healthLogDispatchEntryReporter, syncQueueManager);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		protected virtual DispatchResult HandSubscriptionToDispatcher(DispatchEntry dispatchEntry, ISubscriptionInformation subscriptionInformation)
		{
			return this.dispatcher.DispatchSubscription(dispatchEntry, subscriptionInformation);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		protected virtual DispatchResult TryGetSubscriptionInformation(DispatchEntry dispatchEntry, out ISubscriptionInformation subscriptionInformation)
		{
			bool flag = false;
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(dispatchEntry.MiniSubscriptionInformation.MailboxGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			subscriptionInformation = null;
			SubscriptionInformation subscriptionInformation2;
			if (!DataAccessLayer.TryReadSubscriptionInformation(dispatchEntry.MiniSubscriptionInformation.DatabaseGuid, dispatchEntry.MiniSubscriptionInformation.MailboxGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid, out subscriptionInformation2, out flag))
			{
				return DispatchResult.TransientFailureReadingCache;
			}
			if (subscriptionInformation2 == null)
			{
				syncLogSession.LogInformation((TSLID)353UL, "DM: Subscription no longer exists in cache.", new object[0]);
				return DispatchResult.SubscriptionCacheMessageDoesNotExist;
			}
			if (subscriptionInformation2.Disabled)
			{
				syncLogSession.LogInformation((TSLID)354UL, "DM: Subscription is disabled.", new object[0]);
				subscriptionInformation = null;
				return DispatchResult.SubscriptionDisabled;
			}
			subscriptionInformation = subscriptionInformation2;
			return DispatchResult.Success;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000FC98 File Offset: 0x0000DE98
		private void DispatchSubscriptionForDatabase(Guid databaseGuid, IList<WorkType> workTypes, DispatchTrigger dispatchTrigger, WorkType? completedWorkType)
		{
			this.syncLogSession.LogDebugging((TSLID)196UL, "DM.DispatchSubscriptionForDatabase: databaseGuid: {0}, dispatchTrigger: {1}, completedWorkType: {2}.", new object[]
			{
				databaseGuid,
				dispatchTrigger,
				(completedWorkType != null) ? completedWorkType.Value.ToString() : string.Empty
			});
			if (!this.isDispatchingEnabled)
			{
				this.syncLogSession.LogVerbose((TSLID)1363UL, "DM.DispatchSubscriptionForDatabase. Not dispatching subcription. Dispatching is disabled", new object[0]);
				return;
			}
			DispatchEntry dispatchEntry = null;
			lock (this.dispatchEntryManager)
			{
				foreach (WorkType workType in workTypes)
				{
					if (!this.dispatchEntryManager.HasBudget(workType))
					{
						this.syncLogSession.LogVerbose((TSLID)496UL, "DM.DispatchSubscriptionForDatabase: Work type {0} is out of budget.", new object[]
						{
							workType
						});
					}
					else if (this.TryGetSubscriptionForDatabaseAndWorkType(databaseGuid, workType, dispatchTrigger, completedWorkType, out dispatchEntry))
					{
						break;
					}
				}
				if (dispatchEntry == null)
				{
					foreach (WorkType workType2 in workTypes)
					{
						if (this.TryGetSubscriptionForDatabaseAndWorkType(databaseGuid, workType2, dispatchTrigger, completedWorkType, out dispatchEntry))
						{
							break;
						}
					}
				}
			}
			if (dispatchEntry != null)
			{
				this.DispatchSubscription(dispatchEntry, dispatchTrigger);
				return;
			}
			this.syncLogSession.LogDebugging((TSLID)335UL, "DispatchSubscriptionForDatabase: No subscription available for dispatch", new object[0]);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000FE58 File Offset: 0x0000E058
		private bool TryGetSubscriptionForDatabaseAndWorkType(Guid databaseGuid, WorkType workType, DispatchTrigger dispatchTrigger, WorkType? completedWorkType, out DispatchEntry dispatchEntry)
		{
			dispatchEntry = null;
			if (!this.dispatchWorkChecker.CanAttemptDispatchForWorkType(dispatchTrigger, workType, completedWorkType))
			{
				return false;
			}
			this.lastDequeueAttempt = new ExDateTime?(this.GetCurrentTime());
			if (!this.syncQueueManager.TryGetWorkToDispatch(this.GetCurrentTime(), databaseGuid, workType, out dispatchEntry))
			{
				this.syncLogSession.LogDebugging((TSLID)1320UL, "DM.TryDispatchSubscriptionForDatabaseAndWorkType: No work to dispatch", new object[0]);
				return false;
			}
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(dispatchEntry.MiniSubscriptionInformation.MailboxGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			DispatchResult? dispatchResult;
			if (!this.dispatchWorkChecker.CanAttemptDispatchForSubscription(dispatchEntry, out dispatchResult))
			{
				this.ReportDispatchAttempt(dispatchEntry, dispatchTrigger, new WorkType?(workType), dispatchResult.Value, null, null);
				WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(workType);
				if (!workTypeDefinition.IsOneOff)
				{
					this.syncQueueManager.DispatchAttemptCompleted(dispatchEntry, dispatchResult.Value);
				}
				else
				{
					syncLogSession.LogDebugging((TSLID)215UL, "DM.TryDispatchSubscriptionForDatabaseAndWorkType: Not reenqueueing for dispatch one-off worktype : {0}", new object[]
					{
						workType
					});
				}
				dispatchEntry = null;
				return false;
			}
			this.dispatchEntryManager.Add(dispatchEntry);
			return true;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000FF8C File Offset: 0x0000E18C
		private void DispatchSubscription(DispatchEntry dispatchEntry, DispatchTrigger dispatchTrigger)
		{
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(dispatchEntry.MiniSubscriptionInformation.MailboxGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			ISubscriptionInformation subscriptionInformation;
			DispatchResult dispatchResult = this.TryGetSubscriptionInformation(dispatchEntry, out subscriptionInformation);
			ExDateTime? lastDispatchTime = null;
			if (dispatchResult == DispatchResult.Success)
			{
				bool flag = false;
				lastDispatchTime = subscriptionInformation.LastSuccessfulDispatchTime;
				AggregationSubscription subscription = null;
				if (subscriptionInformation.SupportsSerialization && subscriptionInformation.SerializedSubscription.TryDeserializeSubscription(out subscription))
				{
					flag = MrsAdapter.UpdateAndCheckMrsJob(syncLogSession, subscription, dispatchEntry.MiniSubscriptionInformation.DatabaseGuid, dispatchEntry.MiniSubscriptionInformation.ExternalDirectoryOrgId);
				}
				if (flag)
				{
					return;
				}
				dispatchResult = this.HandSubscriptionToDispatcher(dispatchEntry, subscriptionInformation);
			}
			syncLogSession.LogDebugging((TSLID)342UL, "DM.DispatchSubscription. Result: {0}", new object[]
			{
				dispatchResult
			});
			this.syncQueueManager.DispatchAttemptCompleted(dispatchEntry, dispatchResult);
			DispatchResult dispatchResult2 = dispatchResult;
			if (dispatchResult2 == DispatchResult.Success)
			{
				this.dispatchEntryManager.MarkDispatchSuccess(dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			}
			else
			{
				this.dispatchEntryManager.RemoveDispatchAttempt(dispatchEntry.MiniSubscriptionInformation.DatabaseGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			}
			this.dbPicker.MarkDispatchCompleted(dispatchEntry.MiniSubscriptionInformation.DatabaseGuid, dispatchResult);
			this.ReportDispatchAttempt(dispatchEntry, dispatchTrigger, new WorkType?(dispatchEntry.WorkType), dispatchResult, subscriptionInformation, lastDispatchTime);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000100C8 File Offset: 0x0000E2C8
		private void SyncCompletedCallback(object state)
		{
			OnSyncCompletedEventArgs onSyncCompletedEventArgs = (OnSyncCompletedEventArgs)state;
			this.SyncCompleted(onSyncCompletedEventArgs.SubscriptionCompletionData);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private void OnPrimingEventHandler(object sender, EventArgs args)
		{
			Guid databaseGuid;
			IList<WorkType> workTypes;
			if (this.dbPicker.TryGetNextDatabaseToDispatchFrom(DispatchTrigger.Primer, out databaseGuid, out workTypes))
			{
				this.DispatchSubscriptionForDatabase(databaseGuid, workTypes, DispatchTrigger.Primer, null);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00010119 File Offset: 0x0000E319
		private void OnSyncTimedOut(object sender, DispatchEntry dispatchEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("dispatchEntry", dispatchEntry);
			this.syncQueueManager.SyncTimedOut(dispatchEntry);
		}

		// Token: 0x04000147 RID: 327
		private SyncQueueManager syncQueueManager;

		// Token: 0x04000148 RID: 328
		private IDispatchEntryManager dispatchEntryManager;

		// Token: 0x04000149 RID: 329
		private IDispatcher dispatcher;

		// Token: 0x0400014A RID: 330
		private IDispatchDriver dispatchDriver;

		// Token: 0x0400014B RID: 331
		private SyncLogSession syncLogSession;

		// Token: 0x0400014C RID: 332
		private DatabasePicker dbPicker;

		// Token: 0x0400014D RID: 333
		private ExDateTime? lastDequeueAttempt = null;

		// Token: 0x0400014E RID: 334
		private ExDateTime? lastPrimerStartTime = null;

		// Token: 0x0400014F RID: 335
		private DispatchWorkChecker dispatchWorkChecker;

		// Token: 0x04000150 RID: 336
		private bool isDispatchingEnabled;
	}
}
