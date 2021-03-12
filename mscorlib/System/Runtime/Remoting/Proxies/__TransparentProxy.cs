using System;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007D7 RID: 2007
	internal sealed class __TransparentProxy
	{
		// Token: 0x0600574F RID: 22351 RVA: 0x00133391 File Offset: 0x00131591
		private __TransparentProxy()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
		}

		// Token: 0x04002796 RID: 10134
		[SecurityCritical]
		private RealProxy _rp;

		// Token: 0x04002797 RID: 10135
		private object _stubData;

		// Token: 0x04002798 RID: 10136
		private IntPtr _pMT;

		// Token: 0x04002799 RID: 10137
		private IntPtr _pInterfaceMT;

		// Token: 0x0400279A RID: 10138
		private IntPtr _stub;
	}
}
