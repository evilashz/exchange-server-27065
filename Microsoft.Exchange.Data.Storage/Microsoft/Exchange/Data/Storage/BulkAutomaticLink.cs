using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000499 RID: 1177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class BulkAutomaticLink : DisposeTrackableBase
	{
		// Token: 0x06003411 RID: 13329 RVA: 0x000D3990 File Offset: 0x000D1B90
		public BulkAutomaticLink(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(session);
			this.logger = new ContactLinkingLogger("BulkAutomaticLink", mailboxInfo);
			this.performanceTracker = new ContactLinkingPerformanceTracker(session);
			this.contactStore = new ContactStoreForBulkContactLinking(session, this.performanceTracker);
			this.automaticLink = new AutomaticLink(mailboxInfo, this.logger, this.performanceTracker, new DirectoryPersonSearcher(session.MailboxOwner), this.contactStore);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000D3A10 File Offset: 0x000D1C10
		internal BulkAutomaticLink(MailboxInfoForLinking mailboxInfo, ContactLinkingLogger logger, IContactLinkingPerformanceTracker performanceTracker, IDirectoryPersonSearcher directoryPersonSearcher, ContactStoreForBulkContactLinking contactStoreForBulkContactLinking)
		{
			Util.ThrowOnNullArgument(mailboxInfo, "mailboxInfo");
			Util.ThrowOnNullArgument(logger, "logger");
			Util.ThrowOnNullArgument(performanceTracker, "performanceTracker");
			Util.ThrowOnNullArgument(directoryPersonSearcher, "directoryPersonSearcher");
			Util.ThrowOnNullArgument(contactStoreForBulkContactLinking, "contactStoreForBulkContactLinking");
			this.logger = logger;
			this.performanceTracker = performanceTracker;
			this.contactStore = contactStoreForBulkContactLinking;
			this.automaticLink = new AutomaticLink(mailboxInfo, this.logger, this.performanceTracker, directoryPersonSearcher, this.contactStore);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000D3A94 File Offset: 0x000D1C94
		public void Link(Contact contact)
		{
			base.CheckDisposed();
			if (!AutomaticLinkConfiguration.IsBulkEnabled)
			{
				BulkAutomaticLink.Tracer.TraceDebug((long)this.GetHashCode(), "BulkAutomaticLink::Link. Suppressing Automatic Linking based on registry key value.");
				return;
			}
			this.automaticLink.LinkNewOrUpdatedContactBeforeSave(contact.CoreItem, new Func<ContactInfoForLinking, IContactStoreForContactLinking, IEnumerable<ContactInfoForLinking>>(this.GetOtherContactsEnumeratorForBulk));
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000D3AE4 File Offset: 0x000D1CE4
		public void NotifyContactSaved(Contact contact)
		{
			base.CheckDisposed();
			if (!AutomaticLinkConfiguration.IsBulkEnabled)
			{
				BulkAutomaticLink.Tracer.TraceDebug((long)this.GetHashCode(), "BulkAutomaticLink::NotifyContactSaved. Suppressing Automatic Linking based on registry key value.");
				return;
			}
			Util.ThrowOnNullArgument(contact, "contact");
			Util.ThrowOnNullArgument(contact.Id, "contact.Id");
			this.performanceTracker.Start();
			try
			{
				this.PushContactOntoWorkingSet(contact);
			}
			finally
			{
				this.performanceTracker.Stop();
			}
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000D3B60 File Offset: 0x000D1D60
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BulkAutomaticLink>(this);
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000D3B68 File Offset: 0x000D1D68
		protected override void InternalDispose(bool disposing)
		{
			if (this.performanceTracker != null)
			{
				this.logger.LogEvent(this.performanceTracker.GetLogEvent());
				this.performanceTracker = null;
			}
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000D3B8F File Offset: 0x000D1D8F
		private void PushContactOntoWorkingSet(Contact contact)
		{
			contact.PropertyBag.Load(ContactInfoForLinking.Properties);
			this.contactStore.PushContactOntoWorkingSet(contact);
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000D3BAD File Offset: 0x000D1DAD
		private IEnumerable<ContactInfoForLinking> GetOtherContactsEnumeratorForBulk(ContactInfoForLinking contactInfoContactBeingSaved, IContactStoreForContactLinking contactStoreForContactLinking)
		{
			return contactStoreForContactLinking.GetAllContacts().Take(AutomaticLink.MaximumNumberOfContactsToProcess.Value);
		}

		// Token: 0x04001BFE RID: 7166
		internal static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001BFF RID: 7167
		private readonly ContactLinkingLogger logger;

		// Token: 0x04001C00 RID: 7168
		private readonly AutomaticLink automaticLink;

		// Token: 0x04001C01 RID: 7169
		private ContactStoreForBulkContactLinking contactStore;

		// Token: 0x04001C02 RID: 7170
		private IContactLinkingPerformanceTracker performanceTracker;
	}
}
