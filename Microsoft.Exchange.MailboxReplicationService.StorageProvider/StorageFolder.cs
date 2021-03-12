using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal abstract class StorageFolder : DisposeTrackableBase, IFolder, IDisposable
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002A2F File Offset: 0x00000C2F
		public StorageFolder()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002A37 File Offset: 0x00000C37
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002A3F File Offset: 0x00000C3F
		public CoreFolder CoreFolder { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002A50 File Offset: 0x00000C50
		public Folder Folder { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002A5C File Offset: 0x00000C5C
		public IFolder FxFolder
		{
			get
			{
				if (this.fxFolder == null)
				{
					this.CoreFolder.PropertyBag.Load(FolderSchema.Instance.AutoloadProperties);
					this.fxFolder = FolderAdaptor.Create(new ReferenceCount<CoreFolder>(this.CoreFolder), null, FastTransferCopyFlag.MoveUser, Encoding.ASCII, true, true);
				}
				return this.fxFolder;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002AB4 File Offset: 0x00000CB4
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002ABC File Offset: 0x00000CBC
		public StorageMailbox Mailbox { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002AC5 File Offset: 0x00000CC5
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002ACD File Offset: 0x00000CCD
		public byte[] FolderId { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002AD6 File Offset: 0x00000CD6
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002ADE File Offset: 0x00000CDE
		private protected string DisplayNameForTracing { protected get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002AE8 File Offset: 0x00000CE8
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			MrsTracer.Provider.Function("StorageFolder.GetFolderRec: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			FolderRec folderRec;
			if (flags.HasFlag(GetFolderRecFlags.NoProperties))
			{
				folderRec = new FolderRec();
			}
			else
			{
				NativeStorePropertyDefinition[] array;
				if (additionalPtagsToLoad == null || additionalPtagsToLoad.Length == 0)
				{
					array = this.Mailbox.FolderPropertyDefinitionsToLoad;
				}
				else
				{
					List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>();
					list.AddRange(this.Mailbox.ConvertPropTagsToDefinitions(additionalPtagsToLoad));
					list.AddRange(this.Mailbox.FolderPropertyDefinitionsToLoad);
					array = list.ToArray();
				}
				object[] array2 = new object[array.Length];
				this.CoreFolder.PropertyBag.Load(array);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = this.CoreFolder.PropertyBag.TryGetProperty(array[i]);
				}
				folderRec = FolderRec.Create(this.Mailbox.StoreSession, array, array2);
				this.GetExtendedProps(folderRec, flags);
			}
			folderRec.IsGhosted = !this.CoreFolder.IsContentAvailable();
			return folderRec;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003050 File Offset: 0x00001250
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("StorageFolder.EnumerateMessages: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			List<MessageRec> result = new List<MessageRec>();
			ItemQueryType[] array = new ItemQueryType[]
			{
				ItemQueryType.None,
				ItemQueryType.Associated
			};
			for (int i = 0; i < array.Length; i++)
			{
				ItemQueryType flags = array[i];
				bool doingFAI = (flags & ItemQueryType.Associated) != ItemQueryType.None;
				bool doingDeletes = (flags & ItemQueryType.SoftDeleted) != ItemQueryType.None;
				if ((emFlags & ((!doingDeletes || 2 != 0) ? EnumerateMessagesFlags.RegularMessages : ((EnumerateMessagesFlags)0))) != (EnumerateMessagesFlags)0)
				{
					List<NativeStorePropertyDefinition> dataColumns = new List<NativeStorePropertyDefinition>(5);
					int idxEntryId = -1;
					int idxCreationTime = -1;
					int idxMessageClass = -1;
					int idxRuleMsgVersion = -1;
					int idxMessageSize = -1;
					idxEntryId = dataColumns.Count;
					dataColumns.Add(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
					{
						PropTag.EntryId
					})[0]);
					if ((emFlags & EnumerateMessagesFlags.IncludeExtendedData) != (EnumerateMessagesFlags)0)
					{
						idxMessageSize = dataColumns.Count;
						dataColumns.Add(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
						{
							PropTag.MessageSize
						})[0]);
						idxCreationTime = dataColumns.Count;
						dataColumns.Add(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
						{
							PropTag.CreationTime
						})[0]);
					}
					if (doingFAI)
					{
						idxMessageClass = dataColumns.Count;
						dataColumns.Add(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
						{
							PropTag.MessageClass
						})[0]);
						idxRuleMsgVersion = dataColumns.Count;
						dataColumns.Add(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
						{
							PropTag.RuleMsgVersion
						})[0]);
					}
					int idxExtraPtags = dataColumns.Count;
					if (additionalPtagsToLoad != null)
					{
						dataColumns.AddRange(this.Mailbox.ConvertPropTagsToDefinitions(additionalPtagsToLoad));
					}
					MrsTracer.Provider.Debug("StorageFolder.GetContentsTable({0})", new object[]
					{
						flags
					});
					ExecutionContext.Create(new DataContext[]
					{
						new OperationDataContext("StorageFolder.EnumerateMessages", OperationType.None),
						new SimpleValueDataContext("Flags", flags)
					}).Execute(delegate
					{
						QueryFilter queryFilter = null;
						if (this.contentsRestriction != null)
						{
							queryFilter = this.contentsRestriction.GetQueryFilter(this.Mailbox.StoreSession);
						}
						using (QueryResult queryResult = this.CoreFolder.QueryExecutor.ItemQuery(flags, queryFilter, null, dataColumns.ToArray()))
						{
							MrsTracer.Provider.Debug("StorageFolder.EnumerateMessages: ItemQuery returned {0} items.", new object[]
							{
								queryResult.EstimatedRowCount
							});
							object[][] rows;
							do
							{
								using (this.Mailbox.RHTracker.Start())
								{
									rows = queryResult.GetRows(1000);
								}
								object[][] array2 = rows;
								int j = 0;
								while (j < array2.Length)
								{
									object[] array3 = array2[j];
									if (!doingFAI)
									{
										goto IL_197;
									}
									string x = (string)array3[idxMessageClass];
									if ((this.Mailbox.Options & MailboxOptions.IgnoreExtendedRuleFAIs) != MailboxOptions.None)
									{
										if (!StringComparer.OrdinalIgnoreCase.Equals(x, "IPM.Rule.Message") && !StringComparer.OrdinalIgnoreCase.Equals(x, "IPM.Rule.Version2.Message"))
										{
											if (!StringComparer.OrdinalIgnoreCase.Equals(x, "IPM.ExtendedRule.Message"))
											{
												goto IL_197;
											}
										}
									}
									else if (!StringComparer.OrdinalIgnoreCase.Equals(x, "IPM.Rule.Message") || !(array3[idxRuleMsgVersion] as short? == 1))
									{
										goto IL_197;
									}
									IL_3E6:
									j++;
									continue;
									IL_197:
									DateTime dateTime = DateTime.MinValue;
									if (idxCreationTime != -1)
									{
										object obj = array3[idxCreationTime];
										if (obj is ExDateTime)
										{
											dateTime = (DateTime)((ExDateTime)obj);
										}
									}
									byte[] entryId = (byte[])array3[idxEntryId];
									if (emFlags.HasFlag(EnumerateMessagesFlags.SkipICSMidSetMissing) && this.Mailbox.SupportsSavingSyncState)
									{
										SyncContentsManifestState syncContentsManifestState = this.Mailbox.SyncState[this.FolderId];
										if (syncContentsManifestState != null && !syncContentsManifestState.IdSetGivenContainsEntryId(entryId))
										{
											MrsTracer.Provider.Debug("entry id {0} with creation time {1} not found in given items.", new object[]
											{
												TraceUtils.DumpEntryId(entryId),
												dateTime
											});
											goto IL_3E6;
										}
									}
									List<PropValueData> list = null;
									if (additionalPtagsToLoad != null && additionalPtagsToLoad.Length > 0)
									{
										list = new List<PropValueData>();
										for (int k = idxExtraPtags; k < array3.Length; k++)
										{
											list.Add(new PropValueData(additionalPtagsToLoad[k - idxExtraPtags], array3[k]));
										}
									}
									int messageSize = 1000;
									if (idxMessageSize != -1)
									{
										object obj2 = array3[idxMessageSize];
										if (obj2 is int)
										{
											messageSize = (int)obj2;
										}
									}
									if (emFlags.HasFlag(EnumerateMessagesFlags.ReturnLongTermIDs))
									{
										if (list == null)
										{
											list = new List<PropValueData>();
										}
										list.Add(new PropValueData(PropTag.LTID, this.Mailbox.StoreSession.IdConverter.GetLongTermIdFromId(this.Mailbox.StoreSession.IdConverter.GetMidFromMessageId(StoreObjectId.FromProviderSpecificId(entryId)))));
									}
									MsgRecFlags msgRecFlags = doingFAI ? MsgRecFlags.Associated : MsgRecFlags.None;
									if (doingDeletes)
									{
										msgRecFlags |= MsgRecFlags.Deleted;
									}
									MessageRec item = new MessageRec(entryId, this.FolderId, dateTime, messageSize, msgRecFlags, (list == null) ? null : list.ToArray());
									result.Add(item);
									goto IL_3E6;
								}
							}
							while (rows.Length > 0);
						}
					});
				}
			}
			MrsTracer.Provider.Debug("StorageFolder.EnumerateMessages returns {0} items.", new object[]
			{
				result.Count
			});
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003368 File Offset: 0x00001568
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			MrsTracer.Provider.Function("StorageFolder.GetSecurityDescriptor: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			if (!this.HasSecurityDescriptor(secProp))
			{
				return null;
			}
			PropertyDefinition propertyDefinitionFromSecurityProp = this.GetPropertyDefinitionFromSecurityProp(secProp);
			this.CoreFolder.PropertyBag.Load(new PropertyDefinition[]
			{
				propertyDefinitionFromSecurityProp
			});
			object obj = this.CoreFolder.PropertyBag[propertyDefinitionFromSecurityProp];
			if (obj == null || obj is PropertyError)
			{
				return null;
			}
			return (RawSecurityDescriptor)obj;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003458 File Offset: 0x00001658
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			MrsTracer.Provider.Function("StorageFolder.DeleteMessages: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			MapiUtils.ProcessMapiCallInBatches<byte[]>(entryIds, delegate(byte[][] batch)
			{
				using (this.Mailbox.RHTracker.Start())
				{
					StoreObjectId[] array = new StoreObjectId[batch.Length];
					for (int i = 0; i < batch.Length; i++)
					{
						array[i] = StoreObjectId.FromProviderSpecificId(batch[i]);
					}
					this.CoreFolder.DeleteItems(DeleteItemFlags.HardDelete, array);
				}
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003498 File Offset: 0x00001698
		byte[] IFolder.GetFolderId()
		{
			MrsTracer.Provider.Function("StorageFolder.GetFolderId: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			return this.FolderId;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000034CC File Offset: 0x000016CC
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			MrsTracer.Provider.Function("StorageFolder.SetContentsRestriction: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			this.contentsRestriction = restriction;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003500 File Offset: 0x00001700
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			MrsTracer.Provider.Function("StorageFolder.GetProps: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			PropValueData[] array = new PropValueData[pta.Length];
			PropertyDefinition[] array2 = this.Mailbox.ConvertPropTagsToDefinitions(pta);
			PropValueData[] result;
			using (this.Mailbox.RHTracker.Start())
			{
				this.CoreFolder.PropertyBag.Load(array2);
				for (int i = 0; i < pta.Length; i++)
				{
					PropTag propTag = pta[i];
					object value = this.CoreFolder.PropertyBag.TryGetProperty(array2[i]);
					array[i] = new PropValueData(propTag, value);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000035C0 File Offset: 0x000017C0
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			MrsTracer.Provider.Function("StorageFolder.GetSearchCriteria: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			restriction = null;
			entryIds = null;
			state = SearchState.None;
			SearchFolderCriteria searchFolderCriteria = null;
			try
			{
				using (this.Mailbox.RHTracker.Start())
				{
					searchFolderCriteria = this.CoreFolder.GetSearchCriteria(false);
				}
			}
			catch (ObjectNotInitializedException ex)
			{
				MrsTracer.Provider.Warning("GetSearchCriteria failed with ObjectNotInitializedException, ignoring.\n{0}", new object[]
				{
					CommonUtils.FullExceptionMessage(ex)
				});
			}
			if (searchFolderCriteria != null)
			{
				state = (SearchState)searchFolderCriteria.SearchState;
				if (searchFolderCriteria.FolderScope != null)
				{
					entryIds = new byte[searchFolderCriteria.FolderScope.Length][];
					for (int i = 0; i < searchFolderCriteria.FolderScope.Length; i++)
					{
						entryIds[i] = StoreId.GetStoreObjectId(searchFolderCriteria.FolderScope[i]).ProviderLevelItemId;
					}
				}
				restriction = RestrictionData.GetRestrictionData(this.Mailbox.StoreSession, searchFolderCriteria.SearchQuery);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000036D4 File Offset: 0x000018D4
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			MrsTracer.Provider.Function("StorageFolder.GetRules: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			if (this.CoreFolder.Id.ObjectId.ObjectType == StoreObjectType.SearchFolder)
			{
				return Array<RuleData>.Empty;
			}
			Rule[] rules;
			using (this.Mailbox.RHTracker.Start())
			{
				rules = this.Folder.MapiFolder.GetRules(extraProps);
			}
			return DataConverter<RuleConverter, Rule, RuleData>.GetData(rules);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003764 File Offset: 0x00001964
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			MrsTracer.Provider.Function("StorageFolder.GetACL: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			if (!this.HasSecurityDescriptor(secProp))
			{
				return null;
			}
			ModifyTableOptions options = (secProp == SecurityProp.FreeBusyNTSD) ? ModifyTableOptions.FreeBusyAware : ModifyTableOptions.None;
			return this.GetAcl(options, StorageFolder.AclTableColumns);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000037B4 File Offset: 0x000019B4
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			MrsTracer.Provider.Function("StorageFolder.GetExtendedAcl: flags = {0}, {1}", new object[]
			{
				aclFlags,
				this.DisplayNameForTracing
			});
			if (!this.HasSecurityDescriptor(aclFlags))
			{
				return null;
			}
			ModifyTableOptions modifyTableOptions = aclFlags.HasFlag(AclFlags.FreeBusyAcl) ? ModifyTableOptions.FreeBusyAware : ModifyTableOptions.None;
			modifyTableOptions |= ModifyTableOptions.ExtendedPermissionInformation;
			return this.GetAcl(modifyTableOptions, StorageFolder.ExtendedAclTableColumns);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003C84 File Offset: 0x00001E84
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("StorageFolder.LookupMessages: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			EntryIdMap<MessageRec> result = new EntryIdMap<MessageRec>();
			ItemQueryType[] array = new ItemQueryType[]
			{
				ItemQueryType.None,
				ItemQueryType.Associated
			};
			int capacity = 3 + ((additionalPtagsToLoad == null) ? 0 : additionalPtagsToLoad.Length);
			List<PropertyDefinition> columns = new List<PropertyDefinition>(capacity)
			{
				CoreObjectSchema.EntryId,
				CoreItemSchema.Size,
				CoreObjectSchema.CreationTime
			};
			int idxEntryId = 0;
			int idxMessageSize = 1;
			int idxCreationTime = 2;
			int idxExtraProperties = 3;
			if (additionalPtagsToLoad != null && additionalPtagsToLoad.Length > 0)
			{
				columns.AddRange(this.Mailbox.ConvertPropTagsToDefinitions(additionalPtagsToLoad));
			}
			ItemQueryType[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				StorageFolder.<>c__DisplayClassb CS$<>8__locals2 = new StorageFolder.<>c__DisplayClassb();
				CS$<>8__locals2.queryType = array2[i];
				if (result.Count >= keysToLookup.Count)
				{
					break;
				}
				bool doingFAI = CS$<>8__locals2.queryType == ItemQueryType.Associated;
				ExecutionContext.Create(new DataContext[]
				{
					new OperationDataContext("StorageFolder.GetContentsTable", OperationType.None),
					new SimpleValueDataContext("QueryType", CS$<>8__locals2.queryType)
				}).Execute(delegate
				{
					SortBy[] sortColumns = null;
					PropertyDefinition propertyDefinition = CoreObjectSchema.EntryId;
					if (ptagToLookup != PropTag.EntryId)
					{
						propertyDefinition = this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
						{
							ptagToLookup
						})[0];
						sortColumns = new SortBy[]
						{
							new SortBy(propertyDefinition, SortOrder.Ascending)
						};
					}
					QueryResult queryResult;
					using (this.Mailbox.RHTracker.Start())
					{
						queryResult = this.CoreFolder.QueryExecutor.ItemQuery(CS$<>8__locals2.queryType, null, sortColumns, columns);
					}
					using (queryResult)
					{
						using (List<byte[]>.Enumerator enumerator = keysToLookup.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								StorageFolder.<>c__DisplayClassd.<>c__DisplayClass11 <>c__DisplayClass = new StorageFolder.<>c__DisplayClassd.<>c__DisplayClass11();
								<>c__DisplayClass.key = enumerator.Current;
								if (!result.ContainsKey(<>c__DisplayClass.key))
								{
									ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.Equal, propertyDefinition, <>c__DisplayClass.key);
									ExecutionContext.Create(new DataContext[]
									{
										new EntryIDsDataContext(<>c__DisplayClass.key)
									}).Execute(delegate
									{
										try
										{
											using (this.Mailbox.RHTracker.Start())
											{
												if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, comparisonFilter))
												{
													return;
												}
											}
										}
										catch (Exception ex)
										{
											if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
											{
												WellKnownException.MapiNotFound
											}))
											{
												return;
											}
											throw;
										}
										object[][] rows;
										using (this.Mailbox.RHTracker.Start())
										{
											rows = queryResult.GetRows(1);
										}
										if (rows.Length == 1)
										{
											object[] array3 = rows[0];
											if (array3.Length != columns.Count)
											{
												return;
											}
											PropValueData[] array4 = null;
											if (additionalPtagsToLoad != null && additionalPtagsToLoad.Length > 0)
											{
												array4 = new PropValueData[additionalPtagsToLoad.Length];
												for (int j = idxExtraProperties; j < array3.Length; j++)
												{
													array4[j - idxExtraProperties] = new PropValueData(additionalPtagsToLoad[j - idxExtraProperties], array3[j]);
												}
											}
											byte[] entryId = (byte[])array3[idxEntryId];
											object obj = array3[idxCreationTime];
											DateTime creationTimestamp;
											if (obj == null || obj is PropertyError)
											{
												creationTimestamp = DateTime.MinValue;
											}
											else if (obj is ExDateTime)
											{
												creationTimestamp = (DateTime)((ExDateTime)obj);
											}
											else
											{
												creationTimestamp = (DateTime)obj;
											}
											int messageSize = (array3[idxMessageSize] == null) ? 1000 : ((int)array3[idxMessageSize]);
											MessageRec value = new MessageRec(entryId, this.FolderId, creationTimestamp, messageSize, doingFAI ? MsgRecFlags.Associated : MsgRecFlags.None, array4);
											result.Add(<>c__DisplayClass.key, value);
											return;
										}
									});
								}
							}
						}
					}
				});
			}
			MrsTracer.Provider.Debug("StorageFolder.LookupMessages returns {0} items.", new object[]
			{
				result.Count
			});
			return new List<MessageRec>(result.Values);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003E8C File Offset: 0x0000208C
		PropProblemData[] IFolder.SetProps(PropValueData[] pvda)
		{
			MrsTracer.Provider.Function("StorageFolder.SetProps: {0}", new object[]
			{
				this.DisplayNameForTracing
			});
			if (pvda == null || pvda.Length == 0)
			{
				return Array<PropProblemData>.Empty;
			}
			this.CoreFolder.PropertyBag.Load(FolderSchema.Instance.AutoloadProperties);
			NativeStorePropertyDefinition[] array = this.Mailbox.SetPropertiesHelper(this.CoreFolder.PropertyBag, pvda);
			List<PropProblemData> list = new List<PropProblemData>();
			FolderSaveResult folderSaveResult = this.CoreFolder.Save(SaveMode.NoConflictResolution);
			this.CoreFolder.PropertyBag.Load(FolderSchema.Instance.AutoloadProperties);
			if (folderSaveResult.PropertyErrors.Length > 0)
			{
				foreach (PropertyError propertyError in folderSaveResult.PropertyErrors)
				{
					int num = Array.IndexOf<PropertyDefinition>(array, propertyError.PropertyDefinition);
					if (num != -1)
					{
						list.Add(new PropProblemData
						{
							Index = num,
							PropTag = pvda[num].PropTag,
							Scode = (int)propertyError.PropertyErrorCode
						});
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003FA4 File Offset: 0x000021A4
		internal virtual void Config(byte[] folderId, CoreFolder folder, StorageMailbox mailbox)
		{
			if (MrsTracer.Provider.IsEnabled(TraceType.FunctionTrace))
			{
				this.DisplayNameForTracing = (string)folder.PropertyBag[FolderSchema.DisplayName];
				MrsTracer.Provider.Function("StorageFolder.Config: {0}", new object[]
				{
					this.DisplayNameForTracing
				});
			}
			this.FolderId = folderId;
			this.CoreFolder = folder;
			this.Mailbox = mailbox;
			this.Folder = Folder.Bind(this.Mailbox.StoreSession, this.CoreFolder.Id);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004030 File Offset: 0x00002230
		protected PropertyDefinition GetPropertyDefinitionFromSecurityProp(SecurityProp secProp)
		{
			switch (secProp)
			{
			case SecurityProp.NTSD:
				return CoreFolderSchema.SecurityDescriptor;
			case SecurityProp.FreeBusyNTSD:
				return CoreFolderSchema.FreeBusySecurityDescriptor;
			}
			throw new UnknownSecurityPropException((int)secProp);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004068 File Offset: 0x00002268
		protected override void InternalDispose(bool calledFromDispose)
		{
			MrsTracer.Provider.Function("StorageFolder.InternalDispose: {0}, {1}", new object[]
			{
				this.DisplayNameForTracing,
				calledFromDispose
			});
			if (calledFromDispose)
			{
				if (this.fxFolder != null)
				{
					this.fxFolder.Dispose();
					this.fxFolder = null;
				}
				if (this.CoreFolder != null)
				{
					this.CoreFolder.Dispose();
					this.CoreFolder = null;
				}
				if (this.Folder != null)
				{
					this.Folder.Dispose();
					this.Folder = null;
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000040EF File Offset: 0x000022EF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StorageFolder>(this);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000040F8 File Offset: 0x000022F8
		private void GetExtendedProps(FolderRec folderRec, GetFolderRecFlags flags)
		{
			MrsTracer.Provider.Function("StorageFolder.GetExtendedProps", new object[0]);
			if ((flags & GetFolderRecFlags.PromotedProperties) != GetFolderRecFlags.None)
			{
				PropertyDefinition[] array = this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
				{
					PropTag.PromotedProperties
				});
				object obj;
				using (this.Mailbox.RHTracker.Start())
				{
					this.CoreFolder.PropertyBag.Load(array);
					obj = this.CoreFolder.PropertyBag.TryGetProperty(array[0]);
				}
				if (obj != null && !(obj is PropertyError))
				{
					List<PropTag> list = new List<PropTag>(PropTagHelper.PropTagArray((byte[])obj));
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].ValueType() == (PropType)251)
						{
							list.RemoveAt(i);
						}
					}
					folderRec.SetPromotedProperties(list.ToArray());
					MrsTracer.Provider.Debug("Found {0} promoted properties.", new object[]
					{
						list.Count
					});
				}
				else
				{
					folderRec.SetPromotedProperties(null);
					MrsTracer.Provider.Debug("Source server does not support promoted property retrieval.", new object[0]);
				}
			}
			if ((flags & (GetFolderRecFlags.Views | GetFolderRecFlags.Restrictions)) != GetFolderRecFlags.None)
			{
				using (this.Mailbox.GetRpcAdmin())
				{
					this.Mailbox.GetFolderViewsOrRestrictions(folderRec, flags, this.FolderId);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004270 File Offset: 0x00002470
		private bool HasSecurityDescriptor(SecurityProp secProp)
		{
			return secProp != SecurityProp.FreeBusyNTSD || this.CoreFolder.PropertyBag.AllFoundProperties.Contains(this.Mailbox.ConvertPropTagsToDefinitions(new PropTag[]
			{
				PropTag.FreeBusyNTSD
			})[0]);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000042B5 File Offset: 0x000024B5
		private bool HasSecurityDescriptor(AclFlags aclFlags)
		{
			return this.HasSecurityDescriptor(aclFlags.HasFlag(AclFlags.FreeBusyAcl) ? SecurityProp.FreeBusyNTSD : SecurityProp.NTSD);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000042D4 File Offset: 0x000024D4
		private PropValueData[][] GetAcl(ModifyTableOptions options, PropTag[] propTags)
		{
			PropertyDefinition[] array = this.Mailbox.ConvertPropTagsToDefinitions(propTags);
			PropValueData[][] result;
			using (this.Mailbox.RHTracker.Start())
			{
				using (IModifyTable permissionTable = this.CoreFolder.GetPermissionTable(options))
				{
					using (IQueryResult queryResult = permissionTable.GetQueryResult(null, array))
					{
						PropValueData[][] array2 = new PropValueData[queryResult.EstimatedRowCount][];
						int num = 0;
						bool flag;
						do
						{
							object[][] rows = queryResult.GetRows(1000, out flag);
							foreach (object[] array4 in rows)
							{
								array2[num] = new PropValueData[array.Length];
								for (int j = 0; j < array.Length; j++)
								{
									array2[num][j] = new PropValueData(propTags[j], array4[j]);
								}
								num++;
							}
						}
						while (flag);
						result = array2;
					}
				}
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private static readonly PropTag[] ExtendedAclTableColumns = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.MemberId,
			PropTag.MemberRights,
			PropTag.MemberName,
			PropTag.MemberSecurityIdentifier,
			PropTag.MemberIsGroup
		};

		// Token: 0x04000005 RID: 5
		private static readonly PropTag[] AclTableColumns = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.MemberId,
			PropTag.MemberRights,
			PropTag.MemberName
		};

		// Token: 0x04000006 RID: 6
		private IFolder fxFolder;

		// Token: 0x04000007 RID: 7
		private RestrictionData contentsRestriction;
	}
}
