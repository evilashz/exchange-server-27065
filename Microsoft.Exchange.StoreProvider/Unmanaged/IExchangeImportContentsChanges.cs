using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020001D1 RID: 465
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f75abfa0-d0e0-11cd-80fc-00aa004bba0b")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExchangeImportContentsChanges
	{
		// Token: 0x060006EE RID: 1774
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x060006EF RID: 1775
		[PreserveSig]
		int Config(IStream pIStream, int ulFlags);

		// Token: 0x060006F0 RID: 1776
		[PreserveSig]
		int UpdateState(IStream pIStream);

		// Token: 0x060006F1 RID: 1777
		[PreserveSig]
		unsafe int ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message);

		// Token: 0x060006F2 RID: 1778
		[PreserveSig]
		unsafe int ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x060006F3 RID: 1779
		[PreserveSig]
		unsafe int ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState);

		// Token: 0x060006F4 RID: 1780
		[PreserveSig]
		int ImportMessageMove(int cbSourceKeySrcFolder, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcMessage, int cbPCLMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbPCLMessage, int cbSourceKeyDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbChangeNumDestMessage);
	}
}
