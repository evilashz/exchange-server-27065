using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	internal class SimpleProviderPropertyBag : PropertyBag
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x0004DA76 File Offset: 0x0004BC76
		public SimpleProviderPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0004DA80 File Offset: 0x0004BC80
		public SimpleProviderPropertyBag()
		{
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0004DA88 File Offset: 0x0004BC88
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.Identity;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0004DA8F File Offset: 0x0004BC8F
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0004DA96 File Offset: 0x0004BC96
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ObjectState;
			}
		}
	}
}
