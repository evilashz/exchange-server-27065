using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028E RID: 654
	internal interface ITimeEntry : IDisposable
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600180A RID: 6154
		TimeId TimeId { get; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600180B RID: 6155
		DateTime StartTime { get; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600180C RID: 6156
		DateTime EndTime { get; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600180D RID: 6157
		TimeSpan ElapsedInclusive { get; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600180E RID: 6158
		TimeSpan ElapsedExclusive { get; }
	}
}
