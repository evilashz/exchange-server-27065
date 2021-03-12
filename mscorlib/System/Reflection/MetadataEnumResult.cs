using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005D0 RID: 1488
	internal struct MetadataEnumResult
	{
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x000FE117 File Offset: 0x000FC317
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000A9B RID: 2715
		public unsafe int this[int index]
		{
			[SecurityCritical]
			get
			{
				if (this.largeResult != null)
				{
					return this.largeResult[index];
				}
				fixed (int* ptr = &this.smallResult.FixedElementField)
				{
					return ptr[index];
				}
			}
		}

		// Token: 0x04001C95 RID: 7317
		private int[] largeResult;

		// Token: 0x04001C96 RID: 7318
		private int length;

		// Token: 0x04001C97 RID: 7319
		[FixedBuffer(typeof(int), 16)]
		private MetadataEnumResult.<smallResult>e__FixedBuffer smallResult;

		// Token: 0x02000C05 RID: 3077
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <smallResult>e__FixedBuffer
		{
			// Token: 0x04003654 RID: 13908
			public int FixedElementField;
		}
	}
}
