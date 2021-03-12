using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B0 RID: 432
	internal class SearchNotificationHandler : MapiNotificationHandlerBase, IOwaCallback
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x0003C00F File Offset: 0x0003A20F
		public SearchNotificationHandler(IMailboxContext userContext) : base(userContext, false)
		{
			this.searchNotifier = new SearchNotifier(userContext);
			this.searchNotifier.RegisterWithPendingRequestNotifier();
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003C030 File Offset: 0x0003A230
		public void ProcessCallback(object owaContext)
		{
			OwaSearchContext owaSearchContext = owaContext as OwaSearchContext;
			if (owaSearchContext != null)
			{
				lock (base.SyncRoot)
				{
					base.MissedNotifications = false;
					base.NeedToReinitSubscriptions = false;
					this.SubscribeForSearchComplete(owaSearchContext);
				}
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003C08C File Offset: 0x0003A28C
		internal override void HandlePendingGetTimerCallback(MapiNotificationsLogEvent logEvent)
		{
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		internal override void HandleNotificationInternal(Notification notif, MapiNotificationsLogEvent logEvent, object context)
		{
			if (notif == null)
			{
				return;
			}
			if ((notif.Type & NotificationType.SearchComplete) != NotificationType.SearchComplete)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification is not for search complete");
				return;
			}
			OwaSearchContext localSearchContext = context as OwaSearchContext;
			if (localSearchContext == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "notification has  not passed in context data");
				throw new ArgumentNullException("context");
			}
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				lock (this.SyncRoot)
				{
					if (localSearchContext != this.currentSearchContext)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "Not sending search completed notification because the currentSearchContext and the localSearchContext are different");
						return;
					}
				}
				SearchNotificationPayload payload = SearchNotificationHandler.SearchPayloadCreator.CreatePayLoad(this.UserContext, localSearchContext);
				lock (this.SyncRoot)
				{
					if (localSearchContext != this.currentSearchContext)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "Payload data calculated. Not sending the notification to the client because another search has been triggered");
					}
					else
					{
						this.searchNotifier.Payload = payload;
						this.searchNotifier.PickupData();
					}
				}
			}, null);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003C22E File Offset: 0x0003A42E
		protected override void InitSubscriptionInternal()
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003C230 File Offset: 0x0003A430
		private void SubscribeForSearchComplete(OwaSearchContext searchContext)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "SearchNotificationHandler.SubscribeForSearchComplete Start");
			if (base.IsDisposed)
			{
				return;
			}
			try
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(searchContext.SearchFolderId);
				base.UserContext.LockAndReconnectMailboxSession(3000);
				if (base.Subscription != null)
				{
					MapiNotificationHandlerBase.DisposeXSOObjects(base.Subscription, base.UserContext);
					base.Subscription = null;
				}
				this.currentSearchContext = searchContext;
				base.Subscription = Subscription.Create(base.UserContext.MailboxSession, this.GetDefaultNotificationHandler(this.currentSearchContext), NotificationType.SearchComplete, storeObjectId);
			}
			catch (OwaLockTimeoutException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<string>((long)this.GetHashCode(), "User context lock timed out in SubscribeForSearchComplete. Exception: {0}", ex.Message);
			}
			finally
			{
				if (base.UserContext.MailboxSessionLockedByCurrentThread())
				{
					base.UserContext.UnlockAndDisconnectMailboxSession();
				}
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003C320 File Offset: 0x0003A520
		protected NotificationHandler GetDefaultNotificationHandler(object context)
		{
			return new NotificationHandler(new SearchNotificationHandler.SubscriptionContextHolder(this, context).HandleNotification);
		}

		// Token: 0x04000942 RID: 2370
		private SearchNotifier searchNotifier;

		// Token: 0x04000943 RID: 2371
		private OwaSearchContext currentSearchContext;

		// Token: 0x020001B1 RID: 433
		private static class SearchPayloadCreator
		{
			// Token: 0x06000F69 RID: 3945 RVA: 0x0003C358 File Offset: 0x0003A558
			public static SearchNotificationPayload CreatePayLoad(IMailboxContext userContext, OwaSearchContext searchContext)
			{
				SearchNotificationPayload payload = new SearchNotificationPayload();
				payload.ClientId = searchContext.ClientSearchFolderIdentity;
				payload.IsComplete = true;
				payload.Source = MailboxLocation.FromMailboxContext(userContext);
				SearchNotificationHandler.SearchPayloadCreator.SetHighlightTerms(payload, searchContext.HighlightTerms);
				try
				{
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						SearchNotificationHandler.SearchPayloadCreator.FillItemDataInPayload(userContext, searchContext, payload);
					});
				}
				catch (GrayException arg)
				{
					payload.ServerSearchResultsRowCount = -1;
					ExTraceGlobals.NotificationsCallTracer.TraceError<GrayException>(0L, "MapiNotificationHandlerBase.CreatePayLoad Unable to create payload with data for search results.  exception {0}", arg);
				}
				return payload;
			}

			// Token: 0x06000F6A RID: 3946 RVA: 0x0003C424 File Offset: 0x0003A624
			private static void FillItemDataInPayload(IMailboxContext userContext, OwaSearchContext searchContext, SearchNotificationPayload payload)
			{
				try
				{
					userContext.LockAndReconnectMailboxSession(3000);
					int serverSearchResultsRowCount = 0;
					if (searchContext.SearchContextType == SearchContextType.ItemSearch)
					{
						payload.MessageItems = SearchFolderItemDataRetriever.GetItemDataFromSearchFolder(searchContext, userContext.MailboxSession, out serverSearchResultsRowCount);
					}
					else if (searchContext.SearchContextType == SearchContextType.ConversationSearch)
					{
						payload.Conversations = SearchFolderConversationRetriever.GetConversationDataFromSearchFolder(searchContext, userContext.MailboxSession, out serverSearchResultsRowCount);
					}
					payload.ServerSearchResultsRowCount = serverSearchResultsRowCount;
				}
				catch (OwaLockTimeoutException ex)
				{
					ExTraceGlobals.CoreCallTracer.TraceError<string>(0L, "User context lock timed out in FillItemDataInPayload. Exception: {0}", ex.Message);
				}
				finally
				{
					if (userContext.MailboxSessionLockedByCurrentThread())
					{
						userContext.UnlockAndDisconnectMailboxSession();
					}
				}
			}

			// Token: 0x06000F6B RID: 3947 RVA: 0x0003C4CC File Offset: 0x0003A6CC
			private static void SetHighlightTerms(SearchNotificationPayload payload, KeyValuePair<string, string>[] searchContextHighlightTerms)
			{
				if (searchContextHighlightTerms == null || searchContextHighlightTerms.Length <= 0)
				{
					return;
				}
				HighlightTermType[] array = new HighlightTermType[searchContextHighlightTerms.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new HighlightTermType
					{
						Scope = searchContextHighlightTerms[i].Key,
						Value = searchContextHighlightTerms[i].Value
					};
				}
				payload.HighlightTerms = array;
			}
		}

		// Token: 0x020001B2 RID: 434
		private class SubscriptionContextHolder
		{
			// Token: 0x06000F6C RID: 3948 RVA: 0x0003C52F File Offset: 0x0003A72F
			public SubscriptionContextHolder(MapiNotificationHandlerBase parent, object context)
			{
				this.parent = parent;
				this.context = context;
			}

			// Token: 0x06000F6D RID: 3949 RVA: 0x0003C545 File Offset: 0x0003A745
			internal void HandleNotification(Notification notification)
			{
				this.parent.HandleNotification(notification, this.context);
			}

			// Token: 0x04000944 RID: 2372
			private readonly object context;

			// Token: 0x04000945 RID: 2373
			private readonly MapiNotificationHandlerBase parent;
		}
	}
}
