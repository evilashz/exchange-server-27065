using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082C RID: 2092
	[ComVisible(true)]
	public interface IMessageSink
	{
		// Token: 0x06005961 RID: 22881
		[SecurityCritical]
		IMessage SyncProcessMessage(IMessage msg);

		// Token: 0x06005962 RID: 22882
		[SecurityCritical]
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005963 RID: 22883
		IMessageSink NextSink { [SecurityCritical] get; }
	}
}
