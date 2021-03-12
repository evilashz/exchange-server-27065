using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ReliableActions
{
	// Token: 0x02000B10 RID: 2832
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActionQueueHasDataProperty : SmartPropertyDefinition
	{
		// Token: 0x060066C6 RID: 26310 RVA: 0x001B3E98 File Offset: 0x001B2098
		public ActionQueueHasDataProperty(NativeStorePropertyDefinition queueHasDataFlagProperty) : base("ActionQueueHasData", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(queueHasDataFlagProperty, PropertyDependencyType.NeedForRead)
		})
		{
			ArgumentValidator.ThrowIfNull("queueHasDataFlagProperty", queueHasDataFlagProperty);
			this.queueHasDataFlagProperty = queueHasDataFlagProperty;
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x001B3EE4 File Offset: 0x001B20E4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValueOrDefault<bool>(this.queueHasDataFlagProperty);
		}

		// Token: 0x04003A46 RID: 14918
		private readonly NativeStorePropertyDefinition queueHasDataFlagProperty;
	}
}
