using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000013 RID: 19
	public interface IStorageEntity : IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
	}
}
