using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002A2 RID: 674
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct ERROR_NOTIFICATION
	{
		// Token: 0x04001155 RID: 4437
		internal int cbEntryID;

		// Token: 0x04001156 RID: 4438
		internal IntPtr lpEntryID;

		// Token: 0x04001157 RID: 4439
		internal int scode;

		// Token: 0x04001158 RID: 4440
		internal int ulFlags;

		// Token: 0x04001159 RID: 4441
		internal unsafe MAPIERROR* lpMAPIError;
	}
}
