using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000108 RID: 264
	internal sealed class SearchPayload : IPendingRequestNotifier
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0004024A File Offset: 0x0003E44A
		internal SearchPayload(UserContext userContext, MailboxSession mailboxSession, OwaMapiNotificationHandler omnhParent)
		{
			this.searchFolderRefreshList = new List<OwaStoreObjectId>();
			this.userContext = userContext;
			this.mailboxSessionDisplayName = string.Copy(mailboxSession.DisplayName);
			this.omnhParent = omnhParent;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060008B5 RID: 2229 RVA: 0x0004027C File Offset: 0x0003E47C
		// (remove) Token: 0x060008B6 RID: 2230 RVA: 0x000402B4 File Offset: 0x0003E4B4
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000402E9 File Offset: 0x0003E4E9
		public bool ShouldThrottle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000402EC File Offset: 0x0003E4EC
		public string ReadDataAndResetState()
		{
			string result = null;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[SearchPayload.ReadDataAndResetState] Mailbox: {0}", this.mailboxSessionDisplayName);
			lock (this)
			{
				this.containsDataToPickup = false;
				StringBuilder stringBuilder = null;
				if (this.searchFolderRefreshList.Count > 0)
				{
					stringBuilder = new StringBuilder();
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
					{
						foreach (OwaStoreObjectId owaStoreObjectId in this.searchFolderRefreshList)
						{
							stringWriter.Write("searchNotification(\"");
							Utilities.JavascriptEncode(owaStoreObjectId.ToBase64String(), stringWriter);
							stringWriter.Write("\");");
						}
					}
				}
				result = ((stringBuilder != null) ? stringBuilder.ToString() : string.Empty);
				this.searchFolderRefreshList.Clear();
				this.UpdateSearchPerformanceData();
			}
			return result;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00040408 File Offset: 0x0003E608
		public void PickupData()
		{
			bool flag = false;
			lock (this)
			{
				flag = (this.searchFolderRefreshList.Count > 0 && !this.containsDataToPickup);
				if (flag)
				{
					this.containsDataToPickup = true;
				}
			}
			if (flag)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[SearchPayload.PickupData] DataAvailable method called after search notification. Mailbox: {0}", this.mailboxSessionDisplayName);
				this.DataAvailable(this, new EventArgs());
				return;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[SearchPayload.PickupData] No need to call DataAvailable method. Mailbox: {0}", this.mailboxSessionDisplayName);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000404B4 File Offset: 0x0003E6B4
		public void ConnectionAliveTimer()
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000404B8 File Offset: 0x0003E6B8
		internal void AddSearchFolderRefreshPayload(OwaStoreObjectId folderId, SearchNotificationType searchNotificationType)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<SearchNotificationType>((long)this.GetHashCode(), "[SearchPayload.AddSearchFolderRefreshPayload] Adding search notification type {0}", searchNotificationType);
			lock (this)
			{
				this.searchNotificationType |= searchNotificationType;
				if (!this.searchFolderRefreshList.Contains(folderId))
				{
					this.searchFolderRefreshList.Add(folderId);
				}
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0004053C File Offset: 0x0003E73C
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.userContext != null && this.userContext.PendingRequestManager != null)
			{
				this.userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00040564 File Offset: 0x0003E764
		private void UpdateSearchPerformanceData()
		{
			if (this.omnhParent != null && this.searchNotificationType != SearchNotificationType.None)
			{
				SearchPerformanceData searchPerformanceData = this.omnhParent.SearchPerformanceData;
				if (searchPerformanceData != null)
				{
					searchPerformanceData.NotificationPickedUpByPendingGet(this.searchNotificationType);
					this.searchNotificationType = SearchNotificationType.None;
				}
			}
		}

		// Token: 0x04000636 RID: 1590
		private List<OwaStoreObjectId> searchFolderRefreshList;

		// Token: 0x04000637 RID: 1591
		private bool containsDataToPickup;

		// Token: 0x04000638 RID: 1592
		private UserContext userContext;

		// Token: 0x04000639 RID: 1593
		private OwaMapiNotificationHandler omnhParent;

		// Token: 0x0400063A RID: 1594
		private string mailboxSessionDisplayName;

		// Token: 0x0400063B RID: 1595
		private SearchNotificationType searchNotificationType;
	}
}
