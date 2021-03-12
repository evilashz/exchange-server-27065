using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Sharing;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Common;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000273 RID: 627
	internal sealed class SharingEngine
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x00053038 File Offset: 0x00051238
		private SharingEngine(CultureInfo culture, IExchangePrincipal exchangePrincipal, StoreId sharingFolderId, SharingSubscriptionData subscription, ExternalAuthentication externalAuthentication, ADUser adUser)
		{
			this.Culture = culture;
			this.MailboxOwner = exchangePrincipal;
			this.SharingFolderId = sharingFolderId;
			this.subscription = subscription;
			this.externalAuthentication = externalAuthentication;
			this.adUser = adUser;
			this.StartTime = DateTime.UtcNow;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00053078 File Offset: 0x00051278
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00053080 File Offset: 0x00051280
		private StoreId SharingFolderId { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00053089 File Offset: 0x00051289
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00053091 File Offset: 0x00051291
		private CultureInfo Culture { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0005309A File Offset: 0x0005129A
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x000530A2 File Offset: 0x000512A2
		private IExchangePrincipal MailboxOwner { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x000530AB File Offset: 0x000512AB
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x000530B3 File Offset: 0x000512B3
		private SharingLevelOfDetails OriginalLevelOfDetails { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000530BC File Offset: 0x000512BC
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x000530C4 File Offset: 0x000512C4
		private SharingLevelOfDetails ActualLevelOfDetails { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x000530CD File Offset: 0x000512CD
		private ExchangeService ExchangeService
		{
			get
			{
				if (this.exchangeService == null)
				{
					this.exchangeService = new ExchangeService(this.CreateAuthenticator(Offer.SharingRead), this.subscription, this.WebProxy, "ExchangeSharingSync", false);
				}
				return this.exchangeService;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00053105 File Offset: 0x00051305
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0005310D File Offset: 0x0005130D
		private string SyncState { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00053116 File Offset: 0x00051316
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0005311E File Offset: 0x0005131E
		private DateTime StartTime { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00053127 File Offset: 0x00051327
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0005312F File Offset: 0x0005132F
		private Deadline Deadline { get; set; }

		// Token: 0x060011F0 RID: 4592 RVA: 0x00053138 File Offset: 0x00051338
		private static ItemSynchronizer CreateItemSynchronizer(MailboxSession mailboxSession, LocalFolder localFolder)
		{
			IEnumerable<CultureInfo> preferredCultures = mailboxSession.MailboxOwner.PreferredCultures;
			CultureInfo cultureInfo = preferredCultures.Any<CultureInfo>() ? preferredCultures.First<CultureInfo>() : CultureInfo.CurrentCulture;
			SharingEngine.Tracer.TraceDebug<CultureInfo, IExchangePrincipal>(0L, "Culture {0} has been selected for mailbox {1}.", cultureInfo, mailboxSession.MailboxOwner);
			return ItemSynchronizer.Create(localFolder, cultureInfo);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00053188 File Offset: 0x00051388
		private IWebProxy WebProxy
		{
			get
			{
				if (this.webProxy == null)
				{
					if (Configuration.BypassProxyForCrossForestRequests)
					{
						this.webProxy = new WebProxy();
					}
					else
					{
						Server localServer = LocalServerCache.LocalServer;
						if (localServer != null && localServer.InternetWebProxy != null)
						{
							SharingEngine.Tracer.TraceDebug<Uri>(0L, "Using custom InternetWebProxy {0}", localServer.InternetWebProxy);
							this.webProxy = new WebProxy(localServer.InternetWebProxy);
						}
					}
				}
				return this.webProxy;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000531F6 File Offset: 0x000513F6
		public static bool SyncFolder(MailboxSession mailboxSession, StoreId sharingFolderId)
		{
			return SharingEngine.SyncFolder(mailboxSession, sharingFolderId, new Deadline(SharingEngine.defaultProcessingMaxTime));
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0005320C File Offset: 0x0005140C
		public static bool SyncFolder(MailboxSession mailboxSession, StoreId sharingFolderId, Deadline processingDeadline)
		{
			if (!SharingEngine.tracker.Start(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, sharingFolderId))
			{
				SharingEngine.Tracer.TraceError<StoreId>(0L, "{0}: Folder is already syncing", sharingFolderId);
				throw new PendingSynchronizationException();
			}
			bool result = true;
			SharingEngine sharingEngine = null;
			LocalFolder localFolder = null;
			ItemSynchronizer itemSynchronizer = null;
			try
			{
				SharingLog.LogEntry(mailboxSession, "Starting sync for folder " + sharingFolderId);
				localFolder = SharingEngine.GetLocalFolder(mailboxSession, sharingFolderId);
				localFolder.UpdateLastAttemptedSyncTime();
				itemSynchronizer = SharingEngine.CreateItemSynchronizer(mailboxSession, localFolder);
				sharingEngine = SharingEngine.Create(mailboxSession, sharingFolderId);
				result = sharingEngine.Execute(mailboxSession, localFolder, itemSynchronizer, processingDeadline);
			}
			catch (Exception arg)
			{
				SharingLog.LogEntry(mailboxSession, string.Format("Synchronization finished unexpectedly due to the following error: {0}, External Url {1}", arg, (sharingEngine != null && sharingEngine.ExchangeService != null) ? sharingEngine.ExchangeService.Url : "Not specified"));
				throw;
			}
			finally
			{
				SharingEngine.UnblockFolderSync(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, sharingFolderId);
				if (itemSynchronizer != null)
				{
					itemSynchronizer = null;
				}
				if (localFolder != null)
				{
					localFolder.Dispose();
					localFolder = null;
				}
				if (sharingEngine != null)
				{
					sharingEngine.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00053314 File Offset: 0x00051514
		public static void ValidateFolder(MailboxSession mailboxSession, StoreId sharingFolderId)
		{
			SharingEngine.GetADUser(mailboxSession);
			SharingEngine.GetSubscriptionData(mailboxSession, StoreId.GetStoreObjectId(sharingFolderId));
			using (SharingEngine.BindToLocalFolder(mailboxSession, sharingFolderId))
			{
			}
			SharingEngine.GetExternalAuthentication(mailboxSession);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00053360 File Offset: 0x00051560
		public override string ToString()
		{
			return this.MailboxOwner.ToString() + "/" + this.SharingFolderId.ToString();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00053382 File Offset: 0x00051582
		private static void UnblockFolderSync(Guid mailboxGuid, StoreId sharingFolderId)
		{
			SharingEngine.Tracer.TraceDebug<StoreId>(0L, "{0}: Unblock folder sync", sharingFolderId);
			SharingEngine.tracker.End(mailboxGuid, sharingFolderId);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000533A4 File Offset: 0x000515A4
		private static SharingEngine Create(MailboxSession mailboxSession, StoreId sharingFolderId)
		{
			ADUser aduser = SharingEngine.GetADUser(mailboxSession);
			SharingSubscriptionData subscriptionData = SharingEngine.GetSubscriptionData(mailboxSession, (StoreObjectId)sharingFolderId);
			SharingEngine.Tracer.TraceDebug<IExchangePrincipal, SharingSubscriptionData>(0L, "{0}: Found subscription data: {1}", mailboxSession.MailboxOwner, subscriptionData);
			ExternalAuthentication externalAuthentication = SharingEngine.GetExternalAuthentication(mailboxSession);
			return new SharingEngine(mailboxSession.Culture, mailboxSession.MailboxOwner, sharingFolderId, subscriptionData, externalAuthentication, aduser);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000533FC File Offset: 0x000515FC
		private static ExternalAuthentication GetExternalAuthentication(MailboxSession mailboxSession)
		{
			ExternalAuthentication current = ExternalAuthentication.GetCurrent();
			if (!current.Enabled)
			{
				SharingEngine.Tracer.TraceError<IExchangePrincipal>(0L, "{0}: The organization is not federated.", mailboxSession.MailboxOwner);
				throw new OrganizationNotFederatedException();
			}
			return current;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00053438 File Offset: 0x00051638
		private static ADUser GetADUser(MailboxSession mailboxSession)
		{
			ADUser aduser = DirectoryHelper.ReadADRecipient(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.IsArchive, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)) as ADUser;
			if (aduser == null)
			{
				SharingEngine.Tracer.TraceDebug<IExchangePrincipal>(0L, "{0}: cannot locate user object in AD", mailboxSession.MailboxOwner);
				throw new ADUserNotFoundException();
			}
			return aduser;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0005349C File Offset: 0x0005169C
		private static SharingSubscriptionData GetSubscriptionData(MailboxSession mailboxSession, StoreObjectId sharingFolderId)
		{
			SharingSubscriptionData result;
			using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(mailboxSession))
			{
				SharingSubscriptionData byLocalFolderId = sharingSubscriptionManager.GetByLocalFolderId(sharingFolderId);
				if (byLocalFolderId == null)
				{
					SharingEngine.Tracer.TraceError<StoreObjectId>(0L, "{0}: No subscription for this folder", sharingFolderId);
					throw new SubscriptionNotFoundException();
				}
				SharingEngine.Tracer.TraceDebug<StoreObjectId, SharingSubscriptionData>(0L, "{0}: Found subscription: {1}", sharingFolderId, byLocalFolderId);
				result = byLocalFolderId;
			}
			return result;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00053508 File Offset: 0x00051708
		private static LocalFolder GetLocalFolder(MailboxSession mailboxSession, StoreId sharingFolderId)
		{
			LocalFolder result;
			try
			{
				result = LocalFolder.Bind(mailboxSession, sharingFolderId);
			}
			catch (ObjectNotFoundException ex)
			{
				SharingEngine.Tracer.TraceError<IExchangePrincipal, ObjectNotFoundException>(0L, "{0}: Failed to bind or initialize the local folder. Exception: {1}", mailboxSession.MailboxOwner, ex);
				throw new SharingFolderNotFoundException(ex);
			}
			return result;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00053550 File Offset: 0x00051750
		private static LocalFolder BindToLocalFolder(MailboxSession mailboxSession, StoreId sharingFolderId)
		{
			LocalFolder result;
			try
			{
				result = LocalFolder.BindOnly(mailboxSession, sharingFolderId);
			}
			catch (ObjectNotFoundException ex)
			{
				SharingEngine.Tracer.TraceError<IExchangePrincipal, ObjectNotFoundException>(0L, "{0}: Failed to bind to local folder. Exception: {1}", mailboxSession.MailboxOwner, ex);
				throw new SharingFolderNotFoundException(ex);
			}
			return result;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00053598 File Offset: 0x00051798
		private void Dispose()
		{
			if (this.exchangeService != null)
			{
				this.exchangeService.Dispose();
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000535B0 File Offset: 0x000517B0
		private bool Execute(MailboxSession mailboxSession, LocalFolder localFolder, ItemSynchronizer itemSynchronizer, Deadline processingDeadline)
		{
			SharingEngine.Tracer.TraceDebug<SharingEngine>(0L, "{0}: Starting synchrounous execution", this);
			this.Deadline = processingDeadline;
			bool flag = false;
			bool flag2;
			try
			{
				flag2 = this.SynchronizeFolder(mailboxSession, localFolder, itemSynchronizer);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					PerformanceCounters.FolderSynchronizationFailures.Increment();
				}
				else
				{
					this.UpdateTimeBasedPerfCounters();
				}
			}
			if (flag2)
			{
				PerformanceCounters.FoldersProcessedSynchronously.Increment();
			}
			else
			{
				PerformanceCounters.SynchronizationTimeouts.Increment();
			}
			SharingEngine.Tracer.TraceDebug<SharingEngine>(0L, "{0}: Ended synchrounous execution", this);
			return flag2;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0005363C File Offset: 0x0005183C
		private bool SynchronizeFolder(MailboxSession mailboxSession, LocalFolder localFolder, ItemSynchronizer itemSynchronizer)
		{
			BaseFolderType folder = this.GetFolder(mailboxSession, localFolder);
			this.ActualLevelOfDetails = new SharingLevelOfDetails(folder);
			this.OriginalLevelOfDetails = new SharingLevelOfDetails(localFolder.LoadLevelOfDetails());
			if (this.OriginalLevelOfDetails == LevelOfDetails.None)
			{
				SharingEngine.Tracer.TraceDebug<SharingEngine, SharingLevelOfDetails>((long)this.GetHashCode(), "{0}: Access level changed from None to {1}. Saving the actual access level.", this, this.ActualLevelOfDetails);
				localFolder.SaveLevelOfDetails(this.ActualLevelOfDetails);
			}
			if (this.ActualLevelOfDetails == LevelOfDetails.None)
			{
				SharingEngine.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Permissions revoked for this folder.", localFolder);
				localFolder.DeleteAllItems();
				localFolder.DeleteSyncState();
				localFolder.SaveLevelOfDetails(this.ActualLevelOfDetails);
				SharingEngine.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: All items have been deleted.", localFolder);
				SharingLog.LogEntry(mailboxSession, "User no longer has permissions to the publisher's folder. All items have been removed.");
				return true;
			}
			if (this.ActualLevelOfDetails > this.OriginalLevelOfDetails)
			{
				SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: Permission to the remote folder has increased. Resetting the sync state.", this);
				this.SyncState = null;
				localFolder.DeleteSyncState();
			}
			SharingLog.LogEntry(mailboxSession, "Current permissions to the publisher's folder: " + this.ActualLevelOfDetails);
			return this.SynchronizeFolderItems(mailboxSession, localFolder, itemSynchronizer);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00053768 File Offset: 0x00051968
		private BaseFolderType GetFolder(MailboxSession mailboxSession, LocalFolder localFolder)
		{
			BaseFolderType folder;
			try
			{
				folder = this.ExchangeService.GetFolder(localFolder.RemoteFolderId);
			}
			catch (FailedCommunicationException ex)
			{
				SharingEngine.Tracer.TraceDebug<SharingEngine, FailedCommunicationException>((long)this.GetHashCode(), "{0}: GetFolder failed with the following exception: {1}", this, ex);
				SharingLog.LogEntry(mailboxSession, "GetFolder failed with the following exception: " + ex);
				SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: Failure was due connection error which might indicate the EWS endpoint needs update", this);
				Uri uri;
				try
				{
					uri = this.Discover();
				}
				catch (LocalizedException ex2)
				{
					SharingEngine.Tracer.TraceDebug<SharingEngine, LocalizedException>((long)this.GetHashCode(), "{0}: Unable to discover new EWS endpoint because of exception: {1}", this, ex2);
					throw new AutodiscoverFailedException(ex2, ex);
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(uri.ToString(), this.ExchangeService.Url))
				{
					SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: EWS endpoint continues to be the same, failing with original exception", this);
					throw ex;
				}
				this.ExchangeService.Url = uri.ToString();
				folder = this.ExchangeService.GetFolder(localFolder.RemoteFolderId);
				SharingLog.LogEntry(mailboxSession, "The Sharing Url for this subscription has been updated to " + uri);
				this.UpdateSubscriptionData(mailboxSession, uri);
			}
			return folder;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00053888 File Offset: 0x00051A88
		private bool SynchronizeFolderItems(MailboxSession mailboxSession, LocalFolder localFolder, ItemSynchronizer itemSynchronizer)
		{
			bool result = false;
			SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: Start syncing folder", this);
			try
			{
				while (this.BatchSync(mailboxSession, localFolder, itemSynchronizer))
				{
					if (this.Deadline.IsOver)
					{
						SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: Timed out while syncing new items.", this);
						break;
					}
				}
			}
			finally
			{
				localFolder.DeleteSelectedItems();
			}
			this.EnforceLevelOfDetails(mailboxSession, localFolder, itemSynchronizer);
			localFolder.SaveLevelOfDetails(this.ActualLevelOfDetails);
			SharingEngine.Tracer.TraceDebug<SharingEngine>((long)this.GetHashCode(), "{0}: End syncing folder", this);
			if (!this.Deadline.IsOver)
			{
				result = true;
				localFolder.UpdateLastSyncTimes();
				SharingLog.LogEntry(mailboxSession, "Synchronization completed.");
			}
			return result;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00053978 File Offset: 0x00051B78
		private void EnforceLevelOfDetails(MailboxSession mailboxSession, LocalFolder localFolder, ItemSynchronizer itemSynchronizer)
		{
			if (this.OriginalLevelOfDetails > this.ActualLevelOfDetails)
			{
				SharingEngine.Tracer.TraceDebug<SharingEngine, SharingLevelOfDetails, SharingLevelOfDetails>((long)this.GetHashCode(), "{0}: Updating existing items due to permission changes. Original level: {1}. Actual level: {2}.", this, this.OriginalLevelOfDetails, this.ActualLevelOfDetails);
				localFolder.ProcessAllItems(delegate(StoreId localItemId)
				{
					itemSynchronizer.EnforceLevelOfDetails(mailboxSession, localItemId, this.ActualLevelOfDetails);
				});
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000539F0 File Offset: 0x00051BF0
		private bool BatchSync(MailboxSession mailboxSession, LocalFolder localFolder, ItemSynchronizer itemSynchronizer)
		{
			List<ItemIdType> list = new List<ItemIdType>();
			if (string.IsNullOrEmpty(this.SyncState))
			{
				this.SyncState = localFolder.LoadSyncState();
			}
			Changes changes = this.ExchangeService.GetChanges(128, this.SyncState);
			if (changes == null)
			{
				SharingEngine.Tracer.TraceDebug<LocalFolder>((long)this.GetHashCode(), "{0}: Unable to retrieve changes", localFolder);
				return false;
			}
			SharingEngine.Tracer.TraceDebug<SharingEngine, int>((long)this.GetHashCode(), "{0}: Called SyncFolderItems. Got {1} changes", this, changes.Items.Length);
			foreach (ItemChange itemChange in changes.Items)
			{
				switch (itemChange.ChangeType)
				{
				case ItemChangeType.Create:
				case ItemChangeType.Update:
					list.Add(itemChange.Id);
					break;
				case ItemChangeType.Delete:
				{
					StoreId localIdFromRemoteId = localFolder.GetLocalIdFromRemoteId(itemChange.Id.Id);
					if (localIdFromRemoteId != null)
					{
						localFolder.SelectItemToDelete(localIdFromRemoteId);
					}
					break;
				}
				}
			}
			if (list.Count > 0)
			{
				SharingEngine.Tracer.TraceDebug<SharingEngine, int>((long)this.GetHashCode(), "{0}: Processing {1} creates/updates", this, list.Count);
				IEnumerable<ItemType> item = this.ExchangeService.GetItem(list.ToArray(), localFolder.Type);
				if (item != null)
				{
					int num = 0;
					foreach (ItemType remoteItem in item)
					{
						if (this.Deadline.IsOver)
						{
							SharingEngine.Tracer.TraceError<SharingEngine>((long)this.GetHashCode(), "{0}: run out of time for completing the sync", this);
							return true;
						}
						if (!this.TrySyncOneItem(mailboxSession, itemSynchronizer, remoteItem))
						{
							num++;
							SharingEngine.Tracer.TraceError<SharingEngine, int>((long)this.GetHashCode(), "{0}: Item failed to sync. Total number of failures during this batch: {1}.", this, num);
							if (num == 30)
							{
								SharingEngine.Tracer.TraceError<SharingEngine>((long)this.GetHashCode(), "{0}: Too many items have failed. Ending batch loop.", this);
								break;
							}
						}
					}
					SharingLog.LogEntry(mailboxSession, string.Format("Synchronized {0} out of {1} items during this batch.", list.Count - num, list.Count));
					if (num == list.Count || num == 30)
					{
						SharingEngine.Tracer.TraceError<SharingEngine>((long)this.GetHashCode(), "{0}: Excessive errors while processing batch. Sync state will not be saved.", this);
						throw new BatchSynchronizationFailedException();
					}
				}
			}
			localFolder.SaveSyncState(changes.SyncState);
			this.SyncState = changes.SyncState;
			return changes.MoreChangesAvailable;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00053D44 File Offset: 0x00051F44
		private bool TrySyncOneItem(MailboxSession mailboxSession, ItemSynchronizer itemSynchronizer, ItemType remoteItem)
		{
			SharingEngine.Tracer.TraceDebug<SharingEngine, string>((long)this.GetHashCode(), "{0}: Syncing item {1}", this, remoteItem.ItemId.Id);
			LocalizedException exception = null;
			try
			{
				Exception syncException = null;
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						itemSynchronizer.Sync(remoteItem, mailboxSession, this.ExchangeService);
					}
					catch (FailedCommunicationException syncException)
					{
						FailedCommunicationException syncException = syncException;
					}
					catch (StorageTransientException syncException2)
					{
						FailedCommunicationException syncException = syncException2;
					}
					catch (ObjectNotFoundException syncException3)
					{
						FailedCommunicationException syncException = syncException3;
					}
					catch (RecurrenceException exception2)
					{
						exception = exception2;
					}
					catch (TimeZoneException exception3)
					{
						exception = exception3;
					}
					catch (ObjectValidationException syncException4)
					{
						FailedCommunicationException syncException = syncException4;
					}
				});
				if (syncException != null)
				{
					SharingEngine.Tracer.TraceError<SharingEngine, Exception>((long)this.GetHashCode(), "{0}: Transient failure while syncing an item. Exception: {1}", this, syncException);
					throw new BatchSynchronizationFailedException(syncException);
				}
			}
			catch (GrayException exception)
			{
				GrayException exception4;
				exception = exception4;
			}
			if (exception != null)
			{
				SharingEngine.Tracer.TraceError<SharingEngine, string, LocalizedException>((long)this.GetHashCode(), "{0}: Error syncing item {1}. Exception: {2}", this, remoteItem.ItemId.Id, exception);
				SharingLog.LogEntry(mailboxSession, string.Format("Error syncing item - Subject: {0}, Id: {1}, Exception: {2}", remoteItem.Subject, remoteItem.ItemId.Id, exception));
				return false;
			}
			return true;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00053E84 File Offset: 0x00052084
		private void UpdateSubscriptionData(MailboxSession mailboxSession, Uri sharingUrl)
		{
			using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(mailboxSession))
			{
				SharingSubscriptionData sharingSubscriptionData = this.subscription;
				int num = 0;
				for (;;)
				{
					sharingSubscriptionData.SharingUrl = sharingUrl;
					num++;
					bool throwIfConflict = num > 3;
					sharingSubscriptionData = sharingSubscriptionManager.CreateOrUpdate(sharingSubscriptionData, throwIfConflict);
					if (StringComparer.OrdinalIgnoreCase.Equals(sharingSubscriptionData.SharingUrl, sharingUrl))
					{
						break;
					}
					sharingSubscriptionData = sharingSubscriptionManager.GetByLocalFolderId(sharingSubscriptionData.LocalFolderId);
				}
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00053EF8 File Offset: 0x000520F8
		private void UpdateTimeBasedPerfCounters()
		{
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(this.StartTime);
			PerformanceCounters.LastFolderSynchronizationTime.RawValue = (long)timeSpan.TotalSeconds;
			PerformanceCounters.AverageFolderSynchronizationTime.IncrementBy(timeSpan.Ticks);
			PerformanceCounters.AverageFolderSynchronizationTimeBase.Increment();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00053F48 File Offset: 0x00052148
		private SoapHttpClientAuthenticator CreateAuthenticator(Offer offer)
		{
			DelegationTokenRequest delegationTokenRequest = this.GetDelegationTokenRequest(offer);
			SharingEngine.Tracer.TraceDebug<DelegationTokenRequest>((long)this.GetHashCode(), "Requesting token for: {0}", delegationTokenRequest);
			if (this.adUser == null)
			{
				SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "adUser is null here");
				throw new ADUserNotFoundException();
			}
			if (this.externalAuthentication == null)
			{
				SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "externalAuthentication is null here");
				throw new InvalidParamException(Strings.ArgumentValidationFailedException("externalAuthentication"));
			}
			SecurityTokenService securityTokenService = this.externalAuthentication.GetSecurityTokenService(this.adUser.OrganizationId);
			if (securityTokenService == null)
			{
				SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "securityTokenService is null here");
				throw new InvalidParamException(Strings.ArgumentValidationFailedException("securityTokenService"));
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool flag = false;
			RequestedToken requestedToken;
			try
			{
				requestedToken = securityTokenService.IssueToken(delegationTokenRequest);
				flag = true;
			}
			catch (WSTrustException ex)
			{
				SharingEngine.Tracer.TraceError<WSTrustException>((long)this.GetHashCode(), "Unable to request token due exception: {0}", ex);
				throw new FailedCommunicationException(ex, delegationTokenRequest);
			}
			finally
			{
				if (flag)
				{
					stopwatch.Stop();
					PerformanceCounters.AverageExternalAuthenticationTokenRequestTime.IncrementBy(stopwatch.ElapsedTicks);
					PerformanceCounters.AverageExternalAuthenticationTokenRequestTimeBase.Increment();
					PerformanceCounters.SuccessfulExternalAuthenticationTokenRequests.Increment();
				}
				else
				{
					PerformanceCounters.FailedExternalAuthenticationTokenRequests.Increment();
				}
			}
			if (requestedToken == null)
			{
				SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "token is null here");
				throw new InvalidParamException(Strings.ArgumentValidationFailedException("token"));
			}
			if (this.subscription == null)
			{
				SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "subscription is null here");
				throw new SubscriptionNotFoundException();
			}
			XmlElement any = SharingKeyHandler.Encrypt(new SmtpAddress(this.subscription.SharingKey), requestedToken.ProofToken);
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.Create(requestedToken);
			soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(new SharingSecurityHeader(any));
			return soapHttpClientAuthenticator;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00054128 File Offset: 0x00052328
		private DelegationTokenRequest GetDelegationTokenRequest(Offer offer)
		{
			Exception ex2;
			try
			{
				return new DelegationTokenRequest
				{
					FederatedIdentity = this.adUser.GetFederatedIdentity(),
					EmailAddress = this.adUser.GetFederatedSmtpAddress().ToString(),
					Target = new TokenTarget(this.subscription.SharerIdentityFederationUri),
					Offer = offer
				};
			}
			catch (UserWithoutFederatedProxyAddressException ex)
			{
				ex2 = ex;
			}
			catch (FederatedIdentityMisconfiguredException ex3)
			{
				ex2 = ex3;
			}
			catch (InvalidFederatedOrganizationIdException ex4)
			{
				ex2 = ex4;
			}
			SharingEngine.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Unable to get delegation token because of exception: {0}", ex2);
			throw new ADUserMisconfigureException(ex2);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000541E8 File Offset: 0x000523E8
		private Uri Discover()
		{
			string sharerIdentity = this.subscription.SharerIdentity;
			Uri result;
			using (AutodiscoverClient autodiscoverClient = new AutodiscoverClient())
			{
				autodiscoverClient.RequestedServerVersion = DefaultBinding_Autodiscover.Exchange2010RequestedServerVersion;
				autodiscoverClient.Authenticator = this.CreateAuthenticator(Offer.Autodiscover);
				autodiscoverClient.Proxy = this.WebProxy;
				autodiscoverClient.UserAgent = "ExchangeSharingSync";
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1351, "Discover", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\Sharing\\SharingEngine.cs");
				string[] autodiscoverTrustedHosters = topologyConfigurationSession.GetAutodiscoverTrustedHosters();
				if (autodiscoverTrustedHosters != null)
				{
					autodiscoverClient.AllowedHostnames.AddRange(autodiscoverTrustedHosters);
				}
				UserSettings userSettings = SharingEngine.SingleGetUserSettings.Discover(autodiscoverClient, sharerIdentity);
				if (userSettings.IsSettingError("ExternalEwsUrl"))
				{
					SharingEngine.Tracer.TraceError<UserSettings>((long)this.GetHashCode(), "ExternalEwsUrl not present in response: {0}", userSettings);
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				UserSetting setting = userSettings.GetSetting("ExternalEwsUrl");
				if (setting == null)
				{
					SharingEngine.Tracer.TraceError<UserSettings>((long)this.GetHashCode(), "ExternalEwsUrl not present in response: {0}", userSettings);
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				StringSetting stringSetting = setting as StringSetting;
				if (stringSetting == null)
				{
					SharingEngine.Tracer.TraceError<Type>((long)this.GetHashCode(), "Unexpected type for ExternalEwsUrl in response. Type is {0}", setting.GetType());
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				if (string.IsNullOrEmpty(stringSetting.Value))
				{
					SharingEngine.Tracer.TraceError((long)this.GetHashCode(), "Empty value returned for ExternalEwsUrl in response.");
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				if (!Uri.IsWellFormedUriString(stringSetting.Value, UriKind.Absolute))
				{
					SharingEngine.Tracer.TraceError<string>((long)this.GetHashCode(), "Value returned for ExternalEwsUrl is not well-formed URL: {0}", stringSetting.Value);
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				Uri uri = new Uri(EwsWsSecurityUrl.Fix(stringSetting.Value));
				if (uri.Scheme != Uri.UriSchemeHttps)
				{
					SharingEngine.Tracer.TraceError<string>((long)this.GetHashCode(), "Response has non-HTTPS ExternalEwsUrl: {0}", stringSetting.Value);
					throw new AutodiscoverException(Strings.UnexpectedUserResponses, userSettings);
				}
				SharingEngine.Tracer.TraceDebug<Uri>((long)this.GetHashCode(), "Received valid response: {0}", uri);
				result = uri;
			}
			return result;
		}

		// Token: 0x04000BC0 RID: 3008
		private const string ComponentId = "ExchangeSharingSync";

		// Token: 0x04000BC1 RID: 3009
		private const string ExternalEwsUrl = "ExternalEwsUrl";

		// Token: 0x04000BC2 RID: 3010
		private const int MaxFailuresPerBatch = 30;

		// Token: 0x04000BC3 RID: 3011
		internal const int BatchSize = 128;

		// Token: 0x04000BC4 RID: 3012
		private static readonly string[] RequestedSettings = new string[]
		{
			"ExternalEwsUrl"
		};

		// Token: 0x04000BC5 RID: 3013
		private static readonly SingleGetUserSettings SingleGetUserSettings = new SingleGetUserSettings(SharingEngine.RequestedSettings)
		{
			UseWSSecurityUrl = true
		};

		// Token: 0x04000BC6 RID: 3014
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.SharingEngineTracer;

		// Token: 0x04000BC7 RID: 3015
		private static TimeSpan defaultProcessingMaxTime = TimeSpan.FromSeconds(60.0);

		// Token: 0x04000BC8 RID: 3016
		private static Tracker tracker = new Tracker();

		// Token: 0x04000BC9 RID: 3017
		private ExternalAuthentication externalAuthentication;

		// Token: 0x04000BCA RID: 3018
		private SharingSubscriptionData subscription;

		// Token: 0x04000BCB RID: 3019
		private ADUser adUser;

		// Token: 0x04000BCC RID: 3020
		private IWebProxy webProxy;

		// Token: 0x04000BCD RID: 3021
		private ExchangeService exchangeService;
	}
}
