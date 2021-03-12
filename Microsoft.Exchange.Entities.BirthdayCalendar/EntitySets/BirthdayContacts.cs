using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.DataProviders;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x02000004 RID: 4
	internal class BirthdayContacts : StorageEntitySet<IBirthdayContacts, IBirthdayContact, IStoreSession>, IBirthdayContacts, IEntitySet<IBirthdayContact>, IStorageEntitySetScope<IStoreSession>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000221D File Offset: 0x0000041D
		public BirthdayContacts(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope, "BirthdayContacts", new SimpleCrudNotSupportedCommandFactory<IBirthdayContacts, IBirthdayContact>())
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002230 File Offset: 0x00000430
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002257 File Offset: 0x00000457
		public BirthdayContactDataProvider BirthdayContactDataProvider
		{
			get
			{
				BirthdayContactDataProvider result;
				if ((result = this.contactDataProvider) == null)
				{
					result = (this.contactDataProvider = new BirthdayContactDataProvider(this, null));
				}
				return result;
			}
			set
			{
				this.contactDataProvider = value;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002260 File Offset: 0x00000460
		public IEnumerable<IBirthdayContact> GetLinkedContacts(PersonId personId)
		{
			return this.BirthdayContactDataProvider.GetLinkedContacts(personId);
		}

		// Token: 0x04000001 RID: 1
		private BirthdayContactDataProvider contactDataProvider;
	}
}
