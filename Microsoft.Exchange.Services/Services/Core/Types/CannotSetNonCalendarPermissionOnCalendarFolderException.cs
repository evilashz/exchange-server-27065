using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071C RID: 1820
	internal sealed class CannotSetNonCalendarPermissionOnCalendarFolderException : ServicePermanentException
	{
		// Token: 0x06003749 RID: 14153 RVA: 0x000C565A File Offset: 0x000C385A
		public CannotSetNonCalendarPermissionOnCalendarFolderException() : base(CoreResources.IDs.ErrorCannotSetNonCalendarPermissionOnCalendarFolder)
		{
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000C566C File Offset: 0x000C386C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
