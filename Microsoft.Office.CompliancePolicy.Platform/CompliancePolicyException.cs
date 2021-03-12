using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class CompliancePolicyException : Exception
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027B7 File Offset: 0x000009B7
		public CompliancePolicyException(string message) : this(message, null)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027C1 File Offset: 0x000009C1
		public CompliancePolicyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027CB File Offset: 0x000009CB
		protected CompliancePolicyException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
