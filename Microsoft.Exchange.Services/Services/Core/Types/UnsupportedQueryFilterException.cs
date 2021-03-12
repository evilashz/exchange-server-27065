using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B5 RID: 2229
	internal sealed class UnsupportedQueryFilterException : ServicePermanentException
	{
		// Token: 0x06003F46 RID: 16198 RVA: 0x000DB2E3 File Offset: 0x000D94E3
		public UnsupportedQueryFilterException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000DB2EC File Offset: 0x000D94EC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
