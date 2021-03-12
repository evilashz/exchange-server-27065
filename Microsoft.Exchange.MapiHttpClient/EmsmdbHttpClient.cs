using System;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbHttpClient : MapiHttpClient, IExchangeAsyncDispatch
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004F41 File Offset: 0x00003141
		public EmsmdbHttpClient(MapiHttpBindingInfo bindingInfo) : base(bindingInfo)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004F4A File Offset: 0x0000314A
		internal override string VdirPath
		{
			get
			{
				return MapiHttpEndpoints.VdirPathEmsmdb;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004FDC File Offset: 0x000031DC
		public ICancelableAsyncResult BeginConnect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string userDn, int flags, int conMod, int cpid, int lcidString, int lcidSort, short[] clientVersion, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			if (clientVersion.Length != 4)
			{
				throw new ArgumentException("Client version needs to be a quartet", "clientVersion");
			}
			IntPtr contextHandle = base.CreateNewContextHandle(new Func<IntPtr, IntPtr>(EmsmdbHttpClient.MakeContextHandle));
			return base.BeginWrapper<EmsmdbConnectClientAsyncOperation>(contextHandle, true, delegate(ClientSessionContext context)
			{
				EmsmdbConnectClientAsyncOperation emsmdbConnectClientAsyncOperation = new EmsmdbConnectClientAsyncOperation(context, asyncCallback, asyncState);
				emsmdbConnectClientAsyncOperation.ClientVersion = new MapiHttpVersion((ushort)clientVersion[0], (ushort)clientVersion[1], (ushort)clientVersion[2], (ushort)clientVersion[3]);
				emsmdbConnectClientAsyncOperation.Begin(new EmsmdbConnectRequest(userDn, (uint)flags, (uint)cpid, (uint)lcidSort, (uint)lcidString, segmentExtendedAuxIn));
				return emsmdbConnectClientAsyncOperation;
			});
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005094 File Offset: 0x00003294
		public int EndConnect(ICancelableAsyncResult asyncResult, out IntPtr contextHandle, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, out short[] serverVersion, out ArraySegment<byte> segmentExtendedAuxOut)
		{
			MapiHttpVersion mapiHttpVersion = null;
			if (!((ClientAsyncOperation)asyncResult).TryGetServerVersion(out mapiHttpVersion))
			{
				mapiHttpVersion = MapiHttpVersion.Version15;
			}
			EmsmdbConnectResponse localResponse = null;
			ErrorCode result = base.EndWrapper<EmsmdbConnectClientAsyncOperation>(asyncResult, false, true, out contextHandle, (EmsmdbConnectClientAsyncOperation operation) => operation.End(out localResponse));
			pollsMax = new TimeSpan(0, 0, 0, 0, (int)localResponse.MaxPollingInterval);
			retryCount = (int)localResponse.RetryCount;
			retryDelay = new TimeSpan(0, 0, 0, 0, (int)localResponse.RetryDelay);
			dnPrefix = localResponse.DnPrefix;
			displayName = localResponse.DisplayName;
			serverVersion = (from part in mapiHttpVersion.ToQuartet()
			select (short)part).ToArray<short>();
			segmentExtendedAuxOut = localResponse.AuxiliaryBuffer;
			return (int)result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000051D8 File Offset: 0x000033D8
		public ICancelableAsyncResult BeginDisconnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ICancelableAsyncResult result;
			try
			{
				result = base.BeginWrapper<EmsmdbDisconnectClientAsyncOperation>(contextHandle, false, delegate(ClientSessionContext context)
				{
					EmsmdbDisconnectClientAsyncOperation emsmdbDisconnectClientAsyncOperation = new EmsmdbDisconnectClientAsyncOperation(context, asyncCallback, asyncState);
					emsmdbDisconnectClientAsyncOperation.Begin(new EmsmdbDisconnectRequest(Array<byte>.EmptySegment));
					return emsmdbDisconnectClientAsyncOperation;
				});
			}
			catch (ContextNotFoundException exception)
			{
				FailureAsyncResult<int> failureAsyncResult = new FailureAsyncResult<int>(0, IntPtr.Zero, exception, asyncCallback, asyncState);
				if (!ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					FailureAsyncResult<int> failureAsyncResult2 = (FailureAsyncResult<int>)state;
					failureAsyncResult2.InvokeCallback();
				}, failureAsyncResult))
				{
					failureAsyncResult.InvokeCallback();
				}
				result = failureAsyncResult;
			}
			return result;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005290 File Offset: 0x00003490
		public int EndDisconnect(ICancelableAsyncResult asyncResult, out IntPtr contextHandle)
		{
			int result;
			try
			{
				FailureAsyncResult<int> failureAsyncResult = asyncResult as FailureAsyncResult<int>;
				if (failureAsyncResult != null)
				{
					throw new AggregateException(new Exception[]
					{
						failureAsyncResult.Exception
					});
				}
				EmsmdbDisconnectResponse localResponse = null;
				ErrorCode errorCode = base.EndWrapper<EmsmdbDisconnectClientAsyncOperation>(asyncResult, true, true, out contextHandle, (EmsmdbDisconnectClientAsyncOperation operation) => operation.End(out localResponse));
				result = (int)errorCode;
			}
			catch (AggregateException exception)
			{
				if (exception.FindException<ContextNotFoundException>() == null && exception.FindException<InvalidSequenceException>() == null)
				{
					throw;
				}
				contextHandle = IntPtr.Zero;
				result = 0;
			}
			catch (ContextNotFoundException)
			{
				contextHandle = IntPtr.Zero;
				result = 0;
			}
			catch (InvalidSequenceException)
			{
				contextHandle = IntPtr.Zero;
				result = 0;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000053B0 File Offset: 0x000035B0
		public ICancelableAsyncResult BeginExecute(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int flags, ArraySegment<byte> segmentExtendedRopIn, ArraySegment<byte> segmentExtendedRopOut, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginWrapper<EmsmdbExecuteClientAsyncOperation>(contextHandle, false, delegate(ClientSessionContext context)
			{
				EmsmdbExecuteClientAsyncOperation emsmdbExecuteClientAsyncOperation = new EmsmdbExecuteClientAsyncOperation(context, asyncCallback, asyncState);
				emsmdbExecuteClientAsyncOperation.Begin(new EmsmdbExecuteRequest((uint)flags, segmentExtendedRopIn, (uint)segmentExtendedRopOut.Count, segmentExtendedAuxIn));
				return emsmdbExecuteClientAsyncOperation;
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000541C File Offset: 0x0000361C
		public int EndExecute(ICancelableAsyncResult asyncResult, out IntPtr contextHandle, out ArraySegment<byte> segmentExtendedRopOut, out ArraySegment<byte> segmentExtendedAuxOut)
		{
			EmsmdbExecuteResponse localResponse = null;
			ErrorCode result = base.EndWrapper<EmsmdbExecuteClientAsyncOperation>(asyncResult, false, false, out contextHandle, (EmsmdbExecuteClientAsyncOperation operation) => operation.End(out localResponse));
			segmentExtendedRopOut = localResponse.RopBuffer;
			segmentExtendedAuxOut = localResponse.AuxiliaryBuffer;
			return (int)result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000054A4 File Offset: 0x000036A4
		public ICancelableAsyncResult BeginNotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginWrapper<EmsmdbNotificationConnectClientAsyncOperation>(contextHandle, false, delegate(ClientSessionContext context)
			{
				EmsmdbNotificationConnectClientAsyncOperation emsmdbNotificationConnectClientAsyncOperation = new EmsmdbNotificationConnectClientAsyncOperation(context, asyncCallback, asyncState);
				emsmdbNotificationConnectClientAsyncOperation.Begin();
				return emsmdbNotificationConnectClientAsyncOperation;
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005510 File Offset: 0x00003710
		public int EndNotificationConnect(ICancelableAsyncResult asyncResult, out IntPtr notificationContextHandle)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr localNotificationContextHandle = IntPtr.Zero;
			ErrorCode result = base.EndWrapper<EmsmdbNotificationConnectClientAsyncOperation>(asyncResult, false, false, out zero, delegate(EmsmdbNotificationConnectClientAsyncOperation operation)
			{
				ErrorCode result2 = operation.End();
				localNotificationContextHandle = EmsmdbHttpClient.MakeNotificationContextHandle(operation.Context.ContextHandle);
				return result2;
			});
			notificationContextHandle = localNotificationContextHandle;
			return (int)result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005598 File Offset: 0x00003798
		public ICancelableAsyncResult BeginNotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr notificationContextHandle, int flagsIn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			return base.BeginWrapper<EmsmdbNotificationWaitClientAsyncOperation>(EmsmdbHttpClient.MakeContextHandle(notificationContextHandle), false, delegate(ClientSessionContext context)
			{
				EmsmdbNotificationWaitClientAsyncOperation emsmdbNotificationWaitClientAsyncOperation = new EmsmdbNotificationWaitClientAsyncOperation(context, asyncCallback, asyncState);
				emsmdbNotificationWaitClientAsyncOperation.Begin(new EmsmdbNotificationWaitRequest(0U, Array<byte>.EmptySegment));
				return emsmdbNotificationWaitClientAsyncOperation;
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000055EC File Offset: 0x000037EC
		public int EndNotificationWait(ICancelableAsyncResult asyncResult, out int flags)
		{
			EmsmdbNotificationWaitResponse localResponse = null;
			IntPtr intPtr;
			ErrorCode result = base.EndWrapper<EmsmdbNotificationWaitClientAsyncOperation>(asyncResult, false, false, out intPtr, (EmsmdbNotificationWaitClientAsyncOperation operation) => operation.End(out localResponse));
			flags = (int)localResponse.Flags;
			return (int)result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000562C File Offset: 0x0000382C
		public ICancelableAsyncResult BeginRegisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, ArraySegment<byte> segmentContext, int adviseBits, ArraySegment<byte> segmentClientBlob, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new InvalidOperationException("BeginRegisterPushNotification");
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005638 File Offset: 0x00003838
		public int EndRegisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle, out int notificationHandle)
		{
			throw new InvalidOperationException("EndRegisterPushNotification");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005644 File Offset: 0x00003844
		public ICancelableAsyncResult BeginUnregisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int notificationHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new InvalidOperationException("BeginUnregisterPushNotification");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005650 File Offset: 0x00003850
		public int EndUnregisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle)
		{
			throw new InvalidOperationException("EndUnregisterPushNotification");
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000565C File Offset: 0x0000385C
		public void ContextHandleRundown(IntPtr contextHandle)
		{
			throw new InvalidOperationException("ContextHandleRundown");
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005668 File Offset: 0x00003868
		public void NotificationContextHandleRundown(IntPtr notificationContextHandle)
		{
			throw new InvalidOperationException("NotificationContextHandleRundown");
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000056A8 File Offset: 0x000038A8
		public ICancelableAsyncResult BeginDummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			IntPtr contextHandle = base.CreateNewContextHandle(null);
			return base.BeginWrapper<EmsmdbDummyClientAsyncOperation>(contextHandle, true, delegate(ClientSessionContext context)
			{
				EmsmdbDummyClientAsyncOperation emsmdbDummyClientAsyncOperation = new EmsmdbDummyClientAsyncOperation(context, asyncCallback, asyncState);
				emsmdbDummyClientAsyncOperation.Begin(new EmsmdbDummyRequest());
				return emsmdbDummyClientAsyncOperation;
			});
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000056FC File Offset: 0x000038FC
		public int EndDummy(ICancelableAsyncResult asyncResult)
		{
			EmsmdbDummyResponse localResponse = null;
			IntPtr intPtr;
			return (int)base.EndWrapper<EmsmdbDummyClientAsyncOperation>(asyncResult, true, true, out intPtr, (EmsmdbDummyClientAsyncOperation operation) => operation.End(out localResponse));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000572F File Offset: 0x0000392F
		public void DroppedConnection(IntPtr notificationContextHandle)
		{
			throw new InvalidOperationException("DroppedConnection");
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000573C File Offset: 0x0000393C
		protected override void InternalDispose()
		{
			foreach (IntPtr contextHandle in base.ContextHandles)
			{
				try
				{
					ICancelableAsyncResult asyncResult = this.BeginDisconnect(null, contextHandle, null, null);
					IntPtr intPtr;
					this.EndDisconnect(asyncResult, out intPtr);
				}
				catch (Exception)
				{
				}
			}
			base.InternalDispose();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000057A0 File Offset: 0x000039A0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbHttpClient>(this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000057A8 File Offset: 0x000039A8
		private static IntPtr MakeContextHandle(IntPtr rawContextHandle)
		{
			long num = rawContextHandle.ToInt64();
			num &= 2147483647L;
			return new IntPtr(num);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000057CC File Offset: 0x000039CC
		private static IntPtr MakeNotificationContextHandle(IntPtr rawContextHandle)
		{
			long num = rawContextHandle.ToInt64();
			num &= 2147483647L;
			num |= (long)((ulong)int.MinValue);
			return new IntPtr(num);
		}
	}
}
