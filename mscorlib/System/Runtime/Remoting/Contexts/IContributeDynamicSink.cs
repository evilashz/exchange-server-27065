using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E6 RID: 2022
	[ComVisible(true)]
	public interface IContributeDynamicSink
	{
		// Token: 0x060057BD RID: 22461
		[SecurityCritical]
		IDynamicMessageSink GetDynamicSink();
	}
}
