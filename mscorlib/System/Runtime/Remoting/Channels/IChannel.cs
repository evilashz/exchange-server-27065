using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000810 RID: 2064
	[ComVisible(true)]
	public interface IChannel
	{
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060058E9 RID: 22761
		int ChannelPriority { [SecurityCritical] get; }

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060058EA RID: 22762
		string ChannelName { [SecurityCritical] get; }

		// Token: 0x060058EB RID: 22763
		[SecurityCritical]
		string Parse(string url, out string objectURI);
	}
}
