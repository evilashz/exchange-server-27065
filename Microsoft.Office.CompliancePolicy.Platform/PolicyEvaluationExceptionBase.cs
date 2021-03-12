using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public class PolicyEvaluationExceptionBase : CompliancePolicyException
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000DF4D File Offset: 0x0000C14D
		public PolicyEvaluationExceptionBase(string message) : this(message, null, false)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DF58 File Offset: 0x0000C158
		public PolicyEvaluationExceptionBase(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000DF63 File Offset: 0x0000C163
		public PolicyEvaluationExceptionBase(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DF6E File Offset: 0x0000C16E
		public PolicyEvaluationExceptionBase(string message, Exception innerException, bool isPerObjectException) : base(message, innerException)
		{
			this.IsPerObjectException = isPerObjectException;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000DF7F File Offset: 0x0000C17F
		public PolicyEvaluationExceptionBase(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context)
		{
			this.IsPerObjectException = isPerObjectException;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000DF90 File Offset: 0x0000C190
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000DF98 File Offset: 0x0000C198
		public bool IsPerObjectException { get; private set; }
	}
}
