using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AC RID: 1964
	internal sealed class InvalidExtendedPropertyValueException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AA2 RID: 15010 RVA: 0x000CF374 File Offset: 0x000CD574
		public InvalidExtendedPropertyValueException(ExtendedPropertyUri propertyPath) : base((CoreResources.IDs)3635256568U, propertyPath)
		{
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000CF387 File Offset: 0x000CD587
		public InvalidExtendedPropertyValueException(ExtendedPropertyUri propertyPath, Exception innerException) : base((CoreResources.IDs)3635256568U, propertyPath, innerException)
		{
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000CF39B File Offset: 0x000CD59B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
