using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000803 RID: 2051
	[ComVisible(true)]
	public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
	{
		// Token: 0x0600588E RID: 22670
		[SecurityCritical]
		void Push(IServerChannelSink sink, object state);

		// Token: 0x0600588F RID: 22671
		[SecurityCritical]
		object Pop(IServerChannelSink sink);

		// Token: 0x06005890 RID: 22672
		[SecurityCritical]
		void Store(IServerChannelSink sink, object state);

		// Token: 0x06005891 RID: 22673
		[SecurityCritical]
		void StoreAndDispatch(IServerChannelSink sink, object state);

		// Token: 0x06005892 RID: 22674
		[SecurityCritical]
		void ServerCallback(IAsyncResult ar);
	}
}
