using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AAB RID: 2731
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PrimaryPropertyRule : PropertyRule
	{
		// Token: 0x0600638E RID: 25486 RVA: 0x001A3BF8 File Offset: 0x001A1DF8
		public PrimaryPropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, NativeStorePropertyDefinition primaryProperty, NativeStorePropertyDefinition secondaryProperty) : base(name, onSetWriteEnforceLocationIdentifier, new PropertyReference[]
		{
			new PropertyReference(primaryProperty, PropertyAccess.Read),
			new PropertyReference(secondaryProperty, PropertyAccess.Write)
		})
		{
			if (primaryProperty.Type != secondaryProperty.Type)
			{
				throw new ArgumentException("properties should be same type for PrimaryPropertyRule.");
			}
			this.primary = primaryProperty;
			this.secondary = secondaryProperty;
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x001A3C58 File Offset: 0x001A1E58
		protected override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			bool result = false;
			if (propertyBag.IsPropertyDirty(this.primary))
			{
				propertyBag.SetOrDeleteProperty(this.secondary, propertyBag.TryGetProperty(this.primary));
				result = true;
			}
			return result;
		}

		// Token: 0x0400383C RID: 14396
		private readonly PropertyDefinition primary;

		// Token: 0x0400383D RID: 14397
		private readonly PropertyDefinition secondary;
	}
}
