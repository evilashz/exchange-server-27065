using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000863 RID: 2147
	internal sealed class RequiredPropertyMissingException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003DAD RID: 15789 RVA: 0x000D7B77 File Offset: 0x000D5D77
		public RequiredPropertyMissingException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorRequiredPropertyMissing, propertyPath)
		{
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000D7B8A File Offset: 0x000D5D8A
		public RequiredPropertyMissingException(ResponseCodeType responseCodeType) : base(responseCodeType, CoreResources.IDs.ErrorRequiredPropertyMissing)
		{
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000D7B9D File Offset: 0x000D5D9D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
