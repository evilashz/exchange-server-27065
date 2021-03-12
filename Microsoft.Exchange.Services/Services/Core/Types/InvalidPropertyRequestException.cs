using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C5 RID: 1989
	[Serializable]
	internal sealed class InvalidPropertyRequestException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000CF969 File Offset: 0x000CDB69
		public InvalidPropertyRequestException(PropertyPath propertyPath) : base((CoreResources.IDs)3673396595U, propertyPath)
		{
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000CF97C File Offset: 0x000CDB7C
		public InvalidPropertyRequestException(LocalizedString message, PropertyPath propertyPath) : base(ResponseCodeType.ErrorInvalidPropertyRequest, message, propertyPath)
		{
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000CF98B File Offset: 0x000CDB8B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
