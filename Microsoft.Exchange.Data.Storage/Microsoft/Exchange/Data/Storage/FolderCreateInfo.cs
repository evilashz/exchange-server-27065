using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007A3 RID: 1955
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderCreateInfo
	{
		// Token: 0x060049D0 RID: 18896 RVA: 0x00134F90 File Offset: 0x00133190
		private FolderCreateInfo(StoreObjectType folderType, string containerClass, FolderSchema schema, FolderCreateInfo.FolderCreator creator)
		{
			this.FolderType = folderType;
			this.ContainerClass = containerClass;
			this.Schema = schema;
			this.Creator = creator;
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x00134FB5 File Offset: 0x001331B5
		private static SearchFolder SearchFolderCreator(CoreFolder coreFolder)
		{
			return new SearchFolder(coreFolder);
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x00134FBD File Offset: 0x001331BD
		private static OutlookSearchFolder OutlookSearchFolderCreator(CoreFolder coreFolder)
		{
			return new OutlookSearchFolder(coreFolder);
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x00134FC5 File Offset: 0x001331C5
		private static Folder GenericFolderCreator(CoreFolder coreFolder)
		{
			return new Folder(coreFolder);
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x00134FCD File Offset: 0x001331CD
		private static ContactsFolder ContactsFolderCreator(CoreFolder coreFolder)
		{
			return new ContactsFolder(coreFolder);
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x00134FD5 File Offset: 0x001331D5
		private static CalendarFolder CalendarFolderCreator(CoreFolder coreFolder)
		{
			return new CalendarFolder(coreFolder);
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x00134FDD File Offset: 0x001331DD
		internal static FolderCreateInfo GetFolderCreateInfo(StoreObjectType folderType)
		{
			return FolderCreateInfo.folderCreateInfoDictionary[folderType];
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x00134FEC File Offset: 0x001331EC
		private static Dictionary<StoreObjectType, FolderCreateInfo> CreateFolderCreateInfoDictionary()
		{
			FolderCreateInfo[] array = new FolderCreateInfo[]
			{
				FolderCreateInfo.SearchFolderInfo,
				FolderCreateInfo.OutlookSearchFolderInfo,
				FolderCreateInfo.CalendarFolderInfo,
				FolderCreateInfo.ContactsFolderInfo,
				FolderCreateInfo.TasksFolderInfo,
				FolderCreateInfo.JournalFolderInfo,
				FolderCreateInfo.NotesFolderInfo,
				FolderCreateInfo.ShortcutFolderInfo,
				FolderCreateInfo.GenericFolderInfo
			};
			Dictionary<StoreObjectType, FolderCreateInfo> dictionary = new Dictionary<StoreObjectType, FolderCreateInfo>(new StoreObjectTypeComparer());
			foreach (FolderCreateInfo folderCreateInfo in array)
			{
				dictionary.Add(folderCreateInfo.FolderType, folderCreateInfo);
			}
			return dictionary;
		}

		// Token: 0x040027BF RID: 10175
		internal readonly string ContainerClass;

		// Token: 0x040027C0 RID: 10176
		internal readonly FolderSchema Schema;

		// Token: 0x040027C1 RID: 10177
		internal readonly FolderCreateInfo.FolderCreator Creator;

		// Token: 0x040027C2 RID: 10178
		internal readonly StoreObjectType FolderType;

		// Token: 0x040027C3 RID: 10179
		private static readonly FolderCreateInfo OutlookSearchFolderInfo = new FolderCreateInfo(StoreObjectType.OutlookSearchFolder, "IPF.Note", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.OutlookSearchFolderCreator));

		// Token: 0x040027C4 RID: 10180
		private static readonly FolderCreateInfo CalendarFolderInfo = new FolderCreateInfo(StoreObjectType.CalendarFolder, "IPF.Appointment", CalendarFolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.CalendarFolderCreator));

		// Token: 0x040027C5 RID: 10181
		private static readonly FolderCreateInfo ContactsFolderInfo = new FolderCreateInfo(StoreObjectType.ContactsFolder, "IPF.Contact", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.ContactsFolderCreator));

		// Token: 0x040027C6 RID: 10182
		private static readonly FolderCreateInfo TasksFolderInfo = new FolderCreateInfo(StoreObjectType.TasksFolder, "IPF.Task", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.GenericFolderCreator));

		// Token: 0x040027C7 RID: 10183
		private static readonly FolderCreateInfo JournalFolderInfo = new FolderCreateInfo(StoreObjectType.JournalFolder, "IPF.Journal", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.GenericFolderCreator));

		// Token: 0x040027C8 RID: 10184
		private static readonly FolderCreateInfo NotesFolderInfo = new FolderCreateInfo(StoreObjectType.NotesFolder, "IPF.StickyNote", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.GenericFolderCreator));

		// Token: 0x040027C9 RID: 10185
		private static readonly FolderCreateInfo ShortcutFolderInfo = new FolderCreateInfo(StoreObjectType.ShortcutFolder, "IPF.ShortcutFolder", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.GenericFolderCreator));

		// Token: 0x040027CA RID: 10186
		internal static readonly FolderCreateInfo SearchFolderInfo = new FolderCreateInfo(StoreObjectType.SearchFolder, "IPF.Note", FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.SearchFolderCreator));

		// Token: 0x040027CB RID: 10187
		internal static readonly FolderCreateInfo GenericFolderInfo = new FolderCreateInfo(StoreObjectType.Folder, null, FolderSchema.Instance, new FolderCreateInfo.FolderCreator(FolderCreateInfo.GenericFolderCreator));

		// Token: 0x040027CC RID: 10188
		private static readonly Dictionary<StoreObjectType, FolderCreateInfo> folderCreateInfoDictionary = FolderCreateInfo.CreateFolderCreateInfoDictionary();

		// Token: 0x020007A4 RID: 1956
		// (Invoke) Token: 0x060049DA RID: 18906
		internal delegate Folder FolderCreator(CoreFolder coreFolder);
	}
}
