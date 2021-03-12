using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000085 RID: 133
	internal static class MapiTimedEvents
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001FED9 File Offset: 0x0001E0D9
		internal static Guid EventSource
		{
			get
			{
				return MapiTimedEvents.eventSource;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		internal static void RaiseDeferredSendEvent(MapiContext context, DateTime eventTime, int mailboxNumber, ExchangeId folderId, ExchangeId messageId)
		{
			byte[] eventData = DeferredSendEvent.SerializeExtraData(folderId.ToLong(), messageId.ToLong());
			MapiTimedEvents.RaiseMapiTimedEvent(context, eventTime, new int?(mailboxNumber), MapiTimedEvents.EventType.DeferredSend, eventData);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001FF10 File Offset: 0x0001E110
		internal static void RaiseMapiTimedEvent(MapiContext context, DateTime eventTime, int? mailboxNumber, MapiTimedEvents.EventType eventType, byte[] eventData)
		{
			TimedEventEntry timedEvent = new TimedEventEntry(eventTime, mailboxNumber, MapiTimedEvents.EventSource, (int)eventType, eventData);
			context.RaiseTimedEvent(timedEvent);
		}

		// Token: 0x040002C1 RID: 705
		private static readonly Guid eventSource = new Guid("98F7D371-BC39-4C55-A9DD-E7DEA76EE738");

		// Token: 0x02000086 RID: 134
		internal enum EventType
		{
			// Token: 0x040002C3 RID: 707
			None,
			// Token: 0x040002C4 RID: 708
			DeferredSend
		}
	}
}
