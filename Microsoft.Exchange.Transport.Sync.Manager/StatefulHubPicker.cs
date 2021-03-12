using System;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class StatefulHubPicker
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000103D0 File Offset: 0x0000E5D0
		private StatefulHubPicker()
		{
			this.hubState = new StatefulHubPicker.HubState();
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000103EE File Offset: 0x0000E5EE
		internal static StatefulHubPicker Instance
		{
			get
			{
				return StatefulHubPicker.instance;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000103F8 File Offset: 0x0000E5F8
		internal void Shutdown()
		{
			lock (this.syncObject)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)112UL, StatefulHubPicker.Tracer, "StatefulHubPicker Shutdown called.", new object[0]);
				this.hubLoadReducerTimer.Dispose();
				this.hubLoadReducerTimer = null;
				this.hubActivatorTimer.Dispose();
				this.hubActivatorTimer = null;
				this.hubSubscriptionTypeActivatorTimer.Dispose();
				this.hubSubscriptionTypeActivatorTimer = null;
				if (this.currentServer != null)
				{
					this.currentServer.Dispose();
					this.currentServer = null;
				}
				this.state = StatefulHubPicker.StatefulHubPickerStatus.Stopped;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000104AC File Offset: 0x0000E6AC
		internal void Start()
		{
			lock (this.syncObject)
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)113UL, StatefulHubPicker.Tracer, "StatefulHubPicker: Start called.", new object[0]);
				this.hubActivatorTimer = new StatefulHubPicker.WorkTimer(ContentAggregationConfig.HubInactivityPeriod, new TimerCallback(this.HubActivator));
				this.hubLoadReducerTimer = new StatefulHubPicker.WorkTimer(ContentAggregationConfig.HubBusyPeriod, new TimerCallback(this.HubLoadReducer));
				this.hubSubscriptionTypeActivatorTimer = new StatefulHubPicker.WorkTimer(ContentAggregationConfig.HubSubscriptionTypeNotSupportedPeriod, new TimerCallback(this.HubSubscriptionTypeActivator));
				this.state = StatefulHubPicker.StatefulHubPickerStatus.Started;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00010564 File Offset: 0x0000E764
		internal void ResetHubLoad()
		{
			lock (this.syncObject)
			{
				this.CheckStarted();
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)90UL, StatefulHubPicker.Tracer, "Resetting hub load.", new object[0]);
				switch (this.hubState.Status)
				{
				case StatefulHubPicker.HubState.HubStatus.Busy:
					this.hubLoadReducerTimer.Deactivate();
					break;
				case StatefulHubPicker.HubState.HubStatus.Inactive:
					this.hubActivatorTimer.Deactivate();
					break;
				}
				this.hubState.Status = StatefulHubPicker.HubState.HubStatus.Active;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010608 File Offset: 0x0000E808
		internal XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("StatefulHubPicker");
			lock (this.syncObject)
			{
				if (this.state == StatefulHubPicker.StatefulHubPickerStatus.Started)
				{
					XElement xelement2 = new XElement("HubState");
					xelement2.Add(new XElement("status", this.hubState.Status));
					xelement2.Add(new XElement("firstUnsuccessfulDispatchTime", (this.hubState.FirstUnsuccessfulDispatch != null) ? this.hubState.FirstUnsuccessfulDispatch.Value.ToString("o") : string.Empty));
					xelement2.Add(new XElement("lastDispatchAttemptTime", (this.hubState.LastDispatchUpdate != null) ? this.hubState.LastDispatchUpdate.Value.ToString("o") : string.Empty));
					xelement.Add(xelement2);
					this.hubLoadReducerTimer.AddDiagnosticsTo(xelement, "HubLoadReducer");
					this.hubActivatorTimer.AddDiagnosticsTo(xelement, "HubActivator");
					this.hubSubscriptionTypeActivatorTimer.AddDiagnosticsTo(xelement, "HubSubscriptionTypeActivator");
				}
			}
			return xelement;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010784 File Offset: 0x0000E984
		internal bool IsSubscriptionTypeEnabled(AggregationSubscriptionType subscriptionType)
		{
			bool result;
			lock (this.syncObject)
			{
				this.CheckStarted();
				result = this.hubState.IsSubscriptionTypeEnabled(subscriptionType);
			}
			return result;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000107D4 File Offset: 0x0000E9D4
		internal bool TryGetServerForDispatch(out ContentAggregationHubServer hubServer, out SubscriptionSubmissionResult? lastSubmissionResult)
		{
			hubServer = null;
			lock (this.syncObject)
			{
				this.CheckStarted();
				lastSubmissionResult = this.lastSubmissionResult;
				if (this.currentServer == null)
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)221UL, StatefulHubPicker.Tracer, "Reset current hub server", new object[0]);
					this.currentServer = new ContentAggregationHubServer();
				}
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)222UL, StatefulHubPicker.Tracer, "Current Hub Server: {0} Status: {1}", new object[]
				{
					this.currentServer,
					this.hubState
				});
				if (this.hubState.IsActive())
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)223UL, StatefulHubPicker.Tracer, "Hub Selected for next dispatch.", new object[0]);
					hubServer = this.currentServer;
					hubServer.IncrementRef();
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)296UL, StatefulHubPicker.Tracer, "No Hub Selected for next dispatch.", new object[0]);
				}
			}
			return hubServer != null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00010904 File Offset: 0x0000EB04
		internal void ProcessDispatchResult(ContentAggregationHubServer server, AggregationSubscriptionType subscriptionType, SubscriptionSubmissionResult subscriptionSubmissionResult, ExDateTime dispatchTime)
		{
			lock (this.syncObject)
			{
				this.CheckStarted();
				this.lastSubmissionResult = new SubscriptionSubmissionResult?(subscriptionSubmissionResult);
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)300UL, StatefulHubPicker.Tracer, "ProcessDispatchResult called: SubscriptionSubmissionResult {0}.", new object[]
				{
					subscriptionSubmissionResult
				});
				this.MarkHubDispatchTime(dispatchTime, subscriptionSubmissionResult);
				if (subscriptionSubmissionResult == SubscriptionSubmissionResult.ServerNotAvailable)
				{
					ContentAggregationConfig.LogEvent(TransportSyncManagerEventLogConstants.Tuple_NoActiveBridgeheadServerForContentAggregation, Environment.MachineName, new object[0]);
					if (this.currentServer == server)
					{
						this.currentServer = null;
					}
					server.RequestDispose();
				}
				switch (subscriptionSubmissionResult)
				{
				case SubscriptionSubmissionResult.Success:
				case SubscriptionSubmissionResult.SubscriptionAlreadyOnHub:
				case SubscriptionSubmissionResult.UnknownRetryableError:
				case SubscriptionSubmissionResult.MaxConcurrentMailboxSubmissions:
				case SubscriptionSubmissionResult.RpcServerTooBusy:
				case SubscriptionSubmissionResult.RetryableRpcError:
				case SubscriptionSubmissionResult.DatabaseRpcLatencyUnhealthy:
				case SubscriptionSubmissionResult.DatabaseHealthUnknown:
				case SubscriptionSubmissionResult.DatabaseOverloaded:
				case SubscriptionSubmissionResult.TransportSyncDisabled:
				case SubscriptionSubmissionResult.UserTransportQueueUnhealthy:
				case SubscriptionSubmissionResult.TransportQueueHealthUnknown:
					goto IL_133;
				case SubscriptionSubmissionResult.SchedulerQueueFull:
				case SubscriptionSubmissionResult.ServerTransportQueueUnhealthy:
					this.MarkHubBusy();
					goto IL_133;
				case SubscriptionSubmissionResult.ServerNotAvailable:
				case SubscriptionSubmissionResult.EdgeTransportStopped:
					this.MarkHubInactive();
					goto IL_133;
				case SubscriptionSubmissionResult.SubscriptionTypeDisabled:
					this.MarkHubSubscriptionTypeDisabled(subscriptionType);
					goto IL_133;
				}
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)179UL, StatefulHubPicker.Tracer, "ProcessDispatchResult: Unexpected value of SubscriptionSubmissionResult: {0}", new object[]
				{
					subscriptionSubmissionResult
				});
				IL_133:
				server.DecrementRef();
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00010A74 File Offset: 0x0000EC74
		private void MarkHubDispatchTime(ExDateTime dispatchTime, SubscriptionSubmissionResult subscriptionSubmissionResult)
		{
			if (subscriptionSubmissionResult == SubscriptionSubmissionResult.ServerNotAvailable)
			{
				if (this.hubState.FirstUnsuccessfulDispatch == null)
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)301UL, StatefulHubPicker.Tracer, "Hub server became unavailable for dispatch.", new object[0]);
					this.hubState.FirstUnsuccessfulDispatch = new ExDateTime?(dispatchTime);
				}
			}
			else
			{
				if (this.hubState.FirstUnsuccessfulDispatch != null)
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)302UL, StatefulHubPicker.Tracer, "Hub server became available again for dispatch.", new object[0]);
				}
				this.hubState.FirstUnsuccessfulDispatch = null;
			}
			this.hubState.LastDispatchUpdate = new ExDateTime?(dispatchTime);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00010B44 File Offset: 0x0000ED44
		private void MarkHubBusy()
		{
			if (this.hubState.Status != StatefulHubPicker.HubState.HubStatus.Busy)
			{
				this.hubState.Status = StatefulHubPicker.HubState.HubStatus.Busy;
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)303UL, StatefulHubPicker.Tracer, "Setting hub status to Busy.", new object[0]);
				if (this.hubLoadReducerTimer.TryActivate())
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)304UL, StatefulHubPicker.Tracer, "Activating the busy timer.", new object[0]);
				}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		private void MarkHubInactive()
		{
			if (this.hubState.Status != StatefulHubPicker.HubState.HubStatus.Inactive)
			{
				this.hubState.Status = StatefulHubPicker.HubState.HubStatus.Inactive;
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)305UL, StatefulHubPicker.Tracer, "Setting hub status to Inactive.", new object[0]);
				if (this.hubActivatorTimer.TryActivate())
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)308UL, StatefulHubPicker.Tracer, "Activating the inactive timer.", new object[0]);
				}
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00010C44 File Offset: 0x0000EE44
		private void MarkHubSubscriptionTypeDisabled(AggregationSubscriptionType subscriptionType)
		{
			if (this.hubState.IsSubscriptionTypeEnabled(subscriptionType))
			{
				this.hubState.MarkSubscriptionTypeDisabled(subscriptionType);
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)310UL, StatefulHubPicker.Tracer, "Setting subscription type: {0} disabled on hub.", new object[]
				{
					subscriptionType
				});
				if (!this.hubSubscriptionTypeActivatorTimer.TryActivate())
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)312UL, StatefulHubPicker.Tracer, "Activating the subscription type disabled timer", new object[0]);
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010CCD File Offset: 0x0000EECD
		private void CheckStarted()
		{
			if (this.state != StatefulHubPicker.StatefulHubPickerStatus.Started)
			{
				throw new InvalidOperationException("Expected HubPicker to be in Started state.");
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		private void HubActivator(object state)
		{
			lock (this.syncObject)
			{
				if (this.state != StatefulHubPicker.StatefulHubPickerStatus.Stopped)
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)315UL, StatefulHubPicker.Tracer, "HubActivator Invoked", new object[0]);
					if (this.hubState.Status == StatefulHubPicker.HubState.HubStatus.Inactive)
					{
						ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)316UL, StatefulHubPicker.Tracer, "Activating hub server.", new object[0]);
						this.hubState.Status = StatefulHubPicker.HubState.HubStatus.Active;
					}
				}
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00010D90 File Offset: 0x0000EF90
		private void HubLoadReducer(object state)
		{
			lock (this.syncObject)
			{
				if (this.state != StatefulHubPicker.StatefulHubPickerStatus.Stopped)
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)317UL, StatefulHubPicker.Tracer, "HubLoadReducer Invoked", new object[0]);
					if (this.hubState.Status == StatefulHubPicker.HubState.HubStatus.Busy)
					{
						ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)318UL, StatefulHubPicker.Tracer, "Marking hub server to be not busy.", new object[0]);
						this.hubState.Status = StatefulHubPicker.HubState.HubStatus.Active;
					}
				}
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010E3C File Offset: 0x0000F03C
		private void HubSubscriptionTypeActivator(object state)
		{
			lock (this.syncObject)
			{
				if (this.state != StatefulHubPicker.StatefulHubPickerStatus.Stopped)
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)319UL, StatefulHubPicker.Tracer, "HubSubscriptionTypeActivator Invoked.", new object[0]);
					this.hubState.MarkAllSubscriptionTypesEnabled();
				}
			}
		}

		// Token: 0x04000157 RID: 343
		private static readonly Trace Tracer = ExTraceGlobals.StatefulHubPickerTracer;

		// Token: 0x04000158 RID: 344
		private static readonly StatefulHubPicker instance = new StatefulHubPicker();

		// Token: 0x04000159 RID: 345
		private readonly object syncObject = new object();

		// Token: 0x0400015A RID: 346
		private readonly StatefulHubPicker.HubState hubState;

		// Token: 0x0400015B RID: 347
		private StatefulHubPicker.StatefulHubPickerStatus state;

		// Token: 0x0400015C RID: 348
		private ContentAggregationHubServer currentServer;

		// Token: 0x0400015D RID: 349
		private StatefulHubPicker.WorkTimer hubLoadReducerTimer;

		// Token: 0x0400015E RID: 350
		private StatefulHubPicker.WorkTimer hubActivatorTimer;

		// Token: 0x0400015F RID: 351
		private StatefulHubPicker.WorkTimer hubSubscriptionTypeActivatorTimer;

		// Token: 0x04000160 RID: 352
		private SubscriptionSubmissionResult? lastSubmissionResult;

		// Token: 0x0200002E RID: 46
		private enum StatefulHubPickerStatus
		{
			// Token: 0x04000162 RID: 354
			Created,
			// Token: 0x04000163 RID: 355
			Started,
			// Token: 0x04000164 RID: 356
			Stopped
		}

		// Token: 0x0200002F RID: 47
		private sealed class HubState
		{
			// Token: 0x0600027A RID: 634 RVA: 0x00010ECA File Offset: 0x0000F0CA
			internal HubState()
			{
				this.hubStatus = StatefulHubPicker.HubState.HubStatus.Active;
				this.firstUnsuccessfulDispatch = null;
				this.lastDispatchUpdate = null;
				this.enabledSubscriptionTypes = AggregationSubscriptionType.All;
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600027B RID: 635 RVA: 0x00010EFC File Offset: 0x0000F0FC
			// (set) Token: 0x0600027C RID: 636 RVA: 0x00010F04 File Offset: 0x0000F104
			internal StatefulHubPicker.HubState.HubStatus Status
			{
				get
				{
					return this.hubStatus;
				}
				set
				{
					this.hubStatus = value;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600027D RID: 637 RVA: 0x00010F0D File Offset: 0x0000F10D
			// (set) Token: 0x0600027E RID: 638 RVA: 0x00010F15 File Offset: 0x0000F115
			internal ExDateTime? FirstUnsuccessfulDispatch
			{
				get
				{
					return this.firstUnsuccessfulDispatch;
				}
				set
				{
					this.firstUnsuccessfulDispatch = value;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600027F RID: 639 RVA: 0x00010F1E File Offset: 0x0000F11E
			// (set) Token: 0x06000280 RID: 640 RVA: 0x00010F26 File Offset: 0x0000F126
			internal ExDateTime? LastDispatchUpdate
			{
				get
				{
					return this.lastDispatchUpdate;
				}
				set
				{
					this.lastDispatchUpdate = value;
				}
			}

			// Token: 0x06000281 RID: 641 RVA: 0x00010F30 File Offset: 0x0000F130
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "Status: {0}, FirstUnsuccessfulDispatch: {1}, LastDispatch: {2}, EnabledSubscriptionTypes: {3}", new object[]
				{
					this.hubStatus,
					this.firstUnsuccessfulDispatch,
					this.lastDispatchUpdate,
					this.enabledSubscriptionTypes
				});
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00010F8C File Offset: 0x0000F18C
			internal bool IsActive()
			{
				return this.hubStatus == StatefulHubPicker.HubState.HubStatus.Active;
			}

			// Token: 0x06000283 RID: 643 RVA: 0x00010F97 File Offset: 0x0000F197
			internal bool IsSubscriptionTypeEnabled(AggregationSubscriptionType subscriptionType)
			{
				return (this.enabledSubscriptionTypes & subscriptionType) != AggregationSubscriptionType.Unknown;
			}

			// Token: 0x06000284 RID: 644 RVA: 0x00010FA7 File Offset: 0x0000F1A7
			internal void MarkSubscriptionTypeDisabled(AggregationSubscriptionType subscriptionType)
			{
				this.enabledSubscriptionTypes &= ~subscriptionType;
			}

			// Token: 0x06000285 RID: 645 RVA: 0x00010FB8 File Offset: 0x0000F1B8
			internal void MarkAllSubscriptionTypesEnabled()
			{
				this.enabledSubscriptionTypes = AggregationSubscriptionType.All;
			}

			// Token: 0x04000165 RID: 357
			private StatefulHubPicker.HubState.HubStatus hubStatus;

			// Token: 0x04000166 RID: 358
			private ExDateTime? firstUnsuccessfulDispatch;

			// Token: 0x04000167 RID: 359
			private ExDateTime? lastDispatchUpdate;

			// Token: 0x04000168 RID: 360
			private AggregationSubscriptionType enabledSubscriptionTypes;

			// Token: 0x02000030 RID: 48
			internal enum HubStatus
			{
				// Token: 0x0400016A RID: 362
				Active,
				// Token: 0x0400016B RID: 363
				Busy,
				// Token: 0x0400016C RID: 364
				Inactive
			}
		}

		// Token: 0x02000031 RID: 49
		private class WorkTimer : DisposeTrackableBase
		{
			// Token: 0x06000286 RID: 646 RVA: 0x00010FC8 File Offset: 0x0000F1C8
			public WorkTimer(TimeSpan delay, TimerCallback timerCallback)
			{
				SyncUtilities.ThrowIfArgumentNull("timerCallback", timerCallback);
				this.delay = delay;
				this.timerCallback = timerCallback;
				this.timer = new GuardedTimer(new TimerCallback(this.OnTimerFired));
				this.Deactivate();
			}

			// Token: 0x06000287 RID: 647 RVA: 0x0001101C File Offset: 0x0000F21C
			public void Deactivate()
			{
				base.CheckDisposed();
				lock (this.syncRoot)
				{
					if (this.active)
					{
						this.timer.Change(StatefulHubPicker.WorkTimer.Infinite, StatefulHubPicker.WorkTimer.Infinite);
						this.nextInvokationTime = null;
						this.active = false;
					}
				}
			}

			// Token: 0x06000288 RID: 648 RVA: 0x00011090 File Offset: 0x0000F290
			public bool TryActivate()
			{
				base.CheckDisposed();
				bool result;
				lock (this.syncRoot)
				{
					if (!this.active)
					{
						this.nextInvokationTime = new ExDateTime?(ExDateTime.UtcNow + this.delay);
						this.timer.Change(this.delay, StatefulHubPicker.WorkTimer.Infinite);
						this.active = true;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x00011118 File Offset: 0x0000F318
			public void AddDiagnosticsTo(XElement componentElement, string tagName)
			{
				base.CheckDisposed();
				XElement xelement = new XElement(tagName);
				xelement.Add(new XElement("active", this.active.ToString()));
				xelement.Add(new XElement("nextInvocationTime", (this.nextInvokationTime != null) ? this.nextInvokationTime.Value.ToString("o") : string.Empty));
				componentElement.Add(xelement);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000111A0 File Offset: 0x0000F3A0
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					lock (this.syncRoot)
					{
						if (this.timer != null)
						{
							this.timer.Dispose(true);
							this.timer = null;
						}
					}
				}
			}

			// Token: 0x0600028B RID: 651 RVA: 0x000111F8 File Offset: 0x0000F3F8
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<StatefulHubPicker.WorkTimer>(this);
			}

			// Token: 0x0600028C RID: 652 RVA: 0x00011200 File Offset: 0x0000F400
			private void OnTimerFired(object state)
			{
				this.timerCallback(null);
				this.Deactivate();
			}

			// Token: 0x0400016D RID: 365
			private static readonly TimeSpan Infinite = TimeSpan.FromMilliseconds(-1.0);

			// Token: 0x0400016E RID: 366
			private readonly object syncRoot = new object();

			// Token: 0x0400016F RID: 367
			private readonly TimeSpan delay;

			// Token: 0x04000170 RID: 368
			private readonly TimerCallback timerCallback;

			// Token: 0x04000171 RID: 369
			private GuardedTimer timer;

			// Token: 0x04000172 RID: 370
			private bool active;

			// Token: 0x04000173 RID: 371
			private ExDateTime? nextInvokationTime;
		}
	}
}
