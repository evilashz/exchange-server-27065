using System;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.ReliableActions
{
	// Token: 0x02000048 RID: 72
	internal interface IActionQueue : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001E3 RID: 483
		// (set) Token: 0x060001E4 RID: 484
		ActionInfo[] ActionsToAdd { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001E5 RID: 485
		// (set) Token: 0x060001E6 RID: 486
		Guid[] ActionsToRemove { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001E7 RID: 487
		// (set) Token: 0x060001E8 RID: 488
		bool HasData { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001E9 RID: 489
		// (set) Token: 0x060001EA RID: 490
		ActionInfo[] OriginalActions { get; set; }
	}
}
