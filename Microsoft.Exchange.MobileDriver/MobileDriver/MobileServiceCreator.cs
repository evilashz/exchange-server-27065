using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000032 RID: 50
	internal static class MobileServiceCreator
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006544 File Offset: 0x00004744
		internal static IMobileService Create(ExchangePrincipal principal, DeliveryPoint dp)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			switch (dp.Type)
			{
			case DeliveryPointType.ExchangeActiveSync:
				return MobileServiceCreator.Create(new EasSelector(principal, dp));
			case DeliveryPointType.SmtpToSmsGateway:
				return MobileServiceCreator.Create(new SmtpToSmsGatewaySelector(principal, dp));
			default:
				throw new ArgumentOutOfRangeException("dp");
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000659C File Offset: 0x0000479C
		public static IMobileService Create(IMobileServiceSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			switch (selector.Type)
			{
			case MobileServiceType.Eas:
				return new Eas((EasSelector)selector);
			case MobileServiceType.SmtpToSmsGateway:
				return new SmtpToSmsGateway((SmtpToSmsGatewaySelector)selector);
			default:
				throw new MobileDriverDataException(Strings.ErrorServiceUnsupported(selector.Type.ToString()));
			}
		}
	}
}
