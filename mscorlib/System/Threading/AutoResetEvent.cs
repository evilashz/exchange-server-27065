using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004BD RID: 1213
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class AutoResetEvent : EventWaitHandle
	{
		// Token: 0x06003A6B RID: 14955 RVA: 0x000DDB36 File Offset: 0x000DBD36
		[__DynamicallyInvokable]
		public AutoResetEvent(bool initialState) : base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
