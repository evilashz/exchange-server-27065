using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000020 RID: 32
	[Flags]
	public enum FailureMode
	{
		// Token: 0x04000023 RID: 35
		Transient = 1,
		// Token: 0x04000024 RID: 36
		Permanent = 2,
		// Token: 0x04000025 RID: 37
		All = 3
	}
}
