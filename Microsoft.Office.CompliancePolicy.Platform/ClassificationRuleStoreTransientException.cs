using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class ClassificationRuleStoreTransientException : ClassificationRuleStoreExceptionBase
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002860 File Offset: 0x00000A60
		public ClassificationRuleStoreTransientException(string message) : this(message, null, false)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000286B File Offset: 0x00000A6B
		public ClassificationRuleStoreTransientException(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002876 File Offset: 0x00000A76
		public ClassificationRuleStoreTransientException(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002881 File Offset: 0x00000A81
		public ClassificationRuleStoreTransientException(string message, Exception innerException, bool isPerObjectException) : base(message, innerException, isPerObjectException)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000288C File Offset: 0x00000A8C
		public ClassificationRuleStoreTransientException(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context, isPerObjectException)
		{
		}
	}
}
