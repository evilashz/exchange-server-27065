using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087E RID: 2174
	internal sealed class ServiceArgumentException : ServicePermanentException
	{
		// Token: 0x06003E5A RID: 15962 RVA: 0x000D8597 File Offset: 0x000D6797
		public ServiceArgumentException(Enum messageId) : base(ResponseCodeType.ErrorInvalidArgument, messageId)
		{
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000D85A2 File Offset: 0x000D67A2
		public ServiceArgumentException(Enum messageId, LocalizedString message) : base(ResponseCodeType.ErrorInvalidArgument, message)
		{
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000D85AD File Offset: 0x000D67AD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
