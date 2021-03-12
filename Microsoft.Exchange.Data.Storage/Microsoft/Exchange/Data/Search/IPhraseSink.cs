using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CE8 RID: 3304
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CC907054-C058-101A-B554-08002B33B0E6")]
	[ComImport]
	internal interface IPhraseSink
	{
		// Token: 0x06007228 RID: 29224
		[PreserveSig]
		void PutSmallPhrase([MarshalAs(UnmanagedType.LPWStr)] string pwcNoun, [MarshalAs(UnmanagedType.U4)] int cwcNoun, [MarshalAs(UnmanagedType.LPWStr)] string pwcModifier, [MarshalAs(UnmanagedType.U4)] int cwcModifier, [MarshalAs(UnmanagedType.U4)] int ulAttachmentType);

		// Token: 0x06007229 RID: 29225
		[PreserveSig]
		void PutPhrase([MarshalAs(UnmanagedType.LPWStr)] string pwcPhrase, [MarshalAs(UnmanagedType.U4)] int cwcPhrase);
	}
}
