using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000016 RID: 22
	internal class LocalFileItemIdList : IItemIdList
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000032BC File Offset: 0x000014BC
		public LocalFileItemIdList(string sourceId, string filePath, bool isUnsearchable)
		{
			this.SourceId = sourceId;
			this.memoryCache = new List<ItemId>(ConstantProvider.ItemIdListCacheSize);
			this.filePath = filePath;
			this.IsUnsearchable = isUnsearchable;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000032F5 File Offset: 0x000014F5
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000032FD File Offset: 0x000014FD
		public string SourceId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003306 File Offset: 0x00001506
		public IList<ItemId> MemoryCache
		{
			get
			{
				return this.memoryCache;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000330E File Offset: 0x0000150E
		public bool Exists
		{
			get
			{
				if (this.exists == null)
				{
					this.exists = new bool?(File.Exists(this.filePath));
				}
				return this.exists.Value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000333E File Offset: 0x0000153E
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003346 File Offset: 0x00001546
		public bool IsUnsearchable { get; private set; }

		// Token: 0x060000B8 RID: 184 RVA: 0x0000334F File Offset: 0x0000154F
		public void WriteItemId(ItemId itemId)
		{
			this.memoryCache.Add(itemId);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000343C File Offset: 0x0000163C
		public void Flush()
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				if (this.memoryCache.Count > 0)
				{
					using (FileStream fileStream = new FileStream(this.filePath, FileMode.Append, FileAccess.Write))
					{
						foreach (ItemId itemId in this.memoryCache)
						{
							itemId.WriteToStream(fileStream);
							if (this.IsUnsearchable && !(itemId is UnsearchableItemId))
							{
								UnsearchableItemId.WriteDummyToStream(fileStream);
							}
						}
					}
					Tracer.TraceInformation("LocalFileItemList.Flush: {0} item IDs are written into '{1}'", new object[]
					{
						this.memoryCache.Count,
						this.filePath
					});
				}
				this.memoryCache.Clear();
			}, ExportErrorType.FailedToWriteItemIdList);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000370C File Offset: 0x0000190C
		public IEnumerable<ItemId> ReadItemIds()
		{
			if (this.Exists)
			{
				FileStream fileStream = null;
				try
				{
					LocalFileHelper.CallFileOperation(delegate
					{
						fileStream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read);
					}, ExportErrorType.FailedToReadItemIdList);
					while (fileStream.Position < fileStream.Length)
					{
						ItemId itemId = this.IsUnsearchable ? new UnsearchableItemId() : new ItemId();
						LocalFileHelper.CallFileOperation(delegate
						{
							itemId.ReadFromStream(fileStream, this.SourceId);
						}, ExportErrorType.FailedToReadItemIdList);
						yield return itemId;
					}
				}
				finally
				{
					if (fileStream != null)
					{
						fileStream.Flush();
						fileStream.Dispose();
					}
				}
			}
			yield break;
		}

		// Token: 0x04000056 RID: 86
		private readonly string filePath;

		// Token: 0x04000057 RID: 87
		private List<ItemId> memoryCache;

		// Token: 0x04000058 RID: 88
		private bool? exists = null;
	}
}
