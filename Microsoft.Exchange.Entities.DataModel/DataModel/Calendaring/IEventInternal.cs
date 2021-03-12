using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200004A RID: 74
	internal interface IEventInternal : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001ED RID: 493
		// (set) Token: 0x060001EE RID: 494
		bool MarkAllPropagatedPropertiesAsException { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001EF RID: 495
		// (set) Token: 0x060001F0 RID: 496
		bool SeriesToInstancePropagation { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001F1 RID: 497
		// (set) Token: 0x060001F2 RID: 498
		bool IsReceived { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F3 RID: 499
		// (set) Token: 0x060001F4 RID: 500
		int InstanceCreationIndex { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001F5 RID: 501
		// (set) Token: 0x060001F6 RID: 502
		int SeriesCreationHash { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F7 RID: 503
		// (set) Token: 0x060001F8 RID: 504
		int SeriesSequenceNumber { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F9 RID: 505
		// (set) Token: 0x060001FA RID: 506
		string GlobalObjectId { get; set; }
	}
}
