using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E76 RID: 3702
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupInfo : FolderTreeDataInfo
	{
		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x0600808C RID: 32908 RVA: 0x002327D3 File Offset: 0x002309D3
		// (set) Token: 0x0600808D RID: 32909 RVA: 0x002327DB File Offset: 0x002309DB
		public string GroupName { get; private set; }

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x0600808E RID: 32910 RVA: 0x002327E4 File Offset: 0x002309E4
		// (set) Token: 0x0600808F RID: 32911 RVA: 0x002327EC File Offset: 0x002309EC
		public Guid GroupClassId { get; private set; }

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06008090 RID: 32912 RVA: 0x002327F5 File Offset: 0x002309F5
		// (set) Token: 0x06008091 RID: 32913 RVA: 0x002327FD File Offset: 0x002309FD
		public TaskGroupType GroupType { get; private set; }

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06008092 RID: 32914 RVA: 0x00232806 File Offset: 0x00230A06
		// (set) Token: 0x06008093 RID: 32915 RVA: 0x0023280E File Offset: 0x00230A0E
		public List<TaskGroupEntryInfo> TaskFolders { get; private set; }

		// Token: 0x06008094 RID: 32916 RVA: 0x00232818 File Offset: 0x00230A18
		public TaskGroupInfo(string groupName, VersionedId id, Guid groupClassId, TaskGroupType groupType, byte[] groupOrdinal, ExDateTime lastModifiedTime) : base(id, groupOrdinal, lastModifiedTime)
		{
			Util.ThrowOnNullArgument(groupName, "groupName");
			EnumValidator.ThrowIfInvalid<TaskGroupType>(groupType, "groupType");
			this.GroupName = groupName;
			this.GroupClassId = groupClassId;
			this.GroupType = groupType;
			this.TaskFolders = new List<TaskGroupEntryInfo>();
		}
	}
}
