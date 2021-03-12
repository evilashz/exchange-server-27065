using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000594 RID: 1428
	[XmlType(TypeName = "RulePredicateSizeRangeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RulePredicateSizeRange
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000AB626 File Offset: 0x000A9826
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x000AB62E File Offset: 0x000A982E
		[XmlElement(Order = 0)]
		public int MinimumSize { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000AB637 File Offset: 0x000A9837
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x000AB63F File Offset: 0x000A983F
		[XmlIgnore]
		public bool MinimumSizeSpecified { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000AB648 File Offset: 0x000A9848
		// (set) Token: 0x0600284D RID: 10317 RVA: 0x000AB650 File Offset: 0x000A9850
		[XmlElement(Order = 1)]
		public int MaximumSize { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x000AB659 File Offset: 0x000A9859
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x000AB661 File Offset: 0x000A9861
		[XmlIgnore]
		public bool MaximumSizeSpecified { get; set; }

		// Token: 0x06002850 RID: 10320 RVA: 0x000AB66A File Offset: 0x000A986A
		public RulePredicateSizeRange()
		{
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000AB674 File Offset: 0x000A9874
		public RulePredicateSizeRange(int? minimumSize, int? maximumSize)
		{
			if (minimumSize != null)
			{
				this.MinimumSize = minimumSize.Value;
				this.MinimumSizeSpecified = true;
			}
			if (maximumSize != null)
			{
				this.MaximumSize = maximumSize.Value;
				this.MaximumSizeSpecified = true;
			}
		}
	}
}
