using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA8 RID: 2728
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PersonIdPropertyRule
	{
		// Token: 0x06006383 RID: 25475 RVA: 0x001A392C File Offset: 0x001A1B2C
		public bool UpdateProperties(ICorePropertyBag propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, null);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				return false;
			}
			if (!ObjectClass.IsContact(valueOrDefault) && !ObjectClass.IsDistributionList(valueOrDefault))
			{
				return false;
			}
			bool valueOrDefault2 = propertyBag.GetValueOrDefault<bool>(InternalSchema.ConversationIndexTracking, false);
			if (valueOrDefault2)
			{
				return false;
			}
			propertyBag[InternalSchema.ConversationIndexTracking] = true;
			return true;
		}

		// Token: 0x04003839 RID: 14393
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x0400383A RID: 14394
		public static readonly PropertyReference[] PropertyReferences = new PropertyReference[]
		{
			new PropertyReference(InternalSchema.ItemClass, PropertyAccess.Read),
			new PropertyReference(InternalSchema.ConversationIndexTracking, PropertyAccess.ReadWrite)
		};
	}
}
