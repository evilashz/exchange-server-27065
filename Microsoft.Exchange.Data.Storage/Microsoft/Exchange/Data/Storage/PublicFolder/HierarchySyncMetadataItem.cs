using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x0200093A RID: 2362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class HierarchySyncMetadataItem : Item, IHierarchySyncMetadataItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06005803 RID: 22531 RVA: 0x00169F5F File Offset: 0x0016815F
		internal HierarchySyncMetadataItem(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x00169F69 File Offset: 0x00168169
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return HierarchySyncMetadataItemSchema.Instance;
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x00169F7B File Offset: 0x0016817B
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x00169F93 File Offset: 0x00168193
		public ExDateTime LastAttemptedSyncTime
		{
			get
			{
				this.CheckDisposed("LastAttemptedSyncTime::get");
				return base.GetValueOrDefault<ExDateTime>(HierarchySyncMetadataItemSchema.LastAttemptedSyncTime);
			}
			set
			{
				this.CheckDisposed("LastAttemptedSyncTime::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.LastAttemptedSyncTime, value);
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x00169FB1 File Offset: 0x001681B1
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x00169FC9 File Offset: 0x001681C9
		public ExDateTime LastFailedSyncTime
		{
			get
			{
				this.CheckDisposed("LastFailedSyncTime::get");
				return base.GetValueOrDefault<ExDateTime>(HierarchySyncMetadataItemSchema.LastFailedSyncTime);
			}
			set
			{
				this.CheckDisposed("LastFailedSyncTime::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.LastFailedSyncTime, value);
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x00169FE7 File Offset: 0x001681E7
		// (set) Token: 0x0600580A RID: 22538 RVA: 0x00169FFF File Offset: 0x001681FF
		public ExDateTime LastSuccessfulSyncTime
		{
			get
			{
				this.CheckDisposed("LastSuccessfulSyncTime::get");
				return base.GetValueOrDefault<ExDateTime>(HierarchySyncMetadataItemSchema.LastSuccessfulSyncTime);
			}
			set
			{
				this.CheckDisposed("LastSuccessfulSyncTime::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.LastSuccessfulSyncTime, value);
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0016A01D File Offset: 0x0016821D
		// (set) Token: 0x0600580C RID: 22540 RVA: 0x0016A035 File Offset: 0x00168235
		public ExDateTime FirstFailedSyncTimeAfterLastSuccess
		{
			get
			{
				this.CheckDisposed("FirstFailedSyncTimeAfterLastSuccess::get");
				return base.GetValueOrDefault<ExDateTime>(HierarchySyncMetadataItemSchema.FirstFailedSyncTimeAfterLastSuccess);
			}
			set
			{
				this.CheckDisposed("FirstFailedSyncTimeAfterLastSuccess::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.FirstFailedSyncTimeAfterLastSuccess, value);
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0016A053 File Offset: 0x00168253
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x0016A06B File Offset: 0x0016826B
		public string LastSyncFailure
		{
			get
			{
				this.CheckDisposed("LastSyncFailure::get");
				return base.GetValueOrDefault<string>(HierarchySyncMetadataItemSchema.LastSyncFailure);
			}
			set
			{
				this.CheckDisposed("LastSyncFailure::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.LastSyncFailure, value);
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x0016A084 File Offset: 0x00168284
		// (set) Token: 0x06005810 RID: 22544 RVA: 0x0016A09C File Offset: 0x0016829C
		public int NumberOfAttemptsAfterLastSuccess
		{
			get
			{
				this.CheckDisposed("NumberOfAttemptsAfterLastSuccess::get");
				return base.GetValueOrDefault<int>(HierarchySyncMetadataItemSchema.NumberOfAttemptsAfterLastSuccess);
			}
			set
			{
				this.CheckDisposed("NumberOfAttemptsAfterLastSuccess::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.NumberOfAttemptsAfterLastSuccess, value);
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x0016A0BA File Offset: 0x001682BA
		// (set) Token: 0x06005812 RID: 22546 RVA: 0x0016A0D2 File Offset: 0x001682D2
		public int NumberOfBatchesExecuted
		{
			get
			{
				this.CheckDisposed("NumberOfBatchesExecuted::get");
				return base.GetValueOrDefault<int>(HierarchySyncMetadataItemSchema.NumberOfBatchesExecuted);
			}
			set
			{
				this.CheckDisposed("NumberOfBatchesExecuted::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.NumberOfBatchesExecuted, value);
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x0016A0F0 File Offset: 0x001682F0
		// (set) Token: 0x06005814 RID: 22548 RVA: 0x0016A108 File Offset: 0x00168308
		public int NumberOfFoldersSynced
		{
			get
			{
				this.CheckDisposed("NumberOfFoldersSynced::get");
				return base.GetValueOrDefault<int>(HierarchySyncMetadataItemSchema.NumberOfFoldersSynced);
			}
			set
			{
				this.CheckDisposed("NumberOfFoldersSynced::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.NumberOfFoldersSynced, value);
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x0016A126 File Offset: 0x00168326
		// (set) Token: 0x06005816 RID: 22550 RVA: 0x0016A13E File Offset: 0x0016833E
		public int NumberOfFoldersToBeSynced
		{
			get
			{
				this.CheckDisposed("NumberOfFoldersToBeSynced::get");
				return base.GetValueOrDefault<int>(HierarchySyncMetadataItemSchema.NumberOfFoldersToBeSynced);
			}
			set
			{
				this.CheckDisposed("NumberOfFoldersToBeSynced::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.NumberOfFoldersToBeSynced, value);
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06005817 RID: 22551 RVA: 0x0016A15C File Offset: 0x0016835C
		// (set) Token: 0x06005818 RID: 22552 RVA: 0x0016A174 File Offset: 0x00168374
		public int BatchSize
		{
			get
			{
				this.CheckDisposed("BatchSize::get");
				return base.GetValueOrDefault<int>(HierarchySyncMetadataItemSchema.BatchSize);
			}
			set
			{
				this.CheckDisposed("BatchSize::set");
				base.SetOrDeleteProperty(HierarchySyncMetadataItemSchema.BatchSize, value);
			}
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x0016A194 File Offset: 0x00168394
		public void SetPartiallyCommittedFolderIds(IdSet value)
		{
			this.CheckDisposed("SetPartiallyCommittedFolderIds");
			using (Stream stream = HierarchySyncMetadataItem.SyncMetadataAttachmentStream.CreateAttachment(this, "PartiallyCommittedFolderIds", true))
			{
				if (value != null)
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						value.SerializeWithReplGuids(streamWriter);
					}
				}
			}
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x0016A200 File Offset: 0x00168400
		public IdSet GetPartiallyCommittedFolderIds()
		{
			this.CheckDisposed("GetPartiallyCommittedFolderIds::get");
			IdSet result = null;
			using (Stream existingAttachment = HierarchySyncMetadataItem.SyncMetadataAttachmentStream.GetExistingAttachment(this, "PartiallyCommittedFolderIds", StreamBase.Capabilities.Readable))
			{
				if (existingAttachment != null)
				{
					using (StreamReader streamReader = new StreamReader(existingAttachment))
					{
						result = IdSet.ParseWithReplGuids(streamReader);
					}
				}
			}
			return result;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0016A270 File Offset: 0x00168470
		private new static HierarchySyncMetadataItem Bind(StoreSession session, StoreId itemId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<HierarchySyncMetadataItem>(session, itemId, HierarchySyncMetadataItemSchema.Instance, propsToReturn);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0016A280 File Offset: 0x00168480
		private static HierarchySyncMetadataItem Create(StoreSession session, StoreId folderId)
		{
			HierarchySyncMetadataItem hierarchySyncMetadataItem = ItemBuilder.CreateNewItem<HierarchySyncMetadataItem>(session, folderId, ItemCreateInfo.HierarchySyncMetadataInfo, CreateMessageType.Associated);
			hierarchySyncMetadataItem[StoreObjectSchema.ItemClass] = "IPM.HierarchySync.Metadata";
			return hierarchySyncMetadataItem;
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x0016A2AC File Offset: 0x001684AC
		public Stream GetSyncStateReadStream()
		{
			this.CheckDisposed("GetSyncStateReadStream");
			return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.GetExistingAttachment(this, "SyncState", StreamBase.Capabilities.Readable);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x0016A2C5 File Offset: 0x001684C5
		public Stream GetSyncStateOverrideStream()
		{
			this.CheckDisposed("GetSyncStateOverrideStream");
			return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.CreateAttachment(this, "SyncState", true);
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x0016A2DE File Offset: 0x001684DE
		public Stream GetFinalJobSyncStateReadStream()
		{
			this.CheckDisposed("GetFinalJobSyncStateReadStream");
			return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.GetExistingAttachment(this, "FinalJobSyncState", StreamBase.Capabilities.Readable);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x0016A2F7 File Offset: 0x001684F7
		public Stream GetFinalJobSyncStateWriteStream(bool overrideIfExisting)
		{
			this.CheckDisposed("GetFinalJobSyncStateWriteStream");
			return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.CreateAttachment(this, "FinalJobSyncState", overrideIfExisting);
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x0016A310 File Offset: 0x00168510
		public void CommitSyncStateForCompletedBatch()
		{
			this.CheckDisposed("CommitSyncStateForCompletedBatch");
			this.DeleteAttachment("SyncState");
			this.RenameAttachment("FinalJobSyncState", "SyncState");
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0016A338 File Offset: 0x00168538
		public void ClearSyncState()
		{
			this.CheckDisposed("DeleteSyncState");
			this.DeleteAttachment("SyncState");
			this.DeleteAttachment("FinalJobSyncState");
			this.DeleteAttachment("PartiallyCommittedFolderIds");
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x0016A368 File Offset: 0x00168568
		private StreamAttachment GetStreamAttachment(string name)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				foreach (AttachmentHandle handle in base.AttachmentCollection)
				{
					Attachment attachment = base.AttachmentCollection.Open(handle, null);
					disposeGuard.Add<Attachment>(attachment);
					if (attachment.AttachmentType == AttachmentType.Stream && !string.IsNullOrWhiteSpace(attachment.FileName) && attachment.FileName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
					{
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						if (streamAttachment != null)
						{
							disposeGuard.Success();
							return streamAttachment;
						}
					}
					attachment.Dispose();
				}
			}
			return null;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x0016A434 File Offset: 0x00168634
		private StreamAttachment CreateStreamAttachment(string name)
		{
			base.OpenAsReadWrite();
			StreamAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StreamAttachment streamAttachment = base.AttachmentCollection.Create(AttachmentType.Stream) as StreamAttachment;
				disposeGuard.Add<StreamAttachment>(streamAttachment);
				streamAttachment.FileName = name;
				disposeGuard.Success();
				result = streamAttachment;
			}
			return result;
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x0016A49C File Offset: 0x0016869C
		private void DeleteAttachment(string attachmentName)
		{
			base.OpenAsReadWrite();
			using (StreamAttachment streamAttachment = this.GetStreamAttachment(attachmentName))
			{
				if (streamAttachment != null)
				{
					HierarchySyncMetadataItem.Tracer.TraceDebug<string, AttachmentId>((long)this.GetHashCode(), "HierarchySyncMetadataItem:DeleteAttachment - Removing attachment with name='{0}' and Id='{1}'", attachmentName, streamAttachment.Id);
					base.AttachmentCollection.Remove(streamAttachment.Id);
				}
				else
				{
					HierarchySyncMetadataItem.Tracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItem:DeleteAttachment - Didn't find an stream attachment with name='{0}'", attachmentName);
				}
			}
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x0016A520 File Offset: 0x00168720
		private void RenameAttachment(string originalAttachmentName, string newAttachmentName)
		{
			base.OpenAsReadWrite();
			using (StreamAttachment streamAttachment = this.GetStreamAttachment(originalAttachmentName))
			{
				if (streamAttachment != null)
				{
					HierarchySyncMetadataItem.Tracer.TraceDebug<AttachmentId, string, string>((long)this.GetHashCode(), "HierarchySyncMetadataItem:RenameAttachment - Renaming attachment with Id='{0}'. Old name='{1}', new name='{2}'.", streamAttachment.Id, originalAttachmentName, newAttachmentName);
					streamAttachment.FileName = newAttachmentName;
					streamAttachment.Save();
				}
				else
				{
					HierarchySyncMetadataItem.Tracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchySyncMetadataItem:RenameAttachment - Didn't find an stream attachment with name='{0}'", originalAttachmentName);
				}
			}
		}

		// Token: 0x04002FFF RID: 12287
		internal const string PartiallyCommittedFolderIdsAttachmentName = "PartiallyCommittedFolderIds";

		// Token: 0x04003000 RID: 12288
		internal const string SyncStateAttachmentName = "SyncState";

		// Token: 0x04003001 RID: 12289
		internal const string FinalJobSyncStateAttachmentName = "FinalJobSyncState";

		// Token: 0x04003002 RID: 12290
		private const int ItemQueryBatchSize = 100;

		// Token: 0x04003003 RID: 12291
		private static readonly Trace Tracer = ExTraceGlobals.PublicFoldersTracer;

		// Token: 0x04003004 RID: 12292
		private static readonly PropertyDefinition[] ItemQueryColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04003005 RID: 12293
		private static readonly SortBy[] ItemQuerySortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x0200093B RID: 2363
		private class SyncMetadataAttachmentStream : StreamWrapper
		{
			// Token: 0x06005828 RID: 22568 RVA: 0x0016A5F7 File Offset: 0x001687F7
			private SyncMetadataAttachmentStream(StreamAttachment streamAttachment, Stream contentStream, StreamBase.Capabilities streamCapabilities) : base(contentStream, true, streamCapabilities | StreamBase.Capabilities.Seekable)
			{
				ArgumentValidator.ThrowIfNull("streamAttachment", streamAttachment);
				ArgumentValidator.ThrowIfNull("contentStream", contentStream);
				this.streamAttachment = streamAttachment;
				this.shouldSaveOnDispose = ((streamCapabilities & StreamBase.Capabilities.Writable) == StreamBase.Capabilities.Writable);
			}

			// Token: 0x06005829 RID: 22569 RVA: 0x0016A630 File Offset: 0x00168830
			private static HierarchySyncMetadataItem.SyncMetadataAttachmentStream Instantate(StreamAttachment attachment, StreamBase.Capabilities streamCapabilities)
			{
				if (attachment == null)
				{
					HierarchySyncMetadataItem.Tracer.TraceDebug(0L, "SyncMetadataAttachmentStream:Instantate - Returning null stream as attachment was also null");
					return null;
				}
				HierarchySyncMetadataItem.SyncMetadataAttachmentStream result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					disposeGuard.Add<StreamAttachment>(attachment);
					PropertyOpenMode propertyOpenMode = ((streamCapabilities & StreamBase.Capabilities.Writable) == StreamBase.Capabilities.Writable) ? PropertyOpenMode.Modify : PropertyOpenMode.ReadOnly;
					HierarchySyncMetadataItem.Tracer.TraceDebug<PropertyOpenMode, StreamBase.Capabilities>(0L, "SyncMetadataAttachmentStream:Instantate - Getting content stream. Open Mode={0}, Stream Capabilities={1}", propertyOpenMode, streamCapabilities);
					Stream contentStream = attachment.GetContentStream(propertyOpenMode);
					disposeGuard.Add<Stream>(contentStream);
					HierarchySyncMetadataItem.SyncMetadataAttachmentStream syncMetadataAttachmentStream = new HierarchySyncMetadataItem.SyncMetadataAttachmentStream(attachment, contentStream, streamCapabilities);
					disposeGuard.Success();
					result = syncMetadataAttachmentStream;
				}
				return result;
			}

			// Token: 0x0600582A RID: 22570 RVA: 0x0016A6C8 File Offset: 0x001688C8
			public static HierarchySyncMetadataItem.SyncMetadataAttachmentStream GetExistingAttachment(HierarchySyncMetadataItem item, string attachmentName, StreamBase.Capabilities streamCapabilities)
			{
				ArgumentValidator.ThrowIfNull("item", item);
				ArgumentValidator.ThrowIfNullOrWhiteSpace("attachmentName", attachmentName);
				return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.Instantate(item.GetStreamAttachment(attachmentName), streamCapabilities);
			}

			// Token: 0x0600582B RID: 22571 RVA: 0x0016A6F0 File Offset: 0x001688F0
			public static HierarchySyncMetadataItem.SyncMetadataAttachmentStream CreateAttachment(HierarchySyncMetadataItem item, string attachmentName, bool overrideIfExisting)
			{
				ArgumentValidator.ThrowIfNull("item", item);
				ArgumentValidator.ThrowIfNullOrWhiteSpace("attachmentName", attachmentName);
				using (StreamAttachment streamAttachment = item.GetStreamAttachment(attachmentName))
				{
					if (streamAttachment != null)
					{
						if (!overrideIfExisting)
						{
							HierarchySyncMetadataItem.Tracer.TraceDebug(0L, "SyncMetadataAttachmentStream:CreateAttachment - Skiping creation of sync state attachment and one already exist and override was not selected");
							return null;
						}
						item.DeleteAttachment(attachmentName);
					}
				}
				return HierarchySyncMetadataItem.SyncMetadataAttachmentStream.Instantate(item.CreateStreamAttachment(attachmentName), StreamBase.Capabilities.Writable);
			}

			// Token: 0x0600582C RID: 22572 RVA: 0x0016A768 File Offset: 0x00168968
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (this.streamAttachment != null)
				{
					if (this.shouldSaveOnDispose)
					{
						this.streamAttachment.Save();
					}
					this.streamAttachment.Dispose();
					this.streamAttachment = null;
				}
			}

			// Token: 0x04003006 RID: 12294
			private readonly bool shouldSaveOnDispose;

			// Token: 0x04003007 RID: 12295
			private StreamAttachment streamAttachment;
		}
	}
}
