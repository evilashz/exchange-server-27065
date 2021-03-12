using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x02000007 RID: 7
	internal class DeleteBirthdayEventForContact : EntityCommand<IBirthdayEvents, IBirthdayEvent>, IBirthdayEventCommand
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022D2 File Offset: 0x000004D2
		internal DeleteBirthdayEventForContact(StoreObjectId contactStoreObjectId, IBirthdayEvents scope)
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "DeleteBirthdayEventForContact:Constructor/store object ID");
			this.ContactStoreObjectId = contactStoreObjectId;
			this.Scope = scope;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002300 File Offset: 0x00000500
		internal DeleteBirthdayEventForContact(IBirthdayContact contact, IBirthdayEvents scope)
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "DeleteBirthdayEventForContact:Constructor/contact");
			IBirthdayContactInternal birthdayContactInternal = contact as IBirthdayContactInternal;
			if (birthdayContactInternal == null)
			{
				throw new ArgumentException("Contact has to implement IBirthdayContactInternal", "contact");
			}
			this.ContactStoreObjectId = StoreId.GetStoreObjectId(birthdayContactInternal.StoreId);
			this.Scope = scope;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002364 File Offset: 0x00000564
		public StoreObjectId ContactStoreObjectId { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000236D File Offset: 0x0000056D
		protected override ITracer Trace
		{
			get
			{
				return DeleteBirthdayEventForContact.DeleteBirthdayEventTracer;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002374 File Offset: 0x00000574
		public BirthdayEventCommandResult ExecuteAndGetResult()
		{
			BirthdayEventCommandResult birthdayEventCommandResult = new BirthdayEventCommandResult();
			IBirthdayEvent birthdayEvent = base.Execute(null);
			if (birthdayEvent != null)
			{
				birthdayEventCommandResult.DeletedEvents.Add(birthdayEvent);
			}
			return birthdayEventCommandResult;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000239F File Offset: 0x0000059F
		protected override IBirthdayEvent OnExecute()
		{
			return this.DeleteExistingBirthdayEventForContact(this.ContactStoreObjectId);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023AD File Offset: 0x000005AD
		private IBirthdayEvent DeleteExistingBirthdayEventForContact(StoreObjectId birthdayContactStoreObjectId)
		{
			return this.Scope.BirthdayEventDataProvider.DeleteBirthdayEventForContact(birthdayContactStoreObjectId);
		}

		// Token: 0x04000004 RID: 4
		private static readonly ITracer DeleteBirthdayEventTracer = ExTraceGlobals.DeleteBirthdayEventForContactTracer;
	}
}
