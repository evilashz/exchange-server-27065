using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000035 RID: 53
	internal abstract class UMVersionedRpcTargetBase : IVersionedRpcTarget, IRpcTarget
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public UMVersionedRpcTargetBase(Server server)
		{
			this.server = server;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public string Name
		{
			get
			{
				if (this.server == null)
				{
					return string.Empty;
				}
				return this.server.Name;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AB46 File Offset: 0x00008D46
		public ADConfigurationObject ConfigObject
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000AB58 File Offset: 0x00008D58
		public override bool Equals(object obj)
		{
			UMVersionedRpcTargetBase umversionedRpcTargetBase = obj as UMVersionedRpcTargetBase;
			if (this.Server == null || umversionedRpcTargetBase == null || umversionedRpcTargetBase.Server == null)
			{
				return base.Equals(obj);
			}
			return this.Server.Guid.Equals(umversionedRpcTargetBase.Server.Guid);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public override int GetHashCode()
		{
			if (this.Server != null)
			{
				return this.Server.Guid.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public UMRpcResponse ExecuteRequest(UMVersionedRpcRequest request)
		{
			UMRpcResponse result = null;
			int num = 3;
			while (num-- > 0)
			{
				try
				{
					result = this.InternalExecuteRequest(request);
					break;
				}
				catch (RpcException ex)
				{
					bool flag = UMErrorCode.IsNetworkError(ex.ErrorCode);
					CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMVersionedRpcTargetBase.ExecuteRequest: {0} IsRetriable={1} RetryCount={2} Exception={3}", new object[]
					{
						ex.Message,
						flag,
						num,
						ex
					});
					if (num == 0 || !flag)
					{
						throw;
					}
				}
			}
			return result;
		}

		// Token: 0x060002BF RID: 703
		protected abstract UMVersionedRpcClientBase CreateRpcClient();

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AC74 File Offset: 0x00008E74
		private UMRpcResponse InternalExecuteRequest(UMVersionedRpcRequest request)
		{
			UMRpcResponse result = null;
			using (UMVersionedRpcClientBase umversionedRpcClientBase = this.CreateRpcClient())
			{
				try
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMVersionedRpcTargetBase: Executing {0} ({1}->{2}).", new object[]
					{
						umversionedRpcClientBase.OperationName,
						base.GetType().Name,
						request
					});
					umversionedRpcClientBase.SetTimeOut(30000);
					byte[] array = Serialization.ObjectToBytes(request);
					if (array == null)
					{
						throw new ArgumentException("request");
					}
					byte[] mbinaryData = umversionedRpcClientBase.ExecuteRequest(array);
					result = (UMRpcResponse)Serialization.BytesToObject(mbinaryData);
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMVersionedRpcTargetBase: {0} ({1}->{2}) succeeded.", new object[]
					{
						umversionedRpcClientBase.OperationName,
						base.GetType().Name,
						request
					});
				}
				catch (RpcException ex)
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMVersionedRpcTargetBase: {0} ({1}->{2}) failed. ErrorCode:{3} Exception:{4}", new object[]
					{
						umversionedRpcClientBase.OperationName,
						base.GetType().Name,
						request,
						ex.ErrorCode,
						ex
					});
					throw;
				}
			}
			return result;
		}

		// Token: 0x040000D8 RID: 216
		internal const int TimeOutMSEC = 30000;

		// Token: 0x040000D9 RID: 217
		private Server server;
	}
}
