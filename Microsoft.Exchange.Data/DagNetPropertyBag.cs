using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000213 RID: 531
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DagNetPropertyBag : PropertyBag
	{
		// Token: 0x060012A0 RID: 4768 RVA: 0x00039550 File Offset: 0x00037750
		public DagNetPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003955A File Offset: 0x0003775A
		public DagNetPropertyBag()
		{
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00039562 File Offset: 0x00037762
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return DatabaseAvailabilityGroupNetworkSchema.Identity;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00039569 File Offset: 0x00037769
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00039570 File Offset: 0x00037770
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ObjectState;
			}
		}
	}
}
