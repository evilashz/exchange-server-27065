using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000058 RID: 88
	internal struct NATIVE_SNOPENLOG
	{
		// Token: 0x040001B7 RID: 439
		public NATIVE_RECOVERY_CONTROL recoveryControl;

		// Token: 0x040001B8 RID: 440
		public uint cbStruct;

		// Token: 0x040001B9 RID: 441
		public uint lGenNext;

		// Token: 0x040001BA RID: 442
		public byte fCurrentLog;

		// Token: 0x040001BB RID: 443
		public byte eReason;

		// Token: 0x040001BC RID: 444
		public int rgbReserved;

		// Token: 0x040001BD RID: 445
		public IntPtr wszLogFile;

		// Token: 0x040001BE RID: 446
		public uint cdbinfomisc;

		// Token: 0x040001BF RID: 447
		public IntPtr rgdbinfomisc;
	}
}
