using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004C0 RID: 1216
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WinRTSynchronizationContextFactoryBase
	{
		// Token: 0x06003A70 RID: 14960 RVA: 0x000DDB40 File Offset: 0x000DBD40
		[SecurityCritical]
		public virtual SynchronizationContext Create(object coreDispatcher)
		{
			return null;
		}
	}
}
