using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F6 RID: 2038
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		// Token: 0x040027F6 RID: 10230
		Null,
		// Token: 0x040027F7 RID: 10231
		Initial,
		// Token: 0x040027F8 RID: 10232
		Active,
		// Token: 0x040027F9 RID: 10233
		Renewing,
		// Token: 0x040027FA RID: 10234
		Expired
	}
}
