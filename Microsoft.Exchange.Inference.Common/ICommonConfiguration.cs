using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICommonConfiguration
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25
		bool OutlookActivityProcessingEnabled { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26
		bool OutlookActivityProcessingEnabledInEba { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27
		TimeSpan OutlookActivityProcessingCutoffWindow { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28
		bool PersistedLabelsEnabled { get; }
	}
}
