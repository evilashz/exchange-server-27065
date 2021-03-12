using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200096F RID: 2415
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FolderChange : ChangedItem
	{
		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x00172C36 File Offset: 0x00170E36
		public Dictionary<Guid, FileChange> FileChanges
		{
			get
			{
				return this.fileChanges;
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x00172C3E File Offset: 0x00170E3E
		public bool HasFileChangesOnly
		{
			get
			{
				return base.Id == Guid.Empty;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x00172C50 File Offset: 0x00170E50
		// (set) Token: 0x06005994 RID: 22932 RVA: 0x00172C58 File Offset: 0x00170E58
		public IEnumerator<FileChange> FileChangesEnumerator { get; private set; }

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x00172C61 File Offset: 0x00170E61
		// (set) Token: 0x06005996 RID: 22934 RVA: 0x00172C69 File Offset: 0x00170E69
		public StoreObjectId FolderId { get; set; }

		// Token: 0x06005997 RID: 22935 RVA: 0x00172C72 File Offset: 0x00170E72
		public FolderChange(Uri authority, Guid id, string version, string relativePath, string leafNode, ExDateTime whenCreated, ExDateTime lastModified) : base(authority, id, version, relativePath, leafNode, whenCreated, lastModified)
		{
			if (!this.HasFileChangesOnly && string.IsNullOrEmpty(leafNode))
			{
				throw new ArgumentException("LeafNode can't be null when there is a new folder change");
			}
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x00172CAC File Offset: 0x00170EAC
		public void Reset()
		{
			this.FolderId = null;
			this.FileChangesEnumerator = this.fileChanges.Values.GetEnumerator();
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00172CD0 File Offset: 0x00170ED0
		public FolderChange Clone()
		{
			FolderChange folderChange = new FolderChange(base.Authority, base.Id, base.Version, base.RelativePath, base.LeafNode, base.WhenCreated, base.LastModified);
			foreach (KeyValuePair<Guid, FileChange> keyValuePair in this.FileChanges)
			{
				folderChange.FileChanges.Add(keyValuePair.Key, keyValuePair.Value);
			}
			folderChange.Reset();
			return folderChange;
		}

		// Token: 0x04003137 RID: 12599
		private readonly Dictionary<Guid, FileChange> fileChanges = new Dictionary<Guid, FileChange>();
	}
}
