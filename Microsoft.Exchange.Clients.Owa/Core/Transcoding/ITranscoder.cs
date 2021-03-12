using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F0 RID: 752
	[Guid("A556B022-130F-443F-AFF5-AE9AAC269D3D")]
	[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
	[ComImport]
	internal interface ITranscoder
	{
		// Token: 0x06001C86 RID: 7302
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		TranscodeErrorCode Initialize([In] TranscodingInitOption initOption);

		// Token: 0x06001C87 RID: 7303
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		TranscodeErrorCode Convert([MarshalAs(UnmanagedType.BStr)] [In] string sourceDocPath, [MarshalAs(UnmanagedType.BStr)] [In] string outputFilePath, [MarshalAs(UnmanagedType.BStr)] [In] string sourceDocType, [In] int currentPageNumber, out int totalPageNumber, out int outputDataSize);
	}
}
