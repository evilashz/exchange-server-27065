using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A99 RID: 2713
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class BinPropertyRule<TDepend, TSet> : PropertyRule
	{
		// Token: 0x0600634A RID: 25418 RVA: 0x001A2CA4 File Offset: 0x001A0EA4
		public BinPropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, NativeStorePropertyDefinition propertyToDepend, NativeStorePropertyDefinition propertyToSet) : base(name, onSetWriteEnforceLocationIdentifier, new PropertyReference[]
		{
			new PropertyReference(propertyToDepend, PropertyAccess.Read),
			new PropertyReference(propertyToSet, PropertyAccess.ReadWrite)
		})
		{
			this.propertyToDepend = propertyToDepend;
			this.propertyToSet = propertyToSet;
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x001A2CE8 File Offset: 0x001A0EE8
		protected sealed override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			bool result = false;
			bool isPropertyDirty = propertyBag.IsPropertyDirty(this.propertyToSet);
			object propertyValue = propertyBag.TryGetProperty(this.propertyToSet);
			if (this.ShouldEnforce(isPropertyDirty, propertyValue))
			{
				TSet valueOrDefault = propertyBag.GetValueOrDefault<TSet>(this.propertyToSet);
				TDepend valueOrDefault2 = propertyBag.GetValueOrDefault<TDepend>(this.propertyToDepend);
				TSet tset;
				if (this.CalculateValue(valueOrDefault2, valueOrDefault, out tset) && (PropertyError.IsPropertyNotFound(propertyValue) || !object.Equals(tset, valueOrDefault)))
				{
					propertyBag.SetOrDeleteProperty(this.propertyToSet, tset);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600634C RID: 25420
		protected abstract bool CalculateValue(TDepend propertyToDependValue, TSet propertyToSetValue, out TSet newValue);

		// Token: 0x0600634D RID: 25421
		protected abstract bool ShouldEnforce(bool isPropertyDirty, object propertyValue);

		// Token: 0x04003828 RID: 14376
		private readonly NativeStorePropertyDefinition propertyToSet;

		// Token: 0x04003829 RID: 14377
		private readonly NativeStorePropertyDefinition propertyToDepend;
	}
}
