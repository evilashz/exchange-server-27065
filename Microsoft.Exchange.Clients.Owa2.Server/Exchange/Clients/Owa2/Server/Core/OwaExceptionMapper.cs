using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F0 RID: 496
	internal class OwaExceptionMapper : ExceptionMappingBase
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x00043263 File Offset: 0x00041463
		public OwaExceptionMapper(Type exceptionType) : base(exceptionType, ExceptionMappingBase.Attributes.None)
		{
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0004326D File Offset: 0x0004146D
		public override LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return exception.LocalizedString;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00043275 File Offset: 0x00041475
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			return ResponseCodeType.ErrorInternalServerError;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004327C File Offset: 0x0004147C
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			return ExchangeVersion.Exchange2012;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00043283 File Offset: 0x00041483
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			return (CoreResources.IDs)0U;
		}
	}
}
