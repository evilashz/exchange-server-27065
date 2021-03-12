using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200008C RID: 140
	public interface IReferenceAttachment : IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003AF RID: 943
		// (set) Token: 0x060003B0 RID: 944
		string PathName { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003B1 RID: 945
		// (set) Token: 0x060003B2 RID: 946
		string ProviderEndpointUrl { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003B3 RID: 947
		// (set) Token: 0x060003B4 RID: 948
		string ProviderType { get; set; }
	}
}
