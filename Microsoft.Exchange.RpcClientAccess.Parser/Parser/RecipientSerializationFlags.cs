using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000083 RID: 131
	[Flags]
	internal enum RecipientSerializationFlags
	{
		// Token: 0x040001BB RID: 443
		RecipientRowId = 1,
		// Token: 0x040001BC RID: 444
		ExtraUnicodeProperties = 2,
		// Token: 0x040001BD RID: 445
		CodePageId = 4
	}
}
