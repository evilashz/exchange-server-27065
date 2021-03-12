using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000289 RID: 649
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct NATIVE_ERRINFOBASIC
	{
		// Token: 0x040006C0 RID: 1728
		public const int HierarchySize = 8;

		// Token: 0x040006C1 RID: 1729
		public const int SourceFileLength = 64;

		// Token: 0x040006C2 RID: 1730
		public uint cbStruct;

		// Token: 0x040006C3 RID: 1731
		public JET_err errValue;

		// Token: 0x040006C4 RID: 1732
		public JET_ERRCAT errcatMostSpecific;

		// Token: 0x040006C5 RID: 1733
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] rgCategoricalHierarchy;

		// Token: 0x040006C6 RID: 1734
		public uint lSourceLine;

		// Token: 0x040006C7 RID: 1735
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string rgszSourceFile;
	}
}
