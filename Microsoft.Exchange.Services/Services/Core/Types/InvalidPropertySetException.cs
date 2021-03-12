using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C6 RID: 1990
	[Serializable]
	internal sealed class InvalidPropertySetException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x000CF992 File Offset: 0x000CDB92
		public InvalidPropertySetException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorInvalidPropertySet, propertyPath)
		{
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000CF9A5 File Offset: 0x000CDBA5
		public InvalidPropertySetException(Enum messageId, PropertyPath propertyPath) : base(ResponseCodeType.ErrorInvalidPropertySet, messageId, propertyPath)
		{
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000CF9B4 File Offset: 0x000CDBB4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
