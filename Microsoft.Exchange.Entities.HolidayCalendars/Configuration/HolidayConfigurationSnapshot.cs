using System;
using Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions;
using Microsoft.Exchange.HolidayCalendars;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000014 RID: 20
	internal class HolidayConfigurationSnapshot
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000029A0 File Offset: 0x00000BA0
		public HolidayConfigurationSnapshot(VariantConfigurationSnapshot configurationSnapshot) : this(configurationSnapshot.HolidayCalendars.HostConfiguration)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029C1 File Offset: 0x00000BC1
		public HolidayConfigurationSnapshot(IHostSettings hostSettings)
		{
			this.hostSettings = hostSettings;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000029D0 File Offset: 0x00000BD0
		public Uri CalendarEndpoint
		{
			get
			{
				if (this.endpointUrl != null)
				{
					return this.endpointUrl;
				}
				if (string.IsNullOrWhiteSpace(this.hostSettings.Endpoint))
				{
					throw new NoEndpointConfigurationFoundException("Endpoint configuration is not available. Endpoint: '{0}'", new object[]
					{
						this.hostSettings.Endpoint
					});
				}
				string endpoint = this.hostSettings.Endpoint;
				if (!Uri.IsWellFormedUriString(endpoint, UriKind.Absolute))
				{
					throw new InvalidHolidayCalendarEndpointUrlException("User is enabled without valid EndPoint configuration URL. {0}", new object[]
					{
						endpoint
					});
				}
				this.endpointUrl = new Uri(endpoint);
				return this.endpointUrl;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A63 File Offset: 0x00000C63
		public int ConfigurationFetchTimeout
		{
			get
			{
				return this.hostSettings.Timeout;
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly IHostSettings hostSettings;

		// Token: 0x0400001B RID: 27
		private Uri endpointUrl;
	}
}
