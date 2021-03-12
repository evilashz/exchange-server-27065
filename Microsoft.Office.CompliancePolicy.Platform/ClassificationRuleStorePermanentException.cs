using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class ClassificationRuleStorePermanentException : ClassificationRuleStoreExceptionBase
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002829 File Offset: 0x00000A29
		public ClassificationRuleStorePermanentException(string message) : this(message, null, false)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002834 File Offset: 0x00000A34
		public ClassificationRuleStorePermanentException(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000283F File Offset: 0x00000A3F
		public ClassificationRuleStorePermanentException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000284A File Offset: 0x00000A4A
		public ClassificationRuleStorePermanentException(string message, Exception innerException, bool isPerObjectException) : base(message, innerException, isPerObjectException)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002855 File Offset: 0x00000A55
		public ClassificationRuleStorePermanentException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context, isPerObjectException)
		{
		}
	}
}
