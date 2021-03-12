using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000002 RID: 2
	internal class SafeRpcAsyncStateHandle : SafeHandleZeroIsInvalid
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00055EC8 File Offset: 0x000552C8
		public SafeRpcAsyncStateHandle(IntPtr handle)
		{
			try
			{
				base.SetHandle(handle);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00055EB4 File Offset: 0x000552B4
		public SafeRpcAsyncStateHandle()
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00055F08 File Offset: 0x00055308
		public IntPtr Detach()
		{
			IntPtr intPtr = Interlocked.CompareExchange(ref this.handle, IntPtr.Zero, this.handle);
			if (IntPtr.Zero != intPtr)
			{
				base.SetHandleAsInvalid();
			}
			return intPtr;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00055F48 File Offset: 0x00055348
		public unsafe void CompleteCall(int result)
		{
			if (!this.IsInvalid)
			{
				IntPtr intPtr = this.Detach();
				IntPtr intPtr2 = intPtr;
				if (IntPtr.Zero != intPtr)
				{
					int num = result;
					<Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)intPtr2.ToPointer(), (void*)(&num));
				}
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00055F90 File Offset: 0x00055390
		public unsafe void AbortCall(uint exceptionCode)
		{
			if (!this.IsInvalid)
			{
				IntPtr intPtr = this.Detach();
				IntPtr intPtr2 = intPtr;
				if (IntPtr.Zero != intPtr)
				{
					<Module>.RpcAsyncAbortCall((_RPC_ASYNC_STATE*)intPtr2.ToPointer(), exceptionCode);
				}
				if (this != null)
				{
					((IDisposable)this).Dispose();
				}
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00055FEC File Offset: 0x000553EC
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			if (this.IsInvalid)
			{
				return true;
			}
			this.AbortCall(1726U);
			return true;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00055FD4 File Offset: 0x000553D4
		private unsafe static int FilterAccessViolations(uint code, _EXCEPTION_POINTERS* ep)
		{
			return (code == 3221225477U) ? 1 : 0;
		}

		// Token: 0x04000813 RID: 2067
		private static FileVersionInfo rpcRuntimeVersionInfo;
	}
}
