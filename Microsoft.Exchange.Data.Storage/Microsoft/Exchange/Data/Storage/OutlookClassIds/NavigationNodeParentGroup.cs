using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OutlookClassIds
{
	// Token: 0x02000ADD RID: 2781
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class NavigationNodeParentGroup
	{
		// Token: 0x04003980 RID: 14720
		internal static readonly OutlookClassId MyFoldersClassId = new OutlookClassId(new Guid("{0006F0B7-0000-0000-C000-000000000046}"));

		// Token: 0x04003981 RID: 14721
		internal static readonly OutlookClassId PeoplesFoldersClassId = new OutlookClassId(new Guid("{0006F0B9-0000-0000-C000-000000000046}"));

		// Token: 0x04003982 RID: 14722
		internal static readonly OutlookClassId OtherFoldersClassId = new OutlookClassId(new Guid("{0006F0B8-0000-0000-C000-000000000046}"));
	}
}
