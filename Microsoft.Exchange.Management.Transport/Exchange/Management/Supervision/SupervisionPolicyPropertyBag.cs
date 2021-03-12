using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	internal class SupervisionPolicyPropertyBag : PropertyBag
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x000143F7 File Offset: 0x000125F7
		public SupervisionPolicyPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00014413 File Offset: 0x00012613
		public SupervisionPolicyPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00014430 File Offset: 0x00012630
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SupervisionPolicySchema.ExchangeVersion;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00014437 File Offset: 0x00012637
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SupervisionPolicySchema.ObjectState;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001443E File Offset: 0x0001263E
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return SupervisionPolicySchema.Identity;
			}
		}
	}
}
