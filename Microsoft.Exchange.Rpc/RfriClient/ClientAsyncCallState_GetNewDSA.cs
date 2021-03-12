using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc.RfriClient
{
	// Token: 0x020003A5 RID: 933
	internal class ClientAsyncCallState_GetNewDSA : ClientAsyncCallState
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0004C380 File Offset: 0x0004B780
		private void Cleanup()
		{
			if (this.m_szUserDN != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_szUserDN);
				this.m_szUserDN = IntPtr.Zero;
			}
			if (this.m_pszServer != IntPtr.Zero)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszServer);
				IntPtr intPtr2 = intPtr;
				if (intPtr != IntPtr.Zero)
				{
					<Module>.MIDL_user_free(intPtr2.ToPointer());
				}
				Marshal.FreeHGlobal(this.m_pszServer);
				this.m_pszServer = IntPtr.Zero;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004C610 File Offset: 0x0004BA10
		public ClientAsyncCallState_GetNewDSA(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, RfriGetNewDSAFlags flags, string userDn) : base("GetNewDSA", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = bindingHandle;
				this.m_flags = flags;
				this.m_szUserDN = IntPtr.Zero;
				this.m_pszServer = IntPtr.Zero;
				bool flag = false;
				try
				{
					IntPtr szUserDN = Marshal.StringToHGlobalAnsi(userDn);
					this.m_szUserDN = szUserDN;
					IntPtr pszServer = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pszServer = pszServer;
					Marshal.WriteIntPtr(this.m_pszServer, IntPtr.Zero);
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

		// Token: 0x06001067 RID: 4199 RVA: 0x0004C420 File Offset: 0x0004B820
		private void ~ClientAsyncCallState_GetNewDSA()
		{
			this.Cleanup();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004C434 File Offset: 0x0004B834
		public unsafe override void InternalBegin()
		{
			<Module>.cli_RfrGetNewDSA((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), this.m_flags, (byte*)this.m_szUserDN.ToPointer(), null, (byte**)this.m_pszServer.ToPointer());
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004C6D8 File Offset: 0x0004BAD8
		public RfriStatus End(out string server)
		{
			RfriStatus result;
			try
			{
				RfriStatus rfriStatus = (RfriStatus)base.CheckCompletion();
				server = null;
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszServer);
				if (intPtr != IntPtr.Zero)
				{
					server = Marshal.PtrToStringAnsi(intPtr);
				}
				result = rfriStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004C740 File Offset: 0x0004BB40
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

		// Token: 0x04000F97 RID: 3991
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000F98 RID: 3992
		private RfriGetNewDSAFlags m_flags;

		// Token: 0x04000F99 RID: 3993
		private IntPtr m_szUserDN;

		// Token: 0x04000F9A RID: 3994
		private IntPtr m_pszServer;
	}
}
