using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000247 RID: 583
	internal sealed class SendFromCache : RecipientCache
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x00078798 File Offset: 0x00076998
		private SendFromCache(UserContext userContext, UserConfiguration configuration) : base(userContext, 10, configuration)
		{
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000787A4 File Offset: 0x000769A4
		public static void FinishSession(UserContext userContext)
		{
			if (userContext.IsSendFromCacheSessionStarted)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.SendFromCache", userContext))
				{
					SendFromCache sendFromCache = SendFromCache.TryGetCache(userContext);
					if (sendFromCache != null)
					{
						sendFromCache.FinishSession(new SendFromCache(userContext, recipientCacheTransaction.Configuration), recipientCacheTransaction.Configuration);
					}
				}
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00078804 File Offset: 0x00076A04
		public static SendFromCache TryGetCache(UserContext userContext)
		{
			return SendFromCache.TryGetCache(userContext, true);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00078810 File Offset: 0x00076A10
		public static SendFromCache TryGetCache(UserContext userContext, bool useInMemCache)
		{
			try
			{
				return SendFromCache.GetCache(userContext, useInMemCache);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<string, string>(0L, "Failed to get send from cache from server. Error: {0}. Stack: {1}", ex.Message, ex.StackTrace);
			}
			return null;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0007885C File Offset: 0x00076A5C
		private static SendFromCache GetCache(UserContext userContext, bool useInMemCache)
		{
			if (userContext.SendFromCache == null || !useInMemCache)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.SendFromCache", userContext))
				{
					userContext.SendFromCache = SendFromCache.Load(userContext, recipientCacheTransaction.Configuration);
					if (!userContext.IsSendFromCacheSessionStarted)
					{
						userContext.SendFromCache.StartCacheSession();
						userContext.SendFromCache.Commit(recipientCacheTransaction.Configuration);
						userContext.IsSendFromCacheSessionStarted = true;
					}
				}
			}
			return userContext.SendFromCache;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000788E0 File Offset: 0x00076AE0
		public void RenderToJavascript(TextWriter writer)
		{
			for (int i = 0; i < base.CacheLength; i++)
			{
				if (i > 0)
				{
					writer.Write(",");
				}
				AutoCompleteCacheEntry.RenderEntryJavascript(writer, base.CacheEntries[i]);
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0007891F File Offset: 0x00076B1F
		private static SendFromCache Load(UserContext userContext, UserConfiguration configuration)
		{
			return new SendFromCache(userContext, configuration);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00078928 File Offset: 0x00076B28
		public override void Commit(bool mergeBeforeCommit)
		{
			if (base.IsDirty)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.SendFromCache", base.UserContext))
				{
					RecipientCache backEndRecipientCache = null;
					if (mergeBeforeCommit)
					{
						backEndRecipientCache = new SendFromCache(base.UserContext, recipientCacheTransaction.Configuration);
					}
					this.Commit(backEndRecipientCache, recipientCacheTransaction.Configuration);
				}
			}
		}

		// Token: 0x04000D7D RID: 3453
		public const string ConfigurationName = "OWA.SendFromCache";

		// Token: 0x04000D7E RID: 3454
		private const short Size = 10;
	}
}
