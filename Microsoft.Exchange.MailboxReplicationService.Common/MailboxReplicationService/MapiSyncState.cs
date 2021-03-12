using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000294 RID: 660
	public sealed class MapiSyncState : XMLSerializableBase
	{
		// Token: 0x0600201C RID: 8220 RVA: 0x00044868 File Offset: 0x00042A68
		public MapiSyncState()
		{
			this.hierData = new SyncHierarchyManifestState();
			this.folderData = new EntryIdMap<SyncContentsManifestState>();
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00044888 File Offset: 0x00042A88
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000448BC File Offset: 0x00042ABC
		[XmlElement]
		public SyncContentsManifestState[] FolderData
		{
			get
			{
				SyncContentsManifestState[] array = new SyncContentsManifestState[this.folderData.Count];
				this.folderData.Values.CopyTo(array, 0);
				return array;
			}
			set
			{
				this.folderData.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						SyncContentsManifestState syncContentsManifestState = value[i];
						this.folderData[syncContentsManifestState.FolderId] = syncContentsManifestState;
					}
				}
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000448FD File Offset: 0x00042AFD
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x00044905 File Offset: 0x00042B05
		[XmlElement]
		public SyncHierarchyManifestState HierarchyData
		{
			get
			{
				return this.hierData;
			}
			set
			{
				this.hierData = value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		[XmlIgnore]
		public SyncContentsManifestState this[byte[] folderKey]
		{
			get
			{
				SyncContentsManifestState syncContentsManifestState;
				if (!this.folderData.TryGetValue(folderKey, out syncContentsManifestState))
				{
					syncContentsManifestState = new SyncContentsManifestState();
					syncContentsManifestState.FolderId = folderKey;
					this.folderData.Add(folderKey, syncContentsManifestState);
				}
				return syncContentsManifestState;
			}
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00044948 File Offset: 0x00042B48
		public void RemoveContentsManifestState(byte[] folderId)
		{
			this.folderData.Remove(folderId);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00044957 File Offset: 0x00042B57
		internal static MapiSyncState Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<MapiSyncState>(data, false);
		}

		// Token: 0x04000D06 RID: 3334
		private EntryIdMap<SyncContentsManifestState> folderData;

		// Token: 0x04000D07 RID: 3335
		private SyncHierarchyManifestState hierData;
	}
}
