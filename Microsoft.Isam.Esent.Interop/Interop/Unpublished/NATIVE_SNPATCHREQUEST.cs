using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200005A RID: 90
	internal struct NATIVE_SNPATCHREQUEST
	{
		// Token: 0x040001C6 RID: 454
		public uint cbStruct;

		// Token: 0x040001C7 RID: 455
		public uint pageNumber;

		// Token: 0x040001C8 RID: 456
		public IntPtr szLogFile;

		// Token: 0x040001C9 RID: 457
		public IntPtr instance;

		// Token: 0x040001CA RID: 458
		public NATIVE_DBINFOMISC7 dbinfomisc;

		// Token: 0x040001CB RID: 459
		public IntPtr pvToken;

		// Token: 0x040001CC RID: 460
		public uint cbToken;

		// Token: 0x040001CD RID: 461
		public IntPtr pvData;

		// Token: 0x040001CE RID: 462
		public uint cbData;
	}
}
