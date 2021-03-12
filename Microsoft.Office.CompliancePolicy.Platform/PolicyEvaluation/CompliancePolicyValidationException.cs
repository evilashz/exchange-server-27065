using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class CompliancePolicyValidationException : PolicyEvaluationPermanentException
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0000E006 File Offset: 0x0000C206
		public CompliancePolicyValidationException(string message) : base(message, null)
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000E010 File Offset: 0x0000C210
		public CompliancePolicyValidationException(string format, params object[] args) : base(string.Format(format, args), null)
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E020 File Offset: 0x0000C220
		public CompliancePolicyValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000E02A File Offset: 0x0000C22A
		protected CompliancePolicyValidationException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
