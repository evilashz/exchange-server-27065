using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007EB RID: 2027
	[ComVisible(true)]
	public interface IDynamicMessageSink
	{
		// Token: 0x060057C2 RID: 22466
		[SecurityCritical]
		void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);

		// Token: 0x060057C3 RID: 22467
		[SecurityCritical]
		void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);
	}
}
