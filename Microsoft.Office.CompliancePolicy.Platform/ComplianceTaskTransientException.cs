using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class ComplianceTaskTransientException : CompliancePolicyException
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000034BA File Offset: 0x000016BA
		public ComplianceTaskTransientException(string message) : this(message, null)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000034C4 File Offset: 0x000016C4
		public ComplianceTaskTransientException(string message, Exception innerException) : this(message, innerException, UnifiedPolicyErrorCode.Unknown)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000034CF File Offset: 0x000016CF
		public ComplianceTaskTransientException(string message, UnifiedPolicyErrorCode errorCode) : this(message, null, errorCode)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000034DA File Offset: 0x000016DA
		public ComplianceTaskTransientException(string message, Exception innerException, UnifiedPolicyErrorCode errorCode) : base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000034EB File Offset: 0x000016EB
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000034F3 File Offset: 0x000016F3
		public UnifiedPolicyErrorCode ErrorCode { get; private set; }
	}
}
