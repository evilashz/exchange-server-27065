using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C1 RID: 1985
	[Serializable]
	internal sealed class InvalidPropertyAppendException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003ADC RID: 15068 RVA: 0x000CF8CF File Offset: 0x000CDACF
		public InvalidPropertyAppendException(PropertyPath propertyPath) : base((CoreResources.IDs)3619206730U, propertyPath)
		{
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000CF8E2 File Offset: 0x000CDAE2
		public InvalidPropertyAppendException(Enum messageId, PropertyPath propertyPath) : base(ResponseCodeType.ErrorInvalidPropertyAppend, messageId, propertyPath)
		{
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000CF8F1 File Offset: 0x000CDAF1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
