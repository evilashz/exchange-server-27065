using System;
using System.Security;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa;
using Microsoft.Exchange.Net.MonitoringWebClient.Rws;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000781 RID: 1921
	internal interface ITestFactory
	{
		// Token: 0x0600260E RID: 9742
		ITestStep CreateOwaLoginScenario(Uri uri, string userName, string userDomain, SecureString password, OwaLoginParameters owaLoginParameters, ITestFactory testFactory);

		// Token: 0x0600260F RID: 9743
		ITestStep CreateOwaExternalLoginAgainstSpecificServerScenario(Uri uri, string userName, string userDomain, SecureString password, string serverToHit, ITestFactory testFactory);

		// Token: 0x06002610 RID: 9744
		ITestStep CreateOwaHealthCheckScenario(Uri uri, ITestFactory testFactory);

		// Token: 0x06002611 RID: 9745
		ITestStep CreateOwaStaticPageScenario(Uri uri, ITestFactory testFactory);

		// Token: 0x06002612 RID: 9746
		ITestStep CreateOwaFindPlacesScenario(Uri uri, string userName, string userDomain, SecureString password, ITestFactory testFactory);

		// Token: 0x06002613 RID: 9747
		ITestStep CreateOwaCertificateRevocationCheckScenario(Uri uri, ITestFactory testFactory);

		// Token: 0x06002614 RID: 9748
		ITestStep CreateEcpLoginScenario(Uri uri, string userName, string userDomain, SecureString password, ITestFactory testFactory);

		// Token: 0x06002615 RID: 9749
		ITestStep CreateEcpExternalLoginAgainstSpecificServerScenario(Uri uri, string userName, string userDomain, SecureString password, string serverToHit, ITestFactory testFactory);

		// Token: 0x06002616 RID: 9750
		ITestStep CreateEcpActiveMonitoringLocalScenario(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters, ITestFactory testFactory, Func<EcpStartPage, ITestStep> getFeatureTestStep);

		// Token: 0x06002617 RID: 9751
		ITestStep CreateEcpActiveMonitoringOutsideInScenario(Uri uri, string userName, SecureString password, ITestFactory testFactory, Func<EcpStartPage, ITestStep> getFeatureTestStep);

		// Token: 0x06002618 RID: 9752
		ITestStep CreateRwsCallScenario(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory testFactory);

		// Token: 0x06002619 RID: 9753
		ITestStep CreateAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory testFactory);

		// Token: 0x0600261A RID: 9754
		ITestStep CreateLiveIDAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory testFactory);

		// Token: 0x0600261B RID: 9755
		ITestStep CreateIisAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password);

		// Token: 0x0600261C RID: 9756
		ITestStep CreateFbaAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters);

		// Token: 0x0600261D RID: 9757
		ITestStep CreateBrickAuthenticateStep(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters);

		// Token: 0x0600261E RID: 9758
		ITestStep CreateAdfsAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AdfsLogonPage adfsLogonPage);

		// Token: 0x0600261F RID: 9759
		ITestStep CreateMeasureClientLatencyStep();

		// Token: 0x06002620 RID: 9760
		ITestStep CreateOwaStartPageStep(Uri uri);

		// Token: 0x06002621 RID: 9761
		ITestStep CreateLogoffStep(Uri uri, string logoffPath);

		// Token: 0x06002622 RID: 9762
		ITestStep CreateOwaSessionDataStep(Uri uri);

		// Token: 0x06002623 RID: 9763
		ITestStep CreateOwaWebServiceStep(Uri uri, string action);

		// Token: 0x06002624 RID: 9764
		ITestStep CreateOwaWebServiceStep(Uri uri, string action, RequestBody requestBody);

		// Token: 0x06002625 RID: 9765
		ITestStep CreateOwaPingStep(Uri uri);

		// Token: 0x06002626 RID: 9766
		ITestStep CreateOwaHealthCheckStep(Uri uri);

		// Token: 0x06002627 RID: 9767
		ITestStep CreateOwaDownloadStaticFileStep(Uri uri);

		// Token: 0x06002628 RID: 9768
		ITestStep CreateEstablishAffinityStep(Uri uri, string serverToHit);

		// Token: 0x06002629 RID: 9769
		ITestStep CreateEcpStartPageStep(Uri uri);

		// Token: 0x0600262A RID: 9770
		ITestStep CreateEcpWebServiceCallStep(Uri uri);

		// Token: 0x0600262B RID: 9771
		ITestStep CreateRwsAuthenticateStep(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory testFactory);

		// Token: 0x0600262C RID: 9772
		ITestStep CreateRwsBrickAuthenticateStep(CommonAccessToken token, Uri uri);

		// Token: 0x0600262D RID: 9773
		ITestStep CreateRwsCallStep(Uri uri);
	}
}
