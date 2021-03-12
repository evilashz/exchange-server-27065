using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000283 RID: 643
	internal struct NATIVE_ENUMCOLUMNID
	{
		// Token: 0x04000530 RID: 1328
		public uint columnid;

		// Token: 0x04000531 RID: 1329
		public uint ctagSequence;

		// Token: 0x04000532 RID: 1330
		public unsafe uint* rgtagSequence;
	}
}
