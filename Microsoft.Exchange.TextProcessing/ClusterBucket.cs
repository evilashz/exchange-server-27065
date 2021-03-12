using System;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200000F RID: 15
	internal class ClusterBucket : IInitialize
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00009463 File Offset: 0x00007663
		public ClusterBucket()
		{
			this.Initialize();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00009471 File Offset: 0x00007671
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00009479 File Offset: 0x00007679
		public DateTime LastRefreshTime { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00009482 File Offset: 0x00007682
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000948A File Offset: 0x0000768A
		public int SizeIncoming
		{
			get
			{
				return this.sizeIncoming;
			}
			set
			{
				this.sizeIncoming = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00009493 File Offset: 0x00007693
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000949B File Offset: 0x0000769B
		public int SizeOutgoing
		{
			get
			{
				return this.sizeOutgoing;
			}
			set
			{
				this.sizeOutgoing = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000094A4 File Offset: 0x000076A4
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000094AC File Offset: 0x000076AC
		public ActionEnum ActionMode { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000094B5 File Offset: 0x000076B5
		public ClusteringStatusEnum Status
		{
			get
			{
				return (ClusteringStatusEnum)this.status;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000094BD File Offset: 0x000076BD
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000094C5 File Offset: 0x000076C5
		public SmallCounterMap SenderDomains { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000094CE File Offset: 0x000076CE
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000094D6 File Offset: 0x000076D6
		public SmallCounterMap Senders { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000094DF File Offset: 0x000076DF
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000094E7 File Offset: 0x000076E7
		public SmallCounterMap RecipientDomains { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000094F0 File Offset: 0x000076F0
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000094F8 File Offset: 0x000076F8
		public SmallCounterMap Subjects { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00009501 File Offset: 0x00007701
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00009509 File Offset: 0x00007709
		public SmallCounterMap ClientIps { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00009512 File Offset: 0x00007712
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000951A File Offset: 0x0000771A
		public SmallCounterMap ClientIp24s { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00009523 File Offset: 0x00007723
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000952B File Offset: 0x0000772B
		public int SenCount
		{
			get
			{
				return this.senCount;
			}
			private set
			{
				this.senCount = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00009534 File Offset: 0x00007734
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000953C File Offset: 0x0000773C
		public int HoneypotCount
		{
			get
			{
				return this.honeypotCount;
			}
			private set
			{
				this.honeypotCount = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00009545 File Offset: 0x00007745
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000954D File Offset: 0x0000774D
		public int FnCount
		{
			get
			{
				return this.fnCount;
			}
			private set
			{
				this.fnCount = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00009556 File Offset: 0x00007756
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000955E File Offset: 0x0000775E
		public int ThirdPartyCount
		{
			get
			{
				return this.thirdPartyCount;
			}
			private set
			{
				this.thirdPartyCount = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00009567 File Offset: 0x00007767
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000956F File Offset: 0x0000776F
		public int SewrCount
		{
			get
			{
				return this.sewrCount;
			}
			private set
			{
				this.sewrCount = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00009578 File Offset: 0x00007778
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00009580 File Offset: 0x00007780
		public int SpamCount
		{
			get
			{
				return this.spamCount;
			}
			private set
			{
				this.spamCount = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00009589 File Offset: 0x00007789
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00009591 File Offset: 0x00007791
		public LshFingerprint Clusteroid
		{
			get
			{
				return this.clusteroid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.clusteroid = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000095A8 File Offset: 0x000077A8
		public void Initialize()
		{
			this.LastRefreshTime = DateTime.UtcNow;
			this.clusteroid = null;
			this.ActionMode = ActionEnum.BelowThreshold;
			this.SenderDomains = new SmallCounterMap();
			this.Senders = new SmallCounterMap();
			this.RecipientDomains = new SmallCounterMap();
			this.Subjects = new SmallCounterMap();
			this.ClientIps = new SmallCounterMap();
			this.ClientIp24s = new SmallCounterMap();
			this.SenCount = 0;
			this.HoneypotCount = 0;
			this.FnCount = 0;
			this.ThirdPartyCount = 0;
			this.SizeIncoming = 0;
			this.SizeOutgoing = 0;
			this.status = 0;
			this.sewrCount = 0;
			this.spamCount = 0;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000964F File Offset: 0x0000784F
		public int BucketSize(DirectionEnum direction)
		{
			if (direction == DirectionEnum.Incoming)
			{
				return this.sizeIncoming;
			}
			return this.sizeOutgoing;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00009664 File Offset: 0x00007864
		public void Add(MailInfo mailInfo, ClusterContext clusterContext, out ClusterResult result)
		{
			if (mailInfo == null)
			{
				throw new ArgumentNullException("mailInfo");
			}
			if (mailInfo.RecipientsDomainHash == null || mailInfo.RecipientsDomainHash.Length == 0)
			{
				throw new ArgumentException("emailDirection is null or empty.");
			}
			if (clusterContext == null)
			{
				throw new ArgumentNullException("clusterContext");
			}
			this.IncrementAttributeCounter(mailInfo);
			this.IncrementFeedCounter(mailInfo);
			this.IncrementClusterSize(mailInfo);
			int[] propertyValues;
			this.Summarize(mailInfo, clusterContext, out propertyValues);
			result = new ClusterResult();
			this.SetClusterResult(mailInfo, clusterContext, propertyValues, result);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000096DC File Offset: 0x000078DC
		internal ClusteringStatusEnum CaculateSourceStatus(int[] propertyValues, ClusterContext clusterContext)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (clusterContext == null)
			{
				throw new ArgumentNullException("clusterContext");
			}
			ClusteringStatusEnum clusteringStatusEnum = this.SourceType(propertyValues[0], clusterContext);
			ClusteringStatusEnum clusteringStatusEnum2 = this.SourceType(propertyValues[1], clusterContext);
			ClusteringStatusEnum clusteringStatusEnum3 = this.SourceType(propertyValues[2], clusterContext);
			ClusteringStatusEnum clusteringStatusEnum4 = this.SourceType(propertyValues[4], clusterContext);
			ClusteringStatusEnum clusteringStatusEnum5 = this.SourceType(propertyValues[5], clusterContext);
			ClusteringStatusEnum clusteringStatusEnum6 = clusteringStatusEnum | clusteringStatusEnum2 | clusteringStatusEnum3 | clusteringStatusEnum4 | clusteringStatusEnum5;
			if ((clusteringStatusEnum6 & ClusteringStatusEnum.SourceMask) == ClusteringStatusEnum.OneSource)
			{
				return ClusteringStatusEnum.OneSource;
			}
			if ((clusteringStatusEnum6 & ClusteringStatusEnum.SourceMask) == ClusteringStatusEnum.MultiSource)
			{
				return ClusteringStatusEnum.MultiSource;
			}
			if ((clusteringStatusEnum6 & ClusteringStatusEnum.SourceMask) == (ClusteringStatusEnum.OneSource | ClusteringStatusEnum.MultiSource))
			{
				return ClusteringStatusEnum.OneAndMultiSource;
			}
			return ClusteringStatusEnum.UkOneOrMultiSource;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00009768 File Offset: 0x00007968
		internal ClusteringStatusEnum CalculateSpamFeedStatus(int[] propertyValues, ClusterContext clusterContext)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (clusterContext == null)
			{
				throw new ArgumentNullException("clusterContext");
			}
			ClusteringStatusEnum clusteringStatusEnum = ClusteringStatusEnum.None;
			if (propertyValues[9] > clusterContext.SenFeedSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.SenFeed;
			}
			if (propertyValues[7] > clusterContext.FnFeedSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.FnFeed;
			}
			if (propertyValues[8] > clusterContext.HoneypotFeedSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.HoneypotFeed;
			}
			if (propertyValues[10] > clusterContext.ThirdPartyFeedSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.ThirdPartyFeed;
			}
			if (propertyValues[11] > clusterContext.SewrFeedSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.SewrFeed;
			}
			if (propertyValues[12] > clusterContext.SpamSizeAbove)
			{
				clusteringStatusEnum |= ClusteringStatusEnum.SpamVerdict;
			}
			return clusteringStatusEnum;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000980C File Offset: 0x00007A0C
		private ActionEnum ResolveActionMode(DirectionEnum emailDirection, int[] propertyValues, ClusterContext clusterContext)
		{
			ClusteringStatusEnum clusteringStatusEnum = this.Status;
			if ((clusteringStatusEnum & ClusteringStatusEnum.MultiSource) != ClusteringStatusEnum.MultiSource)
			{
				clusteringStatusEnum |= this.CaculateSourceStatus(propertyValues, clusterContext);
			}
			clusteringStatusEnum |= this.CalculateSpamFeedStatus(propertyValues, clusterContext);
			ClusteringStatusEnum clusteringStatusEnum2 = (ClusteringStatusEnum)Interlocked.CompareExchange(ref this.status, this.status | (int)clusteringStatusEnum, this.status);
			int num = this.BucketSize(emailDirection);
			if (propertyValues[3] <= clusterContext.NumberOfRecipientDomainAbove)
			{
				this.ActionMode = ActionEnum.BelowThreshold;
				return ActionEnum.BelowThreshold;
			}
			if (this.ActionMode == ActionEnum.BelowThreshold)
			{
				bool flag = ((this.Status & ClusteringStatusEnum.SpamFeedMask) != ClusteringStatusEnum.None && num > clusterContext.SpamFeedClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.SpamVerdict) != ClusteringStatusEnum.None && num > clusterContext.SpamVerdictFeedClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.OneSource) == ClusteringStatusEnum.OneSource && num > clusterContext.AllOneSourceClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.OneAndMultiSource) == ClusteringStatusEnum.OneAndMultiSource && num > clusterContext.OneAndMultiSourceClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.MultiSource) == ClusteringStatusEnum.MultiSource && num > clusterContext.AllMultiSourceClusterSizeAbove);
				if (flag)
				{
					this.ActionMode = ActionEnum.ReachedThreshold;
					return ActionEnum.ReachedThreshold;
				}
				return ActionEnum.BelowThreshold;
			}
			else
			{
				if (this.ActionMode == ActionEnum.ReachedThreshold)
				{
					this.ActionMode = ActionEnum.OverThreshold;
					return ActionEnum.OverThreshold;
				}
				if (this.ActionMode == ActionEnum.OverThreshold && (clusteringStatusEnum & ClusteringStatusEnum.SpamFeedMask) != ClusteringStatusEnum.None && (clusteringStatusEnum2 & ClusteringStatusEnum.SpamFeedMask) == ClusteringStatusEnum.None)
				{
					this.ActionMode = ActionEnum.ReachedThreshold;
					return ActionEnum.ReachedThreshold;
				}
				return this.ActionMode;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00009938 File Offset: 0x00007B38
		private void Summarize(MailInfo mailInfo, ClusterContext clusterContext, out int[] propertyValues)
		{
			propertyValues = new int[ClusterBucket.StatusEnumLength];
			propertyValues[0] = this.Subjects.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[1] = this.SenderDomains.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[2] = this.Senders.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[3] = this.RecipientDomains.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[4] = this.ClientIps.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[5] = this.ClientIp24s.NumberOfMajorSource(clusterContext.NearOneSourcePercentageAbove);
			propertyValues[6] = this.BucketSize(mailInfo.EmailDirection);
			propertyValues[7] = this.fnCount;
			propertyValues[8] = this.honeypotCount;
			propertyValues[9] = this.senCount;
			propertyValues[10] = this.thirdPartyCount;
			propertyValues[11] = this.sewrCount;
			propertyValues[12] = this.spamCount;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00009A1F File Offset: 0x00007C1F
		private ClusteringStatusEnum SourceType(int sourceCount, ClusterContext clusterContext)
		{
			if (sourceCount == 1)
			{
				return ClusteringStatusEnum.OneSource;
			}
			if (sourceCount <= clusterContext.NumberofSourcesMadeOfMajorSourcesAbove)
			{
				return ClusteringStatusEnum.UkOneOrMultiSource;
			}
			return ClusteringStatusEnum.MultiSource;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00009A34 File Offset: 0x00007C34
		private void IncrementAttributeCounter(MailInfo mailInfo)
		{
			this.SenderDomains.Increment((long)mailInfo.SenderDomainHash);
			this.Senders.Increment((long)mailInfo.SenderHash);
			this.Subjects.Increment((long)mailInfo.SubjectHash);
			this.ClientIps.Increment((long)mailInfo.ClientIpHash);
			this.ClientIp24s.Increment((long)mailInfo.ClientIp24Hash);
			if (this.RecipientDomains.CounterNumbers <= 20)
			{
				foreach (ulong key in mailInfo.RecipientsDomainHash)
				{
					if (this.RecipientDomains.CounterNumbers <= 20)
					{
						this.RecipientDomains.Increment((long)key);
					}
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00009ADC File Offset: 0x00007CDC
		private void IncrementFeedCounter(MailInfo mailInfo)
		{
			if (mailInfo.SenFeed)
			{
				Interlocked.Add(ref this.senCount, mailInfo.RecipientNumber);
			}
			if (mailInfo.FnFeed)
			{
				Interlocked.Add(ref this.fnCount, mailInfo.RecipientNumber);
			}
			if (mailInfo.HoneypotFeed)
			{
				Interlocked.Add(ref this.honeypotCount, mailInfo.RecipientNumber);
			}
			if (mailInfo.ThirdPartyFeed)
			{
				Interlocked.Add(ref this.thirdPartyCount, mailInfo.RecipientNumber);
			}
			if (mailInfo.SewrFeed)
			{
				Interlocked.Add(ref this.sewrCount, mailInfo.RecipientNumber);
			}
			if (mailInfo.SpamVerdict)
			{
				Interlocked.Add(ref this.spamCount, mailInfo.RecipientNumber);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00009B85 File Offset: 0x00007D85
		private void IncrementClusterSize(MailInfo mailInfo)
		{
			if (!mailInfo.SpamVerdict)
			{
				if (mailInfo.EmailDirection == DirectionEnum.Incoming)
				{
					Interlocked.Add(ref this.sizeIncoming, mailInfo.RecipientNumber);
					return;
				}
				Interlocked.Add(ref this.sizeOutgoing, mailInfo.RecipientNumber);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private void SetClusterResult(MailInfo mailInfo, ClusterContext clusterContext, int[] propertyValues, ClusterResult result)
		{
			result.ActionMode = this.ResolveActionMode(mailInfo.EmailDirection, propertyValues, clusterContext);
			result.Status = this.Status;
			result.PropertyValues = propertyValues;
			if (result.ActionMode != ActionEnum.BelowThreshold)
			{
				result.Clusteroid = this.Clusteroid;
				result.StartTimeStamp = this.LastRefreshTime;
			}
		}

		// Token: 0x04000032 RID: 50
		private static readonly int StatusEnumLength = Enum.GetNames(typeof(ClusterPropertyEnum)).Length;

		// Token: 0x04000033 RID: 51
		private LshFingerprint clusteroid;

		// Token: 0x04000034 RID: 52
		private int senCount;

		// Token: 0x04000035 RID: 53
		private int honeypotCount;

		// Token: 0x04000036 RID: 54
		private int fnCount;

		// Token: 0x04000037 RID: 55
		private int thirdPartyCount;

		// Token: 0x04000038 RID: 56
		private int sewrCount;

		// Token: 0x04000039 RID: 57
		private int spamCount;

		// Token: 0x0400003A RID: 58
		private int sizeIncoming;

		// Token: 0x0400003B RID: 59
		private int sizeOutgoing;

		// Token: 0x0400003C RID: 60
		private int status;
	}
}
