using System;
using System.Diagnostics;
using System.Web;
using Microsoft.Exchange.Autodiscover;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.AutodiscoverV2;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000006 RID: 6
	public class AutoDiscoverV2
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002527 File Offset: 0x00000727
		internal AutoDiscoverV2(RequestDetailsLogger logger, IFlightSettingRepository flightSettings = null, ITenantRepository tenantRepository = null)
		{
			this.logger = logger;
			this.flightSettings = flightSettings;
			this.tenantRepository = tenantRepository;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002544 File Offset: 0x00000744
		internal static TResult TrackLatency<TResult>(IFlightSettingRepository flightSettingRepository, RequestDetailsLogger logger, Func<TResult> method, string keyToLog)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TResult result;
			try
			{
				result = method();
			}
			finally
			{
				stopwatch.Stop();
				logger.AppendGenericInfo(keyToLog, stopwatch.ElapsedMilliseconds);
				ExTraceGlobals.LatencyTracer.TraceDebug<string>((long)keyToLog.GetHashCode(), string.Format("{0} - {1}", keyToLog, stopwatch.ElapsedMilliseconds), null);
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025B8 File Offset: 0x000007B8
		internal AutoDiscoverV2Response ProcessRequest(AutoDiscoverV2Request request, IFlightSettingRepository flightSettings)
		{
			return this.ProcessRequest(request, flightSettings, new TenantRepository(this.logger));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D0 File Offset: 0x000007D0
		internal AutoDiscoverV2Response ProcessRequest(AutoDiscoverV2Request request, IFlightSettingRepository flightSettings, ITenantRepository tenantRepository)
		{
			this.flightSettings = flightSettings;
			this.tenantRepository = tenantRepository;
			return this.Execute(request, tenantRepository);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025F8 File Offset: 0x000007F8
		internal AutoDiscoverV2Request CreateRequestFromContext(HttpContextBase context, string emailAddressFromUrl)
		{
			AutoDiscoverV2Request autoDiscoverV2Request = new AutoDiscoverV2Request();
			string protocol = context.Request.Params["Protocol"];
			uint redirectCount = 0U;
			uint.TryParse(context.Request.Params["RedirectCount"], out redirectCount);
			autoDiscoverV2Request.ValidateRequest(emailAddressFromUrl, protocol, redirectCount, this.logger);
			autoDiscoverV2Request.EmailAddress = SmtpAddress.Parse(emailAddressFromUrl);
			autoDiscoverV2Request.Protocol = protocol;
			autoDiscoverV2Request.HostNameHint = context.Request.Params["HostNameHint"];
			autoDiscoverV2Request.RedirectCount = redirectCount;
			return autoDiscoverV2Request;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002687 File Offset: 0x00000887
		private AutoDiscoverV2Response Execute(AutoDiscoverV2Request request, ITenantRepository tenantRepository)
		{
			this.logger.AppendGenericInfo("IsOnPrem", "true");
			return this.ExecuteOnPremEndFlow(request);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A8 File Offset: 0x000008A8
		private AutoDiscoverV2Response GetResourceUrlResponse(string hostName, string protocol)
		{
			string resourceUrl = ResourceUrlBuilder.GetResourceUrl(protocol, hostName);
			this.logger.AppendGenericInfo("GetResourceUrlResponse", resourceUrl);
			return new AutoDiscoverV2Response
			{
				ProtocolName = protocol,
				Url = resourceUrl
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026E8 File Offset: 0x000008E8
		private AutoDiscoverV2Response ExecuteOnPremEndFlow(AutoDiscoverV2Request request)
		{
			this.logger.AppendGenericInfo("ExecuteOnPremEndFlow", "OnPrem");
			ADRecipient onPremUser = this.tenantRepository.GetOnPremUser(request.EmailAddress);
			if (onPremUser == null)
			{
				this.logger.AppendGenericInfo("TryGetEmailRedirectResponse", "UserNotFound");
				IAutodMiniRecipient nextUserFromSortedList = this.tenantRepository.GetNextUserFromSortedList(request.EmailAddress);
				if (nextUserFromSortedList != null)
				{
					this.logger.AppendGenericInfo("TryGetEmailRedirectResponse", "FoundRandomUser");
					AutoDiscoverV2Response result;
					if (this.TryGetEmailRedirectResponse(request, nextUserFromSortedList, out result))
					{
						return result;
					}
				}
				return this.GetResourceUrlResponse(this.flightSettings.GetHostNameFromVdir(null, request.Protocol), request.Protocol);
			}
			AutoDiscoverV2Response result2;
			if (this.TryGetEmailRedirectResponse(request, onPremUser, out result2))
			{
				return result2;
			}
			return this.GetResourceUrlResponse(this.flightSettings.GetHostNameFromVdir(null, request.Protocol), request.Protocol);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027B8 File Offset: 0x000009B8
		private bool TryGetEmailRedirectResponse(AutoDiscoverV2Request request, IAutodMiniRecipient recipient, out AutoDiscoverV2Response redirectResponse)
		{
			redirectResponse = null;
			if (recipient == null)
			{
				return false;
			}
			this.logger.AppendGenericInfo("TryGetEmailRedirectResponseUserFound", recipient.RecipientType);
			if (recipient.ExternalEmailAddress != null && recipient.ExternalEmailAddress.AddressString != null && recipient.ExternalEmailAddress.PrefixString == "SMTP")
			{
				this.logger.AppendGenericInfo("TryGetEmailRedirectResponse", string.Format("ExternalEmailAddressFound - {0}", recipient.ExternalEmailAddress.AddressString));
				redirectResponse = ResourceUrlBuilder.GetRedirectResponse(this.logger, "outlook.office365.com", recipient.ExternalEmailAddress.AddressString, request.Protocol, request.RedirectCount, null);
				return true;
			}
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000286C File Offset: 0x00000A6C
		private bool TryGetEmailRedirectResponse(AutoDiscoverV2Request request, ADRecipient recipient, out AutoDiscoverV2Response redirectResponse)
		{
			redirectResponse = null;
			if (recipient == null)
			{
				return false;
			}
			this.logger.AppendGenericInfo("ADUserFound", recipient.RecipientType);
			if (recipient.ExternalEmailAddress != null && recipient.ExternalEmailAddress.AddressString != null && recipient.ExternalEmailAddress.PrefixString == "SMTP")
			{
				this.logger.AppendGenericInfo("TryGetEmailRedirectResponse", string.Format("ExternalEmailAddressFound - {0}", recipient.ExternalEmailAddress.AddressString + " " + request.EmailAddress.Address));
				redirectResponse = ResourceUrlBuilder.GetRedirectResponse(this.logger, "outlook.office365.com", recipient.ExternalEmailAddress.AddressString, request.Protocol, request.RedirectCount, null);
				return true;
			}
			return false;
		}

		// Token: 0x04000004 RID: 4
		private readonly RequestDetailsLogger logger;

		// Token: 0x04000005 RID: 5
		private IFlightSettingRepository flightSettings;

		// Token: 0x04000006 RID: 6
		private ITenantRepository tenantRepository;
	}
}
