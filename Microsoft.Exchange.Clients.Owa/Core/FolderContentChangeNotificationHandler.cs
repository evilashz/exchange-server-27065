using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200025E RID: 606
	internal sealed class FolderContentChangeNotificationHandler : NotificationHandlerBase
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0007C2B6 File Offset: 0x0007A4B6
		private string MailboxSessionDisplayName
		{
			get
			{
				if (this.mailboxSessionDisplayName != null)
				{
					return this.mailboxSessionDisplayName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0007C2CC File Offset: 0x0007A4CC
		private ListViewContents2 ItemList
		{
			get
			{
				if (this.listView is GroupByList2)
				{
					return ((GroupByList2)this.listView).ItemList;
				}
				return this.listView;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
		internal QueryResult QueryResult
		{
			get
			{
				QueryResult result;
				lock (this.syncRoot)
				{
					if (this.isDisposed || this.needReinitSubscriptions)
					{
						result = null;
					}
					else
					{
						result = this.result;
					}
				}
				return result;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0007C34C File Offset: 0x0007A54C
		internal bool ShouldIgnoreNotification
		{
			get
			{
				return this.payload.ShouldIgnoreNotification(this.contextFolderId);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0007C35F File Offset: 0x0007A55F
		private bool IsConversationView
		{
			get
			{
				return this.isConversationView;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0007C367 File Offset: 0x0007A567
		internal ExDateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0007C36F File Offset: 0x0007A56F
		internal bool IssueDelayedLoadfresh
		{
			get
			{
				return this.issueDelayedLoadfresh;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0007C377 File Offset: 0x0007A577
		internal bool NeedReinitSubscriptions
		{
			get
			{
				return this.needReinitSubscriptions;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0007C37F File Offset: 0x0007A57F
		internal bool AllowFolderRefreshNotification
		{
			get
			{
				return this.needReinitSubscriptions || this.missedNotifications;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0007C391 File Offset: 0x0007A591
		internal SortBy[] SortBy
		{
			get
			{
				return this.sortBy;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0007C399 File Offset: 0x0007A599
		internal FolderVirtualListViewFilter FolderFilter
		{
			get
			{
				return this.folderFilter;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0007C3A4 File Offset: 0x0007A5A4
		internal FolderContentChangeNotificationHandler(UserContext userContext, MailboxSession mailboxSession, OwaStoreObjectId contextFolderId, OwaStoreObjectId dataFolderId, QueryResult result, EmailPayload emailPayload, ListViewContents2 listView, PropertyDefinition[] subscriptionProperties, Dictionary<PropertyDefinition, int> propertyMap, SortBy[] sortBy, FolderVirtualListViewFilter folderFilter, bool isConversationView) : base(userContext, mailboxSession)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.listView = listView;
			this.subscriptionProperties = subscriptionProperties;
			this.contextFolderId = contextFolderId;
			this.dataFolderId = dataFolderId;
			this.result = result;
			this.payload = emailPayload;
			this.propertyMap = propertyMap;
			this.sortBy = sortBy;
			this.folderFilter = folderFilter;
			this.isConversationView = isConversationView;
			this.mailboxSessionDisplayName = mailboxSession.DisplayName;
			this.InitializeCachedObjectsThatNeedMailboxSession();
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0007C450 File Offset: 0x0007A650
		internal bool TrySubscribe(ConnectionDroppedNotificationHandler connectionDroppedNotificationHandler)
		{
			bool result;
			lock (this.syncRoot)
			{
				if (this.isDisposed)
				{
					throw new InvalidOperationException("Cannot call Subscribe on a Disposed object");
				}
				if (!this.userContext.LockedByCurrentThread())
				{
					throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscribing for folder content change notifications for user {0}", this.MailboxSessionDisplayName);
				try
				{
					this.InitSubscription();
				}
				catch (MapiExceptionObjectDisposed)
				{
					OwaMapiNotificationHandler.DisposeXSOObjects(this.result);
					return false;
				}
				catch (ObjectDisposedException)
				{
					return false;
				}
				catch (StoragePermanentException)
				{
					return false;
				}
				catch (StorageTransientException)
				{
					return false;
				}
				this.payload.AttachFolderContentChangeNotificationHandler(this.contextFolderId, this);
				connectionDroppedNotificationHandler.OnConnectionDropped += this.HandleConnectionDroppedNotification;
				result = true;
			}
			return result;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0007C550 File Offset: 0x0007A750
		protected override void InitSubscription()
		{
			lock (this.syncRoot)
			{
				if (this.mapiSubscription == null)
				{
					this.mapiSubscription = Subscription.Create(this.result, new NotificationHandler(this.HandleNotification));
					if (Globals.ArePerfCountersEnabled)
					{
						OwaSingleCounters.ActiveMailboxSubscriptions.Increment();
					}
				}
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0007C5C4 File Offset: 0x0007A7C4
		internal void RemoveSubscription(ConnectionDroppedNotificationHandler connectionDroppedNotificationHandler)
		{
			lock (this.syncRoot)
			{
				if (!this.userContext.LockedByCurrentThread())
				{
					throw new InvalidOperationException("UserContext lock should be acquired before calling this method");
				}
				if (connectionDroppedNotificationHandler != null)
				{
					connectionDroppedNotificationHandler.OnConnectionDropped -= this.HandleConnectionDroppedNotification;
				}
				this.needReinitSubscriptions = true;
				this.payload.DetachFolderContentChangeNotificationHandler(this.contextFolderId);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Removed folder content change notification subscription for user {0}", this.MailboxSessionDisplayName);
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0007C660 File Offset: 0x0007A860
		internal override void HandleNotification(Notification xsoNotification)
		{
			try
			{
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.TotalMailboxNotifications.Increment();
				}
				lock (this.syncRoot)
				{
					if (!this.isDisposed && !this.missedNotifications && !this.needReinitSubscriptions && !this.ShouldIgnoreNotification)
					{
						if (xsoNotification == null)
						{
							ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Received NULL notification");
						}
						else
						{
							QueryNotification queryNotification = (QueryNotification)xsoNotification;
							if (!this.ProcessErrorNotification(queryNotification))
							{
								switch (queryNotification.EventType)
								{
								case QueryNotificationType.QueryResultChanged:
								case QueryNotificationType.Reload:
									this.ProcessReloadNotification();
									break;
								case QueryNotificationType.RowAdded:
								case QueryNotificationType.RowDeleted:
								case QueryNotificationType.RowModified:
									this.payload.AddFolderContentChangePayload(this.contextFolderId, queryNotification);
									break;
								}
								this.payload.PickupData();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Unexpected exception in FolderContentChangeNotificationHandler:HandleNotification for user {0}. Exception: {1}", this.MailboxSessionDisplayName, ex.Message);
				this.missedNotifications = true;
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0007C7A0 File Offset: 0x0007A9A0
		internal void ProcessNotification(TextWriter writer, QueryNotification notification, out bool successfullyProcessed)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			successfullyProcessed = true;
			if (this.isDisposed || this.missedNotifications || this.needReinitSubscriptions)
			{
				string message = string.Format("Ignoring folder content change notification for user {0}, isDisposed:{1}, missedNotifications:{2}, needReinitSubscriptions:{3}", new object[]
				{
					this.MailboxSessionDisplayName,
					this.isDisposed,
					this.missedNotifications,
					this.needReinitSubscriptions
				});
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), message);
				return;
			}
			try
			{
				QueryNotificationType eventType = notification.EventType;
				Culture.InternalSetAsyncThreadCulture(this.userContext.UserCulture);
				switch (eventType)
				{
				case QueryNotificationType.RowAdded:
					this.ProcessRowAddedNotification(writer, notification);
					break;
				case QueryNotificationType.RowDeleted:
					this.ProcessRowDeletedNotification(writer, notification);
					break;
				case QueryNotificationType.RowModified:
					this.ProcessRowModifiedNotification(writer, notification);
					break;
				default:
					throw new ArgumentException("Invalid queryNotificationType :" + eventType);
				}
			}
			catch (Exception ex)
			{
				if (!this.isDisposed && !this.missedNotifications && !this.needReinitSubscriptions)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in FolderContentChangeNotificationHandler:ProcessNotification. Exception: {0}", ex.Message);
					if (Globals.SendWatsonReports)
					{
						ExTraceGlobals.CoreTracer.TraceDebug(0L, "Sending watson report");
						ExWatson.AddExtraData(this.GetExtraWatsonData());
						ExWatson.SendReport(ex, ReportOptions.None, null);
					}
					successfullyProcessed = false;
					this.missedNotifications = true;
				}
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0007C924 File Offset: 0x0007AB24
		internal bool HasDataFolderChanged(OwaStoreObjectId inDataFolderId)
		{
			if (inDataFolderId == null)
			{
				throw new ArgumentNullException("inDataFolderId");
			}
			return !this.dataFolderId.Equals(inDataFolderId);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0007C944 File Offset: 0x0007AB44
		internal override void HandlePendingGetTimerCallback()
		{
			lock (this.syncRoot)
			{
				if (!this.isDisposed)
				{
					if (this.needReinitSubscriptions)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Sending refresh payload to the client for user {0}", this.MailboxSessionDisplayName);
						this.AddFolderRefreshPayload();
					}
				}
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0007C9B4 File Offset: 0x0007ABB4
		internal override void DisposeInternal()
		{
			this.DisposeInternal(false);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0007C9BD File Offset: 0x0007ABBD
		internal override void DisposeInternal(bool doNotDisposeQueryResult)
		{
			base.DisposeInternal(doNotDisposeQueryResult);
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ActiveMailboxSubscriptions.Decrement();
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0007C9D8 File Offset: 0x0007ABD8
		private void ProcessRowAddedNotification(TextWriter writer, QueryNotification notification)
		{
			this.CreateNotificationDataSource(notification);
			this.AddRowNotificationPrefix(writer, notification);
			writer.Write(",'");
			Utilities.JavascriptEncode(Convert.ToBase64String(notification.Prior), writer);
			writer.Write("','");
			this.AddRowData(writer, notification.EventType, ListViewContents2.ListViewRowType.RenderOnlyRow1);
			writer.Write("','");
			this.AddRowData(writer, notification.EventType, ListViewContents2.ListViewRowType.RenderOnlyRow2);
			writer.Write("','");
			this.AddRowProperties(writer, notification.EventType);
			writer.Write("'");
			this.AddRowNotificationSuffix(writer);
			this.listView.DataSource = null;
			this.ItemList.DataSource = null;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0007CA84 File Offset: 0x0007AC84
		private void ProcessRowDeletedNotification(TextWriter writer, QueryNotification notification)
		{
			this.AddRowNotificationPrefix(writer, notification);
			this.AddRowNotificationSuffix(writer);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0007CA98 File Offset: 0x0007AC98
		private void ProcessRowModifiedNotification(TextWriter writer, QueryNotification notification)
		{
			this.CreateNotificationDataSource(notification);
			this.AddRowNotificationPrefix(writer, notification);
			writer.Write(",'");
			Utilities.JavascriptEncode(Convert.ToBase64String(notification.Prior), writer);
			writer.Write("','");
			this.AddRowData(writer, notification.EventType, ListViewContents2.ListViewRowType.RenderOnlyRow1);
			writer.Write("','");
			this.AddRowData(writer, notification.EventType, ListViewContents2.ListViewRowType.RenderOnlyRow2);
			writer.Write("','");
			this.AddRowProperties(writer, notification.EventType);
			writer.Write("'");
			this.AddRowNotificationSuffix(writer);
			this.listView.DataSource = null;
			this.ItemList.DataSource = null;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0007CB44 File Offset: 0x0007AD44
		private void ProcessReloadNotification()
		{
			this.AddFolderRefreshPayload();
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0007CB4C File Offset: 0x0007AD4C
		private void AddRowNotificationPrefix(TextWriter writer, QueryNotification notification)
		{
			writer.Write("folderChangeNotification('");
			Utilities.JavascriptEncode(this.contextFolderId.StoreObjectId.ToBase64String(), writer);
			writer.Write("',['");
			QueryNotificationType eventType = notification.EventType;
			switch (eventType)
			{
			case QueryNotificationType.RowAdded:
			case QueryNotificationType.RowDeleted:
			case QueryNotificationType.RowModified:
				writer.Write((int)eventType);
				writer.Write("','");
				Utilities.JavascriptEncode(Convert.ToBase64String(notification.Index), writer);
				writer.Write("'");
				return;
			default:
				throw new ArgumentException("invalid value for notificationType");
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0007CBE0 File Offset: 0x0007ADE0
		private void AddRowData(TextWriter writer, QueryNotificationType notificationType, ListViewContents2.ListViewRowType rowTypeToRender)
		{
			bool renderContainer = notificationType != QueryNotificationType.RowModified;
			bool showFlag = this.ItemList.ViewDescriptor.ContainsColumn(ColumnId.FlagDueDate);
			StringBuilder stringBuilder = new StringBuilder(1280);
			using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				this.ItemList.RenderRow(stringWriter, showFlag, rowTypeToRender, renderContainer);
			}
			Utilities.JavascriptEncode(stringBuilder.ToString(), writer, true);
			stringBuilder.Length = 0;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0007CC60 File Offset: 0x0007AE60
		private void AddRowProperties(TextWriter writer, QueryNotificationType notificationType)
		{
			Utilities.JavascriptEncode(this.ItemList.DataSource.GetItemProperty<bool>(MessageItemSchema.IsRead, true) ? "1" : "0", writer);
			writer.Write("','");
			string s = string.Empty;
			if (this.listView is GroupByList2)
			{
				s = ((GroupByList2)this.listView).GetItemGroupByValueString();
			}
			Utilities.JavascriptEncode(s, writer);
			if (notificationType == QueryNotificationType.RowModified && !this.isConversationView)
			{
				writer.Write("','");
				Utilities.JavascriptEncode(this.ItemList.DataSource.GetItemProperty<string>(ItemSchema.Subject, string.Empty), writer);
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0007CD04 File Offset: 0x0007AF04
		private void AddRowNotificationSuffix(TextWriter writer)
		{
			writer.Write("]);");
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0007CD14 File Offset: 0x0007AF14
		private void CreateNotificationDataSource(QueryNotification notification)
		{
			this.listView.DataSource = new ListViewNotificationDataSource(this.userContext, this.dataFolderId.StoreObjectId, this.isConversationView, this.propertyMap, this.sortBy, notification);
			this.ItemList.DataSource = this.listView.DataSource;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0007CD6C File Offset: 0x0007AF6C
		private bool ProcessErrorNotification(QueryNotification notification)
		{
			bool flag = false;
			if (notification.ErrorCode != 0)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<int>((long)this.GetHashCode(), "Error in Notification: {0}", notification.ErrorCode);
				flag = true;
			}
			else if (notification.EventType == QueryNotificationType.Error)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Error in Notification, Type is QueryNotificationType.Error");
				flag = true;
			}
			else if ((notification.EventType == QueryNotificationType.RowAdded || notification.EventType == QueryNotificationType.RowModified) && notification.Row.Length < this.subscriptionProperties.Length)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<int, int>((long)this.GetHashCode(), "XSO gave an incomplete notification, expected row length {0}, actual row length {1}", notification.Row.Length, this.subscriptionProperties.Length);
				flag = true;
			}
			try
			{
				TimeGroupByList2 timeGroupByList = this.listView as TimeGroupByList2;
				if (timeGroupByList != null && !timeGroupByList.IsValid())
				{
					flag = true;
					this.needReinitSubscriptions = true;
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected exception in FolderContentChangeNotificationHandler:ProcessErrorNotification. Exception: {0}", ex.ToString());
				if (Globals.SendWatsonReports)
				{
					ExTraceGlobals.CoreTracer.TraceDebug(0L, "Sending watson report");
					ExWatson.AddExtraData(this.GetExtraWatsonData());
					ExWatson.SendReport(ex);
				}
				flag = true;
				this.needReinitSubscriptions = true;
			}
			if (flag)
			{
				this.AddFolderRefreshPayload();
				this.payload.PickupData();
				return true;
			}
			return false;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0007CEAC File Offset: 0x0007B0AC
		private void AddFolderRefreshPayload()
		{
			this.payload.AddFolderRefreshPayload(this.contextFolderId);
			this.missedNotifications = true;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0007CEC6 File Offset: 0x0007B0C6
		private void InitializeCachedObjectsThatNeedMailboxSession()
		{
			this.userContext.GetMasterCategoryList();
			if (this.isConversationView)
			{
				IList<StoreObjectId> excludedFolderIds = ((ConversationItemList2)this.ItemList).ExcludedFolderIds;
				StoreObjectId draftsFolderId = this.userContext.DraftsFolderId;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0007CEFC File Offset: 0x0007B0FC
		private void ThrottleNotifications()
		{
			ExDateTime now = ExDateTime.Now;
			this.notificationCountSinceLastTimer++;
			if (now - this.throttleDurationStart >= this.throttleDuration)
			{
				if (this.notificationCountSinceLastTimer >= 300)
				{
					this.issueDelayedLoadfresh = true;
					return;
				}
				this.notificationCountSinceLastTimer = 0;
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0007CF54 File Offset: 0x0007B154
		private string GetExtraWatsonData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OWA Version: ");
			stringBuilder.Append(Globals.ApplicationVersion);
			stringBuilder.AppendLine();
			if (this.listView is GroupByList2)
			{
				GroupByList2 groupByList = (GroupByList2)this.listView;
				stringBuilder.Append(string.Format("GroupByList: SortColumn: {0}, SortOrder: {1}", groupByList.GroupByColumn.Id, groupByList.SortOrder));
			}
			else
			{
				ItemList2 itemList = (ItemList2)this.ItemList;
				stringBuilder.Append(string.Format("NOT GroupByList: SortColumn: {0}, SortOrder: {1}", itemList.SortedColumn.Id, itemList.SortOrder));
			}
			stringBuilder.AppendLine();
			if (this.userContext != null && !Globals.DisableBreadcrumbs)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(this.userContext.DumpBreadcrumbs());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DE3 RID: 3555
		private const int ThrottleDurationSeconds = 30;

		// Token: 0x04000DE4 RID: 3556
		private const int ThrottleThreshold = 300;

		// Token: 0x04000DE5 RID: 3557
		private OwaStoreObjectId contextFolderId;

		// Token: 0x04000DE6 RID: 3558
		private OwaStoreObjectId dataFolderId;

		// Token: 0x04000DE7 RID: 3559
		private EmailPayload payload;

		// Token: 0x04000DE8 RID: 3560
		private ListViewContents2 listView;

		// Token: 0x04000DE9 RID: 3561
		private bool isConversationView;

		// Token: 0x04000DEA RID: 3562
		private PropertyDefinition[] subscriptionProperties;

		// Token: 0x04000DEB RID: 3563
		private ExDateTime creationTime = ExDateTime.Now;

		// Token: 0x04000DEC RID: 3564
		private Dictionary<PropertyDefinition, int> propertyMap;

		// Token: 0x04000DED RID: 3565
		private SortBy[] sortBy;

		// Token: 0x04000DEE RID: 3566
		private FolderVirtualListViewFilter folderFilter;

		// Token: 0x04000DEF RID: 3567
		private string mailboxSessionDisplayName;

		// Token: 0x04000DF0 RID: 3568
		private TimeSpan throttleDuration = new TimeSpan(0, 0, 30);

		// Token: 0x04000DF1 RID: 3569
		private bool issueDelayedLoadfresh;

		// Token: 0x04000DF2 RID: 3570
		private ExDateTime throttleDurationStart = ExDateTime.Now;

		// Token: 0x04000DF3 RID: 3571
		private int notificationCountSinceLastTimer;
	}
}
