using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004AE RID: 1198
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactInfoForLinkingFromPropertyBag : ContactInfoForLinking
	{
		// Token: 0x06003532 RID: 13618 RVA: 0x000D6DB2 File Offset: 0x000D4FB2
		protected ContactInfoForLinkingFromPropertyBag(PropertyBagAdaptor propertyBagAdaptor, MailboxSession mailboxSession, IStorePropertyBag propertyBag) : base(propertyBagAdaptor)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000D6DC4 File Offset: 0x000D4FC4
		public static ContactInfoForLinkingFromPropertyBag Create(MailboxSession mailboxSession, IStorePropertyBag propertyBag)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			PropertyBagAdaptor propertyBagAdaptor = PropertyBagAdaptor.Create(propertyBag);
			return new ContactInfoForLinkingFromPropertyBag(propertyBagAdaptor, mailboxSession, propertyBag);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000D6E60 File Offset: 0x000D5060
		protected override void UpdateContact(IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker)
		{
			ContactInfoForLinking.Tracer.TraceDebug<VersionedId, string>((long)this.GetHashCode(), "ContactInfoForLinkingFromPropertyBag.UpdateContact: setting link properties AND saving contact with id = {0}; given-name: {1}", base.ItemId, base.GivenName);
			base.RetryOnTransientExceptionCatchObjectNotFoundException(logger, "update of contact Id=" + base.ItemId, delegate
			{
				using (Contact contact = Contact.Bind(this.mailboxSession, base.ItemId, new PropertyDefinition[]
				{
					ContactSchema.PersonId
				}))
				{
					base.SetLinkingProperties(PropertyBagAdaptor.Create(contact));
					AutomaticLink.DisableAutomaticLinkingForItem(contact);
					contact.Save(SaveMode.NoConflictResolution);
				}
			});
			performanceTracker.IncrementContactsUpdated();
		}

		// Token: 0x04001C45 RID: 7237
		private readonly MailboxSession mailboxSession;
	}
}
