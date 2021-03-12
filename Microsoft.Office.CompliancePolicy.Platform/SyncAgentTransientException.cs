using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class SyncAgentTransientException : SyncAgentExceptionBase
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00005E9A File Offset: 0x0000409A
		public SyncAgentTransientException(string message) : this(message, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005EA5 File Offset: 0x000040A5
		public SyncAgentTransientException(string message, Exception innerException) : this(message, innerException, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005EB1 File Offset: 0x000040B1
		public SyncAgentTransientException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005EBD File Offset: 0x000040BD
		public SyncAgentTransientException(string message, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : this(message, null, isPerObjectException, errorCode)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005EC9 File Offset: 0x000040C9
		public SyncAgentTransientException(string message, Exception innerException, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(message, innerException, isPerObjectException, errorCode)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005ED6 File Offset: 0x000040D6
		public SyncAgentTransientException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException, SyncAgentErrorCode errorCode = SyncAgentErrorCode.Generic) : base(serializationInfo, context, isPerObjectException, errorCode)
		{
		}
	}
}
