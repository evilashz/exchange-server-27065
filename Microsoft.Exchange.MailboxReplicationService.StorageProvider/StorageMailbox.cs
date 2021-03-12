using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal abstract class StorageMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00005534 File Offset: 0x00003734
		public StorageMailbox(LocalMailboxFlags flags) : base(flags)
		{
			this.InTransitStatus = InTransitStatus.NotInTransit;
			this.StoreSession = null;
			this.isStorageProvider = true;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00005552 File Offset: 0x00003752
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000555A File Offset: 0x0000375A
		public StoreSession StoreSession { get; protected set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00005563 File Offset: 0x00003763
		public NativeStorePropertyDefinition[] FolderPropertyDefinitionsToLoad
		{
			get
			{
				if (this.folderPropertyDefinitionsToLoad == null)
				{
					this.folderPropertyDefinitionsToLoad = this.ConvertPropTagsToDefinitions(FolderRec.PtagsToLoad);
				}
				return this.folderPropertyDefinitionsToLoad;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00005584 File Offset: 0x00003784
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000558C File Offset: 0x0000378C
		public InTransitStatus InTransitStatus { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00005595 File Offset: 0x00003795
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000559C File Offset: 0x0000379C
		public override int ServerVersion
		{
			get
			{
				return StorageMailbox.serverVersion;
			}
			protected set
			{
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000055A0 File Offset: 0x000037A0
		public static List<CultureInfo> InitializeCultureInfos()
		{
			return new List<CultureInfo>
			{
				CultureInfo.InvariantCulture
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000055BF File Offset: 0x000037BF
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.None;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000055C4 File Offset: 0x000037C4
		public MapiProp OpenMapiEntry(byte[] folderId, byte[] entryId, OpenEntryFlags flags)
		{
			MapiProp result;
			using (base.RHTracker.Start())
			{
				result = (MapiProp)this.StoreSession.Mailbox.MapiStore.OpenEntry(entryId, flags);
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005618 File Offset: 0x00003818
		public ExTimeZone GetTimeZone()
		{
			ExTimeZone exTimeZone = this.StoreSession.ExTimeZone;
			if (exTimeZone == null)
			{
				exTimeZone = ExTimeZone.UtcTimeZone;
			}
			return exTimeZone;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000563B File Offset: 0x0000383B
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005642 File Offset: 0x00003842
		bool IMailbox.IsConnected()
		{
			return this.connectedWithoutMailboxSession || this.StoreSession != null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000565A File Offset: 0x0000385A
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			return this.IsMailboxCapabilitySupportedInternal(capability);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005664 File Offset: 0x00003864
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("StorageMailbox.GetMailboxInformation", new object[0]);
			MailboxInformation mailboxInformation = new MailboxInformation();
			MailboxDatabase mailboxDatabase = base.FindDatabaseByGuid<MailboxDatabase>(base.MdbGuid);
			mailboxInformation.ProviderName = "StorageProvider";
			mailboxInformation.MailboxGuid = base.MailboxGuid;
			mailboxInformation.MdbGuid = base.MdbGuid;
			mailboxInformation.MdbName = mailboxDatabase.Identity.ToString();
			mailboxInformation.MdbLegDN = mailboxDatabase.ExchangeLegacyDN;
			mailboxInformation.MdbQuota = (mailboxDatabase.ProhibitSendReceiveQuota.IsUnlimited ? null : new ulong?(mailboxDatabase.ProhibitSendReceiveQuota.Value.ToBytes()));
			mailboxInformation.MdbDumpsterQuota = (mailboxDatabase.RecoverableItemsQuota.IsUnlimited ? null : new ulong?(mailboxDatabase.RecoverableItemsQuota.Value.ToBytes()));
			mailboxInformation.ServerVersion = this.ServerVersion;
			mailboxInformation.ServerMailboxRelease = base.ServerMailboxRelease.ToString();
			mailboxInformation.RecipientType = this.recipientType;
			mailboxInformation.RecipientDisplayType = this.recipientDisplayType;
			mailboxInformation.RecipientTypeDetailsLong = this.recipientTypeDetails;
			mailboxInformation.MailboxHomeMdbGuid = (base.IsArchiveMailbox ? base.MbxArchiveMdbGuid : base.MbxHomeMdbGuid);
			mailboxInformation.ArchiveGuid = this.archiveGuid;
			mailboxInformation.AlternateMailboxes = this.alternateMailboxes;
			mailboxInformation.UseMdbQuotaDefaults = this.useMdbQuotaDefaults;
			mailboxInformation.MailboxQuota = this.mbxQuota;
			mailboxInformation.MailboxDumpsterQuota = this.mbxDumpsterQuota;
			mailboxInformation.MailboxArchiveQuota = this.mbxArchiveQuota;
			mailboxInformation.MailboxIdentity = ((base.MailboxId != null) ? base.MailboxId.ToString() : null);
			mailboxInformation.MailboxItemCount = 0UL;
			mailboxInformation.MailboxSize = 0UL;
			mailboxInformation.RegularItemCount = 0UL;
			mailboxInformation.RegularDeletedItemCount = 0UL;
			mailboxInformation.AssociatedItemCount = 0UL;
			mailboxInformation.AssociatedDeletedItemCount = 0UL;
			mailboxInformation.RegularItemsSize = 0UL;
			mailboxInformation.RegularDeletedItemsSize = 0UL;
			mailboxInformation.AssociatedItemsSize = 0UL;
			mailboxInformation.AssociatedDeletedItemsSize = 0UL;
			mailboxInformation.RulesSize = 0;
			mailboxInformation.MrsVersion = CommonUtils.GetEffectiveMrsVersion();
			int intValueAndDecrement = base.TestIntegration.GetIntValueAndDecrement("SimulatePushMove", 0, 0, int.MaxValue);
			if (intValueAndDecrement > 0)
			{
				if (intValueAndDecrement % 2 == 0)
				{
					mailboxInformation.MrsVersion = 99.9f;
				}
				else
				{
					mailboxInformation.MrsVersion = 99.8f;
				}
			}
			if (this.StoreSession != null)
			{
				object[] properties;
				using (base.RHTracker.Start())
				{
					NativeStorePropertyDefinition[] array = this.ConvertPropTagsToDefinitions(MailboxProviderBase.MailboxInformationPropertyTags);
					this.StoreSession.Mailbox.Load(array);
					properties = this.StoreSession.Mailbox.GetProperties(array);
				}
				for (int i = 0; i < properties.Length; i++)
				{
					object obj = properties[i];
					if (obj != null && !(obj is PropertyError))
					{
						MailboxProviderBase.PopulateMailboxInformation(mailboxInformation, MailboxProviderBase.MailboxInformationPropertyTags[i], obj);
					}
				}
				if (!base.IsPublicFolderMailbox)
				{
					using (base.RHTracker.Start())
					{
						try
						{
							MailboxSession mailboxSession = this.GetMailboxSession();
							using (CoreFolder coreFolder = CoreFolder.Bind(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox)))
							{
								coreFolder.PropertyBag.Load(StorageMailbox.InboxProperties);
								object obj2 = coreFolder.PropertyBag.TryGetProperty(FolderSchema.FolderRulesSize);
								if (obj2 is int)
								{
									mailboxInformation.RulesSize = (int)obj2;
								}
								object obj3 = coreFolder.PropertyBag.TryGetProperty(FolderSchema.ContentAggregationFlags);
								if (obj3 is int)
								{
									mailboxInformation.ContentAggregationFlags = (int)obj3;
								}
								else
								{
									mailboxInformation.ContentAggregationFlags = 0;
								}
							}
						}
						catch (ObjectNotFoundException)
						{
							MrsTracer.Provider.Debug("Inbox not found for mailbox '{0}'", new object[]
							{
								base.MdbGuid
							});
						}
					}
				}
			}
			using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
			{
				using (base.RHTracker.Start())
				{
					mailboxInformation.MailboxTableFlags = (int)MapiUtils.GetMailboxTableFlags(rpcAdmin, base.MdbGuid, base.MailboxGuid);
				}
			}
			return mailboxInformation;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005AEC File Offset: 0x00003CEC
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			base.CreateStoreSession(connectFlags, delegate
			{
				this.StoreSession = this.CreateStoreConnection(connectFlags);
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005B28 File Offset: 0x00003D28
		public override void Disconnect()
		{
			base.CheckDisposed();
			lock (this)
			{
				base.Disconnect();
				MrsTracer.Provider.Debug("Disconnecting from server \"{0}\", mailbox \"{1}\".", new object[]
				{
					base.ServerDN,
					base.TraceMailboxId
				});
				if (this.StoreSession != null)
				{
					this.StoreSession.Dispose();
					this.StoreSession = null;
				}
				this.InTransitStatus = InTransitStatus.NotInTransit;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005BB4 File Offset: 0x00003DB4
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			MrsTracer.Provider.Function("StorageMailbox.SetInTransitStatus({0})", new object[]
			{
				status
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			onlineMoveSupported = true;
			if (this.InTransitStatus == status)
			{
				return;
			}
			using (base.RHTracker.Start())
			{
				this.StoreSession.Mailbox.SetProperties(this.ConvertPropTagsToDefinitions(new PropTag[]
				{
					PropTag.InTransitStatus
				}), new object[]
				{
					status
				});
				try
				{
					this.StoreSession.Mailbox.Save();
				}
				catch (FolderSaveException ex)
				{
					if (status != InTransitStatus.NotInTransit)
					{
						PropertyErrorException ex2 = ex.InnerException as PropertyErrorException;
						if (ex2 != null && ex2.PropertyErrors.Length == 1 && ex2.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.AccessDenied)
						{
							throw this.GetMailboxInTransitException(ex);
						}
					}
					throw;
				}
				this.StoreSession.Mailbox.Load();
			}
			this.InTransitStatus = status;
			if (status.HasFlag(InTransitStatus.SyncDestination))
			{
				this.StoreSession.MailboxMoveStage = MailboxMoveStage.OnlineMoveDestination;
				return;
			}
			this.StoreSession.MailboxMoveStage = MailboxMoveStage.None;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005CF8 File Offset: 0x00003EF8
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("StorageMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<FolderRec> list = new List<FolderRec>(50);
			using (base.RHTracker.Start())
			{
				bool flag = flags.HasFlag(EnumerateFolderHierarchyFlags.WellKnownPublicFoldersOnly);
				NativeStorePropertyDefinition[] propertiesToLoad;
				if (additionalPtagsToLoad == null || additionalPtagsToLoad.Length == 0)
				{
					propertiesToLoad = this.FolderPropertyDefinitionsToLoad;
				}
				else
				{
					List<NativeStorePropertyDefinition> list2 = new List<NativeStorePropertyDefinition>();
					list2.AddRange(this.ConvertPropTagsToDefinitions(additionalPtagsToLoad));
					list2.AddRange(this.FolderPropertyDefinitionsToLoad);
					propertiesToLoad = list2.ToArray();
				}
				if (!flag)
				{
					this.LoadFolderHierarchy(this.GetFolderId(null), propertiesToLoad, list);
				}
				else
				{
					PublicFolderSession publicFolderSession = this.GetPublicFolderSession();
					list.Add(this.GetFolderRec(this.GetFolderId(null), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetIpmSubtreeFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetNonIpmSubtreeFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetEFormsRegistryFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetAsyncDeleteStateFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetDumpsterRootFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetInternalSubmissionFolderId(), propertiesToLoad));
					list.Add(this.GetFolderRec(publicFolderSession.GetTombstonesRootFolderId(), propertiesToLoad));
				}
			}
			MrsTracer.Provider.Debug("Loaded {0} folders", new object[]
			{
				list.Count
			});
			return list;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005EA0 File Offset: 0x000040A0
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			MrsTracer.Provider.Function("StorageMailbox.GetNamedFromIDs", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			new List<NamedProp>(pta.Length);
			List<ushort> list = new List<ushort>(pta.Length);
			foreach (PropTag propTag in pta)
			{
				list.Add((ushort)propTag.Id());
			}
			NamedPropertyDefinition.NamedPropertyKey[] namesFromIds;
			using (base.RHTracker.Start())
			{
				namesFromIds = NamedPropConverter.GetNamesFromIds(this.StoreSession, list);
			}
			NamedPropData[] array = new NamedPropData[namesFromIds.Length];
			for (int j = 0; j < namesFromIds.Length; j++)
			{
				array[j] = new NamedPropData();
				if (namesFromIds[j] is GuidNamePropertyDefinition.GuidNameKey)
				{
					GuidNamePropertyDefinition.GuidNameKey guidNameKey = (GuidNamePropertyDefinition.GuidNameKey)namesFromIds[j];
					array[j].Kind = 1;
					array[j].Guid = guidNameKey.PropertyGuid;
					array[j].Name = guidNameKey.PropertyName;
				}
				else
				{
					if (!(namesFromIds[j] is GuidIdPropertyDefinition.GuidIdKey))
					{
						throw new CorruptNamedPropDataException((namesFromIds[j] == null) ? "null" : namesFromIds[j].GetType().ToString());
					}
					GuidIdPropertyDefinition.GuidIdKey guidIdKey = (GuidIdPropertyDefinition.GuidIdKey)namesFromIds[j];
					array[j].Kind = 0;
					array[j].Guid = guidIdKey.PropertyGuid;
					array[j].Id = guidIdKey.PropertyId;
				}
			}
			return array;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006014 File Offset: 0x00004214
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("StorageMailbox.GetIDsFromNames", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<NamedPropertyDefinition.NamedPropertyKey> list = new List<NamedPropertyDefinition.NamedPropertyKey>(npda.Length);
			PropTag[] array = new PropTag[npda.Length];
			foreach (NamedPropData namedPropData in npda)
			{
				switch (namedPropData.Kind)
				{
				case 0:
					list.Add(new GuidIdPropertyDefinition.GuidIdKey(namedPropData.Guid, namedPropData.Id));
					break;
				case 1:
					list.Add(new GuidNamePropertyDefinition.GuidNameKey(namedPropData.Guid, namedPropData.Name));
					break;
				default:
					throw new CorruptNamedPropDataException(namedPropData.Kind.ToString());
				}
			}
			ushort[] idsFromNames;
			using (base.RHTracker.Start())
			{
				idsFromNames = NamedPropConverter.GetIdsFromNames(this.StoreSession, createIfNotExists, list);
			}
			for (int j = 0; j < npda.Length; j++)
			{
				array[j] = (PropTag)(idsFromNames[j] << 16);
			}
			return array;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006128 File Offset: 0x00004328
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetSessionSpecificEntryId", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			long folderFid;
			if (entryId.Length == 22)
			{
				folderFid = this.StoreSession.IdConverter.GetIdFromLongTermId(entryId);
			}
			else
			{
				folderFid = this.StoreSession.IdConverter.GetFidFromId(StoreObjectId.FromProviderSpecificId(entryId));
			}
			return this.StoreSession.IdConverter.CreateFolderId(folderFid).ProviderLevelItemId;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000061A4 File Offset: 0x000043A4
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			MrsTracer.Provider.Function("StorageMailbox.AddMoveHistoryEntry", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			using (base.RHTracker.Start())
			{
				mhei.SaveToMailbox(this.StoreSession.Mailbox.MapiStore, maxMoveHistoryLength);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000620C File Offset: 0x0000440C
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			MrsTracer.Provider.Function("StorageMailbox.GetProps", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			object[] properties;
			using (base.RHTracker.Start())
			{
				PropertyDefinition[] array = this.ConvertPropTagsToDefinitions(ptags);
				this.StoreSession.Mailbox.ForceReload(array);
				properties = this.StoreSession.Mailbox.GetProperties(array);
			}
			PropValueData[] array2 = new PropValueData[properties.Length];
			for (int i = 0; i < ptags.Length; i++)
			{
				object obj = properties[i];
				PropTag propTag = ptags[i];
				if (obj is ExDateTime)
				{
					obj = (DateTime)((ExDateTime)obj);
				}
				if (obj is PropertyError)
				{
					propTag = propTag.ChangePropType(PropType.Error);
					obj = (int)((PropertyError)obj).PropertyErrorCode;
				}
				if (obj == null)
				{
					propTag = propTag.ChangePropType(PropType.Null);
				}
				array2[i] = new PropValueData(propTag, obj);
			}
			return array2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000630C File Offset: 0x0000450C
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			MrsTracer.Provider.Function("StorageMailbox.GetReceiveFolderEntryId", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			StoreObjectId receiveFolderId;
			using (base.RHTracker.Start())
			{
				string text;
				receiveFolderId = this.GetMailboxSession().GetReceiveFolderId(msgClass, out text);
			}
			return StoreId.GetStoreObjectId(receiveFolderId).ProviderLevelItemId;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000639C File Offset: 0x0000459C
		string IMailbox.LoadSyncState(byte[] key)
		{
			MrsTracer.Provider.Function("StorageMailbox.LoadSyncState", new object[0]);
			string syncState = null;
			this.ExecuteSyncStateOperation(delegate(StoreSession storeSession, CoreFolder folder)
			{
				syncState = this.GetSyncState(storeSession, key, folder);
			});
			return syncState;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006444 File Offset: 0x00004644
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncStateStr)
		{
			MrsTracer.Provider.Function("StorageMailbox.SaveSyncState", new object[0]);
			if (this.StoreSession == null)
			{
				return null;
			}
			MessageRec messageRec = null;
			this.ExecuteSyncStateOperation(delegate(StoreSession storeSession, CoreFolder folder)
			{
				folder.PropertyBag.Load(new PropertyDefinition[]
				{
					CoreFolderSchema.Id
				});
				messageRec = this.SetSyncState(storeSession, key, syncStateStr, folder);
			});
			return messageRec;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000064AC File Offset: 0x000046AC
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			if (statisticsTypes.HasFlag(SessionStatisticsFlags.ContentIndexingWordBreaking) && this.StoreSession != null)
			{
				SessionStatistics sessionStatistics = new SessionStatistics();
				ContentIndexingSession contentIndexingSession = this.StoreSession.ContentIndexingSession as ContentIndexingSession;
				if (contentIndexingSession != null && contentIndexingSession.Statistics != null)
				{
					TransportFlowStatistics statistics = contentIndexingSession.Statistics;
					sessionStatistics.TotalMessagesProcessed = statistics.TotalMessagesProcessed;
					sessionStatistics.TotalTimeProcessingMessages = statistics.TotalTimeProcessingMessages;
					sessionStatistics.TimeInGetConnection = statistics.TimeInGetConnection;
					sessionStatistics.TimeInPropertyBagLoad = statistics.TimeInPropertyBagLoad;
					sessionStatistics.TimeInMessageItemConversion = statistics.TimeInMessageItemConversion;
					sessionStatistics.TimeDeterminingAgeOfItem = statistics.TimeDeterminingAgeOfItem;
					sessionStatistics.TimeInMimeConversion = statistics.TimeInMimeConversion;
					sessionStatistics.TimeInShouldAnnotateMessage = statistics.TimeInShouldAnnotateMessage;
					sessionStatistics.TimeInWordbreaker = statistics.TimeInWordbreaker;
					sessionStatistics.TimeInQueue = statistics.TimeInQueue;
					sessionStatistics.TimeProcessingFailedMessages = statistics.TimeProcessingFailedMessages;
					sessionStatistics.TimeInTransportRetriever = statistics.TimeInTransportRetriever;
					sessionStatistics.TimeInDocParser = statistics.TimeInDocParser;
					sessionStatistics.TimeInNLGSubflow = statistics.TimeInNLGSubflow;
					sessionStatistics.MessageLevelFailures = statistics.MessageLevelFailures;
					sessionStatistics.MessagesSuccessfullyAnnotated = statistics.MessagesSuccessfullyAnnotated;
					sessionStatistics.AnnotationSkipped = statistics.AnnotationSkipped;
					sessionStatistics.ConnectionLevelFailures = statistics.ConnectionLevelFailures;
				}
				return sessionStatistics;
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000065E4 File Offset: 0x000047E4
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			ExAssert.RetailAssert(mailboxCorruptionTypes != null, "IMailbox.StartIsInteg input must be non-null");
			Guid result = Guid.Empty;
			using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
			{
				using (base.RHTracker.Start())
				{
					MrsTracer.Provider.Debug("Starting IsInteg task \"{0}\" in MDB {1}", new object[]
					{
						base.TraceMailboxId,
						base.MdbGuid
					});
					PropValue[][] array = rpcAdmin.StoreIntegrityCheckEx(base.MdbGuid, base.MailboxGuid, Guid.Empty, 1U, 20U, mailboxCorruptionTypes.ToArray(), IsInteg.JobPropTags);
					foreach (PropValue[] propValues in array)
					{
						StoreIntegrityCheckJob storeIntegrityCheckJob = new StoreIntegrityCheckJob(propValues);
						result = storeIntegrityCheckJob.RequestGuid;
						MrsTracer.Provider.Debug("Started Store IsInteg task: {0}", new object[]
						{
							storeIntegrityCheckJob.ToString()
						});
					}
				}
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000066FC File Offset: 0x000048FC
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			List<StoreIntegrityCheckJob> list = new List<StoreIntegrityCheckJob>(1);
			using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
			{
				using (base.RHTracker.Start())
				{
					MrsTracer.Provider.Debug("Starting IsInteg task \"{0}\" in MDB {1}", new object[]
					{
						base.TraceMailboxId,
						base.MdbGuid
					});
					PropValue[][] array = rpcAdmin.StoreIntegrityCheckEx(base.MdbGuid, base.MailboxGuid, isIntegRequestGuid, 2U, 0U, null, IsInteg.JobPropTags);
					foreach (PropValue[] propValues in array)
					{
						StoreIntegrityCheckJob storeIntegrityCheckJob = new StoreIntegrityCheckJob(propValues);
						list.Add(storeIntegrityCheckJob);
						MrsTracer.Provider.Debug("Queried Store IsInteg task: {0}", new object[]
						{
							storeIntegrityCheckJob.ToString()
						});
					}
				}
			}
			return list;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000067FC File Offset: 0x000049FC
		public NativeStorePropertyDefinition[] SetPropertiesHelper(ICorePropertyBag propertyBag, PropValueData[] pvda)
		{
			if (pvda == null || pvda.Length == 0)
			{
				return null;
			}
			PropTag[] array = new PropTag[pvda.Length];
			for (int i = 0; i < pvda.Length; i++)
			{
				array[i] = (PropTag)pvda[i].PropTag;
				if (base.IsPublicFolderMailbox && array[i] == PropTag.IpmWasteBasketEntryId)
				{
					byte[] array2 = pvda[i].Value as byte[];
					if (array2 != null)
					{
						pvda[i].Value = this.StoreSession.IdConverter.GetLongTermIdFromId(StoreObjectId.FromProviderSpecificId(array2));
					}
				}
			}
			NativeStorePropertyDefinition[] result;
			using (base.RHTracker.Start())
			{
				NativeStorePropertyDefinition[] array3 = this.ConvertPropTagsToDefinitions(array);
				for (int j = 0; j < pvda.Length; j++)
				{
					if (pvda[j].Value == null)
					{
						propertyBag.Delete(array3[j]);
					}
					else
					{
						propertyBag.SetProperty(array3[j], pvda[j].Value);
					}
				}
				result = array3;
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000068EC File Offset: 0x00004AEC
		public NativeStorePropertyDefinition[] ConvertPropTagsToDefinitions(params PropTag[] propTags)
		{
			return MapiUtils.ConvertPropTagsToDefinitions(this.StoreSession, propTags);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000068FC File Offset: 0x00004AFC
		public T GetFolder<T>(byte[] folderId) where T : StorageFolder, new()
		{
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			T result;
			using (base.RHTracker.Start())
			{
				StoreObjectId folderId2 = this.GetFolderId(folderId);
				CoreFolder coreFolder;
				try
				{
					coreFolder = CoreFolder.Bind(this.StoreSession, folderId2);
				}
				catch (ObjectNotFoundException)
				{
					return default(T);
				}
				if (MrsTracer.Provider.IsEnabled(TraceType.DebugTrace))
				{
					coreFolder.PropertyBag.Load(new PropertyDefinition[]
					{
						FolderSchema.DisplayName
					});
					object obj = coreFolder.PropertyBag.TryGetProperty(FolderSchema.DisplayName);
					MrsTracer.Provider.Debug("Opened folder '{0}'", new object[]
					{
						obj
					});
				}
				StorageFolder storageFolder = Activator.CreateInstance<T>();
				storageFolder.Config(folderId, coreFolder, this);
				result = (T)((object)storageFolder);
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000069E8 File Offset: 0x00004BE8
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000069FA File Offset: 0x00004BFA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StorageMailbox>(this);
		}

		// Token: 0x06000071 RID: 113
		protected abstract Exception GetMailboxInTransitException(Exception innerException);

		// Token: 0x06000072 RID: 114
		protected abstract OpenEntryFlags GetFolderOpenEntryFlags();

		// Token: 0x06000073 RID: 115 RVA: 0x00006A04 File Offset: 0x00004C04
		protected virtual StoreSession CreateStoreConnection(MailboxConnectFlags mailboxConnectFlags)
		{
			MrsTracer.Provider.Function("StorageMailbox.CreateStoreConnection", new object[0]);
			base.CheckDisposed();
			if (!base.IsRestore)
			{
				Guid guid = base.IsArchiveMailbox ? base.MbxArchiveMdbGuid : base.MbxHomeMdbGuid;
				if (mailboxConnectFlags.HasFlag(MailboxConnectFlags.NonMrsLogon) && base.TestIntegration.GetIntValueAndDecrement("InjectNFaultsTargetConnectivityVerification", 0, 0, 2147483647) > 0)
				{
					guid = new Guid("F00DBABE-0000-0000-0000-000000000000");
				}
				if (base.MdbGuid != guid)
				{
					MrsTracer.Provider.Warning("Source mailbox does not exist or is in the wrong database.", new object[0]);
					throw new MailboxIsNotInExpectedMDBPermanentException(guid);
				}
			}
			MrsTracer.Provider.Debug("Opening StoreSession: mailbox=\"{0}\"", new object[]
			{
				base.TraceMailboxId
			});
			StoreSession result;
			try
			{
				using (base.RHTracker.Start())
				{
					ADSessionSettings adsessionSettings = (base.PartitionHint != null) ? ADSessionSettings.FromTenantPartitionHint(base.PartitionHint) : ADSessionSettings.FromRootOrgScopeSet();
					adsessionSettings.IncludeSoftDeletedObjects = true;
					adsessionSettings.IncludeInactiveMailbox = true;
					ExchangePrincipal exchangePrincipal;
					if (base.IsRestore)
					{
						exchangePrincipal = ExchangePrincipal.FromMailboxData(base.MailboxGuid, base.MdbGuid, StorageMailbox.CultureInfos);
					}
					else
					{
						exchangePrincipal = ExchangePrincipal.FromMailboxGuid(adsessionSettings, base.MailboxGuid, base.MdbGuid, RemotingOptions.LocalConnectionsOnly, base.EffectiveDomainControllerName, false);
					}
					string text = ((mailboxConnectFlags & MailboxConnectFlags.NonMrsLogon) != MailboxConnectFlags.None) ? "Client=Management" : "Client=MSExchangeMigration";
					if ((mailboxConnectFlags & MailboxConnectFlags.PublicFolderHierarchyReplication) != MailboxConnectFlags.None)
					{
						text = "Client=PublicFolderSystem;Action=PublicFolderHierarchyReplication";
					}
					if (base.IsPublicFolderMailbox)
					{
						OpenMailboxSessionFlags openMailboxSessionFlags = OpenMailboxSessionFlags.None;
						if (base.RestoreFlags.HasFlag(MailboxRestoreType.Recovery))
						{
							openMailboxSessionFlags |= OpenMailboxSessionFlags.UseRecoveryDatabase;
						}
						result = PublicFolderSession.OpenAsMRS(exchangePrincipal, text, openMailboxSessionFlags);
					}
					else
					{
						MailboxSession.InitializationFlags initializationFlags = MailboxSession.InitializationFlags.None;
						if (base.IsRestore)
						{
							initializationFlags |= MailboxSession.InitializationFlags.OverrideHomeMdb;
							if (base.RestoreFlags.HasFlag(MailboxRestoreType.Recovery))
							{
								initializationFlags |= MailboxSession.InitializationFlags.UseRecoveryDatabase;
							}
							if (base.RestoreFlags.HasFlag(MailboxRestoreType.SoftDeleted) || base.RestoreFlags.HasFlag(MailboxRestoreType.Disabled))
							{
								initializationFlags |= MailboxSession.InitializationFlags.DisconnectedMailbox;
							}
							base.VerifyRestoreSource(mailboxConnectFlags);
						}
						if (base.IsFolderMove && this is StorageDestinationMailbox)
						{
							initializationFlags |= MailboxSession.InitializationFlags.MoveUser;
						}
						if (base.IsOlcSync)
						{
							initializationFlags |= MailboxSession.InitializationFlags.OlcSync;
						}
						this.PerformPreLogonOperations(exchangePrincipal, mailboxConnectFlags, text);
						result = MailboxSession.OpenAsMrs(exchangePrincipal, initializationFlags, text);
					}
				}
			}
			catch (ObjectNotFoundException originalException)
			{
				base.VerifyMdbIsOnline(originalException);
				throw;
			}
			catch (MailboxInTransitException innerException)
			{
				throw this.GetMailboxInTransitException(innerException);
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006CC8 File Offset: 0x00004EC8
		protected virtual void PerformPreLogonOperations(ExchangePrincipal exchangePrincipal, MailboxConnectFlags mailboxConnectFlags, string clientAppId)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006CCC File Offset: 0x00004ECC
		protected virtual bool IsMailboxCapabilitySupportedInternal(MailboxCapabilities capability)
		{
			switch (capability)
			{
			case MailboxCapabilities.PagedEnumerateChanges:
			case MailboxCapabilities.RunIsInteg:
			case MailboxCapabilities.ExtendedAclInformation:
			case MailboxCapabilities.PagedEnumerateHierarchyChanges:
			case MailboxCapabilities.FolderRules:
			case MailboxCapabilities.FolderAcls:
				break;
			default:
				if (capability != MailboxCapabilities.PagedGetActions)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006D04 File Offset: 0x00004F04
		protected StoreObjectId GetFolderId(byte[] entryId)
		{
			if (entryId != null)
			{
				return StoreObjectId.FromProviderSpecificId(entryId);
			}
			if (!base.IsPublicFolderMailbox)
			{
				return this.GetMailboxSession().GetDefaultFolderId(DefaultFolderType.Configuration);
			}
			return this.GetPublicFolderSession().GetPublicFolderRootId();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006D34 File Offset: 0x00004F34
		private void LoadFolderHierarchy(StoreObjectId rootFolderId, NativeStorePropertyDefinition[] propertiesToLoad, List<FolderRec> result)
		{
			using (CoreFolder coreFolder = CoreFolder.Bind(this.StoreSession, rootFolderId))
			{
				FolderRec folderRec = this.GetFolderRec(coreFolder, propertiesToLoad);
				if (base.Flags.HasFlag(LocalMailboxFlags.ParallelPublicFolderMigration))
				{
					folderRec.FolderName = "Public Root";
				}
				result.Add(folderRec);
				using (QueryResult queryResult = coreFolder.QueryExecutor.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, propertiesToLoad))
				{
					object[][] rows;
					do
					{
						using (base.RHTracker.Start())
						{
							rows = queryResult.GetRows(1000);
						}
						foreach (object[] values in rows)
						{
							FolderRec folderRec2 = FolderRec.Create(this.StoreSession, propertiesToLoad, values);
							if (folderRec2 != null)
							{
								result.Add(folderRec2);
							}
						}
					}
					while (rows.Length != 0);
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006E3C File Offset: 0x0000503C
		private FolderRec GetFolderRec(StoreObjectId folderId, NativeStorePropertyDefinition[] propertiesToLoad)
		{
			FolderRec folderRec;
			using (CoreFolder coreFolder = CoreFolder.Bind(this.StoreSession, folderId))
			{
				folderRec = this.GetFolderRec(coreFolder, propertiesToLoad);
			}
			return folderRec;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006E7C File Offset: 0x0000507C
		private FolderRec GetFolderRec(CoreFolder folder, NativeStorePropertyDefinition[] propertiesToLoad)
		{
			folder.PropertyBag.Load(propertiesToLoad);
			object[] array = new object[propertiesToLoad.Length];
			for (int i = 0; i < propertiesToLoad.Length; i++)
			{
				array[i] = folder.PropertyBag.TryGetProperty(propertiesToLoad[i]);
			}
			return FolderRec.Create(this.StoreSession, propertiesToLoad, array);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006ECC File Offset: 0x000050CC
		private MailboxSession OpenSystemMailbox()
		{
			MrsTracer.Provider.Function("StorageMailbox.OpenSystemMailbox", new object[0]);
			base.CheckDisposed();
			Server server = LocalServer.GetServer();
			ADSystemMailbox adsystemMailbox = MapiUtils.GetADSystemMailbox(base.MdbGuid, null, null);
			MailboxSession result;
			using (base.RHTracker.Start())
			{
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADSystemMailbox(ADSessionSettings.FromRootOrgScopeSet(), adsystemMailbox, server);
				result = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=MSExchangeMigration");
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006F54 File Offset: 0x00005154
		private void ExecuteSyncStateOperation(Action<StoreSession, CoreFolder> operation)
		{
			using (base.RHTracker.Start())
			{
				if (base.IsPublicFolderMove || base.IsPublicFolderMailboxRestore)
				{
					PublicFolderSession publicFolderSession = this.GetPublicFolderSession();
					using (CoreFolder coreFolder = CoreFolder.Bind(publicFolderSession, publicFolderSession.GetTombstonesRootFolderId()))
					{
						operation(publicFolderSession, coreFolder);
						goto IL_A9;
					}
				}
				MailboxSession mailboxSession = null;
				bool flag = false;
				try
				{
					if (base.UseHomeMDB)
					{
						mailboxSession = this.GetMailboxSession();
					}
					else
					{
						mailboxSession = this.OpenSystemMailbox();
						flag = true;
					}
					using (CoreFolder coreFolder2 = CoreFolder.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration), false, "MailboxReplicationService SyncStates", CreateMode.OpenIfExists))
					{
						coreFolder2.Save(SaveMode.FailOnAnyConflict);
						operation(mailboxSession, coreFolder2);
					}
				}
				finally
				{
					if (flag && mailboxSession != null)
					{
						mailboxSession.Dispose();
					}
				}
				IL_A9:;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000704C File Offset: 0x0000524C
		private string GetSyncState(StoreSession storeSession, byte[] key, CoreFolder folder)
		{
			using (QueryResult queryResult = folder.QueryExecutor.ItemQuery(ItemQueryType.None, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ReplyTemplateId, this.GetSyncStateSearchKey(key)), null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				object[][] rows = queryResult.GetRows(1);
				if (rows.Length == 1)
				{
					VersionedId versionedId = rows[0][0] as VersionedId;
					if (versionedId == null)
					{
						return null;
					}
					try
					{
						using (Item item = Item.Bind(storeSession, versionedId))
						{
							using (Stream stream = item.PropertyBag.OpenPropertyStream(MailboxProviderBase.SyncStateStorePropertyDefinition, PropertyOpenMode.ReadOnly))
							{
								using (StreamReader streamReader = new StreamReader(stream))
								{
									return streamReader.ReadToEnd();
								}
							}
						}
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007154 File Offset: 0x00005354
		private MessageRec SetSyncState(StoreSession storeSession, byte[] key, string syncStateStr, CoreFolder folder)
		{
			byte[] syncStateSearchKey = this.GetSyncStateSearchKey(key);
			MessageRec result;
			using (QueryResult queryResult = folder.QueryExecutor.ItemQuery(ItemQueryType.None, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ReplyTemplateId, syncStateSearchKey), null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				object[][] rows = queryResult.GetRows(1);
				CoreItem coreItem;
				if (rows.Length == 1)
				{
					coreItem = CoreItem.Bind(storeSession, (VersionedId)rows[0][0]);
				}
				else
				{
					coreItem = CoreItem.Create(storeSession, folder.Id, CreateMessageType.Normal);
				}
				using (coreItem)
				{
					coreItem.PropertyBag[StoreObjectSchema.ItemClass] = "IPM.MS-Exchange.MailboxSyncState";
					coreItem.PropertyBag[ItemSchema.Subject] = this.GetSyncStateSubject(key);
					if (string.IsNullOrWhiteSpace(syncStateStr))
					{
						coreItem.PropertyBag.Delete(MailboxProviderBase.SyncStateStorePropertyDefinition);
					}
					else
					{
						using (Stream stream = coreItem.PropertyBag.OpenPropertyStream(MailboxProviderBase.SyncStateStorePropertyDefinition, PropertyOpenMode.Create))
						{
							using (StreamWriter streamWriter = new StreamWriter(stream))
							{
								streamWriter.Write(syncStateStr);
								CommonUtils.AppendNewLinesAndFlush(streamWriter);
							}
						}
					}
					coreItem.PropertyBag[ItemSchema.ReplyTemplateId] = syncStateSearchKey;
					coreItem.Save(SaveMode.FailOnAnyConflict);
					coreItem.PropertyBag.Load(new PropertyDefinition[]
					{
						ItemSchema.Id
					});
					result = new MessageRec((byte[])coreItem.Id.ObjectId.ProviderLevelItemId.Clone(), (byte[])folder.Id.ObjectId.ProviderLevelItemId.Clone(), DateTime.MinValue, 0, MsgRecFlags.None, null);
				}
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00007350 File Offset: 0x00005550
		private string GetSyncStateSubject(byte[] key)
		{
			DateTime utcNow = DateTime.UtcNow;
			return string.Format(CultureInfo.InvariantCulture, "SyncState: {0}-{1} :: {2} :: {3}", new object[]
			{
				base.MailboxGuid.ToString(),
				TraceUtils.DumpBytes(key),
				utcNow.ToLongDateString(),
				utcNow.ToLongTimeString()
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000073B0 File Offset: 0x000055B0
		private byte[] GetSyncStateSearchKey(byte[] key)
		{
			byte[] array = new byte[16 + key.Length];
			base.MailboxGuid.ToByteArray().CopyTo(array, 0);
			key.CopyTo(array, 16);
			return array;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000073E8 File Offset: 0x000055E8
		private MailboxSession GetMailboxSession()
		{
			MailboxSession mailboxSession = this.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new StorageConnectionTypeException((this.StoreSession == null) ? "null" : this.StoreSession.GetType().ToString());
			}
			return mailboxSession;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000742C File Offset: 0x0000562C
		private PublicFolderSession GetPublicFolderSession()
		{
			PublicFolderSession publicFolderSession = this.StoreSession as PublicFolderSession;
			if (publicFolderSession == null)
			{
				throw new StorageConnectionTypeException((this.StoreSession == null) ? "null" : this.StoreSession.GetType().ToString());
			}
			return publicFolderSession;
		}

		// Token: 0x0400000F RID: 15
		private const string ProviderName = "StorageProvider";

		// Token: 0x04000010 RID: 16
		internal static readonly List<CultureInfo> CultureInfos = StorageMailbox.InitializeCultureInfos();

		// Token: 0x04000011 RID: 17
		private static readonly PropertyDefinition[] InboxProperties = new PropertyDefinition[]
		{
			FolderSchema.FolderRulesSize,
			FolderSchema.ContentAggregationFlags
		};

		// Token: 0x04000012 RID: 18
		private static int serverVersion = new ServerVersion(15, 0, 1497, 10).ToInt();

		// Token: 0x04000013 RID: 19
		private NativeStorePropertyDefinition[] folderPropertyDefinitionsToLoad;
	}
}
