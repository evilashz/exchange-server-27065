using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc.RfriClient
{
	// Token: 0x020003A6 RID: 934
	internal class ClientAsyncCallState_GetFQDNFromLegacyDN : ClientAsyncCallState
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x0004C480 File Offset: 0x0004B880
		private void Cleanup()
		{
			if (this.m_szServerDN != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_szServerDN);
				this.m_szServerDN = IntPtr.Zero;
			}
			if (this.m_pszServerFQDN != IntPtr.Zero)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszServerFQDN);
				IntPtr intPtr2 = intPtr;
				if (intPtr != IntPtr.Zero)
				{
					<Module>.MIDL_user_free(intPtr2.ToPointer());
				}
				Marshal.FreeHGlobal(this.m_pszServerFQDN);
				this.m_pszServerFQDN = IntPtr.Zero;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004C784 File Offset: 0x0004BB84
		public ClientAsyncCallState_GetFQDNFromLegacyDN(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, RfriGetFQDNFromLegacyDNFlags flags, string serverDn) : base("GetFQDNFromLegacyDN", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = bindingHandle;
				this.m_flags = flags;
				this.m_szServerDN = IntPtr.Zero;
				this.m_pszServerFQDN = IntPtr.Zero;
				bool flag = false;
				try
				{
					IntPtr szServerDN = Marshal.StringToHGlobalAnsi(serverDn);
					this.m_szServerDN = szServerDN;
					IntPtr pszServerFQDN = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pszServerFQDN = pszServerFQDN;
					Marshal.WriteIntPtr(this.m_pszServerFQDN, IntPtr.Zero);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004C520 File Offset: 0x0004B920
		private void ~ClientAsyncCallState_GetFQDNFromLegacyDN()
		{
			this.Cleanup();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004C534 File Offset: 0x0004B934
		public unsafe override void InternalBegin()
		{
			void* ptr = this.m_szServerDN.ToPointer();
			void* ptr2 = ptr;
			if (*(sbyte*)ptr != 0)
			{
				do
				{
					ptr2 = (void*)((byte*)ptr2 + 1L);
				}
				while (*(sbyte*)ptr2 != 0);
			}
			long num = (long)(ptr2 - ptr);
			<Module>.cli_RfrGetFQDNFromLegacyDN((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), this.m_flags, (uint)(num + 1L), (byte*)this.m_szServerDN.ToPointer(), (byte**)this.m_pszServerFQDN.ToPointer());
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004C84C File Offset: 0x0004BC4C
		public RfriStatus End(out string serverFqdn)
		{
			RfriStatus result;
			try
			{
				RfriStatus rfriStatus = (RfriStatus)base.CheckCompletion();
				serverFqdn = null;
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszServerFQDN);
				if (intPtr != IntPtr.Zero)
				{
					serverFqdn = Marshal.PtrToStringAnsi(intPtr);
				}
				result = rfriStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004C8B4 File Offset: 0x0004BCB4
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.Cleanup();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000F9B RID: 3995
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000F9C RID: 3996
		private RfriGetFQDNFromLegacyDNFlags m_flags;

		// Token: 0x04000F9D RID: 3997
		private IntPtr m_szServerDN;

		// Token: 0x04000F9E RID: 3998
		private IntPtr m_pszServerFQDN;
	}
}
