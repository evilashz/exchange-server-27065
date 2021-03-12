using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CE9 RID: 3305
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D53552C8-77E3-101A-B552-08002B33B0E6")]
	[ComImport]
	internal interface IWordBreaker
	{
		// Token: 0x0600722A RID: 29226
		[PreserveSig]
		int Init([In] bool fQuery, [MarshalAs(UnmanagedType.U4)] int maxTokenSize, out bool pfLicense);

		// Token: 0x0600722B RID: 29227
		[PreserveSig]
		int BreakText([MarshalAs(UnmanagedType.Struct)] [In] [Out] ref TEXT_SOURCE pTextSource, [In] IWordSink pWordSink, [In] IPhraseSink pPhraseSink);

		// Token: 0x0600722C RID: 29228
		[PreserveSig]
		int GetLicenseToUse([MarshalAs(UnmanagedType.LPWStr)] out string ppwcsLicense);
	}
}
