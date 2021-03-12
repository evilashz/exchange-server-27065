using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000270 RID: 624
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F5F9FFFE-D1AF-45d3-B790-E4D489D38B7E")]
	[ComImport]
	internal interface IExchangeImportContentsChanges4 : IExchangeImportContentsChanges3, IExchangeImportContentsChanges
	{
		// Token: 0x06000AC7 RID: 2759
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AC8 RID: 2760
		[PreserveSig]
		int Config(IStream pIStream, int ulFlags);

		// Token: 0x06000AC9 RID: 2761
		[PreserveSig]
		int UpdateState(IStream pIStream);

		// Token: 0x06000ACA RID: 2762
		[PreserveSig]
		unsafe int ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message);

		// Token: 0x06000ACB RID: 2763
		[PreserveSig]
		unsafe int ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x06000ACC RID: 2764
		[PreserveSig]
		unsafe int ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState);

		// Token: 0x06000ACD RID: 2765
		[PreserveSig]
		int ImportMessageMove(int cbSourceKeySrcFolder, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcMessage, int cbPCLMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbPCLMessage, int cbSourceKeyDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbChangeNumDestMessage);

		// Token: 0x06000ACE RID: 2766
		[PreserveSig]
		unsafe int ImportMessageChangePartial(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message);

		// Token: 0x06000ACF RID: 2767
		[PreserveSig]
		int ConfigEx([MarshalAs(UnmanagedType.LPArray)] byte[] pbIdsetGiven, int cbIdsetGiven, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeen, int cbCnsetSeen, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetSeenFAI, int cbCnsetSeenFAI, [MarshalAs(UnmanagedType.LPArray)] byte[] pbCnsetRead, int cbCnsetRead, int ulFlags);

		// Token: 0x06000AD0 RID: 2768
		[PreserveSig]
		int UpdateStateEx(out IntPtr pbIdsetGiven, out int cbIdsetGiven, out IntPtr pbCnsetSeen, out int cbCnsetSeen, out IntPtr pbCnsetSeenFAI, out int cbCnsetSeenFAI, out IntPtr pbCnsetRead, out int cbCnsetRead);
	}
}
