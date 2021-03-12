using System;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000319 RID: 793
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmRpcExceptionWrapper : HaRpcExceptionWrapperBase<AmServerException, AmServerTransientException>
	{
		// Token: 0x06002398 RID: 9112 RVA: 0x00091301 File Offset: 0x0008F501
		private AmRpcExceptionWrapper()
		{
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x00091309 File Offset: 0x0008F509
		public static AmRpcExceptionWrapper Instance
		{
			get
			{
				return AmRpcExceptionWrapper.s_amRpcWrapper;
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00091310 File Offset: 0x0008F510
		internal override bool IsKnownException(Exception ex)
		{
			return ex is TaskServerException || ex is TaskServerTransientException || base.IsKnownException(ex);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0009132B File Offset: 0x0008F52B
		protected override AmServerException GetGenericOperationFailedException(string message)
		{
			return new AmOperationFailedException(message);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00091333 File Offset: 0x0008F533
		protected override AmServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new AmOperationFailedException(message, innerException);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0009133C File Offset: 0x0008F53C
		protected override AmServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			return new AmOperationFailedWithEcException(errorCode);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00091344 File Offset: 0x0008F544
		protected override AmServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new AmReplayServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00091353 File Offset: 0x0008F553
		protected override AmServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			return new AmServerTransientException(message, innerException);
		}

		// Token: 0x04001521 RID: 5409
		private static AmRpcExceptionWrapper s_amRpcWrapper = new AmRpcExceptionWrapper();
	}
}
