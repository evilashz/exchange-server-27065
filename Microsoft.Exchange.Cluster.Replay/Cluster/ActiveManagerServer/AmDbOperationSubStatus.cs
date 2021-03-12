using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200002D RID: 45
	internal class AmDbOperationSubStatus
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000C90E File Offset: 0x0000AB0E
		public AmDbOperationSubStatus(AmServerName serverAttempted, AmAcllReturnStatus acllStatus, Exception exception)
		{
			this.ServerAttempted = serverAttempted;
			this.AcllReturnStatus = acllStatus;
			this.LastException = exception;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000C92B File Offset: 0x0000AB2B
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000C933 File Offset: 0x0000AB33
		internal AmServerName ServerAttempted { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000C93C File Offset: 0x0000AB3C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000C944 File Offset: 0x0000AB44
		internal AmAcllReturnStatus AcllReturnStatus { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000C94D File Offset: 0x0000AB4D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000C955 File Offset: 0x0000AB55
		internal Exception LastException { get; private set; }

		// Token: 0x0600020C RID: 524 RVA: 0x0000C960 File Offset: 0x0000AB60
		public AmDbRpcOperationSubStatus ConvertToRpcSubStatus()
		{
			RpcErrorExceptionInfo errorInfo = AmRpcExceptionWrapper.Instance.ConvertExceptionToErrorExceptionInfo(this.LastException);
			return new AmDbRpcOperationSubStatus(this.ServerAttempted.Fqdn, this.AcllReturnStatus, errorInfo);
		}
	}
}
