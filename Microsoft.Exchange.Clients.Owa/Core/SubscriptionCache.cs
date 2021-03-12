using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000257 RID: 599
	internal sealed class SubscriptionCache
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0007AFC6 File Offset: 0x000791C6
		public SubscriptionCache()
		{
			this.cacheEntries = new List<SubscriptionCacheEntry>(128);
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0007AFDE File Offset: 0x000791DE
		public int CacheLength
		{
			get
			{
				return this.cacheEntries.Count;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0007AFEB File Offset: 0x000791EB
		internal List<SubscriptionCacheEntry> CacheEntries
		{
			get
			{
				return this.cacheEntries;
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0007AFF3 File Offset: 0x000791F3
		public static SubscriptionCache GetCache(UserContext userContext)
		{
			return SubscriptionCache.GetCache(userContext, true);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0007AFFC File Offset: 0x000791FC
		public static SubscriptionCache GetCache(UserContext userContext, bool useInMemCache)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			lock (SubscriptionCache.syncRoot)
			{
				if (!userContext.CanActAsOwner)
				{
					userContext.SubscriptionCache = new SubscriptionCache();
				}
				else if (userContext.SubscriptionCache == null || !useInMemCache)
				{
					List<PimAggregationSubscription> allSendAsSubscriptions = SubscriptionManager.GetAllSendAsSubscriptions(userContext.MailboxSession);
					userContext.SubscriptionCache = new SubscriptionCache();
					short num = 1;
					foreach (PimAggregationSubscription pimAggregationSubscription in allSendAsSubscriptions)
					{
						string address = Utilities.DecodeIDNDomain(pimAggregationSubscription.UserEmailAddress);
						if (128 <= num)
						{
							break;
						}
						SubscriptionCacheEntry item = new SubscriptionCacheEntry(pimAggregationSubscription.SubscriptionGuid, address, pimAggregationSubscription.UserDisplayName, pimAggregationSubscription.InstanceKey, userContext.MailboxSession.PreferedCulture);
						userContext.SubscriptionCache.cacheEntries.Add(item);
						num += 1;
					}
					userContext.SubscriptionCache.cacheEntries.Sort();
				}
			}
			return userContext.SubscriptionCache;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0007B124 File Offset: 0x00079324
		public void RenderToJavascript(TextWriter writer)
		{
			lock (SubscriptionCache.syncRoot)
			{
				for (int i = 0; i < this.cacheEntries.Count; i++)
				{
					if (i > 0)
					{
						writer.Write(",");
					}
					this.cacheEntries[i].RenderToJavascript(writer);
				}
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0007B198 File Offset: 0x00079398
		public int Add(SubscriptionCacheEntry entry)
		{
			if (128 == this.cacheEntries.Count)
			{
				return -1;
			}
			lock (SubscriptionCache.syncRoot)
			{
				for (int i = 0; i < this.cacheEntries.Count; i++)
				{
					int num = this.cacheEntries[i].CompareTo(entry);
					if (num == 0)
					{
						this.cacheEntries[i] = entry;
						return i;
					}
					if (0 < num)
					{
						this.cacheEntries.Insert(i, entry);
						return i;
					}
				}
				this.cacheEntries.Add(entry);
			}
			return this.cacheEntries.Count - 1;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0007B3F4 File Offset: 0x000795F4
		public int Modify(SubscriptionCacheEntry entry, SendAsState sendAsState, AggregationStatus status)
		{
			int index = -1;
			this.ForFirstEntryByIdThenAddress(entry, delegate(int i)
			{
				if (!SubscriptionManager.IsValidForSendAs(sendAsState, status))
				{
					this.cacheEntries.RemoveAt(i);
					index = i;
					return;
				}
				if (this.cacheEntries[i].Id.Equals(entry.Id))
				{
					int num = this.cacheEntries[i].CompareTo(entry);
					if (num == 0 && entry.DisplayNameMatch(this.cacheEntries[i]))
					{
						index = -2;
						return;
					}
					if (0 < num)
					{
						while (0 < i)
						{
							if (0 >= this.cacheEntries[i - 1].CompareTo(entry))
							{
								break;
							}
							this.cacheEntries[i] = this.cacheEntries[--i];
						}
					}
					else if (0 > num)
					{
						while (this.cacheEntries.Count - 1 > i && 0 > this.cacheEntries[i + 1].CompareTo(entry))
						{
							this.cacheEntries[i] = this.cacheEntries[++i];
						}
					}
				}
				this.cacheEntries[i] = entry;
				index = i;
			});
			if (-1 == index && SubscriptionManager.IsValidForSendAs(sendAsState, status))
			{
				index = this.Add(entry);
			}
			return index;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0007B478 File Offset: 0x00079678
		public int Delete(byte[] instanceKey)
		{
			lock (SubscriptionCache.syncRoot)
			{
				for (int i = 0; i < this.cacheEntries.Count; i++)
				{
					if (this.cacheEntries[i].MatchOnInstanceKey(instanceKey))
					{
						this.cacheEntries.RemoveAt(i);
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0007B4F4 File Offset: 0x000796F4
		public bool TryGetEntry(Participant fromParticipant, out SubscriptionCacheEntry subscriptionCacheEntry)
		{
			SubscriptionCacheEntry subscriptionCacheEntry2 = new SubscriptionCacheEntry(Guid.Empty, fromParticipant.EmailAddress, fromParticipant.DisplayName, null, CultureInfo.InvariantCulture);
			lock (SubscriptionCache.syncRoot)
			{
				foreach (SubscriptionCacheEntry subscriptionCacheEntry3 in this.cacheEntries)
				{
					if (subscriptionCacheEntry3.CompareTo(subscriptionCacheEntry2) == 0)
					{
						subscriptionCacheEntry = subscriptionCacheEntry3;
						return true;
					}
				}
			}
			subscriptionCacheEntry = subscriptionCacheEntry2;
			return false;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0007B5A8 File Offset: 0x000797A8
		public bool TryGetEntry(Guid subscriptionId, out SubscriptionCacheEntry subscriptionCacheEntry)
		{
			lock (SubscriptionCache.syncRoot)
			{
				foreach (SubscriptionCacheEntry subscriptionCacheEntry2 in this.cacheEntries)
				{
					if (subscriptionId.Equals(subscriptionCacheEntry2.Id))
					{
						subscriptionCacheEntry = subscriptionCacheEntry2;
						return true;
					}
				}
			}
			subscriptionCacheEntry = new SubscriptionCacheEntry(Guid.Empty, string.Empty, string.Empty, null, CultureInfo.InvariantCulture);
			return false;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0007B65C File Offset: 0x0007985C
		public bool TryGetSendAsDefaultEntry(SendAddressDefaultSetting sendAddressDefaultSetting, out SubscriptionCacheEntry subscriptionCacheEntry)
		{
			subscriptionCacheEntry = new SubscriptionCacheEntry(Guid.Empty, string.Empty, string.Empty, null, CultureInfo.InvariantCulture);
			Guid subscriptionId;
			return sendAddressDefaultSetting.TryGetSubscriptionSendAddressId(out subscriptionId) && this.TryGetEntry(subscriptionId, out subscriptionCacheEntry);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0007B6A0 File Offset: 0x000798A0
		private int ForFirstEntryByIdThenAddress(SubscriptionCacheEntry entry, SubscriptionCache.CacheEntryProcessor processor)
		{
			lock (SubscriptionCache.syncRoot)
			{
				for (int i = 0; i < this.cacheEntries.Count; i++)
				{
					if (this.cacheEntries[i].Id.Equals(entry.Id))
					{
						processor(i);
						return i;
					}
				}
				for (int j = 0; j < this.cacheEntries.Count; j++)
				{
					int num = this.cacheEntries[j].CompareTo(entry);
					if (num == 0)
					{
						processor(j);
						return j;
					}
					if (0 < num)
					{
						break;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000DCB RID: 3531
		private const short Size = 128;

		// Token: 0x04000DCC RID: 3532
		private static object syncRoot = new object();

		// Token: 0x04000DCD RID: 3533
		private List<SubscriptionCacheEntry> cacheEntries;

		// Token: 0x02000258 RID: 600
		// (Invoke) Token: 0x06001426 RID: 5158
		private delegate void CacheEntryProcessor(int i);
	}
}
