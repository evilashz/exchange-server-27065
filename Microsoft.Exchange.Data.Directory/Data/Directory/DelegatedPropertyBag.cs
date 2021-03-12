using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	internal class DelegatedPropertyBag : ADPropertyBag
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x00033622 File Offset: 0x00031822
		public DelegatedPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0003362C File Offset: 0x0003182C
		public DelegatedPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00033637 File Offset: 0x00031837
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return DelegatedObjectSchema.Identity;
			}
		}
	}
}
