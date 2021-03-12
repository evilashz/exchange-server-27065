using System;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C69 RID: 3177
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class InferenceOLKUserActivityLoggingEnabledSmartProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FD1 RID: 28625 RVA: 0x001E164E File Offset: 0x001DF84E
		public InferenceOLKUserActivityLoggingEnabledSmartProperty() : base("InferenceOLKUserActivityLoggingEnabled", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[0])
		{
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x001E1671 File Offset: 0x001DF871
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return ActivityLogHelper.IsActivityLoggingEnabled(false);
		}
	}
}
