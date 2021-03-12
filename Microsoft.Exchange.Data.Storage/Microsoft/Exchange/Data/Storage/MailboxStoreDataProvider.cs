using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020009AD RID: 2477
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxStoreDataProvider : IConfigDataProvider
	{
		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06005B5B RID: 23387 RVA: 0x0017D904 File Offset: 0x0017BB04
		public ADUser ADUser
		{
			get
			{
				return this.adUser;
			}
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x0017D90C File Offset: 0x0017BB0C
		public MailboxStoreDataProvider(ADUser adUser)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			this.adUser = adUser;
		}

		// Token: 0x06005B5D RID: 23389
		public abstract IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new();

		// Token: 0x06005B5E RID: 23390
		public abstract IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new();

		// Token: 0x06005B5F RID: 23391
		public abstract IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new();

		// Token: 0x06005B60 RID: 23392
		public abstract void Save(IConfigurable instance);

		// Token: 0x06005B61 RID: 23393
		public abstract void Delete(IConfigurable instance);

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0017D929 File Offset: 0x0017BB29
		public virtual string Source
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04003264 RID: 12900
		private ADUser adUser;
	}
}
