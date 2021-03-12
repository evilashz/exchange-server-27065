using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.ReliableActions
{
	// Token: 0x02000049 RID: 73
	internal interface IActionPropagationState : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001EB RID: 491
		// (set) Token: 0x060001EC RID: 492
		Guid? LastExecutedAction { get; set; }
	}
}
