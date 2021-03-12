using System;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200000D RID: 13
	internal class ClusterBin
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00009208 File Offset: 0x00007408
		public ClusterBin()
		{
			this.currentBinData = new TimePeriodBigMap<ulong, ClusterBucket>();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000921C File Offset: 0x0000741C
		public ClusterBin(ClusterContext clusterContext)
		{
			this.clusterContext = clusterContext;
			int internalStoreNumber = clusterContext.InternalStoreNumber;
			TimeSpan swapTime = new TimeSpan(0, clusterContext.SwapTimeInMinutes, 0);
			TimeSpan mergeTime = new TimeSpan(0, clusterContext.MergeTimeInMinutes, 0);
			TimeSpan cleanTime = new TimeSpan(0, clusterContext.CleanTimeInMinutes, 0);
			this.currentBinData = new TimePeriodBigMap<ulong, ClusterBucket>(internalStoreNumber, swapTime, mergeTime, cleanTime, 71993);
			if (clusterContext.UseBloomFilter)
			{
				this.NewBloomFilter(clusterContext.PowerIndexOf2, clusterContext.MaxCountValue, clusterContext.HashNumbers);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000929E File Offset: 0x0000749E
		internal TimePeriodBigMap<ulong, ClusterBucket> CurrentBinData
		{
			get
			{
				return this.currentBinData;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000092A6 File Offset: 0x000074A6
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000092AE File Offset: 0x000074AE
		internal ClusterContext ClusterContext
		{
			get
			{
				return this.clusterContext;
			}
			set
			{
				this.clusterContext = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000092B8 File Offset: 0x000074B8
		public void NewBloomFilter(int powerIndex, int maxBitsValue, int hashNumbers)
		{
			CountingBloomFilter<Bits4> value = new CountingBloomFilter<Bits4>(powerIndex, maxBitsValue, hashNumbers);
			Interlocked.CompareExchange<CountingBloomFilter<Bits4>>(ref this.bloomFilter, value, this.bloomFilter);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000092E4 File Offset: 0x000074E4
		public void ProcessOneFeed(MailInfo mailInfo, out ClusterResult result)
		{
			result = null;
			if (mailInfo == null)
			{
				throw new ArgumentNullException("mailInfo");
			}
			if (mailInfo.RecipientsDomainHash == null || mailInfo.RecipientsDomainHash.Length == 0)
			{
				throw new ArgumentException("emailDirection is null or empty.");
			}
			if (mailInfo.RecipientsDomainHash.Length > this.clusterContext.MaxHashSetSize || mailInfo.RecipientsDomainHash.Length <= 0)
			{
				throw new ArgumentException("recipientsDomainHash is out of range.");
			}
			if (!this.Filtering(mailInfo))
			{
				return;
			}
			ulong num = mailInfo.Key;
			ClusterBucket bucket = this.GetBucket(mailInfo.DocumentTime, mailInfo.Key);
			for (int i = 0; i < 4; i++)
			{
				if (bucket.Clusteroid == null)
				{
					bucket.Clusteroid = mailInfo.Fingerprint;
					bucket.Add(mailInfo, this.ClusterContext, out result);
					return;
				}
				int num2;
				int num3;
				LshFingerprint.ComputeSimilarity(bucket.Clusteroid, mailInfo.Fingerprint, out num2, out num3, false, false);
				if (num2 >= this.clusterContext.LowSimilarityBoundInt)
				{
					bucket.Add(mailInfo, this.ClusterContext, out result);
					return;
				}
				num += 1UL;
				bucket = this.GetBucket(mailInfo.DocumentTime, num);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000093E4 File Offset: 0x000075E4
		internal ClusterBucket GetBucket(DateTime dateTime, ulong key)
		{
			return this.currentBinData.GetValue(dateTime, key);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000093F4 File Offset: 0x000075F4
		private bool Filtering(MailInfo mailInfo)
		{
			if (this.bloomFilter != null)
			{
				bool flag = this.bloomFilter.Add(FnvHash.Fnv1A64(BitConverter.GetBytes(mailInfo.Key)), mailInfo.RecipientNumber);
				return this.IsSpamFeed(mailInfo) || flag;
			}
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00009439 File Offset: 0x00007639
		private bool IsSpamFeed(MailInfo mailInfo)
		{
			return mailInfo.FnFeed || mailInfo.SenFeed || mailInfo.HoneypotFeed || mailInfo.ThirdPartyFeed || mailInfo.SewrFeed;
		}

		// Token: 0x0400002F RID: 47
		private readonly TimePeriodBigMap<ulong, ClusterBucket> currentBinData;

		// Token: 0x04000030 RID: 48
		private CountingBloomFilter<Bits4> bloomFilter;

		// Token: 0x04000031 RID: 49
		private ClusterContext clusterContext;
	}
}
