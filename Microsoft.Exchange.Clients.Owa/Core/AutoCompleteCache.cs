using System;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000D3 RID: 211
	internal sealed class AutoCompleteCache : RecipientCache
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00038E5E File Offset: 0x0003705E
		private AutoCompleteCache(UserContext userContext, UserConfiguration configuration) : base(userContext, 100, configuration)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00038E6A File Offset: 0x0003706A
		private static AutoCompleteCache Load(UserContext userContext, UserConfiguration configuration)
		{
			return new AutoCompleteCache(userContext, configuration);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00038E74 File Offset: 0x00037074
		public void RemoveResolvedRecipients(ResolvedRecipientDetail[] resolvedRecipientDetails)
		{
			foreach (ResolvedRecipientDetail resolvedRecipientDetail in resolvedRecipientDetails)
			{
				base.RemoveEntry(resolvedRecipientDetail.RoutingAddress, resolvedRecipientDetail.ItemId);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00038EA8 File Offset: 0x000370A8
		public static void FinishSession(UserContext userContext)
		{
			using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.AutocompleteCache", userContext))
			{
				AutoCompleteCache autoCompleteCache = AutoCompleteCache.TryGetCache(userContext);
				if (autoCompleteCache != null)
				{
					autoCompleteCache.FinishSession(new AutoCompleteCache(userContext, recipientCacheTransaction.Configuration), recipientCacheTransaction.Configuration);
				}
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00038F00 File Offset: 0x00037100
		private static AutoCompleteCache GetCache(UserContext userContext, bool useInMemCache)
		{
			if (userContext.AutoCompleteCache == null || !useInMemCache)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.AutocompleteCache", userContext))
				{
					userContext.AutoCompleteCache = AutoCompleteCache.Load(userContext, recipientCacheTransaction.Configuration);
					if (!userContext.IsAutoCompleteSessionStarted)
					{
						userContext.AutoCompleteCache.StartCacheSession();
						userContext.IsAutoCompleteSessionStarted = true;
					}
				}
			}
			return userContext.AutoCompleteCache;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00038F74 File Offset: 0x00037174
		public static AutoCompleteCache TryGetCache(UserContext userContext)
		{
			return AutoCompleteCache.TryGetCache(userContext, true);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00038F80 File Offset: 0x00037180
		public static AutoCompleteCache TryGetCache(UserContext userContext, bool useInMemCache)
		{
			try
			{
				return AutoCompleteCache.GetCache(userContext, useInMemCache);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<string, string>(0L, "Failed to get autocomplete cache from server. Error: {0}. Stack: {1}", ex.Message, ex.StackTrace);
			}
			return null;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00038FCC File Offset: 0x000371CC
		public static void UpdateAutoCompleteCacheFromRecipientInfoList(RecipientInfoAC[] entries, UserContext userContext)
		{
			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			AutoCompleteCache autoCompleteCache = AutoCompleteCache.TryGetCache(userContext);
			if (autoCompleteCache != null)
			{
				for (int i = 0; i < entries.Length; i++)
				{
					RecipientInfoCacheEntry recipientInfoCacheEntry = AutoCompleteCacheEntry.ParseClientEntry(entries[i]);
					if (recipientInfoCacheEntry != null)
					{
						autoCompleteCache.AddEntry(recipientInfoCacheEntry);
					}
				}
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00039020 File Offset: 0x00037220
		public override void AddEntry(RecipientInfoCacheEntry newEntry)
		{
			base.AddEntry(newEntry);
			if (0 < (newEntry.RecipientFlags & 2))
			{
				RecipientCache recipientCache = RoomsCache.TryGetCache(base.UserContext);
				if (recipientCache != null)
				{
					recipientCache.AddEntry(newEntry);
				}
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00039058 File Offset: 0x00037258
		public override void Commit(bool mergeBeforeCommit)
		{
			if (base.IsDirty)
			{
				using (RecipientCacheTransaction recipientCacheTransaction = new RecipientCacheTransaction("OWA.AutocompleteCache", base.UserContext))
				{
					AutoCompleteCache backEndRecipientCache = null;
					if (mergeBeforeCommit)
					{
						backEndRecipientCache = new AutoCompleteCache(base.UserContext, recipientCacheTransaction.Configuration);
					}
					this.Commit(backEndRecipientCache, recipientCacheTransaction.Configuration);
				}
			}
			if (base.UserContext.RoomsCache != null && base.UserContext.RoomsCache.IsDirty)
			{
				base.UserContext.RoomsCache.Commit(true);
			}
		}

		// Token: 0x04000511 RID: 1297
		public const string ConfigurationName = "OWA.AutocompleteCache";

		// Token: 0x04000512 RID: 1298
		public const short CacheSize = 100;
	}
}
