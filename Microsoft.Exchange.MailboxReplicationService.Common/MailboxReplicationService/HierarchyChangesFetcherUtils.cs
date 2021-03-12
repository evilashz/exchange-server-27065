using System;
using System.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028F RID: 655
	internal static class HierarchyChangesFetcherUtils
	{
		// Token: 0x06002004 RID: 8196 RVA: 0x00044054 File Offset: 0x00042254
		[Conditional("DEBUG")]
		internal static void ValidateEnumeration(EnumerateHierarchyChangesFlags flags, IHierarchyChangesFetcher hierarchyChangesFetcher, bool hasMoreHierarchyChangesPrevPage, bool isPagedEnumeration)
		{
			flags.HasFlag(EnumerateHierarchyChangesFlags.FirstPage);
			flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			if (!isPagedEnumeration || flags.HasFlag(EnumerateHierarchyChangesFlags.FirstPage) || flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup))
			{
				return;
			}
		}
	}
}
