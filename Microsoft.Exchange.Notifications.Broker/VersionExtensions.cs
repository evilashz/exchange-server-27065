using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000038 RID: 56
	internal static class VersionExtensions
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000CBBE File Offset: 0x0000ADBE
		internal static bool IsTooNew(this Version lhsVersion, Version rhsVersion)
		{
			return lhsVersion.MajorRevision > rhsVersion.MajorRevision;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CBCE File Offset: 0x0000ADCE
		internal static bool IsTooOld(this Version lhsVersion, Version rhsVersion)
		{
			return lhsVersion.MajorRevision < rhsVersion.MajorRevision;
		}

		// Token: 0x04000102 RID: 258
		private static readonly Version CurrentVersion = new Version(1, 0);
	}
}
