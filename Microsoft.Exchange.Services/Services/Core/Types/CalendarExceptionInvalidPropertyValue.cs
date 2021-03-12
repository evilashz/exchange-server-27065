using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F4 RID: 1780
	internal class CalendarExceptionInvalidPropertyValue : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x0600362C RID: 13868 RVA: 0x000C1FA8 File Offset: 0x000C01A8
		public CalendarExceptionInvalidPropertyValue(PropertyPath propertyPath) : base((CoreResources.IDs)3349192959U, propertyPath)
		{
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000C1FBB File Offset: 0x000C01BB
		public CalendarExceptionInvalidPropertyValue(PropertyPath propertyPath, Exception innerException) : base((CoreResources.IDs)3349192959U, propertyPath, innerException)
		{
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000C1FCF File Offset: 0x000C01CF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
