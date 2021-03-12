using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200029C RID: 668
	internal struct NATIVE_INDEXRANGE
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00017F1C File Offset: 0x0001611C
		public static NATIVE_INDEXRANGE MakeIndexRangeFromTableid(JET_TABLEID tableid)
		{
			NATIVE_INDEXRANGE result = new NATIVE_INDEXRANGE
			{
				tableid = tableid.Value,
				grbit = 1U
			};
			result.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_INDEXRANGE));
			return result;
		}

		// Token: 0x04000750 RID: 1872
		public uint cbStruct;

		// Token: 0x04000751 RID: 1873
		public IntPtr tableid;

		// Token: 0x04000752 RID: 1874
		public uint grbit;
	}
}
