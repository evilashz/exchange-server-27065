using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AlternateMailboxPropertyBag : PropertyBag
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023B7 File Offset: 0x000005B7
		public AlternateMailboxPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023C1 File Offset: 0x000005C1
		public AlternateMailboxPropertyBag()
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023C9 File Offset: 0x000005C9
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return AlternateMailboxSchema.Identity;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023D0 File Offset: 0x000005D0
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023D7 File Offset: 0x000005D7
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ObjectState;
			}
		}
	}
}
