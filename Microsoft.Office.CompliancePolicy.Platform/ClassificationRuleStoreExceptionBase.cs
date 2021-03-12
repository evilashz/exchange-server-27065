using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class ClassificationRuleStoreExceptionBase : CompliancePolicyException
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027D5 File Offset: 0x000009D5
		public ClassificationRuleStoreExceptionBase(string message) : this(message, null, false)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027E0 File Offset: 0x000009E0
		public ClassificationRuleStoreExceptionBase(string message, Exception innerException) : this(message, innerException, false)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027EB File Offset: 0x000009EB
		public ClassificationRuleStoreExceptionBase(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027F6 File Offset: 0x000009F6
		public ClassificationRuleStoreExceptionBase(string message, Exception innerException, bool isPerObjectException) : base(message, innerException)
		{
			this.IsPerObjectException = isPerObjectException;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002807 File Offset: 0x00000A07
		public ClassificationRuleStoreExceptionBase(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException) : base(serializationInfo, context)
		{
			this.IsPerObjectException = isPerObjectException;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002818 File Offset: 0x00000A18
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002820 File Offset: 0x00000A20
		public bool IsPerObjectException { get; private set; }
	}
}
