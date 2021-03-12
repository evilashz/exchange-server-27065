using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x02000789 RID: 1929
	[ComVisible(true)]
	public interface IChannelInfo
	{
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06005468 RID: 21608
		// (set) Token: 0x06005469 RID: 21609
		object[] ChannelData { [SecurityCritical] get; [SecurityCritical] set; }
	}
}
