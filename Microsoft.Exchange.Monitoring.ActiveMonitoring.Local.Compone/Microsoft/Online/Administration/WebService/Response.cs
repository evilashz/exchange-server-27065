using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200031A RID: 794
	[KnownType(typeof(GetSubscriptionResponse))]
	[KnownType(typeof(GetRoleByNameResponse))]
	[KnownType(typeof(ListRolesResponse))]
	[KnownType(typeof(ListRolesForUserResponse))]
	[KnownType(typeof(ListRolesForUserByUpnResponse))]
	[KnownType(typeof(ListRoleMembersResponse))]
	[KnownType(typeof(NavigateRoleMemberResultsResponse))]
	[KnownType(typeof(AddUserResponse))]
	[KnownType(typeof(ChangeUserPrincipalNameResponse))]
	[KnownType(typeof(ChangeUserPrincipalNameByUpnResponse))]
	[KnownType(typeof(ResetUserPasswordResponse))]
	[KnownType(typeof(ResetUserPasswordByUpnResponse))]
	[KnownType(typeof(GetUserResponse))]
	[KnownType(typeof(GetUserByUpnResponse))]
	[KnownType(typeof(GetUserByLiveIdResponse))]
	[KnownType(typeof(ListUsersResponse))]
	[KnownType(typeof(NavigateUserResultsResponse))]
	[KnownType(typeof(ConvertFederatedUserToManagedResponse))]
	[KnownType(typeof(RestoreUserResponse))]
	[KnownType(typeof(GetRoleResponse))]
	[KnownType(typeof(AddServicePrincipalResponse))]
	[KnownType(typeof(GetHeaderInfoResponse))]
	[KnownType(typeof(MsolConnectResponse))]
	[KnownType(typeof(GetPartnerInformationResponse))]
	[KnownType(typeof(GetCompanyInformationResponse))]
	[DebuggerStepThrough]
	[KnownType(typeof(ListSubscriptionsResponse))]
	[KnownType(typeof(ListAccountSkusResponse))]
	[KnownType(typeof(ListPartnerContractsResponse))]
	[KnownType(typeof(NavigatePartnerContractsResponse))]
	[KnownType(typeof(GetContactResponse))]
	[KnownType(typeof(ListContactsResponse))]
	[KnownType(typeof(NavigateContactResultsResponse))]
	[KnownType(typeof(AddDomainResponse))]
	[KnownType(typeof(GetDomainFederationSettingsResponse))]
	[KnownType(typeof(GetDomainResponse))]
	[KnownType(typeof(ListDomainsResponse))]
	[KnownType(typeof(GetDomainVerificationDnsResponse))]
	[KnownType(typeof(AddGroupResponse))]
	[KnownType(typeof(GetGroupResponse))]
	[KnownType(typeof(ListGroupsResponse))]
	[KnownType(typeof(NavigateGroupResultsResponse))]
	[KnownType(typeof(ListGroupMembersResponse))]
	[KnownType(typeof(NavigateGroupMemberResultsResponse))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Response", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(GetServicePrincipalResponse))]
	[KnownType(typeof(GetServicePrincipalByAppPrincipalIdResponse))]
	[KnownType(typeof(GetServicePrincipalBySpnResponse))]
	[KnownType(typeof(RestoreUserByUpnResponse))]
	[KnownType(typeof(ListServicePrincipalsResponse))]
	[KnownType(typeof(NavigateListServicePrincipalsResponse))]
	[KnownType(typeof(ListServicePrincipalCredentialsResponse))]
	[KnownType(typeof(ListServicePrincipalCredentialsByAppPrincipalIdResponse))]
	[KnownType(typeof(ListServicePrincipalCredentialsBySpnResponse))]
	public class Response : IExtensibleDataObject
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0008B65F File Offset: 0x0008985F
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0008B667 File Offset: 0x00089867
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x04000FAD RID: 4013
		private ExtensionDataObject extensionDataField;
	}
}
