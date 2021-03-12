using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200081B RID: 2075
	[ComVisible(true)]
	public interface IChannelSinkBase
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005905 RID: 22789
		IDictionary Properties { [SecurityCritical] get; }
	}
}
