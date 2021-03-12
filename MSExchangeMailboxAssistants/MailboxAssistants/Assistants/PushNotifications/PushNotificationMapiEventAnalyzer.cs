using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000211 RID: 529
	internal static class PushNotificationMapiEventAnalyzer
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x00074D95 File Offset: 0x00072F95
		internal static bool IsSubscriptionChangeEvent(IMapiEvent mapiEvent)
		{
			return mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && ObjectClass.IsOfClass(mapiEvent.ObjectClass, "Exchange.PushNotification.Subscription") && (mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectCreated) || mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectModified));
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00074DD4 File Offset: 0x00072FD4
		internal static bool IsIpmFolderContentChangeEvent(IMapiEvent mapiEvent)
		{
			return mapiEvent.ItemType == ObjectType.MAPI_FOLDER && ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPF.Note") && mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectModified) && mapiEvent.EventFlags.HasFlag(MapiEventFlags.ContentOnly) && mapiEvent.ExtendedEventFlags.Contains((MapiExtendedEventFlags)int.MinValue) && mapiEvent.ItemCount != -1L;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00074E48 File Offset: 0x00073048
		internal static bool IsNewMessageEvent(IMapiEvent mapiEvent)
		{
			return mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Note") && mapiEvent.EventMask.Contains(MapiEventTypeFlags.ObjectCreated) && !mapiEvent.EventFlags.HasFlag(MapiEventFlags.SearchFolder) && mapiEvent.ExtendedEventFlags.Contains((MapiExtendedEventFlags)int.MinValue);
		}
	}
}
