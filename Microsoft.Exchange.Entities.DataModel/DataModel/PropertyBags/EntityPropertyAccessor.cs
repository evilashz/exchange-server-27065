using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A0 RID: 160
	public class EntityPropertyAccessor<TEntity, TValue> : EntityPropertyAccessorBase<TEntity, TValue> where TEntity : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x000075C7 File Offset: 0x000057C7
		public EntityPropertyAccessor(TypedPropertyDefinition<TValue> propertyDefinition, Func<TEntity, TValue> getterDelegate, Action<TEntity, TValue> setterDelegate) : base(propertyDefinition, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000075D2 File Offset: 0x000057D2
		protected override bool IsPropertySet(TEntity container)
		{
			return container.IsPropertySet(base.PropertyDefinition);
		}
	}
}
