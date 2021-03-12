using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.QueueQuota
{
	// Token: 0x02000349 RID: 841
	internal class OrganizationUsageData : UsageData, IEnumerable<KeyValuePair<string, UsageData>>, IEnumerable
	{
		// Token: 0x06002440 RID: 9280 RVA: 0x00089EB9 File Offset: 0x000880B9
		public OrganizationUsageData(TimeSpan historyInterval, TimeSpan historyBucketLength) : base(historyInterval, historyBucketLength)
		{
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00089ED3 File Offset: 0x000880D3
		public OrganizationUsageData(Guid key, TimeSpan historyInterval, TimeSpan historyBucketLength, Func<DateTime> currentTimeProvider) : base(historyInterval, historyBucketLength, currentTimeProvider)
		{
			this.key = key;
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00089EF6 File Offset: 0x000880F6
		public ConcurrentDictionary<string, UsageData> SenderQuotaDictionary
		{
			get
			{
				return this.senderQuotaDictionary;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00089F00 File Offset: 0x00088100
		public override void Merge(UsageData source)
		{
			base.Merge(source);
			foreach (KeyValuePair<string, UsageData> keyValuePair in ((OrganizationUsageData)source).SenderQuotaDictionary)
			{
				UsageData.AddOrMerge<string, UsageData>(this.SenderQuotaDictionary, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0008A0E4 File Offset: 0x000882E4
		public IEnumerator<KeyValuePair<string, UsageData>> GetEnumerator()
		{
			yield return new KeyValuePair<string, UsageData>(this.key.ToString(), this);
			foreach (KeyValuePair<string, UsageData> pair in this.senderQuotaDictionary)
			{
				yield return pair;
			}
			yield break;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x0008A100 File Offset: 0x00088300
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040012CE RID: 4814
		private readonly ConcurrentDictionary<string, UsageData> senderQuotaDictionary = new ConcurrentDictionary<string, UsageData>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040012CF RID: 4815
		private readonly Guid key;
	}
}
