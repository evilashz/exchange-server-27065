using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200004C RID: 76
	internal struct NATIVE_SNCORRUPTEDPAGE
	{
		// Token: 0x0400018F RID: 399
		public uint cbStruct;

		// Token: 0x04000190 RID: 400
		public IntPtr wszDatabase;

		// Token: 0x04000191 RID: 401
		public uint dbid;

		// Token: 0x04000192 RID: 402
		public NATIVE_DBINFOMISC7 dbinfomisc;

		// Token: 0x04000193 RID: 403
		public uint pageNumber;
	}
}
