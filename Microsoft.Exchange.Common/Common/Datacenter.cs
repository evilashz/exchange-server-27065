using System;
using System.Security;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000031 RID: 49
	public static class Datacenter
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000554A File Offset: 0x0000374A
		public static bool IsLiveIDForExchangeLogin(bool wrapException)
		{
			return Datacenter.IsMicrosoftHostedOnly(wrapException);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005552 File Offset: 0x00003752
		public static bool IsMicrosoftHostedOnly(bool wrapException)
		{
			return Datacenter.IsFeatureEnabled(wrapException, new Func<bool>(DatacenterRegistry.IsMicrosoftHostedOnly));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005566 File Offset: 0x00003766
		public static bool TreatPreReqErrorsAsWarnings(bool wrapException)
		{
			return Datacenter.IsFeatureEnabled(wrapException, new Func<bool>(DatacenterRegistry.TreatPreReqErrorsAsWarnings));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000557A File Offset: 0x0000377A
		public static bool IsDatacenterDedicated(bool wrapException)
		{
			return Datacenter.IsFeatureEnabled(wrapException, new Func<bool>(DatacenterRegistry.IsDatacenterDedicated));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000558E File Offset: 0x0000378E
		public static bool IsPartnerHostedOnly(bool wrapException)
		{
			return Datacenter.IsFeatureEnabled(wrapException, new Func<bool>(DatacenterRegistry.IsPartnerHostedOnly));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000055A4 File Offset: 0x000037A4
		public static bool IsRunningInExchangeDatacenter(bool defaultValue)
		{
			bool result = defaultValue;
			try
			{
				result = Datacenter.IsMicrosoftHostedOnly(true);
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000055D0 File Offset: 0x000037D0
		public static bool IsForefrontForOfficeDatacenter()
		{
			return DatacenterRegistry.IsForefrontForOffice();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000055D7 File Offset: 0x000037D7
		public static bool IsGallatinDatacenter()
		{
			return DatacenterRegistry.IsGallatinDatacenter();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000055DE File Offset: 0x000037DE
		public static bool IsFFOGallatinDatacenter()
		{
			return DatacenterRegistry.IsFFOGallatinDatacenter();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000055E5 File Offset: 0x000037E5
		public static bool IsDatacenterDedicated()
		{
			return DatacenterRegistry.IsDatacenterDedicated();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000055EC File Offset: 0x000037EC
		public static bool IsMultiTenancyEnabled()
		{
			try
			{
				if (Datacenter.IsPartnerHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			try
			{
				if (Datacenter.IsMicrosoftHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000563C File Offset: 0x0000383C
		public static Datacenter.ExchangeSku GetExchangeSku()
		{
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				return Datacenter.ExchangeSku.ExchangeDatacenter;
			}
			if (Datacenter.IsPartnerHostedOnly(true))
			{
				return Datacenter.ExchangeSku.PartnerHosted;
			}
			if (Datacenter.IsDatacenterDedicated(true))
			{
				return Datacenter.ExchangeSku.DatacenterDedicated;
			}
			return Datacenter.ExchangeSku.Enterprise;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005660 File Offset: 0x00003860
		private static bool IsFeatureEnabled(bool wrapException, Func<bool> feature)
		{
			bool result = false;
			if (wrapException)
			{
				Exception ex = null;
				try
				{
					result = feature();
				}
				catch (DatacenterInvalidRegistryException ex2)
				{
					ex = ex2;
				}
				catch (SecurityException ex3)
				{
					ex = ex3;
				}
				catch (UnauthorizedAccessException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					throw new CannotDetermineExchangeModeException(ex.Message, ex);
				}
			}
			else
			{
				result = feature();
			}
			return result;
		}

		// Token: 0x02000032 RID: 50
		public enum ExchangeSku
		{
			// Token: 0x040000B2 RID: 178
			Enterprise,
			// Token: 0x040000B3 RID: 179
			ExchangeDatacenter,
			// Token: 0x040000B4 RID: 180
			PartnerHosted,
			// Token: 0x040000B5 RID: 181
			ForefrontForOfficeDatacenter,
			// Token: 0x040000B6 RID: 182
			DatacenterDedicated
		}
	}
}
