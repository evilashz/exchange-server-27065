using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class SimplePropertyBag : PropertyBag
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003B85 File Offset: 0x00001D85
		public SimplePropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B8F File Offset: 0x00001D8F
		public SimplePropertyBag() : base(false, 16)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003B9A File Offset: 0x00001D9A
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return InMemoryObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003BA1 File Offset: 0x00001DA1
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return InMemoryObjectSchema.ObjectState;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003BA8 File Offset: 0x00001DA8
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return InMemoryObjectSchema.Identity;
			}
		}
	}
}
