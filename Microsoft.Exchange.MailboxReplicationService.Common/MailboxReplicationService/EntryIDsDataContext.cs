using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F7 RID: 247
	internal class EntryIDsDataContext : DataContext
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0001262B File Offset: 0x0001082B
		public EntryIDsDataContext(byte[][] entryIDs)
		{
			this.entryIDs = entryIDs;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001263A File Offset: 0x0001083A
		public EntryIDsDataContext(List<byte[]> entryIDs)
		{
			this.entryIDs = entryIDs.ToArray();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00012650 File Offset: 0x00010850
		public EntryIDsDataContext(byte[] entryID)
		{
			this.entryIDs = new byte[][]
			{
				entryID
			};
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00012675 File Offset: 0x00010875
		public override string ToString()
		{
			return string.Format("EntryIDs: {0}", CommonUtils.ConcatEntries<byte[]>(this.entryIDs, new Func<byte[], string>(TraceUtils.DumpEntryId)));
		}

		// Token: 0x0400055C RID: 1372
		private byte[][] entryIDs;
	}
}
