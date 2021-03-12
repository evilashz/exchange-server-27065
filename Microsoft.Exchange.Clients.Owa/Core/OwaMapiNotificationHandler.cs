using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E5 RID: 485
	internal sealed class OwaMapiNotificationHandler : DisposeTrackableBase
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0005F934 File Offset: 0x0005DB34
		internal EmailPayload EmailPayload
		{
			get
			{
				return this.emailPayload;
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0005F93C File Offset: 0x0005DB3C
		public static void ProcessReminders(UserContext userContext, TextWriter writer)
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime(userContext);
			int num = (int)localTime.Bias.TotalMinutes;
			if (num != userContext.RemindersTimeZoneOffset)
			{
				userContext.RemindersTimeZoneOffset = num;
			}
			writer.Write("rmNotfy(");
			writer.Write(num);
			writer.Write(", 1, \"");
			Utilities.JavascriptEncode(DateTimeUtilities.GetJavascriptDate(localTime), writer);
			writer.Write("\", \"");
			bool reminderItems = RemindersRenderingUtilities.GetReminderItems(userContext, localTime, writer);
			if (reminderItems)
			{
				writer.Write("\", \"");
				Utilities.JavascriptEncode(LocalizedStrings.GetHtmlEncoded(-1707229168), writer);
			}
			writer.Write("\");");
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0005F9D8 File Offset: 0x0005DBD8
		internal static void DisposeXSOObjects(object o)
		{
			IDisposable disposable = o as IDisposable;
			if (o != null)
			{
				try
				{
					disposable.Dispose();
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose object.  exception {0}", ex.Message);
				}
				catch (StorageTransientException ex2)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose object.  exception {0}", ex2.Message);
				}
				catch (MapiExceptionObjectDisposed mapiExceptionObjectDisposed)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose object.  exception {0}", mapiExceptionObjectDisposed.Message);
				}
				catch (ThreadAbortException ex3)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose object.  exception {0}", ex3.Message);
				}
				catch (ResourceUnhealthyException ex4)
				{
					ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to dispose object.  exception {0}", ex4.Message);
				}
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0005FAC4 File Offset: 0x0005DCC4
		internal OwaMapiNotificationHandler(UserContext userContext, MailboxSession mailboxSession, OwaStoreObjectIdSessionHandle delegateSessionHandle)
		{
			this.mailboxSession = mailboxSession;
			this.userContext = userContext;
			this.delegateSessionHandle = delegateSessionHandle;
			this.owaStoreObjectIdType = OwaStoreObjectId.GetOwaStoreObjectIdType(userContext, mailboxSession, out this.mailboxOwnerLegacyDN);
			StoreObjectId remindersSearchFolderId = this.userContext.RemindersSearchFolderId;
			this.emailPayload = new EmailPayload(userContext, mailboxSession, this);
			this.emailPayload.RegisterWithPendingRequestNotifier();
			this.searchPayload = new SearchPayload(userContext, mailboxSession, this);
			this.searchPayload.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0005FB6B File Offset: 0x0005DD6B
		public OwaStoreObjectIdSessionHandle DelegateSessionHandle
		{
			get
			{
				return this.delegateSessionHandle;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0005FB73 File Offset: 0x0005DD73
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0005FB7B File Offset: 0x0005DD7B
		internal MailboxSession ArchiveMailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0005FB84 File Offset: 0x0005DD84
		public object[][] GetReminderRows(ComparisonFilter filter, int maxRows)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			object[][] rows;
			lock (this)
			{
				this.InitSubscriptions(ClientSubscriptionFlags.Reminders);
				this.queryResultReminder.SeekToCondition(SeekReference.OriginBeginning, filter);
				rows = this.queryResultReminder.GetRows(maxRows);
			}
			return rows;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0005FBF8 File Offset: 0x0005DDF8
		public void AddFolderChangeNotification(OwaStoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (!this.folderChangeList.Contains(folderId))
				{
					this.folderChangeList.Add(folderId);
				}
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0005FC60 File Offset: 0x0005DE60
		public void DeleteFolderChangeNotification(OwaStoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.folderChangeList.Contains(folderId))
				{
					this.folderChangeList.Remove(folderId);
				}
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0005FCC8 File Offset: 0x0005DEC8
		public void AddFolderCountsNotification(OwaStoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.folderCountsList == null)
				{
					this.folderCountsList = new List<OwaStoreObjectId>();
				}
				if (!this.folderCountsList.Contains(folderId))
				{
					this.folderCountsList.Add(folderId);
				}
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0005FD44 File Offset: 0x0005DF44
		public void DeleteFolderCountsNotification(OwaStoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.folderCountsList != null && this.folderCountsList.Contains(folderId))
				{
					this.folderCountsList.Remove(folderId);
				}
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
		public bool IsSearchInProgress(StoreObjectId folderId)
		{
			bool result = false;
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.searchFolderIdCurrent != null && folderId.Equals(this.searchFolderIdCurrent.StoreObjectId) && !this.isTopPageSearchComplete)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public bool HasCurrentSearchCompleted(StoreObjectId folderId, out bool wasFailNonContentIndexedSearchFlagSet)
		{
			bool result = false;
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.searchFolderIdCurrent == null && this.hasCurrentSearchCompleted)
				{
					result = true;
				}
				wasFailNonContentIndexedSearchFlagSet = this.wasFailNonContentIndexedSearchFlagSet;
			}
			return result;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0005FE98 File Offset: 0x0005E098
		public SearchPerformanceData SearchPerformanceData
		{
			get
			{
				return this.searchPerformanceData;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		public void AddSearchFolderDeleteList(StoreObjectId folderId)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				if (this.searchFolderDeleteList == null)
				{
					this.searchFolderDeleteList = new List<StoreObjectId>();
				}
				if (!this.searchFolderDeleteList.Contains(folderId))
				{
					this.searchFolderDeleteList.Add(folderId);
				}
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0005FF1C File Offset: 0x0005E11C
		public void SubscribeForFolderChanges()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.InitSubscriptions(ClientSubscriptionFlags.FolderChange);
				if (this.folderChangeList == null)
				{
					this.folderChangeList = new List<OwaStoreObjectId>();
				}
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0005FF84 File Offset: 0x0005E184
		public void SubscribeForFolderCounts()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.InitSubscriptions(ClientSubscriptionFlags.FolderCount);
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0005FFDC File Offset: 0x0005E1DC
		public void SubscribeForReminderChanges()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.InitSubscriptions(ClientSubscriptionFlags.Reminders);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00060034 File Offset: 0x0005E234
		public void SubscribeForNewMail()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.InitSubscriptions(ClientSubscriptionFlags.NewMail);
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0006008C File Offset: 0x0005E28C
		public void UnsubscribeNewMail()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.flags &= ~ClientSubscriptionFlags.NewMail;
				if (this.newmailSub != null)
				{
					OwaMapiNotificationHandler.DisposeXSOObjects(this.newmailSub);
				}
				this.newmailSub = null;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00060104 File Offset: 0x0005E304
		public void SubscribeForSearchPageNotify(StoreObjectId searchFolderId, SearchFolder searchFolder, SearchFolderCriteria searchCriteria, string searchString)
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.ResetSearchFolderReferences(true);
				this.InitSubscriptions(ClientSubscriptionFlags.StaticSearch);
				this.hasCurrentSearchCompleted = false;
				this.wasFailNonContentIndexedSearchFlagSet = searchCriteria.FailNonContentIndexedSearch;
				this.searchFolderIdCurrent = OwaStoreObjectId.CreateFromSessionFolderId(this.userContext, this.mailboxSession, searchFolderId);
				this.searchSub = Subscription.Create(this.mailboxSession, new NotificationHandler(this.HandleFullSearchComplete), NotificationType.SearchComplete, searchFolderId);
				searchFolder.ApplyOneTimeSearch(searchCriteria);
				this.searchPerformanceData = new SearchPerformanceData();
				this.searchPerformanceData.StartSearch(searchString);
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000601CC File Offset: 0x0005E3CC
		public void CancelSearchPageNotify()
		{
			if (!this.userContext.LockedByCurrentThread())
			{
				throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
			}
			lock (this)
			{
				this.ResetSearchFolderReferences(true);
				this.searchPerformanceData = null;
			}
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00060228 File Offset: 0x0005E428
		internal void HandleNewMailNotification(Notification notif)
		{
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.TotalMailboxNotifications.Increment();
			}
			NewMailNotification newMailNotification = notif as NewMailNotification;
			if (newMailNotification == null)
			{
				return;
			}
			bool flag = false;
			StringBuilder stringBuilder = null;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (newMailNotification.NewMailItemId == null || newMailNotification.ParentFolderId == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
				return;
			}
			lock (this)
			{
				if (this.isDisposed || this.missedNotifications || this.needReinitSubscriptions)
				{
					return;
				}
			}
			try
			{
				this.userContext.Lock();
				OwaMapiNotificationHandler.UpdateMailboxSessionBeforeAccessing(this.mailboxSession, this.userContext);
				lock (this)
				{
					if ((this.flags & ClientSubscriptionFlags.NewMail) != ClientSubscriptionFlags.NewMail)
					{
						return;
					}
					if (newMailNotification.ParentFolderId == null || !newMailNotification.ParentFolderId.Equals(this.userContext.InboxFolderId))
					{
						return;
					}
					stringBuilder = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
					{
						bool flag7 = false;
						if (ObjectClass.IsVoiceMessage(newMailNotification.MessageClass))
						{
							flag3 = true;
						}
						else if (ObjectClass.IsOfClass(newMailNotification.MessageClass, "IPM.Note.Microsoft.Fax.CA"))
						{
							flag4 = true;
						}
						else
						{
							flag2 = true;
						}
						if (flag4)
						{
							flag7 = true;
							if ((this.userContext.UserOptions.NewItemNotify & NewNotification.FaxToast) == NewNotification.FaxToast)
							{
								stringWriter.Write("shwNF(1);");
								stringWriter.Write("g_sFId=\"");
								Utilities.JavascriptEncode(newMailNotification.NewMailItemId.ToBase64String(), stringWriter);
								stringWriter.Write("\";");
								flag7 = this.BindItemAndShowDialog(newMailNotification.NewMailItemId, "lnkNwFx", stringWriter);
							}
							else
							{
								flag4 = false;
							}
						}
						if (flag3)
						{
							flag7 = true;
							if ((this.userContext.UserOptions.NewItemNotify & NewNotification.VoiceMailToast) == NewNotification.VoiceMailToast)
							{
								stringWriter.Write("shwNVM(1);");
								stringWriter.Write("g_sVMId=\"");
								Utilities.JavascriptEncode(newMailNotification.NewMailItemId.ToBase64String(), stringWriter);
								stringWriter.Write("\";");
								flag7 = this.BindItemAndShowDialog(newMailNotification.NewMailItemId, "lnkNwVMl", stringWriter);
							}
							else
							{
								flag3 = false;
							}
						}
						if (flag2)
						{
							flag7 = true;
							if ((this.userContext.UserOptions.NewItemNotify & NewNotification.EMailToast) == NewNotification.EMailToast)
							{
								stringWriter.Write("shwNM(1);");
								stringWriter.Write("g_sMId=\"");
								Utilities.JavascriptEncode(newMailNotification.NewMailItemId.ToBase64String(), stringWriter);
								stringWriter.Write("\";");
								flag7 = this.BindItemAndShowDialog(newMailNotification.NewMailItemId, "lnkNwMl", stringWriter);
							}
							else
							{
								flag2 = false;
							}
						}
						if ((this.userContext.UserOptions.NewItemNotify & NewNotification.Sound) == NewNotification.Sound && flag7)
						{
							stringWriter.Write("plySnd();");
						}
					}
				}
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSession(this.mailboxSession);
					this.userContext.Unlock();
				}
				if (flag4 || flag3 || flag2)
				{
					this.emailPayload.AddNewMailPayload(stringBuilder);
					this.emailPayload.PickupData();
				}
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out for notification thread. Exception: {0}", ex.Message);
				flag = true;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleNewMailNotification on the notification thread. Exception: {0}", ex2.Message);
				flag = true;
			}
			finally
			{
				if (flag)
				{
					lock (this)
					{
						this.missedNotifications = true;
					}
				}
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
					this.userContext.Unlock();
				}
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00060668 File Offset: 0x0005E868
		internal void HandleHierarchyNotification(Notification notif)
		{
			bool flag = false;
			try
			{
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.TotalMailboxNotifications.Increment();
				}
				QueryNotification queryNotification = notif as QueryNotification;
				if (queryNotification != null)
				{
					if (queryNotification.Row.Length >= this.querySubscriptionProperties.Length)
					{
						VersionedId versionedId = queryNotification.Row[0] as VersionedId;
						StoreObjectId storeObjectId = null;
						if (versionedId != null)
						{
							storeObjectId = versionedId.ObjectId;
						}
						if (storeObjectId == null || queryNotification.Row[2] == null || queryNotification.Row[3] == null)
						{
							ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification has a null notifying item id");
						}
						else
						{
							int num = (int)queryNotification.Row[2];
							int num2 = (int)queryNotification.Row[3];
							bool flag2 = false;
							bool flag3 = false;
							OwaStoreObjectId folderId = null;
							bool flag4 = false;
							lock (this)
							{
								if (this.isDisposed || this.missedNotifications || this.needReinitSubscriptions)
								{
									return;
								}
								if (!this.HandleSearchNotification(storeObjectId, num, ref flag4, ref folderId))
								{
									if (storeObjectId.Equals(this.userContext.RemindersSearchFolderId) || (this.searchFolderIdCurrent != null && storeObjectId.Equals(this.searchFolderIdCurrent.StoreObjectId)))
									{
										return;
									}
									if ((this.flags & ClientSubscriptionFlags.FolderCount) == ClientSubscriptionFlags.FolderCount && num != -1 && num2 != -1)
									{
										if (this.folderCountsList != null)
										{
											using (List<OwaStoreObjectId>.Enumerator enumerator = this.folderCountsList.GetEnumerator())
											{
												while (enumerator.MoveNext())
												{
													OwaStoreObjectId owaStoreObjectId = enumerator.Current;
													if (owaStoreObjectId.StoreObjectId != null && storeObjectId.Equals(owaStoreObjectId.StoreObjectId))
													{
														flag2 = true;
														break;
													}
												}
												goto IL_195;
											}
										}
										flag2 = true;
									}
									IL_195:
									if ((this.flags & ClientSubscriptionFlags.FolderChange) == ClientSubscriptionFlags.FolderChange && this.folderChangeList != null)
									{
										foreach (OwaStoreObjectId owaStoreObjectId2 in this.folderChangeList)
										{
											if (owaStoreObjectId2.StoreObjectId != null && storeObjectId.Equals(owaStoreObjectId2.StoreObjectId))
											{
												flag3 = true;
												break;
											}
										}
									}
									folderId = OwaStoreObjectId.CreateFromSessionFolderId(this.owaStoreObjectIdType, this.mailboxOwnerLegacyDN, storeObjectId);
								}
							}
							if (flag2)
							{
								this.emailPayload.AddFolderCountPayload(folderId, (long)num, (long)num2);
							}
							if (flag3)
							{
								this.emailPayload.AddFolderRefreshPayload(folderId, false);
							}
							if (flag4)
							{
								this.searchPayload.AddSearchFolderRefreshPayload(folderId, SearchNotificationType.FirstPage);
								this.searchPayload.PickupData();
							}
							this.emailPayload.PickupData();
						}
					}
				}
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out for notification thread. Exception: {0}", ex.Message);
				flag = true;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleHierarchyNotification on the notification thread. Exception: {0}", ex2.Message);
				flag = true;
			}
			finally
			{
				if (flag)
				{
					lock (this)
					{
						this.missedNotifications = true;
					}
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000609FC File Offset: 0x0005EBFC
		internal void HandleConnectionDroppedNotification(Notification notification)
		{
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.TotalMailboxNotifications.Increment();
			}
			lock (this)
			{
				if (!this.isDisposed)
				{
					this.needReinitSubscriptions = true;
				}
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00060A54 File Offset: 0x0005EC54
		internal bool HandleSearchNotification(StoreObjectId folderId, int itemCount, ref bool isFirstPageSearchNotification, ref OwaStoreObjectId owaFolderId)
		{
			bool result = false;
			if (this.searchFolderIdCurrent != null && folderId.Equals(this.searchFolderIdCurrent.StoreObjectId))
			{
				if (itemCount != -1 && itemCount >= 50 && !this.isTopPageSearchComplete)
				{
					if (this.searchPerformanceData != null)
					{
						this.searchPerformanceData.FirstPage(itemCount);
					}
					this.isTopPageSearchComplete = true;
					isFirstPageSearchNotification = true;
					owaFolderId = this.searchFolderIdCurrent;
				}
				result = true;
			}
			else if (this.searchFolderDeleteList != null && this.searchFolderDeleteList.Contains(folderId))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00060AD4 File Offset: 0x0005ECD4
		internal void HandleFullSearchComplete(Notification notification)
		{
			OwaStoreObjectId owaStoreObjectId = null;
			try
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[OwaMapiNotificationHandler.HandleFullSearchComplete] Processing search COMPLETE notification");
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.TotalMailboxNotifications.Increment();
				}
				lock (this)
				{
					if (this.isDisposed)
					{
						return;
					}
					if (this.searchFolderIdCurrent != null)
					{
						owaStoreObjectId = this.searchFolderIdCurrent;
					}
					this.hasCurrentSearchCompleted = true;
				}
				if (owaStoreObjectId != null)
				{
					this.searchPayload.AddSearchFolderRefreshPayload(owaStoreObjectId, SearchNotificationType.Complete);
				}
				if (this.searchPerformanceData != null)
				{
					this.searchPerformanceData.Complete(false, false);
				}
				this.searchPayload.PickupData();
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleFullSearchComplete on the notification thread. Exception: {0}", ex.Message);
			}
			finally
			{
				lock (this)
				{
					this.ResetSearchFolderReferences(false);
				}
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00060BF4 File Offset: 0x0005EDF4
		internal void HandlePendingGetTimerCallback(bool clearSearchFolderDeleteList)
		{
			string mailboxQuotaHtml = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				this.userContext.Lock();
				OwaMapiNotificationHandler.UpdateMailboxSessionBeforeAccessing(this.mailboxSession, this.userContext);
				lock (this)
				{
					if (this.isDisposed)
					{
						return;
					}
					flag3 = this.InitSubscriptions(ClientSubscriptionFlags.None);
					if (this.missedNotifications)
					{
						flag3 = true;
					}
					if (clearSearchFolderDeleteList)
					{
						this.ClearSearchFolderDeleteList();
					}
					if (this.searchFolderIdCurrent == null)
					{
						this.ResetSearchFolderReferences(true);
					}
					this.missedNotifications = false;
				}
				long num = Globals.ApplicationTime - this.userContext.LastQuotaUpdateTime;
				if ((this.userContext.IsQuotaAboveWarning && num >= 900000L) || num >= 1800000L)
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						RenderingUtilities.RenderMailboxQuota(stringWriter, this.userContext);
						mailboxQuotaHtml = stringWriter.ToString();
					}
					flag = true;
				}
				int num2 = (int)DateTimeUtilities.GetLocalTime(this.userContext).Bias.TotalMinutes;
				if (num2 != this.userContext.RemindersTimeZoneOffset)
				{
					this.userContext.RemindersTimeZoneOffset = num2;
					flag2 = true;
				}
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSession(this.mailboxSession);
					this.userContext.Unlock();
				}
				if (flag3)
				{
					this.emailPayload.AddRefreshPayload();
				}
				else
				{
					if (flag)
					{
						this.emailPayload.AddQuotaPayload(mailboxQuotaHtml);
					}
					if (flag2)
					{
						this.emailPayload.AddReminderNotifyPayload(num2);
					}
				}
				this.emailPayload.PickupData();
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out in the pending GET timer callback. Exception: {0}", ex.Message);
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in pending GET timer callback thread. Exception: {0}", ex2.Message);
			}
			finally
			{
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
					this.userContext.Unlock();
				}
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00060E60 File Offset: 0x0005F060
		internal void HandleReminderNotification(Notification notif)
		{
			QueryNotification queryNotification = notif as QueryNotification;
			StringBuilder stringBuilder = null;
			if (queryNotification == null || !this.FProcessEventType(queryNotification.EventType))
			{
				return;
			}
			bool flag = false;
			lock (this)
			{
				if (this.isDisposed || this.missedNotifications || this.needReinitSubscriptions)
				{
					return;
				}
			}
			try
			{
				this.userContext.Lock();
				Culture.InternalSetAsyncThreadCulture(this.userContext.UserCulture, this.userContext);
				OwaMapiNotificationHandler.UpdateMailboxSessionBeforeAccessing(this.mailboxSession, this.userContext);
				lock (this)
				{
					bool flag4 = (this.flags & ClientSubscriptionFlags.Reminders) == ClientSubscriptionFlags.Reminders;
					if (flag4)
					{
						stringBuilder = new StringBuilder();
						using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
						{
							OwaMapiNotificationHandler.ProcessReminders(this.userContext, stringWriter);
							stringWriter.Write("setRmPllInt(" + 28800000L + "); ");
						}
					}
				}
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSession(this.mailboxSession);
					this.userContext.Unlock();
				}
				if (stringBuilder != null)
				{
					this.emailPayload.AddRemindersPayload(stringBuilder);
				}
				this.emailPayload.PickupData();
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "User context lock timed out for notification thread. Exception: {0}", ex.Message);
				flag = true;
			}
			catch (Exception ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in HandleHierarchyNotification on the notification thread. Exception: {0}", ex2.Message);
				flag = true;
			}
			finally
			{
				if (flag)
				{
					lock (this)
					{
						this.missedNotifications = true;
					}
				}
				if (this.userContext.LockedByCurrentThread())
				{
					Utilities.DisconnectStoreSessionSafe(this.mailboxSession);
					this.userContext.Unlock();
				}
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000610F0 File Offset: 0x0005F2F0
		private static void UpdateMailboxSessionBeforeAccessing(MailboxSession mailboxSession, UserContext userContext)
		{
			Utilities.ReconnectStoreSession(mailboxSession, userContext);
			mailboxSession.AccountingObject = null;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00061100 File Offset: 0x0005F300
		private void InitHierarchyTableSubscription()
		{
			if (this.hierSub == null)
			{
				if (this.resultHierarchy != null)
				{
					OwaMapiNotificationHandler.DisposeXSOObjects(this.resultHierarchy);
				}
				this.resultHierarchy = null;
				using (Folder folder = Folder.Bind(this.mailboxSession, DefaultFolderType.Configuration))
				{
					this.resultHierarchy = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, this.querySubscriptionProperties);
					this.resultHierarchy.GetRows(this.resultHierarchy.EstimatedRowCount);
					this.hierSub = Subscription.Create(this.resultHierarchy, new NotificationHandler(this.HandleHierarchyNotification));
				}
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000611A4 File Offset: 0x0005F3A4
		private void InitNewMailSubscription()
		{
			if (this.newmailSub == null)
			{
				this.newmailSub = Subscription.CreateMailboxSubscription(this.mailboxSession, new NotificationHandler(this.HandleNewMailNotification), NotificationType.NewMail);
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000611CC File Offset: 0x0005F3CC
		private void InitReminderTableSubscription()
		{
			if (this.reminderSub == null)
			{
				using (SearchFolder searchFolder = SearchFolder.Bind(this.mailboxSession, DefaultFolderType.Reminders))
				{
					SortBy[] sortColumns = new SortBy[]
					{
						new SortBy(ItemSchema.ReminderIsSet, SortOrder.Descending),
						new SortBy(ItemSchema.ReminderNextTime, SortOrder.Descending)
					};
					this.queryResultReminder = searchFolder.ItemQuery(ItemQueryType.None, null, sortColumns, RemindersRenderingUtilities.QueryProperties);
					this.queryResultReminder.GetRows(1);
					this.reminderSub = Subscription.Create(this.queryResultReminder, new NotificationHandler(this.HandleReminderNotification));
				}
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00061270 File Offset: 0x0005F470
		private bool InitSubscriptions(ClientSubscriptionFlags flagsInit)
		{
			bool result = false;
			if (this.needReinitSubscriptions)
			{
				this.CleanupSubscriptions();
				result = true;
			}
			this.flags |= flagsInit;
			if ((this.flags & ClientSubscriptionFlags.FolderChange) == ClientSubscriptionFlags.FolderChange || (this.flags & ClientSubscriptionFlags.FolderCount) == ClientSubscriptionFlags.FolderCount)
			{
				this.InitHierarchyTableSubscription();
			}
			if ((this.flags & ClientSubscriptionFlags.NewMail) == ClientSubscriptionFlags.NewMail)
			{
				this.InitNewMailSubscription();
			}
			if ((this.flags & ClientSubscriptionFlags.Reminders) == ClientSubscriptionFlags.Reminders)
			{
				this.InitReminderTableSubscription();
			}
			if ((this.flags & ClientSubscriptionFlags.StaticSearch) == ClientSubscriptionFlags.StaticSearch)
			{
				this.InitHierarchyTableSubscription();
			}
			this.needReinitSubscriptions = false;
			return result;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000612F8 File Offset: 0x0005F4F8
		private bool FProcessEventType(QueryNotificationType type)
		{
			bool result = false;
			switch (type)
			{
			case QueryNotificationType.RowAdded:
			case QueryNotificationType.RowDeleted:
			case QueryNotificationType.RowModified:
			case QueryNotificationType.Reload:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0006133C File Offset: 0x0005F53C
		private void CleanupSubscriptions()
		{
			if (this.hierSub != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.hierSub);
			}
			this.hierSub = null;
			if (this.resultHierarchy != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.resultHierarchy);
			}
			this.resultHierarchy = null;
			if (this.newmailSub != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.newmailSub);
			}
			this.newmailSub = null;
			if (this.reminderSub != null)
			{
				OwaMapiNotificationHandler.DisposeXSOObjects(this.reminderSub);
				OwaMapiNotificationHandler.DisposeXSOObjects(this.queryResultReminder);
			}
			this.reminderSub = null;
			this.queryResultReminder = null;
			this.ResetSearchFolderReferences(true);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000613CC File Offset: 0x0005F5CC
		private void ClearSearchFolderDeleteList()
		{
			if (this.searchFolderDeleteList != null)
			{
				foreach (StoreObjectId storeObjectId in this.searchFolderDeleteList)
				{
					try
					{
						this.mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							storeObjectId
						});
					}
					catch (Exception ex)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in ClearSearchFolderDeleteList. Exception: {0}", ex.Message);
					}
				}
				this.searchFolderDeleteList.Clear();
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00061474 File Offset: 0x0005F674
		protected override void InternalDispose(bool isDisposing)
		{
			bool flag = false;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaMapiNotificationHandler.Dispose. IsDisposing: {0}", isDisposing);
			lock (this)
			{
				if (this.isDisposed)
				{
					return;
				}
				if (isDisposing)
				{
					this.isDisposed = true;
					flag = true;
				}
			}
			if (flag)
			{
				try
				{
					this.userContext.Lock();
					OwaMapiNotificationHandler.UpdateMailboxSessionBeforeAccessing(this.mailboxSession, this.userContext);
					lock (this)
					{
						this.flags = ClientSubscriptionFlags.None;
						if (this.folderChangeList != null)
						{
							this.folderChangeList.Clear();
						}
						this.folderChangeList = null;
						if (this.folderCountsList != null)
						{
							this.folderCountsList.Clear();
						}
						this.folderCountsList = null;
						this.ClearSearchFolderDeleteList();
						this.searchFolderDeleteList = null;
						this.CleanupSubscriptions();
						if (this.delegateSessionHandle != null)
						{
							OwaMapiNotificationHandler.DisposeXSOObjects(this.delegateSessionHandle);
						}
						this.delegateSessionHandle = null;
					}
				}
				finally
				{
					if (this.userContext.LockedByCurrentThread())
					{
						this.userContext.Unlock();
					}
				}
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000615B4 File Offset: 0x0005F7B4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaMapiNotificationHandler>(this);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000615BC File Offset: 0x0005F7BC
		private void ResetSearchFolderReferences(bool cleanupOldSubscription)
		{
			if (cleanupOldSubscription && this.searchSub != null)
			{
				if (!this.userContext.LockedByCurrentThread())
				{
					throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
				}
				OwaMapiNotificationHandler.DisposeXSOObjects(this.searchSub);
				this.searchSub = null;
			}
			this.searchFolderIdCurrent = null;
			this.isTopPageSearchComplete = false;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0006160C File Offset: 0x0005F80C
		private bool BindItemAndShowDialog(StoreObjectId itemId, string type, StringWriter writer)
		{
			MessageItem messageItem = null;
			bool result;
			try
			{
				messageItem = Item.BindAsMessage(this.mailboxSession, itemId);
				if (messageItem != null)
				{
					string text = ItemUtility.GetProperty<string>(messageItem, StoreObjectSchema.ItemClass, null);
					if (text == null)
					{
						text = "IPM.Note";
					}
					string property = ItemUtility.GetProperty<string>(messageItem, StoreObjectSchema.ContentClass, string.Empty);
					if (ObjectClass.IsOfClass(property, "rpmsg.message"))
					{
						text += ".irm";
					}
					writer.Write("shwNwItmDlg(\"");
					if (messageItem.From != null && messageItem.From.DisplayName != null)
					{
						Utilities.JavascriptEncode(Utilities.HtmlEncode(messageItem.From.DisplayName), writer);
					}
					writer.Write("\",\"");
					if (messageItem.Subject != null)
					{
						Utilities.JavascriptEncode(Utilities.HtmlEncode(messageItem.Subject), writer);
					}
					writer.Write("\",\"" + type + "\",\"");
					using (StringWriter stringWriter = new StringWriter())
					{
						SmallIconManager.RenderItemIcon(stringWriter, this.userContext, text, false, "nwItmImg", new string[0]);
						Utilities.JavascriptEncode(stringWriter.ToString(), writer);
					}
					writer.Write("\");");
				}
				result = true;
			}
			catch (ObjectNotFoundException)
			{
				if (type != null)
				{
					if (!(type == "lnkNwMl"))
					{
						if (!(type == "lnkNwVMl"))
						{
							if (type == "lnkNwFx")
							{
								writer.Write("shwNF(0);");
							}
						}
						else
						{
							writer.Write("shwNVM(0);");
						}
					}
					else
					{
						writer.Write("shwNM(0);");
					}
				}
				result = false;
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
				}
			}
			return result;
		}

		// Token: 0x04000A69 RID: 2665
		public const int BelowWarningQuotaUpdateInterval = 1800000;

		// Token: 0x04000A6A RID: 2666
		public const int AboveWarningQuotaUpdateInterval = 900000;

		// Token: 0x04000A6B RID: 2667
		public const long NotificationIntervalForPush = 28800L;

		// Token: 0x04000A6C RID: 2668
		private const int InitialSearchPageMax = 50;

		// Token: 0x04000A6D RID: 2669
		private const string IrmItemClass = ".irm";

		// Token: 0x04000A6E RID: 2670
		private MailboxSession mailboxSession;

		// Token: 0x04000A6F RID: 2671
		private UserContext userContext;

		// Token: 0x04000A70 RID: 2672
		private OwaStoreObjectIdSessionHandle delegateSessionHandle;

		// Token: 0x04000A71 RID: 2673
		private Subscription hierSub;

		// Token: 0x04000A72 RID: 2674
		private QueryResult resultHierarchy;

		// Token: 0x04000A73 RID: 2675
		private Subscription newmailSub;

		// Token: 0x04000A74 RID: 2676
		private ClientSubscriptionFlags flags;

		// Token: 0x04000A75 RID: 2677
		private EmailPayload emailPayload;

		// Token: 0x04000A76 RID: 2678
		private SearchPayload searchPayload;

		// Token: 0x04000A77 RID: 2679
		private List<OwaStoreObjectId> folderChangeList;

		// Token: 0x04000A78 RID: 2680
		private List<OwaStoreObjectId> folderCountsList;

		// Token: 0x04000A79 RID: 2681
		private bool isDisposed;

		// Token: 0x04000A7A RID: 2682
		private bool missedNotifications;

		// Token: 0x04000A7B RID: 2683
		private bool needReinitSubscriptions;

		// Token: 0x04000A7C RID: 2684
		private Subscription searchSub;

		// Token: 0x04000A7D RID: 2685
		private OwaStoreObjectId searchFolderIdCurrent;

		// Token: 0x04000A7E RID: 2686
		private bool isTopPageSearchComplete;

		// Token: 0x04000A7F RID: 2687
		private bool hasCurrentSearchCompleted;

		// Token: 0x04000A80 RID: 2688
		private bool wasFailNonContentIndexedSearchFlagSet;

		// Token: 0x04000A81 RID: 2689
		private List<StoreObjectId> searchFolderDeleteList;

		// Token: 0x04000A82 RID: 2690
		private QueryResult queryResultReminder;

		// Token: 0x04000A83 RID: 2691
		private Subscription reminderSub;

		// Token: 0x04000A84 RID: 2692
		private string mailboxOwnerLegacyDN;

		// Token: 0x04000A85 RID: 2693
		private OwaStoreObjectIdType owaStoreObjectIdType;

		// Token: 0x04000A86 RID: 2694
		private SearchPerformanceData searchPerformanceData;

		// Token: 0x04000A87 RID: 2695
		private PropertyDefinition[] querySubscriptionProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName,
			FolderSchema.ItemCount,
			FolderSchema.UnreadCount
		};
	}
}
