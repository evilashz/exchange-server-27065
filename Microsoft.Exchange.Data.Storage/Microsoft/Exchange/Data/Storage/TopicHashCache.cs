using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200087A RID: 2170
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TopicHashCache
	{
		// Token: 0x060051B8 RID: 20920 RVA: 0x00154F91 File Offset: 0x00153191
		internal static TopicHashCache Load(IXSOFactory xsoFactory, IMailboxSession session)
		{
			return TopicHashCache.Load(xsoFactory, session, 50);
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00154F9C File Offset: 0x0015319C
		internal static TopicHashCache Load(IXSOFactory xsoFactory, IMailboxSession session, int cacheSize)
		{
			TopicHashCache topicHashCache = new TopicHashCache(cacheSize);
			try
			{
				byte[] array = null;
				using (IFolder folder = xsoFactory.BindToFolder(session, DefaultFolderType.Inbox, new PropertyDefinition[]
				{
					FolderSchema.ConversationTopicHashEntries
				}))
				{
					array = (folder.TryGetProperty(FolderSchema.ConversationTopicHashEntries) as byte[]);
				}
				if (array != null)
				{
					using (MemoryStream memoryStream = new MemoryStream(array))
					{
						topicHashCache.Deserialize(memoryStream);
					}
				}
			}
			catch (IOException arg)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<IOException>(0L, "TopicHashCache::Load. Encountered the following exception. Exception = {0}.", arg);
				topicHashCache = new TopicHashCache(cacheSize);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<StorageTransientException>(0L, "TopicHashCache::Load. Encountered the following exception. Exception = {0}.", arg2);
				topicHashCache = new TopicHashCache(cacheSize);
			}
			catch (StoragePermanentException arg3)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<StoragePermanentException>(0L, "TopicHashCache::Load. Encountered the following exception. Exception = {0}.", arg3);
				topicHashCache = new TopicHashCache(cacheSize);
			}
			return topicHashCache;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x001550A8 File Offset: 0x001532A8
		internal static void Save(TopicHashCache cache, IXSOFactory xsoFactory, IMailboxSession session)
		{
			try
			{
				using (IFolder folder = xsoFactory.BindToFolder(session, DefaultFolderType.Inbox, new PropertyDefinition[]
				{
					FolderSchema.ConversationTopicHashEntries
				}))
				{
					using (MemoryStream memoryStream = new MemoryStream(cache.EstimatedSize))
					{
						cache.Serialize(memoryStream);
						folder[FolderSchema.ConversationTopicHashEntries] = memoryStream.ToArray();
						folder.Save();
					}
				}
			}
			catch (IOException arg)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<IOException>(0L, "TopicHashCache::Save. Encountered the following exception. Exception = {0}.", arg);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<StorageTransientException>(0L, "TopicHashCache::Load. Encountered the following exception. Exception = {0}.", arg2);
			}
			catch (StoragePermanentException arg3)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<StoragePermanentException>(0L, "TopicHashCache::Load. Encountered the following exception. Exception = {0}.", arg3);
			}
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x00155198 File Offset: 0x00153398
		private TopicHashCache(int cacheSize)
		{
			this.TopicHashCacheSize = cacheSize;
			this.topicHashes = new List<uint>(this.TopicHashCacheSize);
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x001551D2 File Offset: 0x001533D2
		internal ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x060051BD RID: 20925 RVA: 0x001551DA File Offset: 0x001533DA
		private int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x001551E2 File Offset: 0x001533E2
		internal int Count
		{
			get
			{
				return this.topicHashes.Count;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x001551EF File Offset: 0x001533EF
		internal int Capacity
		{
			get
			{
				return this.TopicHashCacheSize;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x001551F7 File Offset: 0x001533F7
		internal ReadOnlyCollection<uint> TopicHashes
		{
			get
			{
				return new ReadOnlyCollection<uint>(this.topicHashes);
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x00155204 File Offset: 0x00153404
		private int EstimatedSize
		{
			get
			{
				return 4 * this.topicHashes.Count + 20;
			}
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00155218 File Offset: 0x00153418
		internal void Add(uint hash)
		{
			int num = (this.index == this.TopicHashCacheSize - 1) ? 0 : (this.index + 1);
			if (this.topicHashes.Count == this.TopicHashCacheSize)
			{
				this.topicHashes[num] = hash;
			}
			else
			{
				this.topicHashes.Add(hash);
			}
			this.index = num;
			this.lastModifiedTime = ExDateTime.Now.ToUtc();
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0015528B File Offset: 0x0015348B
		internal bool Contains(uint hash)
		{
			return this.index != -1 && this.topicHashes.Contains(hash);
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x001552A4 File Offset: 0x001534A4
		private void Serialize(MemoryStream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(this.lastModifiedTime.ToBinary());
				binaryWriter.Write(this.TopicHashCacheSize);
				binaryWriter.Write(this.index);
				binaryWriter.Write(this.topicHashes.Count);
				for (int i = 0; i < this.topicHashes.Count; i++)
				{
					binaryWriter.Write(this.topicHashes[i]);
				}
			}
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00155338 File Offset: 0x00153538
		private void Deserialize(MemoryStream stream)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				if (binaryReader.BaseStream.Length != 0L)
				{
					ExDateTime dt = ExDateTime.FromBinary(binaryReader.ReadInt64());
					if ((ExDateTime.Now.ToUtc() - dt).TotalDays <= 3.0)
					{
						int num = binaryReader.ReadInt32();
						if (this.TopicHashCacheSize == num)
						{
							this.lastModifiedTime = dt;
							this.index = binaryReader.ReadInt32();
							int num2 = binaryReader.ReadInt32();
							for (int i = 0; i < num2; i++)
							{
								this.topicHashes.Add(binaryReader.ReadUInt32());
							}
						}
					}
				}
			}
		}

		// Token: 0x04002C70 RID: 11376
		private const int DefaultCacheSize = 50;

		// Token: 0x04002C71 RID: 11377
		private const int DiscardCacheAfterDays = 3;

		// Token: 0x04002C72 RID: 11378
		private readonly int TopicHashCacheSize = 50;

		// Token: 0x04002C73 RID: 11379
		private ExDateTime lastModifiedTime = ExDateTime.MinValue;

		// Token: 0x04002C74 RID: 11380
		private int index = -1;

		// Token: 0x04002C75 RID: 11381
		private List<uint> topicHashes;
	}
}
