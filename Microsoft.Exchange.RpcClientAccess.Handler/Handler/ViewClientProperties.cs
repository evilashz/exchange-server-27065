using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ViewClientProperties
	{
		// Token: 0x020000A4 RID: 164
		internal static class HierarchyViewClientProperties
		{
			// Token: 0x040002AD RID: 685
			public static PropertyTag[] DisallowList = new PropertyTag[]
			{
				PropertyTag.OfflineFlags,
				PropertyTag.StoreSupportMask,
				PropertyTag.ReplicaServer,
				PropertyTag.ReplicaVersion,
				PropertyTag.StoreSupportMask,
				PropertyTag.AccessLevel
			};
		}

		// Token: 0x020000A5 RID: 165
		internal static class ContentsViewClientProperties
		{
			// Token: 0x040002AE RID: 686
			public static PropertyTag[] DisallowList = new PropertyTag[]
			{
				PropertyTag.InstanceKey,
				PropertyTag.ObjectType,
				PropertyTag.EntryId,
				PropertyTag.RecordKey,
				PropertyTag.StoreEntryId,
				PropertyTag.StoreRecordKey,
				PropertyTag.ParentEntryId,
				PropertyTag.SentMailEntryId,
				PropertyTag.PostReplyFolderEntries,
				PropertyTag.RuleFolderFid
			};
		}
	}
}
