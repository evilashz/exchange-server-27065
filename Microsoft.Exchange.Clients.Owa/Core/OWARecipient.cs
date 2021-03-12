using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F2 RID: 498
	internal class OWARecipient : IComparable<OWARecipient>
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0006478A File Offset: 0x0006298A
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00064792 File Offset: 0x00062992
		public ADObjectId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0006479B File Offset: 0x0006299B
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000647A3 File Offset: 0x000629A3
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000647AC File Offset: 0x000629AC
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x000647B4 File Offset: 0x000629B4
		public string PhoneticDisplayName
		{
			get
			{
				return this.phoneticDisplayName;
			}
			set
			{
				this.phoneticDisplayName = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000647BD File Offset: 0x000629BD
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x000647C5 File Offset: 0x000629C5
		public RecipientType UserRecipientType
		{
			get
			{
				return this.recipientType;
			}
			set
			{
				this.recipientType = value;
				this.isDistributionList = Utilities.IsADDistributionList(this.recipientType);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x000647DF File Offset: 0x000629DF
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x000647E7 File Offset: 0x000629E7
		public string LegacyDN
		{
			get
			{
				return this.legacyExchangeDN;
			}
			set
			{
				this.legacyExchangeDN = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x000647F0 File Offset: 0x000629F0
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x000647F8 File Offset: 0x000629F8
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00064801 File Offset: 0x00062A01
		public bool IsDistributionList
		{
			get
			{
				return this.isDistributionList;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00064809 File Offset: 0x00062A09
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x00064811 File Offset: 0x00062A11
		public bool HasValidDigitalId
		{
			get
			{
				return this.hasValidDigitalId;
			}
			set
			{
				this.hasValidDigitalId = value;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0006481A File Offset: 0x00062A1A
		public int CompareTo(OWARecipient x)
		{
			return string.Compare(this.DisplayName, x.DisplayName, StringComparison.CurrentCulture);
		}

		// Token: 0x04000AE0 RID: 2784
		private string displayName;

		// Token: 0x04000AE1 RID: 2785
		private string phoneticDisplayName;

		// Token: 0x04000AE2 RID: 2786
		private RecipientType recipientType;

		// Token: 0x04000AE3 RID: 2787
		private string alias;

		// Token: 0x04000AE4 RID: 2788
		private string legacyExchangeDN;

		// Token: 0x04000AE5 RID: 2789
		private ADObjectId id;

		// Token: 0x04000AE6 RID: 2790
		private bool isDistributionList;

		// Token: 0x04000AE7 RID: 2791
		private bool hasValidDigitalId;
	}
}
