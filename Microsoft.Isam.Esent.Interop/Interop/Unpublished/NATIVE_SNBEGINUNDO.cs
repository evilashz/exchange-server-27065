using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200004A RID: 74
	internal struct NATIVE_SNBEGINUNDO
	{
		// Token: 0x04000189 RID: 393
		public NATIVE_RECOVERY_CONTROL recoveryControl;

		// Token: 0x0400018A RID: 394
		public uint cbStruct;

		// Token: 0x0400018B RID: 395
		public uint cdbinfomisc;

		// Token: 0x0400018C RID: 396
		public IntPtr rgdbinfomisc;
	}
}
