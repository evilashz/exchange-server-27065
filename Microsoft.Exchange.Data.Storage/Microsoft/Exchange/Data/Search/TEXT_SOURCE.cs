using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CEA RID: 3306
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct TEXT_SOURCE
	{
		// Token: 0x04004F81 RID: 20353
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public FillTextBuffer FillTextBuffer;

		// Token: 0x04004F82 RID: 20354
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Buffer;

		// Token: 0x04004F83 RID: 20355
		[MarshalAs(UnmanagedType.U4)]
		public int End;

		// Token: 0x04004F84 RID: 20356
		[MarshalAs(UnmanagedType.U4)]
		public int Current;
	}
}
