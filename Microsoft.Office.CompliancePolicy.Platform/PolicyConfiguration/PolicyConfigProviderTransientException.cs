using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public class PolicyConfigProviderTransientException : SyncAgentTransientException
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000BA8D File Offset: 0x00009C8D
		public PolicyConfigProviderTransientException(string message) : this(message, null, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000BA99 File Offset: 0x00009C99
		public PolicyConfigProviderTransientException(string message, Exception innerException) : this(message, innerException, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		public PolicyConfigProviderTransientException(string message, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : this(message, null, isPerObjectException, errorCode)
		{
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000BAB1 File Offset: 0x00009CB1
		public PolicyConfigProviderTransientException(string message, Exception innerException, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(message, innerException, isPerObjectException, errorCode)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000BABE File Offset: 0x00009CBE
		protected PolicyConfigProviderTransientException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
