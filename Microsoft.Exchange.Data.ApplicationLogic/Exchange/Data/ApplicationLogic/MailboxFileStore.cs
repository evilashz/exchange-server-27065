using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200013D RID: 317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxFileStore
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00035ACE File Offset: 0x00033CCE
		private static void NeverAbort()
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00035AD0 File Offset: 0x00033CD0
		public MailboxFileStore(string folderName) : this(folderName, null, null)
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00035ADC File Offset: 0x00033CDC
		public MailboxFileStore(string folderName, IPerformanceDataLogger logger, ITracer additionalTracer)
		{
			this.folderName = folderName;
			if (logger == null)
			{
				this.logger = NullPerformanceDataLogger.Instance;
			}
			else
			{
				this.logger = logger;
			}
			if (additionalTracer == null)
			{
				this.tracer = ExTraceGlobals.MailboxFileStoreTracer;
				return;
			}
			this.tracer = new CompositeTracer(ExTraceGlobals.MailboxFileStoreTracer, additionalTracer);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00035B30 File Offset: 0x00033D30
		public List<FileSetItem> GetAll(string fileSetId, MailboxSession mailboxSession)
		{
			List<FileSetItem> allItems;
			using (Folder folder = this.GetFolder(mailboxSession))
			{
				allItems = this.GetAllItems(folder, fileSetId);
			}
			return allItems;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00035B74 File Offset: 0x00033D74
		public void RemoveAll(string fileSetId, MailboxSession mailboxSession)
		{
			using (Folder folder = this.GetFolder(mailboxSession))
			{
				List<FileSetItem> allItems = this.GetAllItems(folder, fileSetId);
				if (allItems.Count > 0)
				{
					StoreId[] array = allItems.ConvertAll<StoreId>((FileSetItem fileSetItem) => fileSetItem.Id).ToArray();
					this.tracer.TraceDebug<string, string, ArrayTracer<StoreId>>((long)this.GetHashCode(), "Deleting items in folder '{0}' with fileSetId='{1}': {2}", this.folderName, fileSetId, new ArrayTracer<StoreId>(array));
					mailboxSession.Delete(DeleteItemFlags.HardDelete, array);
				}
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00035C10 File Offset: 0x00033E10
		public FileSetItem GetCurrent(string fileSetId, MailboxSession mailboxSession)
		{
			FileSetItem newestItem;
			using (Folder folder = this.GetFolder(mailboxSession))
			{
				newestItem = this.GetNewestItem(folder, fileSetId);
			}
			return newestItem;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00035C4C File Offset: 0x00033E4C
		public List<string> Download(FileSetItem fileSetItem, MailboxSession mailboxSession)
		{
			return this.Download(fileSetItem, mailboxSession, Path.GetTempPath(), false, new Action(MailboxFileStore.NeverAbort));
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00035C68 File Offset: 0x00033E68
		public List<string> Download(FileSetItem fileSetItem, MailboxSession mailboxSession, string tempFolderPath, bool ignoreDuplicateFiles, Action abortFileOperation)
		{
			bool flag = false;
			string text = Path.Combine(tempFolderPath, this.folderName + Path.GetRandomFileName());
			Directory.CreateDirectory(text);
			List<string> list = new List<string>(100);
			List<string> result;
			try
			{
				this.tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Downloading files for fileSetId='{0}' from mailbox folder '{1}' in filesystem folder {2}", fileSetItem.Name, this.folderName, text);
				Stopwatch stopwatch = Stopwatch.StartNew();
				using (Item item = Item.Bind(mailboxSession, fileSetItem.Id))
				{
					foreach (AttachmentHandle attachmentHandle in item.IAttachmentCollection)
					{
						this.tracer.TraceDebug<int>((long)this.GetHashCode(), "Handling attachment id:'{0}'", attachmentHandle.AttachNumber);
						using (IAttachment attachment = item.IAttachmentCollection.OpenIAttachment(attachmentHandle))
						{
							IStreamAttachment streamAttachment = attachment as IStreamAttachment;
							if (streamAttachment != null)
							{
								string text2 = this.DownloadSingleFile(streamAttachment, text, ignoreDuplicateFiles, abortFileOperation);
								if (text2 != null)
								{
									this.tracer.TraceDebug<int, string>((long)this.GetHashCode(), "Successfully downloaded attachment '{0}' into file {1}", attachmentHandle.AttachNumber, text2);
									list.Add(text2);
								}
								else
								{
									this.tracer.TraceError<int>((long)this.GetHashCode(), "Ignoring attachment '{0}' because could not download its content", attachmentHandle.AttachNumber);
								}
							}
							else
							{
								this.tracer.TraceError<int>((long)this.GetHashCode(), "Ignoring attachment '{0}' because it is not stream attachment", attachmentHandle.AttachNumber);
							}
						}
					}
				}
				stopwatch.Stop();
				this.tracer.TraceDebug<int, long>((long)this.GetHashCode(), "Downloaded '{0}' files. Downloaded elapsed time: {1}ms", list.Count, stopwatch.ElapsedMilliseconds);
				flag = true;
				result = list;
			}
			finally
			{
				if (!flag || list == null || list.Count == 0)
				{
					this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Download failed and temporary folder '{0}' needs to be removed", text);
					this.DeleteFolder(text);
				}
			}
			return result;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00035EA4 File Offset: 0x000340A4
		public void Upload(IEnumerable<string> sources, string fileSetId, MailboxSession mailboxSession)
		{
			this.Upload(sources, fileSetId, mailboxSession, new Action(MailboxFileStore.NeverAbort));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00035EC4 File Offset: 0x000340C4
		public void Upload(IEnumerable<string> sources, string fileSetId, MailboxSession mailboxSession, Action abortFileOperation)
		{
			using (Folder folder = this.GetFolder(mailboxSession))
			{
				List<FileSetItem> allItems = this.GetAllItems(folder, fileSetId);
				this.UploadInternal(sources, fileSetId, folder, mailboxSession, abortFileOperation);
				if (allItems.Count > 0)
				{
					StoreId[] array = allItems.ConvertAll<StoreId>((FileSetItem fileSetItem) => fileSetItem.Id).ToArray();
					this.tracer.TraceDebug<string, string, ArrayTracer<StoreId>>((long)this.GetHashCode(), "Deleting items in folder '{0}' with fileSetId='{1}' that are now outdated: {2}", this.folderName, fileSetId, new ArrayTracer<StoreId>(array));
					mailboxSession.Delete(DeleteItemFlags.HardDelete, array);
				}
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00035F6C File Offset: 0x0003416C
		public Stream GetSingleFile(FileSetItem fileSetItem, string fileName, MailboxSession mailboxSession)
		{
			MemoryStream memoryStream = null;
			try
			{
				using (Item item = Item.Bind(mailboxSession, fileSetItem.Id))
				{
					foreach (AttachmentHandle handle in item.IAttachmentCollection)
					{
						using (IAttachment attachment = item.IAttachmentCollection.OpenIAttachment(handle))
						{
							if (string.Equals(fileName, attachment[AttachmentSchema.AttachLongFileName] as string, StringComparison.OrdinalIgnoreCase))
							{
								IStreamAttachment attachment2 = attachment as IStreamAttachment;
								memoryStream = new MemoryStream();
								this.ReadAttachmentIntoStreamAndValidateHash(attachment2, new NoCloseStream(memoryStream), new Action(MailboxFileStore.NeverAbort));
								memoryStream.Seek(0L, SeekOrigin.Begin);
								break;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
					memoryStream = null;
				}
				throw;
			}
			return memoryStream;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00036070 File Offset: 0x00034270
		internal void UploadInternal(IEnumerable<string> sources, string fileSetId, Folder folder, MailboxSession mailboxSession, Action abortFileOperation)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			VersionedId id;
			using (Item item = Item.Create(mailboxSession, "IPM.FileSet", folder.Id))
			{
				item[ItemSchema.Subject] = fileSetId;
				foreach (string fileName in sources)
				{
					this.UploadSingleFile(fileName, item.IAttachmentCollection, abortFileOperation);
				}
				item.Save(SaveMode.NoConflictResolution);
				item.Load(new PropertyDefinition[]
				{
					ItemSchema.Id
				});
				id = item.Id;
			}
			stopwatch.Stop();
			this.tracer.TraceDebug((long)this.GetHashCode(), "New item created in folder '{0}' for fileSetId='{1}'. Id: {2}. Upload elapsed time: {3}ms", new object[]
			{
				this.folderName,
				fileSetId,
				id,
				stopwatch.ElapsedMilliseconds
			});
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00036174 File Offset: 0x00034374
		internal List<FileSetItem> GetAllItems(Folder folder, string fileSetId)
		{
			List<FileSetItem> list = new List<FileSetItem>(2);
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, MailboxFileStore.sortColumns, MailboxFileStore.dataColumns))
			{
				QueryFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Subject, fileSetId);
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter, SeekToConditionFlags.None))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ItemSchema.Subject, null);
						if (valueOrDefault == null || !StringComparer.OrdinalIgnoreCase.Equals(valueOrDefault, fileSetId))
						{
							break;
						}
						ExDateTime valueOrDefault2 = storePropertyBag.GetValueOrDefault<ExDateTime>(StoreObjectSchema.CreationTime, ExDateTime.MinValue);
						StoreId valueOrDefault3 = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
						this.tracer.TraceDebug((long)this.GetHashCode(), "Found item fileSetId='{0}' in folder '{1}'. CreationTime={2}, Id={3}", new object[]
						{
							fileSetId,
							this.folderName,
							valueOrDefault2,
							valueOrDefault3
						});
						list.Add(new FileSetItem(fileSetId, valueOrDefault3, valueOrDefault2));
					}
				}
			}
			return list;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0003629C File Offset: 0x0003449C
		internal Folder GetFolder(MailboxSession mailboxSession)
		{
			Folder folder = Folder.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration), StoreObjectType.Folder, this.folderName, CreateMode.OpenIfExists);
			if (folder.Id == null)
			{
				this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Found no folder '{0}' in mailbox", this.folderName);
				bool flag = false;
				try
				{
					folder.Save();
					folder.Load(new PropertyDefinition[]
					{
						FolderSchema.Id
					});
					flag = true;
					this.tracer.TraceDebug<string, VersionedId>((long)this.GetHashCode(), "Created new folder '{0}' in mailbox. Id: {1}", this.folderName, folder.Id);
					return folder;
				}
				finally
				{
					if (!flag)
					{
						folder.Dispose();
					}
				}
			}
			this.tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "Found existing folder '{0}' in mailbox. Id: {1}", folder.Id);
			return folder;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00036364 File Offset: 0x00034564
		private FileSetItem GetNewestItem(Folder folder, string fileSetId)
		{
			List<FileSetItem> allItems = this.GetAllItems(folder, fileSetId);
			FileSetItem fileSetItem = null;
			foreach (FileSetItem fileSetItem2 in allItems)
			{
				if (fileSetItem == null || fileSetItem2.Time > fileSetItem.Time)
				{
					fileSetItem = fileSetItem2;
				}
			}
			if (fileSetItem != null)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Found {0} items for fileSetId='{1}'in folder '{2}'. Most recent item is {3} from {4}", new object[]
				{
					allItems.Count,
					fileSetId,
					this.folderName,
					fileSetItem.Id,
					fileSetItem.Time
				});
			}
			return fileSetItem;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003642C File Offset: 0x0003462C
		private void UploadSingleFile(string fileName, IAttachmentCollection attachmentCollection, Action abortFileOperation)
		{
			abortFileOperation();
			string fileName2 = Path.GetFileName(fileName);
			using (IOCostStream iocostStream = new IOCostStream(new FileStream(fileName, FileMode.Open)))
			{
				using (new FileSystemPerformanceTracker("MailboxUpload", iocostStream, this.logger))
				{
					using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
					{
						using (IStreamAttachment streamAttachment = (IStreamAttachment)attachmentCollection.CreateIAttachment(AttachmentType.Stream))
						{
							using (new StorePerformanceTracker("MailboxUpload", this.logger))
							{
								CopyStreamResult arg;
								using (IOCostStream iocostStream2 = new IOCostStream(streamAttachment.GetContentStream()))
								{
									arg = MailboxFileStore.streamCopier.CopyStream(iocostStream, iocostStream2, hashAlgorithm, abortFileOperation);
									iocostStream2.Flush();
								}
								streamAttachment[AttachmentSchema.DisplayName] = fileName2;
								streamAttachment[AttachmentSchema.AttachLongFileName] = fileName2;
								streamAttachment[AttachmentSchema.AttachHash] = hashAlgorithm.Hash;
								streamAttachment.Save();
								this.tracer.TraceDebug<string, CopyStreamResult>((long)this.GetHashCode(), "Uploaded file '{0}' to mailbox. {1}", fileName, arg);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00036598 File Offset: 0x00034798
		private string DownloadSingleFile(IStreamAttachment attachment, string tempDirectory, bool ignoreDuplicateFiles, Action abortFileOperation)
		{
			abortFileOperation();
			string text = attachment[AttachmentSchema.AttachLongFileName] as string;
			if (string.IsNullOrWhiteSpace(text))
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Unable to get AttachLongFileName property from attachment");
				return null;
			}
			string text2 = Path.Combine(tempDirectory, text);
			if (ignoreDuplicateFiles && File.Exists(text2))
			{
				this.tracer.TraceError<string>((long)this.GetHashCode(), "File {0} already exists, so skipping download", text2);
				return null;
			}
			bool flag = false;
			try
			{
				this.ReadAttachmentIntoStreamAndValidateHash(attachment, new FileStream(text2, FileMode.CreateNew), abortFileOperation);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Download failed and temporary file '{0}' needs to be removed", text2);
					this.DeleteFile(text2);
				}
			}
			return text2;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00036658 File Offset: 0x00034858
		private void ReadAttachmentIntoStreamAndValidateHash(IStreamAttachment attachment, Stream outputStream, Action abortFileOperation)
		{
			using (IOCostStream iocostStream = new IOCostStream(outputStream))
			{
				using (new FileSystemPerformanceTracker("MailboxDownload", iocostStream, this.logger))
				{
					using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
					{
						using (new StorePerformanceTracker("MailboxDownload", this.logger))
						{
							CopyStreamResult arg;
							using (IOCostStream iocostStream2 = new IOCostStream(attachment.GetContentStream()))
							{
								using (new FileSystemPerformanceTracker("MailboxDownload", iocostStream2, this.logger))
								{
									arg = MailboxFileStore.streamCopier.CopyStream(iocostStream2, iocostStream, hashAlgorithm, abortFileOperation);
								}
							}
							this.tracer.TraceDebug<CopyStreamResult>((long)this.GetHashCode(), "Downloaded file attachment from mailbox. {0}", arg);
							byte[] array = attachment[AttachmentSchema.AttachHash] as byte[];
							this.tracer.TraceDebug<ArrayTracer<byte>>((long)this.GetHashCode(), "Expected hash in attachment downloaded from mailbox is: {0}.", new ArrayTracer<byte>(array));
							if (!ArrayComparer<byte>.Comparer.Equals(hashAlgorithm.Hash, array))
							{
								throw new InvalidDataException(string.Format("Hash of data content doesn't match hash of original uploaded file. Expected: {0}, actual: {1}.", BitConverter.ToString(array), BitConverter.ToString(hashAlgorithm.Hash)));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00036831 File Offset: 0x00034A31
		private void DeleteFolder(string folderName)
		{
			this.HandleDeleteExceptions(folderName, delegate(string name)
			{
				Directory.Delete(name, true);
			});
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00036857 File Offset: 0x00034A57
		private void DeleteFile(string fileName)
		{
			this.HandleDeleteExceptions(fileName, new Action<string>(File.Delete));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003686C File Offset: 0x00034A6C
		private void HandleDeleteExceptions(string name, Action<string> deleteAction)
		{
			Exception ex = null;
			try
			{
				deleteAction(name);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				this.tracer.TraceError<string, Exception>((long)this.GetHashCode(), "Unable to delete '{0}' due exception: {1}", name, ex);
			}
		}

		// Token: 0x040006E7 RID: 1767
		private const int queryBatchSize = 100;

		// Token: 0x040006E8 RID: 1768
		private const string messageClass = "IPM.FileSet";

		// Token: 0x040006E9 RID: 1769
		internal const string MailboxUploadMarker = "MailboxUpload";

		// Token: 0x040006EA RID: 1770
		internal const string MailboxDownloadMarker = "MailboxDownload";

		// Token: 0x040006EB RID: 1771
		private static readonly StreamCopier streamCopier = new StreamCopier(8192);

		// Token: 0x040006EC RID: 1772
		private static readonly SortBy[] sortColumns = new SortBy[]
		{
			new SortBy(ItemSchema.Subject, SortOrder.Ascending)
		};

		// Token: 0x040006ED RID: 1773
		private static readonly PropertyDefinition[] dataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Subject,
			StoreObjectSchema.CreationTime
		};

		// Token: 0x040006EE RID: 1774
		private readonly string folderName;

		// Token: 0x040006EF RID: 1775
		private readonly IPerformanceDataLogger logger;

		// Token: 0x040006F0 RID: 1776
		private readonly ITracer tracer;
	}
}
