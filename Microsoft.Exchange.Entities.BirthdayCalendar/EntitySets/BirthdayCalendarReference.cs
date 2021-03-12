using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x02000016 RID: 22
	internal class BirthdayCalendarReference : StorageEntityReference<IBirthdayCalendars, IBirthdayCalendar, IMailboxSession>, IBirthdayCalendarReference, IEntityReference<IBirthdayCalendar>
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000360C File Offset: 0x0000180C
		public BirthdayCalendarReference(IBirthdayCalendars entitySet) : base(entitySet)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003615 File Offset: 0x00001815
		public BirthdayCalendarReference(IBirthdayCalendars entitySet, string entityKey) : base(entitySet, entityKey)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000361F File Offset: 0x0000181F
		public BirthdayCalendarReference(IBirthdayCalendars entitySet, StoreId entityStoreId) : base(entitySet, entityStoreId)
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003629 File Offset: 0x00001829
		public IBirthdayEvents Events
		{
			get
			{
				return new BirthdayEvents(base.EntitySet);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003636 File Offset: 0x00001836
		protected override StoreId ResolveReference()
		{
			ExTraceGlobals.BirthdayCalendarReferenceTracer.TraceDebug<Guid>((long)this.GetHashCode(), "BirthdayCalendarReference::ResolveReference. MailboxGuid:{0}", base.StoreSession.MailboxGuid);
			return base.EntitySet.BirthdayCalendarFolderId;
		}
	}
}
