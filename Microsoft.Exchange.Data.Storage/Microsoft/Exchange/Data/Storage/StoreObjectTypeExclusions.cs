using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000867 RID: 2151
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class StoreObjectTypeExclusions
	{
		// Token: 0x06005107 RID: 20743 RVA: 0x00151920 File Offset: 0x0014FB20
		public static bool E12KnownObjectType(StoreObjectType storeObjectType)
		{
			switch (storeObjectType)
			{
			case StoreObjectType.Unknown:
			case StoreObjectType.Folder:
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
			case StoreObjectType.TasksFolder:
			case StoreObjectType.NotesFolder:
			case StoreObjectType.JournalFolder:
			case StoreObjectType.SearchFolder:
			case StoreObjectType.OutlookSearchFolder:
			case StoreObjectType.Message:
			case StoreObjectType.MeetingMessage:
			case StoreObjectType.MeetingRequest:
			case StoreObjectType.MeetingResponse:
			case StoreObjectType.MeetingCancellation:
			case StoreObjectType.ConflictMessage:
			case StoreObjectType.CalendarItem:
			case StoreObjectType.CalendarItemOccurrence:
			case StoreObjectType.Contact:
			case StoreObjectType.DistributionList:
			case StoreObjectType.Task:
			case StoreObjectType.TaskRequest:
			case StoreObjectType.Note:
			case StoreObjectType.Post:
			case StoreObjectType.Report:
			case StoreObjectType.MeetingForwardNotification:
				break;
			default:
				if (storeObjectType != StoreObjectType.Mailbox)
				{
					return false;
				}
				break;
			}
			return true;
		}
	}
}
