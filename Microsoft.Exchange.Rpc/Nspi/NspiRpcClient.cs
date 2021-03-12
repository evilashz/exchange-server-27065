using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Exchange.Rpc.Nspi
{
	// Token: 0x02000367 RID: 871
	internal class NspiRpcClient : RpcClientBase
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000440F8 File Offset: 0x000434F8
		private unsafe _RPC_ASYNC_STATE* AsyncState
		{
			get
			{
				return (_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer();
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0004461C File Offset: 0x00043A1C
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate, string certificateSubjectName, [MarshalAs(UnmanagedType.U1)] bool useEncryption)
		{
			string servicePrincipalName;
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				servicePrincipalName = string.Format("exchangeAB/{0}", instanceName);
			}
			else
			{
				servicePrincipalName = string.Format("msstd:{0}", instanceName);
			}
			base..ctor(machineName, proxyServer, protocolSequence, servicePrincipalName, useEncryption, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, certificateSubjectName);
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

		// Token: 0x06000F99 RID: 3993 RVA: 0x000445A8 File Offset: 0x000439A8
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate, string certificateSubjectName)
		{
			string servicePrincipalName;
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				servicePrincipalName = string.Format("exchangeAB/{0}", instanceName);
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

		// Token: 0x06000F9A RID: 3994 RVA: 0x0004454C File Offset: 0x0004394C
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate) : base(machineName, proxyServer, protocolSequence, string.Format("exchangeAB/{0}", instanceName), true, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, false)
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

		// Token: 0x06000F9B RID: 3995 RVA: 0x000444F4 File Offset: 0x000438F4
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, string instanceName) : base(machineName, proxyServer, protocolSequence, string.Format("exchangeAB/{0}", instanceName), true, nc, httpAuthenticationScheme, authenticationService, true)
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

		// Token: 0x06000F9C RID: 3996 RVA: 0x000444A4 File Offset: 0x000438A4
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService, [MarshalAs(UnmanagedType.U1)] bool ignoreInvalidServerCertificate) : base(machineName, proxyServer, protocolSequence, true, nc, httpAuthenticationScheme, authenticationService, true, ignoreInvalidServerCertificate, false)
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

		// Token: 0x06000F9D RID: 3997 RVA: 0x00044458 File Offset: 0x00043858
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc, HttpAuthenticationScheme httpAuthenticationScheme, AuthenticationService authenticationService) : base(machineName, proxyServer, protocolSequence, nc, httpAuthenticationScheme, authenticationService, true)
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

		// Token: 0x06000F9E RID: 3998 RVA: 0x00044410 File Offset: 0x00043810
		public NspiRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential nc) : base(machineName, proxyServer, protocolSequence, nc, HttpAuthenticationScheme.Basic, AuthenticationService.Negotiate, true)
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

		// Token: 0x06000F9F RID: 3999 RVA: 0x000443CC File Offset: 0x000437CC
		public NspiRpcClient(string machineName, string protocolSequence, NetworkCredential nc) : base(machineName, protocolSequence, nc, AuthenticationService.Negotiate)
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

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00044388 File Offset: 0x00043788
		public NspiRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
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

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00044348 File Offset: 0x00043748
		public NspiRpcClient(string machineName) : base(machineName)
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00044118 File Offset: 0x00043518
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00044130 File Offset: 0x00043530
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00044144 File Offset: 0x00043544
		public int TotalRpcCounter
		{
			get
			{
				return this.totalRpcCounter;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00044158 File Offset: 0x00043558
		public TimeSpan TotalRpcTime
		{
			get
			{
				return this.totalRpcTime;
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00044694 File Offset: 0x00043A94
		[HandleProcessCorruptedStateExceptions]
		public unsafe int Bind(uint flags, IntPtr stat, IntPtr guid)
		{
			int num = -2147467259;
			void* ptr = null;
			ref void* void*& = ref ptr;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					try
					{
						<Module>.cli_NspiBind(this.AsyncState, base.BindingHandle, flags, (__MIDL_nspi_0002*)stat.ToPointer(), (__MIDL_nspi_0001*)guid.ToPointer(), ref void*&);
						num = this.WaitForCompletion<int>();
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
						<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiBind");
					}
					if (num == null)
					{
						this.contextHandle = ptr;
					}
				}
				finally
				{
				}
				break;
			}
			return num;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000447C4 File Offset: 0x00043BC4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int Unbind()
		{
			int result = 2;
			ref void* void*& = ref this.contextHandle;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiUnbind(this.AsyncState, ref void*&, 0);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiUnbind");
				}
				finally
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000448D0 File Offset: 0x00043CD0
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetHierarchyInfo(uint ulFlags, IntPtr stat, ref uint lpVersion, out SafeRpcMemoryHandle pHierTabRows)
		{
			int result = -2147467259;
			uint num = lpVersion;
			_SRowSet_r* ptr = null;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiGetHierarchyInfo(this.AsyncState, this.contextHandle, ulFlags, (__MIDL_nspi_0002*)stat.ToPointer(), &num, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiGetHierarchyInfo");
				}
				finally
				{
				}
				break;
			}
			lpVersion = num;
			if (ptr == null)
			{
				pHierTabRows = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				pHierTabRows = new SafeSRowSetHandle(handle);
			}
			return result;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004536C File Offset: 0x0004476C
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetMatches(IntPtr stat, IntPtr restriction, IntPtr propnames, int maxRows, int[] propTags, out int[] mids, out SafeRpcMemoryHandle rowset)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			_SPropTagArray_r* ptr2 = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiGetMatches(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), null, 0, (_SRestriction_r*)restriction.ToPointer(), null, maxRows, &ptr2, (_SPropTagArray_r*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiGetMatches");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (ptr == null)
			{
				rowset = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				rowset = new SafeSRowSetHandle(handle);
			}
			if (ptr2 == null)
			{
				mids = null;
			}
			else
			{
				mids = NspiHelper.ConvertPropTagArrayToIntArray(ptr2);
			}
			return result;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000454EC File Offset: 0x000448EC
		[HandleProcessCorruptedStateExceptions]
		public unsafe int QueryRows(uint flags, IntPtr stat, int[] mids, int count, int[] propTags, out SafeRpcMemoryHandle rowset)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = new SafeRpcMemoryHandle();
			int num = 0;
			if (mids != null)
			{
				int num2 = mids.Length;
				if (num2 != 0)
				{
					num = num2;
					safeRpcMemoryHandle2.Allocate((ulong)((long)num * 4L));
					int* value = (int*)safeRpcMemoryHandle2.DangerousGetHandle().ToPointer();
					IntPtr destination = new IntPtr((void*)value);
					Marshal.Copy(mids, 0, destination, num);
				}
			}
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					IntPtr intPtr2 = safeRpcMemoryHandle2.DangerousGetHandle();
					<Module>.cli_NspiQueryRows(this.AsyncState, this.contextHandle, flags, (__MIDL_nspi_0002*)stat.ToPointer(), num, (uint*)intPtr2.ToPointer(), count, (_SPropTagArray_r*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiQueryRows");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (safeRpcMemoryHandle2 != null)
			{
				((IDisposable)safeRpcMemoryHandle2).Dispose();
			}
			if (ptr == null)
			{
				rowset = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				rowset = new SafeSRowSetHandle(handle);
			}
			return result;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00044A10 File Offset: 0x00043E10
		[HandleProcessCorruptedStateExceptions]
		public unsafe int DNToEph(string[] DNs, out int[] mids)
		{
			int result = -2147467259;
			SafeStringArrayHandle safeStringArrayHandle = new SafeStringArrayHandle(DNs, true);
			_SPropTagArray_r* ptr = null;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeStringArrayHandle.DangerousGetHandle();
					<Module>.cli_NspiDNToEph(this.AsyncState, this.contextHandle, 0, (_StringsArray*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiDNToEph");
				}
				finally
				{
				}
				break;
			}
			if (ptr == null)
			{
				mids = null;
			}
			else
			{
				mids = NspiHelper.ConvertPropTagArrayToIntArray(ptr);
			}
			if (safeStringArrayHandle != null)
			{
				((IDisposable)safeStringArrayHandle).Dispose();
			}
			return result;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000456AC File Offset: 0x00044AAC
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ResolveNames(IntPtr stat, int[] propTags, byte[][] names, out int[] results, out SafeRpcMemoryHandle rowset)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			_SPropTagArray_r* ptr2 = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			SafeStringArrayHandle safeStringArrayHandle = new SafeStringArrayHandle(names);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeStringArrayHandle.DangerousGetHandle();
					IntPtr intPtr2 = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiResolveNames(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropTagArray_r*)intPtr2.ToPointer(), (_StringsArray*)intPtr.ToPointer(), &ptr2, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiResolveNames");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (safeStringArrayHandle != null)
			{
				((IDisposable)safeStringArrayHandle).Dispose();
			}
			if (ptr == null)
			{
				rowset = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				rowset = new SafeSRowSetHandle(handle);
			}
			if (ptr2 == null)
			{
				results = null;
			}
			else
			{
				results = NspiHelper.ConvertPropTagArrayToIntArray(ptr2);
			}
			return result;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00045840 File Offset: 0x00044C40
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ResolveNames(IntPtr stat, int[] propTags, string[] names, out int[] results, out SafeRpcMemoryHandle rowset)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			_SPropTagArray_r* ptr2 = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			SafeStringArrayHandle safeStringArrayHandle = new SafeStringArrayHandle(names, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeStringArrayHandle.DangerousGetHandle();
					IntPtr intPtr2 = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiResolveNamesW(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropTagArray_r*)intPtr2.ToPointer(), (_WStringsArray*)intPtr.ToPointer(), &ptr2, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiResolveNamesW");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (safeStringArrayHandle != null)
			{
				((IDisposable)safeStringArrayHandle).Dispose();
			}
			if (ptr == null)
			{
				rowset = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				rowset = new SafeSRowSetHandle(handle);
			}
			if (ptr2 == null)
			{
				results = null;
			}
			else
			{
				results = NspiHelper.ConvertPropTagArrayToIntArray(ptr2);
			}
			return result;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000459D4 File Offset: 0x00044DD4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetProps(uint flags, IntPtr stat, int[] propTags, out SafeRpcMemoryHandle row)
		{
			int result = -2147467259;
			_SRow_r* ptr = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiGetProps(this.AsyncState, this.contextHandle, flags, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropTagArray_r*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiGetProps");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (ptr == null)
			{
				row = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				row = new SafeSRowHandle(handle);
			}
			return result;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00045B2C File Offset: 0x00044F2C
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ResortRestriction(IntPtr stat, int[] inMidlist, out int[] outMidlist)
		{
			int result = -2147467259;
			_SPropTagArray_r* ptr = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(inMidlist, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiResortRestriction(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropTagArray_r*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiResortRestriction");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (ptr == null)
			{
				outMidlist = null;
			}
			else
			{
				outMidlist = NspiHelper.ConvertPropTagArrayToIntArray(ptr);
			}
			return result;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00044C74 File Offset: 0x00044074
		[HandleProcessCorruptedStateExceptions]
		public unsafe int UpdateStat(IntPtr stat)
		{
			int result = -2147467259;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiUpdateStat(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), null);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiUpdateStat");
				}
				finally
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00044B58 File Offset: 0x00043F58
		[HandleProcessCorruptedStateExceptions]
		public unsafe int UpdateStat(IntPtr stat, out int returnedDelta)
		{
			int result = -2147467259;
			int num = 0;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiUpdateStat(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), &num);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiUpdateStat");
				}
				finally
				{
				}
				break;
			}
			returnedDelta = num;
			return result;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00044D88 File Offset: 0x00044188
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetPropList(uint flags, int mid, int codepage, out int[] propTags)
		{
			int result = -2147467259;
			_SPropTagArray_r* ptr = null;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiGetPropList(this.AsyncState, this.contextHandle, flags, mid, codepage, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_GetPropList");
				}
				finally
				{
				}
				break;
			}
			if (ptr == null)
			{
				propTags = null;
			}
			else
			{
				propTags = NspiHelper.ConvertPropTagArrayToIntArray(ptr);
			}
			return result;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00044EB0 File Offset: 0x000442B0
		[HandleProcessCorruptedStateExceptions]
		public unsafe int CompareMids(IntPtr stat, int mid1, int mid2, out int result)
		{
			int result2 = -2147467259;
			int num = 0;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiCompareDNTs(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), mid1, mid2, &num);
					result2 = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiCompareDNTs");
				}
				finally
				{
				}
				break;
			}
			result = num;
			return result2;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00045C78 File Offset: 0x00045078
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ModProps(IntPtr stat, int[] propTags, IntPtr row)
		{
			int result = -2147467259;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiModProps(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropTagArray_r*)intPtr.ToPointer(), (_SRow_r*)row.ToPointer());
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiModProps");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			return result;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00044FD0 File Offset: 0x000443D0
		[HandleProcessCorruptedStateExceptions]
		public unsafe int GetTemplateInfo(uint flags, uint type, string dn, uint codePage, uint localeId, out SafeRpcMemoryHandle row)
		{
			int result = -2147467259;
			_SRow_r* ptr = null;
			IntPtr hglobal = Marshal.StringToHGlobalAnsi(dn);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiGetTemplateInfo(this.AsyncState, this.contextHandle, flags, type, (sbyte*)hglobal.ToPointer(), codePage, localeId, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiGetTemplateInfo");
				}
				finally
				{
					Marshal.FreeHGlobal(hglobal);
				}
				break;
			}
			if (ptr == null)
			{
				row = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				row = new SafeSRowHandle(handle);
			}
			return result;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00045118 File Offset: 0x00044518
		[HandleProcessCorruptedStateExceptions]
		public unsafe int QueryColumns(int clientFlags, out int[] columns)
		{
			int result = -2147467259;
			_SPropTagArray_r* ptr = null;
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					<Module>.cli_NspiQueryColumns(this.AsyncState, this.contextHandle, 0, clientFlags, &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiQueryColumns");
				}
				finally
				{
				}
				break;
			}
			if (ptr == null)
			{
				columns = null;
			}
			else
			{
				columns = NspiHelper.ConvertPropTagArrayToIntArray(ptr);
			}
			return result;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004523C File Offset: 0x0004463C
		[HandleProcessCorruptedStateExceptions]
		public unsafe int ModLinkAtt(uint flags, int propTag, int mid, byte[][] entryIDs)
		{
			int result = -2147467259;
			SafeRpcMemoryHandle safeRpcMemoryHandle = new SafeByteArraysHandle(entryIDs);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					<Module>.cli_NspiModLinkAtt(this.AsyncState, this.contextHandle, flags, propTag, mid, (_SBinaryArray_r*)intPtr.ToPointer());
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiModLinkAtt");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			return result;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00045DB4 File Offset: 0x000451B4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int SeekEntries(IntPtr stat, IntPtr propValue, int[] mids, int[] propTags, out SafeRpcMemoryHandle rowset)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(propTags, false);
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = NspiHelper.ConvertIntArrayToPropTagArray(mids, false);
			base.ResetRetryCounter();
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					IntPtr intPtr = safeRpcMemoryHandle.DangerousGetHandle();
					IntPtr intPtr2 = safeRpcMemoryHandle2.DangerousGetHandle();
					<Module>.cli_NspiSeekEntries(this.AsyncState, this.contextHandle, 0, (__MIDL_nspi_0002*)stat.ToPointer(), (_SPropValue_r*)propValue.ToPointer(), (_SPropTagArray_r*)intPtr2.ToPointer(), (_SPropTagArray_r*)intPtr.ToPointer(), &ptr);
					result = this.WaitForCompletion<int>();
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
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.ServerBusy) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_NspiSeekEntries");
				}
				finally
				{
				}
				break;
			}
			if (safeRpcMemoryHandle != null)
			{
				((IDisposable)safeRpcMemoryHandle).Dispose();
			}
			if (safeRpcMemoryHandle2 != null)
			{
				((IDisposable)safeRpcMemoryHandle2).Dispose();
			}
			if (ptr == null)
			{
				rowset = null;
			}
			else
			{
				IntPtr handle = new IntPtr((void*)ptr);
				rowset = new SafeSRowSetHandle(handle);
			}
			return result;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00044170 File Offset: 0x00043570
		private void ~NspiRpcClient()
		{
			IDisposable disposable = this.asyncState;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			IDisposable disposable2 = this.rpcCompleteEvent;
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000441A0 File Offset: 0x000435A0
		private unsafe void Initialize()
		{
			this.totalRpcCounter = 0;
			this.totalRpcTime = TimeSpan.Zero;
			this.contextHandle = null;
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

		// Token: 0x06000FBB RID: 4027 RVA: 0x00044284 File Offset: 0x00043684
		private unsafe type WaitForCompletion<type>()
		{
			bool flag = false;
			if (!this.rpcCompleteEvent.WaitOne(this.timeout))
			{
				<Module>.RpcAsyncCancelCall((_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer(), 1);
				this.rpcCompleteEvent.WaitOne();
				flag = true;
			}
			type result;
			int num = <Module>.RpcAsyncCompleteCall((_RPC_ASYNC_STATE*)this.asyncState.DangerousGetHandle().ToPointer(), (void*)(&result));
			if (num != null)
			{
				<Module>.RpcRaiseException(flag ? 1460 : num);
			}
			return result;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00044304 File Offset: 0x00043704
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.~NspiRpcClient();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000F47 RID: 3911
		private unsafe void* contextHandle;

		// Token: 0x04000F48 RID: 3912
		private AutoResetEvent rpcCompleteEvent;

		// Token: 0x04000F49 RID: 3913
		private SafeRpcMemoryHandle asyncState;

		// Token: 0x04000F4A RID: 3914
		private TimeSpan timeout;

		// Token: 0x04000F4B RID: 3915
		private int totalRpcCounter;

		// Token: 0x04000F4C RID: 3916
		private TimeSpan totalRpcTime;
	}
}
