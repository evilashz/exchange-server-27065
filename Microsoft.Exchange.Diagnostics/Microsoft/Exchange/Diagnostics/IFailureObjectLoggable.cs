using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B5 RID: 437
	public interface IFailureObjectLoggable
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C13 RID: 3091
		Guid ObjectGuid { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C14 RID: 3092
		string ObjectType { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C15 RID: 3093
		int Flags { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C16 RID: 3094
		string FailureContext { get; }
	}
}
