using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000701 RID: 1793
	internal class CalendarExceptionOutOfRange : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x0600364B RID: 13899 RVA: 0x000C22FE File Offset: 0x000C04FE
		public CalendarExceptionOutOfRange(PropertyPath propertyPath) : base((CoreResources.IDs)3773356320U, propertyPath)
		{
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x000C2311 File Offset: 0x000C0511
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
