using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200051F RID: 1311
	[Serializable]
	internal class CasTransactionPropertyBag : PropertyBag
	{
		// Token: 0x06002F45 RID: 12101 RVA: 0x000BE6F3 File Offset: 0x000BC8F3
		public CasTransactionPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000BE6FD File Offset: 0x000BC8FD
		public CasTransactionPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000BE708 File Offset: 0x000BC908
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return CasTransationObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000BE70F File Offset: 0x000BC90F
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return CasTransationObjectSchema.ObjectState;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000BE716 File Offset: 0x000BC916
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return CasTransationObjectSchema.Identity;
			}
		}
	}
}
