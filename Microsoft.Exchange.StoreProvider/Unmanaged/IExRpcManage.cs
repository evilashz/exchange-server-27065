using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200027A RID: 634
	[ClassAccessLevel(AccessLevel.Implementation)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("74B90D3B-56F1-434c-B9FC-677440094D50")]
	[ComImport]
	internal interface IExRpcManage
	{
		// Token: 0x06000B4D RID: 2893
		[PreserveSig]
		int Connect(int ulConnectFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszServerDN, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, [MarshalAs(UnmanagedType.Interface)] out IExRpcConnection iExRpcConnection);

		// Token: 0x06000B4E RID: 2894
		[PreserveSig]
		int ConnectEx(int ulFlags, int ulConnectFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszServerDN, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, [MarshalAs(UnmanagedType.LPStr)] string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, [MarshalAs(UnmanagedType.Interface)] out IExRpcConnection iExRpcConnection);

		// Token: 0x06000B4F RID: 2895
		[PreserveSig]
		int FromIStg(IStorage pIStorage, IMessage pIMessage);

		// Token: 0x06000B50 RID: 2896
		[PreserveSig]
		int ToIStg(IStorage pIStorage, IMessage pIMessage, int ulFlags);

		// Token: 0x06000B51 RID: 2897
		[PreserveSig]
		int AdminConnect([MarshalAs(UnmanagedType.LPStr)] string lpszClientId, [MarshalAs(UnmanagedType.LPStr)] string lpszServer, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, out IExRpcAdmin iExRpcAdmin);

		// Token: 0x06000B52 RID: 2898
		[PreserveSig]
		int FromIStream(IStream pIStream, IMessage pIMessage);

		// Token: 0x06000B53 RID: 2899
		[PreserveSig]
		int ToIStream(IStream pIStream, IMessage pIMessage, int ulFlags);

		// Token: 0x06000B54 RID: 2900
		[PreserveSig]
		int CreateAddressBookEntryIdFromLegacyDN([MarshalAs(UnmanagedType.LPStr)] string lpszLegacyDN, out int lpcbEntryId, out SafeExMemoryHandle lppEntryId);

		// Token: 0x06000B55 RID: 2901
		[PreserveSig]
		int CreateLegacyDNFromAddressBookEntryId(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out SafeExMemoryHandle lpszLegacyDN);

		// Token: 0x06000B56 RID: 2902
		[PreserveSig]
		int GetEntryIdType(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int ulType);

		// Token: 0x06000B57 RID: 2903
		[PreserveSig]
		int GetFolderEntryIdFromMessageEntryId(int cbMessageEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpMessageEntryID, out int lpcbFolderEntryId, out SafeExLinkedMemoryHandle lppFolderEntryId);

		// Token: 0x06000B58 RID: 2904
		[PreserveSig]
		int CreateAddressBookEntryIdFromLocalDirectorySID(int cbSid, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpSid, out int lpcbEntryId, out SafeExMemoryHandle lppEntryId);

		// Token: 0x06000B59 RID: 2905
		[PreserveSig]
		int CreateLocalDirectorySIDFromAddressBookEntryId(int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbSid, out SafeExMemoryHandle lpSid);
	}
}
