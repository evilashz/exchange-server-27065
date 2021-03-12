using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073C RID: 1852
	[XmlType(TypeName = "DelegatePermissionsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DelegatePermissionsType
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000C5C97 File Offset: 0x000C3E97
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000C5C9F File Offset: 0x000C3E9F
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		[XmlElement("CalendarFolderPermissionLevel")]
		public DelegateFolderPermissionLevelType CalendarFolderPermissionLevel
		{
			get
			{
				return this.calenderFolderPermission;
			}
			set
			{
				this.calenderFolderPermission = value;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000C5CA8 File Offset: 0x000C3EA8
		// (set) Token: 0x060037BE RID: 14270 RVA: 0x000C5CB0 File Offset: 0x000C3EB0
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		[XmlElement("TasksFolderPermissionLevel")]
		public DelegateFolderPermissionLevelType TasksFolderPermissionLevel
		{
			get
			{
				return this.tasksFolderPermission;
			}
			set
			{
				this.tasksFolderPermission = value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000C5CB9 File Offset: 0x000C3EB9
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x000C5CC1 File Offset: 0x000C3EC1
		[XmlElement("InboxFolderPermissionLevel")]
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		public DelegateFolderPermissionLevelType InboxFolderPermissionLevel
		{
			get
			{
				return this.inboxFolderPermission;
			}
			set
			{
				this.inboxFolderPermission = value;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000C5CCA File Offset: 0x000C3ECA
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000C5CD2 File Offset: 0x000C3ED2
		[XmlElement("ContactsFolderPermissionLevel")]
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		public DelegateFolderPermissionLevelType ContactsFolderPermissionLevel
		{
			get
			{
				return this.contactsFolderPermission;
			}
			set
			{
				this.contactsFolderPermission = value;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000C5CDB File Offset: 0x000C3EDB
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x000C5CE3 File Offset: 0x000C3EE3
		[XmlElement("NotesFolderPermissionLevel")]
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		public DelegateFolderPermissionLevelType NotesFolderPermissionLevel
		{
			get
			{
				return this.notesFolderPermission;
			}
			set
			{
				this.notesFolderPermission = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000C5CEC File Offset: 0x000C3EEC
		// (set) Token: 0x060037C6 RID: 14278 RVA: 0x000C5CF4 File Offset: 0x000C3EF4
		[DefaultValue(DelegateFolderPermissionLevelType.Default)]
		[XmlElement("JournalFolderPermissionLevel")]
		public DelegateFolderPermissionLevelType JournalFolderPermissionLevel
		{
			get
			{
				return this.journalFolderPermission;
			}
			set
			{
				this.journalFolderPermission = value;
			}
		}

		// Token: 0x04001F01 RID: 7937
		private DelegateFolderPermissionLevelType calenderFolderPermission;

		// Token: 0x04001F02 RID: 7938
		private DelegateFolderPermissionLevelType tasksFolderPermission;

		// Token: 0x04001F03 RID: 7939
		private DelegateFolderPermissionLevelType inboxFolderPermission;

		// Token: 0x04001F04 RID: 7940
		private DelegateFolderPermissionLevelType contactsFolderPermission;

		// Token: 0x04001F05 RID: 7941
		private DelegateFolderPermissionLevelType notesFolderPermission;

		// Token: 0x04001F06 RID: 7942
		private DelegateFolderPermissionLevelType journalFolderPermission;
	}
}
