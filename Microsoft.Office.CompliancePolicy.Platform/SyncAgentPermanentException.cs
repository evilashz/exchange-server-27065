using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class SyncAgentPermanentException : SyncAgentExceptionBase
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00005E51 File Offset: 0x00004051
		public SyncAgentPermanentException(string message) : this(message, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005E5C File Offset: 0x0000405C
		public SyncAgentPermanentException(string message, Exception innerException) : this(message, innerException, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005E68 File Offset: 0x00004068
		public SyncAgentPermanentException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005E74 File Offset: 0x00004074
		public SyncAgentPermanentException(string message, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : this(message, null, isPerObjectException, errorCode)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005E80 File Offset: 0x00004080
		public SyncAgentPermanentException(string message, Exception innerException, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(message, innerException, isPerObjectException, errorCode)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005E8D File Offset: 0x0000408D
		public SyncAgentPermanentException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(serializationInfo, context, isPerObjectException, errorCode)
		{
		}
	}
}
