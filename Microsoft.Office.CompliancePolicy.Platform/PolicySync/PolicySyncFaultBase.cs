using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FD RID: 253
	[DataContract]
	[Serializable]
	public abstract class PolicySyncFaultBase
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00014BC8 File Offset: 0x00012DC8
		protected PolicySyncFaultBase(int errorCode, string message, string serverIdentifier, SyncCallerContext callerContext)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("message", message);
			ArgumentValidator.ThrowIfNullOrEmpty("serverIdentifier", serverIdentifier);
			this.ErrorCode = errorCode;
			this.Message = message;
			this.ServerIdentifier = serverIdentifier;
			this.CallerContext = callerContext;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00014C03 File Offset: 0x00012E03
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00014C0B File Offset: 0x00012E0B
		[DataMember]
		public int ErrorCode { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00014C14 File Offset: 0x00012E14
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00014C1C File Offset: 0x00012E1C
		[DataMember]
		public string Message { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00014C25 File Offset: 0x00012E25
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00014C2D File Offset: 0x00012E2D
		[DataMember]
		public string ServerIdentifier { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00014C36 File Offset: 0x00012E36
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00014C3E File Offset: 0x00012E3E
		[DataMember]
		public bool IsPerObjectException { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00014C47 File Offset: 0x00012E47
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00014C4F File Offset: 0x00012E4F
		[DataMember]
		public SyncCallerContext CallerContext { get; private set; }
	}
}
