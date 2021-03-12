using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200023B RID: 571
	internal sealed class RoomsCache : RecipientCache
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x000775FB File Offset: 0x000757FB
		private RoomsCache(UserContext userContext, UserConfiguration configuration) : base(userContext, 15, configuration)
		{
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00077608 File Offset: 0x00075808
		public static void FinishSession(UserContext userContext)
		{
			if (userContext.IsRoomsCacheSessionStarted)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.RoomsCache", userContext))
				{
					RoomsCache roomsCache = RoomsCache.TryGetCache(userContext);
					if (roomsCache != null)
					{
						roomsCache.FinishSession(new RoomsCache(userContext, recipientCacheTransaction.Configuration), recipientCacheTransaction.Configuration);
					}
				}
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00077668 File Offset: 0x00075868
		private static RoomsCache GetCache(UserContext userContext, bool useInMemCache)
		{
			if (userContext.RoomsCache == null || !useInMemCache)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.RoomsCache", userContext))
				{
					userContext.RoomsCache = RoomsCache.Load(userContext, recipientCacheTransaction.Configuration);
					if (!userContext.IsRoomsCacheSessionStarted)
					{
						userContext.RoomsCache.StartCacheSession();
						userContext.RoomsCache.Commit(recipientCacheTransaction.Configuration);
						userContext.IsRoomsCacheSessionStarted = true;
					}
				}
			}
			return userContext.RoomsCache;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000776EC File Offset: 0x000758EC
		public static RoomsCache TryGetCache(UserContext userContext)
		{
			return RoomsCache.TryGetCache(userContext, true);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000776F8 File Offset: 0x000758F8
		public static RoomsCache TryGetCache(UserContext userContext, bool useInMemCache)
		{
			try
			{
				return RoomsCache.GetCache(userContext, useInMemCache);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<string, string>(0L, "Failed to get rooms cache from server. Error: {0}. Stack: {1}", ex.Message, ex.StackTrace);
			}
			return null;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00077744 File Offset: 0x00075944
		public override void Commit(bool mergeBeforeCommit)
		{
			if (base.IsDirty)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.RoomsCache", base.UserContext))
				{
					RecipientCache backEndRecipientCache = null;
					if (mergeBeforeCommit)
					{
						backEndRecipientCache = new RoomsCache(base.UserContext, recipientCacheTransaction.Configuration);
					}
					this.Commit(backEndRecipientCache, recipientCacheTransaction.Configuration);
				}
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000777AC File Offset: 0x000759AC
		private static RoomsCache Load(UserContext userContext, UserConfiguration configuration)
		{
			return new RoomsCache(userContext, configuration);
		}

		// Token: 0x04000D3D RID: 3389
		public const string ConfigurationName = "OWA.RoomsCache";

		// Token: 0x04000D3E RID: 3390
		private const short Size = 15;
	}
}
