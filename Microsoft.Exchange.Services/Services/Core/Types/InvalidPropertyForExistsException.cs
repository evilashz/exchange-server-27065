using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C3 RID: 1987
	internal class InvalidPropertyForExistsException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AE3 RID: 15075 RVA: 0x000CF935 File Offset: 0x000CDB35
		public InvalidPropertyForExistsException(PropertyPath propertyPath) : base((CoreResources.IDs)3788524313U, propertyPath)
		{
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000CF948 File Offset: 0x000CDB48
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
