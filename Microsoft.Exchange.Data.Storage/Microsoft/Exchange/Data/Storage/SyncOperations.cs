using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E2B RID: 3627
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncOperations : IEnumerable<SyncOperation>, IEnumerable
	{
		// Token: 0x06007D85 RID: 32133 RVA: 0x00229CA8 File Offset: 0x00227EA8
		public SyncOperations(FolderSync folderSync, IDictionary changes, bool moreAvailable)
		{
			this.moreAvailable = moreAvailable;
			this.changes = new List<SyncOperation>(changes.Keys.Count);
			foreach (object obj in changes.Values)
			{
				ServerManifestEntry serverManifestEntry = (ServerManifestEntry)obj;
				if (serverManifestEntry.ChangeType != ChangeType.OutOfFilter)
				{
					SyncOperation syncOperation = new SyncOperation();
					syncOperation.Bind(folderSync, serverManifestEntry, false);
					this.changes.Add(syncOperation);
				}
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06007D86 RID: 32134 RVA: 0x00229D44 File Offset: 0x00227F44
		public int Count
		{
			get
			{
				return this.changes.Count;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06007D87 RID: 32135 RVA: 0x00229D51 File Offset: 0x00227F51
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
		}

		// Token: 0x17002191 RID: 8593
		public SyncOperation this[int idx]
		{
			get
			{
				return this.changes[idx];
			}
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x00229D67 File Offset: 0x00227F67
		public void RemoveAt(int index)
		{
			this.changes.RemoveAt(index);
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x00229D75 File Offset: 0x00227F75
		IEnumerator<SyncOperation> IEnumerable<SyncOperation>.GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x00229D87 File Offset: 0x00227F87
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x04005591 RID: 21905
		private List<SyncOperation> changes;

		// Token: 0x04005592 RID: 21906
		private bool moreAvailable;
	}
}
