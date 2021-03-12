using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000038 RID: 56
	internal abstract class EventController : Base, IDisposable
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00009D48 File Offset: 0x00007F48
		public EventController(DatabaseInfo databaseInfo, EventBasedAssistantCollection assistants, PoisonEventControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters, ThrottleGovernor serverGovernor, MapiEventTypeFlags moreEvents)
		{
			this.databaseInfo = databaseInfo;
			this.databaseCounters = databaseCounters;
			this.assistants = assistants;
			this.shutdownState = 0;
			this.poisonControl = poisonControl;
			MapiEventTypeFlags mapiEventTypeFlags = this.assistants.EventMask | moreEvents;
			this.filter = (((MapiEventTypeFlags)(-1) == mapiEventTypeFlags) ? null : Restriction.BitMaskNonZero(PropTag.EventMask, (int)mapiEventTypeFlags));
			this.governor = new DatabaseGovernor("event processing on '" + databaseInfo.DisplayName + "'", serverGovernor, new Throttle("EventDatabase", serverGovernor.Throttle.OpenThrottleValue, serverGovernor.Throttle));
			this.eventAccess = EventAccess.Create(this.DatabaseInfo, this.assistants);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009E28 File Offset: 0x00008028
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009E30 File Offset: 0x00008030
		public EventBasedAssistantCollection Assistants
		{
			get
			{
				return this.assistants;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00009E38 File Offset: 0x00008038
		public PoisonEventControl PoisonControl
		{
			get
			{
				return this.poisonControl;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009E40 File Offset: 0x00008040
		public PerformanceCountersPerDatabaseInstance DatabaseCounters
		{
			get
			{
				return this.databaseCounters;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009E48 File Offset: 0x00008048
		public bool Shutdown
		{
			get
			{
				return this.shutdownState != 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009E56 File Offset: 0x00008056
		public Throttle Throttle
		{
			get
			{
				return this.governor.Throttle;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009E63 File Offset: 0x00008063
		public ThrottleGovernor Governor
		{
			get
			{
				return this.governor;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009E6B File Offset: 0x0000806B
		public Restriction Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009E73 File Offset: 0x00008073
		public EventAccess EventAccess
		{
			get
			{
				return this.eventAccess;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009E7B File Offset: 0x0000807B
		public bool RestartRequired
		{
			get
			{
				return this.eventAccess.RestartRequired;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009E88 File Offset: 0x00008088
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00009E90 File Offset: 0x00008090
		private protected Bookmark DatabaseBookmark { protected get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00009E99 File Offset: 0x00008099
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00009EA4 File Offset: 0x000080A4
		protected long HighestEventPolled
		{
			get
			{
				return this.highestEventPolled;
			}
			set
			{
				this.highestEventPolled = value;
				this.DatabaseCounters.HighestEventPolled.RawValue = this.highestEventPolled;
				long timestamp = Stopwatch.GetTimestamp();
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(2764451133U, ref timestamp);
				}
				this.DatabaseCounters.ElapsedTimeSinceLastEventPolled.RawValue = timestamp;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009F04 File Offset: 0x00008104
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "EventController for database '" + this.databaseInfo.DisplayName + "'";
			}
			return this.toString;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009F34 File Offset: 0x00008134
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009F98 File Offset: 0x00008198
		public void Start()
		{
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Starting", this);
			bool flag = false;
			try
			{
				AIBreadcrumbs.StartupTrail.Drop("Starting database: " + this.DatabaseInfo.Guid);
				this.DatabaseBookmark = this.eventAccess.GetDatabaseBookmark();
				Btree<Guid, Bookmark> btree = this.eventAccess.LoadAllMailboxBookmarks(this.DatabaseBookmark);
				bool flag2 = false;
				int num = 0;
				using (List<AssistantCollectionEntry>.Enumerator enumerator = this.assistants.ToList<AssistantCollectionEntry>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AssistantCollectionEntry assistant = enumerator.Current;
						do
						{
							try
							{
								base.CatchMeIfYouCan(delegate
								{
									assistant.Start(EventBasedStartInfo.NoInformation);
								}, assistant.Name);
							}
							catch (AIException ex)
							{
								if (num >= 1 || this.assistants.Count <= 1)
								{
									throw;
								}
								if (!flag2)
								{
									ExTraceGlobals.EventControllerTracer.TraceError<AssistantCollectionEntry, AIException>((long)this.GetHashCode(), "Event Based Assistant {0} cannot start due to Exception: {1}, Retrying now", assistant, ex);
									SingletonEventLogger.Logger.LogEvent(AssistantsEventLogConstants.Tuple_RetryAssistantFailedToStart, null, new object[]
									{
										assistant.Identity.ToString(),
										ex.ToString(),
										EventController.sleepStartingThread.TotalSeconds.ToString()
									});
									Thread.Sleep(EventController.sleepStartingThread);
									flag2 = true;
								}
								else
								{
									ExTraceGlobals.EventControllerTracer.TraceError<AssistantCollectionEntry, AIException>((long)this.GetHashCode(), "Event Based Assistant {0} cannot start after retry, due to Exception: {1}, will not start it anymore", assistant, ex);
									SingletonEventLogger.Logger.LogEvent(AssistantsEventLogConstants.Tuple_AssistantFailedToStart, null, new object[]
									{
										assistant.Identity.ToString(),
										ex.ToString()
									});
									flag2 = false;
									this.assistants.RemoveAssistant(assistant);
									num++;
								}
							}
						}
						while (flag2);
					}
				}
				this.InitializeEventDispatchers(btree);
				this.timeToSaveWatermarks = DateTime.UtcNow + Configuration.ActiveWatermarksSaveInterval;
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, DateTime>((long)this.GetHashCode(), "{0}: Next time to save watermarks: {1}", this, this.timeToSaveWatermarks);
				long num2 = long.MaxValue;
				foreach (Bookmark bookmark in btree)
				{
					num2 = Math.Min(num2, bookmark.GetLowestWatermark());
				}
				num2 = Math.Min(this.DatabaseBookmark.GetLowestWatermark(), num2);
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long>((long)this.GetHashCode(), "{0}: Smallest watermark after initialization is: {1}", this, num2);
				this.HighestEventPolled = num2;
				this.timer = new Timer(new TimerCallback(this.TimerRoutine), null, TimeSpan.Zero, Configuration.EventPollingInterval);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					RpcHangDetector rpcHangDetector = RpcHangDetector.Create();
					rpcHangDetector.InvokeUnderHangDetection(delegate(HangDetector hangDetector)
					{
						AIBreadcrumbs.StatusTrail.Drop("Did not succeed to start event controller, stopping.");
						this.RequestStop(rpcHangDetector);
						this.WaitUntilAssistantsStopped();
						AIBreadcrumbs.StatusTrail.Drop("Exiting stop on fail to start event controller to start.");
					});
				}
				else
				{
					AIBreadcrumbs.StartupTrail.Drop("Finished starting " + this.DatabaseInfo.Guid);
				}
			}
			base.TracePfd("PFD AIS {0} {1}: Started successfully", new object[]
			{
				21335,
				this
			});
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A370 File Offset: 0x00008570
		public void RequestStop(HangDetector hangDetector)
		{
			EventController.ShutdownState shutdownState = (EventController.ShutdownState)Interlocked.CompareExchange(ref this.shutdownState, 1, 0);
			AIBreadcrumbs.ShutdownTrail.Drop(string.Concat(new object[]
			{
				"Previous shutdown state: ",
				shutdownState,
				". Current: ",
				this.shutdownState.ToString()
			}));
			if (shutdownState == EventController.ShutdownState.NotRequested)
			{
				base.TracePfd("PFD AIS {0} {1}: phase1 shutdown", new object[]
				{
					27735,
					this
				});
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				this.assistants.ShutdownAssistants(hangDetector);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A418 File Offset: 0x00008618
		public void WaitUntilStopped()
		{
			this.WaitUntilAssistantsStopped();
			if (this.shutdownState == 1)
			{
				lock (this.watermarkUpdateLock)
				{
					if (this.shutdownState == 1)
					{
						ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Saving watermarks...", this);
						this.UpdateWatermarks();
						this.shutdownState = 2;
					}
				}
			}
			base.TracePfd("PFD AIS {0} {1}: Saved watermarks.", new object[]
			{
				17495,
				this
			});
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public void Stop()
		{
			RpcHangDetector rpcHangDetector = RpcHangDetector.Create();
			rpcHangDetector.InvokeUnderHangDetection(delegate(HangDetector hangDetector)
			{
				AIBreadcrumbs.StatusTrail.Drop("Event controller stop called.");
				this.RequestStop(rpcHangDetector);
				this.WaitUntilStopped();
				AIBreadcrumbs.StatusTrail.Drop("Exiting event controller stop.");
			});
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A534 File Offset: 0x00008734
		public virtual void IncrementEventQueueCount()
		{
			long num = Interlocked.Increment(ref this.numberEventsInQueueCurrent);
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(3838192957U, ref num);
			}
			this.DatabaseCounters.EventsInQueueCurrent.RawValue = num;
			if (num == (long)Configuration.MaximumEventQueueSize)
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Number of events queued is at maximum.", this);
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long>((long)this.GetHashCode(), "{0}: Incremented numberEventsInQueueCurrent to {1}", this, num);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A5B5 File Offset: 0x000087B5
		public void DecrementEventQueueCount()
		{
			this.DecrementEventQueueCount(1L);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public virtual void DecrementEventQueueCount(long count)
		{
			long num = Interlocked.Add(ref this.numberEventsInQueueCurrent, -count);
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(3838192957U, ref num);
			}
			this.DatabaseCounters.EventsInQueueCurrent.RawValue = num;
			if (num + count >= (long)Configuration.MaximumEventQueueSize && num < (long)Configuration.MaximumEventQueueSize)
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Number of events queued is below maximum.", this);
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long, long>((long)this.GetHashCode(), "{0}: Decremented numberEventsInQueueCurrent from {1} to {2}", this, num + count, num);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A654 File Offset: 0x00008854
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableEventController queryableEventController = queryableObject as QueryableEventController;
			if (queryableEventController != null)
			{
				queryableEventController.ShutdownState = ((EventController.ShutdownState)this.shutdownState).ToString();
				queryableEventController.TimeToSaveWatermarks = this.timeToSaveWatermarks;
				queryableEventController.HighestEventPolled = this.highestEventPolled;
				queryableEventController.NumberEventsInQueueCurrent = this.numberEventsInQueueCurrent;
				queryableEventController.RestartRequired = this.RestartRequired;
				QueryableThrottleGovernor queryableObject2 = new QueryableThrottleGovernor();
				this.governor.ExportToQueryableObject(queryableObject2);
				queryableEventController.Governor = queryableObject2;
				if (this.filter != null)
				{
					queryableEventController.EventFilter = this.filter.ToString();
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A6EC File Offset: 0x000088EC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.assistants != null)
				{
					this.assistants.Dispose();
				}
				this.eventAccess.Dispose();
				if (this.timer != null)
				{
					this.timer.Dispose();
				}
				this.governor.Dispose();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A738 File Offset: 0x00008938
		protected virtual void InitializeEventDispatchers(Btree<Guid, Bookmark> allBookmarks)
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A73A File Offset: 0x0000893A
		protected virtual void WaitUntilStoppedInternal()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A73C File Offset: 0x0000893C
		protected virtual void PeriodicMaintenance()
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A73E File Offset: 0x0000893E
		protected virtual void DisposeOfIdleDispatchers()
		{
		}

		// Token: 0x060001FE RID: 510
		protected abstract void ProcessPolledEvent(MapiEvent mapiEvent);

		// Token: 0x060001FF RID: 511
		protected abstract void UpdateWatermarksForAssistant(Guid assistantId);

		// Token: 0x06000200 RID: 512 RVA: 0x0000A740 File Offset: 0x00008940
		private void WaitUntilAssistantsStopped()
		{
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Waiting for timer routine to exit...", this);
			lock (this.timerLock)
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Timer routine is clear", this);
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Waiting for poller to stop...", this);
			base.TracePfd("PFD AIS {0} {1}: Poller has stopped.", new object[]
			{
				23639,
				this
			});
			this.WaitUntilStoppedInternal();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A848 File Offset: 0x00008A48
		private void TimerRoutine(object stateNotUsed)
		{
			using (ExPerfTrace.RelatedActivity(this.pollingActivityId))
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: TimerRoutine", this);
				if (!Monitor.TryEnter(this.timerLock))
				{
					ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: timer already busy", this);
				}
				else
				{
					try
					{
						long timestamp = Stopwatch.GetTimestamp();
						if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
						{
							ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(3443928381U, ref timestamp);
							long rawValue = 0L;
							long rawValue2 = 0L;
							ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(2764451133U, ref rawValue);
							ExTraceGlobals.FaultInjectionTracer.TraceTest<long>(3838192957U, ref rawValue2);
							this.DatabaseCounters.EventsInQueueCurrent.RawValue = rawValue2;
							this.DatabaseCounters.ElapsedTimeSinceLastEventPolled.RawValue = rawValue;
							ExTraceGlobals.FaultInjectionTracer.TraceTest(3703975229U);
						}
						this.DatabaseCounters.ElapsedTimeSinceLastEventPollingAttempt.RawValue = timestamp;
						bool noMoreEvents = false;
						while (this.ReadyToPoll() && !noMoreEvents)
						{
							try
							{
								base.CatchMeIfYouCan(delegate
								{
									noMoreEvents = this.PollAndQueueEvents();
								}, "EventController");
							}
							catch (AIException ex)
							{
								ExTraceGlobals.EventControllerTracer.TraceError<EventController, AIException>((long)this.GetHashCode(), "{0}: Exception while polling: {1}", this, ex);
								this.governor.ReportResult(ex);
							}
							this.PeriodicMaintenance();
						}
						ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Out of polling loop", this);
						if (!this.Shutdown && this.RestartRequired)
						{
							ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Restart required; stopping...", this);
							RpcHangDetector rpcHangDetector = RpcHangDetector.Create();
							rpcHangDetector.InvokeUnderHangDetection(delegate(HangDetector hangDetector)
							{
								AIBreadcrumbs.StatusTrail.Drop("Restart required, stopping.");
								this.RequestStop(rpcHangDetector);
								AIBreadcrumbs.StatusTrail.Drop("Exiting stop due to restart.");
							});
						}
						if (!this.Shutdown && this.governor.Status != GovernorStatus.Failure && this.timeToSaveWatermarks < DateTime.UtcNow)
						{
							ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Time to update watermarks...", this);
							AIBreadcrumbs.StatusTrail.Drop("Begin Update Watermarks");
							this.UpdateWatermarks();
							AIBreadcrumbs.StatusTrail.Drop("End Update Watermarks");
							ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Updated watermarks.", this);
							this.timeToSaveWatermarks = DateTime.UtcNow + Configuration.ActiveWatermarksSaveInterval;
							ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, DateTime>((long)this.GetHashCode(), "{0}: Next watermark update: {1}", this, this.timeToSaveWatermarks);
						}
					}
					finally
					{
						Monitor.Exit(this.timerLock);
					}
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AB40 File Offset: 0x00008D40
		private bool ReadyToPoll()
		{
			if (this.Shutdown || this.governor.Status == GovernorStatus.Failure || this.RestartRequired)
			{
				return false;
			}
			long num = Interlocked.Read(ref this.numberEventsInQueueCurrent);
			return num < (long)Configuration.MaximumEventQueueSize;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AB84 File Offset: 0x00008D84
		private bool PollAndQueueEvents()
		{
			long num = this.highestEventPolled + 1L;
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long>((long)this.GetHashCode(), "{0}: ReadEvents({1},...)", this, num);
			long num2;
			MapiEvent[] array;
			try
			{
				array = this.eventAccess.ReadEvents(num, 100, 10000, this.filter, out num2);
			}
			catch (MapiExceptionNotFound innerException)
			{
				throw new DatabaseIneptException(innerException);
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, int, long>((long)this.GetHashCode(), "{0}: Processing {1} polled events.  Endcounter: {2}", this, array.Length, num2);
			this.DatabaseCounters.EventsPolled.IncrementBy((long)array.Length);
			TimeSpan timeSpan = (array.Length <= 0) ? TimeSpan.Zero : (DateTime.UtcNow - array[array.Length - 1].CreateTime);
			this.DatabaseCounters.PollingDelay.RawValue = (long)timeSpan.TotalSeconds;
			for (int i = 0; i < array.Length; i++)
			{
				if (this.IsSearchFolderEvent(array[i]))
				{
					ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, MapiEvent>((long)this.GetHashCode(), "{0}: Ignoring search folder event: {1}", this, array[i]);
				}
				else
				{
					this.ProcessPolledEvent(array[i]);
				}
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long, long>((long)this.GetHashCode(), "{0}: Updating highest event polled from {1} to {2}", this, this.highestEventPolled, array[i].EventCounter);
				this.HighestEventPolled = array[i].EventCounter;
			}
			if (array.Length > 0 || num2 > num)
			{
				long arg = Math.Max(this.highestEventPolled, num2);
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController, long, long>((long)this.GetHashCode(), "{0}: Endcounter updating highest event polled from {1} to {2}", this, this.highestEventPolled, arg);
				this.HighestEventPolled = arg;
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Processed events.", this);
			return array.Length == 0 && num2 <= num;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		private void UpdateWatermarks()
		{
			if (this.RestartRequired)
			{
				ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Not updating watermarks because restart is required", this);
				return;
			}
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					foreach (AssistantCollectionEntry assistantCollectionEntry in this.Assistants)
					{
						this.UpdateWatermarksForAssistant(assistantCollectionEntry.Identity);
					}
					if (!this.Shutdown)
					{
						this.DisposeOfIdleDispatchers();
					}
					if (Test.NotifyAllWatermarksCommitted != null)
					{
						Test.NotifyAllWatermarksCommitted();
					}
				}, "EventController");
			}
			catch (AIException arg)
			{
				ExTraceGlobals.EventControllerTracer.TraceError<EventController, AIException>((long)this.GetHashCode(), "{0}: failed to save high watermark due to exception: {1}", this, arg);
				return;
			}
			ExTraceGlobals.EventControllerTracer.TraceDebug<EventController>((long)this.GetHashCode(), "{0}: Updated watermarks successfully", this);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AE38 File Offset: 0x00009038
		private bool IsSearchFolderEvent(MapiEvent notification)
		{
			return (notification.EventFlags & MapiEventFlags.SearchFolder) != MapiEventFlags.None;
		}

		// Token: 0x04000177 RID: 375
		private const string EventControlerName = "EventController";

		// Token: 0x04000178 RID: 376
		private const int MaximumEventsToCheckPerPoll = 10000;

		// Token: 0x04000179 RID: 377
		private const int NumberOfEventsPerPoll = 100;

		// Token: 0x0400017A RID: 378
		private const int MaxAssistantsToRemove = 1;

		// Token: 0x0400017B RID: 379
		private static readonly TimeSpan sleepStartingThread = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400017C RID: 380
		private readonly Guid pollingActivityId = Guid.NewGuid();

		// Token: 0x0400017D RID: 381
		private PerformanceCountersPerDatabaseInstance databaseCounters;

		// Token: 0x0400017E RID: 382
		private EventBasedAssistantCollection assistants;

		// Token: 0x0400017F RID: 383
		private DateTime timeToSaveWatermarks = DateTime.MinValue;

		// Token: 0x04000180 RID: 384
		private long numberEventsInQueueCurrent;

		// Token: 0x04000181 RID: 385
		private PoisonEventControl poisonControl;

		// Token: 0x04000182 RID: 386
		private string toString;

		// Token: 0x04000183 RID: 387
		private DatabaseInfo databaseInfo;

		// Token: 0x04000184 RID: 388
		private ThrottleGovernor governor;

		// Token: 0x04000185 RID: 389
		private Restriction filter;

		// Token: 0x04000186 RID: 390
		private EventAccess eventAccess;

		// Token: 0x04000187 RID: 391
		private Timer timer;

		// Token: 0x04000188 RID: 392
		private object timerLock = new object();

		// Token: 0x04000189 RID: 393
		private object watermarkUpdateLock = new object();

		// Token: 0x0400018A RID: 394
		private long highestEventPolled;

		// Token: 0x0400018B RID: 395
		private int shutdownState;

		// Token: 0x02000039 RID: 57
		private enum ShutdownState
		{
			// Token: 0x0400018E RID: 398
			NotRequested,
			// Token: 0x0400018F RID: 399
			InProgress,
			// Token: 0x04000190 RID: 400
			Completed
		}
	}
}
