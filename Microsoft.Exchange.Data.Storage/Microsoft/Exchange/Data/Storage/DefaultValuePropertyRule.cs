using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA4 RID: 2724
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DefaultValuePropertyRule : PropertyRule
	{
		// Token: 0x06006374 RID: 25460 RVA: 0x001A33E4 File Offset: 0x001A15E4
		public DefaultValuePropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, NativeStorePropertyDefinition propertyToSet, object defaultPropertyValue) : base(name, onSetWriteEnforceLocationIdentifier, new PropertyReference[]
		{
			new PropertyReference(propertyToSet, PropertyAccess.ReadWrite)
		})
		{
			this.propertyToSet = propertyToSet;
			if (defaultPropertyValue == null)
			{
				throw new ArgumentNullException("defaultPropertyValue");
			}
			this.defaultValue = defaultPropertyValue;
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x001A342C File Offset: 0x001A162C
		protected sealed override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			bool result = false;
			object propertyValue = propertyBag.TryGetProperty(this.propertyToSet);
			if (PropertyError.IsPropertyNotFound(propertyValue))
			{
				propertyBag.SetOrDeleteProperty(this.propertyToSet, this.defaultValue);
				result = true;
			}
			return result;
		}

		// Token: 0x04003833 RID: 14387
		private readonly PropertyDefinition propertyToSet;

		// Token: 0x04003834 RID: 14388
		private readonly object defaultValue;
	}
}
