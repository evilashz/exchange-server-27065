using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004AF RID: 1199
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactInfoForLinkingWithPropertyBagUpdater : ContactInfoForLinkingFromPropertyBag
	{
		// Token: 0x06003536 RID: 13622 RVA: 0x000D6EB8 File Offset: 0x000D50B8
		private ContactInfoForLinkingWithPropertyBagUpdater(PropertyBagAdaptor propertyBagAdaptor, MailboxSession mailboxSession, IStorePropertyBag propertyBag, ICollection<PropertyDefinition> loadedProperties) : base(propertyBagAdaptor, mailboxSession, propertyBag)
		{
			this.originalPropertyBag = propertyBag;
			this.loadedProperties = loadedProperties;
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000D6ED2 File Offset: 0x000D50D2
		public IStorePropertyBag PropertyBag
		{
			get
			{
				return this.writablePropertyBag ?? this.originalPropertyBag;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000D6EE4 File Offset: 0x000D50E4
		private IStorePropertyBag WritablePropertyBag
		{
			get
			{
				if (this.writablePropertyBag == null)
				{
					this.writablePropertyBag = ContactInfoForLinkingWithPropertyBagUpdater.CloneToWritablePropertyBag(this.originalPropertyBag, this.loadedProperties);
				}
				return this.writablePropertyBag;
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000D6F0C File Offset: 0x000D510C
		public static ContactInfoForLinkingWithPropertyBagUpdater Create(MailboxSession mailboxSession, IStorePropertyBag propertyBag, ICollection<PropertyDefinition> loadedProperties)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			Util.ThrowOnNullArgument(loadedProperties, "loadedProperties");
			PropertyBagAdaptor propertyBagAdaptor = PropertyBagAdaptor.Create(propertyBag);
			return new ContactInfoForLinkingWithPropertyBagUpdater(propertyBagAdaptor, mailboxSession, propertyBag, loadedProperties);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000D6F4A File Offset: 0x000D514A
		protected override void UpdateContact(IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker)
		{
			base.UpdateContact(logger, performanceTracker);
			base.SetLinkingProperties(PropertyBagAdaptor.Create(this.WritablePropertyBag));
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000D6F68 File Offset: 0x000D5168
		private static IStorePropertyBag CloneToWritablePropertyBag(IStorePropertyBag propertyBag, ICollection<PropertyDefinition> loadedProperties)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			memoryPropertyBag.LoadFromStorePropertyBag(propertyBag, loadedProperties);
			return memoryPropertyBag.AsIStorePropertyBag();
		}

		// Token: 0x04001C46 RID: 7238
		private readonly IStorePropertyBag originalPropertyBag;

		// Token: 0x04001C47 RID: 7239
		private readonly ICollection<PropertyDefinition> loadedProperties;

		// Token: 0x04001C48 RID: 7240
		private IStorePropertyBag writablePropertyBag;
	}
}
