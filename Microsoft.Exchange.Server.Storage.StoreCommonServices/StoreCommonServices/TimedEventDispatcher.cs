using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200014C RID: 332
	internal sealed class TimedEventDispatcher : ITimedEventHandler
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00040B93 File Offset: 0x0003ED93
		internal static IDictionary<Guid, ITimedEventHandler> DispatchMap
		{
			get
			{
				return TimedEventDispatcher.dispatchMap;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00040B9C File Offset: 0x0003ED9C
		public static void RegisterHandler(Guid eventSource, ITimedEventHandler handler)
		{
			if (!TimedEventDispatcher.dispatchMap.ContainsKey(eventSource))
			{
				TimedEventDispatcher.dispatchMap.Add(eventSource, handler);
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug<string, string>(37192L, "Handler registered, source={0}, handler={1}", eventSource.ToString(), handler.GetType().ToString());
				}
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00040BFC File Offset: 0x0003EDFC
		public static void UnregisterHandler(Guid eventSource)
		{
			if (TimedEventDispatcher.dispatchMap.ContainsKey(eventSource))
			{
				TimedEventDispatcher.dispatchMap.Remove(eventSource);
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug<string>(53576L, "Handler unregistered, source={0}", eventSource.ToString());
				}
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00040C54 File Offset: 0x0003EE54
		public void Invoke(Context context, TimedEventEntry timedEvent)
		{
			ITimedEventHandler timedEventHandler = null;
			if (!TimedEventDispatcher.dispatchMap.TryGetValue(timedEvent.EventSource, out timedEventHandler))
			{
				TimedEventDispatcher.defaultHandler.Invoke(context, timedEvent);
				return;
			}
			if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.TimedEventsTracer.TraceDebug(41288L, "Event=[" + timedEvent.ToString() + "] handler=" + timedEventHandler.GetType().ToString());
			}
			timedEventHandler.Invoke(context, timedEvent);
		}

		// Token: 0x04000733 RID: 1843
		private static readonly ITimedEventHandler defaultHandler = new TimedEventDispatcher.TimedEventDefaultHandler();

		// Token: 0x04000734 RID: 1844
		private static IDictionary<Guid, ITimedEventHandler> dispatchMap = new LockFreeDictionary<Guid, ITimedEventHandler>();

		// Token: 0x0200014D RID: 333
		private sealed class TimedEventDefaultHandler : ITimedEventHandler
		{
			// Token: 0x06000CE1 RID: 3297 RVA: 0x00040CE7 File Offset: 0x0003EEE7
			public void Invoke(Context context, TimedEventEntry timedEvent)
			{
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug<string>(61768L, "TimedEvent {0} has no registered handler", timedEvent.ToString());
				}
			}
		}
	}
}
