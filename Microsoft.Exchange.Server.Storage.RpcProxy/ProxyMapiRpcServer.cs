using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.MapiDisp;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000008 RID: 8
	internal class ProxyMapiRpcServer : IProxyServer
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00005590 File Offset: 0x00003790
		internal ProxyMapiRpcServer(IPoolSessionManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000055A0 File Offset: 0x000037A0
		public int EcDoConnectEx(ClientSecurityContext callerSecurityContext, out IntPtr contextHandle, string userDn, uint flags, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, out uint maximumPolls, out uint retryCount, out uint retryDelay, out string displayName, short[] clientVersion, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ProxyMapiRpcServer.TraceStartRpcMarker("EcDoConnectEx", IntPtr.Zero);
			ErrorCode errorCode = ErrorCode.NoError;
			uint num = 0U;
			contextHandle = IntPtr.Zero;
			displayName = "ProxySession";
			maximumPolls = (uint)MapiRpc.PollsMaxDefault.TotalMilliseconds;
			retryCount = 60U;
			retryDelay = (uint)MapiRpc.RetryDelayDefault.TotalMilliseconds;
			auxiliaryOut = null;
			try
			{
				errorCode = this.manager.CreateProxySession(callerSecurityContext, flags, userDn, connectionMode, codePageId, localeIdString, localeIdSort, clientVersion, auxiliaryIn, null, out num, out auxiliaryOut);
				if (errorCode == ErrorCode.NoError)
				{
					ErrorHelper.AssertRetail(0U == (num & 2147483648U), "Session handle value overlaps with asyc handle flag.");
					contextHandle = new IntPtr((int)num);
					num = 0U;
				}
			}
			finally
			{
				if (num != 0U)
				{
					this.manager.CloseSession(num);
				}
				ProxyMapiRpcServer.TraceEndRpcMarker("EcDoConnectEx", contextHandle, errorCode);
			}
			return (int)errorCode;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000568C File Offset: 0x0000388C
		public int EcDoDisconnect(ref IntPtr contextHandle)
		{
			ErrorCode noError = ErrorCode.NoError;
			ProxyMapiRpcServer.TraceStartRpcMarker("EcDoDisconnect", contextHandle);
			int result;
			try
			{
				uint sessionHandle = (uint)contextHandle.ToInt32();
				this.manager.CloseSession(sessionHandle);
				contextHandle = IntPtr.Zero;
				result = (int)noError;
			}
			finally
			{
				ProxyMapiRpcServer.TraceEndRpcMarker("EcDoDisconnect", contextHandle, noError);
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000056FC File Offset: 0x000038FC
		public int EcDoRpcExt2(ref IntPtr contextHandle, ref uint flags, ArraySegment<byte> request, uint maximumResponseSize, out ArraySegment<byte> response, ArraySegment<byte> auxiliaryIn, out ArraySegment<byte> auxiliaryOut)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			ProxyMapiRpcServer.TraceStartRpcMarker("EcDoRpcExt2", contextHandle);
			int result;
			try
			{
				response = RpcServerBase.EmptyArraySegment;
				auxiliaryOut = RpcServerBase.EmptyArraySegment;
				uint num = (uint)contextHandle.ToInt32();
				uint num2 = num;
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					ProxyMapiRpcServer.DoRpcHelper doRpcHelper = new ProxyMapiRpcServer.DoRpcHelper(manualResetEvent);
					errorCode = this.manager.BeginPoolDoRpc(ref num2, flags, maximumResponseSize, request, auxiliaryIn, new DoRpcCompleteCallback(doRpcHelper.OnDoRpcComplete), new Action<RpcException>(doRpcHelper.OnDoRpcException));
					if (ErrorCode.NoError != errorCode)
					{
						if (num2 == 0U)
						{
							contextHandle = IntPtr.Zero;
						}
						return (int)errorCode;
					}
					errorCode = doRpcHelper.WaitForDoRpc(out flags, out response, out auxiliaryOut);
					if (ErrorCodeValue.MdbNotInitialized == errorCode || ErrorCodeValue.Exiting == errorCode)
					{
						this.manager.CloseSession(num);
						contextHandle = IntPtr.Zero;
					}
				}
				result = (int)errorCode;
			}
			finally
			{
				ProxyMapiRpcServer.TraceEndRpcMarker("EcDoRpcExt2", contextHandle, errorCode);
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005828 File Offset: 0x00003A28
		public int EcDoAsyncConnectEx(IntPtr contextHandle, out IntPtr asynchronousContextHandle)
		{
			ErrorCode noError = ErrorCode.NoError;
			ProxyMapiRpcServer.TraceStartRpcMarker("EcDoAsyncConnectEx", contextHandle);
			int result;
			try
			{
				uint num = (uint)contextHandle.ToInt32();
				ErrorHelper.AssertRetail(0U == (num & 2147483648U), "Session handle value overlaps with asyc handle flag.");
				num |= 2147483648U;
				asynchronousContextHandle = new IntPtr((int)num);
				result = (int)noError;
			}
			finally
			{
				ProxyMapiRpcServer.TraceEndRpcMarker("EcDoAsyncConnectEx", contextHandle, noError);
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000058A0 File Offset: 0x00003AA0
		public int DoAsyncWaitEx(IntPtr asynchronousContextHandle, uint flags, IProxyAsyncWaitCompletion completionObject)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			ProxyMapiRpcServer.TraceStartRpcMarker("DoAsyncWaitEx", asynchronousContextHandle);
			int result;
			try
			{
				try
				{
					uint num = (uint)asynchronousContextHandle.ToInt32();
					if ((num & 2147483648U) != 0U)
					{
						num &= (num & 2147483647U);
						errorCode = this.manager.QueueNotificationWait(ref num, completionObject);
						if (ErrorCode.NoError == errorCode)
						{
							completionObject = null;
						}
					}
					else
					{
						ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification rejected: invalid context handle");
					}
					result = 0;
				}
				finally
				{
					if (completionObject != null)
					{
						completionObject.CompleteAsyncCall(false, 2030);
						ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, "Notification rejected: error or exception occured");
					}
				}
			}
			finally
			{
				ProxyMapiRpcServer.TraceEndRpcMarker("DoAsyncWaitEx", asynchronousContextHandle, errorCode);
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000595C File Offset: 0x00003B5C
		public ushort GetVersionDelta()
		{
			return 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005960 File Offset: 0x00003B60
		private static void TraceStartRpcMarker(string rpcName, IntPtr contextHandle)
		{
			if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(60);
				stringBuilder.Append("MARK PROXY CALL [MAPI][");
				stringBuilder.Append(rpcName);
				stringBuilder.Append("] session:[");
				if (contextHandle != IntPtr.Zero)
				{
					stringBuilder.Append(contextHandle.ToInt32());
				}
				else
				{
					stringBuilder.Append("new");
				}
				stringBuilder.Append("]");
				ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000059EC File Offset: 0x00003BEC
		private static void TraceEndRpcMarker(string rpcName, IntPtr contextHandle, ErrorCode error)
		{
			if (ExTraceGlobals.ProxyMapiTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(60);
				stringBuilder.Append("MARK PROXY CALL END [MAPI][");
				stringBuilder.Append(rpcName);
				stringBuilder.Append("] session:[");
				if (contextHandle != IntPtr.Zero)
				{
					stringBuilder.Append(contextHandle.ToInt32());
				}
				else
				{
					stringBuilder.Append("end");
				}
				if (error != ErrorCode.NoError)
				{
					stringBuilder.Append("] error:[");
					stringBuilder.Append(error);
				}
				stringBuilder.Append("]");
				ExTraceGlobals.ProxyMapiTracer.TraceFunction(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x04000010 RID: 16
		private const uint AsyncHandleFlag = 2147483648U;

		// Token: 0x04000011 RID: 17
		private IPoolSessionManager manager;

		// Token: 0x02000009 RID: 9
		private class DoRpcHelper
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00005A9E File Offset: 0x00003C9E
			public DoRpcHelper(ManualResetEvent completeEvent)
			{
				this.completeEvent = completeEvent;
				this.response = RpcServerBase.EmptyArraySegment;
				this.auxiliaryOut = RpcServerBase.EmptyArraySegment;
				this.result = ErrorCode.NoError;
				this.flags = 0U;
				this.exception = null;
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00005ADC File Offset: 0x00003CDC
			public void OnDoRpcComplete(ErrorCode result, uint flags, ArraySegment<byte> rpcResponse, ArraySegment<byte> auxOut)
			{
				this.result = result;
				this.flags = flags;
				this.auxiliaryOut = auxOut;
				this.response = rpcResponse;
				this.completeEvent.Set();
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00005B07 File Offset: 0x00003D07
			public void OnDoRpcException(RpcException exception)
			{
				this.exception = exception;
				this.completeEvent.Set();
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00005B1C File Offset: 0x00003D1C
			public ErrorCode WaitForDoRpc(out uint flags, out ArraySegment<byte> response, out ArraySegment<byte> auxiliaryOut)
			{
				this.completeEvent.WaitOne();
				if (this.exception != null)
				{
					throw new RpcException("RPC exception thrown by worker process. See inner exception for details", this.exception.ErrorCode, this.exception);
				}
				flags = this.flags;
				response = this.response;
				auxiliaryOut = this.auxiliaryOut;
				return this.result;
			}

			// Token: 0x04000012 RID: 18
			private ManualResetEvent completeEvent;

			// Token: 0x04000013 RID: 19
			private RpcException exception;

			// Token: 0x04000014 RID: 20
			private ArraySegment<byte> response;

			// Token: 0x04000015 RID: 21
			private ArraySegment<byte> auxiliaryOut;

			// Token: 0x04000016 RID: 22
			private ErrorCode result;

			// Token: 0x04000017 RID: 23
			private uint flags;
		}
	}
}
