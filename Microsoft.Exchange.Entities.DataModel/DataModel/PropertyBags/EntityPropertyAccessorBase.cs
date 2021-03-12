using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200009F RID: 159
	public abstract class EntityPropertyAccessorBase<TEntity, TValue> : PropertyAccessor<TEntity, TValue>
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00007558 File Offset: 0x00005758
		protected EntityPropertyAccessorBase(TypedPropertyDefinition<TValue> propertyDefinition, Func<TEntity, TValue> getterDelegate, Action<TEntity, TValue> setterDelegate) : base(false)
		{
			this.PropertyDefinition = propertyDefinition;
			this.getterDelegate = getterDelegate;
			this.setterDelegate = setterDelegate;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00007576 File Offset: 0x00005776
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000757E File Offset: 0x0000577E
		public TypedPropertyDefinition<TValue> PropertyDefinition { get; private set; }

		// Token: 0x06000407 RID: 1031
		protected abstract bool IsPropertySet(TEntity container);

		// Token: 0x06000408 RID: 1032 RVA: 0x00007587 File Offset: 0x00005787
		protected override void PerformSet(TEntity container, TValue value)
		{
			this.setterDelegate(container, value);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00007596 File Offset: 0x00005796
		protected override bool PerformTryGetValue(TEntity container, out TValue value)
		{
			if (this.IsPropertySet(container))
			{
				value = this.getterDelegate(container);
				return true;
			}
			value = this.PropertyDefinition.DefaultValue;
			return false;
		}

		// Token: 0x040001F6 RID: 502
		private readonly Func<TEntity, TValue> getterDelegate;

		// Token: 0x040001F7 RID: 503
		private readonly Action<TEntity, TValue> setterDelegate;
	}
}
