using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000158 RID: 344
	internal static class MapiUtils
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001B166 File Offset: 0x00019366
		public static byte[] MapiFolderObjectData
		{
			get
			{
				return MapiUtils.mapiFolderObjectData.Member;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001B172 File Offset: 0x00019372
		public static MapiStore GetSystemMailbox(Guid mdbGuid, string dcName, NetworkCredential cred)
		{
			return MapiUtils.GetSystemMailbox(mdbGuid, dcName, cred, true);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001B180 File Offset: 0x00019380
		public static MapiStore GetSystemMailbox(Guid mdbGuid, string dcName, NetworkCredential cred, bool allowCrossSiteLogon)
		{
			bool flag = false;
			ConnectFlag connectFlag = ConnectFlag.UseAdminPrivilege | ConnectFlag.AllowLegacyStore;
			string userName;
			string password;
			string domainName;
			TimeSpan connectionTimeout;
			TimeSpan callTimeout;
			if (cred != null)
			{
				userName = cred.UserName;
				password = cred.Password;
				domainName = cred.Domain;
				connectionTimeout = TestIntegration.Instance.RemoteMailboxConnectionTimeout;
				callTimeout = TestIntegration.Instance.RemoteMailboxCallTimeout;
			}
			else
			{
				userName = null;
				password = null;
				domainName = null;
				connectFlag |= ConnectFlag.UseRpcContextPool;
				connectionTimeout = TestIntegration.Instance.LocalMailboxConnectionTimeout;
				callTimeout = TestIntegration.Instance.LocalMailboxCallTimeout;
			}
			DatabaseInformation databaseInformation;
			for (;;)
			{
				FindServerFlags findServerFlags = FindServerFlags.FindSystemMailbox;
				if (flag)
				{
					findServerFlags |= FindServerFlags.ForceRediscovery;
				}
				databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, dcName, cred, findServerFlags);
				if (!allowCrossSiteLogon && !databaseInformation.IsInLocalSite)
				{
					break;
				}
				MapiStore result;
				try
				{
					MapiStore mapiStore = MapiStore.OpenMailbox(databaseInformation.ServerFqdn, Server.GetSystemAttendantLegacyDN(databaseInformation.ServerDN), databaseInformation.SystemMailboxGuid, mdbGuid, userName, domainName, password, connectFlag, OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership | OpenStoreFlag.MailboxGuid, CultureInfo.InvariantCulture, null, "Client=MSExchangeMigration", connectionTimeout, callTimeout, null);
					MapiUtils.StartMapiDeadSessionChecking(mapiStore, mdbGuid.ToString());
					result = mapiStore;
				}
				catch (MapiExceptionWrongServer)
				{
					if (!flag)
					{
						MrsTracer.Common.Debug("OpenMailbox returned WrongServer, forcing AM rediscovery", new object[0]);
						flag = true;
						continue;
					}
					throw;
				}
				catch (MapiExceptionLogonFailed)
				{
					if (!flag)
					{
						MrsTracer.Common.Debug("OpenMailbox returned LogonFailed, forcing AM rediscovery", new object[0]);
						flag = true;
						continue;
					}
					throw;
				}
				catch (MapiExceptionUnknownUser)
				{
					if (!flag)
					{
						MrsTracer.Common.Debug("OpenMailbox returned UnknownUser, forcing AM rediscovery", new object[0]);
						flag = true;
						continue;
					}
					throw;
				}
				return result;
			}
			throw new CrossSiteLogonTransientException(mdbGuid, databaseInformation.ServerGuid, databaseInformation.ServerSite.ToString(), CommonUtils.LocalSiteId.ToString());
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001B344 File Offset: 0x00019544
		public static byte[] CreateObjectData(Guid objectType)
		{
			byte[] exchangeVersionBlob = new byte[]
			{
				(byte)VersionInformation.MRS.ProductMinor,
				(byte)VersionInformation.MRS.ProductMajor,
				(byte)(VersionInformation.MRS.BuildMajor % 256),
				(byte)(VersionInformation.MRS.BuildMajor / 256 + 128),
				(byte)(VersionInformation.MRS.BuildMinor % 256),
				(byte)(VersionInformation.MRS.BuildMinor / 256)
			};
			return BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(objectType);
				serializer.Write(exchangeVersionBlob);
			});
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001B3F1 File Offset: 0x000195F1
		public static MapiStore GetSystemMailbox(Guid mdbGuid)
		{
			return MapiUtils.GetSystemMailbox(mdbGuid, null, null);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001B3FB File Offset: 0x000195FB
		public static MapiStore GetSystemMailbox(Guid mdbGuid, bool allowCrossSiteLogon)
		{
			return MapiUtils.GetSystemMailbox(mdbGuid, null, null, allowCrossSiteLogon);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001B408 File Offset: 0x00019608
		public static MapiStore GetUserMailbox(Guid mailboxGuid, Guid mdbGuid, UserMailboxFlags umFlags)
		{
			OpenStoreFlag openStoreFlag = OpenStoreFlag.UseAdminPrivilege | OpenStoreFlag.TakeOwnership | OpenStoreFlag.NoLocalization | OpenStoreFlag.MailboxGuid;
			ConnectFlag connectFlags = ConnectFlag.UseAdminPrivilege | ConnectFlag.UseRpcContextPool | ConnectFlag.AllowLegacyStore;
			if (umFlags.HasFlag(UserMailboxFlags.RecoveryMDB))
			{
				openStoreFlag |= OpenStoreFlag.RestoreDatabase;
			}
			if (umFlags.HasFlag(UserMailboxFlags.Disconnected) || umFlags.HasFlag(UserMailboxFlags.SoftDeleted) || umFlags.HasFlag(UserMailboxFlags.MoveDestination))
			{
				openStoreFlag |= (OpenStoreFlag.OverrideHomeMdb | OpenStoreFlag.DisconnectedMailbox);
			}
			bool flag = false;
			TimeSpan localMailboxConnectionTimeout = TestIntegration.Instance.LocalMailboxConnectionTimeout;
			TimeSpan localMailboxCallTimeout = TestIntegration.Instance.LocalMailboxCallTimeout;
			MapiStore result;
			for (;;)
			{
				FindServerFlags findServerFlags = FindServerFlags.None;
				if (flag)
				{
					findServerFlags |= FindServerFlags.ForceRediscovery;
				}
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, findServerFlags);
				try
				{
					MrsTracer.Common.Debug("Opening MapiStore: serverFqdn=\"{0}\", mailboxGuid=\"{1}\", mdbGuid=\"{2}\", flags=[{3}].", new object[]
					{
						databaseInformation.ServerFqdn,
						mailboxGuid,
						mdbGuid,
						openStoreFlag
					});
					MapiStore mapiStore = MapiStore.OpenMailbox(databaseInformation.ServerFqdn, Server.GetSystemAttendantLegacyDN(databaseInformation.ServerDN), mailboxGuid, mdbGuid, null, null, null, connectFlags, openStoreFlag, null, null, "Client=MSExchangeMigration", localMailboxConnectionTimeout, localMailboxCallTimeout, null);
					MapiUtils.StartMapiDeadSessionChecking(mapiStore, mailboxGuid.ToString());
					result = mapiStore;
				}
				catch (MapiExceptionWrongServer)
				{
					if (!flag)
					{
						MrsTracer.Common.Debug("OpenMailbox returned WrongServer, forcing AM rediscovery", new object[0]);
						flag = true;
						continue;
					}
					throw;
				}
				catch (MapiExceptionLogonFailed)
				{
					if (!flag)
					{
						MrsTracer.Common.Debug("OpenMailbox returned LogonFailed, forcing AM rediscovery", new object[0]);
						flag = true;
						continue;
					}
					throw;
				}
				break;
			}
			return result;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001B59C File Offset: 0x0001979C
		public static DatabaseInformation FindServerForMdb(Guid mdbGuid, string dcName, NetworkCredential cred, FindServerFlags flags)
		{
			Guid systemMailboxGuid = Guid.Empty;
			if (flags.HasFlag(FindServerFlags.FindSystemMailbox))
			{
				systemMailboxGuid = MapiUtils.GetSystemMailboxGuid(mdbGuid, dcName, cred, flags);
			}
			if (cred == null)
			{
				try
				{
					GetServerForDatabaseFlags getServerForDatabaseFlags = GetServerForDatabaseFlags.IgnoreAdSiteBoundary;
					if (flags.HasFlag(FindServerFlags.ForceRediscovery))
					{
						MrsTracer.Common.Debug("Looking up MDB {0} with rediscovery", new object[]
						{
							mdbGuid
						});
						getServerForDatabaseFlags |= GetServerForDatabaseFlags.ReadThrough;
					}
					DatabaseLocationInfo serverForDatabase = ActiveManager.GetCachingActiveManagerInstance().GetServerForDatabase(mdbGuid, getServerForDatabaseFlags);
					if (serverForDatabase != null)
					{
						return DatabaseInformation.FromDatabaseLocationInfo(mdbGuid, serverForDatabase, systemMailboxGuid);
					}
				}
				catch (ObjectNotFoundException)
				{
				}
				MrsTracer.Common.Debug("ActiveManager was unable to locate MDB {0}, will search AD instead.", new object[]
				{
					mdbGuid
				});
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(dcName, true, ConsistencyMode.PartiallyConsistent, cred, ADSessionSettings.FromRootOrgScopeSet(), 686, "FindServerForMdb", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MapiUtils.cs");
			Database database = topologyConfigurationSession.FindDatabaseByGuid<Database>(mdbGuid);
			if (database == null)
			{
				MrsTracer.Common.Error("Unable to locate MDB by guid {0}", new object[]
				{
					mdbGuid
				});
				if (!flags.HasFlag(FindServerFlags.AllowMissing))
				{
					throw new DatabaseNotFoundByGuidPermanentException(mdbGuid);
				}
				return DatabaseInformation.Missing(mdbGuid, PartitionId.LocalForest.ForestFQDN);
			}
			else
			{
				PropertyDefinition[] properties = new PropertyDefinition[]
				{
					ServerSchema.ExchangeLegacyDN,
					ServerSchema.Fqdn,
					ServerSchema.ServerSite,
					ServerSchema.VersionNumber,
					ActiveDirectoryServerSchema.MailboxRelease
				};
				MiniServer miniServer = topologyConfigurationSession.ReadMiniServer(database.Server, properties);
				if (miniServer != null)
				{
					return DatabaseInformation.FromAD(database, miniServer, systemMailboxGuid);
				}
				MrsTracer.Common.Error("Unable to locate DB server {0}", new object[]
				{
					database.Server.DistinguishedName
				});
				if ((flags & FindServerFlags.AllowMissing) == FindServerFlags.None)
				{
					throw new UnexpectedErrorPermanentException(-2147221233);
				}
				return DatabaseInformation.Missing(mdbGuid, PartitionId.LocalForest.ForestFQDN);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001B788 File Offset: 0x00019988
		public static DatabaseInformation FindServerForMdb(ADObjectId database, string dcName, NetworkCredential cred, FindServerFlags flags)
		{
			if (!ConfigBase<MRSConfigSchema>.GetConfig<bool>("CrossResourceForestEnabled"))
			{
				return MapiUtils.FindServerForMdb(database.ObjectGuid, dcName, cred, flags);
			}
			Guid empty = Guid.Empty;
			DatabaseInformation result;
			try
			{
				if (database.GetPartitionId().IsLocalForestPartition())
				{
					result = MapiUtils.FindServerForMdb(database.ObjectGuid, dcName, cred, flags);
				}
				else
				{
					BackEndServer backEndServer = BackEndLocator.GetBackEndServer(database);
					result = DatabaseInformation.FromBackEndServer(database, backEndServer);
				}
			}
			catch (BackEndLocatorException)
			{
				MrsTracer.Common.Debug("BE Locator was unable to locate MDB {0}.", new object[]
				{
					database.ObjectGuid
				});
				if ((flags & FindServerFlags.AllowMissing) == FindServerFlags.None)
				{
					throw;
				}
				result = DatabaseInformation.Missing(database.ObjectGuid, database.PartitionFQDN);
			}
			return result;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001B838 File Offset: 0x00019A38
		public static Server FindServerByGuid(Guid serverGuid, string dcName, NetworkCredential cred)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(dcName, true, ConsistencyMode.PartiallyConsistent, cred, ADSessionSettings.FromRootOrgScopeSet(), 785, "FindServerByGuid", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MapiUtils.cs");
			return topologyConfigurationSession.Read<Server>(new ADObjectId(serverGuid));
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001B874 File Offset: 0x00019A74
		public static MiniServer FindMiniServerByFqdn(ITopologyConfigurationSession adSession, string serverFqdn)
		{
			return adSession.FindMiniServerByFqdn(serverFqdn, MapiUtils.MiniServerProperties);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001B882 File Offset: 0x00019A82
		public static DateTime GetDateTimeOrDefault(PropValue pv)
		{
			if (pv.IsError() || pv.IsNull() || pv.Value == null)
			{
				return DateTime.MinValue;
			}
			return pv.GetDateTime();
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001B8AC File Offset: 0x00019AAC
		public static InboundConversionOptions GetScopedInboundConversionOptions(IRecipientSession recipientSession)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			return new InboundConversionOptions(recipientSession)
			{
				IsSenderTrusted = true,
				ServerSubmittedSecurely = true,
				RecipientCache = null,
				ClearCategories = true,
				Limits = 
				{
					MimeLimits = MimeLimits.Unlimited
				},
				ApplyHeaderFirewall = true
			};
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001B904 File Offset: 0x00019B04
		public static MapiFolder OpenFolderUnderRoot(MapiStore mailbox, string folderName, bool createIfMissing)
		{
			MapiFolder result;
			using (MapiFolder nonIpmSubtreeFolder = mailbox.GetNonIpmSubtreeFolder())
			{
				MapiFolder mapiFolder = null;
				try
				{
					mapiFolder = nonIpmSubtreeFolder.OpenSubFolderByName(folderName);
				}
				catch (MapiExceptionNotFound)
				{
				}
				if (mapiFolder == null && createIfMissing)
				{
					MrsTracer.Common.Debug("Folder '{0}' does not exist, creating it.", new object[]
					{
						folderName
					});
					try
					{
						mapiFolder = nonIpmSubtreeFolder.CreateFolder(folderName, null, false);
						MrsTracer.Common.Debug("Created '{0}' folder.", new object[]
						{
							folderName
						});
					}
					catch (MapiExceptionCollision)
					{
						MrsTracer.Common.Debug("Somebody beat us to creating the folder. Will attempt to open it.", new object[0]);
					}
					if (mapiFolder == null)
					{
						mapiFolder = nonIpmSubtreeFolder.OpenSubFolderByName(folderName);
						MrsTracer.Common.Debug("Opened '{0}' folder.", new object[]
						{
							folderName
						});
					}
				}
				result = mapiFolder;
			}
			return result;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		public static void ProcessMapiCallInBatches<T>(T[] data, Action<T[]> processBatch)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int i = 0;
			int num = data.Length;
			while (i < data.Length)
			{
				T[] array = new T[num];
				Array.Copy(data, i, array, 0, num);
				try
				{
					processBatch(array);
					i += num;
					if (i + num > data.Length)
					{
						num = data.Length - i;
					}
				}
				catch (Exception ex)
				{
					if (!CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
					{
						WellKnownException.MapiRpcBufferTooSmall,
						WellKnownException.MapiNotEnoughMemory
					}))
					{
						throw;
					}
					MrsTracer.Common.Warning("Unable to process batch with {0} elements", new object[]
					{
						num
					});
					if (num <= 1)
					{
						MrsTracer.Common.Error("Unable to process batch with a single element! {0}", new object[]
						{
							CommonUtils.FullExceptionMessage(ex)
						});
						throw;
					}
					num = (num + 1) / 2;
				}
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public static void RetryOnObjectChanged(Action retryableOperation)
		{
			for (int i = 1; i < 3; i++)
			{
				try
				{
					retryableOperation();
					return;
				}
				catch (Exception ex)
				{
					if (!CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
					{
						WellKnownException.MapiObjectChanged,
						WellKnownException.MapiNoAccess
					}))
					{
						throw;
					}
					MrsTracer.Common.Warning("MapiUtils.RetryOnObjectChanged: got object changed or no access, will retry ({0}/{1}) {2}", new object[]
					{
						i,
						3,
						CommonUtils.FullExceptionMessage(ex)
					});
				}
			}
			retryableOperation();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001BB64 File Offset: 0x00019D64
		public static Guid GetSystemMailboxGuid(Guid mdbGuid, string dcName, NetworkCredential cred, FindServerFlags flags)
		{
			Guid guid = Guid.Empty;
			if ((flags & FindServerFlags.ForceRediscovery) == FindServerFlags.None)
			{
				lock (MapiUtils.syncRoot)
				{
					if (MapiUtils.mdbToSystemMailboxMap.TryGetValue(mdbGuid, out guid))
					{
						return guid;
					}
				}
			}
			try
			{
				ADSystemMailbox adsystemMailbox = MapiUtils.GetADSystemMailbox(mdbGuid, dcName, cred);
				guid = adsystemMailbox.ExchangeGuid;
			}
			catch (SystemMailboxNotFoundPermanentException)
			{
				if ((flags & FindServerFlags.AllowMissing) == FindServerFlags.None)
				{
					throw;
				}
				return Guid.Empty;
			}
			lock (MapiUtils.syncRoot)
			{
				MapiUtils.mdbToSystemMailboxMap[mdbGuid] = guid;
			}
			return guid;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001BC28 File Offset: 0x00019E28
		public static ADSystemMailbox GetADSystemMailbox(Guid mdbGuid, string dcName, NetworkCredential cred)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(dcName, true, ConsistencyMode.PartiallyConsistent, cred, ADSessionSettings.FromRootOrgScopeSet(), 1080, "GetADSystemMailbox", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MapiUtils.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			string text = string.Format(CultureInfo.InvariantCulture, "SystemMailbox{{{0}}}", new object[]
			{
				mdbGuid.ToString()
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
			ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, filter, null, 2);
			if (array.Length != 1)
			{
				MrsTracer.Common.Error("Found {0} mailboxes named '{1}' in the AD", new object[]
				{
					array.Length,
					text
				});
				throw new SystemMailboxNotFoundPermanentException(mdbGuid.ToString());
			}
			ADSystemMailbox adsystemMailbox = array[0] as ADSystemMailbox;
			if (adsystemMailbox == null)
			{
				throw new SystemMailboxNotFoundPermanentException(text);
			}
			return adsystemMailbox;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001BD04 File Offset: 0x00019F04
		public static void ExportMessagesWithBadItemDetection(ISourceMailbox mailbox, List<MessageRec> messages, Func<IFxProxyPool> getProxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps, TestIntegration testIntegration, ref List<BadMessageRec> badMessages)
		{
			MrsTracer.Common.Debug("ExportMessagesWithBadItemDetection: Exporting {0} messages", new object[]
			{
				messages.Count
			});
			Exception ex;
			if (MapiUtils.ExportMessageBatch(mailbox, messages, getProxyPool, flags, propsToCopyExplicitly, excludeProps, testIntegration, out ex))
			{
				return;
			}
			MrsTracer.Common.Warning("ExportMessages failed with a bad item error. Current batch count {0}. Will retry copying messages in smaller batches. {1}", new object[]
			{
				messages.Count,
				CommonUtils.FullExceptionMessage(ex)
			});
			if (messages.Count != 1)
			{
				int num = messages.Count / 2;
				List<MessageRec> list = new List<MessageRec>(num);
				List<MessageRec> list2 = new List<MessageRec>(messages.Count - num);
				for (int i = 0; i < messages.Count; i++)
				{
					if (i < num)
					{
						list.Add(messages[i]);
					}
					else
					{
						list2.Add(messages[i]);
					}
				}
				MapiUtils.ExportMessagesWithBadItemDetection(mailbox, list, getProxyPool, flags, propsToCopyExplicitly, excludeProps, testIntegration, ref badMessages);
				MapiUtils.ExportMessagesWithBadItemDetection(mailbox, list2, getProxyPool, flags, propsToCopyExplicitly, excludeProps, testIntegration, ref badMessages);
				return;
			}
			MessageRec messageRec = messages[0];
			MrsTracer.Common.Warning("Single message {0} copy failed. Error {1}", new object[]
			{
				TraceUtils.DumpEntryId(messageRec.EntryId),
				CommonUtils.FullExceptionMessage(ex)
			});
			EntryIdMap<MessageRec> entryIdMap;
			EntryIdMap<FolderRec> entryIdMap2;
			MapiUtils.LookupBadMessagesInMailbox(mailbox, messages, out entryIdMap, out entryIdMap2);
			MessageRec msgData;
			if (!entryIdMap.TryGetValue(messageRec.EntryId, out msgData))
			{
				badMessages.Add(BadMessageRec.MissingItem(messageRec));
				return;
			}
			DownlevelBadItemsPermanentException ex2 = ex as DownlevelBadItemsPermanentException;
			if (ex2 != null)
			{
				badMessages.Add(ex2.BadItems[0]);
				return;
			}
			FolderRec folderRec = entryIdMap2[messageRec.FolderId];
			badMessages.Add(BadMessageRec.Item(msgData, folderRec, ex));
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		public static T GetValue<T>(PropValue pv, T defaultValue)
		{
			object obj = null;
			PropType propType = pv.PropType;
			if (propType <= PropType.String)
			{
				if (propType != PropType.Int)
				{
					if (propType != PropType.Boolean)
					{
						if (propType == PropType.String)
						{
							obj = pv.GetString();
						}
					}
					else
					{
						obj = pv.GetBoolean();
					}
				}
				else
				{
					obj = pv.GetInt();
				}
			}
			else if (propType != PropType.SysTime)
			{
				if (propType != PropType.Guid)
				{
					if (propType == PropType.Binary)
					{
						obj = pv.GetBytes();
						if (typeof(T) == typeof(Guid))
						{
							byte[] array = obj as byte[];
							if (array != null && array.Length == 16)
							{
								obj = new Guid(array);
							}
							else
							{
								obj = null;
							}
						}
					}
				}
				else
				{
					obj = pv.GetGuid();
				}
			}
			else
			{
				obj = pv.GetDateTime();
			}
			if (obj == null)
			{
				return defaultValue;
			}
			T result;
			try
			{
				result = (T)((object)obj);
			}
			catch (InvalidCastException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		public static byte[] GetSignatureBytes(MapiStore systemMailbox)
		{
			byte[] result = null;
			using (MapiFolder rootFolder = systemMailbox.GetRootFolder())
			{
				result = (rootFolder.GetProp(PropTag.MappingSignature).Value as byte[]);
			}
			return result;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001C000 File Offset: 0x0001A200
		public static void LookupBadMessagesInMailbox(ISourceMailbox mailbox, List<MessageRec> messages, out EntryIdMap<MessageRec> lookedUpMsgs, out EntryIdMap<FolderRec> folderRecs)
		{
			EntryIdMap<EntryIdMap<MessageRec>> entryIdMap = new EntryIdMap<EntryIdMap<MessageRec>>();
			folderRecs = new EntryIdMap<FolderRec>();
			lookedUpMsgs = new EntryIdMap<MessageRec>();
			foreach (MessageRec messageRec in messages)
			{
				EntryIdMap<MessageRec> entryIdMap2;
				if (!entryIdMap.TryGetValue(messageRec.FolderId, out entryIdMap2))
				{
					entryIdMap2 = new EntryIdMap<MessageRec>();
					entryIdMap.Add(messageRec.FolderId, entryIdMap2);
				}
				entryIdMap2[messageRec.EntryId] = null;
			}
			MrsTracer.Common.Debug("Looking up {0} messages in {1} folders.", new object[]
			{
				messages.Count,
				entryIdMap.Count
			});
			foreach (KeyValuePair<byte[], EntryIdMap<MessageRec>> keyValuePair in entryIdMap)
			{
				using (ISourceFolder folder = mailbox.GetFolder(keyValuePair.Key))
				{
					if (folder == null)
					{
						MrsTracer.Common.Warning("Folder {0} disappeared.", new object[]
						{
							TraceUtils.DumpEntryId(keyValuePair.Key)
						});
					}
					else
					{
						FolderRec folderRec = folder.GetFolderRec(null, GetFolderRecFlags.None);
						folderRecs[folderRec.EntryId] = folderRec;
						EntryIdMap<MessageRec> value = keyValuePair.Value;
						MrsTracer.Common.Debug("Looking up {0} messages in folder '{1}'.", new object[]
						{
							value.Count,
							folderRec.FolderName
						});
						int num = 0;
						if (mailbox.IsCapabilitySupported(MRSProxyCapabilities.SimpleExport))
						{
							List<byte[]> keysToLookup = new List<byte[]>(value.Keys);
							List<MessageRec> list = folder.LookupMessages(PropTag.EntryId, keysToLookup, BadMessageRec.BadItemPtags);
							if (list == null)
							{
								goto IL_230;
							}
							using (List<MessageRec>.Enumerator enumerator3 = list.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									MessageRec messageRec2 = enumerator3.Current;
									lookedUpMsgs[messageRec2.EntryId] = messageRec2;
									num++;
								}
								goto IL_230;
							}
						}
						List<MessageRec> list2 = folder.EnumerateMessages(EnumerateMessagesFlags.RegularMessages, BadMessageRec.BadItemPtags);
						foreach (MessageRec messageRec3 in list2)
						{
							if (value.ContainsKey(messageRec3.EntryId))
							{
								lookedUpMsgs[messageRec3.EntryId] = messageRec3;
								num++;
							}
						}
						IL_230:
						MrsTracer.Common.Debug("Found {0} messages.", new object[]
						{
							num
						});
					}
				}
			}
			MrsTracer.Common.Debug("Looked up {0} messages.", new object[]
			{
				lookedUpMsgs.Count
			});
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001C330 File Offset: 0x0001A530
		public static bool IsStoreDisconnectedMailbox(ExRpcAdmin rpcAdmin, Guid mdbGuid, Guid mailboxGuid)
		{
			if (MapiUtils.IsStoreDisconnectedMailboxInternal(rpcAdmin, mdbGuid, mailboxGuid))
			{
				return true;
			}
			rpcAdmin.SyncMailboxWithDS(mdbGuid, mailboxGuid);
			return MapiUtils.IsStoreDisconnectedMailboxInternal(rpcAdmin, mdbGuid, mailboxGuid);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001C350 File Offset: 0x0001A550
		public static bool IsMailboxInDatabase(ExRpcAdmin rpcAdmin, Guid mdbGuid, Guid mailboxGuid)
		{
			PropValue[][] mailboxTableInfo;
			try
			{
				mailboxTableInfo = rpcAdmin.GetMailboxTableInfo(mdbGuid, mailboxGuid, new PropTag[]
				{
					PropTag.UserGuid,
					PropTag.MailboxMiscFlags
				});
			}
			catch (MapiExceptionNotFound)
			{
				return false;
			}
			if (mailboxTableInfo != null)
			{
				foreach (PropValue[] array2 in mailboxTableInfo)
				{
					if (array2 != null && array2.Length == 2 && array2[0].PropTag == PropTag.UserGuid)
					{
						byte[] bytes = array2[0].GetBytes();
						Guid a = (bytes != null && bytes.Length == 16) ? new Guid(bytes) : Guid.Empty;
						if (a == mailboxGuid)
						{
							MailboxMiscFlags mailboxMiscFlags = (MailboxMiscFlags)((array2[1].PropTag == PropTag.MailboxMiscFlags) ? array2[1].GetInt() : 0);
							if ((mailboxMiscFlags & MailboxMiscFlags.SoftDeletedMailbox) == MailboxMiscFlags.None)
							{
								return true;
							}
							return false;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0001C44C File Offset: 0x0001A64C
		public static MailboxMiscFlags GetMailboxTableFlags(ExRpcAdmin rpcAdmin, Guid mdbGuid, Guid mailboxGuid)
		{
			MailboxMiscFlags result = MailboxMiscFlags.None;
			try
			{
				PropValue[][] mailboxTableInfo = rpcAdmin.GetMailboxTableInfo(mdbGuid, mailboxGuid, new PropTag[]
				{
					PropTag.UserGuid,
					PropTag.MailboxMiscFlags
				});
				if (mailboxTableInfo != null)
				{
					foreach (PropValue[] array2 in mailboxTableInfo)
					{
						if (array2 != null && array2.Length == 2 && array2[0].PropTag == PropTag.UserGuid)
						{
							byte[] bytes = array2[0].GetBytes();
							Guid a = (bytes != null && bytes.Length == 16) ? new Guid(bytes) : Guid.Empty;
							if (a == mailboxGuid)
							{
								result = (MailboxMiscFlags)((array2[1].PropTag == PropTag.MailboxMiscFlags) ? array2[1].GetInt() : 0);
							}
						}
					}
				}
			}
			catch (MapiExceptionNotFound)
			{
			}
			return result;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		public static List<Guid> GetDatabasesOnThisServer()
		{
			if (MapiUtils.localMDBs != null && DateTime.UtcNow - MapiUtils.LocalMDBRefreshInterval < MapiUtils.lastLocalMDBRefresh)
			{
				return MapiUtils.localMDBs;
			}
			List<Guid> list = new List<Guid>();
			MdbStatus[] mdbStatuses = null;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=MSExchangeMigration", null, null, null, null))
				{
					mdbStatuses = exRpcAdmin.ListMdbStatus(true);
				}
			}, delegate(Exception failure)
			{
				LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
				CommonUtils.LogEvent(MRSEventLogConstants.Tuple_UnableToDetermineHostedMdbsOnServer, new object[]
				{
					CommonUtils.LocalComputerName,
					localizedString.ToString()
				});
				return false;
			});
			if (mdbStatuses == null)
			{
				MrsTracer.Common.Debug("MapiUtils.GetDatabasesOnServer() returned null.", new object[0]);
				return list;
			}
			foreach (MdbStatus mdbStatus in mdbStatuses)
			{
				if (mdbStatus.Status.HasFlag(MdbStatusFlags.Online))
				{
					list.Add(mdbStatus.MdbGuid);
				}
			}
			if (list.Count == 0)
			{
				MrsTracer.Common.Debug("MapiUtils.GetDatabasesOnServer() found {0} databases, but none of them are online.", new object[]
				{
					mdbStatuses.Length
				});
			}
			foreach (Guid mdbGuid in list)
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.AllowMissing);
				if (!databaseInformation.IsMissing && !databaseInformation.IsOnThisServer)
				{
					databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.ForceRediscovery | FindServerFlags.AllowMissing);
				}
			}
			MapiUtils.localMDBs = list;
			MapiUtils.lastLocalMDBRefresh = DateTime.UtcNow;
			return list;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0001C740 File Offset: 0x0001A940
		public static PropValue[][] QueryAllRows(MapiTable msgTable, Restriction restriction, ICollection<PropTag> propTags)
		{
			return MapiUtils.QueryAllRows(msgTable, restriction, propTags, 1000);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001C750 File Offset: 0x0001A950
		public static PropValue[][] QueryAllRows(MapiTable msgTable, Restriction restriction, ICollection<PropTag> propTags, int pageSize)
		{
			MapiUtils.InitQueryAllRows(msgTable, restriction, propTags);
			List<PropValue[]> list = new List<PropValue[]>();
			for (;;)
			{
				PropValue[][] array = msgTable.QueryRows(pageSize);
				if (array.GetLength(0) == 0)
				{
					break;
				}
				list.AddRange(array);
			}
			return list.ToArray();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001C78D File Offset: 0x0001A98D
		public static void InitQueryAllRows(MapiTable msgTable, Restriction restriction, ICollection<PropTag> propTags)
		{
			if (restriction != null)
			{
				msgTable.Restrict(restriction);
			}
			msgTable.SeekRow(BookMark.Beginning, 0);
			msgTable.SetColumns(propTags);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public static NativeStorePropertyDefinition[] ConvertPropTagsToDefinitions(StoreSession storeSession, params PropTag[] propTags)
		{
			if (propTags == null || propTags.Length == 0)
			{
				return Array<NativeStorePropertyDefinition>.Empty;
			}
			PropertyTag[] array = new PropertyTag[propTags.Length];
			for (int i = 0; i < propTags.Length; i++)
			{
				uint num = (uint)propTags[i];
				array[i] = PropertyConverter.Folder.ConvertPropertyTagFromClient(new PropertyTag(num));
				if (num != array[i])
				{
					MrsTracer.Common.Debug("StorageMailbox: Property 0x{0:x} is converted to 0x{1:x}", new object[]
					{
						num,
						array[i]
					});
				}
			}
			NativeStorePropertyDefinition[] propertyDefinitionsIgnoreTypeChecking;
			try
			{
				propertyDefinitionsIgnoreTypeChecking = MEDSPropertyTranslator.GetPropertyDefinitionsIgnoreTypeChecking(storeSession, storeSession.Mailbox.CoreObject.PropertyBag, array);
			}
			catch (RopExecutionException ex)
			{
				ResolvePropertyDefinitionException ex2 = ex.InnerException as ResolvePropertyDefinitionException;
				if (ex2 != null)
				{
					throw new PropTagToPropertyDefinitionConversionException((int)ex2.UnresolvablePropertyTag);
				}
				throw;
			}
			return propertyDefinitionsIgnoreTypeChecking;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001C934 File Offset: 0x0001AB34
		public static void StartMapiDeadSessionChecking(MapiStore store, string mailboxId)
		{
			if (store == null)
			{
				return;
			}
			CommonUtils.ProcessKnownExceptions(delegate
			{
				store.Advise(null, AdviseFlags.Extended, new MapiNotificationHandler(MapiUtils.OnDroppedMapiSessionNotify), NotificationCallbackMode.Async, MapiNotificationClientFlags.AutoDisposeNotificationHandles);
				store.Advise(null, AdviseFlags.ConnectionDropped, new MapiNotificationHandler(MapiUtils.OnDroppedMapiSessionNotify), NotificationCallbackMode.Async, MapiNotificationClientFlags.AutoDisposeNotificationHandles);
			}, delegate(Exception ex)
			{
				MrsTracer.Provider.Warning("Failed to subscribe to Store session dead notification for {0}. Failure: {1}.", new object[]
				{
					mailboxId,
					CommonUtils.FullExceptionMessage(ex)
				});
				return true;
			});
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001C97C File Offset: 0x0001AB7C
		public static bool IsBadItemIndicator(Exception ex)
		{
			Exception ex2;
			return MapiUtils.IsBadItemIndicator(ex, out ex2);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001C994 File Offset: 0x0001AB94
		private static bool IsBadItemIndicator(Exception ex, out Exception actualException)
		{
			actualException = ex;
			if (ex is DownlevelBadItemsPermanentException)
			{
				return true;
			}
			bool flag = ExecutionContext.GetExceptionSide(ex) == ExceptionSide.Target;
			if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
			{
				WellKnownException.MapiImportFailure
			}))
			{
				actualException = ex.InnerException;
				ex = actualException;
			}
			if (CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
			{
				WellKnownException.MRS,
				WellKnownException.AD,
				WellKnownException.CommandExecution,
				WellKnownException.Mapi,
				WellKnownException.Storage,
				WellKnownException.FxParser
			}))
			{
				if (flag)
				{
					if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
					{
						WellKnownException.MapiVersion
					}) && CommonUtils.GetMapiLowLevelError(ex) == 1721)
					{
						MrsTracer.Common.Warning("Import failed with MapiExceptionVersion/ecNoServerSupport, treating this failure as a bad item.", new object[0]);
						return true;
					}
					if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
					{
						WellKnownException.CorruptData
					}))
					{
						MrsTracer.Common.Warning("Import failed with CorruptData, treating this failure as a bad item.", new object[0]);
						return true;
					}
				}
				return !CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
				{
					WellKnownException.Transient,
					WellKnownException.DataProviderTransient,
					WellKnownException.ADTransient,
					WellKnownException.MapiMdbOffline,
					WellKnownException.MapiCannotRegisterMapping,
					WellKnownException.MapiMaxObjectsExceeded,
					WellKnownException.MapiShutoffQuotaExceeded
				});
			}
			return CommonUtils.ExceptionIs(ex, new WellKnownException[]
			{
				WellKnownException.MRSUnableToReadPSTMessage
			}) || CommonUtils.ExceptionIs(ex, new WellKnownException[]
			{
				WellKnownException.MRSUnableToGetPSTProps
			}) || CommonUtils.ExceptionIs(ex, new WellKnownException[]
			{
				WellKnownException.MRSUnableToFetchEasMessage
			});
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0001CB24 File Offset: 0x0001AD24
		private static bool ExportMessageBatch(ISourceMailbox mailbox, List<MessageRec> messages, Func<IFxProxyPool> getProxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps, TestIntegration testIntegration, out Exception failure)
		{
			failure = null;
			try
			{
				using (IFxProxyPool fxProxyPool = getProxyPool())
				{
					mailbox.ExportMessages(messages, fxProxyPool, flags, propsToCopyExplicitly, excludeProps);
				}
				return true;
			}
			catch (Exception ex)
			{
				if (!MapiUtils.IsBadItemIndicator(ex, out failure))
				{
					if (failure != ex)
					{
						failure.PreserveExceptionStack();
						throw failure;
					}
					throw;
				}
			}
			return false;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		private static void OnDroppedMapiSessionNotify(MapiNotification notification)
		{
			MrsTracer.Provider.Debug("OnDroppedNotify() was called.", new object[0]);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		private static bool IsStoreDisconnectedMailboxInternal(ExRpcAdmin rpcAdmin, Guid mdbGuid, Guid mailboxGuid)
		{
			PropValue[][] mailboxTableInfo = rpcAdmin.GetMailboxTableInfo(mdbGuid, mailboxGuid, MailboxTableFlags.IncludeSoftDeletedMailbox, new PropTag[]
			{
				PropTag.UserGuid,
				PropTag.MailboxMiscFlags
			});
			if (mailboxTableInfo == null)
			{
				throw new RecipientNotFoundPermanentException(mailboxGuid);
			}
			foreach (PropValue[] array2 in mailboxTableInfo)
			{
				if (array2.Length == 2 && array2[0].PropTag == PropTag.UserGuid)
				{
					byte[] bytes = array2[0].GetBytes();
					Guid a = (bytes != null && bytes.Length == 16) ? new Guid(bytes) : Guid.Empty;
					if (!(a != mailboxGuid))
					{
						MailboxMiscFlags mailboxMiscFlags = (MailboxMiscFlags)((array2[1].PropTag == PropTag.MailboxMiscFlags) ? array2[1].GetInt() : 0);
						return mailboxMiscFlags.HasFlag(MailboxMiscFlags.SoftDeletedMailbox) || mailboxMiscFlags.HasFlag(MailboxMiscFlags.DisabledMailbox) || mailboxMiscFlags.HasFlag(MailboxMiscFlags.MRSSoftDeletedMailbox);
					}
				}
			}
			throw new RecipientNotFoundPermanentException(mailboxGuid);
		}

		// Token: 0x040006D6 RID: 1750
		public const int PageSize = 1000;

		// Token: 0x040006D7 RID: 1751
		public const int PreE12RuleSizeLimit = 32768;

		// Token: 0x040006D8 RID: 1752
		public const int AccessMaskMailboxFullControl = 1;

		// Token: 0x040006D9 RID: 1753
		public const string MrsClientAppId = "Client=MSExchangeMigration";

		// Token: 0x040006DA RID: 1754
		public const string NonMrsClientAppId = "Client=Management";

		// Token: 0x040006DB RID: 1755
		public const string PublicFolderSystemClientAppId = "Client=PublicFolderSystem";

		// Token: 0x040006DC RID: 1756
		public const string PublicFolderHierarchyReplicationAction = ";Action=PublicFolderHierarchyReplication";

		// Token: 0x040006DD RID: 1757
		private static readonly PropertyDefinition[] MiniServerProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Guid,
			ServerSchema.Fqdn,
			ServerSchema.ExchangeLegacyDN,
			ServerSchema.VersionNumber,
			ActiveDirectoryServerSchema.MailboxRelease
		};

		// Token: 0x040006DE RID: 1758
		private static readonly TimeSpan LocalMDBRefreshInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x040006DF RID: 1759
		private static readonly Dictionary<Guid, Guid> mdbToSystemMailboxMap = new Dictionary<Guid, Guid>();

		// Token: 0x040006E0 RID: 1760
		private static DateTime lastLocalMDBRefresh = DateTime.MinValue;

		// Token: 0x040006E1 RID: 1761
		private static List<Guid> localMDBs = null;

		// Token: 0x040006E2 RID: 1762
		private static readonly object syncRoot = new object();

		// Token: 0x040006E3 RID: 1763
		private static readonly PropertyDefinition[] MiniServerProps = new PropertyDefinition[]
		{
			ADObjectSchema.Guid
		};

		// Token: 0x040006E4 RID: 1764
		private static readonly LazyMember<byte[]> mapiFolderObjectData = new LazyMember<byte[]>(() => MapiUtils.CreateObjectData(InterfaceIds.IMAPIFolderGuid));
	}
}
