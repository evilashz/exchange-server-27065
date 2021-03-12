using System;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000323 RID: 803
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActiveMonitoringRpcExceptionWrapper : HaRpcExceptionWrapperBase<ActiveMonitoringServerException, ActiveMonitoringServerTransientException>
	{
		// Token: 0x060023D0 RID: 9168 RVA: 0x000936A8 File Offset: 0x000918A8
		protected ActiveMonitoringRpcExceptionWrapper()
		{
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000936B0 File Offset: 0x000918B0
		public static ActiveMonitoringRpcExceptionWrapper Instance
		{
			get
			{
				return ActiveMonitoringRpcExceptionWrapper.rpcWrapper;
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000936B7 File Offset: 0x000918B7
		protected override ActiveMonitoringServerException GetGenericOperationFailedException(string message)
		{
			return new ActiveMonitoringOperationFailedException(message);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000936BF File Offset: 0x000918BF
		protected override ActiveMonitoringServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new ActiveMonitoringOperationFailedException(message, innerException);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000936C8 File Offset: 0x000918C8
		protected override ActiveMonitoringServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			return new ActiveMonitoringOperationFailedWithEcException(errorCode);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000936D0 File Offset: 0x000918D0
		protected override ActiveMonitoringServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new ActiveMonitoringServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000936DF File Offset: 0x000918DF
		protected override ActiveMonitoringServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			return new ActiveMonitoringServerTransientException(message, innerException);
		}

		// Token: 0x04001566 RID: 5478
		private static ActiveMonitoringRpcExceptionWrapper rpcWrapper = new ActiveMonitoringRpcExceptionWrapper();
	}
}
