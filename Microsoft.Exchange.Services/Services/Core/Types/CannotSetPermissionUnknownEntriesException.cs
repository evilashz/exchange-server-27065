using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071D RID: 1821
	internal sealed class CannotSetPermissionUnknownEntriesException : ServicePermanentException
	{
		// Token: 0x0600374B RID: 14155 RVA: 0x000C5673 File Offset: 0x000C3873
		public CannotSetPermissionUnknownEntriesException() : base((CoreResources.IDs)2549623104U)
		{
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000C5685 File Offset: 0x000C3885
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
