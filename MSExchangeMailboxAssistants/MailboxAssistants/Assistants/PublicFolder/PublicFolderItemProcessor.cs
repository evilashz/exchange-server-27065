using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderItemProcessor : PublicFolderProcessor
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x000589BC File Offset: 0x00056BBC
		public PublicFolderItemProcessor(PublicFolderSession publicFolderSession, Trace tracer) : base(publicFolderSession, tracer)
		{
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000589C8 File Offset: 0x00056BC8
		private Organization Organization
		{
			get
			{
				if (this.organization == null)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderSession.OrganizationId ?? OrganizationId.ForestWideOrgId), 72, "Organization", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\PublicFolder\\PublicFolderItemProcessor.cs");
					this.organization = tenantOrTopologyConfigurationSession.GetOrgContainer();
				}
				return this.organization;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00058A20 File Offset: 0x00056C20
		public override void Invoke()
		{
			PublicFolderAssistantLogger publicFolderAssistantLogger = null;
			try
			{
				publicFolderAssistantLogger = new PublicFolderAssistantLogger(this.publicFolderSession);
				using (Folder folder = PublicFolderItemProcessor.xsoFactory.BindToFolder(this.publicFolderSession, this.publicFolderSession.GetPublicFolderRootId()) as Folder)
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal | FolderQueryFlags.NoNotifications, null, null, PublicFolderItemProcessor.FolderRec.PropertiesToLoad))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(100);
							if (rows.Length <= 0)
							{
								break;
							}
							foreach (object[] properties in rows)
							{
								PublicFolderItemProcessor.FolderRec folderRec = new PublicFolderItemProcessor.FolderRec(this.publicFolderSession, properties);
								if (folderRec.FolderId != null && !folderRec.FolderId.Equals(this.publicFolderSession.GetTombstonesRootFolderId()))
								{
									if ((folderRec.FolderId.Equals(this.publicFolderSession.GetAsyncDeleteStateFolderId()) || folderRec.IsDumpsterFolder) && folderRec.SubfolderCount > 0 && this.publicFolderSession.IsPrimaryHierarchySession)
									{
										int? retentionAgeLimit = folderRec.RetentionAgeLimit;
										if (retentionAgeLimit == null && this.Organization.DefaultPublicFolderDeletedItemRetention != null)
										{
											retentionAgeLimit = new int?((int)this.Organization.DefaultPublicFolderDeletedItemRetention.Value.TotalSeconds);
										}
										if (retentionAgeLimit != null)
										{
											this.HardDeleteSubFolders(folderRec, this.now.Subtract(EnhancedTimeSpan.FromSeconds((double)retentionAgeLimit.Value)), publicFolderAssistantLogger);
										}
									}
									if (folderRec.TotalItemCount != 0)
									{
										if (folderRec.ContentMailboxGuid != this.publicFolderSession.MailboxGuid)
										{
											ExDateTime? lastMovedTimeStamp = folderRec.LastMovedTimeStamp;
											if (lastMovedTimeStamp != null)
											{
												int? num = new int?((int)this.Organization.DefaultPublicFolderMovedItemRetention.Value.TotalSeconds);
												double totalSeconds = this.now.Subtract(lastMovedTimeStamp.Value).TotalSeconds;
												int? num2 = num;
												if (totalSeconds >= (double)num2.GetValueOrDefault() && num2 != null)
												{
													this.HardDeleteItems(folderRec, ExDateTime.MaxValue, publicFolderAssistantLogger, "PublicFolderContentsFromMove");
												}
											}
										}
										else if (folderRec.IsDumpsterFolder)
										{
											int? retentionAgeLimit2 = folderRec.RetentionAgeLimit;
											if (retentionAgeLimit2 == null && this.Organization.DefaultPublicFolderDeletedItemRetention != null)
											{
												retentionAgeLimit2 = new int?((int)this.Organization.DefaultPublicFolderDeletedItemRetention.Value.TotalSeconds);
											}
											if (retentionAgeLimit2 != null)
											{
												this.HardDeleteItems(folderRec, this.now.Subtract(EnhancedTimeSpan.FromSeconds((double)retentionAgeLimit2.Value)), publicFolderAssistantLogger, "PublicFolderDeletedItemExpiration");
											}
										}
										else
										{
											int? overallAgeLimit = folderRec.OverallAgeLimit;
											if (overallAgeLimit == null && this.Organization.DefaultPublicFolderAgeLimit != null)
											{
												overallAgeLimit = new int?((int)this.Organization.DefaultPublicFolderAgeLimit.Value.TotalSeconds);
											}
											if (overallAgeLimit != null && folderRec.DumpsterId != null)
											{
												this.SoftDeleteItems(folderRec, this.now.Subtract(EnhancedTimeSpan.FromSeconds((double)overallAgeLimit.Value)), publicFolderAssistantLogger);
											}
										}
									}
								}
							}
						}
					}
				}
				publicFolderAssistantLogger.TrySave();
			}
			catch (StoragePermanentException ex)
			{
				if (publicFolderAssistantLogger != null)
				{
					publicFolderAssistantLogger.ReportError("Error occurred while processing items", ex);
					publicFolderAssistantLogger.TrySave();
				}
				else
				{
					PublicFolderAssistantLogger.LogOnServer(ex);
				}
			}
			catch (StorageTransientException ex2)
			{
				if (publicFolderAssistantLogger != null)
				{
					publicFolderAssistantLogger.ReportError("Error occurred while processing items", ex2);
					publicFolderAssistantLogger.TrySave();
				}
				else
				{
					PublicFolderAssistantLogger.LogOnServer(ex2);
				}
			}
			catch (Exception ex3)
			{
				if (publicFolderAssistantLogger != null)
				{
					publicFolderAssistantLogger.ReportError("Error occurred while processing items", ex3);
					publicFolderAssistantLogger.TrySave();
				}
				else
				{
					PublicFolderAssistantLogger.LogOnServer(ex3);
				}
				throw;
			}
			finally
			{
				if (publicFolderAssistantLogger != null)
				{
					publicFolderAssistantLogger.Dispose();
					publicFolderAssistantLogger = null;
				}
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000590C0 File Offset: 0x000572C0
		private void SoftDeleteItems(PublicFolderItemProcessor.FolderRec folderRec, ExDateTime expiration, PublicFolderAssistantLogger assistantLogger)
		{
			PublicFolderItemProcessor.CatchAndLogStorageExceptions(assistantLogger, folderRec, "SoftDeleteItems", delegate
			{
				using (Folder folder = PublicFolderItemProcessor.xsoFactory.BindToFolder(this.publicFolderSession, folderRec.FolderId) as Folder)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
					{
						new SortBy(CoreItemSchema.ReceivedTime, SortOrder.Descending),
						new SortBy(CoreObjectSchema.CreationTime, SortOrder.Descending)
					}, PublicFolderItemProcessor.MessageRec.PropertiesToLoad))
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, CoreItemSchema.ReceivedTime, expiration);
						if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
						{
							for (;;)
							{
								object[][] rows = queryResult.GetRows(100);
								if (rows.Length <= 0)
								{
									break;
								}
								List<StoreObjectId> list = null;
								foreach (object[] properties in rows)
								{
									PublicFolderItemProcessor.MessageRec messageRec = new PublicFolderItemProcessor.MessageRec(properties);
									ExDateTime? exDateTime = messageRec.ReceivedTime;
									if (exDateTime == null)
									{
										exDateTime = ((messageRec.CreationTime != null) ? messageRec.CreationTime : new ExDateTime?(ExDateTime.MinValue));
									}
									if (exDateTime < expiration)
									{
										if (list == null)
										{
											list = new List<StoreObjectId>(rows.Length);
										}
										list.Add(messageRec.ItemId.ObjectId);
									}
								}
								if (list != null)
								{
									folder.MoveItems(folderRec.DumpsterId, list.ToArray());
									assistantLogger.LogEvent(LogEventType.Success, string.Format(CultureInfo.InvariantCulture, "SoftDeleteItems. {0}. {1}", new object[]
									{
										folderRec.FolderId.ToHexEntryId(),
										list.Count
									}));
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00059338 File Offset: 0x00057538
		private void HardDeleteItems(PublicFolderItemProcessor.FolderRec folderRec, ExDateTime expiration, PublicFolderAssistantLogger assistantLogger, string operation)
		{
			PublicFolderItemProcessor.CatchAndLogStorageExceptions(assistantLogger, folderRec, operation, delegate
			{
				using (Folder folder = PublicFolderItemProcessor.xsoFactory.BindToFolder(this.publicFolderSession, folderRec.FolderId) as Folder)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
					{
						new SortBy(CoreObjectSchema.LastModifiedTime, SortOrder.Descending)
					}, PublicFolderItemProcessor.MessageRec.PropertiesToLoad))
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, CoreObjectSchema.LastModifiedTime, expiration);
						if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
						{
							for (;;)
							{
								object[][] rows = queryResult.GetRows(100);
								if (rows.Length <= 0)
								{
									break;
								}
								List<StoreObjectId> list = null;
								foreach (object[] properties in rows)
								{
									PublicFolderItemProcessor.MessageRec messageRec = new PublicFolderItemProcessor.MessageRec(properties);
									ExDateTime? exDateTime = (messageRec.LastModifiedTime != null) ? messageRec.LastModifiedTime : messageRec.ReceivedTime;
									if (exDateTime == null)
									{
										exDateTime = ((messageRec.CreationTime != null) ? messageRec.CreationTime : new ExDateTime?(ExDateTime.MinValue));
									}
									if (exDateTime < expiration)
									{
										if (list == null)
										{
											list = new List<StoreObjectId>(rows.Length);
										}
										list.Add(messageRec.ItemId.ObjectId);
									}
								}
								if (list != null)
								{
									folder.DeleteObjects(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, list.ToArray());
									assistantLogger.LogEvent(LogEventType.Success, string.Format(CultureInfo.InvariantCulture, "HardDeleteItems. {0}. {1}. {2}", new object[]
									{
										folderRec.FolderId.ToHexEntryId(),
										list.Count,
										operation
									}));
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00059524 File Offset: 0x00057724
		private void HardDeleteSubFolders(PublicFolderItemProcessor.FolderRec folderRec, ExDateTime expiration, PublicFolderAssistantLogger assistantLogger)
		{
			PublicFolderItemProcessor.CatchAndLogStorageExceptions(assistantLogger, folderRec, "HardDeleteSubFolders", delegate
			{
				using (Folder folder = PublicFolderItemProcessor.xsoFactory.BindToFolder(this.publicFolderSession, folderRec.FolderId) as Folder)
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.NoNotifications, null, null, PublicFolderItemProcessor.FolderRec.PropertiesToLoad))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(100);
							if (rows == null || rows.Length == 0)
							{
								break;
							}
							List<StoreObjectId> list = null;
							foreach (object[] properties in rows)
							{
								PublicFolderItemProcessor.FolderRec folderRec2 = new PublicFolderItemProcessor.FolderRec(this.publicFolderSession, properties);
								if (folderRec2.FolderId != null && folderRec2.LastModifiedTime < expiration)
								{
									if (list == null)
									{
										list = new List<StoreObjectId>(rows.Length);
									}
									list.Add(folderRec2.FolderId);
								}
							}
							if (list != null)
							{
								folder.DeleteObjects(DeleteItemFlags.HardDelete, list.ToArray());
								assistantLogger.LogEvent(LogEventType.Success, string.Format(CultureInfo.InvariantCulture, "HardDeleteSubFolders. {0}. {1}", new object[]
								{
									folderRec.FolderId.ToHexEntryId(),
									list.Count
								}));
							}
						}
					}
				}
			});
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00059578 File Offset: 0x00057778
		private static void CatchAndLogStorageExceptions(PublicFolderAssistantLogger assistantLogger, PublicFolderItemProcessor.FolderRec folderRec, string context, Action actionDelegate)
		{
			Exception ex = null;
			try
			{
				actionDelegate();
			}
			catch (StoragePermanentException ex2)
			{
				ex = ex2;
			}
			catch (StorageTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				assistantLogger.LogEvent(LogEventType.Error, string.Format(CultureInfo.InvariantCulture, "[ErrorContext:{0}.{1}] {2}", new object[]
				{
					context,
					folderRec.FolderId.ToHexEntryId(),
					PublicFolderMailboxLoggerBase.GetExceptionLogString(ex, PublicFolderMailboxLoggerBase.ExceptionLogOption.All)
				}));
			}
		}

		// Token: 0x04000961 RID: 2401
		private const int QueryResultBatchSize = 100;

		// Token: 0x04000962 RID: 2402
		private static IXSOFactory xsoFactory = new XSOFactory();

		// Token: 0x04000963 RID: 2403
		private Organization organization;

		// Token: 0x02000177 RID: 375
		private class FolderRec
		{
			// Token: 0x06000EF7 RID: 3831 RVA: 0x00059600 File Offset: 0x00057800
			public FolderRec(IPublicFolderSession publicFolderSession, object[] properties)
			{
				for (int i = 0; i < PublicFolderItemProcessor.FolderRec.PropertiesToLoad.Length; i++)
				{
					if (!(properties[i] is PropertyError))
					{
						switch (i)
						{
						case 0:
						{
							VersionedId versionedId = properties[i] as VersionedId;
							if (versionedId != null)
							{
								this.FolderId = versionedId.ObjectId;
							}
							break;
						}
						case 1:
						{
							string[] array = properties[i] as string[];
							if (array != null && array.Length > 0)
							{
								GuidHelper.TryParseGuid(array[0], out this.ContentMailboxGuid);
							}
							break;
						}
						case 3:
							this.OverallAgeLimit = (properties[i] as int?);
							break;
						case 4:
							this.RetentionAgeLimit = (properties[i] as int?);
							break;
						case 5:
						{
							byte[] array2 = properties[i] as byte[];
							if (array2 != null)
							{
								if (array2.Length == 22)
								{
									this.DumpsterId = publicFolderSession.IdConverter.CreateFolderId(publicFolderSession.IdConverter.GetIdFromLongTermId(array2));
								}
								if (array2.Length == 46)
								{
									this.DumpsterId = StoreObjectId.FromProviderSpecificId(array2, StoreObjectType.Folder);
								}
							}
							break;
						}
						case 6:
							this.ItemCount = (properties[i] as int?);
							break;
						case 7:
							this.AssociatedItemCount = (properties[i] as int?);
							break;
						case 8:
							this.SubfolderCount = (properties[i] as int?);
							break;
						case 9:
						{
							byte[] array3 = properties[i] as byte[];
							if (array3 != null && array3.Length > 0)
							{
								this.ParentId = StoreObjectId.FromProviderSpecificId(array3, StoreObjectType.Folder);
							}
							break;
						}
						case 10:
							this.LastModifiedTime = (properties[i] as ExDateTime?);
							break;
						case 11:
						{
							ELCFolderFlags elcfolderFlags = (ELCFolderFlags)properties[i];
							this.IsDumpsterFolder = ((elcfolderFlags & ELCFolderFlags.DumpsterFolder) != ELCFolderFlags.None);
							break;
						}
						case 12:
							this.LastMovedTimeStamp = (properties[i] as ExDateTime?);
							break;
						}
					}
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000597FC File Offset: 0x000579FC
			public int TotalItemCount
			{
				get
				{
					return ((this.ItemCount != null) ? this.ItemCount.Value : 0) + ((this.AssociatedItemCount != null) ? this.AssociatedItemCount.Value : 0);
				}
			}

			// Token: 0x04000964 RID: 2404
			private const int IdIndex = 0;

			// Token: 0x04000965 RID: 2405
			private const int ReplicaListIndex = 1;

			// Token: 0x04000966 RID: 2406
			private const int ReplicaListBinaryIndex = 2;

			// Token: 0x04000967 RID: 2407
			private const int OverallAgeLimitIndex = 3;

			// Token: 0x04000968 RID: 2408
			private const int RetentionAgeLimitIndex = 4;

			// Token: 0x04000969 RID: 2409
			private const int DeletedItemsEntryIdIndex = 5;

			// Token: 0x0400096A RID: 2410
			private const int ItemCountIndex = 6;

			// Token: 0x0400096B RID: 2411
			private const int AssociatedItemCountIndex = 7;

			// Token: 0x0400096C RID: 2412
			private const int ChildCountIndex = 8;

			// Token: 0x0400096D RID: 2413
			private const int ParentEntryIdIndex = 9;

			// Token: 0x0400096E RID: 2414
			private const int LastModifiedTimeIndex = 10;

			// Token: 0x0400096F RID: 2415
			private const int AdminFolderFlagsIndex = 11;

			// Token: 0x04000970 RID: 2416
			private const int LastMovedTimeStampIndex = 12;

			// Token: 0x04000971 RID: 2417
			public static PropertyDefinition[] PropertiesToLoad = new PropertyDefinition[]
			{
				FolderSchema.Id,
				FolderSchema.ReplicaList,
				FolderSchema.ReplicaListBinary,
				FolderSchema.OverallAgeLimit,
				FolderSchema.RetentionAgeLimit,
				CoreFolderSchema.DeletedItemsEntryId,
				FolderSchema.ItemCount,
				FolderSchema.AssociatedItemCount,
				FolderSchema.ChildCount,
				StoreObjectSchema.ParentEntryId,
				StoreObjectSchema.LastModifiedTime,
				FolderSchema.AdminFolderFlags,
				FolderSchema.LastMovedTimeStamp
			};

			// Token: 0x04000972 RID: 2418
			public readonly StoreObjectId FolderId;

			// Token: 0x04000973 RID: 2419
			public readonly Guid ContentMailboxGuid;

			// Token: 0x04000974 RID: 2420
			public readonly int? OverallAgeLimit;

			// Token: 0x04000975 RID: 2421
			public readonly int? RetentionAgeLimit;

			// Token: 0x04000976 RID: 2422
			public readonly StoreObjectId DumpsterId;

			// Token: 0x04000977 RID: 2423
			public readonly int? ItemCount;

			// Token: 0x04000978 RID: 2424
			public readonly int? AssociatedItemCount;

			// Token: 0x04000979 RID: 2425
			public readonly int? SubfolderCount;

			// Token: 0x0400097A RID: 2426
			public readonly StoreObjectId ParentId;

			// Token: 0x0400097B RID: 2427
			public readonly ExDateTime? LastModifiedTime;

			// Token: 0x0400097C RID: 2428
			public readonly bool IsDumpsterFolder;

			// Token: 0x0400097D RID: 2429
			public readonly ExDateTime? LastMovedTimeStamp;
		}

		// Token: 0x02000178 RID: 376
		private class MessageRec
		{
			// Token: 0x06000EFA RID: 3834 RVA: 0x000598D4 File Offset: 0x00057AD4
			public MessageRec(object[] properties)
			{
				for (int i = 0; i < PublicFolderItemProcessor.MessageRec.PropertiesToLoad.Length; i++)
				{
					if (!(properties[i] is PropertyError))
					{
						switch (i)
						{
						case 0:
							this.ItemId = (properties[i] as VersionedId);
							break;
						case 1:
							this.ReceivedTime = (properties[i] as ExDateTime?);
							break;
						case 2:
							this.CreationTime = (properties[i] as ExDateTime?);
							break;
						case 3:
							this.LastModifiedTime = (properties[i] as ExDateTime?);
							break;
						}
					}
				}
			}

			// Token: 0x0400097E RID: 2430
			private const int ItemIdIndex = 0;

			// Token: 0x0400097F RID: 2431
			private const int ReceivedTimeIndex = 1;

			// Token: 0x04000980 RID: 2432
			private const int CreationTimeIndex = 2;

			// Token: 0x04000981 RID: 2433
			private const int LastModifiedTimeIndex = 3;

			// Token: 0x04000982 RID: 2434
			public static PropertyDefinition[] PropertiesToLoad = new PropertyDefinition[]
			{
				CoreItemSchema.Id,
				CoreItemSchema.ReceivedTime,
				CoreObjectSchema.CreationTime,
				CoreObjectSchema.LastModifiedTime
			};

			// Token: 0x04000983 RID: 2435
			public readonly VersionedId ItemId;

			// Token: 0x04000984 RID: 2436
			public readonly ExDateTime? ReceivedTime;

			// Token: 0x04000985 RID: 2437
			public readonly ExDateTime? CreationTime;

			// Token: 0x04000986 RID: 2438
			public readonly ExDateTime? LastModifiedTime;
		}
	}
}
