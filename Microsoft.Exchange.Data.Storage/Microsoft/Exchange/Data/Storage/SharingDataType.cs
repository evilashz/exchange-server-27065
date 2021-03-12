using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC8 RID: 3528
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingDataType
	{
		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x0600795B RID: 31067 RVA: 0x00218BBD File Offset: 0x00216DBD
		// (set) Token: 0x0600795C RID: 31068 RVA: 0x00218BC5 File Offset: 0x00216DC5
		public string ContainerClass { get; private set; }

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x0600795D RID: 31069 RVA: 0x00218BCE File Offset: 0x00216DCE
		// (set) Token: 0x0600795E RID: 31070 RVA: 0x00218BD6 File Offset: 0x00216DD6
		public string ExternalName { get; private set; }

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x00218BDF File Offset: 0x00216DDF
		// (set) Token: 0x06007960 RID: 31072 RVA: 0x00218BE7 File Offset: 0x00216DE7
		public bool IsExternallySharable { get; private set; }

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x06007961 RID: 31073 RVA: 0x00218BF0 File Offset: 0x00216DF0
		// (set) Token: 0x06007962 RID: 31074 RVA: 0x00218BF8 File Offset: 0x00216DF8
		public string PublishName { get; private set; }

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x06007963 RID: 31075 RVA: 0x00218C01 File Offset: 0x00216E01
		// (set) Token: 0x06007964 RID: 31076 RVA: 0x00218C09 File Offset: 0x00216E09
		public string PublishResourceName { get; private set; }

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x06007965 RID: 31077 RVA: 0x00218C12 File Offset: 0x00216E12
		// (set) Token: 0x06007966 RID: 31078 RVA: 0x00218C1A File Offset: 0x00216E1A
		public LocalizedString DisplayName { get; private set; }

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x06007967 RID: 31079 RVA: 0x00218C23 File Offset: 0x00216E23
		// (set) Token: 0x06007968 RID: 31080 RVA: 0x00218C2B File Offset: 0x00216E2B
		internal DefaultFolderType DefaultFolderType { get; private set; }

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x06007969 RID: 31081 RVA: 0x00218C34 File Offset: 0x00216E34
		// (set) Token: 0x0600796A RID: 31082 RVA: 0x00218C3C File Offset: 0x00216E3C
		internal StoreObjectType StoreObjectType { get; private set; }

		// Token: 0x0600796B RID: 31083 RVA: 0x00218C48 File Offset: 0x00216E48
		public static SharingDataType FromContainerClass(string containerClass)
		{
			if (!string.IsNullOrEmpty(containerClass))
			{
				foreach (SharingDataType sharingDataType in SharingDataType.dataTypes)
				{
					if (ObjectClass.IsOfClass(containerClass, sharingDataType.ContainerClass))
					{
						return sharingDataType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x00218C8C File Offset: 0x00216E8C
		public static SharingDataType FromExternalName(string externalName)
		{
			if (!string.IsNullOrEmpty(externalName))
			{
				foreach (SharingDataType sharingDataType in SharingDataType.dataTypes)
				{
					if (StringComparer.InvariantCultureIgnoreCase.Equals(externalName, sharingDataType.ExternalName))
					{
						return sharingDataType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x00218CD4 File Offset: 0x00216ED4
		public static SharingDataType FromPublishName(string publishName)
		{
			if (!string.IsNullOrEmpty(publishName))
			{
				foreach (SharingDataType sharingDataType in SharingDataType.dataTypes)
				{
					if (StringComparer.InvariantCultureIgnoreCase.Equals(publishName, sharingDataType.PublishName))
					{
						return sharingDataType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x00218D1C File Offset: 0x00216F1C
		public static SharingDataType FromPublishResourceName(string publishResourceName)
		{
			if (!string.IsNullOrEmpty(publishResourceName))
			{
				foreach (SharingDataType sharingDataType in SharingDataType.publishDataTypes)
				{
					if (StringComparer.InvariantCultureIgnoreCase.Equals(publishResourceName, sharingDataType.PublishResourceName))
					{
						return sharingDataType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x00218D63 File Offset: 0x00216F63
		public override string ToString()
		{
			return this.ExternalName ?? this.ContainerClass;
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x00218D78 File Offset: 0x00216F78
		private SharingDataType(string containerClass, string externalName, bool isExternallySharable, string publishName, string publishResourceName, LocalizedString displayName, DefaultFolderType defaultFolderType, StoreObjectType storeObjectType)
		{
			Util.ThrowOnNullOrEmptyArgument(containerClass, "containerClass");
			Util.ThrowOnNullOrEmptyArgument(externalName, "externalName");
			this.PublishResourceName = publishResourceName;
			this.ContainerClass = containerClass;
			this.ExternalName = externalName;
			this.IsExternallySharable = isExternallySharable;
			this.PublishName = publishName;
			this.DisplayName = displayName;
			this.DefaultFolderType = defaultFolderType;
			this.StoreObjectType = storeObjectType;
		}

		// Token: 0x040053C3 RID: 21443
		public static readonly SharingDataType Calendar = new SharingDataType("IPF.Appointment", "calendar", true, "text/calendar", "calendar", ClientStrings.Calendar, DefaultFolderType.Calendar, StoreObjectType.CalendarFolder);

		// Token: 0x040053C4 RID: 21444
		public static readonly SharingDataType ReachCalendar = new SharingDataType("IPF.Appointment", "calendar", true, "text/calendar", "reachcalendar", ClientStrings.Calendar, DefaultFolderType.Calendar, StoreObjectType.CalendarFolder);

		// Token: 0x040053C5 RID: 21445
		public static readonly SharingDataType Contacts = new SharingDataType("IPF.Contact", "contacts", true, null, null, ClientStrings.Contacts, DefaultFolderType.Contacts, StoreObjectType.ContactsFolder);

		// Token: 0x040053C6 RID: 21446
		public static readonly SharingDataType Tasks = new SharingDataType("IPF.Task", "tasks", false, null, null, ClientStrings.Tasks, DefaultFolderType.Tasks, StoreObjectType.TasksFolder);

		// Token: 0x040053C7 RID: 21447
		public static readonly SharingDataType Journal = new SharingDataType("IPF.Journal", "journals", false, null, null, ClientStrings.Journal, DefaultFolderType.Journal, StoreObjectType.JournalFolder);

		// Token: 0x040053C8 RID: 21448
		public static readonly SharingDataType Notes = new SharingDataType("IPF.StickyNote", "notes", false, null, null, ClientStrings.Notes, DefaultFolderType.Notes, StoreObjectType.NotesFolder);

		// Token: 0x040053C9 RID: 21449
		private static SharingDataType[] dataTypes = new SharingDataType[]
		{
			SharingDataType.Calendar,
			SharingDataType.Contacts,
			SharingDataType.Tasks,
			SharingDataType.Journal,
			SharingDataType.Notes
		};

		// Token: 0x040053CA RID: 21450
		private static SharingDataType[] publishDataTypes = new SharingDataType[]
		{
			SharingDataType.Calendar,
			SharingDataType.ReachCalendar
		};
	}
}
