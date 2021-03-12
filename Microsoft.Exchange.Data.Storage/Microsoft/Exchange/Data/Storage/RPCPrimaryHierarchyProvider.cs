using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B4 RID: 180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RPCPrimaryHierarchyProvider
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000535F3 File Offset: 0x000517F3
		private ExchangePrincipal PrimaryHierarchyMailboxPrincipal
		{
			get
			{
				if (this.primaryHierarchyMailboxPrincipal == null && !PublicFolderSession.TryGetPublicFolderMailboxPrincipal(this.organizationId, PublicFolderSession.HierarchyMailboxGuidAlias, false, out this.primaryHierarchyMailboxPrincipal))
				{
					throw new ObjectNotFoundException(PublicFolderSession.GetNoPublicFoldersProvisionedError(this.organizationId));
				}
				return this.primaryHierarchyMailboxPrincipal;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0005362D File Offset: 0x0005182D
		public RPCPrimaryHierarchyProvider(OrganizationId organizationId, string userDn, ClientSecurityContext clientSecurityContext, Func<ExchangePrincipal, LegacyDN> getLegacyDN)
		{
			this.organizationId = organizationId;
			this.userDn = userDn;
			this.clientSecurityContext = clientSecurityContext;
			this.getLegacyDN = getLegacyDN;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00053654 File Offset: 0x00051854
		private MapiStore GetHierarchyStore()
		{
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			MapiStore result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = MapiStore.OpenPublicStore(this.getLegacyDN(this.PrimaryHierarchyMailboxPrincipal).ToString(), this.userDn, null, null, null, string.Format("{0}:{1}", this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn, 81), ConnectFlag.UseDelegatedAuthPrivilege | ConnectFlag.UseNTLM | ConnectFlag.ConnectToExchangeRpcServerOnly, OpenStoreFlag.Public, CultureInfo.CurrentCulture, PublicFolderSession.FromClientSecurityContext(this.organizationId, this.clientSecurityContext), "Client=MSExchangeRPC");
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.PrimaryHierarchyMailboxPrincipal.LegacyDn), ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PublicFolderRPCHierarchyProvider.GetHierarchyStore : ServerFullyQualifiedDomainName = {0}, UserLegacyDN = {1}.", this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn, this.userDn),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenMailbox(this.PrimaryHierarchyMailboxPrincipal.LegacyDn), ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("PublicFolderRPCHierarchyProvider.GetHierarchyStore : ServerFullyQualifiedDomainName = {0}, UserLegacyDN = {1}.", this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn, this.userDn),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00053844 File Offset: 0x00051A44
		public StoreId CreateFolder(string folderName, string folderDescription, StoreId parentFolderId, CreateMode mode, out Guid contentMailboxGuid)
		{
			StoreId result;
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, parentFolderId)))
						{
							using (MapiFolder mapiFolder2 = mapiFolder.CreateFolder(folderName, folderDescription, mode == CreateMode.OpenIfExists))
							{
								contentMailboxGuid = RPCPrimaryHierarchyProvider.GetMailboxGuidFromPersonalizedLegacyDN(mapiFolder2.GetReplicaServers()[0]);
								result = StoreObjectId.FromProviderSpecificId(mapiFolder2.GetProp(PropTag.EntryId).GetBytes(), StoreObjectType.Folder);
							}
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateFolder(folderName), ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.CreateFolder : folderName = {0}", folderName),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateFolder(folderName), ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.CreateFolder : folderName = {0}", folderName),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00053A90 File Offset: 0x00051C90
		public void DeleteFolder(StoreId parentFolderId, StoreId folderId, DeleteFolderFlags deleteFlags)
		{
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, parentFolderId)))
						{
							mapiFolder.DeleteFolder(this.GetDestinationSpecificEntryId(hierarchyStore, folderId), deleteFlags);
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.DeleteFolder : folderId = {0}", folderId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.DeleteFolder : folderId = {0}", folderId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00053C88 File Offset: 0x00051E88
		public void MoveFolder(StoreId parentFolderId, StoreId destinationFolderId, StoreId sourceFolderId, string newFolderName)
		{
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, parentFolderId)))
						{
							using (MapiFolder mapiFolder2 = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, destinationFolderId)))
							{
								mapiFolder.CopyFolder(CopyFolderFlags.FolderMove, mapiFolder2, this.GetDestinationSpecificEntryId(hierarchyStore, sourceFolderId), newFolderName);
							}
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.MoveFolder : folderId = {0}, ParentId = {1}, DestinationId = {2}", sourceFolderId, parentFolderId, destinationFolderId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.MoveFolder : folderId = {0}, ParentId = {1}, DestinationId = {2}", sourceFolderId, parentFolderId, destinationFolderId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00053EC0 File Offset: 0x000520C0
		public PropTag[] GetIDsFromNames(bool shouldCreate, ICollection<NamedProp> namedProps)
		{
			PropTag[] idsFromNames;
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						idsFromNames = hierarchyStore.GetIDsFromNames(shouldCreate, namedProps);
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.GetIDFromNames", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetIDFromNames, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.GetIDFromNames", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return idsFromNames;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00054038 File Offset: 0x00052238
		public PropProblem[] SetProperties(StoreId folderId, PropValue[] propertyValues, out Guid contentMailboxGuid)
		{
			PropProblem[] result;
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, folderId)))
						{
							contentMailboxGuid = RPCPrimaryHierarchyProvider.GetMailboxGuidFromPersonalizedLegacyDN(mapiFolder.GetReplicaServers()[0]);
							result = mapiFolder.SetProps(propertyValues, true);
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetProps, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.SetProperties : folderId = {0}", folderId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetProps, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.SetProperties : folderId = {0}", folderId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0005423C File Offset: 0x0005243C
		public PropProblem[] DeleteProperties(StoreId folderId, PropTag[] propertyTags, out Guid contentMailboxGuid)
		{
			PropProblem[] result;
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, folderId)))
						{
							contentMailboxGuid = RPCPrimaryHierarchyProvider.GetMailboxGuidFromPersonalizedLegacyDN(mapiFolder.GetReplicaServers()[0]);
							result = mapiFolder.DeleteProps(propertyTags, true);
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotDeleteProperties, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.DeleteProperties : folderId = {0}", folderId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotDeleteProperties, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.DeleteProperties : folderId = {0}", folderId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00054440 File Offset: 0x00052640
		public void ModifyPermissions(StoreId folderId, AclTableEntry.ModifyOperation[] modifyOperations, ModifyTableOptions options, bool replaceAllRows)
		{
			using (PublicFolderConnectionLimitsTracker.Instance.GetToken(this.PrimaryHierarchyMailboxPrincipal.MailboxInfo.Location.ServerFqdn))
			{
				StoreSession storeSession = null;
				object thisObject = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					using (MapiStore hierarchyStore = this.GetHierarchyStore())
					{
						using (MapiFolder mapiFolder = (MapiFolder)hierarchyStore.OpenEntry(this.GetDestinationSpecificEntryId(hierarchyStore, folderId)))
						{
							using (MapiModifyTable mapiModifyTable = (MapiModifyTable)mapiFolder.OpenProperty(PropTag.AclTable, InterfaceIds.IExchangeModifyTable, 0, OpenPropertyFlags.DeferredErrors))
							{
								GetTableFlags getTableFlags = GetTableFlags.None;
								ModifyTableFlags modifyTableFlags = ModifyTableFlags.None;
								if (options == ModifyTableOptions.FreeBusyAware)
								{
									getTableFlags |= GetTableFlags.FreeBusy;
									modifyTableFlags |= ModifyTableFlags.FreeBusy;
								}
								if (replaceAllRows)
								{
									modifyTableFlags |= ModifyTableFlags.RowListReplace;
								}
								using (MapiTable table = mapiModifyTable.GetTable(getTableFlags))
								{
									Dictionary<byte[], long> entryIdToMemberIdMap = RPCPrimaryHierarchyProvider.GetEntryIdToMemberIdMap(table.QueryAllRows(null, RPCPrimaryHierarchyProvider.MapiAclTableColumns));
									List<RowEntry> list = new List<RowEntry>(modifyOperations.Length);
									foreach (AclTableEntry.ModifyOperation modifyOperation in modifyOperations)
									{
										switch (modifyOperation.Operation)
										{
										case ModifyTableOperationType.Add:
											list.Add(RPCPrimaryHierarchyProvider.ConvertToRowEntry(modifyOperation));
											break;
										case ModifyTableOperationType.Modify:
										case ModifyTableOperationType.Remove:
										{
											AclTableEntry.ModifyOperation modifyOperation2 = modifyOperation;
											if (modifyOperation.Entry.MemberId != -1L && modifyOperation.Entry.MemberId != 0L)
											{
												if (entryIdToMemberIdMap.ContainsKey(modifyOperation.Entry.MemberEntryId))
												{
													modifyOperation2 = new AclTableEntry.ModifyOperation(modifyOperation.Operation, new AclTableEntry(entryIdToMemberIdMap[modifyOperation.Entry.MemberEntryId], null, null, modifyOperation.Entry.MemberRights));
												}
												else if (modifyOperation.Operation == ModifyTableOperationType.Modify)
												{
													modifyOperation2 = new AclTableEntry.ModifyOperation(ModifyTableOperationType.Add, new AclTableEntry(0L, modifyOperation.Entry.MemberEntryId, null, modifyOperation.Entry.MemberRights));
												}
												else
												{
													modifyOperation2 = null;
												}
											}
											if (modifyOperation2 != null)
											{
												list.Add(RPCPrimaryHierarchyProvider.ConvertToRowEntry(modifyOperation2));
											}
											break;
										}
										}
									}
									mapiModifyTable.ModifyTable(modifyTableFlags, list.ToArray());
								}
							}
						}
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.WriteAclPropertyStream : folderId = {0}", folderId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("RPCPrimaryHierarchyProvider.WriteAclPropertyStream : folderId = {0}", folderId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000547FC File Offset: 0x000529FC
		private static Dictionary<byte[], long> GetEntryIdToMemberIdMap(PropValue[][] aclRows)
		{
			Dictionary<byte[], long> dictionary = new Dictionary<byte[], long>(aclRows.GetLength(0), ArrayComparer<byte>.Comparer);
			foreach (PropValue[] array in aclRows)
			{
				byte[] key = null;
				long num = -1L;
				bool flag = false;
				bool flag2 = false;
				foreach (PropValue propValue in array)
				{
					PropTag propTag = propValue.PropTag;
					if (propTag != PropTag.EntryId)
					{
						if (propTag == PropTag.MemberId)
						{
							num = (long)propValue.Value;
							flag2 = true;
						}
					}
					else
					{
						key = (propValue.Value as byte[]);
						flag = true;
					}
				}
				if (flag && flag2 && num != -1L && num != 0L)
				{
					dictionary.Add(key, num);
				}
			}
			return dictionary;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000548C8 File Offset: 0x00052AC8
		private static RowEntry ConvertToRowEntry(AclTableEntry.ModifyOperation aclModifyOperation)
		{
			switch (aclModifyOperation.Operation)
			{
			case ModifyTableOperationType.Add:
				return RowEntry.Add(new PropValue[]
				{
					new PropValue(PropTag.EntryId, aclModifyOperation.Entry.MemberEntryId),
					new PropValue(PropTag.MemberRights, aclModifyOperation.Entry.MemberRights)
				});
			case ModifyTableOperationType.Modify:
				return RowEntry.Modify(new PropValue[]
				{
					new PropValue(PropTag.MemberId, aclModifyOperation.Entry.MemberId),
					new PropValue(PropTag.MemberRights, aclModifyOperation.Entry.MemberRights)
				});
			case ModifyTableOperationType.Remove:
				return RowEntry.Remove(new PropValue[]
				{
					new PropValue(PropTag.MemberId, aclModifyOperation.Entry.MemberId)
				});
			default:
				return RowEntry.Empty();
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000549DC File Offset: 0x00052BDC
		private byte[] GetDestinationSpecificEntryId(MapiStore hierarchyStore, StoreId folderId)
		{
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			byte[] result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = hierarchyStore.CreateEntryId(hierarchyStore.GetFidFromEntryId(StoreId.GetStoreObjectId(folderId).ProviderLevelItemId));
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RPCPrimaryHierarchyProvider.GetDestinationSpecificEntryId : folderId = {0}", folderId),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("RPCPrimaryHierarchyProvider.GetDestinationSpecificEntryId : folderId = {0}", folderId),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00054B00 File Offset: 0x00052D00
		private static Guid GetMailboxGuidFromPersonalizedLegacyDN(string personalizedMailboxLegacyDN)
		{
			Guid empty = Guid.Empty;
			LegacyDN legacyDN;
			if (!string.IsNullOrWhiteSpace(personalizedMailboxLegacyDN) && LegacyDN.TryParse(personalizedMailboxLegacyDN, out legacyDN))
			{
				string text;
				string address;
				legacyDN.GetParentLegacyDN(out text, out address);
				GuidHelper.TryParseGuid(new SmtpAddress(address).Local, out empty);
			}
			return empty;
		}

		// Token: 0x0400036B RID: 875
		private const int BackendHttpPort = 81;

		// Token: 0x0400036C RID: 876
		private static readonly PropTag[] MapiAclTableColumns = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.MemberId
		};

		// Token: 0x0400036D RID: 877
		private OrganizationId organizationId;

		// Token: 0x0400036E RID: 878
		private readonly string userDn;

		// Token: 0x0400036F RID: 879
		private readonly ClientSecurityContext clientSecurityContext;

		// Token: 0x04000370 RID: 880
		private Func<ExchangePrincipal, LegacyDN> getLegacyDN;

		// Token: 0x04000371 RID: 881
		private ExchangePrincipal primaryHierarchyMailboxPrincipal;
	}
}
