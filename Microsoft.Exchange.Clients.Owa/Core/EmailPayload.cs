using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000106 RID: 262
	internal sealed class EmailPayload : IPendingRequestNotifier
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0003F3AC File Offset: 0x0003D5AC
		internal EmailPayload(UserContext userContext, MailboxSession mailboxSession, OwaMapiNotificationHandler omnhParent)
		{
			this.folderCountTable = new Dictionary<OwaStoreObjectId, ItemCountPair>();
			this.folderRefreshList = new List<OwaStoreObjectId>();
			this.payloadStringReminderChanges = new StringBuilder(256);
			this.payloadStringNewMail = new StringBuilder(256);
			this.payloadStringRefreshAll = new StringBuilder(256);
			this.payloadStringQuota = new StringBuilder(256);
			this.payloadStringReminderNotify = new StringBuilder(256);
			this.folderContentChangeNotifications = new Dictionary<OwaStoreObjectId, EmailPayload.FCNHState>();
			this.userContext = userContext;
			this.mailboxSessionDisplayName = string.Copy(mailboxSession.DisplayName);
			this.omnhParent = omnhParent;
			this.connectionAliveTimerCount = 1;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600089B RID: 2203 RVA: 0x0003F458 File Offset: 0x0003D658
		// (remove) Token: 0x0600089C RID: 2204 RVA: 0x0003F490 File Offset: 0x0003D690
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0003F4C5 File Offset: 0x0003D6C5
		public bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0003F4C8 File Offset: 0x0003D6C8
		public string ReadDataAndResetState()
		{
			string result = null;
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "EmailPayload.ReadDataAndResetState. Mailbox: {0}", this.mailboxSessionDisplayName);
			lock (this)
			{
				this.containsDataToPickup = false;
				if (this.payloadStringRefreshAll.Length > 0)
				{
					result = this.payloadStringRefreshAll.ToString();
				}
				else
				{
					StringBuilder stringBuilder = null;
					StringBuilder stringBuilder2 = null;
					if (this.folderCountTable.Count > 0)
					{
						stringBuilder = new StringBuilder();
						using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
						{
							foreach (KeyValuePair<OwaStoreObjectId, ItemCountPair> keyValuePair in this.folderCountTable)
							{
								OwaStoreObjectId key = keyValuePair.Key;
								ItemCountPair value = keyValuePair.Value;
								stringWriter.Write("updMapiCnt(\"");
								Utilities.JavascriptEncode(key.ToBase64String(), stringWriter);
								stringWriter.Write(string.Concat(new object[]
								{
									"\",",
									value.ItemCount,
									",",
									value.UnreadItemCount,
									");"
								}));
							}
						}
					}
					stringBuilder2 = new StringBuilder();
					if (this.folderRefreshList.Count > 0)
					{
						using (StringWriter stringWriter2 = new StringWriter(stringBuilder2, CultureInfo.InvariantCulture))
						{
							foreach (OwaStoreObjectId owaStoreObjectId in this.folderRefreshList)
							{
								if (!this.folderContentChangeNotifications.ContainsKey(owaStoreObjectId) || this.folderContentChangeNotifications[owaStoreObjectId].NotificationHandler == null || this.folderContentChangeNotifications[owaStoreObjectId].NotificationHandler.AllowFolderRefreshNotification)
								{
									stringWriter2.Write("stMapiDrty(\"");
									Utilities.JavascriptEncode(owaStoreObjectId.ToBase64String(), stringWriter2);
									stringWriter2.Write("\");");
								}
							}
						}
					}
					int num = 0;
					if (this.folderContentChangeNotifications.Count > 0)
					{
						foreach (EmailPayload.FCNHState fcnhstate in this.folderContentChangeNotifications.Values)
						{
							num += fcnhstate.Queue.Count;
						}
					}
					StringBuilder stringBuilder3 = null;
					bool flag2 = true;
					if (num > 0)
					{
						stringBuilder3 = new StringBuilder(1280 * num);
						using (StringWriter stringWriter3 = new StringWriter(stringBuilder3, CultureInfo.InvariantCulture))
						{
							foreach (KeyValuePair<OwaStoreObjectId, EmailPayload.FCNHState> keyValuePair2 in this.folderContentChangeNotifications)
							{
								OwaStoreObjectId key2 = keyValuePair2.Key;
								Queue<QueryNotification> queue = keyValuePair2.Value.Queue;
								FolderContentChangeNotificationHandler notificationHandler = keyValuePair2.Value.NotificationHandler;
								foreach (QueryNotification notification in queue)
								{
									notificationHandler.ProcessNotification(stringWriter3, notification, out flag2);
								}
							}
						}
					}
					result = string.Concat(new string[]
					{
						(stringBuilder != null) ? stringBuilder.ToString() : string.Empty,
						(stringBuilder2 != null) ? stringBuilder2.ToString() : string.Empty,
						this.payloadStringReminderChanges.ToString(),
						this.payloadStringNewMail.ToString(),
						this.payloadStringQuota.ToString(),
						this.payloadStringReminderNotify.ToString(),
						(stringBuilder3 != null) ? stringBuilder3.ToString() : string.Empty
					});
				}
				this.Clear(true);
			}
			return result;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003F96C File Offset: 0x0003DB6C
		public void PickupData()
		{
			bool flag = false;
			lock (this)
			{
				flag = ((this.folderCountTable.Count > 0 || this.folderRefreshList.Count > 0 || this.payloadStringNewMail.Length > 0 || this.payloadStringQuota.Length > 0 || this.payloadStringReminderChanges.Length > 0 || this.payloadStringReminderNotify.Length > 0 || this.payloadStringRefreshAll.Length > 0 || this.AreThereFolderContentChangeNotifications()) && !this.containsDataToPickup);
				if (flag)
				{
					this.containsDataToPickup = true;
				}
			}
			if (flag)
			{
				this.DataAvailable(this, new EventArgs());
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "EmailPayload.PickupData. DataAvailable method called. Mailbox: {0}", this.mailboxSessionDisplayName);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "EmailPayload.PickupData. No need to call DataAvailable method. Mailbox: {0}", this.mailboxSessionDisplayName);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003FA74 File Offset: 0x0003DC74
		public void Clear(bool clearRefreshPayload)
		{
			this.folderCountTable.Clear();
			this.folderRefreshList.Clear();
			this.payloadStringReminderChanges.Remove(0, this.payloadStringReminderChanges.Length);
			this.payloadStringNewMail.Remove(0, this.payloadStringNewMail.Length);
			this.payloadStringQuota.Remove(0, this.payloadStringQuota.Length);
			this.payloadStringReminderNotify.Remove(0, this.payloadStringReminderNotify.Length);
			this.ClearAllFolderContentChangeNotifications();
			if (clearRefreshPayload)
			{
				this.payloadStringRefreshAll.Remove(0, this.payloadStringRefreshAll.Length);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0003FB18 File Offset: 0x0003DD18
		public void ConnectionAliveTimer()
		{
			int num = Interlocked.Increment(ref this.connectionAliveTimerCount);
			bool clearSearchFolderDeleteList = false;
			if (num % 5 == 0)
			{
				if (num % 15 == 0)
				{
					clearSearchFolderDeleteList = true;
				}
				this.omnhParent.HandlePendingGetTimerCallback(clearSearchFolderDeleteList);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003FB4C File Offset: 0x0003DD4C
		public void AddFolderRefreshPayload(OwaStoreObjectId folderId)
		{
			this.AddFolderRefreshPayload(folderId, true);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003FB58 File Offset: 0x0003DD58
		public void AddFolderRefreshPayload(OwaStoreObjectId folderId, bool forceRefresh)
		{
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					if (!this.folderRefreshList.Contains(folderId) && (forceRefresh || !this.folderContentChangeNotifications.ContainsKey(folderId) || this.folderContentChangeNotifications[folderId].NotificationHandler == null || this.folderContentChangeNotifications[folderId].NotificationHandler.AllowFolderRefreshNotification))
					{
						this.folderRefreshList.Add(folderId);
						this.ClearFolderContentChangePayload(folderId);
					}
					if (this.folderRefreshList.Count > 20)
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_MessagingPayloadNotBeingPickedup, string.Empty, new object[]
						{
							this.mailboxSessionDisplayName
						});
						this.AddRefreshPayload();
					}
				}
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0003FC34 File Offset: 0x0003DE34
		public void AttachFolderContentChangeNotificationHandler(OwaStoreObjectId folderId, FolderContentChangeNotificationHandler notificationHandler)
		{
			lock (this)
			{
				if (this.folderContentChangeNotifications.ContainsKey(folderId))
				{
					throw new OwaInvalidOperationException("There is already an active notification handler for this folder. That should not be the case");
				}
				this.folderContentChangeNotifications[folderId] = new EmailPayload.FCNHState(notificationHandler);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0003FC94 File Offset: 0x0003DE94
		public void DetachFolderContentChangeNotificationHandler(OwaStoreObjectId folderId)
		{
			lock (this)
			{
				if (this.folderContentChangeNotifications.ContainsKey(folderId))
				{
					this.ClearFolderContentChangePayload(folderId);
					this.folderContentChangeNotifications[folderId].NotificationHandler = null;
					this.folderContentChangeNotifications[folderId].Queue = null;
					this.folderContentChangeNotifications.Remove(folderId);
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0003FD10 File Offset: 0x0003DF10
		public bool ShouldIgnoreNotification(OwaStoreObjectId folderId)
		{
			lock (this)
			{
				if (this.folderRefreshList.Contains(folderId))
				{
					return true;
				}
				Queue<QueryNotification> folderContentChangePayloadQueue = this.GetFolderContentChangePayloadQueue(folderId);
				if (folderContentChangePayloadQueue != null && folderContentChangePayloadQueue.Count >= 40)
				{
					this.AddFolderRefreshPayload(folderId);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0003FD7C File Offset: 0x0003DF7C
		public void AddFolderContentChangePayload(OwaStoreObjectId folderId, QueryNotification notification)
		{
			lock (this)
			{
				if (!this.folderRefreshList.Contains(folderId))
				{
					Queue<QueryNotification> folderContentChangePayloadQueue = this.GetFolderContentChangePayloadQueue(folderId);
					if (folderContentChangePayloadQueue != null && folderContentChangePayloadQueue.Count >= 40)
					{
						this.AddFolderRefreshPayload(folderId);
					}
					else
					{
						folderContentChangePayloadQueue.Enqueue(notification);
					}
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0003FDE4 File Offset: 0x0003DFE4
		private void ClearFolderContentChangePayload(OwaStoreObjectId folderId)
		{
			lock (this)
			{
				Queue<QueryNotification> folderContentChangePayloadQueue = this.GetFolderContentChangePayloadQueue(folderId);
				if (folderContentChangePayloadQueue != null)
				{
					folderContentChangePayloadQueue.Clear();
				}
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003FE2C File Offset: 0x0003E02C
		private Queue<QueryNotification> GetFolderContentChangePayloadQueue(OwaStoreObjectId folderId)
		{
			if (this.folderContentChangeNotifications.ContainsKey(folderId))
			{
				return this.folderContentChangeNotifications[folderId].Queue;
			}
			return null;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003FE50 File Offset: 0x0003E050
		private void ClearAllFolderContentChangeNotifications()
		{
			if (this.folderContentChangeNotifications.Count > 0)
			{
				foreach (EmailPayload.FCNHState fcnhstate in this.folderContentChangeNotifications.Values)
				{
					fcnhstate.Queue.Clear();
				}
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0003FEBC File Offset: 0x0003E0BC
		private bool AreThereFolderContentChangeNotifications()
		{
			if (this.folderContentChangeNotifications.Count > 0)
			{
				foreach (EmailPayload.FCNHState fcnhstate in this.folderContentChangeNotifications.Values)
				{
					if (fcnhstate.Queue.Count > 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0003FF30 File Offset: 0x0003E130
		public void AddNewMailPayload(StringBuilder newMailBuilder)
		{
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					this.payloadStringNewMail = newMailBuilder;
				}
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0003FF7C File Offset: 0x0003E17C
		public void AddRemindersPayload(StringBuilder remindersBuilder)
		{
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					this.payloadStringReminderChanges = remindersBuilder;
				}
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0003FFC8 File Offset: 0x0003E1C8
		public void AddQuotaPayload(string mailboxQuotaHtml)
		{
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					StringBuilder sb = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture))
					{
						stringWriter.Write("updateMailboxUsage(\"");
						Utilities.JavascriptEncode(mailboxQuotaHtml, stringWriter);
						stringWriter.Write("\"); ");
					}
					this.payloadStringQuota = sb;
				}
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0004005C File Offset: 0x0003E25C
		public void AddReminderNotifyPayload(int minutesOffset)
		{
			StringBuilder sb = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture))
			{
				stringWriter.Write("rmNotfy(");
				stringWriter.Write(minutesOffset);
				stringWriter.Write(", 0, \"\");");
			}
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					this.payloadStringReminderNotify = sb;
				}
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000400F0 File Offset: 0x0003E2F0
		public void AddRefreshPayload()
		{
			StringBuilder sb = null;
			lock (this)
			{
				this.Clear(false);
				if (this.payloadStringRefreshAll.Length == 0)
				{
					sb = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture))
					{
						stringWriter.Write("stMapiRfrshAll();");
					}
					this.payloadStringRefreshAll = sb;
				}
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00040178 File Offset: 0x0003E378
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.userContext != null && this.userContext.PendingRequestManager != null)
			{
				this.userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000401A0 File Offset: 0x0003E3A0
		internal void AddFolderCountPayload(OwaStoreObjectId folderId, long itemCount, long itemUnreadCount)
		{
			lock (this)
			{
				if (this.payloadStringRefreshAll.Length == 0)
				{
					this.folderCountTable[folderId] = new ItemCountPair(itemCount, itemUnreadCount);
					if (this.folderCountTable.Count > 200)
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_MessagingPayloadNotBeingPickedup, string.Empty, new object[]
						{
							this.mailboxSessionDisplayName
						});
						this.AddRefreshPayload();
					}
				}
			}
		}

		// Token: 0x04000621 RID: 1569
		private const int MaxFolderCountTableSize = 200;

		// Token: 0x04000622 RID: 1570
		private const int MaxFolderChangeListSize = 20;

		// Token: 0x04000623 RID: 1571
		private const int DefaultPayloadStringSize = 256;

		// Token: 0x04000624 RID: 1572
		private const int DefaultFolderContentChangePayloadStringSize = 1280;

		// Token: 0x04000625 RID: 1573
		private const int MaxFolderContentNotificationQueueSize = 40;

		// Token: 0x04000626 RID: 1574
		private Dictionary<OwaStoreObjectId, ItemCountPair> folderCountTable;

		// Token: 0x04000627 RID: 1575
		private bool containsDataToPickup;

		// Token: 0x04000628 RID: 1576
		private List<OwaStoreObjectId> folderRefreshList;

		// Token: 0x04000629 RID: 1577
		private StringBuilder payloadStringReminderChanges;

		// Token: 0x0400062A RID: 1578
		private StringBuilder payloadStringNewMail;

		// Token: 0x0400062B RID: 1579
		private StringBuilder payloadStringQuota;

		// Token: 0x0400062C RID: 1580
		private StringBuilder payloadStringReminderNotify;

		// Token: 0x0400062D RID: 1581
		private StringBuilder payloadStringRefreshAll;

		// Token: 0x0400062E RID: 1582
		private Dictionary<OwaStoreObjectId, EmailPayload.FCNHState> folderContentChangeNotifications;

		// Token: 0x0400062F RID: 1583
		private UserContext userContext;

		// Token: 0x04000630 RID: 1584
		private OwaMapiNotificationHandler omnhParent;

		// Token: 0x04000631 RID: 1585
		private int connectionAliveTimerCount;

		// Token: 0x04000632 RID: 1586
		private string mailboxSessionDisplayName;

		// Token: 0x02000107 RID: 263
		internal class FCNHState
		{
			// Token: 0x060008B3 RID: 2227 RVA: 0x00040230 File Offset: 0x0003E430
			internal FCNHState(FolderContentChangeNotificationHandler notificationHandler)
			{
				this.NotificationHandler = notificationHandler;
				this.Queue = new Queue<QueryNotification>();
			}

			// Token: 0x04000634 RID: 1588
			internal FolderContentChangeNotificationHandler NotificationHandler;

			// Token: 0x04000635 RID: 1589
			internal Queue<QueryNotification> Queue;
		}
	}
}
