using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000868 RID: 2152
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class StoreObjectTypeClassifier
	{
		// Token: 0x06005108 RID: 20744 RVA: 0x001519A4 File Offset: 0x0014FBA4
		public static bool IsFolderObjectType(StoreObjectType storeObjectType)
		{
			switch (storeObjectType)
			{
			case StoreObjectType.Folder:
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
			case StoreObjectType.TasksFolder:
			case StoreObjectType.NotesFolder:
			case StoreObjectType.JournalFolder:
			case StoreObjectType.SearchFolder:
			case StoreObjectType.OutlookSearchFolder:
				break;
			default:
				if (storeObjectType != StoreObjectType.ShortcutFolder)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x001519E4 File Offset: 0x0014FBE4
		public static bool AlwaysReportRealType(StoreObjectType storeObjectType)
		{
			switch (storeObjectType)
			{
			case StoreObjectType.OofMessage:
			case StoreObjectType.ExternalOofMessage:
				break;
			default:
				if (storeObjectType != StoreObjectType.CalendarItemSeries)
				{
					return false;
				}
				break;
			}
			return true;
		}
	}
}
