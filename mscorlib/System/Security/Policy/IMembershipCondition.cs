using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200032B RID: 811
	[ComVisible(true)]
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002951 RID: 10577
		bool Check(Evidence evidence);

		// Token: 0x06002952 RID: 10578
		IMembershipCondition Copy();

		// Token: 0x06002953 RID: 10579
		string ToString();

		// Token: 0x06002954 RID: 10580
		bool Equals(object obj);
	}
}
