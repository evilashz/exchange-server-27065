using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A3 RID: 1443
	[Serializable]
	internal class MessageStatisticsReportPropertyBag : PropertyBag
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x000CF76E File Offset: 0x000CD96E
		public MessageStatisticsReportPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000CF778 File Offset: 0x000CD978
		public MessageStatisticsReportPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000CF783 File Offset: 0x000CD983
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return TransportReportSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000CF78A File Offset: 0x000CD98A
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return TransportReportSchema.ObjectState;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000CF791 File Offset: 0x000CD991
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return TransportReportSchema.Identity;
			}
		}
	}
}
