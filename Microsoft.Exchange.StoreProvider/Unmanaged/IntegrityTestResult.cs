using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002E7 RID: 743
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct IntegrityTestResult
	{
		// Token: 0x0400123B RID: 4667
		private const int TestNameLength = 40;

		// Token: 0x0400123C RID: 4668
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
		public string Name;

		// Token: 0x0400123D RID: 4669
		public uint Errors;

		// Token: 0x0400123E RID: 4670
		public uint Warnings;

		// Token: 0x0400123F RID: 4671
		public uint Fixes;

		// Token: 0x04001240 RID: 4672
		public uint Time;

		// Token: 0x04001241 RID: 4673
		public uint Rows;

		// Token: 0x04001242 RID: 4674
		public int ErrorCode;
	}
}
