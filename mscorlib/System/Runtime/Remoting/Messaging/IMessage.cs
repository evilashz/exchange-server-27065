using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082A RID: 2090
	[ComVisible(true)]
	public interface IMessage
	{
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600595F RID: 22879
		IDictionary Properties { [SecurityCritical] get; }
	}
}
