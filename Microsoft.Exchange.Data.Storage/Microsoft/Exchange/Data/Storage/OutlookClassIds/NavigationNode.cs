using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OutlookClassIds
{
	// Token: 0x02000ADC RID: 2780
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class NavigationNode
	{
		// Token: 0x0400397D RID: 14717
		internal static readonly OutlookClassId MailFolderFavoriteClassId = new OutlookClassId(new Guid("{00067800-0000-0000-C000-000000000046}"));

		// Token: 0x0400397E RID: 14718
		internal static readonly OutlookClassId CalendarFolderFavoriteClassId = new OutlookClassId(new Guid("{00067802-0000-0000-C000-000000000046}"));

		// Token: 0x0400397F RID: 14719
		internal static readonly OutlookClassId ContactFolderFavoriteClassId = new OutlookClassId(new Guid("{00067801-0000-0000-C000-000000000046}"));
	}
}
