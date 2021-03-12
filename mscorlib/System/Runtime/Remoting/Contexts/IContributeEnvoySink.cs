using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E7 RID: 2023
	[ComVisible(true)]
	public interface IContributeEnvoySink
	{
		// Token: 0x060057BE RID: 22462
		[SecurityCritical]
		IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
