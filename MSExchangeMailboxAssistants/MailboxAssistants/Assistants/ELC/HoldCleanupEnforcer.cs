using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200009B RID: 155
	internal class HoldCleanupEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		internal HoldCleanupEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
			object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.HoldCleanupBatchSizeForELC);
			if (obj is int)
			{
				this.batchSizeForELC = (int)obj;
			}
			obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.HoldCleanupEnabledForELC);
			if (obj is string && !bool.TryParse((string)obj, out this.enabledForELC))
			{
				this.enabledForELC = true;
				HoldCleanupEnforcer.Tracer.TraceWarning<HoldCleanupEnforcer, string>((long)this.GetHashCode(), "{0}: {1} override provided but the value is in the wrong format.  Defaulting to true.", this, ElcGlobals.HoldCleanupEnabledForELC);
			}
			obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, ElcGlobals.HoldCleanupLogOnly);
			if (obj is string && !bool.TryParse((string)obj, out this.logOnly))
			{
				this.logOnly = false;
				HoldCleanupEnforcer.Tracer.TraceWarning<HoldCleanupEnforcer, string>((long)this.GetHashCode(), "{0}: {1} override provided but the value is in the wrong format.  Defaulting to false.", this, ElcGlobals.HoldCleanupLogOnly);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002DAC1 File Offset: 0x0002BCC1
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by HoldCleanupEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0002DAFB File Offset: 0x0002BCFB
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			base.MailboxDataForTags.StatisticsLogEntry.HoldCleanupEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002DB10 File Offset: 0x0002BD10
		protected override bool QueryIsEnabled()
		{
			if ((base.MailboxDataForTags.StatisticsLogEntry.IsOnDemandJob || this.enabledForELC) && base.MailboxDataForTags.HoldEnabled)
			{
				HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold. This user will be checked.", this);
				return true;
			}
			HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. This user will be skipped.", this);
			return false;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002DB80 File Offset: 0x0002BD80
		protected override void CollectItemsToExpire()
		{
			int num = 0;
			if (base.MailboxDataForTags.HoldCleanupFolderType != DefaultFolderType.None)
			{
				HoldCleanupEnforcer.Tracer.TraceInformation<HoldCleanupEnforcer, DefaultFolderType, string>(32992, (long)this.GetHashCode(), "{0}: Resuming HoldCleanup at folder {1} and InternetMessageId {2}", this, base.MailboxDataForTags.HoldCleanupFolderType, base.MailboxDataForTags.HoldCleanupInternetMessageId);
				num = Array.IndexOf<DefaultFolderType>(HoldCleanupEnforcer.DumpsterFolders, base.MailboxDataForTags.HoldCleanupFolderType);
			}
			int num2 = num;
			try
			{
				for (int i = num; i < HoldCleanupEnforcer.DumpsterFolders.Length; i++)
				{
					if (!this.CollectItemsInFolder(HoldCleanupEnforcer.DumpsterFolders[i]))
					{
						HoldCleanupEnforcer.Tracer.TraceWarning<HoldCleanupEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: CollectItemsToExpire did not complete for folderType {1}.", this, HoldCleanupEnforcer.DumpsterFolders[i]);
						break;
					}
					num2++;
				}
			}
			finally
			{
				StringBuilder stringBuilder = new StringBuilder();
				IOrderedEnumerable<KeyValuePair<string, long>> source = from dirtyPropertyPair in this.dirtyPropertyCount
				orderby dirtyPropertyPair.Value descending
				select dirtyPropertyPair;
				foreach (KeyValuePair<string, long> keyValuePair in source.Take(10))
				{
					stringBuilder.AppendFormat("{0}:  {1}", keyValuePair.Key, keyValuePair.Value);
					stringBuilder.AppendLine();
				}
				Globals.Logger.LogEvent(base.MailboxDataForTags.ElcUserTagInformation.ADUser.OrganizationId, InfoWorkerEventLogConstants.Tuple_HoldCleanupStatistics, null, new object[]
				{
					base.MailboxDataForTags.MailboxSession.MailboxOwner,
					base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsAnalyzedByHoldCleanupEnforcer,
					base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeterminedDuplicateByHoldCleanupEnforcer,
					base.MailboxDataForTags.StatisticsLogEntry.SizeOfItemsDeterminedDuplicateByHoldCleanupEnforcer,
					base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsSkippedByHoldCleanupEnforcer,
					num2 == HoldCleanupEnforcer.DumpsterFolders.Length,
					stringBuilder
				});
			}
			if (num2 == HoldCleanupEnforcer.DumpsterFolders.Length)
			{
				Exception arg = null;
				if (!ElcMailboxHelper.ClearHoldCleanupWatermarkFAIMessage(base.MailboxDataForTags.MailboxSession.ClientInfoString, base.MailboxDataForTags.MailboxSession.MailboxOwner, out arg))
				{
					HoldCleanupEnforcer.Tracer.TraceError<HoldCleanupEnforcer, Exception>((long)this.GetHashCode(), "{0}: Unable to clear the watermark due to {1}.", this, arg);
				}
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002DDFC File Offset: 0x0002BFFC
		private bool CollectItemsInFolder(DefaultFolderType folderToCollect)
		{
			HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: CollectItemsInFolder: folderToCollect={1}.", this, folderToCollect);
			StoreObjectId defaultFolderId = base.MailboxDataForTags.MailboxSession.GetDefaultFolderId(folderToCollect);
			if (defaultFolderId == null)
			{
				HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer, DefaultFolderType>((long)this.GetHashCode(), "{0}: {1} folder is null", this, folderToCollect);
				return true;
			}
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, defaultFolderId))
			{
				int num = base.FolderItemTypeCount(folder, ItemQueryType.None);
				if (num <= 0)
				{
					HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer, DefaultFolderType, string>((long)this.GetHashCode(), "{0}: {1} [{2}] folder is empty", this, folderToCollect, defaultFolderId.ToHexEntryId());
					return true;
				}
				QueryFilter queryFilter = null;
				if (base.MailboxDataForTags.HoldCleanupFolderType == folderToCollect && !string.IsNullOrEmpty(base.MailboxDataForTags.HoldCleanupInternetMessageId))
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.InternetMessageId, base.MailboxDataForTags.HoldCleanupInternetMessageId);
				}
				else
				{
					this.SetWatermark(folderToCollect, string.Empty);
				}
				base.SysCleanupSubAssistant.ThrottleStoreCall();
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, HoldCleanupEnforcer.Sort, HoldCleanupEnforcer.PropertyColumns))
				{
					StoreId storeId = null;
					string text = null;
					Item item = null;
					bool flag = false;
					ICollection<PropertyDefinition> collection = null;
					try
					{
						if (queryFilter != null)
						{
							queryResult.SeekToCondition(SeekReference.OriginBeginning, queryFilter);
						}
						object[][] rows;
						while ((rows = queryResult.GetRows(100)).Length > 0)
						{
							foreach (object[] array2 in rows)
							{
								VersionedId value = array2.GetValue(HoldCleanupEnforcer.PropertyIndex.ItemId);
								string text2 = (array2[3] is string) ? array2.GetValue(HoldCleanupEnforcer.PropertyIndex.ItemClass) : null;
								string text3 = (array2[2] is string) ? array2.GetValue(HoldCleanupEnforcer.PropertyIndex.InternetMessageId) : null;
								int value2 = array2.GetValue(HoldCleanupEnforcer.PropertyIndex.Size);
								if (string.IsNullOrEmpty(text3))
								{
									HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer, VersionedId, string>((long)this.GetHashCode(), "{0}: Skipping item {1} of class {2} due to missing InternetMessageId", this, value, text2);
									base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsSkippedByHoldCleanupEnforcer += 1L;
								}
								else
								{
									if (text3 == text)
									{
										if (collection == null)
										{
											if (string.IsNullOrEmpty(text2))
											{
												HoldCleanupEnforcer.Tracer.TraceDebug<HoldCleanupEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: Skipping item {1} due to missing ItemClass", this, value);
												base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsSkippedByHoldCleanupEnforcer += 1L;
												goto IL_349;
											}
											collection = this.GetLegalTrackingProperties(text2);
										}
										base.SysCleanupSubAssistant.ThrottleStoreCall();
										if (item == null)
										{
											item = Item.Bind(base.MailboxDataForTags.MailboxSession, storeId, collection);
										}
										using (Item item2 = Item.Bind(base.MailboxDataForTags.MailboxSession, value, collection))
										{
											if (this.AreItemsLegallyEqual(collection, item, item2))
											{
												HoldCleanupEnforcer.Tracer.TraceInformation(49376, (long)this.GetHashCode(), "{0}: Item {1} of class {2} is a duplicate of {3}, it will be deleted.", new object[]
												{
													this,
													value,
													text2,
													storeId
												});
												base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeterminedDuplicateByHoldCleanupEnforcer += 1L;
												base.MailboxDataForTags.StatisticsLogEntry.SizeOfItemsDeterminedDuplicateByHoldCleanupEnforcer += (long)value2;
												if (!this.logOnly)
												{
													base.TagExpirationExecutor.AddToDoomedHardDeleteList(new ItemData(value, value2), true);
												}
											}
											else
											{
												storeId = value;
												item.Dispose();
												item = null;
											}
											goto IL_330;
										}
										goto IL_310;
									}
									goto IL_310;
									IL_330:
									base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsAnalyzedByHoldCleanupEnforcer += 1L;
									goto IL_349;
									IL_310:
									text = text3;
									storeId = value;
									flag = true;
									if (item != null)
									{
										item.Dispose();
										item = null;
									}
									if (collection != null)
									{
										collection = null;
										goto IL_330;
									}
									goto IL_330;
								}
								IL_349:;
							}
							if (!base.MailboxDataForTags.StatisticsLogEntry.IsOnDemandJob && base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsAnalyzedByHoldCleanupEnforcer >= (long)this.batchSizeForELC)
							{
								this.SetWatermark(folderToCollect, text);
								return false;
							}
							if (flag && !string.IsNullOrEmpty(text))
							{
								this.SetWatermark(folderToCollect, text);
								flag = false;
							}
						}
					}
					finally
					{
						if (item != null)
						{
							item.Dispose();
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0002E25C File Offset: 0x0002C45C
		private bool AreItemsLegallyEqual(string itemClass, Item originalItem, Item currentItem)
		{
			ICollection<PropertyDefinition> legalTrackingProperties = this.GetLegalTrackingProperties(itemClass);
			originalItem.Load(legalTrackingProperties);
			currentItem.Load(legalTrackingProperties);
			return this.AreItemsLegallyEqual(legalTrackingProperties, originalItem, currentItem);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002E2C8 File Offset: 0x0002C4C8
		private bool AreItemsLegallyEqual(ICollection<PropertyDefinition> legalTrackingProperties, Item originalItem, Item currentItem)
		{
			using (IEnumerator<PropertyDefinition> enumerator = legalTrackingProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyDefinition legalTrackingProperty = enumerator.Current;
					object value = originalItem.GetValue(legalTrackingProperty);
					object value2 = currentItem.GetValue(legalTrackingProperty);
					if (value != null && value2 != null)
					{
						if (!PropertyError.IsPropertyNotFound(value) || !PropertyError.IsPropertyNotFound(value2))
						{
							if (PropertyError.IsPropertyValueTooBig(value) && PropertyError.IsPropertyValueTooBig(value2))
							{
								if (!this.CompareStreams(() => originalItem.GetStream(legalTrackingProperty), () => currentItem.GetStream(legalTrackingProperty)))
								{
									this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, legalTrackingProperty);
									return false;
								}
							}
							else if (!PropertyError.IsPropertyError(value) && !PropertyError.IsPropertyError(value2))
							{
								if (legalTrackingProperty.Type.IsArray)
								{
									if (!((Array)value).Compare((Array)value2))
									{
										this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, legalTrackingProperty);
										return false;
									}
								}
								else if (!value.Equals(value2))
								{
									this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, legalTrackingProperty);
									return false;
								}
							}
						}
					}
					else if (value != null || value2 != null)
					{
						this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, legalTrackingProperty);
						return false;
					}
				}
			}
			if (originalItem is MessageItem && !this.CompareRecipients(((MessageItem)originalItem).Recipients, ((MessageItem)currentItem).Recipients))
			{
				this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, "Recipients");
				return false;
			}
			if (!this.CompareAttachments(originalItem.AttachmentCollection, currentItem.AttachmentCollection))
			{
				this.LogDirtyProperty(currentItem.Id, originalItem.Id, originalItem.ClassName, "Attachments");
				return false;
			}
			return true;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002E5C8 File Offset: 0x0002C7C8
		private void SetWatermark(DefaultFolderType folder, string internetMessageId)
		{
			Exception arg = null;
			if (!ElcMailboxHelper.UpdateHoldCleanupWatermarkFAIMessage(folder, internetMessageId, base.MailboxDataForTags.MailboxSession.ClientInfoString, base.MailboxDataForTags.MailboxSession.MailboxOwner, out arg))
			{
				HoldCleanupEnforcer.Tracer.TraceError<HoldCleanupEnforcer, Exception>((long)this.GetHashCode(), "{0}: Unable to save the watermark due to {1}.", this, arg);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002E61A File Offset: 0x0002C81A
		private void LogDirtyProperty(StoreId currentId, StoreId originalId, string itemClass, PropertyDefinition property)
		{
			this.LogDirtyProperty(currentId, originalId, itemClass, property.Name);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002E62C File Offset: 0x0002C82C
		private void LogDirtyProperty(StoreId currentId, StoreId originalId, string itemClass, string propertyName)
		{
			HoldCleanupEnforcer.Tracer.TraceInformation(57568, (long)this.GetHashCode(), "{0}: Item {1} of class {2} compared to {3} is legally modified due to property {4}", new object[]
			{
				this,
				currentId,
				itemClass,
				originalId,
				propertyName
			});
			long num;
			if (this.dirtyPropertyCount.TryGetValue(propertyName, out num))
			{
				num += 1L;
			}
			else
			{
				num = 1L;
			}
			this.dirtyPropertyCount[propertyName] = num;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		private ICollection<PropertyDefinition> GetLegalTrackingProperties(string itemClass)
		{
			ICollection<PropertyDefinition> collection = null;
			if (!this.legalTrackingPropertyMap.TryGetValue(itemClass, out collection))
			{
				Schema schema = ObjectClass.GetSchema(itemClass);
				collection = (from x in schema.LegalTrackingProperties
				select x into x
				where !HoldCleanupEnforcer.IgnoredLegalTrackingProperties.Contains(x)
				select x).ToList<PropertyDefinition>();
				this.legalTrackingPropertyMap.Add(itemClass, collection);
			}
			return collection;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002E734 File Offset: 0x0002C934
		private bool CompareStreams(Func<Stream> getOriginalItemStream, Func<Stream> getCurrentItemStream)
		{
			byte[] array = new byte[1024];
			byte[] array2 = new byte[1024];
			int num = -1;
			int num2 = -1;
			bool result;
			using (Stream stream = getOriginalItemStream())
			{
				using (Stream stream2 = getCurrentItemStream())
				{
					if (stream == null && stream2 == null)
					{
						result = true;
					}
					else if (stream == null || stream2 == null)
					{
						result = false;
					}
					else
					{
						try
						{
							num = stream.Read(array, 0, array.Length);
						}
						catch (ObjectNotFoundException)
						{
						}
						try
						{
							num2 = stream2.Read(array2, 0, array2.Length);
						}
						catch (ObjectNotFoundException)
						{
						}
						if (num2 == -1 && num == -1)
						{
							result = true;
						}
						else
						{
							while (num == num2)
							{
								if (num == 0 && num2 == 0)
								{
									return true;
								}
								if (!array.Compare(array2, num))
								{
									return false;
								}
								num = stream.Read(array, 0, array.Length);
								num2 = stream2.Read(array2, 0, array2.Length);
							}
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002E84C File Offset: 0x0002CA4C
		private bool CompareRecipients(RecipientCollection originalRecipients, RecipientCollection currentRecipients)
		{
			if (originalRecipients != null && currentRecipients != null)
			{
				if (originalRecipients.Count != currentRecipients.Count)
				{
					return false;
				}
				using (IEnumerator<Recipient> enumerator = originalRecipients.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Recipient value = enumerator.Current;
						if (!currentRecipients.Contains(value, RecipientEqualityComparer.Default))
						{
							return false;
						}
					}
					return true;
				}
			}
			if (originalRecipients != null || currentRecipients != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		private bool CompareAttachments(AttachmentCollection originalAttachments, AttachmentCollection currentAttachments)
		{
			if (originalAttachments != null && currentAttachments != null)
			{
				if (originalAttachments.Count != currentAttachments.Count)
				{
					return false;
				}
				IList<AttachmentHandle> handles = originalAttachments.GetHandles();
				IList<AttachmentHandle> handles2 = currentAttachments.GetHandles();
				for (int i = 0; i < handles.Count; i++)
				{
					using (Attachment originalAttachment = originalAttachments.Open(handles[i]))
					{
						using (Attachment currentAttachment = currentAttachments.Open(handles2[i]))
						{
							originalAttachment.Load();
							currentAttachment.Load();
							if (originalAttachment.AttachmentType != currentAttachment.AttachmentType)
							{
								return false;
							}
							if (originalAttachment.DisplayName != currentAttachment.DisplayName)
							{
								return false;
							}
							if (originalAttachment.FileExtension != currentAttachment.FileExtension)
							{
								return false;
							}
							if (originalAttachment.FileName != currentAttachment.FileName)
							{
								return false;
							}
							if (originalAttachment.Size != currentAttachment.Size)
							{
								return false;
							}
							if (originalAttachment is StreamAttachmentBase && currentAttachment is StreamAttachmentBase)
							{
								if (!this.CompareStreams(() => ((StreamAttachmentBase)originalAttachment).TryGetContentStream(PropertyOpenMode.ReadOnly), () => ((StreamAttachmentBase)currentAttachment).TryGetContentStream(PropertyOpenMode.ReadOnly)))
								{
									return false;
								}
							}
							else if (originalAttachment is ItemAttachment)
							{
								using (Item itemAsReadOnly = ((ItemAttachment)originalAttachment).GetItemAsReadOnly(HoldCleanupEnforcer.PropertyColumns))
								{
									using (Item itemAsReadOnly2 = ((ItemAttachment)currentAttachment).GetItemAsReadOnly(HoldCleanupEnforcer.PropertyColumns))
									{
										if (!this.AreItemsLegallyEqual(itemAsReadOnly.ClassName, itemAsReadOnly, itemAsReadOnly2))
										{
											return false;
										}
									}
								}
							}
						}
					}
				}
			}
			else if (originalAttachments != null || currentAttachments != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000472 RID: 1138
		private static readonly Trace Tracer = ExTraceGlobals.HoldCleanupEnforcerTracer;

		// Token: 0x04000473 RID: 1139
		private static readonly DefaultFolderType[] DumpsterFolders = new DefaultFolderType[]
		{
			DefaultFolderType.RecoverableItemsVersions,
			DefaultFolderType.RecoverableItemsDiscoveryHolds
		};

		// Token: 0x04000474 RID: 1140
		private static readonly List<PropertyDefinition> PropertyColumns = new List<PropertyDefinition>
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.InternetMessageId,
			StoreObjectSchema.ItemClass,
			ItemSchema.Size
		};

		// Token: 0x04000475 RID: 1141
		private static readonly SortBy[] Sort = new SortBy[]
		{
			new SortBy(ItemSchema.InternetMessageId, SortOrder.Ascending),
			new SortBy(StoreObjectSchema.CreationTime, SortOrder.Ascending)
		};

		// Token: 0x04000476 RID: 1142
		private static readonly PropertyDefinition[] IgnoredLegalTrackingProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.CreationTime,
			StoreObjectSchema.EntryId
		};

		// Token: 0x04000477 RID: 1143
		private readonly bool enabledForELC = true;

		// Token: 0x04000478 RID: 1144
		private readonly bool logOnly;

		// Token: 0x04000479 RID: 1145
		private readonly int batchSizeForELC = 2000;

		// Token: 0x0400047A RID: 1146
		private string toString;

		// Token: 0x0400047B RID: 1147
		private Dictionary<string, ICollection<PropertyDefinition>> legalTrackingPropertyMap = new Dictionary<string, ICollection<PropertyDefinition>>();

		// Token: 0x0400047C RID: 1148
		private Dictionary<string, long> dirtyPropertyCount = new Dictionary<string, long>();

		// Token: 0x0200009C RID: 156
		internal enum PropertyIndex
		{
			// Token: 0x04000481 RID: 1153
			ItemId,
			// Token: 0x04000482 RID: 1154
			ParentUniqueItemId,
			// Token: 0x04000483 RID: 1155
			InternetMessageId,
			// Token: 0x04000484 RID: 1156
			ItemClass,
			// Token: 0x04000485 RID: 1157
			Size
		}
	}
}
