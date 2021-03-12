using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027A RID: 634
	internal class MailboxUtilityDeviceInfo
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x0008B930 File Offset: 0x00089B30
		internal MailboxUtilityDeviceInfo(string displayName, string parentDisplayName, StoreObjectId storeObjectId, HashSet<string> folderList)
		{
			this.displayName = displayName;
			this.parentDisplayName = parentDisplayName;
			this.storeObjectId = storeObjectId;
			this.folderList = folderList;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0008B955 File Offset: 0x00089B55
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x0008B95D File Offset: 0x00089B5D
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0008B966 File Offset: 0x00089B66
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x0008B96E File Offset: 0x00089B6E
		internal string ParentDisplayName
		{
			get
			{
				return this.parentDisplayName;
			}
			set
			{
				this.parentDisplayName = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0008B977 File Offset: 0x00089B77
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x0008B97F File Offset: 0x00089B7F
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
			set
			{
				this.storeObjectId = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0008B988 File Offset: 0x00089B88
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0008B990 File Offset: 0x00089B90
		internal HashSet<string> FolderList
		{
			get
			{
				return this.folderList;
			}
			set
			{
				this.folderList = value;
			}
		}

		// Token: 0x04000E5A RID: 3674
		private string displayName;

		// Token: 0x04000E5B RID: 3675
		private string parentDisplayName;

		// Token: 0x04000E5C RID: 3676
		private StoreObjectId storeObjectId;

		// Token: 0x04000E5D RID: 3677
		private HashSet<string> folderList;
	}
}
