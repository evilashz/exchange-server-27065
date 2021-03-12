using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BD RID: 1981
	internal class InvalidNameForNameResolutionException : ServicePermanentException
	{
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000CF7EB File Offset: 0x000CD9EB
		public InvalidNameForNameResolutionException() : base((CoreResources.IDs)4279571010U)
		{
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000CF7FD File Offset: 0x000CD9FD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
