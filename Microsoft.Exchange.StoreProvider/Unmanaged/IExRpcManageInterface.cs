using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028E RID: 654
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExRpcManageInterface : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000BB7 RID: 2999
		int Connect(int ulConnectFlags, string lpszServerDN, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, out IExRpcConnectionInterface iExRpcConnection);

		// Token: 0x06000BB8 RID: 3000
		int ConnectEx(int ulFlags, int ulConnectFlags, string lpszServerDN, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, out IExRpcConnectionInterface iExRpcConnection);

		// Token: 0x06000BB9 RID: 3001
		int ConnectEx2(int ulFlags, int ulConnectFlags, string lpszServerDN, byte[] pbMdbGuid, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, int cbClientSessionInfoSize, byte[] pbClientSessionInfo, IntPtr connectDelegate, IntPtr executeDelegate, IntPtr disconnectDelegate, int ulConnectionTimeoutMSecs, int ulCallTimeoutMSecs, out IExRpcConnectionInterface iExRpcConnection);

		// Token: 0x06000BBA RID: 3002
		int FromIStg(IStorage iStorage, IntPtr iMessage);

		// Token: 0x06000BBB RID: 3003
		int ToIStg(IStorage iStorage, IntPtr iMessage, int ulFlags);

		// Token: 0x06000BBC RID: 3004
		int FromIStream(IStream iStream, IntPtr iMessage);

		// Token: 0x06000BBD RID: 3005
		int ToIStream(IStream iStream, IntPtr iMessage, int ulFlags);

		// Token: 0x06000BBE RID: 3006
		int CreateAddressBookEntryIdFromLegacyDN(string lpszLegacyDN, out byte[] entryID);

		// Token: 0x06000BBF RID: 3007
		int CreateLegacyDNFromAddressBookEntryId(int cbEntryId, byte[] lpEntryID, out string lpszLegacyDN);

		// Token: 0x06000BC0 RID: 3008
		int GetEntryIdType(int cbEntryId, byte[] lpEntryID, out int ulType);

		// Token: 0x06000BC1 RID: 3009
		int GetFolderEntryIdFromMessageEntryId(int cbMessageEntryId, byte[] lpMessageEntryID, out byte[] folderEntryId);

		// Token: 0x06000BC2 RID: 3010
		int CreateAddressBookEntryIdFromLocalDirectorySID(int cbSid, byte[] lpSid, out byte[] entryId);

		// Token: 0x06000BC3 RID: 3011
		int CreateLocalDirectorySIDFromAddressBookEntryId(int cbEntryId, byte[] lpEntryID, out byte[] lpSid);

		// Token: 0x06000BC4 RID: 3012
		int CreateIdSetBlobFromIStream(PropTag ptag, IStream iStream, out byte[] idSetBlob);
	}
}
