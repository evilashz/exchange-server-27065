using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020007D9 RID: 2009
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		// Token: 0x06005754 RID: 22356
		[SecurityCritical]
		void MarshaledObject(object obj, ObjRef or);

		// Token: 0x06005755 RID: 22357
		[SecurityCritical]
		void UnmarshaledObject(object obj, ObjRef or);

		// Token: 0x06005756 RID: 22358
		[SecurityCritical]
		void DisconnectedObject(object obj);
	}
}
