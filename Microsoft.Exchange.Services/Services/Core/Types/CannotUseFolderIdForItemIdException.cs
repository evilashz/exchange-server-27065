using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071E RID: 1822
	internal sealed class CannotUseFolderIdForItemIdException : ServicePermanentException
	{
		// Token: 0x0600374D RID: 14157 RVA: 0x000C568C File Offset: 0x000C388C
		public CannotUseFolderIdForItemIdException() : base((CoreResources.IDs)2770848984U)
		{
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000C569E File Offset: 0x000C389E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
