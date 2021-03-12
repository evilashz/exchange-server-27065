using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000831 RID: 2097
	[Serializable]
	internal sealed class ObjectTypeChangedException : ServicePermanentException
	{
		// Token: 0x06003C79 RID: 15481 RVA: 0x000D5DA3 File Offset: 0x000D3FA3
		public ObjectTypeChangedException() : base((CoreResources.IDs)4261845811U)
		{
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x000D5DB5 File Offset: 0x000D3FB5
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
