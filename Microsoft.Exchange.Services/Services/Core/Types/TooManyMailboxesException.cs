using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A8 RID: 2216
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TooManyMailboxesException : ServicePermanentException
	{
		// Token: 0x06003F04 RID: 16132 RVA: 0x000DA009 File Offset: 0x000D8209
		public TooManyMailboxesException(int mailboxes, int maxAllowedMailboxes) : base(ResponseCodeType.ErrorSearchTooManyMailboxes, CoreResources.ErrorSearchTooManyMailboxes("EDiscoveryError:E010::", mailboxes, maxAllowedMailboxes))
		{
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000DA022 File Offset: 0x000D8222
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
