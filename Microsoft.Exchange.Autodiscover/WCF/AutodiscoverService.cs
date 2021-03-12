using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200005E RID: 94
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class AutodiscoverService : IAutodiscover
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000122A0 File Offset: 0x000104A0
		public GetUserSettingsResponseMessage GetUserSettings(GetUserSettingsRequestMessage message)
		{
			return this.Execute<GetUserSettingsResponseMessage>(message);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000122A9 File Offset: 0x000104A9
		public GetDomainSettingsResponseMessage GetDomainSettings(GetDomainSettingsRequestMessage message)
		{
			return this.Execute<GetDomainSettingsResponseMessage>(message);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000122B2 File Offset: 0x000104B2
		public GetFederationInformationResponseMessage GetFederationInformation(GetFederationInformationRequestMessage message)
		{
			return this.Execute<GetFederationInformationResponseMessage>(message);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000122BB File Offset: 0x000104BB
		public GetOrganizationRelationshipSettingsResponseMessage GetOrganizationRelationshipSettings(GetOrganizationRelationshipSettingsRequestMessage message)
		{
			return this.Execute<GetOrganizationRelationshipSettingsResponseMessage>(message);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012304 File Offset: 0x00010504
		private TResponse Execute<TResponse>(AutodiscoverRequestMessage message) where TResponse : AutodiscoverResponseMessage
		{
			TResponse response = default(TResponse);
			Common.SendWatsonReportOnUnhandledException(delegate
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.CoreExecutionLatency, delegate()
				{
					response = (TResponse)((object)message.Execute());
				});
			});
			return response;
		}
	}
}
