using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200086D RID: 2157
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		// Token: 0x0400293B RID: 10555
		Construction = 4,
		// Token: 0x0400293C RID: 10556
		Context = 8,
		// Token: 0x0400293D RID: 10557
		AppDomain = 12,
		// Token: 0x0400293E RID: 10558
		Process = 16,
		// Token: 0x0400293F RID: 10559
		Machine = 20
	}
}
