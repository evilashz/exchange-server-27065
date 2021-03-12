using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000127 RID: 295
	internal sealed class OmexConstants
	{
		// Token: 0x0400062D RID: 1581
		internal static readonly XNamespace OfficeNamespace = XNamespace.Get("urn:schemas-microsoft-com:office:office");

		// Token: 0x02000128 RID: 296
		public enum AppState
		{
			// Token: 0x0400062F RID: 1583
			Undefined = -1,
			// Token: 0x04000630 RID: 1584
			Killed,
			// Token: 0x04000631 RID: 1585
			OK,
			// Token: 0x04000632 RID: 1586
			Withdrawn,
			// Token: 0x04000633 RID: 1587
			Flagged,
			// Token: 0x04000634 RID: 1588
			WithdrawingSoon
		}

		// Token: 0x02000129 RID: 297
		public class StringKeys
		{
			// Token: 0x04000635 RID: 1589
			public const string AppStateProductId = "prodid";

			// Token: 0x04000636 RID: 1590
			public const string Asset = "asset";

			// Token: 0x04000637 RID: 1591
			public const string AssetId = "assetid";

			// Token: 0x04000638 RID: 1592
			public const string Name = "name";

			// Token: 0x04000639 RID: 1593
			public const string Service = "service";

			// Token: 0x0400063A RID: 1594
			public const string State = "state";

			// Token: 0x0400063B RID: 1595
			public const string Token = "token";

			// Token: 0x0400063C RID: 1596
			public const string Url = "url";

			// Token: 0x0400063D RID: 1597
			public const string Version = "ver";

			// Token: 0x0400063E RID: 1598
			public const string AppInstallInfoQuery15 = "AppInstallInfoQuery15";

			// Token: 0x0400063F RID: 1599
			public const string AppQuery15 = "AppQuery15";

			// Token: 0x04000640 RID: 1600
			public const string AppStateQuery15 = "AppStateQuery15";

			// Token: 0x04000641 RID: 1601
			public const string AppInfoQuery15 = "AppInfoQuery15";
		}
	}
}
