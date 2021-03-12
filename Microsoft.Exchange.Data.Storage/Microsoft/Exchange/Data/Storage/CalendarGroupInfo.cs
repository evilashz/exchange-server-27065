using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A0 RID: 928
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarGroupInfo : FolderTreeDataInfo
	{
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000A4128 File Offset: 0x000A2328
		// (set) Token: 0x06002940 RID: 10560 RVA: 0x000A4130 File Offset: 0x000A2330
		public string GroupName { get; private set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000A4139 File Offset: 0x000A2339
		// (set) Token: 0x06002942 RID: 10562 RVA: 0x000A4141 File Offset: 0x000A2341
		public Guid GroupClassId { get; private set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000A414A File Offset: 0x000A234A
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x000A4152 File Offset: 0x000A2352
		public CalendarGroupType GroupType { get; private set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000A415B File Offset: 0x000A235B
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x000A4163 File Offset: 0x000A2363
		public List<CalendarGroupEntryInfo> Calendars { get; private set; }

		// Token: 0x06002947 RID: 10567 RVA: 0x000A416C File Offset: 0x000A236C
		public CalendarGroupInfo(string groupName, VersionedId id, Guid groupClassId, CalendarGroupType groupType, byte[] groupOrdinal, ExDateTime lastModifiedTime) : base(id, groupOrdinal, lastModifiedTime)
		{
			Util.ThrowOnNullArgument(groupName, "groupName");
			EnumValidator.ThrowIfInvalid<CalendarGroupType>(groupType, "groupType");
			this.GroupName = groupName;
			this.GroupClassId = groupClassId;
			this.GroupType = groupType;
			this.Calendars = new List<CalendarGroupEntryInfo>();
		}
	}
}
