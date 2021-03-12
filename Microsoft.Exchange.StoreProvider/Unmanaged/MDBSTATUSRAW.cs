using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028B RID: 651
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct MDBSTATUSRAW
	{
		// Token: 0x0400112B RID: 4395
		public static readonly int SizeOf = Marshal.SizeOf(typeof(MDBSTATUSRAW));

		// Token: 0x0400112C RID: 4396
		internal Guid guidMdb;

		// Token: 0x0400112D RID: 4397
		internal uint ulStatus;

		// Token: 0x0400112E RID: 4398
		internal uint cbMdbName;

		// Token: 0x0400112F RID: 4399
		internal uint cbVServerName;

		// Token: 0x04001130 RID: 4400
		internal uint cbMdbLegacyDN;

		// Token: 0x04001131 RID: 4401
		internal uint cbStorageGroupName;

		// Token: 0x04001132 RID: 4402
		internal uint ibMdbName;

		// Token: 0x04001133 RID: 4403
		internal uint ibVServerName;

		// Token: 0x04001134 RID: 4404
		internal uint ibMdbLegacyDN;

		// Token: 0x04001135 RID: 4405
		internal uint ibStorageGroupName;
	}
}
