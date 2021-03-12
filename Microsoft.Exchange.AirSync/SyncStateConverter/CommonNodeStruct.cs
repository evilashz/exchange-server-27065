using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x0200027C RID: 636
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct CommonNodeStruct
	{
		// Token: 0x04000E62 RID: 3682
		public string ServerId;

		// Token: 0x04000E63 RID: 3683
		public string VersionId;

		// Token: 0x04000E64 RID: 3684
		public byte SentToClient;

		// Token: 0x04000E65 RID: 3685
		public byte IsEmail;

		// Token: 0x04000E66 RID: 3686
		public byte Read;

		// Token: 0x04000E67 RID: 3687
		public byte IsCalendar;

		// Token: 0x04000E68 RID: 3688
		public IntPtr Next;

		// Token: 0x04000E69 RID: 3689
		public long EndTime;
	}
}
