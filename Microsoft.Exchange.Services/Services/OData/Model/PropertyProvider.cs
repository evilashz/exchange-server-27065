using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E87 RID: 3719
	internal abstract class PropertyProvider
	{
		// Token: 0x060060D1 RID: 24785
		public abstract void GetPropertyFromDataSource(Entity entity, PropertyDefinition property, object dataSource);

		// Token: 0x060060D2 RID: 24786
		public abstract void SetPropertyToDataSource(Entity entity, PropertyDefinition property, object dataSource);
	}
}
