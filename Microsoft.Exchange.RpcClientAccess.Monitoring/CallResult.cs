using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CallResult
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7
		public abstract bool IsSuccessful { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002132 File Offset: 0x00000332
		public TimeSpan Latency { get; internal set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000213B File Offset: 0x0000033B
		public virtual void Validate()
		{
		}
	}
}
