using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FF RID: 255
	[DataContract]
	[Serializable]
	public class PolicySyncTransientFault : PolicySyncFaultBase
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00014C65 File Offset: 0x00012E65
		public PolicySyncTransientFault(int errorCode, string message, string serverIdentifier, SyncCallerContext callerContext) : base(errorCode, message, serverIdentifier, callerContext)
		{
		}
	}
}
