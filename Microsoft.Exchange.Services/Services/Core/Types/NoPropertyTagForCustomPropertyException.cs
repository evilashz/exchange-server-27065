using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000827 RID: 2087
	internal sealed class NoPropertyTagForCustomPropertyException : ServicePermanentException
	{
		// Token: 0x06003C65 RID: 15461 RVA: 0x000D5C6E File Offset: 0x000D3E6E
		public NoPropertyTagForCustomPropertyException() : base((CoreResources.IDs)3969305989U)
		{
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000D5C80 File Offset: 0x000D3E80
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
