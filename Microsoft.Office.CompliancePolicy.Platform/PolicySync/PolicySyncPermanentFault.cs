using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FE RID: 254
	[DataContract]
	[Serializable]
	public class PolicySyncPermanentFault : PolicySyncFaultBase
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00014C58 File Offset: 0x00012E58
		public PolicySyncPermanentFault(int errorCode, string message, string serverIdentifier, SyncCallerContext callerContext) : base(errorCode, message, serverIdentifier, callerContext)
		{
		}

		// Token: 0x040003E9 RID: 1001
		public const int Unknown = 0;

		// Token: 0x040003EA RID: 1002
		public const int TenantNotFoundInRegion = 1;

		// Token: 0x040003EB RID: 1003
		public const int UnauthorizedAccess = -1;

		// Token: 0x040003EC RID: 1004
		public const int GlsError = 2;
	}
}
