using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E20 RID: 3616
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HierarchySyncOperation
	{
		// Token: 0x06007D08 RID: 32008 RVA: 0x00229154 File Offset: 0x00227354
		internal HierarchySyncOperation()
		{
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06007D09 RID: 32009 RVA: 0x0022915C File Offset: 0x0022735C
		// (set) Token: 0x06007D0A RID: 32010 RVA: 0x00229169 File Offset: 0x00227369
		public ChangeType ChangeType
		{
			get
			{
				return this.manifestEntry.ChangeType;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ChangeType>(value, "value");
				this.manifestEntry.ChangeType = value;
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06007D0B RID: 32011 RVA: 0x00229182 File Offset: 0x00227382
		public StoreObjectId ItemId
		{
			get
			{
				return this.manifestEntry.ItemId;
			}
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06007D0C RID: 32012 RVA: 0x0022918F File Offset: 0x0022738F
		public StoreObjectId ParentId
		{
			get
			{
				return this.manifestEntry.ParentId;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06007D0D RID: 32013 RVA: 0x0022919C File Offset: 0x0022739C
		public bool IsSharedFolder
		{
			get
			{
				return !string.IsNullOrEmpty(this.manifestEntry.Owner);
			}
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06007D0E RID: 32014 RVA: 0x002291B1 File Offset: 0x002273B1
		public SyncPermissions Permissions
		{
			get
			{
				return this.manifestEntry.Permissions;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06007D0F RID: 32015 RVA: 0x002291BE File Offset: 0x002273BE
		public string Owner
		{
			get
			{
				return this.manifestEntry.Owner;
			}
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06007D10 RID: 32016 RVA: 0x002291CB File Offset: 0x002273CB
		public bool Hidden
		{
			get
			{
				return this.manifestEntry.Hidden;
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x002291D8 File Offset: 0x002273D8
		public string DisplayName
		{
			get
			{
				return this.manifestEntry.DisplayName;
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06007D12 RID: 32018 RVA: 0x002291E5 File Offset: 0x002273E5
		public string ClassName
		{
			get
			{
				return this.manifestEntry.ClassName;
			}
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x002291F2 File Offset: 0x002273F2
		public Folder GetFolder(params PropertyDefinition[] prefetchProperties)
		{
			return this.folderHierarchySync.GetFolder(this.manifestEntry, prefetchProperties);
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x00229206 File Offset: 0x00227406
		public Folder GetFolder()
		{
			return this.folderHierarchySync.GetFolder(this.manifestEntry, null);
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x0022921A File Offset: 0x0022741A
		internal void Bind(FolderHierarchySync folderHierarchySync, FolderManifestEntry manifestEntry)
		{
			this.manifestEntry = manifestEntry;
			this.folderHierarchySync = folderHierarchySync;
		}

		// Token: 0x04005575 RID: 21877
		private FolderHierarchySync folderHierarchySync;

		// Token: 0x04005576 RID: 21878
		private FolderManifestEntry manifestEntry;
	}
}
