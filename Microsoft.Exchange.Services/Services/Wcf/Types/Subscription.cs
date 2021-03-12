using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB5 RID: 2741
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class Subscription : OptionsPropertyChangeTracker
	{
		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x00106C67 File Offset: 0x00104E67
		// (set) Token: 0x06004D24 RID: 19748 RVA: 0x00106C6F File Offset: 0x00104E6F
		[DataMember]
		public string DetailedStatus { get; set; }

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x00106C78 File Offset: 0x00104E78
		// (set) Token: 0x06004D26 RID: 19750 RVA: 0x00106C80 File Offset: 0x00104E80
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x00106C89 File Offset: 0x00104E89
		// (set) Token: 0x06004D28 RID: 19752 RVA: 0x00106C91 File Offset: 0x00104E91
		[DataMember]
		public string EmailAddress { get; set; }

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x00106C9A File Offset: 0x00104E9A
		// (set) Token: 0x06004D2A RID: 19754 RVA: 0x00106CA2 File Offset: 0x00104EA2
		[DataMember]
		public Identity Identity { get; set; }

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x00106CAB File Offset: 0x00104EAB
		// (set) Token: 0x06004D2C RID: 19756 RVA: 0x00106CB3 File Offset: 0x00104EB3
		[DataMember]
		public bool IsErrorStatus { get; set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x00106CBC File Offset: 0x00104EBC
		// (set) Token: 0x06004D2E RID: 19758 RVA: 0x00106CC4 File Offset: 0x00104EC4
		[DataMember]
		public bool IsValid { get; set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x00106CCD File Offset: 0x00104ECD
		// (set) Token: 0x06004D30 RID: 19760 RVA: 0x00106CD5 File Offset: 0x00104ED5
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string LastSuccessfulSync { get; set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06004D31 RID: 19761 RVA: 0x00106CDE File Offset: 0x00104EDE
		// (set) Token: 0x06004D32 RID: 19762 RVA: 0x00106CE6 File Offset: 0x00104EE6
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06004D33 RID: 19763 RVA: 0x00106CEF File Offset: 0x00104EEF
		// (set) Token: 0x06004D34 RID: 19764 RVA: 0x00106CF7 File Offset: 0x00104EF7
		[IgnoreDataMember]
		public SendAsState SendAsState { get; set; }

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06004D35 RID: 19765 RVA: 0x00106D00 File Offset: 0x00104F00
		// (set) Token: 0x06004D36 RID: 19766 RVA: 0x00106D0D File Offset: 0x00104F0D
		[DataMember(Name = "SendAsState", IsRequired = false, EmitDefaultValue = false)]
		public string SendAsStateString
		{
			get
			{
				return EnumUtilities.ToString<SendAsState>(this.SendAsState);
			}
			set
			{
				this.SendAsState = EnumUtilities.Parse<SendAsState>(value);
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06004D37 RID: 19767 RVA: 0x00106D1B File Offset: 0x00104F1B
		// (set) Token: 0x06004D38 RID: 19768 RVA: 0x00106D23 File Offset: 0x00104F23
		[IgnoreDataMember]
		public AggregationStatus Status { get; set; }

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x00106D2C File Offset: 0x00104F2C
		// (set) Token: 0x06004D3A RID: 19770 RVA: 0x00106D39 File Offset: 0x00104F39
		[DataMember(Name = "Status", IsRequired = false, EmitDefaultValue = false)]
		public string StatusString
		{
			get
			{
				return EnumUtilities.ToString<AggregationStatus>(this.Status);
			}
			set
			{
				this.Status = EnumUtilities.Parse<AggregationStatus>(value);
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004D3B RID: 19771 RVA: 0x00106D47 File Offset: 0x00104F47
		// (set) Token: 0x06004D3C RID: 19772 RVA: 0x00106D4F File Offset: 0x00104F4F
		[DataMember]
		public string StatusDescription { get; set; }

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x00106D58 File Offset: 0x00104F58
		// (set) Token: 0x06004D3E RID: 19774 RVA: 0x00106D60 File Offset: 0x00104F60
		[IgnoreDataMember]
		public AggregationSubscriptionType SubscriptionType { get; set; }

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x00106D69 File Offset: 0x00104F69
		// (set) Token: 0x06004D40 RID: 19776 RVA: 0x00106D76 File Offset: 0x00104F76
		[DataMember(Name = "SubscriptionType", IsRequired = false, EmitDefaultValue = false)]
		public string SubscriptionTypeString
		{
			get
			{
				return EnumUtilities.ToString<AggregationSubscriptionType>(this.SubscriptionType);
			}
			set
			{
				this.SubscriptionType = EnumUtilities.Parse<AggregationSubscriptionType>(value);
			}
		}
	}
}
