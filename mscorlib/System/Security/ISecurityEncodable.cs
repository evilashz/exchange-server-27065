using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001D3 RID: 467
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		// Token: 0x06001C75 RID: 7285
		SecurityElement ToXml();

		// Token: 0x06001C76 RID: 7286
		void FromXml(SecurityElement e);
	}
}
