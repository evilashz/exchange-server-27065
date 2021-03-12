using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001D2 RID: 466
	[ComVisible(true)]
	public interface IPermission : ISecurityEncodable
	{
		// Token: 0x06001C70 RID: 7280
		IPermission Copy();

		// Token: 0x06001C71 RID: 7281
		IPermission Intersect(IPermission target);

		// Token: 0x06001C72 RID: 7282
		IPermission Union(IPermission target);

		// Token: 0x06001C73 RID: 7283
		bool IsSubsetOf(IPermission target);

		// Token: 0x06001C74 RID: 7284
		void Demand();
	}
}
