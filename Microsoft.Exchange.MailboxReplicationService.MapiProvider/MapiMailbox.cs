﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal abstract class MapiMailbox : MailboxProviderBase, IMailbox, IDisposable
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00004376 File Offset: 0x00002576
		public MapiMailbox(LocalMailboxFlags flags) : base(flags)
		{
			this.createdFromMapiStore = false;
			this.syncStateMessageIds = new Dictionary<byte[], byte[]>();
			this.HTTPProxyServerName = null;
			this.inTransitStatus = InTransitStatus.NotInTransit;
			this.StoreSupportsOnlineMove = true;
			this.store = null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000043AD File Offset: 0x000025AD
		internal MapiMailbox(MapiStore mapiStore) : base(LocalMailboxFlags.None)
		{
			this.MapiStore = mapiStore;
			this.createdFromMapiStore = true;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000043C4 File Offset: 0x000025C4
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000043CC File Offset: 0x000025CC
		public MapiStore MapiStore
		{
			get
			{
				return this.store;
			}
			protected set
			{
				this.store = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000043D5 File Offset: 0x000025D5
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000043DD File Offset: 0x000025DD
		public string UserLegDN { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000043E8 File Offset: 0x000025E8
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000443A File Offset: 0x0000263A
		public override int ServerVersion
		{
			get
			{
				if (this.MapiStore != null)
				{
					return new ServerVersion(this.MapiStore.VersionMajor, this.MapiStore.VersionMinor, this.MapiStore.BuildMajor, this.MapiStore.BuildMinor).ToInt();
				}
				return this.serverVersion;
			}
			protected set
			{
				this.serverVersion = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00004443 File Offset: 0x00002643
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000444B File Offset: 0x0000264B
		public string HTTPProxyServerName { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004454 File Offset: 0x00002654
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000445C File Offset: 0x0000265C
		public bool StoreSupportsOnlineMove { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00004465 File Offset: 0x00002665
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00004470 File Offset: 0x00002670
		public InTransitStatus InTransitStatus
		{
			get
			{
				return this.inTransitStatus;
			}
			set
			{
				bool flag;
				((IMailbox)this).SetInTransitStatus(value, out flag);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004488 File Offset: 0x00002688
		public void ConfigRPCHTTP(string mailboxLegDN, string userLegDN, string serverDN, string httpProxyServerName, NetworkCredential cred, bool credentialIsAdmin, bool useNTLMAuth)
		{
			base.MailboxDN = mailboxLegDN;
			this.UserLegDN = userLegDN;
			base.ServerDN = serverDN;
			this.HTTPProxyServerName = ((!string.IsNullOrEmpty(httpProxyServerName)) ? httpProxyServerName : null);
			base.Credential = cred;
			if (!credentialIsAdmin)
			{
				base.Flags |= LocalMailboxFlags.CredentialIsNotAdmin;
			}
			if (useNTLMAuth)
			{
				base.Flags |= LocalMailboxFlags.UseNTLMAuth;
			}
			base.ServerDisplayName = DNConvertor.ServerNameFromServerLegacyDN(base.ServerDN);
			if (string.IsNullOrEmpty(base.ServerDisplayName))
			{
				base.ServerDisplayName = httpProxyServerName;
			}
			base.ServerGuid = Guid.Empty;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000451C File Offset: 0x0000271C
		public MapiProp OpenMapiEntry(byte[] folderId, byte[] entryId, OpenEntryFlags flags)
		{
			MapiProp result;
			using (base.RHTracker.Start())
			{
				if (!base.IsPublicFolderMigrationSource)
				{
					result = (MapiProp)this.MapiStore.OpenEntry(entryId, flags);
				}
				else
				{
					byte[] key = folderId ?? MailboxProviderBase.NullFolderKey;
					if (this.folderSessions == null)
					{
						this.folderSessions = new EntryIdMap<MapiStore>();
					}
					MapiStore mapiStore;
					if (this.folderSessions.TryGetValue(key, out mapiStore))
					{
						result = (MapiProp)mapiStore.OpenEntry(entryId, flags);
					}
					else
					{
						MapiFolder mapiFolder = null;
						try
						{
							mapiFolder = (MapiFolder)this.MapiStore.OpenEntry(folderId, flags);
							if (mapiFolder == null)
							{
								result = null;
							}
							else
							{
								if (mapiFolder.IsContentAvailable)
								{
									mapiStore = this.MapiStore;
								}
								else
								{
									MrsTracer.Provider.Debug("Folder is not available on server '{0}', will try replicas.", new object[]
									{
										base.ServerDN
									});
									string[] array = null;
									try
									{
										array = mapiFolder.GetReplicaServers();
									}
									catch (MapiExceptionNoReplicaAvailable mapiExceptionNoReplicaAvailable)
									{
										MrsTracer.Provider.Error("Exception encountered while loading replicas for folder '{0}': {1}", new object[]
										{
											TraceUtils.DumpEntryId(folderId),
											mapiExceptionNoReplicaAvailable
										});
									}
									if (array == null || array.Length == 0)
									{
										MrsTracer.Provider.Error("Folder {0} does not have any replicas.", new object[]
										{
											TraceUtils.DumpEntryId(folderId)
										});
										mapiStore = this.MapiStore;
									}
									else
									{
										mapiStore = this.FindStoreSession(array);
									}
								}
								this.folderSessions.Add(key, mapiStore);
								MapiProp mapiProp;
								if (CommonUtils.IsSameEntryId(folderId, entryId) && mapiStore == this.MapiStore)
								{
									mapiProp = mapiFolder;
									mapiFolder = null;
								}
								else
								{
									mapiProp = (MapiProp)mapiStore.OpenEntry(entryId, flags);
								}
								result = mapiProp;
							}
						}
						finally
						{
							if (mapiFolder != null)
							{
								mapiFolder.Dispose();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004708 File Offset: 0x00002908
		public override SyncProtocol GetSyncProtocol()
		{
			return SyncProtocol.None;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000470B File Offset: 0x0000290B
		bool IMailbox.IsConnected()
		{
			return this.connectedWithoutMailboxSession || this.MapiStore != null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004724 File Offset: 0x00002924
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			if (capability == MailboxCapabilities.PagedEnumerateChanges)
			{
				return !base.IsTitanium;
			}
			switch (capability)
			{
			case MailboxCapabilities.FolderRules:
				return !(this is MapiDestinationMailbox) || this.ServerVersion >= Server.E14MinVersion;
			case MailboxCapabilities.FolderAcls:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004774 File Offset: 0x00002974
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			MrsTracer.Provider.Function("MapiMailbox.GetMailboxInformation", new object[0]);
			MailboxInformation mailboxInformation = new MailboxInformation();
			if (!base.IsPureMAPI)
			{
				if (base.IsPublicFolderMigrationSource)
				{
					PublicFolderDatabase publicFolderDatabase = base.FindDatabaseByGuid<PublicFolderDatabase>(base.MdbGuid);
					mailboxInformation.MdbGuid = base.MdbGuid;
					mailboxInformation.MdbName = publicFolderDatabase.Identity.ToString();
					mailboxInformation.MdbLegDN = publicFolderDatabase.ExchangeLegacyDN;
				}
				else
				{
					MailboxDatabase mailboxDatabase = base.FindDatabaseByGuid<MailboxDatabase>(base.MdbGuid);
					mailboxInformation.MailboxGuid = base.MailboxGuid;
					mailboxInformation.MdbGuid = base.MdbGuid;
					mailboxInformation.MdbName = mailboxDatabase.Identity.ToString();
					mailboxInformation.MdbLegDN = mailboxDatabase.ExchangeLegacyDN;
					mailboxInformation.MdbQuota = (mailboxDatabase.ProhibitSendReceiveQuota.IsUnlimited ? null : new ulong?(mailboxDatabase.ProhibitSendReceiveQuota.Value.ToBytes()));
					mailboxInformation.MdbDumpsterQuota = (mailboxDatabase.RecoverableItemsQuota.IsUnlimited ? null : new ulong?(mailboxDatabase.RecoverableItemsQuota.Value.ToBytes()));
				}
			}
			mailboxInformation.ServerVersion = this.ServerVersion;
			mailboxInformation.ServerMailboxRelease = base.ServerMailboxRelease.ToString();
			mailboxInformation.ProviderName = "MapiProvider";
			mailboxInformation.RecipientType = this.recipientType;
			mailboxInformation.RecipientDisplayType = this.recipientDisplayType;
			mailboxInformation.RecipientTypeDetailsLong = this.recipientTypeDetails;
			mailboxInformation.MailboxHomeMdbGuid = base.MbxHomeMdbGuid;
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
			if (this.MapiStore != null)
			{
				PropValue[] props;
				using (base.RHTracker.Start())
				{
					props = this.MapiStore.GetProps(MailboxProviderBase.MailboxInformationPropertyTags);
				}
				for (int i = 0; i < props.Length; i++)
				{
					object value = props[i].Value;
					if (value != null)
					{
						MailboxProviderBase.PopulateMailboxInformation(mailboxInformation, props[i].PropTag, value);
					}
				}
				if (!base.IsPublicFolderMigrationSource && !base.IsPublicFolderMailbox)
				{
					using (base.RHTracker.Start())
					{
						using (MapiFolder inboxFolder = this.MapiStore.GetInboxFolder())
						{
							if (inboxFolder != null)
							{
								PropValue[] props2 = inboxFolder.GetProps(MapiMailbox.InboxProperties);
								PropValue propValue = props2[0];
								if (!propValue.IsNull() && !propValue.IsError())
								{
									mailboxInformation.RulesSize = propValue.GetInt();
								}
								PropValue propValue2 = props2[1];
								if (!propValue2.IsNull() && !propValue2.IsError())
								{
									mailboxInformation.ContentAggregationFlags = propValue2.GetInt();
								}
								else
								{
									mailboxInformation.ContentAggregationFlags = 0;
								}
							}
						}
					}
				}
			}
			if (!base.IsPureMAPI && !base.IsPublicFolderMigrationSource)
			{
				using (ExRpcAdmin rpcAdmin = base.GetRpcAdmin())
				{
					using (base.RHTracker.Start())
					{
						mailboxInformation.MailboxTableFlags = (int)MapiUtils.GetMailboxTableFlags(rpcAdmin, base.MdbGuid, base.MailboxGuid);
					}
				}
			}
			return mailboxInformation;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004BD0 File Offset: 0x00002DD0
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			base.CreateStoreSession(connectFlags, delegate
			{
				this.store = this.CreateStoreConnection(this.ServerDN, this.ServerFqdn, connectFlags);
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004C0C File Offset: 0x00002E0C
		public override void Disconnect()
		{
			base.CheckDisposed();
			lock (this)
			{
				base.Disconnect();
				if (this.additionalSessions != null)
				{
					foreach (MapiStore mapiStore in this.additionalSessions.Values)
					{
						mapiStore.Dispose();
					}
					this.additionalSessions.Clear();
				}
				if (this.folderSessions != null)
				{
					this.folderSessions.Clear();
				}
				this.inTransitStatus = InTransitStatus.NotInTransit;
				if (this.store != null)
				{
					if (this.createdFromMapiStore)
					{
						MrsTracer.Provider.Debug("Not disconnecting as the object was created from the MapiStore.", new object[0]);
					}
					else
					{
						MrsTracer.Provider.Debug("Disconnecting from server \"{0}\", mailbox \"{1}\".", new object[]
						{
							base.ServerDN,
							base.TraceMailboxId
						});
						this.store.Dispose();
					}
					this.store = null;
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004D28 File Offset: 0x00002F28
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			if (this.inTransitStatus == status)
			{
				onlineMoveSupported = this.StoreSupportsOnlineMove;
				return;
			}
			this.SetInTransitStatus(this.MapiStore, status, out onlineMoveSupported);
			if (base.IsPublicFolderMigrationSource)
			{
				if (!onlineMoveSupported)
				{
					throw new OfflinePublicFolderMigrationNotSupportedException();
				}
				if (this.additionalSessions != null)
				{
					foreach (MapiStore mapiStore in this.additionalSessions.Values)
					{
						this.SetInTransitStatus(mapiStore, status, out onlineMoveSupported);
						if (!onlineMoveSupported)
						{
							throw new OfflinePublicFolderMigrationNotSupportedException();
						}
					}
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004DC8 File Offset: 0x00002FC8
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.Provider.Function("MapiMailbox.EnumerateFolderHierarchy({0})", new object[]
			{
				flags
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<FolderRec> list = new List<FolderRec>(50);
			using (base.RHTracker.Start())
			{
				if (!flags.HasFlag(EnumerateFolderHierarchyFlags.WellKnownPublicFoldersOnly))
				{
					this.LoadFolderHierarchy(null, additionalPtagsToLoad, list);
				}
				else
				{
					using (MapiFolder mapiFolder = (MapiFolder)this.MapiStore.OpenEntry(null))
					{
						using (MapiFolder mapiFolder2 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.DeferredActionFolderEntryID).Value))
						{
							using (MapiFolder mapiFolder3 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.SpoolerQueueEntryId).Value))
							{
								using (MapiFolder mapiFolder4 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.IpmSubtreeEntryId).Value))
								{
									using (MapiFolder mapiFolder5 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.IpmSentMailEntryId).Value))
									{
										using (MapiFolder mapiFolder6 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.IpmInboxEntryId).Value))
										{
											using (MapiFolder mapiFolder7 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.IpmWasteBasketEntryId).Value))
											{
												using (MapiFolder mapiFolder8 = (MapiFolder)this.MapiStore.OpenEntry((byte[])this.MapiStore.GetProp(PropTag.IpmOutboxEntryId).Value))
												{
													list.Add(FolderRec.Create(mapiFolder, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder2, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder3, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder4, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder5, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder6, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder7, additionalPtagsToLoad));
													list.Add(FolderRec.Create(mapiFolder8, additionalPtagsToLoad));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			MrsTracer.Provider.Debug("Loaded {0} folders", new object[]
			{
				list.Count
			});
			return list;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000051B0 File Offset: 0x000033B0
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetNamedFromIDs", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<NamedProp> result = new List<NamedProp>(pta.Length);
			using (base.RHTracker.Start())
			{
				MapiUtils.ProcessMapiCallInBatches<PropTag>(pta, delegate(PropTag[] batch)
				{
					result.AddRange(this.MapiStore.GetNamesFromIDs(batch));
				});
			}
			return DataConverter<NamedPropConverter, NamedProp, NamedPropData>.GetData(result.ToArray());
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005278 File Offset: 0x00003478
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetIDsFromNames", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			NamedProp[] native = DataConverter<NamedPropConverter, NamedProp, NamedPropData>.GetNative(npda);
			List<PropTag> result = new List<PropTag>(npda.Length);
			using (base.RHTracker.Start())
			{
				MapiUtils.ProcessMapiCallInBatches<NamedProp>(native, delegate(NamedProp[] batch)
				{
					result.AddRange(this.MapiStore.GetIDsFromNames(createIfNotExists, batch));
				});
			}
			return result.ToArray();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000531C File Offset: 0x0000351C
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetSessionSpecificEntryId", new object[0]);
			base.CheckDisposed();
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			long fid;
			if (entryId.Length == 22)
			{
				fid = this.MapiStore.IdFromGlobalId(entryId);
			}
			else
			{
				fid = this.MapiStore.GetFidFromEntryId(entryId);
			}
			return this.MapiStore.CreateEntryId(fid);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005380 File Offset: 0x00003580
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			MrsTracer.Provider.Function("MapiMailbox.AddMoveHistoryEntry", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			using (base.RHTracker.Start())
			{
				mhei.SaveToMailbox(this.MapiStore, maxMoveHistoryLength);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000053E0 File Offset: 0x000035E0
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetProps", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			PropValue[] props;
			using (base.RHTracker.Start())
			{
				props = this.MapiStore.GetProps(ptags);
			}
			return DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(props);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005444 File Offset: 0x00003644
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			MrsTracer.Provider.Function("MapiMailbox.GetReceiveFolderEntryId", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			if (base.IsPublicFolderMigrationSource)
			{
				return null;
			}
			byte[] receiveFolderEntryId;
			using (base.RHTracker.Start())
			{
				receiveFolderEntryId = this.MapiStore.GetReceiveFolderEntryId(msgClass);
			}
			return receiveFolderEntryId;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000054B0 File Offset: 0x000036B0
		string IMailbox.LoadSyncState(byte[] key)
		{
			MrsTracer.Provider.Function("MapiMailbox.LoadSyncState", new object[0]);
			MapiStore mapiStore = null;
			bool flag = false;
			string result;
			try
			{
				using (base.RHTracker.Start())
				{
					if (base.IsPureMAPI || base.UseHomeMDB || base.IsPublicFolderMove)
					{
						mapiStore = this.MapiStore;
					}
					else
					{
						mapiStore = this.OpenSystemMailbox();
						flag = true;
					}
					using (MoveObjectInfo<string> syncStateMOI = this.GetSyncStateMOI(mapiStore, key))
					{
						string text = syncStateMOI.ReadObject(ReadObjectFlags.DontThrowOnCorruptData);
						if (text == null)
						{
							MrsTracer.Provider.Debug("Sync state does not exist.", new object[0]);
						}
						else
						{
							this.syncStateMessageIds[key] = syncStateMOI.MessageId;
						}
						result = text;
					}
				}
			}
			finally
			{
				if (flag && mapiStore != null)
				{
					mapiStore.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000559C File Offset: 0x0000379C
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncStateStr)
		{
			MrsTracer.Provider.Function("MapiMailbox.SaveSyncState", new object[0]);
			MapiStore mapiStore = null;
			bool flag = false;
			MessageRec result = null;
			try
			{
				using (base.RHTracker.Start())
				{
					if (base.IsPureMAPI || base.UseHomeMDB || base.IsPublicFolderMove)
					{
						mapiStore = this.MapiStore;
					}
					else
					{
						mapiStore = this.OpenSystemMailbox();
						flag = true;
					}
					using (MoveObjectInfo<string> syncStateMOI = this.GetSyncStateMOI(mapiStore, key))
					{
						syncStateMOI.OpenMessage();
						if (syncStateStr != null)
						{
							syncStateMOI.SaveObject(syncStateStr);
							this.syncStateMessageIds[key] = syncStateMOI.MessageId;
							result = new MessageRec(syncStateMOI.MessageId, syncStateMOI.FolderId, DateTime.MinValue, 0, MsgRecFlags.None, null);
						}
						else
						{
							if (syncStateMOI.MessageFound)
							{
								syncStateMOI.DeleteMessage();
							}
							this.syncStateMessageIds[key] = null;
						}
					}
				}
			}
			finally
			{
				if (flag && mapiStore != null)
				{
					mapiStore.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000056B0 File Offset: 0x000038B0
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			return new SessionStatistics();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000056B7 File Offset: 0x000038B7
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000056BE File Offset: 0x000038BE
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000056C5 File Offset: 0x000038C5
		protected override void AfterConnect()
		{
			this.syncStateMessageIds = new Dictionary<byte[], byte[]>();
			base.AfterConnect();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000056D8 File Offset: 0x000038D8
		protected T GetFolder<T>(byte[] folderId) where T : MapiFolder, new()
		{
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			T result;
			using (base.RHTracker.Start())
			{
				MapiFolder mapiFolder = (MapiFolder)this.OpenMapiEntry(folderId, folderId, this.GetFolderOpenEntryFlags());
				if (mapiFolder == null)
				{
					MrsTracer.Provider.Debug("Folder does not exist", new object[0]);
					result = default(T);
				}
				else
				{
					if (MrsTracer.Provider.IsEnabled(TraceType.DebugTrace))
					{
						string @string = mapiFolder.GetProp(PropTag.DisplayName).GetString();
						MrsTracer.Provider.Debug("Opened folder '{0}'", new object[]
						{
							@string
						});
					}
					T t = Activator.CreateInstance<T>();
					t.Config(folderId, mapiFolder, this);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000057AC File Offset: 0x000039AC
		protected MapiStore OpenSystemMailbox()
		{
			MrsTracer.Provider.Function("MapiMailbox.OpenSystemMailbox", new object[0]);
			base.CheckDisposed();
			if (base.IsPureMAPI)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			MapiStore systemMailbox;
			using (base.RHTracker.Start())
			{
				systemMailbox = MapiUtils.GetSystemMailbox(base.MdbGuid, base.ConfigDomainControllerName, base.Credential);
			}
			return systemMailbox;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005828 File Offset: 0x00003A28
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
			if (calledFromDispose)
			{
				((IMailbox)this).Disconnect();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000583A File Offset: 0x00003A3A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiMailbox>(this);
		}

		// Token: 0x06000062 RID: 98
		protected abstract Exception GetMailboxInTransitException(Exception innerException);

		// Token: 0x06000063 RID: 99
		protected abstract OpenEntryFlags GetFolderOpenEntryFlags();

		// Token: 0x06000064 RID: 100 RVA: 0x00005844 File Offset: 0x00003A44
		protected IConfigurationSession GetSystemConfigurationSession(bool readOnly, bool tenantScoped)
		{
			if (base.IsPureMAPI)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			ADSessionSettings sessionSettings;
			if (tenantScoped && base.PartitionHint != null)
			{
				sessionSettings = ADSessionSettings.FromTenantPartitionHint(base.PartitionHint);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.ConfigDomainControllerName, readOnly, ConsistencyMode.PartiallyConsistent, base.Credential, sessionSettings, 1140, "GetSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Provider\\MapiProvider\\MapiMailbox.cs");
			if (!tenantScoped)
			{
				tenantOrTopologyConfigurationSession.EnforceDefaultScope = false;
			}
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000058B8 File Offset: 0x00003AB8
		protected virtual MapiStore CreateStoreConnection(string serverLegDN, string serverFqdn, MailboxConnectFlags mailboxConnectFlags)
		{
			MrsTracer.Provider.Function("MapiMailbox.CreateStoreConnection", new object[0]);
			base.CheckDisposed();
			if (!base.IsPureMAPI && !base.IsRestore && !base.IsPublicFolderMigrationSource)
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
			if (base.IsPublicFolderMigrationSource)
			{
				if (base.IsPureMAPI)
				{
				}
			}
			else
			{
				bool isRestore = base.IsRestore;
			}
			OpenStoreFlag openStoreFlag;
			ConnectFlag connectFlag;
			this.GetConnectionFlags(out openStoreFlag, out connectFlag);
			base.VerifyRestoreSource(mailboxConnectFlags);
			bool flag = false;
			string userName;
			string domainName;
			string password;
			base.GetCreds(out userName, out domainName, out password);
			TimeSpan timeSpan;
			TimeSpan callTimeout;
			if (base.IsPureMAPI || base.Credential != null)
			{
				timeSpan = base.TestIntegration.RemoteMailboxConnectionTimeout;
				callTimeout = base.TestIntegration.RemoteMailboxCallTimeout;
			}
			else
			{
				timeSpan = base.TestIntegration.LocalMailboxConnectionTimeout;
				callTimeout = base.TestIntegration.LocalMailboxCallTimeout;
			}
			string text = ((mailboxConnectFlags & MailboxConnectFlags.NonMrsLogon) != MailboxConnectFlags.None) ? "Client=Management" : "Client=MSExchangeMigration";
			if ((mailboxConnectFlags & MailboxConnectFlags.PublicFolderHierarchyReplication) != MailboxConnectFlags.None)
			{
				text = "Client=PublicFolderSystem;Action=PublicFolderHierarchyReplication";
			}
			byte[] tenantPartitionHint = (base.PartitionHint != null) ? TenantPartitionHint.Serialize(base.PartitionHint) : null;
			MapiStore result;
			for (;;)
			{
				string text2 = (!string.IsNullOrEmpty(serverFqdn)) ? serverFqdn : serverLegDN;
				bool flag2 = (connectFlag & ConnectFlag.ConnectToExchangeRpcServerOnly) != ConnectFlag.None;
				CultureInfo cultureInfo;
				if (base.IsPureMAPI)
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
				else
				{
					cultureInfo = null;
					openStoreFlag |= OpenStoreFlag.NoLocalization;
				}
				MrsTracer.Provider.Debug("Opening MapiStore: serverDN=\"{0}\", mailbox=\"{1}\", connectFlags=[{2}], openStoreFlags=[{3}], timeout={4}", new object[]
				{
					text2,
					base.TraceMailboxId,
					connectFlag,
					openStoreFlag,
					timeSpan
				});
				result = null;
				try
				{
					using (base.RHTracker.Start())
					{
						if (base.IsPublicFolderMigrationSource)
						{
							if (base.IsPureMAPI)
							{
								result = MapiStore.OpenPublicStore(text2, base.MailboxDN, userName, domainName, password, this.HTTPProxyServerName, connectFlag, openStoreFlag, cultureInfo, null, text, timeSpan, callTimeout);
							}
							else
							{
								result = MapiStore.OpenPublicStore(text2, Guid.Empty, Server.GetSystemAttendantLegacyDN(serverLegDN), userName, domainName, password, connectFlag, openStoreFlag, cultureInfo, null, text, timeSpan, callTimeout);
							}
						}
						else if (base.IsRestore)
						{
							result = MapiStore.OpenMailbox(text2, Server.GetSystemAttendantLegacyDN(serverLegDN), base.MailboxGuid, base.MdbGuid, userName, domainName, password, connectFlag, openStoreFlag, cultureInfo, null, text, timeSpan, callTimeout, tenantPartitionHint);
						}
						else if (base.IsPureMAPI || base.IsTitanium || flag2)
						{
							string text3 = base.MailboxDN;
							string userDn = text3;
							if (base.IsPureMAPI && !string.IsNullOrEmpty(this.UserLegDN))
							{
								userDn = this.UserLegDN;
							}
							if (base.IsArchiveMailbox)
							{
								text3 = text3 + "/guid=" + this.archiveGuid.ToString();
							}
							result = MapiStore.OpenMailbox(text2, userDn, text3, userName, domainName, password, this.HTTPProxyServerName, connectFlag, openStoreFlag, cultureInfo, null, text, timeSpan, callTimeout);
						}
						else
						{
							openStoreFlag |= OpenStoreFlag.MailboxGuid;
							result = MapiStore.OpenMailbox(text2, base.MailboxDN, base.MailboxGuid, base.MdbGuid, userName, domainName, password, connectFlag, openStoreFlag, cultureInfo, null, text, timeSpan, callTimeout, tenantPartitionHint);
						}
						MapiUtils.StartMapiDeadSessionChecking(result, base.TraceMailboxId);
					}
				}
				catch (MapiExceptionNotFound originalException)
				{
					base.VerifyMdbIsOnline(originalException);
					throw;
				}
				catch (MapiExceptionLogonFailed originalException2)
				{
					if (!base.IsPureMAPI && !flag)
					{
						MrsTracer.Provider.Debug("OpenMailbox returned LogonFailed, forcing AM rediscovery", new object[0]);
						base.ResolveMDB(true);
						serverLegDN = base.ServerDN;
						serverFqdn = base.ServerFqdn;
						flag = true;
						continue;
					}
					base.VerifyMdbIsOnline(originalException2);
					throw;
				}
				catch (MapiExceptionWrongServer)
				{
					if (!base.IsPureMAPI && !flag)
					{
						MrsTracer.Provider.Debug("OpenMailbox returned WrongServer, forcing AM rediscovery", new object[0]);
						base.ResolveMDB(true);
						serverLegDN = base.ServerDN;
						serverFqdn = base.ServerFqdn;
						flag = true;
						continue;
					}
					throw;
				}
				catch (MapiExceptionNetworkError mapiExceptionNetworkError)
				{
					if (base.IsPureMAPI)
					{
						if (base.IsPublicFolderMigrationSource)
						{
							if (connectFlag.HasFlag(ConnectFlag.PublicFolderMigration))
							{
								MrsTracer.Provider.Debug("PureMAPI OpenPublicStore returned ecNetworkError {0}, retrying without PublicFolderMigration flag", new object[]
								{
									mapiExceptionNetworkError
								});
								connectFlag &= ~ConnectFlag.PublicFolderMigration;
								continue;
							}
						}
						else if (!flag2)
						{
							MrsTracer.Provider.Debug("PureMAPI OpenMailbox returned ecNetworkError {0}, retrying with MoMT flags", new object[]
							{
								mapiExceptionNetworkError
							});
							connectFlag |= ConnectFlag.ConnectToExchangeRpcServerOnly;
							openStoreFlag |= OpenStoreFlag.NoExtendedFlags;
							continue;
						}
					}
					throw;
				}
				catch (MapiExceptionMailboxInTransit innerException)
				{
					throw this.GetMailboxInTransitException(innerException);
				}
				break;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005E0C File Offset: 0x0000400C
		private void GetConnectionFlags(out OpenStoreFlag osFlags, out ConnectFlag connectFlags)
		{
			osFlags = OpenStoreFlag.TakeOwnership;
			connectFlags = ConnectFlag.None;
			if ((base.Flags & LocalMailboxFlags.CredentialIsNotAdmin) == LocalMailboxFlags.None)
			{
				osFlags |= OpenStoreFlag.UseAdminPrivilege;
				connectFlags |= ConnectFlag.UseAdminPrivilege;
			}
			if (base.IsRestore)
			{
				osFlags |= (OpenStoreFlag.OverrideHomeMdb | OpenStoreFlag.NoLocalization | OpenStoreFlag.MailboxGuid);
				if ((base.RestoreFlags & MailboxRestoreType.Recovery) != MailboxRestoreType.None)
				{
					osFlags |= OpenStoreFlag.RestoreDatabase;
				}
				if ((base.RestoreFlags & MailboxRestoreType.SoftDeleted) != MailboxRestoreType.None || (base.RestoreFlags & MailboxRestoreType.Disabled) != MailboxRestoreType.None)
				{
					osFlags |= OpenStoreFlag.DisconnectedMailbox;
				}
			}
			if (base.Credential == null && !base.IsTitanium && !base.IsExchange2007)
			{
				connectFlags |= ConnectFlag.UseRpcContextPool;
			}
			if (!string.IsNullOrEmpty(this.HTTPProxyServerName))
			{
				connectFlags |= ConnectFlag.UseHTTPS;
				if ((base.Flags & LocalMailboxFlags.UseNTLMAuth) != LocalMailboxFlags.None)
				{
					connectFlags |= ConnectFlag.UseNTLM;
				}
				if (base.IsPublicFolderMigrationSource)
				{
					connectFlags |= ConnectFlag.PublicFolderMigration;
				}
			}
			if (base.IsTitanium || base.IsExchange2007)
			{
				connectFlags |= ConnectFlag.AllowLegacyStore;
			}
			if (base.IsPublicFolderMigrationSource)
			{
				connectFlags |= ConnectFlag.AllowLegacyStore;
				connectFlags &= ~(ConnectFlag.ConnectToExchangeRpcServerOnly | ConnectFlag.UseRpcContextPool);
				osFlags |= OpenStoreFlag.IgnoreHomeMdb;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005F20 File Offset: 0x00004120
		private MapiStore FindStoreSession(string[] replicaLegDNs)
		{
			MapiStore mapiStore = null;
			if (this.additionalSessions == null)
			{
				this.additionalSessions = new Dictionary<string, MapiStore>(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				foreach (string text in replicaLegDNs)
				{
					if (this.additionalSessions.TryGetValue(text, out mapiStore))
					{
						MrsTracer.Provider.Debug("Located an existing store session '{0}'", new object[]
						{
							text
						});
						return mapiStore;
					}
				}
			}
			if (this.additionalSessions.Count + 1 >= base.TestIntegration.MaxOpenConnectionsPerPublicFolderMigration)
			{
				throw new UnexpectedErrorPermanentException(-2147221230);
			}
			for (int j = 0; j < replicaLegDNs.Length; j++)
			{
				string text2 = replicaLegDNs[j];
				bool flag = true;
				try
				{
					MrsTracer.Provider.Debug("Connecting to '{0}'", new object[]
					{
						text2
					});
					mapiStore = this.CreateStoreConnection(text2, null, MailboxConnectFlags.None);
					if (base.Flags.HasFlag(LocalMailboxFlags.ParallelPublicFolderMigration))
					{
						ServerVersion sourceServerVersion = new ServerVersion(mapiStore.VersionMajor, mapiStore.VersionMinor, mapiStore.BuildMajor, mapiStore.BuildMinor);
						ParallelPublicFolderMigrationVersionChecker.ThrowIfMinimumRequiredVersionNotInstalled(sourceServerVersion);
					}
					bool flag2;
					this.SetInTransitStatus(mapiStore, this.inTransitStatus, out flag2);
					if (!flag2)
					{
						throw new OfflinePublicFolderMigrationNotSupportedException();
					}
					this.additionalSessions.Add(text2, mapiStore);
					flag = false;
					return mapiStore;
				}
				catch (MapiExceptionNetworkError ex)
				{
					if (j >= replicaLegDNs.Length - 1)
					{
						throw;
					}
					MrsTracer.Provider.Warning("Failed to connect to '{0}', ignoring.\n{1}", new object[]
					{
						text2,
						CommonUtils.FullExceptionMessage(ex)
					});
				}
				finally
				{
					if (flag && mapiStore != null)
					{
						mapiStore.Dispose();
					}
				}
			}
			throw new UnexpectedErrorPermanentException(-2147221230);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000060E8 File Offset: 0x000042E8
		private void SetInTransitStatus(MapiStore mapiStore, InTransitStatus status, out bool onlineMoveSupported)
		{
			MrsTracer.Provider.Function("MapiMailbox.SetInTransitStatus({0})", new object[]
			{
				status
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			onlineMoveSupported = this.StoreSupportsOnlineMove;
			PropTag propTag = base.IsTitanium ? ((PropTag)1712848907U) : PropTag.InTransitStatus;
			for (;;)
			{
				object value = (int)status;
				if (base.IsTitanium)
				{
					status &= ~InTransitStatus.OnlineMove;
					value = (status != InTransitStatus.NotInTransit);
					this.StoreSupportsOnlineMove = false;
				}
				bool flag = (status & InTransitStatus.OnlineMove) != InTransitStatus.NotInTransit;
				try
				{
					PropValue[] props = new PropValue[]
					{
						new PropValue(propTag, value)
					};
					PropProblem[] array;
					using (base.RHTracker.Start())
					{
						array = mapiStore.SetProps(props);
					}
					if (array != null)
					{
						MrsTracer.Provider.Error("Failed to set InTransitStatus: error {0}", new object[]
						{
							array[0].Scode
						});
						if (array[0].Scode == -2147024891 && status != InTransitStatus.NotInTransit)
						{
							throw this.GetMailboxInTransitException(null);
						}
						throw new UnexpectedErrorPermanentException(array[0].Scode);
					}
				}
				catch (MapiExceptionCorruptData)
				{
					if (!flag)
					{
						throw;
					}
					MrsTracer.Provider.Error("Got MapiExceptionCorruptData, probably a downlevel store. Trying to set offline move status instead.", new object[0]);
					status &= ~InTransitStatus.OnlineMove;
					this.StoreSupportsOnlineMove = false;
					continue;
				}
				break;
			}
			this.inTransitStatus = status;
			onlineMoveSupported = this.StoreSupportsOnlineMove;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006270 File Offset: 0x00004470
		private void LoadFolderHierarchy(byte[] rootFolderEntryId, PropTag[] additionalPtagsToLoad, List<FolderRec> result)
		{
			MrsTracer.Provider.Function("MapiMailbox.LoadFolderHierarchy", new object[0]);
			using (MapiFolder mapiFolder = (MapiFolder)this.MapiStore.OpenEntry(rootFolderEntryId))
			{
				FolderRec folderRec = FolderRec.Create(mapiFolder, additionalPtagsToLoad);
				if (base.IsPublicFolderMigrationSource)
				{
					folderRec.FolderName = "Public Root";
				}
				bool flag = true;
				byte[] nonIpmSubtreeId = null;
				using (MapiTable hierarchyTable = mapiFolder.GetHierarchyTable(HierarchyTableFlags.ConvenientDepth))
				{
					PropTag[] propTags;
					if (additionalPtagsToLoad == null || additionalPtagsToLoad.Length == 0)
					{
						propTags = FolderRec.PtagsToLoad;
					}
					else
					{
						List<PropTag> list = new List<PropTag>();
						list.AddRange(FolderRec.PtagsToLoad);
						list.AddRange(additionalPtagsToLoad);
						propTags = list.ToArray();
					}
					foreach (PropValue[] pva in MapiUtils.QueryAllRows(hierarchyTable, null, propTags, 1000))
					{
						FolderRec folderRec2 = FolderRec.Create(pva);
						if (folderRec2 != null)
						{
							if (CommonUtils.IsSameEntryId(folderRec2.EntryId, folderRec.EntryId))
							{
								flag = false;
								if (base.IsPublicFolderMigrationSource)
								{
									folderRec2.FolderName = "Public Root";
								}
							}
							if (base.IsPublicFolderMigrationSource)
							{
								if (StringComparer.OrdinalIgnoreCase.Equals(folderRec2.FolderName, "NON_IPM_SUBTREE") && CommonUtils.IsSameEntryId(folderRec2.ParentId, folderRec.EntryId))
								{
									nonIpmSubtreeId = folderRec2.EntryId;
								}
								else if (this.ShouldSkipFolder(folderRec2, nonIpmSubtreeId))
								{
									this.publicFoldersToSkip[folderRec2.EntryId] = true;
									goto IL_144;
								}
							}
							result.Add(folderRec2);
						}
						IL_144:;
					}
				}
				if (flag)
				{
					result.Insert(0, folderRec);
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000642C File Offset: 0x0000462C
		private bool ShouldSkipFolder(FolderRec folderRec, byte[] nonIpmSubtreeId)
		{
			if (this.publicFoldersToSkip.ContainsKey(folderRec.ParentId))
			{
				return true;
			}
			bool flag = MapiMailbox.PublicFolderBranchToSkip.Contains(folderRec.FolderName) || folderRec.FolderName.StartsWith(MapiMailbox.OWAScratchPad, StringComparison.OrdinalIgnoreCase) || folderRec.FolderName.StartsWith(MapiMailbox.StoreEvents, StringComparison.OrdinalIgnoreCase);
			return flag && CommonUtils.IsSameEntryId(nonIpmSubtreeId, folderRec.ParentId);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000649C File Offset: 0x0000469C
		private MoveObjectInfo<string> GetSyncStateMOI(MapiStore mbx, byte[] key)
		{
			DateTime utcNow = DateTime.UtcNow;
			byte[] messageId;
			if (!this.syncStateMessageIds.TryGetValue(key, out messageId))
			{
				messageId = null;
			}
			string subject = string.Format(CultureInfo.InvariantCulture, "SyncState: {0}-{1} :: {2} :: {3}", new object[]
			{
				base.MailboxGuid.ToString(),
				TraceUtils.DumpBytes(key),
				utcNow.ToLongDateString(),
				utcNow.ToLongTimeString()
			});
			byte[] array = new byte[16 + key.Length];
			base.MailboxGuid.ToByteArray().CopyTo(array, 0);
			key.CopyTo(array, 16);
			return new MoveObjectInfo<string>(Guid.Empty, mbx, messageId, "MailboxReplicationService SyncStates", "IPM.MS-Exchange.MailboxSyncState", subject, array);
		}

		// Token: 0x04000013 RID: 19
		private const string ProviderName = "MapiProvider";

		// Token: 0x04000014 RID: 20
		private static readonly PropTag[] InboxProperties = new PropTag[]
		{
			PropTag.RulesSize,
			PropTag.ContentAggregationFlags
		};

		// Token: 0x04000015 RID: 21
		private static readonly string OWAScratchPad = "OWAScratchPad";

		// Token: 0x04000016 RID: 22
		private static readonly string StoreEvents = "StoreEvents";

		// Token: 0x04000017 RID: 23
		private static readonly HashSet<string> PublicFolderBranchToSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"OFFLINE ADDRESS BOOK",
			"SCHEDULE+ FREE BUSY",
			"schema-root",
			MapiMailbox.OWAScratchPad,
			MapiMailbox.StoreEvents,
			"Events Root"
		};

		// Token: 0x04000018 RID: 24
		private readonly bool createdFromMapiStore;

		// Token: 0x04000019 RID: 25
		private MapiStore store;

		// Token: 0x0400001A RID: 26
		private Dictionary<string, MapiStore> additionalSessions;

		// Token: 0x0400001B RID: 27
		private EntryIdMap<MapiStore> folderSessions;

		// Token: 0x0400001C RID: 28
		private int serverVersion;

		// Token: 0x0400001D RID: 29
		private InTransitStatus inTransitStatus;

		// Token: 0x0400001E RID: 30
		private Dictionary<byte[], byte[]> syncStateMessageIds;
	}
}
