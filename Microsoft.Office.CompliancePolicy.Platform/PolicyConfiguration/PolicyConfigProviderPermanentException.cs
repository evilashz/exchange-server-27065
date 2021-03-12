using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class PolicyConfigProviderPermanentException : SyncAgentPermanentException
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000BA52 File Offset: 0x00009C52
		public PolicyConfigProviderPermanentException(string message) : this(message, null, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000BA5E File Offset: 0x00009C5E
		public PolicyConfigProviderPermanentException(string message, Exception innerException) : this(message, innerException, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000BA6A File Offset: 0x00009C6A
		public PolicyConfigProviderPermanentException(string message, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : this(message, null, isPerObjectException, errorCode)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000BA76 File Offset: 0x00009C76
		public PolicyConfigProviderPermanentException(string message, Exception innerException, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(message, innerException, isPerObjectException, errorCode)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000BA83 File Offset: 0x00009C83
		protected PolicyConfigProviderPermanentException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
