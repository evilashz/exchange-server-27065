using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000801 RID: 2049
	[ComVisible(true)]
	public interface IClientResponseChannelSinkStack
	{
		// Token: 0x06005884 RID: 22660
		[SecurityCritical]
		void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

		// Token: 0x06005885 RID: 22661
		[SecurityCritical]
		void DispatchReplyMessage(IMessage msg);

		// Token: 0x06005886 RID: 22662
		[SecurityCritical]
		void DispatchException(Exception e);
	}
}
