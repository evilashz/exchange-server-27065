using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x0200059F RID: 1439
	[Serializable]
	internal class MessageLatencyReportPropertyBag : PropertyBag
	{
		// Token: 0x060032B4 RID: 12980 RVA: 0x000CF4B0 File Offset: 0x000CD6B0
		public MessageLatencyReportPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000CF4BA File Offset: 0x000CD6BA
		public MessageLatencyReportPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000CF4C5 File Offset: 0x000CD6C5
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return TransportReportSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000CF4CC File Offset: 0x000CD6CC
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return TransportReportSchema.ObjectState;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000CF4D3 File Offset: 0x000CD6D3
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return TransportReportSchema.Identity;
			}
		}
	}
}
