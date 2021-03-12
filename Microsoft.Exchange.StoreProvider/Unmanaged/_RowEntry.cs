using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025D RID: 605
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _RowEntry
	{
		// Token: 0x040010B4 RID: 4276
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_RowEntry));

		// Token: 0x040010B5 RID: 4277
		internal int ulRowFlags;

		// Token: 0x040010B6 RID: 4278
		internal int cValues;

		// Token: 0x040010B7 RID: 4279
		internal unsafe SPropValue* rgPropVals;
	}
}
