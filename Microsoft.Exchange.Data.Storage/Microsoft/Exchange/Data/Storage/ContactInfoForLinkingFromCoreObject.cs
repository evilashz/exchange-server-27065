using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004AC RID: 1196
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactInfoForLinkingFromCoreObject : ContactInfoForLinking
	{
		// Token: 0x0600351D RID: 13597 RVA: 0x000D69D4 File Offset: 0x000D4BD4
		public static ContactInfoForLinkingFromCoreObject Create(ICoreItem coreObject)
		{
			Util.ThrowOnNullArgument(coreObject, "coreObject");
			coreObject.PropertyBag.Load(ContactInfoForLinking.Properties);
			PropertyBagAdaptor propertyBagAdaptor = PropertyBagAdaptor.Create(coreObject);
			return new ContactInfoForLinkingFromCoreObject(propertyBagAdaptor, coreObject);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000D6A0A File Offset: 0x000D4C0A
		private ContactInfoForLinkingFromCoreObject(PropertyBagAdaptor propertyBagAdaptor, ICoreItem coreObject) : base(propertyBagAdaptor)
		{
			this.coreObject = coreObject;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000D6A1A File Offset: 0x000D4C1A
		protected override void UpdateContact(IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker)
		{
			base.SetLinkingProperties(PropertyBagAdaptor.Create(this.coreObject));
			ContactInfoForLinking.Tracer.TraceDebug<VersionedId, string>((long)this.GetHashCode(), "ContactInfoForLinkingFromCoreObject.UpdateContact: setting link properties but not saving contact with id = {0}; given-name: {1}", base.ItemId, base.GivenName);
		}

		// Token: 0x04001C3E RID: 7230
		private readonly ICoreItem coreObject;
	}
}
