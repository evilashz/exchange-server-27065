using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020009AC RID: 2476
	public sealed class MailboxStoreTypeProvider : IConfigDataProvider
	{
		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x0017D7B4 File Offset: 0x0017B9B4
		public ADUser ADUser
		{
			get
			{
				return this.adUser;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x0017D7BC File Offset: 0x0017B9BC
		// (set) Token: 0x06005B53 RID: 23379 RVA: 0x0017D7C4 File Offset: 0x0017B9C4
		internal MailboxSession MailboxSession { get; set; }

		// Token: 0x06005B54 RID: 23380 RVA: 0x0017D7CD File Offset: 0x0017B9CD
		public MailboxStoreTypeProvider(ADUser adUser)
		{
			this.adUser = adUser;
			this.source = string.Empty;
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x0017D7E8 File Offset: 0x0017B9E8
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			IMailboxStoreType mailboxStoreType = t as IMailboxStoreType;
			return mailboxStoreType.Read(this, identity);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x0017D82C File Offset: 0x0017BA2C
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			IMailboxStoreType mailboxStoreType = t as IMailboxStoreType;
			return mailboxStoreType.Find(this, filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x0017D874 File Offset: 0x0017BA74
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			IMailboxStoreType mailboxStoreType = t as IMailboxStoreType;
			return mailboxStoreType.FindPaged<T>(this, filter, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x0017D8BC File Offset: 0x0017BABC
		public void Save(IConfigurable instance)
		{
			IMailboxStoreType mailboxStoreType = instance as IMailboxStoreType;
			mailboxStoreType.Save(this);
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0017D8D8 File Offset: 0x0017BAD8
		public void Delete(IConfigurable instance)
		{
			IMailboxStoreType mailboxStoreType = instance as IMailboxStoreType;
			mailboxStoreType.Delete(this);
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0017D8F3 File Offset: 0x0017BAF3
		public string Source
		{
			get
			{
				return this.source ?? string.Empty;
			}
		}

		// Token: 0x04003261 RID: 12897
		private string source;

		// Token: 0x04003262 RID: 12898
		private ADUser adUser;
	}
}
