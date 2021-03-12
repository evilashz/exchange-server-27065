using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AB RID: 1963
	internal sealed class InvalidExtendedPropertyException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003A9F RID: 15007 RVA: 0x000CF346 File Offset: 0x000CD546
		public InvalidExtendedPropertyException(ExtendedPropertyUri offendingProperty) : base(CoreResources.IDs.ErrorInvalidExtendedProperty, offendingProperty)
		{
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000CF359 File Offset: 0x000CD559
		public InvalidExtendedPropertyException(ExtendedPropertyUri offendingProperty, Exception innerException) : base(CoreResources.IDs.ErrorInvalidExtendedProperty, offendingProperty, innerException)
		{
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000CF36D File Offset: 0x000CD56D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
