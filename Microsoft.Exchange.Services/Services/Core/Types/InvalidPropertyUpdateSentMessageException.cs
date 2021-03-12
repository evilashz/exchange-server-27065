using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C7 RID: 1991
	[Serializable]
	internal sealed class InvalidPropertyUpdateSentMessageException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AED RID: 15085 RVA: 0x000CF9BB File Offset: 0x000CDBBB
		public InvalidPropertyUpdateSentMessageException(PropertyPath propertyPath) : base(CoreResources.IDs.ErrorInvalidPropertyUpdateSentMessage, propertyPath)
		{
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000CF9CE File Offset: 0x000CDBCE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
