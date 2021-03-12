using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A1 RID: 929
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroupInfoList : List<CalendarGroupInfo>
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000A41BC File Offset: 0x000A23BC
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x000A41C4 File Offset: 0x000A23C4
		public IList<FolderTreeDataInfo> DuplicateNodes { get; private set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000A41CD File Offset: 0x000A23CD
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x000A41D5 File Offset: 0x000A23D5
		public Dictionary<CalendarGroupType, CalendarGroupInfo> DefaultGroups { get; private set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000A41DE File Offset: 0x000A23DE
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x000A41E6 File Offset: 0x000A23E6
		public Dictionary<StoreObjectId, LocalCalendarGroupEntryInfo> CalendarFolderMapping { get; private set; }

		// Token: 0x0600294E RID: 10574 RVA: 0x000A41EF File Offset: 0x000A23EF
		public CalendarGroupInfoList(IList<FolderTreeDataInfo> duplicateNodes, Dictionary<CalendarGroupType, CalendarGroupInfo> defaultGroups, Dictionary<StoreObjectId, LocalCalendarGroupEntryInfo> calendarFolderMapping)
		{
			Util.ThrowOnNullArgument(duplicateNodes, "duplicateNodes");
			Util.ThrowOnNullArgument(defaultGroups, "defaultGroups");
			Util.ThrowOnNullArgument(calendarFolderMapping, "calendarFolderMapping");
			this.DuplicateNodes = duplicateNodes;
			this.DefaultGroups = defaultGroups;
			this.CalendarFolderMapping = calendarFolderMapping;
		}
	}
}
