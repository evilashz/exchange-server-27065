using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000425 RID: 1061
	[Flags]
	public enum RpcClientAccessResponsibility
	{
		// Token: 0x0400203E RID: 8254
		None = 0,
		// Token: 0x0400203F RID: 8255
		Mailboxes = 1,
		// Token: 0x04002040 RID: 8256
		PublicFolders = 2
	}
}
