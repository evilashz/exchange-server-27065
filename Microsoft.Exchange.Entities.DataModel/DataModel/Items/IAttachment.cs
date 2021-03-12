using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200007F RID: 127
	public interface IAttachment : IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600036B RID: 875
		// (set) Token: 0x0600036C RID: 876
		string ContentType { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600036D RID: 877
		// (set) Token: 0x0600036E RID: 878
		bool IsInline { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600036F RID: 879
		// (set) Token: 0x06000370 RID: 880
		ExDateTime LastModifiedTime { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000371 RID: 881
		// (set) Token: 0x06000372 RID: 882
		string Name { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000373 RID: 883
		// (set) Token: 0x06000374 RID: 884
		long Size { get; set; }
	}
}
