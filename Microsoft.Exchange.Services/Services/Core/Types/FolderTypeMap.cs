using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000790 RID: 1936
	internal static class FolderTypeMap
	{
		// Token: 0x060039AF RID: 14767 RVA: 0x000CB844 File Offset: 0x000C9A44
		static FolderTypeMap()
		{
			FolderTypeMap.displayNameToStoreObjectTypeMap.Add("Folder", StoreObjectType.Folder);
			FolderTypeMap.displayNameToStoreObjectTypeMap.Add("CalendarFolder", StoreObjectType.CalendarFolder);
			FolderTypeMap.displayNameToStoreObjectTypeMap.Add("ContactsFolder", StoreObjectType.ContactsFolder);
			FolderTypeMap.displayNameToStoreObjectTypeMap.Add("SearchFolder", StoreObjectType.SearchFolder);
			FolderTypeMap.displayNameToStoreObjectTypeMap.Add("TasksFolder", StoreObjectType.TasksFolder);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000CB8AC File Offset: 0x000C9AAC
		public static StoreObjectType FolderTypeToStoreObjectType(string folderElementName)
		{
			StoreObjectType result = StoreObjectType.Unknown;
			if (!FolderTypeMap.displayNameToStoreObjectTypeMap.TryGetValue(folderElementName, out result))
			{
				return StoreObjectType.Folder;
			}
			return result;
		}

		// Token: 0x0400201A RID: 8218
		private static Dictionary<string, StoreObjectType> displayNameToStoreObjectTypeMap = new Dictionary<string, StoreObjectType>();
	}
}
