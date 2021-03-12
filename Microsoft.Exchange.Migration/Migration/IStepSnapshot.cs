using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IStepSnapshot
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C1 RID: 449
		ISnapshotId Id { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C2 RID: 450
		SnapshotStatus Status { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C3 RID: 451
		LocalizedString? ErrorMessage { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C4 RID: 452
		ExDateTime? InjectionCompletedTime { get; }
	}
}
