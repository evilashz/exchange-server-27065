using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x020001D4 RID: 468
	[ComVisible(true)]
	public interface ISecurityPolicyEncodable
	{
		// Token: 0x06001C77 RID: 7287
		SecurityElement ToXml(PolicyLevel level);

		// Token: 0x06001C78 RID: 7288
		void FromXml(SecurityElement e, PolicyLevel level);
	}
}
