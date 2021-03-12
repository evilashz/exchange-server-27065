using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.BirthdayCalendar;
using Microsoft.Exchange.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.BirthdayCalendar.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.BirthdayAssistant
{
	// Token: 0x02000020 RID: 32
	internal sealed class BirthdayAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000059D6 File Offset: 0x00003BD6
		public BirthdayAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000059EC File Offset: 0x00003BEC
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return mapiEvent.ClientType != MapiEventClientTypes.EventBasedAssistants && BirthdayAssistant.IsAnInterestingContact(mapiEvent);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005A00 File Offset: 0x00003C00
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession mailboxSession, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "BirthdayAssistant::HandleEventInternal enter");
			if (!BirthdayCalendar.UserHasBirthdayCalendarFolder(mailboxSession))
			{
				return;
			}
			BirthdayAssistant.HandleContact(mapiEvent, mailboxSession, item, this.tracer);
			this.tracer.TraceDebug((long)this.GetHashCode(), "BirthdayAssistant::HandleEventInternal exit");
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005A52 File Offset: 0x00003C52
		private static bool IsAnInterestingContact(IMapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & (MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectDeleted | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved)) != (MapiEventTypeFlags)0 && ObjectClass.IsContact(mapiEvent.ObjectClass);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005A6C File Offset: 0x00003C6C
		private static void HandleContact(MapiEvent mapiEvent, MailboxSession mailboxSession, IStoreObject contactStoreObject, Trace tracer)
		{
			tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact enter");
			BirthdayAssistantBusinessLogic birthdayAssistantBusinessLogic = new BirthdayAssistantBusinessLogic();
			if (Globals.IsStoreObjectDeleted(mapiEvent, mailboxSession, contactStoreObject))
			{
				tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact: contact was deleted");
				StoreObjectId birthdayContactStoreObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.OldItemEntryId, StoreObjectType.Contact);
				birthdayAssistantBusinessLogic.OnContactDeleted(birthdayContactStoreObjectId, mailboxSession);
			}
			else if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) != (MapiEventTypeFlags)0)
			{
				tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact: contact was created");
				BirthdayContact birthdayContact = BirthdayContactTranslator.Instance.ConvertToEntity(contactStoreObject as IContact);
				tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact: converted contact to entity");
				birthdayAssistantBusinessLogic.OnContactAdded(birthdayContact, mailboxSession);
			}
			else if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectModified) != (MapiEventTypeFlags)0)
			{
				tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact: contact was modified");
				BirthdayContact birthdayContact2 = BirthdayContactTranslator.Instance.ConvertToEntity(contactStoreObject as IContact);
				birthdayAssistantBusinessLogic.OnContactModified(birthdayContact2, mailboxSession);
			}
			else
			{
				tracer.TraceDebug<MapiEvent>(0L, "BirthdayAssistant:HandleContact: no-op for MAPI event {0}", mapiEvent);
			}
			tracer.TraceDebug(0L, "BirthdayAssistant:HandleContact exit");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005B4E File Offset: 0x00003D4E
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005B56 File Offset: 0x00003D56
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005B5E File Offset: 0x00003D5E
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400010A RID: 266
		private const MapiEventTypeFlags ContactEventMask = MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectDeleted | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved;

		// Token: 0x0400010B RID: 267
		private readonly Trace tracer = ExTraceGlobals.BirthdayAssistantTracer;
	}
}
