using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200000A RID: 10
	internal interface IADSystemConfigurationLookup
	{
		// Token: 0x060000A2 RID: 162
		UMDialPlan GetDialPlanFromId(ADObjectId dialPlanId);

		// Token: 0x060000A3 RID: 163
		UMDialPlan GetDialPlanFromRecipient(IADRecipient recipient);

		// Token: 0x060000A4 RID: 164
		UMDialPlan GetDialPlanFromPilotIdentifier(string pilotIdentifier);

		// Token: 0x060000A5 RID: 165
		UMIPGateway GetIPGatewayFromId(ADObjectId gatewayId);

		// Token: 0x060000A6 RID: 166
		ExchangeConfigurationUnit GetConfigurationUnitByTenantGuid(Guid tenantGuid);

		// Token: 0x060000A7 RID: 167
		IEnumerable<UMIPGateway> GetAllGlobalGateways();

		// Token: 0x060000A8 RID: 168
		IEnumerable<UMDialPlan> GetAllDialPlans();

		// Token: 0x060000A9 RID: 169
		UMIPGateway GetIPGatewayFromAddress(IList<string> fqdns);

		// Token: 0x060000AA RID: 170
		IEnumerable<UMIPGateway> GetAllIPGateways();

		// Token: 0x060000AB RID: 171
		IEnumerable<UMIPGateway> GetGatewaysLinkedToDialPlan(UMDialPlan dialPlan);

		// Token: 0x060000AC RID: 172
		UMAutoAttendant GetAutoAttendantFromId(ADObjectId autoAttendantId);

		// Token: 0x060000AD RID: 173
		UMAutoAttendant GetAutoAttendantFromPilotIdentifierAndDialPlan(string pilotIdentifier, ADObjectId dialPlanId);

		// Token: 0x060000AE RID: 174
		UMAutoAttendant GetAutoAttendantWithNoDialplanInformation(string pilotIdentifier);

		// Token: 0x060000AF RID: 175
		OrganizationId GetOrganizationIdFromDomainName(string domainName, out bool isAuthoritative);

		// Token: 0x060000B0 RID: 176
		MicrosoftExchangeRecipient GetMicrosoftExchangeRecipient();

		// Token: 0x060000B1 RID: 177
		AcceptedDomain GetDefaultAcceptedDomain();

		// Token: 0x060000B2 RID: 178
		ExchangeConfigurationUnit GetConfigurationUnitByADObjectId(ADObjectId configUnit);

		// Token: 0x060000B3 RID: 179
		UMMailboxPolicy GetPolicyFromRecipient(ADRecipient recipient);

		// Token: 0x060000B4 RID: 180
		UMMailboxPolicy GetUMMailboxPolicyFromId(ADObjectId mbxPolicyId);

		// Token: 0x060000B5 RID: 181
		UMAutoAttendant GetAutoAttendantFromName(string autoAttendantName);

		// Token: 0x060000B6 RID: 182
		IEnumerable<Guid> GetAutoAttendantDialPlans();

		// Token: 0x060000B7 RID: 183
		void GetAutoAttendantAddressLists(HashSet<Guid> addressListGuids);

		// Token: 0x060000B8 RID: 184
		Guid GetExternalDirectoryOrganizationId();

		// Token: 0x060000B9 RID: 185
		void GetGlobalAddressLists(HashSet<Guid> addressListGuids);
	}
}
