using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200001E RID: 30
	internal class SearchResults : ISearchResults
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004411 File Offset: 0x00002611
		internal SearchResults(SourceInformationCollection sources, ITarget target)
		{
			this.sourceMailboxes = sources;
			this.target = target;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000048B4 File Offset: 0x00002AB4
		public IEnumerable<IDictionary<string, object>> SearchResultItems
		{
			get
			{
				foreach (SourceInformation source in this.sourceMailboxes.Values)
				{
					IItemIdList itemIdList = this.target.CreateItemIdList(source.Configuration.Id, false);
					if (itemIdList.Exists)
					{
						foreach (ItemId itemId in itemIdList.ReadItemIds())
						{
							if (!itemId.IsDuplicate)
							{
								yield return new Dictionary<string, object>
								{
									{
										"Id",
										itemId.Id
									},
									{
										"DocumentId",
										itemId.DocumentId
									},
									{
										"Size",
										itemId.Size
									},
									{
										"OriginalPath",
										itemId.ParentFolder
									},
									{
										"Subject",
										itemId.Subject
									},
									{
										"Sender",
										itemId.Sender
									},
									{
										"SenderSmtpAddress",
										itemId.SenderSmtpAddress
									},
									{
										"SentTime",
										itemId.SentTime
									},
									{
										"ReceivedTime",
										itemId.ReceivedTime
									},
									{
										"Summary",
										itemId.BodyPreview
									},
									{
										"ToRecipients",
										itemId.ToRecipients
									},
									{
										"CcRecipients",
										itemId.CcRecipients
									},
									{
										"BccRecipients",
										itemId.BccRecipients
									},
									{
										"ToGroupExpansionRecipients",
										itemId.ToGroupExpansionRecipients
									},
									{
										"CcGroupExpansionRecipients",
										itemId.CcGroupExpansionRecipients
									},
									{
										"BccGroupExpansionRecipients",
										itemId.BccGroupExpansionRecipients
									},
									{
										"GroupExpansionError",
										(itemId.DGGroupExpansionError == null) ? string.Empty : itemId.DGGroupExpansionError.ToString()
									}
								};
							}
						}
					}
				}
				yield break;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004D04 File Offset: 0x00002F04
		public IEnumerable<IDictionary<string, object>> UnsearchableItems
		{
			get
			{
				foreach (SourceInformation source in this.sourceMailboxes.Values)
				{
					IItemIdList itemIdList = this.target.CreateItemIdList(source.Configuration.Id, true);
					if (itemIdList.Exists)
					{
						foreach (ItemId itemId2 in itemIdList.ReadItemIds())
						{
							UnsearchableItemId itemId = (UnsearchableItemId)itemId2;
							if (!itemId.IsDuplicate)
							{
								yield return new Dictionary<string, object>
								{
									{
										"Id",
										itemId.Id
									},
									{
										"Size",
										itemId.Size
									},
									{
										"OriginalPath",
										itemId.ParentFolder
									},
									{
										"Subject",
										itemId.Subject
									},
									{
										"Sender",
										itemId.Sender
									},
									{
										"SentTime",
										itemId.SentTime
									},
									{
										"ReceivedTime",
										itemId.ReceivedTime
									},
									{
										"Summary",
										itemId.BodyPreview
									},
									{
										"ToRecipients",
										itemId.ToRecipients
									},
									{
										"CcRecipients",
										itemId.CcRecipients
									},
									{
										"BccRecipients",
										itemId.BccRecipients
									},
									{
										"ErrorCode",
										itemId.ErrorCode
									},
									{
										"ErrorDescription",
										itemId.ErrorDescription
									},
									{
										"AdditionalInformation",
										itemId.AdditionalInformation
									},
									{
										"LastAttemptTime",
										itemId.LastAttemptTime
									}
								};
							}
						}
					}
				}
				yield break;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004D3E File Offset: 0x00002F3E
		public int SearchResultItemsCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum(delegate(SourceInformation x)
				{
					if (x.Status.ItemCount < 0)
					{
						return 0;
					}
					return x.Status.ItemCount;
				});
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004D7A File Offset: 0x00002F7A
		public int ProcessedItemCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.ProcessedItemCount);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public int UnsearchableItemsCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum(delegate(SourceInformation x)
				{
					if (x.Status.UnsearchableItemCount < 0)
					{
						return 0;
					}
					return x.Status.UnsearchableItemCount;
				});
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004E02 File Offset: 0x00003002
		public int ProcessedUnsearchableItemCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.ProcessedUnsearchableItemCount);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004E3E File Offset: 0x0000303E
		public long TotalSize
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.TotalSize);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004E6D File Offset: 0x0000306D
		public string ItemIdKey
		{
			get
			{
				return "Id";
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004E74 File Offset: 0x00003074
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004E7C File Offset: 0x0000307C
		public long EstimatedItemCount { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004E85 File Offset: 0x00003085
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004E8D File Offset: 0x0000308D
		public long EstimatedUnsearchableItemCount { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004EA3 File Offset: 0x000030A3
		public long DuplicateItemCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.DuplicateItemCount);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004EDF File Offset: 0x000030DF
		public long UnsearchableDuplicateItemCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.UnsearchableDuplicateItemCount);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004F1B File Offset: 0x0000311B
		public long ErrorItemCount
		{
			get
			{
				return this.sourceMailboxes.Values.Sum((SourceInformation x) => x.Status.ErrorItemCount);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004F4A File Offset: 0x0000314A
		public void IncrementErrorItemCount(string sourceId)
		{
			if (!string.IsNullOrWhiteSpace(sourceId))
			{
				this.sourceMailboxes[sourceId].Status.ErrorItemCount += 1L;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004F94 File Offset: 0x00003194
		public ISourceStatus GetSourceStatusBySourceId(string sourceId)
		{
			SourceInformation sourceInformation = this.sourceMailboxes.Values.FirstOrDefault((SourceInformation x) => x.Configuration.Id == sourceId);
			if (sourceInformation != null)
			{
				return sourceInformation.Status;
			}
			return null;
		}

		// Token: 0x04000070 RID: 112
		private SourceInformationCollection sourceMailboxes;

		// Token: 0x04000071 RID: 113
		private ITarget target;
	}
}
