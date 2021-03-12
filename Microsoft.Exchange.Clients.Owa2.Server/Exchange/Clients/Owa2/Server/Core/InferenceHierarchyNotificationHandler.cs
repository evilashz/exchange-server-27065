using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200017B RID: 379
	internal class InferenceHierarchyNotificationHandler : HierarchyNotificationHandler
	{
		// Token: 0x06000DCE RID: 3534 RVA: 0x0003439E File Offset: 0x0003259E
		public InferenceHierarchyNotificationHandler(string subscriptionId, UserContext userContext, Guid mailboxGuid) : base(subscriptionId, userContext, mailboxGuid)
		{
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000343A9 File Offset: 0x000325A9
		protected override void InitSubscriptionInternal()
		{
			base.InitSubscriptionInternal();
			this.ResolveFilteredViewSearchFolderIds();
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000343B7 File Offset: 0x000325B7
		protected override bool IsAllowedSearchFolder(StoreObjectId folderId)
		{
			return this.clutterViewFolderId.Equals(folderId) || base.IsAllowedSearchFolder(folderId);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000343D0 File Offset: 0x000325D0
		protected override HierarchyNotificationPayload GetPayloadInstance(StoreObjectId folderId)
		{
			if (this.clutterViewFolderId.Equals(folderId))
			{
				return new FilteredViewHierarchyNotificationPayload
				{
					Filter = ViewFilter.Clutter
				};
			}
			return new HierarchyNotificationPayload();
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00034400 File Offset: 0x00032600
		private void ResolveFilteredViewSearchFolderIds()
		{
			StoreId defaultFolderId = base.UserContext.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			this.clutterViewFolderId = OwaFilterState.GetLinkedFolderIdForFilteredView(base.UserContext.MailboxSession, defaultFolderId, OwaViewFilter.Clutter);
			if (this.clutterViewFolderId == null)
			{
				this.clutterViewFolderId = this.ResolveSearchFolderIdForFilteredView(defaultFolderId, OwaViewFilter.Clutter);
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00034450 File Offset: 0x00032650
		private StoreObjectId ResolveSearchFolderIdForFilteredView(StoreId inboxFolderId, OwaViewFilter viewFilter)
		{
			StoreObjectId storeObjectId = null;
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "HierarchyNotificationHandler.ResolveSearchFolderIdForFilteredView Start. SubscriptionId: {0}", base.SubscriptionId);
			OwaSearchContext owaSearchContext = new OwaSearchContext();
			owaSearchContext.ViewFilter = viewFilter;
			owaSearchContext.FolderIdToSearch = inboxFolderId;
			StoreObjectId defaultFolderId = base.UserContext.MailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders);
			using (SearchFolder owaViewFilterSearchFolder = SearchUtil.GetOwaViewFilterSearchFolder(owaSearchContext, base.UserContext.MailboxSession, defaultFolderId, null, CallContext.Current))
			{
				if (owaViewFilterSearchFolder == null)
				{
					throw new ArgumentNullException(string.Format("HierarchyNotificationHandler.ResolveSearchFolderIdForFilteredView null searchFolder returned for subscriptionId: {0}. ViewFilter: {1}; Source folder id: {2}", base.SubscriptionId, viewFilter, inboxFolderId.ToString()));
				}
				storeObjectId = owaViewFilterSearchFolder.StoreObjectId;
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "HierarchyNotificationHandler.ResolveSearchFolderIdForFilteredView found filtered-view search folder subscriptionId: {0} . ViewFilter: {1}; Source folder id: {2}, Search folder id: {3}", new object[]
				{
					base.SubscriptionId,
					viewFilter,
					inboxFolderId.ToString(),
					storeObjectId.ToString()
				});
			}
			return storeObjectId;
		}

		// Token: 0x04000863 RID: 2147
		private StoreObjectId clutterViewFolderId;
	}
}
