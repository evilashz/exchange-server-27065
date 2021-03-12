using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000287 RID: 647
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct THREAD_DIAG_CONTEXT
	{
		// Token: 0x04001107 RID: 4359
		internal long pNextCtx;

		// Token: 0x04001108 RID: 4360
		internal uint dwTid;

		// Token: 0x04001109 RID: 4361
		internal uint dwRequestId;

		// Token: 0x0400110A RID: 4362
		internal long dtFirstEvent;

		// Token: 0x0400110B RID: 4363
		internal byte fDataOverflow;

		// Token: 0x0400110C RID: 4364
		internal byte fCircularBuffering;

		// Token: 0x0400110D RID: 4365
		internal uint dwDataSize;

		// Token: 0x0400110E RID: 4366
		internal uint dwSegm2Beg;

		// Token: 0x0400110F RID: 4367
		internal uint dwSegm2Len;

		// Token: 0x04001110 RID: 4368
		[FixedBuffer(typeof(byte), 512)]
		internal THREAD_DIAG_CONTEXT.<byteData>e__FixedBuffer0 byteData;

		// Token: 0x02000288 RID: 648
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <byteData>e__FixedBuffer0
		{
			// Token: 0x04001111 RID: 4369
			public byte FixedElementField;
		}
	}
}
