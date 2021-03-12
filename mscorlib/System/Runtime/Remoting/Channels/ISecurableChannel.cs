using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000827 RID: 2087
	public interface ISecurableChannel
	{
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005948 RID: 22856
		// (set) Token: 0x06005949 RID: 22857
		bool IsSecured { [SecurityCritical] get; [SecurityCritical] set; }
	}
}
