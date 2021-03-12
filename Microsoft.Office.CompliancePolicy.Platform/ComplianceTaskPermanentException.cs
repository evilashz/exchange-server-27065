using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class ComplianceTaskPermanentException : CompliancePolicyException
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003478 File Offset: 0x00001678
		public ComplianceTaskPermanentException(string message) : this(message, null)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003482 File Offset: 0x00001682
		public ComplianceTaskPermanentException(string message, Exception innerException) : this(message, innerException, UnifiedPolicyErrorCode.Unknown)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000348D File Offset: 0x0000168D
		public ComplianceTaskPermanentException(string message, UnifiedPolicyErrorCode errorCode) : this(message, null, errorCode)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003498 File Offset: 0x00001698
		public ComplianceTaskPermanentException(string message, Exception innerException, UnifiedPolicyErrorCode errorCode) : base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000034A9 File Offset: 0x000016A9
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000034B1 File Offset: 0x000016B1
		public UnifiedPolicyErrorCode ErrorCode { get; private set; }
	}
}
