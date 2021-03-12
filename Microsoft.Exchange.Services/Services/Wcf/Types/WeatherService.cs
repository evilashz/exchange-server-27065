using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B64 RID: 2916
	internal class WeatherService : IWeatherService
	{
		// Token: 0x060052A5 RID: 21157 RVA: 0x0010B5E5 File Offset: 0x001097E5
		public WeatherService(IWeatherConfigurationCache weatherConfigurationCache)
		{
			this.weatherConfigurationCache = weatherConfigurationCache;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0010B5F4 File Offset: 0x001097F4
		public string Get(Uri weatherServiceUri)
		{
			string result;
			using (WebClient webClient = new WebClient())
			{
				result = webClient.DownloadString(weatherServiceUri);
			}
			return result;
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0010B62C File Offset: 0x0010982C
		public void VerifyServiceAvailability(CallContext callContext)
		{
			if (!VariantConfiguration.GetSnapshot(callContext.AccessingADUser.GetContext(null), null, null).OwaClientServer.Weather.Enabled || !this.weatherConfigurationCache.IsFeatureEnabled)
			{
				throw FaultExceptionUtilities.CreateFault(new WeatherServiceDisabledException(), FaultParty.Sender);
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x0010B679 File Offset: 0x00109879
		public string PartnerId
		{
			get
			{
				return "owa";
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x0010B680 File Offset: 0x00109880
		public string BaseUrl
		{
			get
			{
				return this.weatherConfigurationCache.WeatherServiceUrl;
			}
		}

		// Token: 0x04002E08 RID: 11784
		private const string PartnerIdValue = "owa";

		// Token: 0x04002E09 RID: 11785
		private readonly IWeatherConfigurationCache weatherConfigurationCache;
	}
}
