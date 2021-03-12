using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	[XmlType(TypeName = "EasHierarchySyncState")]
	[Serializable]
	public class EasHierarchySyncState : XMLSerializableBase
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003D90 File Offset: 0x00001F90
		public EasHierarchySyncState()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D98 File Offset: 0x00001F98
		internal EasHierarchySyncState(Add[] folders, string syncKey)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("folders", folders);
			ArgumentValidator.ThrowIfNullOrEmpty("syncKey", syncKey);
			this.Folders = new List<Add>(folders);
			this.SyncKey = syncKey;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003E64 File Offset: 0x00002064
		[XmlElement(ElementName = "Folder")]
		public EasHierarchySyncState.EasFolderData[] FolderData
		{
			get
			{
				if (this.Folders == null || this.Folders.Count == 0)
				{
					return Array<EasHierarchySyncState.EasFolderData>.Empty;
				}
				EasHierarchySyncState.EasFolderData[] array = new EasHierarchySyncState.EasFolderData[this.Folders.Count];
				for (int i = 0; i < this.Folders.Count; i++)
				{
					Add add = this.Folders[i];
					array[i] = new EasHierarchySyncState.EasFolderData
					{
						ServerId = add.ServerId,
						ParentId = add.ParentId,
						DisplayName = add.DisplayName,
						Type = add.Type
					};
				}
				return array;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.Folders = new List<Add>(0);
					return;
				}
				List<Add> list = new List<Add>(value.Length);
				for (int i = 0; i < value.Length; i++)
				{
					EasHierarchySyncState.EasFolderData easFolderData = value[i];
					list.Add(new Add
					{
						ServerId = easFolderData.ServerId,
						ParentId = easFolderData.ParentId,
						DisplayName = easFolderData.DisplayName,
						Type = easFolderData.Type
					});
				}
				this.Folders = list;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003EEC File Offset: 0x000020EC
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003EF4 File Offset: 0x000020F4
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003EFD File Offset: 0x000020FD
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003F05 File Offset: 0x00002105
		[XmlIgnore]
		public List<Add> Folders { get; private set; }

		// Token: 0x06000096 RID: 150 RVA: 0x00003F0E File Offset: 0x0000210E
		internal static EasHierarchySyncState Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<EasHierarchySyncState>(data, false);
		}

		// Token: 0x02000008 RID: 8
		public sealed class EasFolderData : XMLSerializableBase
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000097 RID: 151 RVA: 0x00003F17 File Offset: 0x00002117
			// (set) Token: 0x06000098 RID: 152 RVA: 0x00003F1F File Offset: 0x0000211F
			[XmlAttribute(AttributeName = "ServerId")]
			public string ServerId { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000099 RID: 153 RVA: 0x00003F28 File Offset: 0x00002128
			// (set) Token: 0x0600009A RID: 154 RVA: 0x00003F30 File Offset: 0x00002130
			[XmlAttribute(AttributeName = "ParentId")]
			public string ParentId { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00003F39 File Offset: 0x00002139
			// (set) Token: 0x0600009C RID: 156 RVA: 0x00003F41 File Offset: 0x00002141
			[XmlText]
			public string DisplayName { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00003F4A File Offset: 0x0000214A
			// (set) Token: 0x0600009E RID: 158 RVA: 0x00003F52 File Offset: 0x00002152
			[XmlAttribute(AttributeName = "Type")]
			public int Type { get; set; }
		}
	}
}
