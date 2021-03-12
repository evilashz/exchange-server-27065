using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F1 RID: 1265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GALLinkingFixer
	{
		// Token: 0x060036F1 RID: 14065 RVA: 0x000DDBA4 File Offset: 0x000DBDA4
		public GALLinkingFixer(MailboxSession mailboxSession, PersonId personId, Guid adObjectIdGuid, List<IStorePropertyBag> contacts, ICollection<PropertyDefinition> contactProperties)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("personId", personId);
			ArgumentValidator.ThrowIfEmpty("adObjectIdGuid", adObjectIdGuid);
			ArgumentValidator.ThrowIfNull("contacts", contacts);
			ArgumentValidator.ThrowIfZeroOrNegative("contacts.Count", contacts.Count);
			ArgumentValidator.ThrowIfNull("contactProperties", contactProperties);
			this.mailboxSession = mailboxSession;
			this.personId = personId;
			this.adObjectIdGuid = adObjectIdGuid;
			this.contacts = contacts;
			this.contactProperties = contactProperties;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000DDC3C File Offset: 0x000DBE3C
		public bool TryUpdateGALLinkingPropertiesIfChanged(ADRawEntry adPerson)
		{
			ArgumentValidator.ThrowIfNull("adPerson", adPerson);
			GALLinkingFixer.Tracer.TraceDebug<Guid>((long)this.personId.GetHashCode(), "GALLinkingFixer::Updating GAL Linking properties on contact from the latest one in AD Person: {0}.", adPerson.Id.ObjectGuid);
			ContactInfoForLinkingFromDirectory adContactInfo = ContactInfoForLinkingFromDirectory.Create(adPerson);
			return this.TryEnumerateContactsAndApplyUpdates(delegate(ContactInfoForLinking contactInfo)
			{
				contactInfo.SetGALLink(adContactInfo);
			}, "UpdateGALLinkingPropertiesIfChanged", new SchemaBasedLogEvent<ContactLinkingLogSchema.GALLinkFixup>
			{
				{
					ContactLinkingLogSchema.GALLinkFixup.PersonId,
					this.personId
				},
				{
					ContactLinkingLogSchema.GALLinkFixup.ADObjectIdGuid,
					this.adObjectIdGuid
				}
			});
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000DDCEC File Offset: 0x000DBEEC
		public bool TryUnlinkContactsFromGAL()
		{
			ArgumentValidator.ThrowIfNull("contacts", this.contacts);
			ArgumentValidator.ThrowIfZeroOrNegative("contacts.Count", this.contacts.Count);
			GALLinkingFixer.Tracer.TraceDebug<Guid>((long)this.personId.GetHashCode(), "GALLinkingFixer::Unlinking contacts from GAL, ADobjectIdGuid: {0}.", this.adObjectIdGuid);
			bool hasSingleContact = this.contacts.Count == 1;
			return this.TryEnumerateContactsAndApplyUpdates(delegate(ContactInfoForLinking contactInfo)
			{
				contactInfo.ClearGALLink(GALLinkState.NotLinked);
				if (hasSingleContact)
				{
					contactInfo.Linked = false;
				}
			}, "UnlinkContactsFromGAL", new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactUnlinking>
			{
				{
					ContactLinkingLogSchema.ContactUnlinking.PersonId,
					this.personId
				},
				{
					ContactLinkingLogSchema.ContactUnlinking.ADObjectIdGuid,
					this.adObjectIdGuid
				}
			});
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000DDD98 File Offset: 0x000DBF98
		private bool TryEnumerateContactsAndApplyUpdates(Action<ContactInfoForLinking> updateOperation, string sessionName, ILogEvent operationIntentionEvent)
		{
			List<ContactInfoForLinkingFromPropertyBag> list = new List<ContactInfoForLinkingFromPropertyBag>(this.contacts.Count);
			bool flag = false;
			foreach (IStorePropertyBag propertyBag in this.contacts)
			{
				ContactInfoForLinkingWithPropertyBagUpdater contactInfoForLinkingWithPropertyBagUpdater = ContactInfoForLinkingWithPropertyBagUpdater.Create(this.mailboxSession, propertyBag, this.contactProperties);
				updateOperation(contactInfoForLinkingWithPropertyBagUpdater);
				list.Add(contactInfoForLinkingWithPropertyBagUpdater);
				if (contactInfoForLinkingWithPropertyBagUpdater.IsDirty)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				GALLinkingFixer.Tracer.TraceDebug((long)this.personId.GetHashCode(), "GALLinkingFixer::Contact properties are up to date, no need to update contact in store or in memory.");
				return true;
			}
			GALLinkingFixer.Tracer.TraceDebug((long)this.personId.GetHashCode(), "GALLinkingFixer::Contact properties are NOT up to date, update contact in store and in memory.");
			bool result;
			try
			{
				List<IStorePropertyBag> list2 = new List<IStorePropertyBag>(this.contacts.Count);
				MailboxInfoForLinking mailboxInfo = MailboxInfoForLinking.CreateFromMailboxSession(this.mailboxSession);
				ContactLinkingLogger contactLinkingLogger = new ContactLinkingLogger(sessionName, mailboxInfo);
				ContactLinkingPerformanceTracker contactLinkingPerformanceTracker = new ContactLinkingPerformanceTracker(this.mailboxSession);
				contactLinkingPerformanceTracker.Start();
				contactLinkingLogger.LogEvent(operationIntentionEvent);
				foreach (ContactInfoForLinkingFromPropertyBag contactInfoForLinkingFromPropertyBag in list)
				{
					ContactInfoForLinkingWithPropertyBagUpdater contactInfoForLinkingWithPropertyBagUpdater2 = (ContactInfoForLinkingWithPropertyBagUpdater)contactInfoForLinkingFromPropertyBag;
					contactInfoForLinkingWithPropertyBagUpdater2.Commit(contactLinkingLogger, contactLinkingPerformanceTracker);
					list2.Add(contactInfoForLinkingWithPropertyBagUpdater2.PropertyBag);
				}
				contactLinkingPerformanceTracker.Stop();
				contactLinkingLogger.LogEvent(contactLinkingPerformanceTracker.GetLogEvent());
				this.contacts.Clear();
				this.contacts.AddRange(list2);
				GALLinkingFixer.Tracer.TraceDebug((long)this.personId.GetHashCode(), "GALLinkingFixer::Contacts successfully updated in store and in memory.");
				result = true;
			}
			catch (StorageTransientException arg)
			{
				GALLinkingFixer.Tracer.TraceError<StorageTransientException>((long)this.personId.GetHashCode(), "Failed while fixing GAL Linking properties on the contact, exception: {0}.", arg);
				result = false;
			}
			catch (StoragePermanentException arg2)
			{
				GALLinkingFixer.Tracer.TraceError<StoragePermanentException>((long)this.personId.GetHashCode(), "Failed while fixing GAL Linking properties on the contact, exception: {0}.", arg2);
				result = false;
			}
			return result;
		}

		// Token: 0x04001D52 RID: 7506
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001D53 RID: 7507
		private readonly MailboxSession mailboxSession;

		// Token: 0x04001D54 RID: 7508
		private readonly PersonId personId;

		// Token: 0x04001D55 RID: 7509
		private readonly Guid adObjectIdGuid;

		// Token: 0x04001D56 RID: 7510
		private readonly List<IStorePropertyBag> contacts;

		// Token: 0x04001D57 RID: 7511
		private readonly ICollection<PropertyDefinition> contactProperties;
	}
}
