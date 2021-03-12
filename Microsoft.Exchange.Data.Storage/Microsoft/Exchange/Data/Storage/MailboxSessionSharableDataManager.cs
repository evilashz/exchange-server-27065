using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028E RID: 654
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxSessionSharableDataManager
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x0007DE08 File Offset: 0x0007C008
		public MailboxSessionSharableDataManager()
		{
			this.defaultFolderData = new DefaultFolderData[DefaultFolderInfo.DefaultFolderTypeCount];
			for (int i = 0; i < this.defaultFolderData.Length; i++)
			{
				this.defaultFolderData[i] = new DefaultFolderData(false);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0007DE57 File Offset: 0x0007C057
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0007DE5F File Offset: 0x0007C05F
		internal bool DefaultFoldersInitialized
		{
			get
			{
				return this.defaultFoldersInitialized;
			}
			set
			{
				if (this.defaultFoldersInitialized != value && this.defaultFoldersInitialized)
				{
					throw new InvalidOperationException("Default folders already initialized.");
				}
				this.defaultFoldersInitialized = value;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0007DE84 File Offset: 0x0007C084
		internal DefaultFolderData GetDefaultFolder(DefaultFolderType defaultFolderType)
		{
			return this.defaultFolderData[(int)defaultFolderType];
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0007DE8E File Offset: 0x0007C08E
		internal void SetDefaultFolder(DefaultFolderType defaultFolderType, DefaultFolderData defaultFolderData)
		{
			this.defaultFolderData[(int)defaultFolderType] = defaultFolderData;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0007DE99 File Offset: 0x0007C099
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0007DEA1 File Offset: 0x0007C0A1
		internal CultureInfo DefaultFoldersCulture { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0007DEAA File Offset: 0x0007C0AA
		internal bool HasCacheEntryLoaded
		{
			get
			{
				return this.hasUserConfigurationLoaded;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0007DEB4 File Offset: 0x0007C0B4
		internal bool HasCacheEntries
		{
			get
			{
				bool result;
				lock (this.userConfigCacheEntryList)
				{
					result = (this.userConfigCacheEntryList.Count > 0);
				}
				return result;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0007DF00 File Offset: 0x0007C100
		internal void Remove(UserConfigurationName configName, StoreObjectId folderId)
		{
			LinkedListNode<UserConfigurationCacheEntry> linkedListNode = this.userConfigCacheEntryList.First;
			lock (this.userConfigCacheEntryList)
			{
				while (linkedListNode != null)
				{
					UserConfigurationCacheEntry value = linkedListNode.Value;
					if (value.CheckMatch(configName.Name, folderId))
					{
						this.userConfigCacheEntryList.Remove(linkedListNode);
						break;
					}
					linkedListNode = linkedListNode.Next;
				}
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0007DF78 File Offset: 0x0007C178
		internal void ParseUserConfigurationCacheEntries(BinaryReader reader)
		{
			reader.ReadUInt16();
			int num = reader.ReadInt32();
			this.hasUserConfigurationLoaded = true;
			if (num <= 32)
			{
				for (int i = 0; i < num; i++)
				{
					string configName = reader.ReadString();
					int count = reader.ReadInt32();
					byte[] byteArray = reader.ReadBytes(count);
					count = reader.ReadInt32();
					byte[] byteArray2 = reader.ReadBytes(count);
					this.AddUserConfigurationCachedEntry(StoreObjectId.Parse(byteArray, 0), configName, StoreObjectId.Parse(byteArray2, 0), true);
				}
				return;
			}
			ExTraceGlobals.UserConfigurationTracer.TraceError<int>((long)this.GetHashCode(), "UserConfigurationCache::Load. The retreived cache count is too big, persisted data is corrupted. Count = {0}.", num);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0007E010 File Offset: 0x0007C210
		internal void SerializeUserConfigurationCacheEntries(BinaryWriter writer)
		{
			writer.Write(1);
			int capacity = 0;
			lock (this.userConfigCacheEntryList)
			{
				capacity = this.userConfigCacheEntryList.Count;
			}
			List<UserConfigurationCacheEntry> list = new List<UserConfigurationCacheEntry>(capacity);
			lock (this.userConfigCacheEntryList)
			{
				list.AddRange(new List<UserConfigurationCacheEntry>(this.userConfigCacheEntryList));
			}
			writer.Write(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				UserConfigurationCacheEntry userConfigurationCacheEntry = list[i];
				string configurationName = userConfigurationCacheEntry.ConfigurationName;
				writer.Write(configurationName);
				byte[] bytes = userConfigurationCacheEntry.FolderId.GetBytes();
				writer.Write(bytes.Length);
				writer.Write(bytes);
				byte[] bytes2 = userConfigurationCacheEntry.ItemId.GetBytes();
				writer.Write(bytes2.Length);
				writer.Write(bytes2);
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0007E128 File Offset: 0x0007C328
		internal void AddUserConfigurationCachedEntry(StoreObjectId folderId, string configName, StoreObjectId itemId, bool canCleanse)
		{
			UserConfigurationCacheEntry value = new UserConfigurationCacheEntry(configName, folderId, itemId);
			LinkedListNode<UserConfigurationCacheEntry> node = new LinkedListNode<UserConfigurationCacheEntry>(value);
			lock (this.userConfigCacheEntryList)
			{
				for (LinkedListNode<UserConfigurationCacheEntry> linkedListNode = this.userConfigCacheEntryList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					UserConfigurationCacheEntry value2 = linkedListNode.Value;
					if (value2.CheckMatch(configName, folderId))
					{
						linkedListNode.Value = value;
						return;
					}
				}
				if (canCleanse && this.userConfigCacheEntryList.Count > 32)
				{
					this.userConfigCacheEntryList.RemoveLast();
					ExTraceGlobals.UserConfigurationTracer.TraceDebug<int>((long)this.GetHashCode(), "UserConfigurationCache::Add. The cache has reached the capacity: {0} the last entry was removed.", 32);
				}
				this.userConfigCacheEntryList.AddFirst(node);
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0007E1EC File Offset: 0x0007C3EC
		internal bool TryGetUserConfigurationCachedEntry(UserConfigurationName configName, StoreObjectId folderId, out UserConfigurationCacheEntry cacheEntry)
		{
			bool result;
			lock (this.userConfigCacheEntryList)
			{
				for (LinkedListNode<UserConfigurationCacheEntry> linkedListNode = this.userConfigCacheEntryList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					UserConfigurationCacheEntry value = linkedListNode.Value;
					if (value.CheckMatch(configName.Name, folderId))
					{
						cacheEntry = value;
						return true;
					}
				}
				cacheEntry = null;
				result = false;
			}
			return result;
		}

		// Token: 0x040012F2 RID: 4850
		private const int MaxCachedUserConfigurationCacheEntries = 32;

		// Token: 0x040012F3 RID: 4851
		private const ushort StreamBlobVersion = 1;

		// Token: 0x040012F4 RID: 4852
		private readonly LinkedList<UserConfigurationCacheEntry> userConfigCacheEntryList = new LinkedList<UserConfigurationCacheEntry>();

		// Token: 0x040012F5 RID: 4853
		private readonly DefaultFolderData[] defaultFolderData;

		// Token: 0x040012F6 RID: 4854
		private bool hasUserConfigurationLoaded;

		// Token: 0x040012F7 RID: 4855
		private bool defaultFoldersInitialized;
	}
}
