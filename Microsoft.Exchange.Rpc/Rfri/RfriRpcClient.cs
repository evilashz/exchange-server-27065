using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Exchange.Rpc.Rfri
{
	// Token: 0x020003B9 RID: 953
	internal class RfriRpcClient : RpcClientBase
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0004EEF4 File Offset: 0x0004E2F4
		private unsafe _RPC_ASYNC_STATE* AsyncState
		{
			get
			{
				return (_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer();
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004F2EC File Offset: 0x0004E6EC
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate, string certificateSubjectName)
		{
			string servicePrincipalName;
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				servicePrincipalName = string.Format("exchangeRFR/{0}", instanceName);
			}
			else
			{
				servicePrincipalName = string.Format("msstd:{0}", instanceName);
			}
			base..ctor(machineName, proxyServer, protocolSequence, servicePrincipalName, true, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, certificateSubjectName);
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004F290 File Offset: 0x0004E690
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate) : base(machineName, proxyServer, protocolSequence, string.Format("exchangeRFR/{0}", instanceName), true, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, false)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004F240 File Offset: 0x0004E640
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate) : base(machineName, proxyServer, protocolSequence, true, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, false)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004F1E8 File Offset: 0x0004E5E8
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName) : base(machineName, proxyServer, protocolSequence, string.Format("exchangeRFR/{0}", instanceName), true, nc, httpAuthenticationScheme, authenticationService, true, false, false)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004F190 File Offset: 0x0004E590
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, [MarshalAs(UnmanagedType.U1)] bool useEncryption, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService) : base(machineName, proxyServer, protocolSequence, string.Format("exchangeRFR/{0}", machineName), useEncryption, nc, httpAuthenticationScheme, authenticationService, true, false, false)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004F148 File Offset: 0x0004E548
		public RfriRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc) : base(machineName, proxyServer, protocolSequence, nc, HttpAuthenticationScheme.Basic, AuthenticationService.Negotiate, true)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004F104 File Offset: 0x0004E504
		public RfriRpcClient(string machineName, string protocolSequence, NetworkCredential nc) : base(machineName, protocolSequence, nc, AuthenticationService.Negotiate)
		{
			try
			{
				this.Initialize();
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004EF14 File Offset: 0x0004E314
		private void ~RfriRpcClient()
		{
			IDisposable disposable = this.asyncState;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.asyncState = null;
			IDisposable disposable2 = this.rpcCompleteEvent;
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
			this.rpcCompleteEvent = null;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0004EF50 File Offset: 0x0004E350
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x0004EF68 File Offset: 0x0004E368
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0004EF7C File Offset: 0x0004E37C
		public int TotalRpcCounter
		{
			get
			{
				return this.totalRpcCounter;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0004EF90 File Offset: 0x0004E390
		public TimeSpan TotalRpcTime
		{
			get
			{
				return this.totalRpcTime;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004F360 File Offset: 0x0004E760
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetNewDSA(string userDN, out string server)
		{
			byte* ptr = null;
			SafeMarshalHGlobalHandle safeMarshalHGlobalHandle = new SafeMarshalHGlobalHandle(Marshal.StringToHGlobalAnsi(userDN));
			base.ResetRetryCounter();
			int result;
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeMarshalHGlobalHandle.DangerousGetHandle();
					<Module>.cli_RfrGetNewDSA(this.AsyncState, base.BindingHandle, 0, (byte*)intPtr.ToPointer(), null, &ptr);
					result = this.WaitForCompletion();
					this.totalRpcCounter++;
					DateTime utcNow2 = DateTime.UtcNow;
					TimeSpan ts = utcNow2 - utcNow;
					TimeSpan timeSpan = this.totalRpcTime.Add(ts);
					this.totalRpcTime = timeSpan;
				}
				catch when (endfilter(true))
				{
					this.totalRpcCounter++;
					DateTime utcNow3 = DateTime.UtcNow;
					TimeSpan ts2 = utcNow3 - utcNow;
					TimeSpan timeSpan2 = this.totalRpcTime.Add(ts2);
					this.totalRpcTime = timeSpan2;
					int exceptionCode = Marshal.GetExceptionCode();
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.CallCancelled | RpcRetryType.ServerBusy | RpcRetryType.ServerUnavailable | RpcRetryType.AccessDenied) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_RfrGetNewDSA");
				}
				finally
				{
					if (ptr == null)
					{
						server = null;
					}
					else
					{
						server = new string((sbyte*)ptr);
						<Module>.MIDL_user_free((void*)ptr);
					}
				}
				break;
			}
			if (safeMarshalHGlobalHandle != null)
			{
				((IDisposable)safeMarshalHGlobalHandle).Dispose();
			}
			return result;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004F4AC File Offset: 0x0004E8AC
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetFQDNFromLegacyDN(string serverDN, out string serverFQDN)
		{
			byte* ptr = null;
			SafeMarshalHGlobalHandle safeMarshalHGlobalHandle = new SafeMarshalHGlobalHandle(Marshal.StringToHGlobalAnsi(serverDN));
			void* ptr2 = safeMarshalHGlobalHandle.DangerousGetHandle().ToPointer();
			void* ptr3 = ptr2;
			if (*(sbyte*)ptr2 != 0)
			{
				do
				{
					ptr3 = (void*)((byte*)ptr3 + 1L);
				}
				while (*(sbyte*)ptr3 != 0);
			}
			int num = (int)(ptr3 - ptr2 + 1);
			base.ResetRetryCounter();
			int result;
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeMarshalHGlobalHandle.DangerousGetHandle();
					<Module>.cli_RfrGetFQDNFromLegacyDN(this.AsyncState, base.BindingHandle, 0, num, (byte*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion();
					this.totalRpcCounter++;
					DateTime utcNow2 = DateTime.UtcNow;
					TimeSpan ts = utcNow2 - utcNow;
					TimeSpan timeSpan = this.totalRpcTime.Add(ts);
					this.totalRpcTime = timeSpan;
				}
				catch when (endfilter(true))
				{
					this.totalRpcCounter++;
					DateTime utcNow3 = DateTime.UtcNow;
					TimeSpan ts2 = utcNow3 - utcNow;
					TimeSpan timeSpan2 = this.totalRpcTime.Add(ts2);
					this.totalRpcTime = timeSpan2;
					int exceptionCode = Marshal.GetExceptionCode();
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.CallCancelled | RpcRetryType.ServerBusy | RpcRetryType.ServerUnavailable | RpcRetryType.AccessDenied) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_RfrGetFQDNFromLegacyDN");
				}
				finally
				{
					if (ptr == null)
					{
						serverFQDN = null;
					}
					else
					{
						serverFQDN = new string((sbyte*)ptr);
						<Module>.MIDL_user_free((void*)ptr);
					}
				}
				break;
			}
			if (safeMarshalHGlobalHandle != null)
			{
				((IDisposable)safeMarshalHGlobalHandle).Dispose();
			}
			return result;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004EFA8 File Offset: 0x0004E3A8
		private unsafe void Initialize()
		{
			this.totalRpcCounter = 0;
			this.totalRpcTime = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.FromMinutes(5.0);
			this.timeout = timeSpan;
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeRpcMemoryHandle(112);
			this.asyncState = safeRpcMemoryHandle;
			int num = <Module>.RpcAsyncInitializeHandle((_RPC_ASYNC_STATE*)safeRpcMemoryHandle.DangerousGetHandle().ToPointer(), 112U);
			if (num != null)
			{
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, "RpcAsyncInitializeHandle");
			}
			*(long*)((byte*)this.asyncState.DangerousGetHandle().ToPointer() + 24L) = 0L;
			*(int*)((byte*)this.asyncState.DangerousGetHandle().ToPointer() + 44L) = 1;
			AutoResetEvent autoResetEvent = new AutoResetEvent(false);
			this.rpcCompleteEvent = autoResetEvent;
			IntPtr value = autoResetEvent.SafeWaitHandle.DangerousGetHandle();
			*(long*)((byte*)this.asyncState.DangerousGetHandle().ToPointer() + 48L) = (void*)value;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004F084 File Offset: 0x0004E484
		private unsafe int WaitForCompletion()
		{
			bool flag = false;
			if (!this.rpcCompleteEvent.WaitOne(this.timeout))
			{
				<Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer(), 1);
				this.rpcCompleteEvent.WaitOne();
				flag = true;
			}
			int result;
			int num = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer(), (void*)(&result));
			if (num != null)
			{
				<Module>.RpcRaiseException(flag ? 1460 : num);
			}
			return result;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004F628 File Offset: 0x0004EA28
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.~RfriRpcClient();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000FCE RID: 4046
		private AutoResetEvent rpcCompleteEvent;

		// Token: 0x04000FCF RID: 4047
		private SafeRpcMemoryHandle asyncState;

		// Token: 0x04000FD0 RID: 4048
		private TimeSpan timeout;

		// Token: 0x04000FD1 RID: 4049
		private int totalRpcCounter;

		// Token: 0x04000FD2 RID: 4050
		private TimeSpan totalRpcTime;
	}
}
