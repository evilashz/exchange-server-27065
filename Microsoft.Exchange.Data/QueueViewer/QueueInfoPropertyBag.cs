using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000289 RID: 649
	[Serializable]
	internal class QueueInfoPropertyBag : PropertyBag
	{
		// Token: 0x0600174D RID: 5965 RVA: 0x00048CCA File Offset: 0x00046ECA
		public QueueInfoPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00048CE6 File Offset: 0x00046EE6
		public QueueInfoPropertyBag() : base(false, 16)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00048D03 File Offset: 0x00046F03
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return ExtensibleQueueInfoSchema.ExchangeVersion;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00048D0A File Offset: 0x00046F0A
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return ExtensibleQueueInfoSchema.ObjectState;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00048D11 File Offset: 0x00046F11
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return ExtensibleQueueInfoSchema.Identity;
			}
		}
	}
}
