﻿using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal class StorageDestinationFolder : StorageFolder, IDestinationFolder, IFolder, IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00004800 File Offset: 0x00002A00
		public void SetExtendedProps(PropTag[] promotedProperties, RestrictionData[] restrictions, SortOrderData[] views, ICSViewData[] icsViews)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetExtendedProps: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			if (promotedProperties != null && promotedProperties.Length > 0)
			{
				ExecutionContext.Create(new DataContext[]
				{
					new OperationDataContext("StorageDestinationFolder.SetPromotedProps", OperationType.None),
					new PropTagsDataContext(promotedProperties)
				}).Execute(delegate
				{
					PropertyDefinition[] dataColumns = this.Mailbox.ConvertPropTagsToDefinitions(promotedProperties);
					using (this.Mailbox.RHTracker.Start())
					{
						using (QueryResult queryResult = this.CoreFolder.QueryExecutor.ItemQuery(ItemQueryType.None, null, null, dataColumns))
						{
							this.GetRowsIgnoreKnownFailures(queryResult);
						}
					}
				});
			}
			if (restrictions != null && restrictions.Length > 0)
			{
				MrsTracer.Provider.Debug("Applying restrictions.", new object[0]);
				for (int i = 0; i < restrictions.Length; i++)
				{
					RestrictionData rd = restrictions[i];
					ExecutionContext.Create(new DataContext[]
					{
						new OperationDataContext("StorageDestinationFolder.ApplyRestriction", OperationType.None),
						new RestrictionDataContext(rd)
					}).Execute(delegate
					{
						QueryFilter queryFilter = rd.GetQueryFilter(this.Mailbox.StoreSession);
						PropertyDefinition[] dataColumns = new PropertyDefinition[]
						{
							CoreObjectSchema.EntryId
						};
						using (this.Mailbox.RHTracker.Start())
						{
							using (new SortLCIDContext(this.Mailbox.StoreSession, rd.LCID))
							{
								using (QueryResult queryResult = this.CoreFolder.QueryExecutor.ItemQuery(ItemQueryType.None, queryFilter, null, dataColumns))
								{
									this.GetRowsIgnoreKnownFailures(queryResult);
								}
							}
						}
					});
				}
			}
			if (views != null && views.Length > 0)
			{
				MrsTracer.Provider.Debug("Applying views.", new object[0]);
				for (int j = 0; j < views.Length; j++)
				{
					SortOrderData sod = views[j];
					if (sod.Members != null && sod.Members.Length != 0)
					{
						ExecutionContext.Create(new DataContext[]
						{
							new OperationDataContext("StorageDestinationFolder.ApplySortOrder", OperationType.None),
							new SortOrderDataContext(sod)
						}).Execute(delegate
						{
							List<SortBy> list;
							List<GroupByAndOrder> list2;
							int expandCount;
							this.GetSortBy(sod, out list, out list2, out expandCount);
							ItemQueryType itemQueryType = ItemQueryType.None;
							if (sod.FAI)
							{
								itemQueryType |= ItemQueryType.Associated;
							}
							else if (sod.Conversation)
							{
								itemQueryType |= ItemQueryType.ConversationView;
							}
							PropertyDefinition[] dataColumns = new PropertyDefinition[]
							{
								CoreObjectSchema.EntryId
							};
							using (this.Mailbox.RHTracker.Start())
							{
								using (new SortLCIDContext(this.Mailbox.StoreSession, sod.LCID))
								{
									QueryResult queryResult;
									if (list2.Count > 0)
									{
										queryResult = this.CoreFolder.QueryExecutor.GroupedItemQuery(null, itemQueryType, list2.ToArray(), expandCount, list.ToArray(), dataColumns);
									}
									else
									{
										queryResult = this.CoreFolder.QueryExecutor.ItemQuery(itemQueryType, null, (list.Count == 0) ? null : list.ToArray(), dataColumns);
									}
									using (queryResult)
									{
										this.GetRowsIgnoreKnownFailures(queryResult);
									}
								}
							}
						});
					}
				}
			}
			if (icsViews != null && icsViews.Length > 0)
			{
				MrsTracer.Provider.Debug("Applying ICS views.", new object[0]);
				for (int k = 0; k < icsViews.Length; k++)
				{
					ICSViewData icsView = icsViews[k];
					ExecutionContext.Create(new DataContext[]
					{
						new OperationDataContext("StorageDestinationFolder.ApplyICSView", OperationType.None),
						new ICSViewDataContext(icsView)
					}).Execute(delegate
					{
						using (MailboxSyncProvider mailboxSyncProvider = new MailboxSyncProvider(this.Folder, true, !icsView.Conversation, true, icsView.Conversation, true, false, null))
						{
							mailboxSyncProvider.GetMaxItemWatermark(mailboxSyncProvider.CreateNewWatermark());
						}
					});
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004AA0 File Offset: 0x00002CA0
		bool IDestinationFolder.SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetSearchCriteria: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			QueryFilter queryFilter = restriction.GetQueryFilter(base.Mailbox.StoreSession);
			StoreId[] array = null;
			if (entryIds != null && entryIds.Length > 0)
			{
				array = new StoreId[entryIds.Length];
				for (int i = 0; i < entryIds.Length; i++)
				{
					array[i] = StoreObjectId.FromProviderSpecificId(entryIds[i]);
				}
			}
			using (base.Mailbox.RHTracker.Start())
			{
				SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(queryFilter, array);
				base.CoreFolder.SetSearchCriteria(searchFolderCriteria, this.ConvertSearchCriteriaFlags(flags));
			}
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004B5C File Offset: 0x00002D5C
		PropProblemData[] IDestinationFolder.SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetSecurityDescriptor: {0}, is null: {1}", new object[]
			{
				base.DisplayNameForTracing,
				sd == null
			});
			if (sd == null)
			{
				return null;
			}
			using (base.Mailbox.RHTracker.Start())
			{
				switch (secProp)
				{
				case SecurityProp.NTSD:
					AclModifyTable.SetFolderSecurityDescriptor(base.CoreFolder, sd);
					goto IL_7B;
				case SecurityProp.FreeBusyNTSD:
					AclModifyTable.SetFolderFreeBusySecurityDescriptor(base.CoreFolder, sd);
					goto IL_7B;
				}
				throw new UnknownSecurityPropException((int)secProp);
				IL_7B:
				base.CoreFolder.Save(SaveMode.FailOnAnyConflict);
				base.CoreFolder.PropertyBag.Load(FolderSchema.Instance.AutoloadProperties);
			}
			return Array<PropProblemData>.Empty;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004C2C File Offset: 0x00002E2C
		IFxProxy IDestinationFolder.GetFxProxy(FastTransferFlags flags)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.GetFxProxy: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			IFxProxy result;
			using (base.Mailbox.RHTracker.Start())
			{
				IMapiFxProxy destProxy = flags.HasFlag(FastTransferFlags.PassThrough) ? base.CoreFolder.GetFxProxyCollector() : new StorageFolderProxy(this, base.Mailbox.Flags.HasFlag(LocalMailboxFlags.Move));
				IFxProxy proxy = new FxProxyBudgetWrapper(destProxy, true, new Func<IDisposable>(base.Mailbox.RHTracker.Start), new Action<uint>(base.Mailbox.RHTracker.Charge));
				IFxProxy fxProxy = new FxProxyWrapper(proxy, null);
				result = fxProxy;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004D14 File Offset: 0x00002F14
		void IDestinationFolder.SetMessageProps(byte[] entryId, PropValueData[] propValues)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetMessageProps: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			if (entryId == null || propValues == null || propValues.Length == 0)
			{
				return;
			}
			using (base.Mailbox.RHTracker.Start())
			{
				StoreObjectId storeId = StoreObjectId.FromProviderSpecificId(entryId);
				CoreItem coreItem = CoreItem.Bind(base.Mailbox.StoreSession, storeId);
				using (coreItem)
				{
					object[] array = new object[propValues.Length];
					PropTag[] array2 = new PropTag[propValues.Length];
					for (int i = 0; i < propValues.Length; i++)
					{
						array2[i] = (PropTag)propValues[i].PropTag;
						array[i] = propValues[i].Value;
					}
					NativeStorePropertyDefinition[] array3 = base.Mailbox.ConvertPropTagsToDefinitions(array2);
					coreItem.OpenAsReadWrite();
					for (int j = 0; j < propValues.Length; j++)
					{
						coreItem.PropertyBag[array3[j]] = array[j];
					}
					coreItem.Save(SaveMode.ResolveConflicts);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004EBC File Offset: 0x000030BC
		void IDestinationFolder.SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetReadFlagsOnMessages: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			MapiUtils.ProcessMapiCallInBatches<byte[]>(entryIds, delegate(byte[][] batch)
			{
				StoreObjectId[] array = new StoreObjectId[batch.Length];
				for (int i = 0; i < batch.Length; i++)
				{
					array[i] = StoreObjectId.FromProviderSpecificId(batch[i]);
				}
				using (this.Mailbox.RHTracker.Start())
				{
					bool flag;
					this.CoreFolder.SetReadFlags((int)flags, array, out flag);
				}
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004F10 File Offset: 0x00003110
		void IDestinationFolder.SetRules(RuleData[] rules)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetRules: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			Rule[] native = DataConverter<RuleConverter, Rule, RuleData>.GetNative(rules);
			using (base.Mailbox.RHTracker.Start())
			{
				base.Folder.MapiFolder.SetRules(native);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004F84 File Offset: 0x00003184
		void IDestinationFolder.SetACL(SecurityProp secProp, PropValueData[][] aclData)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetACL: {0}, isNullAcl: {1}", new object[]
			{
				base.DisplayNameForTracing,
				aclData == null
			});
			if (aclData == null)
			{
				return;
			}
			ModifyTableOptions options = (secProp == SecurityProp.FreeBusyNTSD) ? ModifyTableOptions.FreeBusyAware : ModifyTableOptions.None;
			using (IModifyTable permissionTable = base.CoreFolder.GetPermissionTable(options))
			{
				permissionTable.Clear();
				foreach (PropValueData[] array in aclData)
				{
					long? num = null;
					byte[] array2 = null;
					MemberRights memberRights = MemberRights.None;
					foreach (PropValueData propValueData in array)
					{
						int propTag = propValueData.PropTag;
						if (propTag != 268370178)
						{
							if (propTag != 1718681620)
							{
								if (propTag == 1718812675)
								{
									memberRights = (MemberRights)propValueData.Value;
								}
							}
							else
							{
								num = new long?((long)propValueData.Value);
							}
						}
						else
						{
							array2 = (byte[])propValueData.Value;
						}
					}
					PropValue[] array3 = new PropValue[2];
					array3[0] = new PropValue(PermissionSchema.MemberRights, memberRights);
					if (num != null && (num.Value == 0L || num.Value == -1L))
					{
						array3[1] = new PropValue(PermissionSchema.MemberId, num);
						permissionTable.ModifyRow(array3);
					}
					else if (array2 != null)
					{
						array3[1] = new PropValue(PermissionSchema.MemberEntryId, array2);
						permissionTable.AddRow(array3);
					}
				}
				using (base.Mailbox.RHTracker.Start())
				{
					permissionTable.ApplyPendingChanges();
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000517C File Offset: 0x0000337C
		void IDestinationFolder.SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.SetExtendedAcl: {0}, flags: {1}, isNullAcl: {2}", new object[]
			{
				base.DisplayNameForTracing,
				aclFlags,
				aclData == null
			});
			using (base.Mailbox.RHTracker.Start())
			{
				new FolderAcl(aclFlags, aclData).Apply(base.CoreFolder);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005200 File Offset: 0x00003400
		Guid IDestinationFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.LinkMailPublicFolder: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
			return base.Mailbox.LinkMailPublicFolder(base.FolderId, flags, objectId);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005240 File Offset: 0x00003440
		void IDestinationFolder.Flush()
		{
			MrsTracer.Provider.Function("StorageDestinationFolder.Flush: {0}", new object[]
			{
				base.DisplayNameForTracing
			});
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005270 File Offset: 0x00003470
		private void GetSortBy(SortOrderData sod, out List<SortBy> sortBy, out List<GroupByAndOrder> groupBy, out int expandCount)
		{
			sortBy = new List<SortBy>();
			groupBy = new List<GroupByAndOrder>();
			foreach (SortOrderMember sortOrderMember in sod.Members)
			{
				if (groupBy.Count >= 4 || groupBy.Count + sortBy.Count >= 6)
				{
					break;
				}
				SortOrder sortOrder;
				if ((sortOrderMember.Flags & 1) != 0)
				{
					sortOrder = SortOrder.Descending;
				}
				else
				{
					sortOrder = SortOrder.Ascending;
				}
				if (sortOrderMember.IsCategory)
				{
					PropertyDefinition propertyDefinition = base.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
					{
						(PropTag)sortOrderMember.PropTag
					})[0];
					Aggregate aggregate;
					if ((sortOrderMember.Flags & 4) != 0)
					{
						aggregate = Aggregate.Max;
					}
					else
					{
						aggregate = Aggregate.Min;
					}
					GroupSort groupSortColumn = new GroupSort(propertyDefinition, sortOrder, aggregate);
					groupBy.Add(new GroupByAndOrder(propertyDefinition, groupSortColumn));
				}
				else
				{
					sortBy.Add(new SortBy(base.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
					{
						(PropTag)sortOrderMember.PropTag
					})[0], sortOrder));
				}
			}
			expandCount = groupBy.Count;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005374 File Offset: 0x00003574
		private SetSearchCriteriaFlags ConvertSearchCriteriaFlags(SearchCriteriaFlags mapiFlags)
		{
			SetSearchCriteriaFlags setSearchCriteriaFlags = SetSearchCriteriaFlags.None;
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Background, SetSearchCriteriaFlags.Background);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.ContentIndexed, SetSearchCriteriaFlags.ContentIndexed);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.ContentIndexedOnly, SetSearchCriteriaFlags.FailNonContentIndexedSearch);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.EstimateCountOnly, SetSearchCriteriaFlags.EstimateCountOnly);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.FailOnForeignEID, SetSearchCriteriaFlags.FailOnForeignEID);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Foreground, SetSearchCriteriaFlags.Foreground);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.NonContentIndexed, SetSearchCriteriaFlags.NonContentIndexed);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Recursive, SetSearchCriteriaFlags.Recursive);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Restart, SetSearchCriteriaFlags.Restart);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Shallow, SetSearchCriteriaFlags.Shallow);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Static, SetSearchCriteriaFlags.Static);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.StatisticsOnly, SetSearchCriteriaFlags.StatisticsOnly);
			setSearchCriteriaFlags |= this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.Stop, SetSearchCriteriaFlags.Stop);
			return setSearchCriteriaFlags | this.GetMappedFlag(mapiFlags, SearchCriteriaFlags.UseCiForComplexQueries, SetSearchCriteriaFlags.UseCiForComplexQueries);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005470 File Offset: 0x00003670
		private SetSearchCriteriaFlags GetMappedFlag(SearchCriteriaFlags mapiFlag, SearchCriteriaFlags mapiValue, SetSearchCriteriaFlags storageValue)
		{
			if ((mapiFlag & mapiValue) != SearchCriteriaFlags.None)
			{
				return storageValue;
			}
			return SetSearchCriteriaFlags.None;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000054C8 File Offset: 0x000036C8
		private void GetRowsIgnoreKnownFailures(QueryResult queryResult)
		{
			CommonUtils.ProcessKnownExceptions(delegate
			{
				queryResult.GetRows(1);
			}, delegate(Exception ex)
			{
				if (ex is StorageTransientException && ex.InnerException is MapiExceptionInvalidObject)
				{
					MrsTracer.Provider.Error(CommonUtils.FullExceptionMessage(ex, true), new object[0]);
					return true;
				}
				return false;
			});
		}

		// Token: 0x02000006 RID: 6
		private class DummyManifestContentsCallback : IMapiManifestCallback
		{
			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00005510 File Offset: 0x00003710
			internal static StorageDestinationFolder.DummyManifestContentsCallback Instance
			{
				get
				{
					return StorageDestinationFolder.DummyManifestContentsCallback.instance;
				}
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00005517 File Offset: 0x00003717
			public ManifestCallbackStatus Change(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModificationTime, ManifestChangeType changeType, bool associated, PropValue[] props)
			{
				return ManifestCallbackStatus.Stop;
			}

			// Token: 0x06000049 RID: 73 RVA: 0x0000551A File Offset: 0x0000371A
			public ManifestCallbackStatus Delete(byte[] entryId, bool softDelete, bool expiry)
			{
				return ManifestCallbackStatus.Stop;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x0000551D File Offset: 0x0000371D
			public ManifestCallbackStatus ReadUnread(byte[] entryId, bool read)
			{
				return ManifestCallbackStatus.Stop;
			}

			// Token: 0x0400000E RID: 14
			private static StorageDestinationFolder.DummyManifestContentsCallback instance = new StorageDestinationFolder.DummyManifestContentsCallback();
		}
	}
}
