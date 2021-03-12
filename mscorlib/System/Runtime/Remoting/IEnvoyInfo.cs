using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078A RID: 1930
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600546A RID: 21610
		// (set) Token: 0x0600546B RID: 21611
		IMessageSink EnvoySinks { [SecurityCritical] get; [SecurityCritical] set; }
	}
}
