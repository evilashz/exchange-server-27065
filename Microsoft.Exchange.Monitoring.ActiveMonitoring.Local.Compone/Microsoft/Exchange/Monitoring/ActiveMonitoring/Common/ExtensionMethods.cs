using System;
using System.Linq.Expressions;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000129 RID: 297
	public static class ExtensionMethods
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x000335DC File Offset: 0x000317DC
		public static void ApplyCafeAuthentication(this ExchangeServiceBase service, string username, string password)
		{
			service.HttpHeaders["X-IsFromCafe"] = "1";
			CommonAccessToken commonAccessToken;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled)
			{
				commonAccessToken = CommonAccessTokenHelper.CreateLiveIdBasic(username);
			}
			else
			{
				commonAccessToken = CommonAccessTokenHelper.CreateWindows(username, password);
			}
			service.HttpHeaders["X-CommonAccessToken"] = commonAccessToken.Serialize();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00033647 File Offset: 0x00031847
		public static void SetComponentId(this ExchangeServiceBase service, string componentId)
		{
			service.HttpHeaders[CertificateValidationManager.ComponentIdHeaderName] = componentId;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003365C File Offset: 0x0003185C
		public static TReturn EnsureNotNull<TInput, TReturn>(this TInput input, Expression<Func<TInput, TReturn>> accessor) where TReturn : class
		{
			TReturn treturn = accessor.Compile()(input);
			if (treturn == null)
			{
				throw new InvalidOperationException(string.Format("{0} returned a null for {1}, which is unexpected. Exact timestamp: {2}", accessor, typeof(TInput).Name, ExDateTime.UtcNow));
			}
			return treturn;
		}
	}
}
