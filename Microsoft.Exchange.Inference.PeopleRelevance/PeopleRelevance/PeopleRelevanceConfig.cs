using System;
using System.Text;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000018 RID: 24
	internal sealed class PeopleRelevanceConfig : Config
	{
		// Token: 0x060000AD RID: 173 RVA: 0x0000444C File Offset: 0x0000264C
		private PeopleRelevanceConfig()
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004454 File Offset: 0x00002654
		internal static PeopleRelevanceConfig Instance
		{
			get
			{
				return PeopleRelevanceConfig.peopleRelevanceConfigInstance;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004463 File Offset: 0x00002663
		internal int RelevanceCategoriesCount { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000446C File Offset: 0x0000266C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004474 File Offset: 0x00002674
		internal int MaxRelevantRecipientsCount { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000447D File Offset: 0x0000267D
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004485 File Offset: 0x00002685
		internal TimeSpan TimeWindowLength { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000448E File Offset: 0x0000268E
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004496 File Offset: 0x00002696
		internal int MaxContactUpdatesCount { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000449F File Offset: 0x0000269F
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000044A7 File Offset: 0x000026A7
		internal TimeSpan SleepTimeBeforeInferenceProcessingStarts { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000044B0 File Offset: 0x000026B0
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000044B8 File Offset: 0x000026B8
		internal TimeSpan RecipientCacheValidationInterval { get; private set; }

		// Token: 0x060000BB RID: 187 RVA: 0x000044C4 File Offset: 0x000026C4
		public override void Load()
		{
			this.RelevanceCategoriesCount = base.ReadInt("relevanceCategoriesCount", 10);
			this.MaxRelevantRecipientsCount = base.ReadInt("maxRelevantRecipientsCount", 200);
			this.TimeWindowLength = base.ReadTimeSpan("timeWindowLength", PeopleRelevanceConfig.DefaultTimeWindowLength);
			this.MaxContactUpdatesCount = base.ReadInt("maxContactUpdatesCount", 10);
			this.SleepTimeBeforeInferenceProcessingStarts = base.ReadTimeSpan("sleepTimeBeforeInferenceProcessingStarts", PeopleRelevanceConfig.DefaultSleepTimeBeforeInferenceProcessingStarts);
			this.RecipientCacheValidationInterval = base.ReadTimeSpan("recipientCacheValidationInterval", PeopleRelevanceConfig.DefaultRecipientCacheValidationInterval);
			this.Validate();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004558 File Offset: 0x00002758
		private void Validate()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder("Invalid PeopleRelevanceConfig parameters. ");
			if (this.RelevanceCategoriesCount < 1)
			{
				flag = false;
				stringBuilder.AppendFormat("RelevanceCategoriesCount {0}. ", this.RelevanceCategoriesCount.ToString());
			}
			if (this.MaxRelevantRecipientsCount < 1 || this.MaxRelevantRecipientsCount == 2147483647)
			{
				flag = false;
				stringBuilder.AppendFormat("MaxRelevantRecipientsCount {0}. ", this.MaxRelevantRecipientsCount.ToString());
			}
			if (this.TimeWindowLength <= TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("TimeWindowLength {0}. ", this.TimeWindowLength.ToString());
			}
			if (this.MaxContactUpdatesCount < 1)
			{
				flag = false;
				stringBuilder.AppendFormat("MaxContactUpdatesCount {0}. ", this.MaxContactUpdatesCount.ToString());
			}
			if (this.SleepTimeBeforeInferenceProcessingStarts < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("SleepTimeBeforeInferenceProcessingStarts {0}. ", this.SleepTimeBeforeInferenceProcessingStarts.ToString());
			}
			if (this.RecipientCacheValidationInterval < TimeSpan.Zero)
			{
				flag = false;
				stringBuilder.AppendFormat("RecipientCacheValidationInterval {0}. ", this.RecipientCacheValidationInterval.ToString());
			}
			if (!flag)
			{
				throw new InvalidOperationException(stringBuilder.ToString());
			}
		}

		// Token: 0x04000044 RID: 68
		internal const int DefaultMaxContactUpdatesCount = 10;

		// Token: 0x04000045 RID: 69
		private const int DefaultRelevanceCategoriesCount = 10;

		// Token: 0x04000046 RID: 70
		private const int DefaultMaxRelevantRecipientsCount = 200;

		// Token: 0x04000047 RID: 71
		private static readonly TimeSpan DefaultTimeWindowLength = TimeSpan.FromHours(8.0);

		// Token: 0x04000048 RID: 72
		private static readonly TimeSpan DefaultSleepTimeBeforeInferenceProcessingStarts = TimeSpan.Zero;

		// Token: 0x04000049 RID: 73
		private static readonly TimeSpan DefaultRecipientCacheValidationInterval = TimeSpan.FromDays(3.0);

		// Token: 0x0400004A RID: 74
		private static PeopleRelevanceConfig peopleRelevanceConfigInstance = new PeopleRelevanceConfig();
	}
}
