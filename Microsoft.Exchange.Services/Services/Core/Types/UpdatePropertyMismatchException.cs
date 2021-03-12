using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B8 RID: 2232
	[Serializable]
	internal sealed class UpdatePropertyMismatchException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003F4C RID: 16204 RVA: 0x000DB325 File Offset: 0x000D9525
		public UpdatePropertyMismatchException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorUpdatePropertyMismatch, propertyPath)
		{
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000DB338 File Offset: 0x000D9538
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
