using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082B RID: 2091
	[ComVisible(true)]
	public interface IMessageCtrl
	{
		// Token: 0x06005960 RID: 22880
		[SecurityCritical]
		void Cancel(int msToCancel);
	}
}
