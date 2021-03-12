using System;

namespace System.Security.Policy
{
	// Token: 0x0200032C RID: 812
	internal interface IReportMatchMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002955 RID: 10581
		bool Check(Evidence evidence, out object usedEvidence);
	}
}
