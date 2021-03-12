using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021B RID: 539
	internal class ExchangeConfigurationExceptionMapping : ExceptionMappingBase
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x0004510C File Offset: 0x0004330C
		public ExchangeConfigurationExceptionMapping(Type exceptionType, ExchangeVersion effectiveVersion, ResponseCodeType responseCode) : base(exceptionType, ExceptionMappingBase.Attributes.None)
		{
			this.effectiveVersion = effectiveVersion;
			this.responseCode = responseCode;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00045124 File Offset: 0x00043324
		public override LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return exception.LocalizedString;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0004512C File Offset: 0x0004332C
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			return this.responseCode;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00045134 File Offset: 0x00043334
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			return this.effectiveVersion;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0004513C File Offset: 0x0004333C
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			return CoreResources.IDs.ErrorExchangeConfigurationException;
		}

		// Token: 0x04000AE8 RID: 2792
		private readonly ExchangeVersion effectiveVersion;

		// Token: 0x04000AE9 RID: 2793
		private readonly ResponseCodeType responseCode;
	}
}
