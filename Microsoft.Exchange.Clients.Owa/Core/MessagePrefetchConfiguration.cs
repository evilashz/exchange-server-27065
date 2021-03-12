using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000161 RID: 353
	public static class MessagePrefetchConfiguration
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00054907 File Offset: 0x00052B07
		public static int NumberOfMessagesToPrefetch
		{
			get
			{
				return MessagePrefetchConfiguration.numberOfMessagesToPrefetch;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0005490E File Offset: 0x00052B0E
		public static bool PrefetchOnlyUnreadMessages
		{
			get
			{
				return MessagePrefetchConfiguration.prefetchOnlyUnreadMessages;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00054915 File Offset: 0x00052B15
		public static int MaxMessagesInCache
		{
			get
			{
				return MessagePrefetchConfiguration.maxMessagesInCache;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0005491C File Offset: 0x00052B1C
		public static int MaxCacheSizeInMegaBytes
		{
			get
			{
				return MessagePrefetchConfiguration.maxCacheSizeInMegaBytes;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00054924 File Offset: 0x00052B24
		public static void InitializeSettings()
		{
			MessagePrefetchConfiguration.isMessagePrefetchEnabled = AppSettings.GetConfiguredValue<bool>("IsMessagePrefetchEnabled", true);
			MessagePrefetchConfiguration.numberOfMessagesToPrefetch = AppSettings.GetConfiguredValue<int>("NumberOfMessagesToPrefetch", 0);
			MessagePrefetchConfiguration.prefetchOnlyUnreadMessages = AppSettings.GetConfiguredValue<bool>("PrefetchOnlyUnreadMessages", false);
			MessagePrefetchConfiguration.maxMessagesInCache = AppSettings.GetConfiguredValue<int>("MaxMessagesInCache", 500);
			MessagePrefetchConfiguration.maxCacheSizeInMegaBytes = AppSettings.GetConfiguredValue<int>("MaxCacheSizeInMegaBytes", 30);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00054986 File Offset: 0x00052B86
		public static bool IsMessagePrefetchEnabled(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return !userContext.IsWebPartRequest && MessagePrefetchConfiguration.isMessagePrefetchEnabled;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000549A8 File Offset: 0x00052BA8
		internal static bool IsMessagePrefetchEnabledForSession(UserContext userContext, StoreSession session)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return MessagePrefetchConfiguration.IsMessagePrefetchEnabled(userContext) && ServerVersion.IsE14SP1OrGreater(userContext.MailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion) && !(session is PublicFolderSession);
		}

		// Token: 0x040008A2 RID: 2210
		private static bool isMessagePrefetchEnabled;

		// Token: 0x040008A3 RID: 2211
		private static int numberOfMessagesToPrefetch;

		// Token: 0x040008A4 RID: 2212
		private static bool prefetchOnlyUnreadMessages;

		// Token: 0x040008A5 RID: 2213
		private static int maxMessagesInCache;

		// Token: 0x040008A6 RID: 2214
		private static int maxCacheSizeInMegaBytes;
	}
}
