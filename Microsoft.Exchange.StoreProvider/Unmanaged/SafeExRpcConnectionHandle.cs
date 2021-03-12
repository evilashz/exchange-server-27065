using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002CC RID: 716
	[ComVisible(false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SafeExRpcConnectionHandle : SafeExInterfaceHandle, IExRpcConnectionInterface, IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00037AC9 File Offset: 0x00035CC9
		protected SafeExRpcConnectionHandle()
		{
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00037AD1 File Offset: 0x00035CD1
		internal SafeExRpcConnectionHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00037ADA File Offset: 0x00035CDA
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SafeExRpcConnectionHandle>(this);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00037AE4 File Offset: 0x00035CE4
		public int OpenMsgStore(int ulFlags, long ullOpenFlags, string lpszMailboxDN, byte[] pbMailboxGuid, byte[] pbMdbGuid, out string lppszWrongServerDN, IntPtr hToken, byte[] pSidUser, byte[] pSidPrimaryGroup, string lpszUserDN, int ulLcidString, int ulLcidSort, int ulCpid, bool fUnifiedLogon, string lpszApplicationId, byte[] pbTenantHint, int cbTenantHint, out IExMapiStore iMsgStore)
		{
			lppszWrongServerDN = null;
			SafeExMemoryHandle safeExMemoryHandle = null;
			SafeExMapiStoreHandle safeExMapiStoreHandle = null;
			int result;
			try
			{
				int num = SafeExRpcConnectionHandle.IExRpcConnection_OpenMsgStore(this.handle, ulFlags, ullOpenFlags, lpszMailboxDN, pbMailboxGuid, pbMdbGuid, out safeExMemoryHandle, hToken, pSidUser, pSidPrimaryGroup, lpszUserDN, ulLcidString, ulLcidSort, ulCpid, fUnifiedLogon, lpszApplicationId, pbTenantHint, cbTenantHint, out safeExMapiStoreHandle);
				if (safeExMemoryHandle != null && !safeExMemoryHandle.IsInvalid)
				{
					lppszWrongServerDN = Marshal.PtrToStringAnsi(safeExMemoryHandle.DangerousGetHandle());
				}
				iMsgStore = safeExMapiStoreHandle;
				safeExMapiStoreHandle = null;
				result = num;
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
				safeExMapiStoreHandle.DisposeIfValid();
			}
			return result;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00037B6C File Offset: 0x00035D6C
		public int SendAuxBuffer(int ulFlags, int cbAuxBuffer, byte[] pbAuxBuffer, int fForceSend)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_SendAuxBuffer(this.handle, ulFlags, cbAuxBuffer, pbAuxBuffer, fForceSend);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00037B7E File Offset: 0x00035D7E
		public int FlushRPCBuffer(bool fForceSend)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_FlushRPCBuffer(this.handle, fForceSend);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00037B8C File Offset: 0x00035D8C
		public int GetServerVersion(out int pulVersionMajor, out int pulVersionMinor, out int pulBuildMajor, out int pulBuildMinor)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_GetServerVersion(this.handle, out pulVersionMajor, out pulVersionMinor, out pulBuildMajor, out pulBuildMinor);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00037B9E File Offset: 0x00035D9E
		public int IsDead(out bool pfDead)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_IsDead(this.handle, out pfDead);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00037BAC File Offset: 0x00035DAC
		public int RpcSentToServer(out bool pfRpcSent)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_RpcSentToServer(this.handle, out pfRpcSent);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00037BBA File Offset: 0x00035DBA
		public int IsMapiMT(out bool pfMapiMT)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_IsMapiMT(this.handle, out pfMapiMT);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00037BC8 File Offset: 0x00035DC8
		public int IsConnectedToMapiServer(out bool pfConnectedToMapiServer)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_IsConnectedToMapiServer(this.handle, out pfConnectedToMapiServer);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00037BD6 File Offset: 0x00035DD6
		public void ClearStorePerRPCStats()
		{
			SafeExRpcConnectionHandle.IExRpcConnection_ClearStorePerRPCStats(this.handle);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00037BE3 File Offset: 0x00035DE3
		public uint GetStorePerRPCStats(out PerRpcStats pPerRpcPerformanceStatistics)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_GetStorePerRPCStats(this.handle, out pPerRpcPerformanceStatistics);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00037BF1 File Offset: 0x00035DF1
		public void ClearRPCStats()
		{
			SafeExRpcConnectionHandle.IExRpcConnection_ClearRPCStats(this.handle);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00037BFE File Offset: 0x00035DFE
		public int GetRPCStats(out RpcStats pRpcStats)
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_GetRPCStats(this.handle, out pRpcStats);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00037C0C File Offset: 0x00035E0C
		public int SetInternalAccess()
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_SetInternalAccess(this.handle);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00037C19 File Offset: 0x00035E19
		public int ClearInternalAccess()
		{
			return SafeExRpcConnectionHandle.IExRpcConnection_ClearInternalAccess(this.handle);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00037C26 File Offset: 0x00035E26
		public void CheckForNotifications()
		{
			SafeExRpcConnectionHandle.IExRpcConnection_CheckForNotifications(this.handle);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00037C33 File Offset: 0x00035E33
		protected override void InternalReleaseHandle()
		{
			SafeExRpcConnectionHandle.IExRpcConnection_PrepareForRelease(this.handle);
		}

		// Token: 0x06000F5F RID: 3935
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_OpenMsgStore(IntPtr iExRpcConnection, int ulFlags, long ullOpenFlags, [MarshalAs(UnmanagedType.LPStr)] string lpszMailboxDN, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbMailboxGuid, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbMdbGuid, out SafeExMemoryHandle lppszWrongServerDN, IntPtr hToken, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSidUser, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pSidPrimaryGroup, [MarshalAs(UnmanagedType.LPStr)] string lpszUserDN, int ulLcidString, int ulLcidSort, int ulCpid, [MarshalAs(UnmanagedType.Bool)] bool fUnifiedLogon, [MarshalAs(UnmanagedType.LPStr)] string lpszApplicationId, [MarshalAs(UnmanagedType.LPArray)] byte[] pbTenantHint, int cbTenantHint, out SafeExMapiStoreHandle iMsgStore);

		// Token: 0x06000F60 RID: 3936
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_SendAuxBuffer(IntPtr iExRpcConnection, int ulFlags, int cbAuxBuffer, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbAuxBuffer, int fForceSend);

		// Token: 0x06000F61 RID: 3937
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_FlushRPCBuffer(IntPtr iExRpcConnection, [MarshalAs(UnmanagedType.Bool)] bool fForceSend);

		// Token: 0x06000F62 RID: 3938
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_GetServerVersion(IntPtr iExRpcConnection, out int pulVersionMajor, out int pulVersionMinor, out int pulBuildMajor, out int pulBuildMinor);

		// Token: 0x06000F63 RID: 3939
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_IsDead(IntPtr iExRpcConnection, [MarshalAs(UnmanagedType.Bool)] out bool pfDead);

		// Token: 0x06000F64 RID: 3940
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_RpcSentToServer(IntPtr iExRpcConnection, [MarshalAs(UnmanagedType.Bool)] out bool pfRpcSent);

		// Token: 0x06000F65 RID: 3941
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_IsMapiMT(IntPtr iExRpcConnection, [MarshalAs(UnmanagedType.Bool)] out bool pfMapiMT);

		// Token: 0x06000F66 RID: 3942
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_IsConnectedToMapiServer(IntPtr iExRpcConnection, [MarshalAs(UnmanagedType.Bool)] out bool pfConnectedToMapiServer);

		// Token: 0x06000F67 RID: 3943
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void IExRpcConnection_ClearStorePerRPCStats(IntPtr iExRpcConnection);

		// Token: 0x06000F68 RID: 3944
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern uint IExRpcConnection_GetStorePerRPCStats(IntPtr iExRpcConnection, out PerRpcStats pPerRpcPerformanceStatistics);

		// Token: 0x06000F69 RID: 3945
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void IExRpcConnection_ClearRPCStats(IntPtr iExRpcConnection);

		// Token: 0x06000F6A RID: 3946
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_GetRPCStats(IntPtr iExRpcConnection, out RpcStats pRpcStats);

		// Token: 0x06000F6B RID: 3947
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_SetInternalAccess(IntPtr iExRpcConnection);

		// Token: 0x06000F6C RID: 3948
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern int IExRpcConnection_ClearInternalAccess(IntPtr iExRpcConnection);

		// Token: 0x06000F6D RID: 3949
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void IExRpcConnection_PrepareForRelease(IntPtr iExRpcConnection);

		// Token: 0x06000F6E RID: 3950
		[DllImport("exrpc32.dll", ExactSpelling = true)]
		private static extern void IExRpcConnection_CheckForNotifications(IntPtr iExRpcConnection);
	}
}
