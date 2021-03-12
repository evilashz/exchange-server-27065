using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026F RID: 623
	[Guid("361487fc-888a-4746-8ab3-2a198c91585a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ComImport]
	internal interface IExchangeImportContentsChanges3 : IExchangeImportContentsChanges
	{
		// Token: 0x06000ABF RID: 2751
		[PreserveSig]
		unsafe int GetLastError(int hResult, int ulFlags, out MAPIERROR* lpMapiError);

		// Token: 0x06000AC0 RID: 2752
		[PreserveSig]
		int Config(IStream pIStream, int ulFlags);

		// Token: 0x06000AC1 RID: 2753
		[PreserveSig]
		int UpdateState(IStream pIStream);

		// Token: 0x06000AC2 RID: 2754
		[PreserveSig]
		unsafe int ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message);

		// Token: 0x06000AC3 RID: 2755
		[PreserveSig]
		unsafe int ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x06000AC4 RID: 2756
		[PreserveSig]
		unsafe int ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState);

		// Token: 0x06000AC5 RID: 2757
		[PreserveSig]
		int ImportMessageMove(int cbSourceKeySrcFolder, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeySrcMessage, int cbPCLMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbPCLMessage, int cbSourceKeyDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbChangeNumDestMessage);

		// Token: 0x06000AC6 RID: 2758
		[PreserveSig]
		unsafe int ImportMessageChangePartial(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out IMessage message);
	}
}
