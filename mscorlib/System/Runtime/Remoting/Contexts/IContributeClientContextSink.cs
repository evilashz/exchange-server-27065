using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E5 RID: 2021
	[ComVisible(true)]
	public interface IContributeClientContextSink
	{
		// Token: 0x060057BC RID: 22460
		[SecurityCritical]
		IMessageSink GetClientContextSink(IMessageSink nextSink);
	}
}
