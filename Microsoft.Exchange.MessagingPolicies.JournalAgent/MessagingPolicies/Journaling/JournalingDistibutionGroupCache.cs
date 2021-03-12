using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000016 RID: 22
	internal sealed class JournalingDistibutionGroupCache : LazyLookupTimeoutCache<string, JournalingDistributionGroupCacheItem>
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00009730 File Offset: 0x00007930
		protected override JournalingDistributionGroupCacheItem CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			if (this.adRecipientCache == null)
			{
				throw new ArgumentNullException("adRecipientCache");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IADDistributionList group = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(key, true);
				group = (this.adRecipientCache.ADSession.FindByProxyAddress(proxyAddress) as IADDistributionList);
			});
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<Exception>((long)this.GetHashCode(), "invalid group object {0}", adoperationResult.Exception);
				shouldAdd = false;
				return null;
			}
			List<string> list = new List<string>();
			foreach (TransportMiniRecipient transportMiniRecipient in this.adRecipientCache.ExpandGroup<TransportMiniRecipient>(group))
			{
				list.Add(transportMiniRecipient[ADRecipientSchema.PrimarySmtpAddress].ToString());
			}
			return new JournalingDistributionGroupCacheItem(list);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00009824 File Offset: 0x00007A24
		public JournalingDistibutionGroupCache() : base(1, 100, false, TimeSpan.FromMinutes(20.0))
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000984C File Offset: 0x00007A4C
		public JournalingDistributionGroupCacheItem GetGroupCacheItem(IADRecipientCache recipientCache, string emailAddress)
		{
			JournalingDistributionGroupCacheItem result;
			lock (this.lockObj)
			{
				this.adRecipientCache = (IADRecipientCache<TransportMiniRecipient>)recipientCache;
				JournalingDistributionGroupCacheItem journalingDistributionGroupCacheItem = base.Get(emailAddress);
				this.adRecipientCache = null;
				result = journalingDistributionGroupCacheItem;
			}
			return result;
		}

		// Token: 0x04000088 RID: 136
		private IADRecipientCache<TransportMiniRecipient> adRecipientCache;

		// Token: 0x04000089 RID: 137
		private object lockObj = new object();
	}
}
