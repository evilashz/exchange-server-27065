using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000724 RID: 1828
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ClientIntentInvalidStateDefinitionException : ServicePermanentException
	{
		// Token: 0x06003782 RID: 14210 RVA: 0x000C5942 File Offset: 0x000C3B42
		public ClientIntentInvalidStateDefinitionException() : base((CoreResources.IDs)3510335548U)
		{
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000C5954 File Offset: 0x000C3B54
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
