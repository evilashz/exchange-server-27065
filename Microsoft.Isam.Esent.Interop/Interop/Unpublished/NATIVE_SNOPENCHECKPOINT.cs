using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000056 RID: 86
	internal struct NATIVE_SNOPENCHECKPOINT
	{
		// Token: 0x040001B3 RID: 435
		public NATIVE_RECOVERY_CONTROL recoveryControl;

		// Token: 0x040001B4 RID: 436
		public uint cbStruct;

		// Token: 0x040001B5 RID: 437
		public IntPtr wszCheckpoint;
	}
}
