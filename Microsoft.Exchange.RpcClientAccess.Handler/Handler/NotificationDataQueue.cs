using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationDataQueue
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00015710 File Offset: 0x00013910
		public NotificationDataQueue(int maxNotifications, Func<string> traceContextDelegate)
		{
			this.maxNotifications = maxNotifications;
			this.traceContextDelegate = traceContextDelegate;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0001573C File Offset: 0x0001393C
		internal bool IsEmpty
		{
			get
			{
				return this.notificationList.Count == 0;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0001574C File Offset: 0x0001394C
		private string TraceContext
		{
			get
			{
				if (this.traceContext == null && this.traceContextDelegate != null)
				{
					this.traceContext = this.traceContextDelegate();
				}
				else
				{
					this.traceContext = string.Empty;
				}
				return this.traceContext;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00015784 File Offset: 0x00013984
		internal void Enqueue(NotificationData data)
		{
			lock (this.notificationListLock)
			{
				if (this.notificationList.Count >= this.maxNotifications)
				{
					this.CollapseTableNotifications();
				}
				if (!this.TryFilterNotificationData(data))
				{
					this.AddNotificationData(data);
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000157E8 File Offset: 0x000139E8
		internal bool Peek(out NotificationData data)
		{
			bool result;
			lock (this.notificationListLock)
			{
				if (this.notificationList.Count > 0)
				{
					data = this.notificationList[0];
					result = true;
				}
				else
				{
					data = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00015848 File Offset: 0x00013A48
		internal bool Dequeue(out NotificationData data)
		{
			data = null;
			if (this.notificationList.Count == 0)
			{
				return false;
			}
			lock (this.notificationListLock)
			{
				if (this.notificationList.Count == 0)
				{
					return false;
				}
				data = this.notificationList[0];
				this.notificationList.RemoveAt(0);
				this.TraceQueueEvent("Dequeue", string.Empty, data);
			}
			return true;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000158D4 File Offset: 0x00013AD4
		internal NotificationData[] ToArray()
		{
			return this.notificationList.ToArray();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000158E4 File Offset: 0x00013AE4
		private static bool CompareByteArrays(byte[] array1, byte[] array2)
		{
			if (array1 != null && array2 != null && array1.Length == array2.Length)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001591A File Offset: 0x00013B1A
		private static NotificationData CreateTableChangedNotification(NotificationData data, QueryNotification queryNotification)
		{
			return new NotificationData(data.NotificationHandleValue, data.Logon, NotificationDataQueue.queryResultChangedNotification, data.RootFolderId, data.View, data.String8Encoding);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00015944 File Offset: 0x00013B44
		private static NotificationData CreateTableRowAddedNotification(NotificationData data, QueryNotification queryNotification)
		{
			return new NotificationData(data.NotificationHandleValue, data.Logon, queryNotification.CreateRowAddedNotification(), data.RootFolderId, data.View, data.String8Encoding);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00015970 File Offset: 0x00013B70
		private void TraceQueueEvent(string eventName, string eventDescription, NotificationData data)
		{
			if (ExTraceGlobals.NotificationHandlerTracer.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				ExTraceGlobals.NotificationHandlerTracer.TracePerformance(Activity.TraceId, "{0}: <<{1}>> Handle = {2}. NotificationType = {3}. CurrentQueueSize = {4}, MaxQueueSize = {5}. {6}", new object[]
				{
					this.TraceContext,
					eventName,
					data.NotificationHandleValue,
					data.Notification.Type,
					this.notificationList.Count,
					this.maxNotifications,
					eventDescription
				});
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000159FC File Offset: 0x00013BFC
		private void CollapseTableNotifications()
		{
			for (int i = 0; i < this.notificationList.Count; i++)
			{
				NotificationData notificationData = this.notificationList[i];
				if (notificationData != null && notificationData.Notification != null && notificationData.Notification.Type == NotificationType.Query)
				{
					QueryNotification queryNotification = notificationData.Notification as QueryNotification;
					if (queryNotification != null && queryNotification.EventType != QueryNotificationType.QueryResultChanged)
					{
						this.PurgeAllNotifications(notificationData.Logon, notificationData.NotificationHandleValue, i + 1);
						this.notificationList[i] = NotificationDataQueue.CreateTableChangedNotification(notificationData, queryNotification);
					}
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00015A8C File Offset: 0x00013C8C
		private void PurgeAllNotifications(Logon logon, ServerObjectHandle handle, int startIndex = 0)
		{
			if (this.notificationList.Count > 0)
			{
				for (int i = this.notificationList.Count - 1; i >= startIndex; i--)
				{
					NotificationData notificationData = this.notificationList[i];
					if (notificationData.Logon == logon && notificationData.NotificationHandleValue == handle)
					{
						this.notificationList.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00015AF0 File Offset: 0x00013CF0
		private bool TryCombineTableNotification(NotificationData data, QueryNotification queryNotification)
		{
			if (queryNotification.EventType == QueryNotificationType.RowModified || queryNotification.EventType == QueryNotificationType.RowDeleted)
			{
				for (int i = this.notificationList.Count - 1; i >= 0; i--)
				{
					NotificationData notificationData = this.notificationList[i];
					if (notificationData != null && notificationData.Notification != null && notificationData.Notification.Type == NotificationType.Query && data.Logon == notificationData.Logon && data.NotificationHandleValue == notificationData.NotificationHandleValue)
					{
						QueryNotification queryNotification2 = notificationData.Notification as QueryNotification;
						if (queryNotification2 != null && (queryNotification2.EventType == QueryNotificationType.RowAdded || queryNotification2.EventType == QueryNotificationType.RowModified))
						{
							if (NotificationDataQueue.CompareByteArrays(queryNotification.Index, queryNotification2.Index))
							{
								if (queryNotification.EventType == QueryNotificationType.RowModified)
								{
									if (queryNotification2.EventType == QueryNotificationType.RowAdded)
									{
										this.notificationList[i] = NotificationDataQueue.CreateTableRowAddedNotification(data, queryNotification);
									}
									else
									{
										this.notificationList[i] = data;
									}
									return true;
								}
								this.notificationList.RemoveAt(i);
								if (queryNotification2.EventType == QueryNotificationType.RowAdded)
								{
									return true;
								}
							}
							else if (NotificationDataQueue.CompareByteArrays(queryNotification.Prior, queryNotification2.Index) || NotificationDataQueue.CompareByteArrays(queryNotification.Index, queryNotification2.Prior))
							{
								return false;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00015C3C File Offset: 0x00013E3C
		private bool IsTableChangedAlreadyQueued(Logon logon, ServerObjectHandle handle)
		{
			for (int i = 0; i < this.notificationList.Count; i++)
			{
				NotificationData notificationData = this.notificationList[i];
				if (notificationData != null && notificationData.Notification != null && notificationData.Notification.Type == NotificationType.Query && notificationData.Logon == logon && notificationData.NotificationHandleValue == handle)
				{
					QueryNotification queryNotification = notificationData.Notification as QueryNotification;
					if (queryNotification != null && queryNotification.EventType == QueryNotificationType.QueryResultChanged)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00015CBC File Offset: 0x00013EBC
		private bool TryFilterNotificationData(NotificationData data)
		{
			if (this.notificationList.Count > 0 && data != null && data.Notification != null && data.Notification.Type == NotificationType.Query)
			{
				QueryNotification queryNotification = data.Notification as QueryNotification;
				if (queryNotification != null)
				{
					if (queryNotification.EventType == QueryNotificationType.QueryResultChanged)
					{
						this.PurgeAllNotifications(data.Logon, data.NotificationHandleValue, 0);
						this.TraceQueueEvent("Purged", "TableChanged event, purging all notifications for this table.", data);
						return false;
					}
					if (this.IsTableChangedAlreadyQueued(data.Logon, data.NotificationHandleValue))
					{
						this.TraceQueueEvent("Filtered", "TableChanged already queued for this table.", data);
						return true;
					}
					if (this.TryCombineTableNotification(data, queryNotification))
					{
						this.TraceQueueEvent("Filtered", "Combined with previous notification for this table.", data);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00015D84 File Offset: 0x00013F84
		private void AddNotificationData(NotificationData data)
		{
			if (this.notificationList.Count >= this.maxNotifications)
			{
				NotificationData data2 = this.notificationList[0];
				this.notificationList.RemoveAt(0);
				this.TraceQueueEvent("Dropped", "Queue full notification dropped.", data2);
			}
			this.notificationList.Add(data);
			this.TraceQueueEvent("Enqueue", string.Empty, data);
		}

		// Token: 0x040000E8 RID: 232
		private static readonly QueryNotification queryResultChangedNotification = QueryNotification.CreateQueryResultChangedNotification();

		// Token: 0x040000E9 RID: 233
		private readonly List<NotificationData> notificationList = new List<NotificationData>();

		// Token: 0x040000EA RID: 234
		private readonly int maxNotifications;

		// Token: 0x040000EB RID: 235
		private readonly object notificationListLock = new object();

		// Token: 0x040000EC RID: 236
		private readonly Func<string> traceContextDelegate;

		// Token: 0x040000ED RID: 237
		private string traceContext;
	}
}
