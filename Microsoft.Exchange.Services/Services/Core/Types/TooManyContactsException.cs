using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A6 RID: 2214
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TooManyContactsException : ServicePermanentException
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x000D9FD6 File Offset: 0x000D81D6
		public TooManyContactsException() : base((CoreResources.IDs)2291046867U)
		{
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003F01 RID: 16129 RVA: 0x000D9FE8 File Offset: 0x000D81E8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
