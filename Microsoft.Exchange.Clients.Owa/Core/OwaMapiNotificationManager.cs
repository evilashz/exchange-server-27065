using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E7 RID: 487
	internal sealed class OwaMapiNotificationManager : DisposeTrackableBase
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x000617E0 File Offset: 0x0005F9E0
		internal OwaMapiNotificationManager(UserContext userContext)
		{
			this.userContext = userContext;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000617FC File Offset: 0x0005F9FC
		public void SubscribeForFolderChanges(OwaStoreObjectId folderId, MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (sessionIn == mailboxSession)
			{
				if (this.omnhLoggedUser == null)
				{
					this.omnhLoggedUser = new OwaMapiNotificationHandler(this.userContext, mailboxSession, null);
					this.WireConnectionDroppedHandler(this.omnhLoggedUser);
				}
				this.omnhLoggedUser.SubscribeForFolderChanges();
				this.omnhLoggedUser.AddFolderChangeNotification(folderId);
				return;
			}
			if (Utilities.IsArchiveMailbox(sessionIn))
			{
				if (this.omnhArchives == null)
				{
					this.omnhArchives = new List<OwaMapiNotificationHandler>();
				}
				OwaMapiNotificationHandler owaMapiNotificationHandler = null;
				foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhArchives)
				{
					if (owaMapiNotificationHandler2.ArchiveMailboxSession == sessionIn)
					{
						owaMapiNotificationHandler = owaMapiNotificationHandler2;
						break;
					}
				}
				if (owaMapiNotificationHandler == null)
				{
					owaMapiNotificationHandler = new OwaMapiNotificationHandler(this.userContext, sessionIn, null);
					this.WireConnectionDroppedHandler(owaMapiNotificationHandler);
					this.omnhArchives.Add(owaMapiNotificationHandler);
				}
				owaMapiNotificationHandler.SubscribeForFolderChanges();
				owaMapiNotificationHandler.AddFolderChangeNotification(folderId);
				return;
			}
			if (this.omnhDelegates == null)
			{
				this.omnhDelegates = new List<OwaMapiNotificationHandler>();
			}
			OwaMapiNotificationHandler owaMapiNotificationHandler3 = null;
			foreach (OwaMapiNotificationHandler owaMapiNotificationHandler4 in this.omnhDelegates)
			{
				if (owaMapiNotificationHandler4.DelegateSessionHandle.Session == sessionIn)
				{
					owaMapiNotificationHandler3 = owaMapiNotificationHandler4;
					break;
				}
			}
			if (owaMapiNotificationHandler3 == null)
			{
				OwaStoreObjectIdSessionHandle delegateSessionHandle = new OwaStoreObjectIdSessionHandle(folderId, this.userContext);
				owaMapiNotificationHandler3 = new OwaMapiNotificationHandler(this.userContext, sessionIn, delegateSessionHandle);
				this.WireConnectionDroppedHandler(owaMapiNotificationHandler3);
				this.omnhDelegates.Add(owaMapiNotificationHandler3);
				this.ReleaseOldestSessionIfNecessary();
			}
			owaMapiNotificationHandler3.SubscribeForFolderChanges();
			owaMapiNotificationHandler3.AddFolderChangeNotification(folderId);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000619BC File Offset: 0x0005FBBC
		public void UnsubscribeFolderChanges(OwaStoreObjectId folderId, MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (sessionIn == null || sessionIn == mailboxSession)
			{
				if (this.omnhLoggedUser != null)
				{
					this.omnhLoggedUser.SubscribeForFolderChanges();
					this.omnhLoggedUser.DeleteFolderChangeNotification(folderId);
					this.UnsubscribeFolderContentChanges(folderId);
					return;
				}
			}
			else if (Utilities.IsArchiveMailbox(sessionIn))
			{
				if (this.omnhArchives != null)
				{
					OwaMapiNotificationHandler owaMapiNotificationHandler = null;
					foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhArchives)
					{
						if (owaMapiNotificationHandler2.ArchiveMailboxSession == sessionIn)
						{
							owaMapiNotificationHandler = owaMapiNotificationHandler2;
							break;
						}
					}
					if (owaMapiNotificationHandler != null)
					{
						owaMapiNotificationHandler.SubscribeForFolderChanges();
						owaMapiNotificationHandler.DeleteFolderChangeNotification(folderId);
						return;
					}
				}
			}
			else if (this.omnhDelegates != null)
			{
				OwaMapiNotificationHandler owaMapiNotificationHandler3 = null;
				foreach (OwaMapiNotificationHandler owaMapiNotificationHandler4 in this.omnhDelegates)
				{
					if (owaMapiNotificationHandler4.DelegateSessionHandle.Session == sessionIn)
					{
						owaMapiNotificationHandler3 = owaMapiNotificationHandler4;
						break;
					}
				}
				if (owaMapiNotificationHandler3 != null)
				{
					owaMapiNotificationHandler3.SubscribeForFolderChanges();
					owaMapiNotificationHandler3.DeleteFolderChangeNotification(folderId);
				}
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00061AFC File Offset: 0x0005FCFC
		public void RenewDelegateHandler(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.omnhDelegates != null)
			{
				int i = 0;
				while (i < this.omnhDelegates.Count)
				{
					OwaMapiNotificationHandler owaMapiNotificationHandler = this.omnhDelegates[i];
					if (owaMapiNotificationHandler.DelegateSessionHandle.Session == session)
					{
						if (i < this.omnhDelegates.Count - 1)
						{
							this.omnhDelegates.Remove(owaMapiNotificationHandler);
							this.omnhDelegates.Add(owaMapiNotificationHandler);
							return;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00061B7C File Offset: 0x0005FD7C
		public void SubscribeForFolderContentChanges(MailboxSession sessionIn, OwaStoreObjectId contextFolderId, OwaStoreObjectId dataFolderId, QueryResult queryResult, ListViewContents2 listView, PropertyDefinition[] subscriptionProperties, Dictionary<PropertyDefinition, int> propertyMap, SortBy[] sortBy, FolderVirtualListViewFilter folderFilter, bool isConversationView)
		{
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			this.SubscribeForFolderChanges(contextFolderId, sessionIn);
			if (queryResult == null)
			{
				return;
			}
			if (sessionIn == mailboxSession)
			{
				FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = null;
				this.folderContentChangeNotificationHandlers.TryGetValue(contextFolderId, out folderContentChangeNotificationHandler);
				if (folderContentChangeNotificationHandler != null && folderContentChangeNotificationHandler.NeedReinitSubscriptions)
				{
					this.RemoveFolderContentChangeSubscription(contextFolderId);
					folderContentChangeNotificationHandler = null;
				}
				if (folderContentChangeNotificationHandler == null)
				{
					this.ClearOldFolderContentChangeSubscriptions();
					this.InitializeConnectionDroppedHandler();
					folderContentChangeNotificationHandler = new FolderContentChangeNotificationHandler(this.userContext, mailboxSession, contextFolderId, dataFolderId, queryResult, this.omnhLoggedUser.EmailPayload, listView, subscriptionProperties, propertyMap, sortBy, folderFilter, isConversationView);
					try
					{
						if (!folderContentChangeNotificationHandler.TrySubscribe(this.connectionDroppedNotificationHandler))
						{
							ExTraceGlobals.NotificationsCallTracer.TraceError((long)this.GetHashCode(), "Failed to create a folder content change subscription.");
							folderContentChangeNotificationHandler.Dispose();
							folderContentChangeNotificationHandler = null;
							return;
						}
						this.folderContentChangeNotificationHandlers[contextFolderId] = folderContentChangeNotificationHandler;
						folderContentChangeNotificationHandler = null;
						return;
					}
					finally
					{
						if (folderContentChangeNotificationHandler != null)
						{
							folderContentChangeNotificationHandler.Dispose();
							folderContentChangeNotificationHandler = null;
						}
					}
				}
				folderContentChangeNotificationHandler.MissedNotifications = false;
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00061C68 File Offset: 0x0005FE68
		public void UnsubscribeFolderContentChanges(OwaStoreObjectId folderId)
		{
			this.RemoveFolderContentChangeSubscription(folderId);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00061C74 File Offset: 0x0005FE74
		public bool HasDataFolderChanged(MailboxSession sessionIn, OwaStoreObjectId contextFolderId, OwaStoreObjectId dataFolderId)
		{
			FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(contextFolderId);
			return folderContentChangeNotificationHandler != null && folderContentChangeNotificationHandler.HasDataFolderChanged(dataFolderId);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00061C98 File Offset: 0x0005FE98
		public FolderContentChangeNotificationHandler GetFolderContentChangeNotificationHandler(OwaStoreObjectId contextFolderId)
		{
			if (contextFolderId == null)
			{
				throw new ArgumentNullException("contextFolderId");
			}
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			FolderContentChangeNotificationHandler result = null;
			this.folderContentChangeNotificationHandlers.TryGetValue(contextFolderId, out result);
			return result;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00061CE0 File Offset: 0x0005FEE0
		private void ClearOldFolderContentChangeSubscriptions()
		{
			if (this.folderContentChangeNotificationHandlers.Count >= 2)
			{
				OwaStoreObjectId owaStoreObjectId = null;
				foreach (OwaStoreObjectId owaStoreObjectId2 in this.folderContentChangeNotificationHandlers.Keys)
				{
					if (owaStoreObjectId == null)
					{
						owaStoreObjectId = owaStoreObjectId2;
					}
					else if (this.folderContentChangeNotificationHandlers[owaStoreObjectId2].CreationTime < this.folderContentChangeNotificationHandlers[owaStoreObjectId].CreationTime)
					{
						owaStoreObjectId = owaStoreObjectId2;
					}
				}
				this.UnsubscribeFolderContentChanges(owaStoreObjectId);
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00061D7C File Offset: 0x0005FF7C
		private void RemoveFolderContentChangeSubscription(OwaStoreObjectId folderId)
		{
			FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(folderId);
			if (folderContentChangeNotificationHandler != null)
			{
				folderContentChangeNotificationHandler.RemoveSubscription(this.connectionDroppedNotificationHandler);
				folderContentChangeNotificationHandler.Dispose();
				this.folderContentChangeNotificationHandlers.Remove(folderId);
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00061DB8 File Offset: 0x0005FFB8
		private void RemoveAllFolderContentChangeSubscriptions()
		{
			foreach (OwaStoreObjectId contextFolderId in this.folderContentChangeNotificationHandlers.Keys)
			{
				FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(contextFolderId);
				if (folderContentChangeNotificationHandler != null)
				{
					folderContentChangeNotificationHandler.RemoveSubscription(this.connectionDroppedNotificationHandler);
					folderContentChangeNotificationHandler.Dispose();
				}
			}
			this.folderContentChangeNotificationHandlers.Clear();
			this.folderContentChangeNotificationHandlers = null;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00061E3C File Offset: 0x0006003C
		public QueryResult GetFolderQueryResult(OwaStoreObjectId folderId)
		{
			FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(folderId);
			if (folderContentChangeNotificationHandler != null)
			{
				return folderContentChangeNotificationHandler.QueryResult;
			}
			return null;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00061E5C File Offset: 0x0006005C
		public SortBy[] GetFolderSortBy(OwaStoreObjectId folderId)
		{
			FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(folderId);
			if (folderContentChangeNotificationHandler != null)
			{
				return folderContentChangeNotificationHandler.SortBy;
			}
			return null;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00061E7C File Offset: 0x0006007C
		public FolderVirtualListViewFilter GetFolderFilter(OwaStoreObjectId folderId)
		{
			FolderContentChangeNotificationHandler folderContentChangeNotificationHandler = this.GetFolderContentChangeNotificationHandler(folderId);
			if (folderContentChangeNotificationHandler != null)
			{
				return folderContentChangeNotificationHandler.FolderFilter;
			}
			return null;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00061E9C File Offset: 0x0006009C
		public void SubscribeForFolderCounts(OwaStoreObjectId delegateFolderId, MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (sessionIn == mailboxSession)
			{
				if (this.omnhLoggedUser == null)
				{
					this.omnhLoggedUser = new OwaMapiNotificationHandler(this.userContext, mailboxSession, null);
					this.WireConnectionDroppedHandler(this.omnhLoggedUser);
				}
				this.omnhLoggedUser.SubscribeForFolderCounts();
				return;
			}
			if (Utilities.IsArchiveMailbox(sessionIn))
			{
				if (this.omnhArchives == null)
				{
					this.omnhArchives = new List<OwaMapiNotificationHandler>();
				}
				OwaMapiNotificationHandler owaMapiNotificationHandler = null;
				foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhArchives)
				{
					if (owaMapiNotificationHandler2.ArchiveMailboxSession == sessionIn)
					{
						owaMapiNotificationHandler = owaMapiNotificationHandler2;
						break;
					}
				}
				if (owaMapiNotificationHandler == null)
				{
					owaMapiNotificationHandler = new OwaMapiNotificationHandler(this.userContext, sessionIn, null);
					this.WireConnectionDroppedHandler(owaMapiNotificationHandler);
					this.omnhArchives.Add(owaMapiNotificationHandler);
				}
				owaMapiNotificationHandler.SubscribeForFolderCounts();
				return;
			}
			if (this.omnhDelegates == null)
			{
				this.omnhDelegates = new List<OwaMapiNotificationHandler>();
			}
			OwaMapiNotificationHandler owaMapiNotificationHandler3 = null;
			foreach (OwaMapiNotificationHandler owaMapiNotificationHandler4 in this.omnhDelegates)
			{
				if (owaMapiNotificationHandler4.DelegateSessionHandle.Session == sessionIn)
				{
					owaMapiNotificationHandler3 = owaMapiNotificationHandler4;
					break;
				}
			}
			if (owaMapiNotificationHandler3 == null)
			{
				OwaStoreObjectIdSessionHandle delegateSessionHandle = new OwaStoreObjectIdSessionHandle(delegateFolderId, this.userContext);
				owaMapiNotificationHandler3 = new OwaMapiNotificationHandler(this.userContext, sessionIn, delegateSessionHandle);
				this.WireConnectionDroppedHandler(owaMapiNotificationHandler3);
				this.omnhDelegates.Add(owaMapiNotificationHandler3);
				this.ReleaseOldestSessionIfNecessary();
			}
			owaMapiNotificationHandler3.SubscribeForFolderCounts();
			owaMapiNotificationHandler3.AddFolderCountsNotification(delegateFolderId);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00062048 File Offset: 0x00060248
		private void ReleaseOldestSessionIfNecessary()
		{
			if (this.omnhDelegates.Count <= 5)
			{
				return;
			}
			OwaMapiNotificationHandler owaMapiNotificationHandler = this.omnhDelegates[0];
			this.omnhDelegates.Remove(owaMapiNotificationHandler);
			owaMapiNotificationHandler.Dispose();
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00062084 File Offset: 0x00060284
		public void UnsubscribeFolderCounts(OwaStoreObjectId delegateFolderId, MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (sessionIn == mailboxSession || Utilities.IsArchiveMailbox(sessionIn))
			{
				return;
			}
			if (this.omnhDelegates != null)
			{
				OwaMapiNotificationHandler owaMapiNotificationHandler = null;
				foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhDelegates)
				{
					if (owaMapiNotificationHandler2.DelegateSessionHandle.Session == sessionIn)
					{
						owaMapiNotificationHandler = owaMapiNotificationHandler2;
						break;
					}
				}
				if (owaMapiNotificationHandler != null)
				{
					owaMapiNotificationHandler.SubscribeForFolderCounts();
					owaMapiNotificationHandler.DeleteFolderCountsNotification(delegateFolderId);
				}
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00062130 File Offset: 0x00060330
		public void SubscribeForNewMail()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (mailboxSession.LogonType == LogonType.Delegated)
			{
				throw new OwaInvalidOperationException("Cannot call subscribe new mail for delegate logon type");
			}
			if (this.omnhLoggedUser == null)
			{
				this.omnhLoggedUser = new OwaMapiNotificationHandler(this.userContext, mailboxSession, null);
				this.WireConnectionDroppedHandler(this.omnhLoggedUser);
			}
			this.omnhLoggedUser.SubscribeForNewMail();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000621A8 File Offset: 0x000603A8
		public void UnsubscribeNewMail()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (mailboxSession.LogonType == LogonType.Delegated)
			{
				throw new OwaInvalidOperationException("Cannot call unsubscribe new mail for delegate logon type");
			}
			if (this.omnhLoggedUser != null)
			{
				this.omnhLoggedUser.UnsubscribeNewMail();
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00062200 File Offset: 0x00060400
		public void SubscribeForReminders()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (mailboxSession.LogonType == LogonType.Delegated)
			{
				throw new OwaInvalidOperationException("Cannot call subscribe reminders for delegate logon type");
			}
			if (this.omnhLoggedUser == null)
			{
				this.omnhLoggedUser = new OwaMapiNotificationHandler(this.userContext, mailboxSession, null);
				this.WireConnectionDroppedHandler(this.omnhLoggedUser);
			}
			this.omnhLoggedUser.SubscribeForReminderChanges();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00062278 File Offset: 0x00060478
		public void SubscribeForSubscriptionChanges()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (mailboxSession.LogonType == LogonType.Owner)
			{
				if (this.subscriptionNotificationHandler == null)
				{
					this.subscriptionNotificationHandler = new SubscriptionNotificationHandler(this.userContext, mailboxSession);
					this.WireConnectionDroppedHandler(this.subscriptionNotificationHandler);
				}
				this.subscriptionNotificationHandler.Subscribe();
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000622E4 File Offset: 0x000604E4
		public object[][] GetReminderRows(ComparisonFilter filter, int maxRows)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			this.SubscribeForReminders();
			object[][] result = null;
			try
			{
				result = this.omnhLoggedUser.GetReminderRows(filter, maxRows);
			}
			catch (MapiExceptionObjectDisposed)
			{
				this.omnhLoggedUser.HandleConnectionDroppedNotification(null);
			}
			return result;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00062350 File Offset: 0x00060550
		public void InitSearchNotifications(MailboxSession sessionIn, StoreObjectId searchFolderId, SearchFolder searchFolder, SearchFolderCriteria searchCriteria, string searchString)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler == null)
			{
				throw new OwaInvalidOperationException("Cannot find the mapi notification handler for this session");
			}
			owaMapiHandler.SubscribeForSearchPageNotify(searchFolderId, searchFolder, searchCriteria, searchString);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0006239C File Offset: 0x0006059C
		public void CancelSearchNotifications(MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler == null)
			{
				throw new OwaInvalidOperationException("Cannot find the mapi notification handler for this session");
			}
			owaMapiHandler.CancelSearchPageNotify();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000623E0 File Offset: 0x000605E0
		public void AddSearchFolderDeleteList(MailboxSession sessionIn, StoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler == null)
			{
				throw new OwaInvalidOperationException("Cannot find the mapi notification handler for this session");
			}
			owaMapiHandler.AddSearchFolderDeleteList(folderId);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00062424 File Offset: 0x00060624
		public bool IsSearchInProgress(MailboxSession sessionIn, StoreObjectId folderId)
		{
			bool result = false;
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler != null)
			{
				result = owaMapiHandler.IsSearchInProgress(folderId);
			}
			return result;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00062464 File Offset: 0x00060664
		public bool HasCurrentSearchCompleted(MailboxSession sessionIn, StoreObjectId folderId, out bool wasFailNonContentIndexedSearchFlagSet)
		{
			bool result = false;
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			wasFailNonContentIndexedSearchFlagSet = false;
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler != null)
			{
				result = owaMapiHandler.HasCurrentSearchCompleted(folderId, out wasFailNonContentIndexedSearchFlagSet);
			}
			return result;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000624A8 File Offset: 0x000606A8
		public SearchPerformanceData GetSearchPerformanceData(MailboxSession sessionIn)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			OwaMapiNotificationHandler owaMapiHandler = this.GetOwaMapiHandler(sessionIn);
			if (owaMapiHandler != null)
			{
				return owaMapiHandler.SearchPerformanceData;
			}
			return null;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000624E0 File Offset: 0x000606E0
		internal static bool IsNotificationEnabled(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return userContext.IsFeatureEnabled(Feature.Notifications) && !userContext.IsWebPartRequest;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0006250C File Offset: 0x0006070C
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaMapiNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing)
			{
				if (this.subscriptionNotificationHandler != null)
				{
					this.subscriptionNotificationHandler.Dispose();
					this.subscriptionNotificationHandler = null;
				}
				if (this.omnhLoggedUser != null)
				{
					this.omnhLoggedUser.Dispose();
					this.omnhLoggedUser = null;
				}
				if (this.omnhArchives != null)
				{
					foreach (OwaMapiNotificationHandler owaMapiNotificationHandler in this.omnhArchives)
					{
						owaMapiNotificationHandler.Dispose();
					}
					this.omnhArchives.Clear();
					this.omnhArchives = null;
				}
				if (this.omnhDelegates != null)
				{
					foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhDelegates)
					{
						owaMapiNotificationHandler2.Dispose();
					}
					this.omnhDelegates.Clear();
					this.omnhDelegates = null;
				}
				this.RemoveAllFolderContentChangeSubscriptions();
				if (this.connectionDroppedNotificationHandler != null)
				{
					this.connectionDroppedNotificationHandler.Dispose();
					this.connectionDroppedNotificationHandler = null;
				}
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00062644 File Offset: 0x00060844
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaMapiNotificationManager>(this);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0006264C File Offset: 0x0006084C
		private void WireConnectionDroppedHandler(OwaMapiNotificationHandler mapiNotificationHandler)
		{
			this.InitializeConnectionDroppedHandler();
			this.connectionDroppedNotificationHandler.OnConnectionDropped += mapiNotificationHandler.HandleConnectionDroppedNotification;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0006266B File Offset: 0x0006086B
		private void WireConnectionDroppedHandler(NotificationHandlerBase handler)
		{
			this.InitializeConnectionDroppedHandler();
			this.connectionDroppedNotificationHandler.OnConnectionDropped += handler.HandleConnectionDroppedNotification;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0006268B File Offset: 0x0006088B
		private void InitializeConnectionDroppedHandler()
		{
			if (this.connectionDroppedNotificationHandler == null)
			{
				this.connectionDroppedNotificationHandler = new ConnectionDroppedNotificationHandler(this.userContext, this.userContext.MailboxSession);
				this.connectionDroppedNotificationHandler.Subscribe();
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000626BC File Offset: 0x000608BC
		internal void HandleConnectionDroppedNotification()
		{
			if (this.connectionDroppedNotificationHandler != null)
			{
				this.connectionDroppedNotificationHandler.HandleNotification(null);
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000626D4 File Offset: 0x000608D4
		private OwaMapiNotificationHandler GetOwaMapiHandler(MailboxSession sessionIn)
		{
			OwaMapiNotificationHandler result = null;
			MailboxSession mailboxSession = this.userContext.MailboxSession;
			if (sessionIn == mailboxSession)
			{
				if (this.omnhLoggedUser == null)
				{
					this.omnhLoggedUser = new OwaMapiNotificationHandler(this.userContext, sessionIn, null);
					this.WireConnectionDroppedHandler(this.omnhLoggedUser);
				}
				result = this.omnhLoggedUser;
			}
			else if (Utilities.IsArchiveMailbox(sessionIn))
			{
				if (this.omnhArchives == null)
				{
					this.omnhArchives = new List<OwaMapiNotificationHandler>();
				}
				OwaMapiNotificationHandler owaMapiNotificationHandler = null;
				foreach (OwaMapiNotificationHandler owaMapiNotificationHandler2 in this.omnhArchives)
				{
					if (owaMapiNotificationHandler2.ArchiveMailboxSession == sessionIn)
					{
						owaMapiNotificationHandler = owaMapiNotificationHandler2;
						break;
					}
				}
				if (owaMapiNotificationHandler == null)
				{
					owaMapiNotificationHandler = new OwaMapiNotificationHandler(this.userContext, sessionIn, null);
					this.WireConnectionDroppedHandler(owaMapiNotificationHandler);
					this.omnhArchives.Add(owaMapiNotificationHandler);
				}
				result = owaMapiNotificationHandler;
			}
			return result;
		}

		// Token: 0x04000A8C RID: 2700
		private const int MAXFOLDERCOUNTCHANGESUBSCRIPTIONS = 2;

		// Token: 0x04000A8D RID: 2701
		private UserContext userContext;

		// Token: 0x04000A8E RID: 2702
		private OwaMapiNotificationHandler omnhLoggedUser;

		// Token: 0x04000A8F RID: 2703
		private SubscriptionNotificationHandler subscriptionNotificationHandler;

		// Token: 0x04000A90 RID: 2704
		private Dictionary<OwaStoreObjectId, FolderContentChangeNotificationHandler> folderContentChangeNotificationHandlers = new Dictionary<OwaStoreObjectId, FolderContentChangeNotificationHandler>();

		// Token: 0x04000A91 RID: 2705
		private ConnectionDroppedNotificationHandler connectionDroppedNotificationHandler;

		// Token: 0x04000A92 RID: 2706
		private List<OwaMapiNotificationHandler> omnhDelegates;

		// Token: 0x04000A93 RID: 2707
		private List<OwaMapiNotificationHandler> omnhArchives;
	}
}
