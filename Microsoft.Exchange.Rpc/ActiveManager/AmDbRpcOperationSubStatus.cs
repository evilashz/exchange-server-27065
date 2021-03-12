using System;
using System.Text;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	internal sealed class AmDbRpcOperationSubStatus
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00002148 File Offset: 0x00001548
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append("AmDbRpcOperationSubStatus: [ ");
			stringBuilder.AppendFormat("ServerAttempted='{0}', ", this.m_serverFqdnAttempted);
			AmAcllReturnStatus acllStatus = this.m_acllStatus;
			if (acllStatus != null)
			{
				stringBuilder.AppendFormat("AmAcllReturnStatus='{0}', ", acllStatus.ToString());
			}
			RpcErrorExceptionInfo errorInfo = this.m_errorInfo;
			if (errorInfo != null)
			{
				stringBuilder.AppendFormat("RpcErrorExceptionInfo='{0}' ", errorInfo.ToString());
			}
			stringBuilder.Append("]");
			this.m_debugString = stringBuilder.ToString();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000021D0 File Offset: 0x000015D0
		public AmDbRpcOperationSubStatus(string serverFqdnAttempted, AmAcllReturnStatus acllStatus, RpcErrorExceptionInfo errorInfo)
		{
			this.BuildDebugString();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00002200 File Offset: 0x00001600
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00002214 File Offset: 0x00001614
		public string ServerAttemptedFqdn
		{
			get
			{
				return this.m_serverFqdnAttempted;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00002228 File Offset: 0x00001628
		public AmAcllReturnStatus AcllStatus
		{
			get
			{
				return this.m_acllStatus;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000223C File Offset: 0x0000163C
		public RpcErrorExceptionInfo ErrorInfo
		{
			get
			{
				return this.m_errorInfo;
			}
		}

		// Token: 0x04000959 RID: 2393
		private readonly string m_serverFqdnAttempted = serverFqdnAttempted;

		// Token: 0x0400095A RID: 2394
		private readonly AmAcllReturnStatus m_acllStatus = acllStatus;

		// Token: 0x0400095B RID: 2395
		private readonly RpcErrorExceptionInfo m_errorInfo = errorInfo;

		// Token: 0x0400095C RID: 2396
		private string m_debugString;
	}
}
