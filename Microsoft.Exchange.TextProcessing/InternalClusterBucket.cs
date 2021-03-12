using System;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000010 RID: 16
	internal class InternalClusterBucket
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00009C34 File Offset: 0x00007E34
		public InternalClusterBucket()
		{
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
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00009CC1 File Offset: 0x00007EC1
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00009CC9 File Offset: 0x00007EC9
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00009CD2 File Offset: 0x00007ED2
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00009CDA File Offset: 0x00007EDA
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00009CE3 File Offset: 0x00007EE3
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00009CEB File Offset: 0x00007EEB
		public ActionEnum ActionMode { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public ClusteringStatusEnum Status
		{
			get
			{
				return (ClusteringStatusEnum)this.status;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00009CFC File Offset: 0x00007EFC
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00009D04 File Offset: 0x00007F04
		public SmallCounterMap SenderDomains { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00009D0D File Offset: 0x00007F0D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00009D15 File Offset: 0x00007F15
		public SmallCounterMap Senders { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00009D1E File Offset: 0x00007F1E
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00009D26 File Offset: 0x00007F26
		public SmallCounterMap RecipientDomains { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00009D2F File Offset: 0x00007F2F
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00009D37 File Offset: 0x00007F37
		public SmallCounterMap Subjects { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00009D40 File Offset: 0x00007F40
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00009D48 File Offset: 0x00007F48
		public SmallCounterMap ClientIps { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00009D51 File Offset: 0x00007F51
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00009D59 File Offset: 0x00007F59
		public SmallCounterMap ClientIp24s { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00009D62 File Offset: 0x00007F62
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00009D6A File Offset: 0x00007F6A
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00009D73 File Offset: 0x00007F73
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00009D7B File Offset: 0x00007F7B
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00009D84 File Offset: 0x00007F84
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00009D8C File Offset: 0x00007F8C
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00009D95 File Offset: 0x00007F95
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00009D9D File Offset: 0x00007F9D
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

		// Token: 0x06000091 RID: 145 RVA: 0x00009DA8 File Offset: 0x00007FA8
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
			result = new ClusterResult();
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
			if (mailInfo.EmailDirection == DirectionEnum.Incoming)
			{
				Interlocked.Add(ref this.sizeIncoming, mailInfo.RecipientNumber);
			}
			else
			{
				Interlocked.Add(ref this.sizeOutgoing, mailInfo.RecipientNumber);
			}
			int[] propertyValues;
			this.Summarize(mailInfo, clusterContext, out propertyValues);
			result.ActionMode = this.ResolveActionMode(mailInfo.EmailDirection, propertyValues, clusterContext);
			result.Status = this.Status;
			result.PropertyValues = propertyValues;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00009F58 File Offset: 0x00008158
		public int BucketSize(DirectionEnum direction)
		{
			int result;
			if (direction == DirectionEnum.Incoming)
			{
				result = this.sizeIncoming;
			}
			else
			{
				result = this.sizeOutgoing;
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009F7C File Offset: 0x0000817C
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

		// Token: 0x06000094 RID: 148 RVA: 0x0000A008 File Offset: 0x00008208
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
			return clusteringStatusEnum;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000A082 File Offset: 0x00008282
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

		// Token: 0x06000096 RID: 150 RVA: 0x0000A098 File Offset: 0x00008298
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
				bool flag = ((this.Status & ClusteringStatusEnum.SpamFeedMask) != ClusteringStatusEnum.None && num > clusterContext.SpamFeedClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.OneSource) == ClusteringStatusEnum.OneSource && num > clusterContext.AllOneSourceClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.OneAndMultiSource) == ClusteringStatusEnum.OneAndMultiSource && num > clusterContext.OneAndMultiSourceClusterSizeAbove) || ((this.Status & ClusteringStatusEnum.MultiSource) == ClusteringStatusEnum.MultiSource && num > clusterContext.AllMultiSourceClusterSizeAbove);
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

		// Token: 0x06000097 RID: 151 RVA: 0x0000A1AC File Offset: 0x000083AC
		private void Summarize(MailInfo mailInfo, ClusterContext clusterContext, out int[] propertyValues)
		{
			propertyValues = new int[InternalClusterBucket.StatusEnumLength];
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
		}

		// Token: 0x04000045 RID: 69
		private static readonly int StatusEnumLength = Enum.GetNames(typeof(ClusterPropertyEnum)).Length;

		// Token: 0x04000046 RID: 70
		private int senCount;

		// Token: 0x04000047 RID: 71
		private int honeypotCount;

		// Token: 0x04000048 RID: 72
		private int fnCount;

		// Token: 0x04000049 RID: 73
		private int thirdPartyCount;

		// Token: 0x0400004A RID: 74
		private int sizeIncoming;

		// Token: 0x0400004B RID: 75
		private int sizeOutgoing;

		// Token: 0x0400004C RID: 76
		private int status;
	}
}
