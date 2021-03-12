using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C2 RID: 1986
	[Serializable]
	internal sealed class InvalidPropertyDeleteException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003ADF RID: 15071 RVA: 0x000CF8F8 File Offset: 0x000CDAF8
		public InvalidPropertyDeleteException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorInvalidPropertyDelete, propertyPath)
		{
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000CF90B File Offset: 0x000CDB0B
		public InvalidPropertyDeleteException(Enum messageId, PropertyPath propertyPath) : base(ResponseCodeType.ErrorInvalidPropertyDelete, messageId, propertyPath)
		{
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000CF91A File Offset: 0x000CDB1A
		public InvalidPropertyDeleteException(PropertyPath propertyPath, Exception innerException) : base(CoreResources.IDs.ErrorInvalidPropertyDelete, propertyPath, innerException)
		{
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000CF92E File Offset: 0x000CDB2E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
