using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004D3 RID: 1235
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class ManualResetEvent : EventWaitHandle
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x000DF8CE File Offset: 0x000DDACE
		[__DynamicallyInvokable]
		public ManualResetEvent(bool initialState) : base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
