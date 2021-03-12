using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class PolicyEvaluationTransientException : PolicyEvaluationExceptionBase
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0000E034 File Offset: 0x0000C234
		public PolicyEvaluationTransientException(string message) : this(message, null, false)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000E03F File Offset: 0x0000C23F
		public PolicyEvaluationTransientException(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000E04A File Offset: 0x0000C24A
		public PolicyEvaluationTransientException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000E055 File Offset: 0x0000C255
		public PolicyEvaluationTransientException(string message, Exception innerException, bool isPerObjectException) : base(message, innerException, isPerObjectException)
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E060 File Offset: 0x0000C260
		public PolicyEvaluationTransientException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context, isPerObjectException)
		{
		}
	}
}
