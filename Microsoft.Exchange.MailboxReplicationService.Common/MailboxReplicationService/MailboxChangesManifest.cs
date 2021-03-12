using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000036 RID: 54
	[DataContract]
	internal sealed class MailboxChangesManifest
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000044A9 File Offset: 0x000026A9
		public MailboxChangesManifest()
		{
			this.changedFolders = null;
			this.deletedFolders = null;
			this.HasMoreHierarchyChanges = false;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000044C6 File Offset: 0x000026C6
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000044CE File Offset: 0x000026CE
		[DataMember(Name = "changedFolders", EmitDefaultValue = false)]
		public List<byte[]> ChangedFolders
		{
			get
			{
				return this.changedFolders;
			}
			set
			{
				this.changedFolders = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000044D7 File Offset: 0x000026D7
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000044DF File Offset: 0x000026DF
		[DataMember(Name = "deletedFolders", EmitDefaultValue = false)]
		public List<byte[]> DeletedFolders
		{
			get
			{
				return this.deletedFolders;
			}
			set
			{
				this.deletedFolders = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000044E8 File Offset: 0x000026E8
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000044F0 File Offset: 0x000026F0
		[DataMember(EmitDefaultValue = false, Name = "FolderChanges")]
		public ICollection<FolderChangesManifest> FolderChangesList
		{
			get
			{
				return new FolderChangesManifest[0];
			}
			set
			{
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000044F2 File Offset: 0x000026F2
		// (set) Token: 0x06000267 RID: 615 RVA: 0x000044FA File Offset: 0x000026FA
		[DataMember(Name = "hasMoreHierarchyChanges", EmitDefaultValue = false)]
		public bool HasMoreHierarchyChanges { get; set; }

		// Token: 0x06000268 RID: 616 RVA: 0x00004504 File Offset: 0x00002704
		public override string ToString()
		{
			return string.Format("{0} changed; {1} deleted", (this.changedFolders == null) ? 0 : this.changedFolders.Count, (this.deletedFolders == null) ? 0 : this.deletedFolders.Count);
		}

		// Token: 0x040001B6 RID: 438
		private List<byte[]> changedFolders;

		// Token: 0x040001B7 RID: 439
		private List<byte[]> deletedFolders;
	}
}
