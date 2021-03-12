using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000194 RID: 404
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessagePropertyFilter : IPropertyFilter
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007EC RID: 2028
		bool IncludeRecipients { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007ED RID: 2029
		bool IncludeAttachments { get; }
	}
}
