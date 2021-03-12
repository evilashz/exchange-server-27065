using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027E RID: 638
	[Serializable]
	internal class MessageInfoPropertyBag : PropertyBag
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x00044F94 File Offset: 0x00043194
		public MessageInfoPropertyBag() : this(false, 16)
		{
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00044F9F File Offset: 0x0004319F
		public MessageInfoPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
			base.SetField(this.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00044FBB File Offset: 0x000431BB
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return ExtensibleMessageInfoSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00044FC2 File Offset: 0x000431C2
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return ExtensibleMessageInfoSchema.ObjectState;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00044FC9 File Offset: 0x000431C9
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return ExtensibleMessageInfoSchema.Identity;
			}
		}
	}
}
