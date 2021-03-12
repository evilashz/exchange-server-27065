using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000033 RID: 51
	public sealed class MailboxMapiSyncState : XMLSerializableBase
	{
		// Token: 0x0600028F RID: 655 RVA: 0x000106F9 File Offset: 0x0000E8F9
		public MailboxMapiSyncState()
		{
			this.folderSnapshots = new EntryIdMap<FolderStateSnapshot>();
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0001070C File Offset: 0x0000E90C
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00010740 File Offset: 0x0000E940
		[XmlArrayItem("FolderSnapshot")]
		[XmlArray("FolderSnapshots")]
		public FolderStateSnapshot[] FolderSnapshots
		{
			get
			{
				FolderStateSnapshot[] array = new FolderStateSnapshot[this.folderSnapshots.Count];
				this.folderSnapshots.Values.CopyTo(array, 0);
				return array;
			}
			set
			{
				this.folderSnapshots.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						FolderStateSnapshot folderStateSnapshot = value[i];
						this.folderSnapshots[folderStateSnapshot.FolderId ?? MailboxMapiSyncState.NullFolderKey] = folderStateSnapshot;
					}
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		[XmlIgnore]
		public FolderStateSnapshot this[byte[] folderId]
		{
			get
			{
				byte[] key = folderId ?? MailboxMapiSyncState.NullFolderKey;
				FolderStateSnapshot folderStateSnapshot;
				if (!this.folderSnapshots.TryGetValue(key, out folderStateSnapshot))
				{
					folderStateSnapshot = new FolderStateSnapshot();
					folderStateSnapshot.FolderId = folderId;
					this.folderSnapshots[key] = folderStateSnapshot;
				}
				return folderStateSnapshot;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000107CF File Offset: 0x0000E9CF
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000107D7 File Offset: 0x0000E9D7
		[XmlElement(ElementName = "ProviderState")]
		public string ProviderState { get; set; }

		// Token: 0x06000295 RID: 661 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public static MailboxMapiSyncState Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<MailboxMapiSyncState>(data, true);
		}

		// Token: 0x0400011E RID: 286
		private static readonly byte[] NullFolderKey = Array<byte>.Empty;

		// Token: 0x0400011F RID: 287
		private EntryIdMap<FolderStateSnapshot> folderSnapshots;
	}
}
