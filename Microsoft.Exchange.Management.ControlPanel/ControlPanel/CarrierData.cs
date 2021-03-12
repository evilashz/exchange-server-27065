using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000488 RID: 1160
	public sealed class CarrierData
	{
		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000AFCA8 File Offset: 0x000ADEA8
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x000AFCB0 File Offset: 0x000ADEB0
		public string ID { get; set; }

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000AFCB9 File Offset: 0x000ADEB9
		public string Name
		{
			get
			{
				return SmsServiceProviders.GetLocalizedName(this.LocalizedNames);
			}
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000AFCC6 File Offset: 0x000ADEC6
		// (set) Token: 0x06003A05 RID: 14853 RVA: 0x000AFCCE File Offset: 0x000ADECE
		public bool HasSmtpGateway { get; set; }

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x000AFCD7 File Offset: 0x000ADED7
		// (set) Token: 0x06003A07 RID: 14855 RVA: 0x000AFCDF File Offset: 0x000ADEDF
		public UnifiedMessagingInfo UnifiedMessagingInfo { get; set; }

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000AFCE8 File Offset: 0x000ADEE8
		// (set) Token: 0x06003A09 RID: 14857 RVA: 0x000AFCF0 File Offset: 0x000ADEF0
		public Dictionary<string, string> LocalizedNames { get; set; }
	}
}
