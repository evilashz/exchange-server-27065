using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E77 RID: 3703
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskGroupInfoList : List<TaskGroupInfo>
	{
		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x06008095 RID: 32917 RVA: 0x00232868 File Offset: 0x00230A68
		// (set) Token: 0x06008096 RID: 32918 RVA: 0x00232870 File Offset: 0x00230A70
		public IList<FolderTreeDataInfo> DuplicateNodes { get; private set; }

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x06008097 RID: 32919 RVA: 0x00232879 File Offset: 0x00230A79
		// (set) Token: 0x06008098 RID: 32920 RVA: 0x00232881 File Offset: 0x00230A81
		public Dictionary<TaskGroupType, TaskGroupInfo> DefaultGroups { get; private set; }

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x06008099 RID: 32921 RVA: 0x0023288A File Offset: 0x00230A8A
		// (set) Token: 0x0600809A RID: 32922 RVA: 0x00232892 File Offset: 0x00230A92
		public Dictionary<StoreObjectId, TaskGroupEntryInfo> TaskFolderMapping { get; private set; }

		// Token: 0x0600809B RID: 32923 RVA: 0x0023289B File Offset: 0x00230A9B
		public TaskGroupInfoList(IList<FolderTreeDataInfo> duplicateNodes, Dictionary<TaskGroupType, TaskGroupInfo> defaultGroups, Dictionary<StoreObjectId, TaskGroupEntryInfo> taskFolderMapping)
		{
			Util.ThrowOnNullArgument(duplicateNodes, "duplicateNodes");
			Util.ThrowOnNullArgument(defaultGroups, "defaultGroups");
			Util.ThrowOnNullArgument(taskFolderMapping, "taskFolderMapping");
			this.DuplicateNodes = duplicateNodes;
			this.DefaultGroups = defaultGroups;
			this.TaskFolderMapping = taskFolderMapping;
		}
	}
}
