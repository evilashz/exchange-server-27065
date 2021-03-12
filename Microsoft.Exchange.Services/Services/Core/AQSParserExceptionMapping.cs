using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021A RID: 538
	internal class AQSParserExceptionMapping : ExceptionMappingBase
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x000450E3 File Offset: 0x000432E3
		public AQSParserExceptionMapping() : base(typeof(ParserException), ExceptionMappingBase.Attributes.StopsBatchProcessing)
		{
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000450F6 File Offset: 0x000432F6
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			return ResponseCodeType.ErrorInvalidArgument;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000450FA File Offset: 0x000432FA
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00045101 File Offset: 0x00043301
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			return (CoreResources.IDs)0U;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00045104 File Offset: 0x00043304
		public override LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return exception.LocalizedString;
		}
	}
}
