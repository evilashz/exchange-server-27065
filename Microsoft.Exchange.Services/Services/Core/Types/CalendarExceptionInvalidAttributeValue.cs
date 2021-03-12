using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F0 RID: 1776
	internal class CalendarExceptionInvalidAttributeValue : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003624 RID: 13860 RVA: 0x000C1F40 File Offset: 0x000C0140
		public CalendarExceptionInvalidAttributeValue(PropertyPath propertyPath) : base((CoreResources.IDs)2961161516U, propertyPath)
		{
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000C1F53 File Offset: 0x000C0153
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
