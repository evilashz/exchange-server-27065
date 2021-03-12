using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class PolicyEvaluationPermanentException : PolicyEvaluationExceptionBase
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000DFA1 File Offset: 0x0000C1A1
		public PolicyEvaluationPermanentException(string message) : this(message, null, false)
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		public PolicyEvaluationPermanentException(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000DFB7 File Offset: 0x0000C1B7
		public PolicyEvaluationPermanentException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000DFC2 File Offset: 0x0000C1C2
		public PolicyEvaluationPermanentException(string message, Exception innerException, bool isPerObjectException) : base(message, innerException, isPerObjectException)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DFCD File Offset: 0x0000C1CD
		public PolicyEvaluationPermanentException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context, isPerObjectException)
		{
		}
	}
}
