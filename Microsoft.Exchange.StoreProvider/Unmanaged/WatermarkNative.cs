using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028A RID: 650
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct WatermarkNative
	{
		// Token: 0x04001127 RID: 4391
		public static readonly int SizeOf = Marshal.SizeOf(typeof(WatermarkNative));

		// Token: 0x04001128 RID: 4392
		internal Guid mailboxGuid;

		// Token: 0x04001129 RID: 4393
		internal Guid consumerGuid;

		// Token: 0x0400112A RID: 4394
		internal long llEventCounter;
	}
}
