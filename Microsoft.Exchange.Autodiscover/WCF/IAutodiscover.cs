using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200005D RID: 93
	[ServiceContract(Name = "Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public interface IAutodiscover
	{
		// Token: 0x060002A9 RID: 681
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings")]
		GetUserSettingsResponseMessage GetUserSettings(GetUserSettingsRequestMessage message);

		// Token: 0x060002AA RID: 682
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetDomainSettings")]
		GetDomainSettingsResponseMessage GetDomainSettings(GetDomainSettingsRequestMessage message);

		// Token: 0x060002AB RID: 683
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetFederationInformation")]
		GetFederationInformationResponseMessage GetFederationInformation(GetFederationInformationRequestMessage message);

		// Token: 0x060002AC RID: 684
		[OperationContract(Action = "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetOrganizationRelationshipSettings")]
		GetOrganizationRelationshipSettingsResponseMessage GetOrganizationRelationshipSettings(GetOrganizationRelationshipSettingsRequestMessage message);
	}
}
