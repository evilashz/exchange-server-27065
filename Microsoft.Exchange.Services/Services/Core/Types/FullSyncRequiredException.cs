using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000791 RID: 1937
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FullSyncRequiredException : ServicePermanentException
	{
		// Token: 0x060039B1 RID: 14769 RVA: 0x000CB8CD File Offset: 0x000C9ACD
		public FullSyncRequiredException() : base(CoreResources.IDs.ErrorFullSyncRequiredException)
		{
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000CB8DF File Offset: 0x000C9ADF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
