using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020002F7 RID: 759
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IIdentity
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600277A RID: 10106
		[__DynamicallyInvokable]
		string Name { [__DynamicallyInvokable] get; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600277B RID: 10107
		[__DynamicallyInvokable]
		string AuthenticationType { [__DynamicallyInvokable] get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600277C RID: 10108
		[__DynamicallyInvokable]
		bool IsAuthenticated { [__DynamicallyInvokable] get; }
	}
}
