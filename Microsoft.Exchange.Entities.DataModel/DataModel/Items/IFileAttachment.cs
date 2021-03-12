using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000084 RID: 132
	public interface IFileAttachment : IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000392 RID: 914
		// (set) Token: 0x06000393 RID: 915
		byte[] Content { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000394 RID: 916
		// (set) Token: 0x06000395 RID: 917
		string ContentId { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000396 RID: 918
		// (set) Token: 0x06000397 RID: 919
		string ContentLocation { get; set; }
	}
}
