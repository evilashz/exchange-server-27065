using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CD RID: 717
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExRpcManageHandle : SafeExInterfaceHandle, IExRpcManageInterface, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x00037C40 File Offset: 0x00035E40
		protected SafeExRpcManageHandle()
		{
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00037C48 File Offset: 0x00035E48
		internal SafeExRpcManageHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00037C51 File Offset: 0x00035E51
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExRpcManageHandle>(this);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00037C5C File Offset: 0x00035E5C
		public int Connect(int ulConnectFlags, string lpszServerDN, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, out IExRpcConnectionInterface iExRpcConnection)
		{
			SafeExRpcConnectionHandle safeExRpcConnectionHandle = null;
			int result = SafeExRpcManageHandle.IExRpcManage_Connect(this.handle, ulConnectFlags, lpszServerDN, lpszUserDN, lpwszUser, lpwszDomain, lpwszPassword, out safeExRpcConnectionHandle);
			iExRpcConnection = safeExRpcConnectionHandle;
			return result;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00037C88 File Offset: 0x00035E88
		public int ConnectEx(int ulFlags, int ulConnectFlags, string lpszServerDN, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, out IExRpcConnectionInterface iExRpcConnection)
		{
			SafeExRpcConnectionHandle safeExRpcConnectionHandle = null;
			int result = SafeExRpcManageHandle.IExRpcManage_ConnectEx(this.handle, ulFlags, ulConnectFlags, lpszServerDN, lpszUserDN, lpwszUser, lpwszDomain, lpwszPassword, lpszHTTPProxyServerName, ulConMod, lcidString, lcidSort, cpid, cReconnectIntervalInMins, cbRpcBufferSize, cbAuxBufferSize, out safeExRpcConnectionHandle);
			iExRpcConnection = safeExRpcConnectionHandle;
			return result;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00037CC8 File Offset: 0x00035EC8
		public int ConnectEx2(int ulFlags, int ulConnectFlags, string lpszServerDN, byte[] pbMdbGuid, string lpszUserDN, string lpwszUser, string lpwszDomain, string lpwszPassword, string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, int cbClientSessionInfoSize, byte[] pbClientSessionInfo, IntPtr connectDelegate, IntPtr executeDelegate, IntPtr disconnectDelegate, int ulConnectionTimeoutMSecs, int ulCallTimeoutMSecs, out IExRpcConnectionInterface iExRpcConnection)
		{
			SafeExRpcConnectionHandle safeExRpcConnectionHandle = null;
			int result = SafeExRpcManageHandle.IExRpcManage_ConnectEx2(this.handle, ulFlags, ulConnectFlags, lpszServerDN, pbMdbGuid, lpszUserDN, lpwszUser, lpwszDomain, lpwszPassword, lpszHTTPProxyServerName, ulConMod, lcidString, lcidSort, cpid, cReconnectIntervalInMins, cbRpcBufferSize, cbAuxBufferSize, cbClientSessionInfoSize, pbClientSessionInfo, connectDelegate, executeDelegate, disconnectDelegate, ulConnectionTimeoutMSecs, ulCallTimeoutMSecs, out safeExRpcConnectionHandle);
			iExRpcConnection = safeExRpcConnectionHandle;
			return result;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00037D15 File Offset: 0x00035F15
		public int FromIStg(IStorage iStorage, IntPtr iMessage)
		{
			return SafeExRpcManageHandle.IExRpcManage_FromIStg(this.handle, iStorage, iMessage);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00037D24 File Offset: 0x00035F24
		public int ToIStg(IStorage iStorage, IntPtr iMessage, int ulFlags)
		{
			return SafeExRpcManageHandle.IExRpcManage_ToIStg(this.handle, iStorage, iMessage, ulFlags);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00037D34 File Offset: 0x00035F34
		internal int AdminConnect(string lpszClientId, string lpszServer, string lpwszUser, string lpwszDomain, string lpwszPassword, out SafeExRpcAdminHandle iExRpcAdmin)
		{
			return SafeExRpcManageHandle.IExRpcManage_AdminConnect(this.handle, lpszClientId, lpszServer, lpwszUser, lpwszDomain, lpwszPassword, out iExRpcAdmin);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00037D4A File Offset: 0x00035F4A
		public int FromIStream(IStream iStream, IntPtr iMessage)
		{
			return SafeExRpcManageHandle.IExRpcManage_FromIStream(this.handle, iStream, iMessage);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00037D59 File Offset: 0x00035F59
		public int ToIStream(IStream iStream, IntPtr iMessage, int ulFlags)
		{
			return SafeExRpcManageHandle.IExRpcManage_ToIStream(this.handle, iStream, iMessage, ulFlags);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00037D6C File Offset: 0x00035F6C
		public int CreateAddressBookEntryIdFromLegacyDN(string lpszLegacyDN, out byte[] entryID)
		{
			entryID = null;
			int num = 0;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num2 = SafeExRpcManageHandle.IExRpcManage_CreateAddressBookEntryIdFromLegacyDN(this.handle, lpszLegacyDN, out num, out safeExMemoryHandle);
				if (num2 == 0)
				{
					entryID = new byte[num];
					safeExMemoryHandle.CopyTo(entryID, 0, num);
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00037DC8 File Offset: 0x00035FC8
		public int CreateLegacyDNFromAddressBookEntryId(int cbEntryId, byte[] lpEntryID, out string lpszLegacyDN)
		{
			lpszLegacyDN = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num = SafeExRpcManageHandle.IExRpcManage_CreateLegacyDNFromAddressBookEntryId(this.handle, cbEntryId, lpEntryID, out safeExMemoryHandle);
				if (num == 0 && !safeExMemoryHandle.IsInvalid)
				{
					lpszLegacyDN = Marshal.PtrToStringAnsi(safeExMemoryHandle.DangerousGetHandle());
				}
				result = num;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00037E24 File Offset: 0x00036024
		public int GetEntryIdType(int cbEntryId, byte[] lpEntryID, out int ulType)
		{
			return SafeExRpcManageHandle.IExRpcManage_GetEntryIdType(this.handle, cbEntryId, lpEntryID, out ulType);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00037E34 File Offset: 0x00036034
		public int GetFolderEntryIdFromMessageEntryId(int cbMessageEntryId, byte[] lpMessageEntryID, out byte[] folderEntryId)
		{
			folderEntryId = null;
			int num = 0;
			SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
			int result;
			try
			{
				int num2 = SafeExRpcManageHandle.IExRpcManage_GetFolderEntryIdFromMessageEntryId(this.handle, cbMessageEntryId, lpMessageEntryID, out num, out safeExLinkedMemoryHandle);
				if (num2 == 0)
				{
					folderEntryId = new byte[num];
					safeExLinkedMemoryHandle.CopyTo(folderEntryId, 0, num);
				}
				result = num2;
			}
			finally
			{
				if (safeExLinkedMemoryHandle != null)
				{
					safeExLinkedMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00037E90 File Offset: 0x00036090
		public int CreateAddressBookEntryIdFromLocalDirectorySID(int cbSid, byte[] lpSid, out byte[] entryID)
		{
			entryID = null;
			int num = 0;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num2 = SafeExRpcManageHandle.IExRpcManage_CreateAddressBookEntryIdFromLocalDirectorySID(this.handle, cbSid, lpSid, out num, out safeExMemoryHandle);
				if (num2 == 0)
				{
					entryID = new byte[num];
					safeExMemoryHandle.CopyTo(entryID, 0, num);
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00037EEC File Offset: 0x000360EC
		public int CreateLocalDirectorySIDFromAddressBookEntryId(int cbEntryId, byte[] lpEntryID, out byte[] lpSid)
		{
			lpSid = null;
			int num = 0;
			SafeExMemoryHandle safeExMemoryHandle = null;
			int result;
			try
			{
				int num2 = SafeExRpcManageHandle.IExRpcManage_CreateLocalDirectorySIDFromAddressBookEntryId(this.handle, cbEntryId, lpEntryID, out num, out safeExMemoryHandle);
				if (num2 == 0)
				{
					lpSid = new byte[num];
					safeExMemoryHandle.CopyTo(lpSid, 0, lpSid.Length);
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00037F4C File Offset: 0x0003614C
		public int CreateIdSetBlobFromIStream(PropTag ptag, IStream iStream, out byte[] idSetBlob)
		{
			SafeExMemoryHandle safeExMemoryHandle = null;
			int num = 0;
			int result;
			try
			{
				int num2 = SafeExRpcManageHandle.IExRpcManage_CreateIdSetBlobFromIStream(this.handle, (int)ptag, iStream, out num, out safeExMemoryHandle);
				if (num > 0 && safeExMemoryHandle != null)
				{
					idSetBlob = new byte[num];
					Marshal.Copy(safeExMemoryHandle.DangerousGetHandle(), idSetBlob, 0, num);
				}
				else
				{
					idSetBlob = null;
				}
				result = num2;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F81 RID: 3969
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_Connect(IntPtr iExRpcManage, int ulConnectFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszServerDN, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, out SafeExRpcConnectionHandle iExRpcConnection);

		// Token: 0x06000F82 RID: 3970
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_ConnectEx(IntPtr iExRpcManage, int ulFlags, int ulConnectFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszServerDN, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, [MarshalAs(UnmanagedType.LPStr)] string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, out SafeExRpcConnectionHandle iExRpcConnection);

		// Token: 0x06000F83 RID: 3971
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_ConnectEx2(IntPtr iExRpcManage, int ulFlags, int ulConnectFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszServerDN, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbMdbGuid, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, [MarshalAs(UnmanagedType.LPStr)] string lpszHTTPProxyServerName, int ulConMod, int lcidString, int lcidSort, int cpid, int cReconnectIntervalInMins, int cbRpcBufferSize, int cbAuxBufferSize, int cbClientSessionInfoSize, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbClientSessionInfo, IntPtr pfnConnectDelegate, IntPtr pfnExecuteDelegate, IntPtr pfnDisconnectDelegate, int ulConnectionTimeoutMSecs, int ulCallTimeoutMSecs, out SafeExRpcConnectionHandle iExRpcConnection);

		// Token: 0x06000F84 RID: 3972
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_FromIStg(IntPtr iExRpcManage, IStorage iStorage, IntPtr iMessage);

		// Token: 0x06000F85 RID: 3973
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_ToIStg(IntPtr iExRpcManage, IStorage iStorage, IntPtr iMessage, int ulFlags);

		// Token: 0x06000F86 RID: 3974
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_AdminConnect(IntPtr iExRpcManage, [MarshalAs(UnmanagedType.LPStr)] string lpszClientId, [MarshalAs(UnmanagedType.LPStr)] string lpszServer, [MarshalAs(UnmanagedType.LPWStr)] string lpwszUser, [MarshalAs(UnmanagedType.LPWStr)] string lpwszDomain, [MarshalAs(UnmanagedType.LPWStr)] string lpwszPassword, out SafeExRpcAdminHandle iExRpcAdmin);

		// Token: 0x06000F87 RID: 3975
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_FromIStream(IntPtr iExRpcManage, IStream iStream, IntPtr iMessage);

		// Token: 0x06000F88 RID: 3976
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_ToIStream(IntPtr iExRpcManage, IStream iStream, IntPtr iMessage, int ulFlags);

		// Token: 0x06000F89 RID: 3977
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_CreateAddressBookEntryIdFromLegacyDN(IntPtr iExRpcManage, [MarshalAs(UnmanagedType.LPStr)] string lpszLegacyDN, out int lpcbEntryId, out SafeExMemoryHandle lppEntryId);

		// Token: 0x06000F8A RID: 3978
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_CreateLegacyDNFromAddressBookEntryId(IntPtr iExRpcManage, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out SafeExMemoryHandle lpszLegacyDN);

		// Token: 0x06000F8B RID: 3979
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_GetEntryIdType(IntPtr iExRpcManage, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int ulType);

		// Token: 0x06000F8C RID: 3980
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_GetFolderEntryIdFromMessageEntryId(IntPtr iExRpcManage, int cbMessageEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpMessageEntryID, out int lpcbFolderEntryId, out SafeExLinkedMemoryHandle lppFolderEntryId);

		// Token: 0x06000F8D RID: 3981
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_CreateAddressBookEntryIdFromLocalDirectorySID(IntPtr iExRpcManage, int cbSid, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpSid, out int lpcbEntryId, out SafeExMemoryHandle lppEntryId);

		// Token: 0x06000F8E RID: 3982
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_CreateLocalDirectorySIDFromAddressBookEntryId(IntPtr iExRpcManage, int cbEntryId, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] lpEntryID, out int lpcbSid, out SafeExMemoryHandle lpSid);

		// Token: 0x06000F8F RID: 3983
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcManage_CreateIdSetBlobFromIStream(IntPtr iExRpcManage, int ptag, IStream iStream, out int cbIdSetBlob, out SafeExMemoryHandle pbIdSetBlob);
	}
}
