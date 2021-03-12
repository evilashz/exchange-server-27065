using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class CompliancePolicyParserException : PolicyEvaluationPermanentException
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public CompliancePolicyParserException(string message) : base(message, null)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DFE2 File Offset: 0x0000C1E2
		public CompliancePolicyParserException(string format, params object[] args) : base(string.Format(format, args), null)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000DFF2 File Offset: 0x0000C1F2
		public CompliancePolicyParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		protected CompliancePolicyParserException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
