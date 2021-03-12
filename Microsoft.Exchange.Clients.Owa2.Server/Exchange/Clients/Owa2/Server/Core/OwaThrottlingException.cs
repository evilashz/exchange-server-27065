using System;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	internal sealed class OwaThrottlingException : ServicePermanentException
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00022D1C File Offset: 0x00020F1C
		public OwaThrottlingException() : base(ResponseCodeType.ErrorServerBusy, (CoreResources.IDs)3655513582U)
		{
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00022D33 File Offset: 0x00020F33
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
