using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x02000009 RID: 9
	internal class UpdateBirthdayEventForContact : EntityCommand<IBirthdayEvents, IBirthdayEvent>, IBirthdayEventCommand
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002463 File Offset: 0x00000663
		internal UpdateBirthdayEventForContact(IBirthdayEvent birthdayEvent, IBirthdayContact birthdayContact, IBirthdayEvents scope)
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "UpdateBirthdayEventForContact:Constructor");
			this.BirthdayContact = birthdayContact;
			this.BirthdayEvent = birthdayEvent;
			this.Scope = scope;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000249F File Offset: 0x0000069F
		public IBirthdayEvent BirthdayEvent { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000024B0 File Offset: 0x000006B0
		public IBirthdayContact BirthdayContact { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024B9 File Offset: 0x000006B9
		protected override ITracer Trace
		{
			get
			{
				return UpdateBirthdayEventForContact.UpdateBirthdayEventsForContactTracer;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000024C8 File Offset: 0x000006C8
		private BirthdayEventCommandResult Result { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x000024D1 File Offset: 0x000006D1
		public BirthdayEventCommandResult ExecuteAndGetResult()
		{
			base.Execute(null);
			return this.Result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024E4 File Offset: 0x000006E4
		protected override IBirthdayEvent OnExecute()
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "Updating birthday for contact");
			this.Result = new BirthdayEventCommandResult();
			IBirthdayEventInternal birthdayEventInternal = this.BirthdayEvent as IBirthdayEventInternal;
			if (birthdayEventInternal == null)
			{
				throw new ArgumentException("Was not expecting a null internal birthday event");
			}
			IBirthdayContactInternal birthdayContactInternal = this.BirthdayContact as IBirthdayContactInternal;
			if (birthdayContactInternal == null)
			{
				throw new ArgumentException("Was not expecting a null internal birthday contact");
			}
			if (UpdateBirthdayEventForContact.ShouldEventBeUpdated(birthdayEventInternal, birthdayContactInternal))
			{
				this.Scope.BirthdayEventDataProvider.Delete(birthdayEventInternal.StoreId, DeleteItemFlags.HardDelete);
				this.Result.DeletedEvents.Add(this.BirthdayEvent);
				this.Result.MergeWith(this.Scope.CreateBirthdayEventForContact(this.BirthdayContact));
			}
			PersonId personId = birthdayEventInternal.PersonId;
			if (personId != null && !personId.Equals(birthdayContactInternal.PersonId))
			{
				IEnumerable<IBirthdayContact> linkedContacts = this.Scope.ParentScope.ParentScope.Contacts.GetLinkedContacts(personId);
				this.Result.MergeWith(this.Scope.UpdateBirthdaysForLinkedContacts(linkedContacts));
			}
			return this.Result.CreatedEvents.FirstOrDefault<IBirthdayEvent>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025F4 File Offset: 0x000007F4
		private static bool ShouldEventBeUpdated(IBirthdayEventInternal birthdayEvent, IBirthdayContactInternal birthdayContact)
		{
			if (birthdayEvent == null)
			{
				throw new ArgumentNullException("birthdayEvent");
			}
			if (birthdayContact == null)
			{
				throw new ArgumentNullException("birthdayContact");
			}
			if (!birthdayEvent.ContactId.Equals(StoreId.GetStoreObjectId(birthdayContact.StoreId)))
			{
				throw new ArgumentException("Birthday event and birthday contact should have the same contact IDs", "birthdayEvent");
			}
			bool flag = birthdayEvent.Subject != birthdayContact.DisplayName;
			bool flag2 = birthdayEvent.Birthday != birthdayContact.Birthday;
			bool flag3 = !birthdayEvent.PersonId.Equals(birthdayContact.PersonId);
			bool flag4 = birthdayEvent.Attribution != birthdayContact.Attribution;
			UpdateBirthdayEventForContact.UpdateBirthdayEventsForContactTracer.TraceDebug(0L, "Differences: subject - {0}, birthday - {1}, person ID - {2}, attribution - {3}", new object[]
			{
				flag,
				flag2,
				flag3,
				flag4
			});
			return flag || flag2 || flag3 || flag4;
		}

		// Token: 0x04000008 RID: 8
		private static readonly ITracer UpdateBirthdayEventsForContactTracer = ExTraceGlobals.UpdateBirthdayEventForContactTracer;
	}
}
