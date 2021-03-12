using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EventPump : IDisposeTrackable, IDisposable
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x000311D0 File Offset: 0x0002F3D0
		internal EventPump(EventPumpManager eventPumpManager, string server, Guid mdbGuid)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.eventPumpManager = eventPumpManager;
			this.server = server;
			this.mdbGuid = mdbGuid;
			this.threadLimiter = new EventPumpThreadLimiter(this);
			StoreSession storeSession = null;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.exRpcAdmin = ExRpcAdmin.Create("Client=EventPump", server, null, null, null);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToCreateEventManager, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::Constructor. Failed to create EventPump.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToCreateEventManager, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::Constructor. Failed to create EventPump.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			StoreSession storeSession2 = null;
			bool flag2 = false;
			try
			{
				if (storeSession2 != null)
				{
					storeSession2.BeginMapiCall();
					storeSession2.BeginServerHealthCall();
					flag2 = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.mapiEventManager = MapiEventManager.Create(this.exRpcAdmin, Guid.Empty, this.mdbGuid);
			}
			catch (MapiPermanentException ex3)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToCreateEventManager, ex3, storeSession2, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::Constructor. Failed to create EventPump.", new object[0]),
					ex3
				});
			}
			catch (MapiRetryableException ex4)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExFailedToCreateEventManager, ex4, storeSession2, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::Constructor. Failed to create EventPump.", new object[0]),
					ex4
				});
			}
			finally
			{
				try
				{
					if (storeSession2 != null)
					{
						storeSession2.EndMapiCall();
						if (flag2)
						{
							storeSession2.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			this.lastEventCounter = this.ReadLastEventWatermark();
			this.RegisterMainPump(EventPump.PollingTimeSpan);
			ExTraceGlobals.EventTracer.TraceDebug<EventPump>((long)this.GetHashCode(), "EventPump::Constructor. {0}", this);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x000314C0 File Offset: 0x0002F6C0
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x000314C7 File Offset: 0x0002F6C7
		public static TimeSpan PollingTimeSpan
		{
			get
			{
				return EventPump.pollingTimeSpan;
			}
			set
			{
				EventPump.pollingTimeSpan = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000314CF File Offset: 0x0002F6CF
		internal Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000314D7 File Offset: 0x0002F6D7
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000314DF File Offset: 0x0002F6DF
		internal int ReferenceCount
		{
			get
			{
				return this.referenceCount;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000314E7 File Offset: 0x0002F6E7
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000314EF File Offset: 0x0002F6EF
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EventPump>(this);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000314F7 File Offset: 0x0002F6F7
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0003150C File Offset: 0x0002F70C
		public override string ToString()
		{
			return string.Format("Server = {0}. MdbGuid = {1}.", this.server, this.mdbGuid);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00031529 File Offset: 0x0002F729
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00031538 File Offset: 0x0002F738
		internal void AddEventSink(EventSink eventSink)
		{
			bool flag = false;
			try
			{
				if (eventSink.FirstMissedEventWaterMark != null)
				{
					long firstEventToConsumeOnSink = eventSink.FirstMissedEventWaterMark.WasEventProcessed ? (eventSink.FirstMissedEventWaterMark.MapiWatermark + 1L) : eventSink.FirstMissedEventWaterMark.MapiWatermark;
					eventSink.SetFirstEventToConsumeOnSink(firstEventToConsumeOnSink);
				}
				else
				{
					eventSink.SetFirstEventToConsumeOnSink(this.ReadLastEventWatermark() + 1L);
				}
				eventSink.SetLastKnownWatermark(this.lastEventCounter, false);
				this.ModifyEventSinkList(EventPump.ModifyEventSinkListType.AddEventSink, eventSink);
				eventSink.SetEventPump(this);
				flag = true;
			}
			finally
			{
				if (!flag && eventSink != null)
				{
					eventSink.Dispose();
					eventSink = null;
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000315D0 File Offset: 0x0002F7D0
		internal void RemoveEventSink(EventSink eventSink)
		{
			this.ModifyEventSinkList(EventPump.ModifyEventSinkListType.RemoveEventSink, eventSink);
			this.Release();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000315E0 File Offset: 0x0002F7E0
		internal void RequestRecovery(EventSink eventSink)
		{
			this.threadLimiter.RequestRecovery(eventSink);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000315F0 File Offset: 0x0002F7F0
		internal void ExecuteRecovery(EventSink eventSink)
		{
			ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Starting Recovery. EventSink = {1}.", this, eventSink);
			try
			{
				IRecoveryEventSink recoveryEventSink = null;
				EventWatermark eventWatermark = null;
				long num = 0L;
				Guid mailboxGuid;
				try
				{
					mailboxGuid = eventSink.MailboxGuid;
					recoveryEventSink = eventSink.StartRecovery();
					eventWatermark = recoveryEventSink.FirstMissedEventWatermark;
					num = recoveryEventSink.LastMissedEventWatermark;
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Executing recovery in a disposed sink. Aborting recovery. EventSink = {1}.", this, eventSink);
					return;
				}
				Restriction restriction = null;
				if (!eventSink.IsPublicFolderDatabase)
				{
					restriction = Restriction.EQ(PropTag.EventMailboxGuid, mailboxGuid.ToByteArray());
				}
				long num2 = eventWatermark.WasEventProcessed ? (eventWatermark.MapiWatermark + 1L) : eventWatermark.MapiWatermark;
				long num3 = num2;
				bool flag = true;
				while (flag)
				{
					int eventCountToCheck = EventPump.GetEventCountToCheck(num3, eventWatermark, num2, num);
					if (eventCountToCheck > 0)
					{
						MapiEvent[] array = null;
						long num4 = 0L;
						try
						{
							this.disposeLock.EnterReadLock();
							if (this.IsDisposed)
							{
								return;
							}
							array = EventPump.ReadEvents(this.mapiEventManager, this.mapiEventManagerLock, num3, eventCountToCheck, eventCountToCheck, restriction, out num4);
						}
						finally
						{
							try
							{
								this.disposeLock.ExitReadLock();
							}
							catch (SynchronizationLockException)
							{
							}
						}
						int num5 = 0;
						while (num5 < array.Length && flag)
						{
							MapiEvent mapiEvent = array[num5];
							if (EventPump.IsEventBetweenCounters(num3, num, mapiEvent.Watermark.EventCounter))
							{
								try
								{
									flag = recoveryEventSink.RecoveryConsume(mapiEvent);
									goto IL_179;
								}
								catch (ObjectDisposedException)
								{
									ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Executing recovery in a disposed sink. Aborting recovery. EventSink = {1}.", this, eventSink);
									return;
								}
								goto Block_9;
							}
							goto IL_150;
							IL_179:
							num5++;
							continue;
							Block_9:
							try
							{
								IL_150:
								recoveryEventSink.EndRecovery();
							}
							catch (ObjectDisposedException)
							{
								ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Executing recovery in a disposed sink. Aborting recovery. EventSink = {1}.", this, eventSink);
								return;
							}
							flag = false;
							goto IL_179;
						}
						num3 = num4 + 1L;
					}
					else
					{
						try
						{
							recoveryEventSink.EndRecovery();
						}
						catch (ObjectDisposedException)
						{
							ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Executing recovery in a disposed sink. Aborting recovery. EventSink = {1}.", this, eventSink);
							return;
						}
						flag = false;
					}
				}
			}
			catch (StoragePermanentException ex)
			{
				eventSink.HandleException(ex);
			}
			catch (StorageTransientException ex2)
			{
				eventSink.HandleException(ex2);
			}
			ExTraceGlobals.EventTracer.TraceDebug<EventPump, EventSink>((long)this.GetHashCode(), "EventPump::ExecuteRecovery. {0}. Exiting Recovery. EventSink = {1}.", this, eventSink);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000318B8 File Offset: 0x0002FAB8
		internal void VerifyWatermarkIsInEventTable(EventWatermark watermark)
		{
			if (watermark.MdbGuid != this.MdbGuid)
			{
				throw new InvalidEventWatermarkException(ServerStrings.ExInvalidEventWatermarkBadOrigin(watermark.MdbGuid, this.MdbGuid));
			}
			long num = 0L;
			EventPump.ReadEvents(this.mapiEventManager, this.mapiEventManagerLock, watermark.MapiWatermark, 1, 1, null, out num);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0003190F File Offset: 0x0002FB0F
		internal void AddRef()
		{
			Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00031920 File Offset: 0x0002FB20
		internal void Release()
		{
			if (Interlocked.Decrement(ref this.referenceCount) == 0)
			{
				this.eventPumpManager.RemoveEventPump(this);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00031948 File Offset: 0x0002FB48
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalStopPumpThread();
				this.exRpcAdmin.Dispose();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00031974 File Offset: 0x0002FB74
		private static MapiEvent[] ReadEvents(MapiEventManager mapiEventManager, object mapiEventManagerLock, long startCounter, int eventCountWanted, int eventCountToCheck, Restriction restriction, out long endCounter)
		{
			long num = 0L;
			MapiEvent[] result = null;
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				lock (mapiEventManagerLock)
				{
					result = mapiEventManager.ReadEvents(startCounter, eventCountWanted, eventCountToCheck, restriction, ReadEventsFlags.FailIfEventsDeleted, false, out num);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCaughtMapiExceptionWhileReadingEvents, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::ReadEvents. Caught MapiException while reading events.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCaughtMapiExceptionWhileReadingEvents, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::ReadEvents. Caught MapiException while reading events.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			endCounter = num;
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00031ACC File Offset: 0x0002FCCC
		private static void ReadAndDistributeEvents(object state, bool timedOut)
		{
			WeakReference weakReference = (WeakReference)state;
			EventPump eventPump = (EventPump)weakReference.Target;
			if (weakReference.IsAlive && timedOut)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				Dictionary<Guid, List<EventSink>> eventSinkDictionary = eventPump.eventSinks;
				bool flag = eventPump.ReadAndDistributeEvents(eventSinkDictionary);
				if (flag)
				{
					TimeSpan timeSpan = ExDateTime.UtcNow - utcNow;
					TimeSpan timeSpan2 = TimeSpan.Zero;
					if (timeSpan < EventPump.PollingTimeSpan)
					{
						timeSpan2 = EventPump.PollingTimeSpan - timeSpan;
					}
					eventPump.RegisterMainPump(timeSpan2);
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00031B4C File Offset: 0x0002FD4C
		private static int GetEventCountToCheck(long startCounter, EventWatermark firstMissedEventWatermark, long firstMissedEventCounter, long lastMissedEventCounter)
		{
			if (firstMissedEventWatermark.WasEventProcessed && firstMissedEventWatermark.MapiWatermark == lastMissedEventCounter)
			{
				return 0;
			}
			if (!EventPump.IsEventBetweenCounters(firstMissedEventCounter, lastMissedEventCounter, startCounter))
			{
				return 0;
			}
			ulong num = (ulong)(lastMissedEventCounter - startCounter);
			int result;
			if (num >= 1000UL)
			{
				result = 1000;
			}
			else
			{
				result = (int)(num + 1UL);
			}
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00031B98 File Offset: 0x0002FD98
		private static bool IsEventBetweenCounters(long firstCounter, long lastCounter, long eventCounter)
		{
			return eventCounter >= firstCounter && eventCounter <= lastCounter;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00031BA8 File Offset: 0x0002FDA8
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				if (disposing && this.disposeLock != null)
				{
					try
					{
						this.disposeLock.EnterWriteLock();
						this.isDisposed = true;
					}
					finally
					{
						try
						{
							this.disposeLock.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
				this.InternalDispose(disposing);
				ExTraceGlobals.EventTracer.TraceDebug<EventPump>((long)this.GetHashCode(), "EventPump::Dispose. {0}", this);
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00031C34 File Offset: 0x0002FE34
		private void ModifyEventSinkList(EventPump.ModifyEventSinkListType modificationType, EventSink eventSink)
		{
			lock (this.sinkListLock)
			{
				this.isModifyingEventSinkList = true;
				try
				{
					Dictionary<Guid, List<EventSink>> dictionary = this.eventSinks;
					if (modificationType == EventPump.ModifyEventSinkListType.AddEventSink)
					{
						List<EventSink> collection = null;
						List<EventSink> list;
						if (dictionary.TryGetValue(eventSink.MailboxGuid, out collection))
						{
							list = new List<EventSink>(collection);
						}
						else
						{
							list = new List<EventSink>();
						}
						list.Add(eventSink);
						Dictionary<Guid, List<EventSink>> dictionary2 = dictionary.ShallowCopy<Guid, List<EventSink>>();
						dictionary2[eventSink.MailboxGuid] = list;
						this.eventSinks = dictionary2;
						if (this.exception != null)
						{
							eventSink.HandleException(this.exception);
						}
					}
					else
					{
						List<EventSink> collection2 = dictionary[eventSink.MailboxGuid];
						List<EventSink> list2 = new List<EventSink>(collection2);
						list2.Remove(eventSink);
						Dictionary<Guid, List<EventSink>> dictionary3 = dictionary.ShallowCopy<Guid, List<EventSink>>();
						if (list2.Count == 0)
						{
							dictionary3.Remove(eventSink.MailboxGuid);
						}
						else
						{
							dictionary3[eventSink.MailboxGuid] = list2;
						}
						this.eventSinks = dictionary3;
					}
				}
				finally
				{
					this.isModifyingEventSinkList = false;
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00031D4C File Offset: 0x0002FF4C
		private void CrashPump(LocalizedException exception)
		{
			Dictionary<Guid, List<EventSink>> dictionary = null;
			lock (this.sinkListLock)
			{
				this.exception = exception;
				dictionary = this.eventSinks;
			}
			ExTraceGlobals.EventTracer.TraceDebug<EventPump, Exception>((long)this.GetHashCode(), "EventPump::CrashPump. {0}. We got an error while reading events on the current EventPump. The EventPump has been disabled. Error = {1}.", this, this.exception);
			foreach (List<EventSink> list in dictionary.Values)
			{
				foreach (EventSink eventSink in list)
				{
					eventSink.HandleException(exception);
				}
			}
			this.eventPumpManager.RemoveBrokenEventPump(this);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00031E3C File Offset: 0x0003003C
		private bool ReadAndDistributeEvents(Dictionary<Guid, List<EventSink>> eventSinkDictionary)
		{
			long num = this.lastEventCounter;
			bool flag = false;
			int num2 = 0;
			while (!flag)
			{
				MapiEvent[] array = null;
				long num3 = this.lastEventCounter + 1L;
				long num4 = 0L;
				try
				{
					try
					{
						this.disposeLock.EnterReadLock();
						if (this.IsDisposed)
						{
							return false;
						}
						array = EventPump.ReadEvents(this.mapiEventManager, this.mapiEventManagerLock, num3, 1000, 2000, null, out num4);
					}
					finally
					{
						try
						{
							this.disposeLock.ExitReadLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
				catch (StoragePermanentException arg)
				{
					ExTraceGlobals.EventTracer.TraceDebug<EventPump, StoragePermanentException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught while reading events. Exception = {1}.", this, arg);
					this.CrashPump(arg);
					return false;
				}
				catch (StorageTransientException arg2)
				{
					ExTraceGlobals.EventTracer.TraceDebug<EventPump, StorageTransientException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught while reading events. Exception = {1}.", this, arg2);
					this.CrashPump(arg2);
					return false;
				}
				foreach (MapiEvent mapiEvent in array)
				{
					List<EventSink> list = null;
					this.lastEventCounter = mapiEvent.EventCounter;
					if (eventSinkDictionary.TryGetValue(mapiEvent.MailboxGuid, out list))
					{
						foreach (EventSink eventSink in list)
						{
							try
							{
								eventSink.Consume(mapiEvent);
							}
							catch (StoragePermanentException arg3)
							{
								ExTraceGlobals.EventTracer.TraceDebug<EventPump, StoragePermanentException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught while distributing events. Exception = {1}.", this, arg3);
								eventSink.HandleException(arg3);
							}
							catch (StorageTransientException arg4)
							{
								ExTraceGlobals.EventTracer.TraceDebug<EventPump, StorageTransientException>((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Exception caught while distributing events. Exception = {1}.", this, arg4);
								eventSink.HandleException(arg4);
							}
						}
					}
				}
				num2 += array.Length;
				flag = (array.Length == 0 && num3 >= num4);
				if (flag && num3 == num4)
				{
					this.lastEventCounter = num4 - 1L;
					continue;
				}
				this.lastEventCounter = num4;
			}
			foreach (List<EventSink> list2 in eventSinkDictionary.Values)
			{
				foreach (EventSink eventSink2 in list2)
				{
					eventSink2.SetLastKnownWatermark(this.lastEventCounter, true);
				}
			}
			ExTraceGlobals.EventTracer.TraceDebug((long)this.GetHashCode(), "EventPump::ReadAndDistributeEvents. {0}. Events processed = {1}. PreviousLastEventCounter = {2}. LastEventCounter = {3}.", new object[]
			{
				this,
				num2,
				num,
				this.lastEventCounter
			});
			return true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00032140 File Offset: 0x00030340
		private long ReadLastEventWatermark()
		{
			StoreSession storeSession = null;
			bool flag = false;
			long eventCounter;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				ExDateTime utcNow = ExDateTime.UtcNow;
				TimeSpan? arg = null;
				try
				{
					if (!Monitor.TryEnter(this.mapiEventManagerLock, EventPump.eventSinkCreationTimeout))
					{
						ExTraceGlobals.EventTracer.TraceDebug<EventPump, TimeSpan>((long)this.GetHashCode(), "EventPump::ReadLastEventWatermark {0}. Could not get MapiEventManager lock after {1} timeout", this, EventPump.eventSinkCreationTimeout);
						throw new CannotCompleteOperationException(ServerStrings.ExReadEventsFailed);
					}
					arg = new TimeSpan?(ExDateTime.UtcNow - utcNow);
					eventCounter = this.mapiEventManager.ReadLastEvent(false).EventCounter;
				}
				finally
				{
					if (Monitor.IsEntered(this.mapiEventManagerLock))
					{
						Monitor.Exit(this.mapiEventManagerLock);
						ExTraceGlobals.EventTracer.TraceDebug<EventPump, TimeSpan?>((long)this.GetHashCode(), "EventPump::ReadLastEventWatermark {0}. Got MapiEventManager lock in {1}", this, arg);
					}
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCaughtMapiExceptionWhileReadingEvents, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::ReadLastEventWatermark. Failed to read current watermark.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCaughtMapiExceptionWhileReadingEvents, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("EventPump::ReadLastEventWatermark. Failed to read current watermark.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return eventCounter;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0003233C File Offset: 0x0003053C
		private void RegisterMainPump(TimeSpan timeSpan)
		{
			if (!this.isDisposed)
			{
				this.registeredPumpDisposedWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.pumpDisposedEvent, new WaitOrTimerCallback(EventPump.ReadAndDistributeEvents), new WeakReference(this), timeSpan, true);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0003236B File Offset: 0x0003056B
		private void InternalStopPumpThread()
		{
			this.registeredPumpDisposedWaitHandle.Unregister(this.pumpDisposedEvent);
		}

		// Token: 0x040001A6 RID: 422
		private const int EventCount = 1000;

		// Token: 0x040001A7 RID: 423
		private const int EventCountToCheck = 2000;

		// Token: 0x040001A8 RID: 424
		private readonly EventPumpManager eventPumpManager;

		// Token: 0x040001A9 RID: 425
		private readonly AutoResetEvent pumpDisposedEvent = new AutoResetEvent(false);

		// Token: 0x040001AA RID: 426
		private readonly EventPumpThreadLimiter threadLimiter;

		// Token: 0x040001AB RID: 427
		private readonly string server;

		// Token: 0x040001AC RID: 428
		private readonly Guid mdbGuid;

		// Token: 0x040001AD RID: 429
		private readonly ExRpcAdmin exRpcAdmin;

		// Token: 0x040001AE RID: 430
		private readonly MapiEventManager mapiEventManager;

		// Token: 0x040001AF RID: 431
		private readonly object mapiEventManagerLock = new object();

		// Token: 0x040001B0 RID: 432
		private readonly ReaderWriterLockSlim disposeLock = new ReaderWriterLockSlim();

		// Token: 0x040001B1 RID: 433
		private readonly object sinkListLock = new object();

		// Token: 0x040001B2 RID: 434
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040001B3 RID: 435
		private static TimeSpan pollingTimeSpan = new TimeSpan(0, 0, 2);

		// Token: 0x040001B4 RID: 436
		private static TimeSpan eventSinkCreationTimeout = new TimeSpan(0, 0, 10);

		// Token: 0x040001B5 RID: 437
		private bool isDisposed;

		// Token: 0x040001B6 RID: 438
		private int referenceCount;

		// Token: 0x040001B7 RID: 439
		private long lastEventCounter;

		// Token: 0x040001B8 RID: 440
		private Exception exception;

		// Token: 0x040001B9 RID: 441
		private Dictionary<Guid, List<EventSink>> eventSinks = new Dictionary<Guid, List<EventSink>>();

		// Token: 0x040001BA RID: 442
		private bool isModifyingEventSinkList;

		// Token: 0x040001BB RID: 443
		private RegisteredWaitHandle registeredPumpDisposedWaitHandle;

		// Token: 0x02000050 RID: 80
		private enum ModifyEventSinkListType
		{
			// Token: 0x040001BD RID: 445
			AddEventSink,
			// Token: 0x040001BE RID: 446
			RemoveEventSink
		}
	}
}
