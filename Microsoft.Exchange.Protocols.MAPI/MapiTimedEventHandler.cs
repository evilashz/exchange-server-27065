using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000083 RID: 131
	internal sealed class MapiTimedEventHandler : ITimedEventHandler
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public void Invoke(Context context, TimedEventEntry timedEvent)
		{
			ITimedEventHandler timedEventHandler = null;
			if (MapiTimedEventHandler.eventHandlerMap.TryGetValue((MapiTimedEvents.EventType)timedEvent.EventType, out timedEventHandler))
			{
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug(45384L, string.Concat(new object[]
					{
						"Event type=",
						timedEvent.EventType,
						" handler=",
						timedEventHandler.GetType().Name
					}));
				}
				timedEventHandler.Invoke(context, timedEvent);
				return;
			}
			MapiTimedEventHandler.defaultHandler.Invoke(context, timedEvent);
		}

		// Token: 0x040002BF RID: 703
		private static readonly Dictionary<MapiTimedEvents.EventType, ITimedEventHandler> eventHandlerMap = new Dictionary<MapiTimedEvents.EventType, ITimedEventHandler>
		{
			{
				MapiTimedEvents.EventType.DeferredSend,
				new DeferredSendHandler()
			}
		};

		// Token: 0x040002C0 RID: 704
		private static readonly ITimedEventHandler defaultHandler = new MapiTimedEventHandler.TimedEventDefaultHandler();

		// Token: 0x02000084 RID: 132
		private sealed class TimedEventDefaultHandler : ITimedEventHandler
		{
			// Token: 0x0600048A RID: 1162 RVA: 0x0001FEA7 File Offset: 0x0001E0A7
			public void Invoke(Context context, TimedEventEntry timedEvent)
			{
				if (ExTraceGlobals.TimedEventsTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TimedEventsTracer.TraceDebug<string>(51528L, "Mapi Timed event {0} does not have registered handler", timedEvent.ToString());
				}
			}
		}
	}
}
