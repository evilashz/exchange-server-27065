using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200001D RID: 29
	internal abstract class ContentSynchronizationScopeBase : DisposableBase, IContentSynchronizationScope, IDisposable
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000086A2 File Offset: 0x000068A2
		protected ContentSynchronizationScopeBase(MapiFolder folder, FastTransferDownloadContext context)
		{
			this.folder = folder;
			this.context = context;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000086B8 File Offset: 0x000068B8
		public MapiLogon Logon
		{
			get
			{
				return this.folder.Logon;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000086C5 File Offset: 0x000068C5
		public MapiFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000086CD File Offset: 0x000068CD
		public FastTransferDownloadContext DownloadContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000086D5 File Offset: 0x000068D5
		public MapiContext CurrentOperationContext
		{
			get
			{
				return (MapiContext)this.Logon.StoreMailbox.CurrentOperationContext;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000086EC File Offset: 0x000068EC
		public ExchangeId GetExchangeId(long shortTermId)
		{
			return ExchangeId.CreateFromInt64(this.CurrentOperationContext, this.Logon.StoreMailbox.ReplidGuidMap, shortTermId);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000870A File Offset: 0x0000690A
		public ReplId GuidToReplid(Guid guid)
		{
			return new ReplId(this.Logon.StoreMailbox.ReplidGuidMap.GetReplidFromGuid(this.CurrentOperationContext, guid));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008730 File Offset: 0x00006930
		public IdSet GetServerCnsetSeen(MapiContext operationContext, bool conversations)
		{
			Folder folder;
			if (conversations)
			{
				folder = Microsoft.Exchange.Server.Storage.LogicalDataModel.Folder.OpenFolder(operationContext, this.Logon.StoreMailbox, ConversationItem.GetConversationFolderId(operationContext, this.Logon.StoreMailbox));
			}
			else
			{
				folder = this.Folder.StoreFolder;
			}
			return folder.GetCnsetSeen(operationContext);
		}

		// Token: 0x06000127 RID: 295
		public abstract IEnumerable<Properties> GetChangedMessages(MapiContext operationContext, IcsState icsState);

		// Token: 0x06000128 RID: 296
		public abstract IdSet GetDeletes(MapiContext operationContext, IcsState icsState);

		// Token: 0x06000129 RID: 297
		public abstract IdSet GetSoftDeletes(MapiContext operationContext, IcsState icsState);

		// Token: 0x0600012A RID: 298
		public abstract void GetNewReadsUnreads(MapiContext operationContext, IcsState icsState, out IdSet midsetNewReads, out IdSet midsetNewUnreads, out IdSet finalCnsetRead);

		// Token: 0x0600012B RID: 299
		public abstract FastTransferMessage OpenMessage(ExchangeId mid);

		// Token: 0x0600012C RID: 300
		public abstract PropertyGroupMapping GetPropertyGroupMapping();

		// Token: 0x0600012D RID: 301 RVA: 0x0000877A File Offset: 0x0000697A
		public virtual IChunked PrepareIndexes(MapiContext operationContext, IcsState icsState)
		{
			return null;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008780 File Offset: 0x00006980
		internal static Restriction CreateCnsetSeenRestriction(MapiContext operationContext, ReplidGuidMap replidGuidMap, StorePropTag propTag, IdSet cnset, bool inclusive, out bool residualFilteringRequired)
		{
			residualFilteringRequired = false;
			if (cnset == null || cnset.IsEmpty)
			{
				return null;
			}
			bool flag = propTag == PropTag.Folder.ChangeNumberBin;
			int countRanges = cnset.CountRanges;
			List<ContentSynchronizationScopeBase.IdRange> list = new List<ContentSynchronizationScopeBase.IdRange>((countRanges > 20) ? 20 : countRanges);
			ulong value = ConfigurationSchema.MinRangeSizeForCnRestriction.Value;
			foreach (object obj in ((IEnumerable)cnset))
			{
				GuidGlobCountSet guidGlobCountSet = (GuidGlobCountSet)obj;
				ushort replidFromGuid = replidGuidMap.GetReplidFromGuid(operationContext, guidGlobCountSet.Guid);
				foreach (GlobCountRange globCountRange in guidGlobCountSet.GlobCountSet)
				{
					if (inclusive || globCountRange.HighBound - globCountRange.LowBound > value || countRanges < 20)
					{
						byte[] lowBound;
						byte[] highBound;
						if (flag)
						{
							lowBound = ExchangeIdHelpers.To26ByteArray(replidFromGuid, guidGlobCountSet.Guid, globCountRange.LowBound);
							highBound = ExchangeIdHelpers.To26ByteArray(replidFromGuid, guidGlobCountSet.Guid, globCountRange.HighBound);
						}
						else
						{
							lowBound = ExchangeIdHelpers.To9ByteArray(replidFromGuid, globCountRange.LowBound);
							highBound = ExchangeIdHelpers.To9ByteArray(replidFromGuid, globCountRange.HighBound);
						}
						list.Add(new ContentSynchronizationScopeBase.IdRange(lowBound, highBound));
					}
					else
					{
						residualFilteringRequired = true;
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			list.Sort();
			int num = 0;
			Restriction[] array;
			if (inclusive)
			{
				array = new Restriction[list.Count];
			}
			else
			{
				array = new Restriction[list.Count + 1];
			}
			ContentSynchronizationScopeBase.IdRange idRange = default(ContentSynchronizationScopeBase.IdRange);
			foreach (ContentSynchronizationScopeBase.IdRange idRange2 in list)
			{
				if (inclusive)
				{
					array[num++] = new RestrictionAND(new Restriction[]
					{
						new RestrictionProperty(propTag, RelationOperator.GreaterThanEqual, idRange2.LowBound),
						new RestrictionProperty(propTag, RelationOperator.LessThanEqual, idRange2.HighBound)
					});
				}
				else if (idRange.HighBound == null)
				{
					array[num++] = new RestrictionProperty(propTag, RelationOperator.LessThan, idRange2.LowBound);
				}
				else
				{
					array[num++] = new RestrictionAND(new Restriction[]
					{
						new RestrictionProperty(propTag, RelationOperator.GreaterThan, idRange.HighBound),
						new RestrictionProperty(propTag, RelationOperator.LessThan, idRange2.LowBound)
					});
				}
				idRange = idRange2;
			}
			if (!inclusive)
			{
				array[num++] = new RestrictionProperty(propTag, RelationOperator.GreaterThan, idRange.HighBound);
			}
			return new RestrictionOR(array);
		}

		// Token: 0x04000083 RID: 131
		private MapiFolder folder;

		// Token: 0x04000084 RID: 132
		private FastTransferDownloadContext context;

		// Token: 0x0200001E RID: 30
		private struct IdRange : IComparable<ContentSynchronizationScopeBase.IdRange>
		{
			// Token: 0x0600012F RID: 303 RVA: 0x00008A34 File Offset: 0x00006C34
			public IdRange(byte[] lowBound, byte[] highBound)
			{
				this.lowBound = lowBound;
				this.highBound = highBound;
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000130 RID: 304 RVA: 0x00008A44 File Offset: 0x00006C44
			public byte[] LowBound
			{
				get
				{
					return this.lowBound;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000131 RID: 305 RVA: 0x00008A4C File Offset: 0x00006C4C
			public byte[] HighBound
			{
				get
				{
					return this.highBound;
				}
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00008A54 File Offset: 0x00006C54
			public int CompareTo(ContentSynchronizationScopeBase.IdRange other)
			{
				return ValueHelper.ArraysCompare<byte>(this.lowBound, other.lowBound);
			}

			// Token: 0x04000085 RID: 133
			private byte[] lowBound;

			// Token: 0x04000086 RID: 134
			private byte[] highBound;
		}
	}
}
