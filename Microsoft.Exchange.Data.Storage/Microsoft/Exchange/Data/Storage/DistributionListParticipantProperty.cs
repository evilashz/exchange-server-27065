using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C55 RID: 3157
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DistributionListParticipantProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F65 RID: 28517 RVA: 0x001DFB0C File Offset: 0x001DDD0C
		internal DistributionListParticipantProperty() : base("DistributionListParticipant", typeof(Participant), PropertyFlags.ReadOnly, Array<PropertyDefinitionConstraint>.Empty, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.EntryId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.DisplayNameFirstLast, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x001DFB68 File Offset: 0x001DDD68
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			if (!ObjectClass.IsOfClass(propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass), "IPM.DistList"))
			{
				return new PropertyError(this, PropertyErrorCode.NotSupported);
			}
			byte[] array = propertyBag.GetValue(InternalSchema.EntryId) as byte[];
			if (array != null)
			{
				return new Participant(propertyBag.GetValue(InternalSchema.DisplayNameFirstLast) as string, null, "MAPIPDL", new StoreParticipantOrigin(StoreObjectId.FromProviderSpecificId(array)), new KeyValuePair<PropertyDefinition, object>[0]);
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}
	}
}
