using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000841 RID: 2113
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleConnectionNoTokenException : ServicePermanentException
	{
		// Token: 0x06003CF5 RID: 15605 RVA: 0x000D6F9D File Offset: 0x000D519D
		public PeopleConnectionNoTokenException() : base((CoreResources.IDs)3141449171U)
		{
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000D6FAF File Offset: 0x000D51AF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
