using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	internal class SupervisionListEntryPropertyBag : PropertyBag
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x00047E0C File Offset: 0x0004600C
		public SupervisionListEntryPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00047E28 File Offset: 0x00046028
		public SupervisionListEntryPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x00047E45 File Offset: 0x00046045
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SupervisionListEntrySchema.ExchangeVersion;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00047E4C File Offset: 0x0004604C
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SupervisionListEntrySchema.ObjectState;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00047E53 File Offset: 0x00046053
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return SupervisionListEntrySchema.Identity;
			}
		}
	}
}
