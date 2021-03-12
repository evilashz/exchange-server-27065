using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	[Serializable]
	internal sealed class InferenceRecipient : MdbRecipient, IInferenceRecipient, IMessageRecipient, IEquatable<IMessageRecipient>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000251F File Offset: 0x0000071F
		internal InferenceRecipient(IMessageRecipient recipient) : base(recipient)
		{
			this.InitializeInternal();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000252E File Offset: 0x0000072E
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002536 File Offset: 0x00000736
		[DataMember(Name = "TotalSentCount")]
		public long TotalSentCount { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000253F File Offset: 0x0000073F
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002547 File Offset: 0x00000747
		[DataMember(Name = "FirstSentTime")]
		public DateTime FirstSentTime { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002550 File Offset: 0x00000750
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002558 File Offset: 0x00000758
		[DataMember(Name = "LastSentTime")]
		public DateTime LastSentTime { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002561 File Offset: 0x00000761
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002569 File Offset: 0x00000769
		[DataMember(Name = "RecipientRank")]
		public int RecipientRank
		{
			get
			{
				return this.recipientRank;
			}
			set
			{
				if (value < 1 || (value > PeopleRelevanceConfig.Instance.MaxRelevantRecipientsCount && value != 2147483647))
				{
					throw new InvalidOperationException(string.Format("Invalid recipient rank: {0}", value.ToString()));
				}
				this.recipientRank = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000025A2 File Offset: 0x000007A2
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025AA File Offset: 0x000007AA
		[DataMember(Name = "RawRecipientWeight")]
		public double RawRecipientWeight { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025B3 File Offset: 0x000007B3
		public int RelevanceCategory
		{
			get
			{
				return InferenceRecipient.GetRelevanceCategoryForRecipient(this);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000025BB File Offset: 0x000007BB
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000025C3 File Offset: 0x000007C3
		[DataMember(Name = "RelevanceCategoryAtLastCapture")]
		public int RelevanceCategoryAtLastCapture { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000025D4 File Offset: 0x000007D4
		[DataMember(Name = "LastUsedInTimeWindow")]
		public long LastUsedInTimeWindow { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000025DD File Offset: 0x000007DD
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000025E5 File Offset: 0x000007E5
		[DataMember(Name = "CaptureFlag")]
		public int CaptureFlag { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000025EE File Offset: 0x000007EE
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000025F6 File Offset: 0x000007F6
		[DataMember(Name = "HasUpdatedData")]
		public bool HasUpdatedData { get; set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002600 File Offset: 0x00000800
		public override void UpdateFromRecipient(IMessageRecipient recipient)
		{
			if (!string.Equals(base.SipUri, recipient.SipUri, StringComparison.OrdinalIgnoreCase) || !string.Equals(base.Alias, recipient.Alias, StringComparison.OrdinalIgnoreCase) || base.RecipientDisplayType != recipient.RecipientDisplayType)
			{
				this.HasUpdatedData = true;
			}
			base.UpdateFromRecipient(recipient);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002654 File Offset: 0x00000854
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[S:{0},", base.SmtpAddress);
			stringBuilder.AppendFormat("R:{0},", this.RecipientRank);
			stringBuilder.AppendFormat("U:{0},", this.HasUpdatedData);
			stringBuilder.AppendFormat("F:{0}]", this.CaptureFlag);
			return stringBuilder.ToString();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026C4 File Offset: 0x000008C4
		internal static int GetRelevanceCategoryForRecipient(IInferenceRecipient recipient)
		{
			return InferenceRecipient.GetRelevanceCategoryForRank(recipient.RecipientRank);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026D1 File Offset: 0x000008D1
		internal static int GetRelevanceCategoryForRank(int relevanceRank)
		{
			if (relevanceRank == 2147483647)
			{
				return int.MaxValue;
			}
			return Math.Min(PeopleRelevanceConfig.Instance.RelevanceCategoriesCount, relevanceRank / (PeopleRelevanceConfig.Instance.MaxRelevantRecipientsCount / PeopleRelevanceConfig.Instance.RelevanceCategoriesCount) + 1);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002709 File Offset: 0x00000909
		private void InitializeInternal()
		{
			this.TotalSentCount = 0L;
			this.RecipientRank = int.MaxValue;
			this.RelevanceCategoryAtLastCapture = int.MaxValue;
		}

		// Token: 0x04000011 RID: 17
		public const int RecipientRankForIrrelevantEntries = 2147483647;

		// Token: 0x04000012 RID: 18
		public const int RelevanceCategoryForIrrelevantEntries = 2147483647;

		// Token: 0x04000013 RID: 19
		public const double RawRecipientWeightForIrrelevantEntries = 0.0;

		// Token: 0x04000014 RID: 20
		private int recipientRank;
	}
}
