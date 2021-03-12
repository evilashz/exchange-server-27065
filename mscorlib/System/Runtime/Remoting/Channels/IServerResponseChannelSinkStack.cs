using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000804 RID: 2052
	[ComVisible(true)]
	public interface IServerResponseChannelSinkStack
	{
		// Token: 0x06005893 RID: 22675
		[SecurityCritical]
		void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06005894 RID: 22676
		[SecurityCritical]
		Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
	}
}
