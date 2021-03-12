using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.DataProviders;
using Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x0200000C RID: 12
	internal class BirthdayEvents : StorageEntitySet<IBirthdayEvents, IBirthdayEvent, IStoreSession>, IBirthdayEvents, IEntitySet<IBirthdayEvent>, IStorageEntitySetScope<IStoreSession>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000292A File Offset: 0x00000B2A
		public BirthdayEvents(IBirthdayCalendars parentScope) : base(parentScope, "BirthdayEvents", new SimpleCrudNotSupportedCommandFactory<IBirthdayEvents, IBirthdayEvent>())
		{
			this.ParentScope = parentScope;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002944 File Offset: 0x00000B44
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002975 File Offset: 0x00000B75
		public BirthdayEventDataProvider BirthdayEventDataProvider
		{
			get
			{
				BirthdayEventDataProvider result;
				if ((result = this.eventDataProvider) == null)
				{
					result = (this.eventDataProvider = new BirthdayEventDataProvider(this, this.ParentScope.BirthdayCalendarFolderId));
				}
				return result;
			}
			set
			{
				this.eventDataProvider = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000297E File Offset: 0x00000B7E
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002986 File Offset: 0x00000B86
		public IBirthdayCalendars ParentScope { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x0000298F File Offset: 0x00000B8F
		public IEnumerable<BirthdayEvent> GetBirthdayCalendarView(ExDateTime rangeStart, ExDateTime rangeEnd)
		{
			return new GetBirthdayCalendarView(this, rangeStart, rangeEnd).Execute(null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000299F File Offset: 0x00000B9F
		public BirthdayEventCommandResult CreateBirthdayEventForContact(IBirthdayContact contact)
		{
			return new CreateBirthdayEventForContact(contact, this).ExecuteAndGetResult();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029AD File Offset: 0x00000BAD
		public BirthdayEventCommandResult DeleteBirthdayEventForContactId(StoreObjectId birthdayContactStoreObjectId)
		{
			return new DeleteBirthdayEventForContact(birthdayContactStoreObjectId, this).ExecuteAndGetResult();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029BB File Offset: 0x00000BBB
		public BirthdayEventCommandResult DeleteBirthdayEventForContact(IBirthdayContact birthdayContact)
		{
			return new DeleteBirthdayEventForContact(birthdayContact, this).ExecuteAndGetResult();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029C9 File Offset: 0x00000BC9
		public BirthdayEventCommandResult UpdateBirthdayEventForContact(IBirthdayEvent birthdayEvent, IBirthdayContact birthdayContact)
		{
			return new UpdateBirthdayEventForContact(birthdayEvent, birthdayContact, this).ExecuteAndGetResult();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029D8 File Offset: 0x00000BD8
		public BirthdayEventCommandResult UpdateBirthdaysForLinkedContacts(IEnumerable<IBirthdayContact> linkedContacts)
		{
			return new UpdateBirthdaysForLinkedContacts(linkedContacts, this).ExecuteAndGetResult();
		}

		// Token: 0x04000010 RID: 16
		private BirthdayEventDataProvider eventDataProvider;
	}
}
