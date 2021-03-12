using System;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000DD RID: 221
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DagRpcExceptionWrapper : HaRpcExceptionWrapperBase<DagTaskServerException, DagTaskServerTransientException>
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0002A4B1 File Offset: 0x000286B1
		public static DagRpcExceptionWrapper Instance
		{
			get
			{
				return DagRpcExceptionWrapper.s_dagRpcWrapper;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002A4B8 File Offset: 0x000286B8
		protected DagRpcExceptionWrapper()
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002A4C0 File Offset: 0x000286C0
		protected override DagTaskServerException GetGenericOperationFailedException(string message)
		{
			return new DagTaskOperationFailedException(message);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002A4C8 File Offset: 0x000286C8
		protected override DagTaskServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new DagTaskOperationFailedException(message, innerException);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002A4D1 File Offset: 0x000286D1
		protected override DagTaskServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			return new DagTaskOperationFailedWithEcException(errorCode);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002A4D9 File Offset: 0x000286D9
		protected override DagTaskServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new DagReplayServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002A4E8 File Offset: 0x000286E8
		protected override DagTaskServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			return new DagTaskServerTransientException(message, innerException);
		}

		// Token: 0x040003BD RID: 957
		private static DagRpcExceptionWrapper s_dagRpcWrapper = new DagRpcExceptionWrapper();
	}
}
