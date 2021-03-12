using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E9 RID: 2025
	[ComVisible(true)]
	public interface IContributeServerContextSink
	{
		// Token: 0x060057C0 RID: 22464
		[SecurityCritical]
		IMessageSink GetServerContextSink(IMessageSink nextSink);
	}
}
