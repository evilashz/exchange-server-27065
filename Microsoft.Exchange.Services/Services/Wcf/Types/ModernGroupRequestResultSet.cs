using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E5 RID: 2533
	[Flags]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum ModernGroupRequestResultSet
	{
		// Token: 0x040028FE RID: 10494
		General = 1,
		// Token: 0x040028FF RID: 10495
		Members = 2,
		// Token: 0x04002900 RID: 10496
		Owners = 4,
		// Token: 0x04002901 RID: 10497
		ExternalResources = 8,
		// Token: 0x04002902 RID: 10498
		GroupMailboxProperties = 16,
		// Token: 0x04002903 RID: 10499
		ForceReload = 65536
	}
}
