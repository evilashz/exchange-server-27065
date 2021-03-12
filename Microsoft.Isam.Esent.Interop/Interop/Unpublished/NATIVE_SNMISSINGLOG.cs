using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000052 RID: 82
	internal struct NATIVE_SNMISSINGLOG
	{
		// Token: 0x040001A0 RID: 416
		public NATIVE_RECOVERY_CONTROL recoveryControl;

		// Token: 0x040001A1 RID: 417
		public uint cbStruct;

		// Token: 0x040001A2 RID: 418
		public uint lGenMissing;

		// Token: 0x040001A3 RID: 419
		public byte fCurrentLog;

		// Token: 0x040001A4 RID: 420
		public byte eNextAction;

		// Token: 0x040001A5 RID: 421
		public int rgbReserved;

		// Token: 0x040001A6 RID: 422
		public IntPtr wszLogFile;

		// Token: 0x040001A7 RID: 423
		public uint cdbinfomisc;

		// Token: 0x040001A8 RID: 424
		public IntPtr rgdbinfomisc;
	}
}
