using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002B1 RID: 689
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeederRpcExceptionWrapper : HaRpcExceptionWrapperBase<SeederServerException, SeederServerTransientException>
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0007417E File Offset: 0x0007237E
		protected SeederRpcExceptionWrapper()
		{
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x00074186 File Offset: 0x00072386
		public static SeederRpcExceptionWrapper Instance
		{
			get
			{
				return SeederRpcExceptionWrapper.s_seederRpcWrapper;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0007418D File Offset: 0x0007238D
		protected override SeederServerException GetGenericOperationFailedException(string message)
		{
			return new SeederOperationFailedException(message);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00074195 File Offset: 0x00072395
		protected override SeederServerException GetGenericOperationFailedException(string message, Exception innerException)
		{
			return new SeederOperationFailedException(message, innerException);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000741A0 File Offset: 0x000723A0
		protected override SeederServerException GetGenericOperationFailedWithEcException(int errorCode)
		{
			Win32Exception ex = new Win32Exception(errorCode);
			return new SeederOperationFailedWithEcException(errorCode, ex.Message);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000741C0 File Offset: 0x000723C0
		protected override SeederServerException GetServiceDownException(string serverName, Exception innerException)
		{
			return new SeederReplayServiceDownException(serverName, innerException.Message, innerException);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000741CF File Offset: 0x000723CF
		protected override SeederServerTransientException GetGenericOperationFailedTransientException(string message, Exception innerException)
		{
			return new SeederServerTransientException(message, innerException);
		}

		// Token: 0x04000AD4 RID: 2772
		private static SeederRpcExceptionWrapper s_seederRpcWrapper = new SeederRpcExceptionWrapper();
	}
}
