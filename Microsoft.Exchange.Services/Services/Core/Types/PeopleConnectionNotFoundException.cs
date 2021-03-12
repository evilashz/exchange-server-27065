using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000840 RID: 2112
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleConnectionNotFoundException : ServicePermanentException
	{
		// Token: 0x06003CF3 RID: 15603 RVA: 0x000D6F84 File Offset: 0x000D5184
		public PeopleConnectionNotFoundException() : base(CoreResources.IDs.ErrorPeopleConnectionNotFound)
		{
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000D6F96 File Offset: 0x000D5196
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
