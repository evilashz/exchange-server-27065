using System;
using System.Collections;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200009D RID: 157
	internal interface IIdMapping
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060008CA RID: 2250
		bool IsDirty { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060008CB RID: 2251
		IDictionaryEnumerator MailboxIdIdEnumerator { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060008CC RID: 2252
		IDictionaryEnumerator SyncIdIdEnumerator { get; }

		// Token: 0x17000357 RID: 855
		ISyncItemId this[string syncId]
		{
			get;
		}

		// Token: 0x17000358 RID: 856
		string this[ISyncItemId mailboxId]
		{
			get;
		}

		// Token: 0x060008CF RID: 2255
		bool Contains(ISyncItemId mailboxId);

		// Token: 0x060008D0 RID: 2256
		bool Contains(string syncId);

		// Token: 0x060008D1 RID: 2257
		void Delete(params ISyncItemId[] mailboxIds);

		// Token: 0x060008D2 RID: 2258
		void Delete(params string[] syncIds);
	}
}
