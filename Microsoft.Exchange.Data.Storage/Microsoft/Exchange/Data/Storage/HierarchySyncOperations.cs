using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E21 RID: 3617
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HierarchySyncOperations : IEnumerable<HierarchySyncOperation>, IEnumerable
	{
		// Token: 0x06007D16 RID: 32022 RVA: 0x0022922C File Offset: 0x0022742C
		internal HierarchySyncOperations(FolderHierarchySync folderHierarchySync, IDictionary changes, bool moreAvailable)
		{
			this.moreAvailable = moreAvailable;
			this.changes = new List<HierarchySyncOperation>(changes.Keys.Count);
			foreach (object obj in changes.Values)
			{
				FolderManifestEntry manifestEntry = (FolderManifestEntry)obj;
				HierarchySyncOperation hierarchySyncOperation = new HierarchySyncOperation();
				hierarchySyncOperation.Bind(folderHierarchySync, manifestEntry);
				this.changes.Add(hierarchySyncOperation);
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06007D17 RID: 32023 RVA: 0x002292BC File Offset: 0x002274BC
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06007D18 RID: 32024 RVA: 0x002292C4 File Offset: 0x002274C4
		public int Count
		{
			get
			{
				return this.changes.Count;
			}
		}

		// Token: 0x17002179 RID: 8569
		public HierarchySyncOperation this[int idx]
		{
			get
			{
				return this.changes[idx];
			}
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x002292DF File Offset: 0x002274DF
		IEnumerator<HierarchySyncOperation> IEnumerable<HierarchySyncOperation>.GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x002292F1 File Offset: 0x002274F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.changes.GetEnumerator();
		}

		// Token: 0x04005577 RID: 21879
		private List<HierarchySyncOperation> changes;

		// Token: 0x04005578 RID: 21880
		private bool moreAvailable;
	}
}
