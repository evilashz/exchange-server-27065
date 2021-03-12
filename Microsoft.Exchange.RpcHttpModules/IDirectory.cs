using System;
using System.Security.Principal;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000002 RID: 2
	public interface IDirectory
	{
		// Token: 0x06000001 RID: 1
		SecurityIdentifier GetExchangeServersUsgSid();

		// Token: 0x06000002 RID: 2
		bool AllowsTokenSerializationBy(WindowsIdentity windowsIdentity);
	}
}
