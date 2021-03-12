using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200003E RID: 62
	public struct NATIVE_PAGEINFO
	{
		// Token: 0x04000144 RID: 324
		public uint pgno;

		// Token: 0x04000145 RID: 325
		public uint bitField;

		// Token: 0x04000146 RID: 326
		public ulong checksumActual;

		// Token: 0x04000147 RID: 327
		public ulong checksumExpected;

		// Token: 0x04000148 RID: 328
		public ulong dbtime;

		// Token: 0x04000149 RID: 329
		public ulong structureChecksum;

		// Token: 0x0400014A RID: 330
		public ulong flags;
	}
}
