using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ABA RID: 2746
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InboxRule : SetInboxRuleData
	{
		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x001075BC File Offset: 0x001057BC
		// (set) Token: 0x06004DDF RID: 19935 RVA: 0x001075C4 File Offset: 0x001057C4
		[DataMember]
		public RuleDescription Description { get; internal set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x001075CD File Offset: 0x001057CD
		// (set) Token: 0x06004DE1 RID: 19937 RVA: 0x001075D5 File Offset: 0x001057D5
		[DataMember]
		public string DescriptionTimeFormat { get; internal set; }

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x001075DE File Offset: 0x001057DE
		// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x001075E6 File Offset: 0x001057E6
		[DataMember]
		public string DescriptionTimeZone { get; internal set; }

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x001075EF File Offset: 0x001057EF
		// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x001075F7 File Offset: 0x001057F7
		[DataMember]
		public bool Enabled { get; internal set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x00107600 File Offset: 0x00105800
		// (set) Token: 0x06004DE7 RID: 19943 RVA: 0x00107608 File Offset: 0x00105808
		[DataMember]
		public bool InError { get; internal set; }

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x00107611 File Offset: 0x00105811
		// (set) Token: 0x06004DE9 RID: 19945 RVA: 0x00107619 File Offset: 0x00105819
		[DataMember]
		public bool SupportedByTask { get; internal set; }

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x00107622 File Offset: 0x00105822
		// (set) Token: 0x06004DEB RID: 19947 RVA: 0x0010762A File Offset: 0x0010582A
		[DataMember]
		public string[] WarningMessages { get; internal set; }

		// Token: 0x06004DEC RID: 19948 RVA: 0x00107633 File Offset: 0x00105833
		public InboxRule()
		{
			this.Enabled = true;
		}
	}
}
