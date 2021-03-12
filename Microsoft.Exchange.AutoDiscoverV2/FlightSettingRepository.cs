using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x0200000F RID: 15
	[ExcludeFromCodeCoverage]
	internal class FlightSettingRepository : IFlightSettingRepository
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C74 File Offset: 0x00000E74
		public string GetHostNameFromVdir(ADObjectId serverSiteId, string protocol)
		{
			SupportedProtocol supportedProtocol;
			Enum.TryParse<SupportedProtocol>(protocol, true, out supportedProtocol);
			Uri externalUrl;
			switch (supportedProtocol)
			{
			case SupportedProtocol.Unknown:
				throw AutoDiscoverResponseException.BadRequest("InvalidProtocol", string.Format("The given protocol value '{0}' is invalid. Supported values are '{1}'", protocol, "ActiveSync, Ews"), null);
			case SupportedProtocol.Rest:
				throw AutoDiscoverResponseException.NotFound("MailboxNotEnabledForRESTAPI", "REST API is not yet supported for this mailbox.", null);
			case SupportedProtocol.ActiveSync:
				externalUrl = GlobalServiceUrls.GetExternalUrl<MobileSyncService>();
				break;
			case SupportedProtocol.Ews:
				externalUrl = GlobalServiceUrls.GetExternalUrl<WebServicesService>();
				break;
			default:
				throw new ArgumentException("Invalid Protocol specified", protocol);
			}
			return externalUrl.Host;
		}
	}
}
