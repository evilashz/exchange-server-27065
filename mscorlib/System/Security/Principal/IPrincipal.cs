using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020002F8 RID: 760
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IPrincipal
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600277D RID: 10109
		[__DynamicallyInvokable]
		IIdentity Identity { [__DynamicallyInvokable] get; }

		// Token: 0x0600277E RID: 10110
		[__DynamicallyInvokable]
		bool IsInRole(string role);
	}
}
