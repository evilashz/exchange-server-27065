using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands;

namespace Microsoft.Exchange.Entities.BirthdayCalendar
{
	// Token: 0x02000002 RID: 2
	internal sealed class BirthdayAssistantBusinessLogic
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BirthdayEventCommandResult OnContactAdded(IBirthdayContact birthdayContact, IStoreSession storeSession)
		{
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug((long)this.GetHashCode(), "OnContactAdded");
			BirthdayEventCommandResult birthdayEventCommandResult = this.OnContactAdded(birthdayContact, new BirthdaysContainer(storeSession, null));
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug<BirthdayEventCommandResult>((long)this.GetHashCode(), "OnContactAdded: birthday event is <{0}>", birthdayEventCommandResult);
			return birthdayEventCommandResult;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000211C File Offset: 0x0000031C
		public BirthdayEventCommandResult OnContactDeleted(StoreObjectId birthdayContactStoreObjectId, IStoreSession storeSession)
		{
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug((long)this.GetHashCode(), "OnContactDeleted");
			BirthdayEventCommandResult birthdayEventCommandResult = this.OnContactDeleted(birthdayContactStoreObjectId, new BirthdaysContainer(storeSession, null));
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug<BirthdayEventCommandResult>((long)this.GetHashCode(), "OnContactDeleted: birthday event was <{0}>", birthdayEventCommandResult);
			return birthdayEventCommandResult;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002168 File Offset: 0x00000368
		public BirthdayEventCommandResult OnContactModified(IBirthdayContact birthdayContact, IStoreSession storeSession)
		{
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug((long)this.GetHashCode(), "OnContactModified: started");
			BirthdaysContainer birthdaysContainer = new BirthdaysContainer(storeSession, null);
			BirthdayEventCommandResult result = this.OnContactModified(birthdayContact, birthdaysContainer);
			ExTraceGlobals.BirthdayAssistantBusinessLogicTracer.TraceDebug((long)this.GetHashCode(), "OnContactModified: finished");
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B3 File Offset: 0x000003B3
		internal BirthdayEventCommandResult OnContactAdded(IBirthdayContact birthdayContact, IBirthdaysContainer birthdaysContainer)
		{
			return birthdaysContainer.Events.CreateBirthdayEventForContact(birthdayContact);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021C1 File Offset: 0x000003C1
		internal BirthdayEventCommandResult OnContactDeleted(StoreObjectId birthdayContactStoreObjectId, IBirthdaysContainer birthdaysContainer)
		{
			return birthdaysContainer.Events.DeleteBirthdayEventForContactId(birthdayContactStoreObjectId);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D0 File Offset: 0x000003D0
		internal BirthdayEventCommandResult OnContactModified(IBirthdayContact birthdayContact, IBirthdaysContainer birthdaysContainer)
		{
			IBirthdayContactInternal birthdayContactInternal = birthdayContact as IBirthdayContactInternal;
			if (birthdayContactInternal == null)
			{
				throw new ArgumentException("Argument must implement IBirthdayContactInternal", "birthdayContact");
			}
			IEnumerable<IBirthdayContact> linkedContacts = birthdaysContainer.Contacts.GetLinkedContacts(birthdayContactInternal.PersonId);
			return birthdaysContainer.Events.UpdateBirthdaysForLinkedContacts(linkedContacts);
		}
	}
}
