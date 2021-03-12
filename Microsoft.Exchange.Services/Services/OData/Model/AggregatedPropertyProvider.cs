using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E88 RID: 3720
	internal abstract class AggregatedPropertyProvider : PropertyProvider
	{
		// Token: 0x060060D4 RID: 24788 RVA: 0x0012E5C4 File Offset: 0x0012C7C4
		public override void GetPropertyFromDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			PropertyProvider propertyProvider = this.SelectProvider(entity.Schema);
			propertyProvider.GetPropertyFromDataSource(entity, property, dataSource);
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0012E608 File Offset: 0x0012C808
		public override void SetPropertyToDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			PropertyProvider propertyProvider = this.SelectProvider(entity.Schema);
			propertyProvider.SetPropertyToDataSource(entity, property, dataSource);
		}

		// Token: 0x060060D6 RID: 24790
		public abstract PropertyProvider SelectProvider(EntitySchema schema);
	}
}
