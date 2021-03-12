using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x0200000A RID: 10
	internal class UpdateBirthdaysForLinkedContacts : EntityCommand<IBirthdayEvents, IBirthdayEvent>, IBirthdayEventCommand
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002704 File Offset: 0x00000904
		internal UpdateBirthdaysForLinkedContacts(IEnumerable<IBirthdayContact> linkedContacts, IBirthdayEvents scope)
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "UpdateBirthdaysForLinkedContacts:Constructor");
			this.LinkedContacts = linkedContacts;
			this.Scope = scope;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002731 File Offset: 0x00000931
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002739 File Offset: 0x00000939
		public IEnumerable<IBirthdayContact> LinkedContacts { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002742 File Offset: 0x00000942
		protected override ITracer Trace
		{
			get
			{
				return UpdateBirthdaysForLinkedContacts.UpdateBirthdaysForLinkedContactsTracer;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002751 File Offset: 0x00000951
		private BirthdayEventCommandResult Result { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000275A File Offset: 0x0000095A
		public BirthdayEventCommandResult ExecuteAndGetResult()
		{
			base.Execute(null);
			return this.Result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002774 File Offset: 0x00000974
		protected override IBirthdayEvent OnExecute()
		{
			this.Trace.TraceDebug((long)this.GetHashCode(), "Updating birthdays for linked contacts");
			this.Result = new BirthdayEventCommandResult();
			IEnumerable<IGrouping<ExDateTime?, IBirthdayContact>> enumerable = from eachContact in this.LinkedContacts
			group eachContact by eachContact.Birthday;
			foreach (IGrouping<ExDateTime?, IBirthdayContact> grouping in enumerable)
			{
				bool flag = false;
				foreach (IBirthdayContact birthdayContact in grouping)
				{
					if (birthdayContact.Birthday != null && !birthdayContact.ShouldHideBirthday && !flag)
					{
						flag = true;
						IBirthdayEvent birthdayEvent = this.FindBirthdayEventForContact(birthdayContact);
						this.Result.MergeWith((birthdayEvent == null) ? this.Scope.CreateBirthdayEventForContact(birthdayContact) : this.Scope.UpdateBirthdayEventForContact(birthdayEvent, birthdayContact));
					}
					else
					{
						this.Result.MergeWith(this.Scope.DeleteBirthdayEventForContact(birthdayContact));
					}
				}
			}
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028B0 File Offset: 0x00000AB0
		private IBirthdayEvent FindBirthdayEventForContact(IBirthdayContact contact)
		{
			IEnumerable<BirthdayEvent> enumerable = this.Scope.BirthdayEventDataProvider.FindBirthdayEventsForContact(contact);
			IBirthdayEvent result = null;
			if (enumerable != null)
			{
				BirthdayEvent[] array = (enumerable as BirthdayEvent[]) ?? enumerable.ToArray<BirthdayEvent>();
				if (array.Length > 1)
				{
					this.Trace.TraceError((long)this.GetHashCode(), "Found multiple birthday events for a single contact.");
					this.Scope.BirthdayEventDataProvider.DeleteBirthdayEvents(array);
				}
				else
				{
					result = array.FirstOrDefault<BirthdayEvent>();
				}
			}
			return result;
		}

		// Token: 0x0400000C RID: 12
		private static readonly ITracer UpdateBirthdaysForLinkedContactsTracer = ExTraceGlobals.UpdateBirthdaysForLinkedContactsTracer;
	}
}
