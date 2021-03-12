using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000055 RID: 85
	[ServiceKnownType("GetKnownTypes", typeof(KnownTypesProvider))]
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.Security.Authentication.FederatedAuthService.IAuthService")]
	internal interface IAuthService
	{
		// Token: 0x06000247 RID: 583
		[FaultContract(typeof(InvalidOperationException))]
		[OperationContract]
		[FaultContract(typeof(ArgumentException))]
		IntPtr LogonUserFederationCreds(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string userEndpoint, string userAgent, string userAddress, Guid requestId, out string iisLogMsg);

		// Token: 0x06000248 RID: 584
		[FaultContract(typeof(ArgumentException))]
		[FaultContract(typeof(InvalidOperationException))]
		[OperationContract]
		AuthStatus LogonCommonAccessTokenFederationCredsTest(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string userEndpoint, string userAgent, string userAddress, Guid requestId, bool? preferOfflineOrgId, TestFailoverFlags testFailOver, out string commonAccessToken, out string iisLogMsg);

		// Token: 0x06000249 RID: 585
		[FaultContract(typeof(InvalidOperationException))]
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(ArgumentException))]
		IAsyncResult BeginLogonUserFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, string remoteOrganizationContext, bool syncLocalAD, string userEndpoint, string userAgent, string userAddress, Guid requestId, AsyncCallback callback, object state);

		// Token: 0x0600024A RID: 586
		IntPtr EndLogonUserFederationCredsAsync(out string iisLogMsg, IAsyncResult ar);

		// Token: 0x0600024B RID: 587
		[OperationContract(AsyncPattern = true)]
		[FaultContract(typeof(InvalidOperationException))]
		[FaultContract(typeof(ArgumentException))]
		IAsyncResult BeginLogonCommonAccessTokenFederationCredsAsync(uint clientProcessId, byte[] remoteUserName, byte[] remotePassword, AuthOptions options, string remoteOrganizationContext, string userEndpoint, string userAgent, string userAddress, Guid requestId, AsyncCallback callback, object state);

		// Token: 0x0600024C RID: 588
		AuthStatus EndLogonCommonAccessTokenFederationCredsAsync(out string commonAccessToken, out string iisLogMsg, IAsyncResult ar);

		// Token: 0x0600024D RID: 589
		[FaultContract(typeof(ArgumentException))]
		[OperationContract]
		bool IsNego2AuthEnabledForDomain(string domain);

		// Token: 0x0600024E RID: 590
		[FaultContract(typeof(ArgumentException))]
		[FaultContract(typeof(InvalidOperationException))]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginIsNego2AuthEnabledForDomainAsync(string domain, AsyncCallback callback, object state);

		// Token: 0x0600024F RID: 591
		bool EndIsNego2AuthEnabledForDomainAsync(IAsyncResult ar);
	}
}
