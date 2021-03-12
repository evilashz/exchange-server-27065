using System;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002B2 RID: 690
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CiSeederClientRpcExceptionWrapper : SeederRpcExceptionWrapper
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x000741E4 File Offset: 0x000723E4
		private CiSeederClientRpcExceptionWrapper()
		{
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x000741EC File Offset: 0x000723EC
		public new static CiSeederClientRpcExceptionWrapper Instance
		{
			get
			{
				return CiSeederClientRpcExceptionWrapper.s_ciSeederRpcWrapper;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000741F3 File Offset: 0x000723F3
		[Obsolete("This method is not supported for this class.", true)]
		public new void ClientRethrowIfFailed(string serverName, RpcErrorExceptionInfo errorInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000741FA File Offset: 0x000723FA
		[Obsolete("This method is not supported for this class.", true)]
		public new void ClientRethrowIfFailed(string databaseName, string serverName, RpcErrorExceptionInfo errorInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00074201 File Offset: 0x00072401
		[Obsolete("This method is not supported for this class.", true)]
		public new RpcErrorExceptionInfo RunRpcServerOperation(RpcServerOperation rpcOperation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00074208 File Offset: 0x00072408
		[Obsolete("This method is not supported for this class.", true)]
		public new RpcErrorExceptionInfo RunRpcServerOperation(string databaseName, RpcServerOperation rpcOperation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0007420F File Offset: 0x0007240F
		protected override SeederServerException GetGenericOperationFailedException(string message)
		{
			return new CiSeederRpcOperationFailedException(message);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00074217 File Offset: 0x00072417
		protected override SeederServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new CiSeederRpcOperationFailedException(message, innerException);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00074220 File Offset: 0x00072420
		protected override SeederServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new CiServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0007422F File Offset: 0x0007242F
		protected override SeederServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00074236 File Offset: 0x00072436
		protected override SeederServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000AD5 RID: 2773
		private static CiSeederClientRpcExceptionWrapper s_ciSeederRpcWrapper = new CiSeederClientRpcExceptionWrapper();
	}
}
