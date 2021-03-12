using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000800 RID: 2048
	[ComVisible(true)]
	public interface IClientChannelSinkStack : IClientResponseChannelSinkStack
	{
		// Token: 0x06005882 RID: 22658
		[SecurityCritical]
		void Push(IClientChannelSink sink, object state);

		// Token: 0x06005883 RID: 22659
		[SecurityCritical]
		object Pop(IClientChannelSink sink);
	}
}
