using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CEC RID: 3308
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CC907054-C058-101A-B554-08002B33B0E6")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IWordSink
	{
		// Token: 0x06007231 RID: 29233
		[PreserveSig]
		void PutWord([MarshalAs(UnmanagedType.U4)] int cwc, [MarshalAs(UnmanagedType.LPWStr)] string pwcInBuf, [MarshalAs(UnmanagedType.U4)] int cwcSrcLen, [MarshalAs(UnmanagedType.U4)] int cwcSrcPos);

		// Token: 0x06007232 RID: 29234
		[PreserveSig]
		void PutAltWord([MarshalAs(UnmanagedType.U4)] int cwc, [MarshalAs(UnmanagedType.LPWStr)] string pwcInBuf, [MarshalAs(UnmanagedType.U4)] int cwcSrcLen, [MarshalAs(UnmanagedType.U4)] int cwcSrcPos);

		// Token: 0x06007233 RID: 29235
		[PreserveSig]
		void StartAltPhrase();

		// Token: 0x06007234 RID: 29236
		[PreserveSig]
		void EndAltPhrase();

		// Token: 0x06007235 RID: 29237
		[PreserveSig]
		void PutBreak([In] WordBreakType breakType);
	}
}
