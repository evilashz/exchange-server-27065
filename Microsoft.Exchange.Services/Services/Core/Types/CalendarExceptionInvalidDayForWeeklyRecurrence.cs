using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F2 RID: 1778
	internal class CalendarExceptionInvalidDayForWeeklyRecurrence : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003628 RID: 13864 RVA: 0x000C1F74 File Offset: 0x000C0174
		public CalendarExceptionInvalidDayForWeeklyRecurrence(PropertyPath propertyPath) : base((CoreResources.IDs)2681298929U, propertyPath)
		{
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000C1F87 File Offset: 0x000C0187
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
