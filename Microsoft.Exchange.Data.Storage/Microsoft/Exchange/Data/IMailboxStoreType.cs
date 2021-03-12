using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020009AF RID: 2479
	public interface IMailboxStoreType : IConfigurable
	{
		// Token: 0x06005B6D RID: 23405
		void Save(MailboxStoreTypeProvider session);

		// Token: 0x06005B6E RID: 23406
		void Delete(MailboxStoreTypeProvider session);

		// Token: 0x06005B6F RID: 23407
		IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity);

		// Token: 0x06005B70 RID: 23408
		IConfigurable[] Find(MailboxStoreTypeProvider session, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy);

		// Token: 0x06005B71 RID: 23409
		IEnumerable<T> FindPaged<T>(MailboxStoreTypeProvider session, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize);
	}
}
