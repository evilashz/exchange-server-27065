using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Online.Administration.WebService;

// Token: 0x02000402 RID: 1026
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class ProvisioningWebServiceClient : ClientBase<IProvisioningWebService>, IProvisioningWebService
{
	// Token: 0x060019B4 RID: 6580 RVA: 0x0008D8F7 File Offset: 0x0008BAF7
	public ProvisioningWebServiceClient()
	{
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x0008D8FF File Offset: 0x0008BAFF
	public ProvisioningWebServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x0008D908 File Offset: 0x0008BB08
	public ProvisioningWebServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x0008D912 File Offset: 0x0008BB12
	public ProvisioningWebServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x0008D91C File Offset: 0x0008BB1C
	public ProvisioningWebServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x0008D926 File Offset: 0x0008BB26
	public Response RemoveServicePrincipalBySpn(RemoveServicePrincipalBySpnRequest request)
	{
		return base.Channel.RemoveServicePrincipalBySpn(request);
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x0008D934 File Offset: 0x0008BB34
	public GetServicePrincipalResponse GetServicePrincipal(GetServicePrincipalRequest request)
	{
		return base.Channel.GetServicePrincipal(request);
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x0008D942 File Offset: 0x0008BB42
	public GetServicePrincipalByAppPrincipalIdResponse GetServicePrincipalByAppPrincipalId(GetServicePrincipalByAppPrincipalIdRequest request)
	{
		return base.Channel.GetServicePrincipalByAppPrincipalId(request);
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x0008D950 File Offset: 0x0008BB50
	public GetServicePrincipalBySpnResponse GetServicePrincipalBySpn(GetServicePrincipalBySpnRequest request)
	{
		return base.Channel.GetServicePrincipalBySpn(request);
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x0008D95E File Offset: 0x0008BB5E
	public Response RemoveServicePrincipalCredentials(RemoveServicePrincipalCredentialsRequest request)
	{
		return base.Channel.RemoveServicePrincipalCredentials(request);
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x0008D96C File Offset: 0x0008BB6C
	public Response RemoveServicePrincipalCredentialsBySpn(RemoveServicePrincipalCredentialsBySpnRequest request)
	{
		return base.Channel.RemoveServicePrincipalCredentialsBySpn(request);
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x0008D97A File Offset: 0x0008BB7A
	public Response RemoveServicePrincipalCredentialsByAppPrincipalId(RemoveServicePrincipalCredentialsByAppPrincipalIdRequest request)
	{
		return base.Channel.RemoveServicePrincipalCredentialsByAppPrincipalId(request);
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x0008D988 File Offset: 0x0008BB88
	public Response SetServicePrincipal(SetServicePrincipalRequest request)
	{
		return base.Channel.SetServicePrincipal(request);
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x0008D996 File Offset: 0x0008BB96
	public Response SetServicePrincipalByAppPrincipalId(SetServicePrincipalByAppPrincipalIdRequest request)
	{
		return base.Channel.SetServicePrincipalByAppPrincipalId(request);
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
	public Response SetServicePrincipalBySpn(SetServicePrincipalBySpnRequest request)
	{
		return base.Channel.SetServicePrincipalBySpn(request);
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0008D9B2 File Offset: 0x0008BBB2
	public Response SetServicePrincipalName(SetServicePrincipalNameRequest request)
	{
		return base.Channel.SetServicePrincipalName(request);
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x0008D9C0 File Offset: 0x0008BBC0
	public ListServicePrincipalsResponse ListServicePrincipals(ListServicePrincipalsRequest request)
	{
		return base.Channel.ListServicePrincipals(request);
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x0008D9CE File Offset: 0x0008BBCE
	public NavigateListServicePrincipalsResponse NavigateListServicePrincipals(NavigateListServicePrincipalsRequest request)
	{
		return base.Channel.NavigateListServicePrincipals(request);
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x0008D9DC File Offset: 0x0008BBDC
	public ListServicePrincipalCredentialsResponse ListServicePrincipalCredentials(ListServicePrincipalCredentialsRequest request)
	{
		return base.Channel.ListServicePrincipalCredentials(request);
	}

	// Token: 0x060019C7 RID: 6599 RVA: 0x0008D9EA File Offset: 0x0008BBEA
	public ListServicePrincipalCredentialsByAppPrincipalIdResponse ListServicePrincipalCredentialsByAppPrincipalId(ListServicePrincipalCredentialsByAppPrincipalIdRequest request)
	{
		return base.Channel.ListServicePrincipalCredentialsByAppPrincipalId(request);
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0008D9F8 File Offset: 0x0008BBF8
	public ListServicePrincipalCredentialsBySpnResponse ListServicePrincipalCredentialsBySpn(ListServicePrincipalCredentialsBySpnRequest request)
	{
		return base.Channel.ListServicePrincipalCredentialsBySpn(request);
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x0008DA06 File Offset: 0x0008BC06
	public NavigateGroupMemberResultsResponse NavigateGroupMemberResults(NavigateGroupMemberResultsRequest request)
	{
		return base.Channel.NavigateGroupMemberResults(request);
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x0008DA14 File Offset: 0x0008BC14
	public GetRoleResponse GetRole(GetRoleRequest request)
	{
		return base.Channel.GetRole(request);
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x0008DA22 File Offset: 0x0008BC22
	public GetRoleByNameResponse GetRoleByName(GetRoleByNameRequest request)
	{
		return base.Channel.GetRoleByName(request);
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x0008DA30 File Offset: 0x0008BC30
	public ListRolesResponse ListRoles(Request request)
	{
		return base.Channel.ListRoles(request);
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x0008DA3E File Offset: 0x0008BC3E
	public ListRolesForUserResponse ListRolesForUser(ListRolesForUserRequest request)
	{
		return base.Channel.ListRolesForUser(request);
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x0008DA4C File Offset: 0x0008BC4C
	public ListRolesForUserByUpnResponse ListRolesForUserByUpn(ListRolesForUserByUpnRequest request)
	{
		return base.Channel.ListRolesForUserByUpn(request);
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x0008DA5A File Offset: 0x0008BC5A
	public Response AddRoleMembers(AddRoleMembersRequest request)
	{
		return base.Channel.AddRoleMembers(request);
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x0008DA68 File Offset: 0x0008BC68
	public Response AddRoleMembersByRoleName(AddRoleMembersByRoleNameRequest request)
	{
		return base.Channel.AddRoleMembersByRoleName(request);
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x0008DA76 File Offset: 0x0008BC76
	public Response RemoveRoleMembers(RemoveRoleMembersRequest request)
	{
		return base.Channel.RemoveRoleMembers(request);
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x0008DA84 File Offset: 0x0008BC84
	public Response RemoveRoleMembersByRoleName(RemoveRoleMembersByRoleNameRequest request)
	{
		return base.Channel.RemoveRoleMembersByRoleName(request);
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x0008DA92 File Offset: 0x0008BC92
	public ListRoleMembersResponse ListRoleMembers(ListRoleMembersRequest request)
	{
		return base.Channel.ListRoleMembers(request);
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x0008DAA0 File Offset: 0x0008BCA0
	public NavigateRoleMemberResultsResponse NavigateRoleMemberResults(NavigateRoleMemberResultsRequest request)
	{
		return base.Channel.NavigateRoleMemberResults(request);
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x0008DAAE File Offset: 0x0008BCAE
	public AddUserResponse AddUser(AddUserRequest request)
	{
		return base.Channel.AddUser(request);
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x0008DABC File Offset: 0x0008BCBC
	public Response RemoveUser(RemoveUserRequest request)
	{
		return base.Channel.RemoveUser(request);
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0008DACA File Offset: 0x0008BCCA
	public Response RemoveUserByUpn(RemoveUserByUpnRequest request)
	{
		return base.Channel.RemoveUserByUpn(request);
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x0008DAD8 File Offset: 0x0008BCD8
	public Response SetUser(SetUserRequest request)
	{
		return base.Channel.SetUser(request);
	}

	// Token: 0x060019D9 RID: 6617 RVA: 0x0008DAE6 File Offset: 0x0008BCE6
	public ChangeUserPrincipalNameResponse ChangeUserPrincipalName(ChangeUserPrincipalNameRequest request)
	{
		return base.Channel.ChangeUserPrincipalName(request);
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x0008DAF4 File Offset: 0x0008BCF4
	public ChangeUserPrincipalNameByUpnResponse ChangeUserPrincipalNameByUpn(ChangeUserPrincipalNameByUpnRequest request)
	{
		return base.Channel.ChangeUserPrincipalNameByUpn(request);
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x0008DB02 File Offset: 0x0008BD02
	public ResetUserPasswordResponse ResetUserPassword(ResetUserPasswordRequest request)
	{
		return base.Channel.ResetUserPassword(request);
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x0008DB10 File Offset: 0x0008BD10
	public ResetUserPasswordByUpnResponse ResetUserPasswordByUpn(ResetUserPasswordByUpnRequest request)
	{
		return base.Channel.ResetUserPasswordByUpn(request);
	}

	// Token: 0x060019DD RID: 6621 RVA: 0x0008DB1E File Offset: 0x0008BD1E
	public GetUserResponse GetUser(GetUserRequest request)
	{
		return base.Channel.GetUser(request);
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x0008DB2C File Offset: 0x0008BD2C
	public GetUserByUpnResponse GetUserByUpn(GetUserByUpnRequest request)
	{
		return base.Channel.GetUserByUpn(request);
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x0008DB3A File Offset: 0x0008BD3A
	public GetUserByLiveIdResponse GetUserByLiveId(GetUserByLiveIdRequest request)
	{
		return base.Channel.GetUserByLiveId(request);
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x0008DB48 File Offset: 0x0008BD48
	public ListUsersResponse ListUsers(ListUsersRequest request)
	{
		return base.Channel.ListUsers(request);
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x0008DB56 File Offset: 0x0008BD56
	public NavigateUserResultsResponse NavigateUserResults(NavigateUserResultsRequest request)
	{
		return base.Channel.NavigateUserResults(request);
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x0008DB64 File Offset: 0x0008BD64
	public Response SetUserLicenses(SetUserLicensesRequest request)
	{
		return base.Channel.SetUserLicenses(request);
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x0008DB72 File Offset: 0x0008BD72
	public Response SetUserLicensesByUpn(SetUserLicensesByUpnRequest request)
	{
		return base.Channel.SetUserLicensesByUpn(request);
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x0008DB80 File Offset: 0x0008BD80
	public ConvertFederatedUserToManagedResponse ConvertFederatedUserToManaged(ConvertFederatedUserToManagedRequest request)
	{
		return base.Channel.ConvertFederatedUserToManaged(request);
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x0008DB8E File Offset: 0x0008BD8E
	public RestoreUserResponse RestoreUser(RestoreUserRequest request)
	{
		return base.Channel.RestoreUser(request);
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x0008DB9C File Offset: 0x0008BD9C
	public RestoreUserByUpnResponse RestoreUserByUpn(RestoreUserByUpnRequest request)
	{
		return base.Channel.RestoreUserByUpn(request);
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x0008DBAA File Offset: 0x0008BDAA
	public AddServicePrincipalResponse AddServicePrincipal(AddServicePrincipalRequest request)
	{
		return base.Channel.AddServicePrincipal(request);
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x0008DBB8 File Offset: 0x0008BDB8
	public Response AddServicePrincipalCredentials(AddServicePrincipalCredentialsRequest request)
	{
		return base.Channel.AddServicePrincipalCredentials(request);
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x0008DBC6 File Offset: 0x0008BDC6
	public Response AddServicePrincipalCredentialsBySpn(AddServicePrincipalCredentialsBySpnRequest request)
	{
		return base.Channel.AddServicePrincipalCredentialsBySpn(request);
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x0008DBD4 File Offset: 0x0008BDD4
	public Response AddServicePrincipalCredentialsByAppPrincipalId(AddServicePrincipalCredentialsByAppPrincipalIdRequest request)
	{
		return base.Channel.AddServicePrincipalCredentialsByAppPrincipalId(request);
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x0008DBE2 File Offset: 0x0008BDE2
	public Response RemoveServicePrincipal(RemoveServicePrincipalRequest request)
	{
		return base.Channel.RemoveServicePrincipal(request);
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x0008DBF0 File Offset: 0x0008BDF0
	public Response RemoveServicePrincipalByAppPrincipalId(RemoveServicePrincipalByAppPrincipalIdRequest request)
	{
		return base.Channel.RemoveServicePrincipalByAppPrincipalId(request);
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x0008DBFE File Offset: 0x0008BDFE
	public GetHeaderInfoResponse GetHeaderInfo(Request request)
	{
		return base.Channel.GetHeaderInfo(request);
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0008DC0C File Offset: 0x0008BE0C
	public MsolConnectResponse MsolConnect(Request request)
	{
		return base.Channel.MsolConnect(request);
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x0008DC1A File Offset: 0x0008BE1A
	public GetPartnerInformationResponse GetPartnerInformation(Request request)
	{
		return base.Channel.GetPartnerInformation(request);
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x0008DC28 File Offset: 0x0008BE28
	public GetCompanyInformationResponse GetCompanyInformation(Request request)
	{
		return base.Channel.GetCompanyInformation(request);
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0008DC36 File Offset: 0x0008BE36
	public GetSubscriptionResponse GetSubscription(GetSubscriptionRequest request)
	{
		return base.Channel.GetSubscription(request);
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x0008DC44 File Offset: 0x0008BE44
	public ListSubscriptionsResponse ListSubscriptions(Request request)
	{
		return base.Channel.ListSubscriptions(request);
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x0008DC52 File Offset: 0x0008BE52
	public ListAccountSkusResponse ListAccountSkus(ListAccountSkusRequest request)
	{
		return base.Channel.ListAccountSkus(request);
	}

	// Token: 0x060019F4 RID: 6644 RVA: 0x0008DC60 File Offset: 0x0008BE60
	public Response SetPartnerInformation(SetPartnerInformationRequest request)
	{
		return base.Channel.SetPartnerInformation(request);
	}

	// Token: 0x060019F5 RID: 6645 RVA: 0x0008DC6E File Offset: 0x0008BE6E
	public Response SetCompanyContactInformation(SetCompanyContactInformationRequest request)
	{
		return base.Channel.SetCompanyContactInformation(request);
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x0008DC7C File Offset: 0x0008BE7C
	public Response SetCompanyDirSyncEnabled(SetCompanyDirSyncEnabledRequest request)
	{
		return base.Channel.SetCompanyDirSyncEnabled(request);
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x0008DC8A File Offset: 0x0008BE8A
	public Response SetCompanySettings(SetCompanySettingsRequest request)
	{
		return base.Channel.SetCompanySettings(request);
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x0008DC98 File Offset: 0x0008BE98
	public ListPartnerContractsResponse ListPartnerContracts(ListPartnerContractsRequest request)
	{
		return base.Channel.ListPartnerContracts(request);
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x0008DCA6 File Offset: 0x0008BEA6
	public NavigatePartnerContractsResponse NavigatePartnerContracts(NavigatePartnerContractsRequest request)
	{
		return base.Channel.NavigatePartnerContracts(request);
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x0008DCB4 File Offset: 0x0008BEB4
	public Response RemoveContact(RemoveContactRequest request)
	{
		return base.Channel.RemoveContact(request);
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0008DCC2 File Offset: 0x0008BEC2
	public GetContactResponse GetContact(GetContactRequest request)
	{
		return base.Channel.GetContact(request);
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x0008DCD0 File Offset: 0x0008BED0
	public ListContactsResponse ListContacts(ListContactsRequest request)
	{
		return base.Channel.ListContacts(request);
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x0008DCDE File Offset: 0x0008BEDE
	public NavigateContactResultsResponse NavigateContactResults(NavigateContactResultsRequest request)
	{
		return base.Channel.NavigateContactResults(request);
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0008DCEC File Offset: 0x0008BEEC
	public AddDomainResponse AddDomain(AddDomainRequest request)
	{
		return base.Channel.AddDomain(request);
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x0008DCFA File Offset: 0x0008BEFA
	public Response VerifyDomain(VerifyDomainRequest request)
	{
		return base.Channel.VerifyDomain(request);
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x0008DD08 File Offset: 0x0008BF08
	public Response RemoveDomain(RemoveDomainRequest request)
	{
		return base.Channel.RemoveDomain(request);
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x0008DD16 File Offset: 0x0008BF16
	public Response SetDomain(SetDomainRequest request)
	{
		return base.Channel.SetDomain(request);
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x0008DD24 File Offset: 0x0008BF24
	public Response SetDomainAuthentication(SetDomainAuthenticationRequest request)
	{
		return base.Channel.SetDomainAuthentication(request);
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x0008DD32 File Offset: 0x0008BF32
	public GetDomainFederationSettingsResponse GetDomainFederationSettings(GetDomainFederationSettingsRequest request)
	{
		return base.Channel.GetDomainFederationSettings(request);
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x0008DD40 File Offset: 0x0008BF40
	public Response SetDomainFederationSettings(SetDomainFederationSettingsRequest request)
	{
		return base.Channel.SetDomainFederationSettings(request);
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0008DD4E File Offset: 0x0008BF4E
	public GetDomainResponse GetDomain(GetDomainRequest request)
	{
		return base.Channel.GetDomain(request);
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x0008DD5C File Offset: 0x0008BF5C
	public ListDomainsResponse ListDomains(ListDomainsRequest request)
	{
		return base.Channel.ListDomains(request);
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x0008DD6A File Offset: 0x0008BF6A
	public GetDomainVerificationDnsResponse GetDomainVerificationDns(GetDomainVerificationDnsRequest request)
	{
		return base.Channel.GetDomainVerificationDns(request);
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x0008DD78 File Offset: 0x0008BF78
	public AddGroupResponse AddGroup(AddGroupRequest request)
	{
		return base.Channel.AddGroup(request);
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0008DD86 File Offset: 0x0008BF86
	public Response RemoveGroup(RemoveGroupRequest request)
	{
		return base.Channel.RemoveGroup(request);
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0008DD94 File Offset: 0x0008BF94
	public Response SetGroup(SetGroupRequest request)
	{
		return base.Channel.SetGroup(request);
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x0008DDA2 File Offset: 0x0008BFA2
	public GetGroupResponse GetGroup(GetGroupRequest request)
	{
		return base.Channel.GetGroup(request);
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x0008DDB0 File Offset: 0x0008BFB0
	public ListGroupsResponse ListGroups(ListGroupsRequest request)
	{
		return base.Channel.ListGroups(request);
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x0008DDBE File Offset: 0x0008BFBE
	public NavigateGroupResultsResponse NavigateGroupResults(NavigateGroupResultsRequest request)
	{
		return base.Channel.NavigateGroupResults(request);
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x0008DDCC File Offset: 0x0008BFCC
	public Response AddGroupMembers(AddGroupMembersRequest request)
	{
		return base.Channel.AddGroupMembers(request);
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x0008DDDA File Offset: 0x0008BFDA
	public Response RemoveGroupMembers(RemoveGroupMembersRequest request)
	{
		return base.Channel.RemoveGroupMembers(request);
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x0008DDE8 File Offset: 0x0008BFE8
	public ListGroupMembersResponse ListGroupMembers(ListGroupMembersRequest request)
	{
		return base.Channel.ListGroupMembers(request);
	}
}
