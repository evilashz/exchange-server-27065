using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E8 RID: 2024
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		// Token: 0x060057BF RID: 22463
		[SecurityCritical]
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
