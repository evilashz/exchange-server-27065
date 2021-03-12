using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071B RID: 1819
	internal sealed class CannotSetCalendarPermissionOnNonCalendarFolderException : ServicePermanentException
	{
		// Token: 0x06003747 RID: 14151 RVA: 0x000C5641 File Offset: 0x000C3841
		public CannotSetCalendarPermissionOnNonCalendarFolderException() : base((CoreResources.IDs)2183377470U)
		{
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000C5653 File Offset: 0x000C3853
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
