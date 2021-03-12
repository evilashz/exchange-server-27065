using System;
using System.Security;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa;
using Microsoft.Exchange.Net.MonitoringWebClient.Rws;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079F RID: 1951
	internal class TestFactory : ITestFactory
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000537D4 File Offset: 0x000519D4
		public ITestStep CreateOwaLoginScenario(Uri uri, string userName, string userDomain, SecureString password, OwaLoginParameters owaLoginParameters, ITestFactory testFactory)
		{
			return new OwaLogin(uri, userName, userDomain, password, owaLoginParameters, testFactory);
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000537E4 File Offset: 0x000519E4
		public ITestStep CreateOwaExternalLoginAgainstSpecificServerScenario(Uri uri, string userName, string userDomain, SecureString password, string serverToHit, ITestFactory testFactory)
		{
			return new OwaExternalLoginAgainstSpecificServer(uri, userName, userDomain, password, testFactory, serverToHit);
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000537F4 File Offset: 0x000519F4
		public ITestStep CreateOwaHealthCheckScenario(Uri uri, ITestFactory testFactory)
		{
			return new OwaHealthCheckScenario(uri, testFactory);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000537FD File Offset: 0x000519FD
		public ITestStep CreateOwaStaticPageScenario(Uri uri, ITestFactory testFactory)
		{
			return new OwaStaticPage(uri, testFactory);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00053806 File Offset: 0x00051A06
		public ITestStep CreateOwaFindPlacesScenario(Uri uri, string userName, string userDomain, SecureString password, ITestFactory testFactory)
		{
			return new FindPlacesScenario(uri, userName, userDomain, password, testFactory);
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00053814 File Offset: 0x00051A14
		public ITestStep CreateOwaCertificateRevocationCheckScenario(Uri uri, ITestFactory testFactory)
		{
			return new OwaCertificateRevocationCheckScenario(uri, testFactory);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0005381D File Offset: 0x00051A1D
		public ITestStep CreateAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory testFactory)
		{
			return new Authenticate(uri, userName, userDomain, password, authenticationParameters, testFactory);
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0005382D File Offset: 0x00051A2D
		public ITestStep CreateLiveIDAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory testFactory)
		{
			return new LiveIdAuthentication(uri, userName, userDomain, password, authenticationParameters, testFactory);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0005383D File Offset: 0x00051A3D
		public ITestStep CreateIisAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password)
		{
			return new IisAuthentication(uri, userName, userDomain, password);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00053849 File Offset: 0x00051A49
		public ITestStep CreateFbaAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters)
		{
			return new FbaAuthentication(uri, userName, userDomain, password, authenticationParameters, this);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00053858 File Offset: 0x00051A58
		public ITestStep CreateBrickAuthenticateStep(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters)
		{
			return new BrickAuthentication(uri, userName, userDomain, authenticationParameters);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00053864 File Offset: 0x00051A64
		public ITestStep CreateAdfsAuthenticateStep(Uri uri, string userName, string userDomain, SecureString password, AdfsLogonPage adfsLogonPage)
		{
			return new AdfsAuthentication(uri, userName, userDomain, password, adfsLogonPage);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00053872 File Offset: 0x00051A72
		public ITestStep CreateMeasureClientLatencyStep()
		{
			return new MeasureClientLantency();
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00053879 File Offset: 0x00051A79
		public ITestStep CreateOwaStartPageStep(Uri uri)
		{
			return new OwaStartPage(uri);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00053881 File Offset: 0x00051A81
		public ITestStep CreateOwaDownloadStaticFileStep(Uri uri)
		{
			return new OwaDownloadStaticFile(uri);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00053889 File Offset: 0x00051A89
		public ITestStep CreateLogoffStep(Uri uri, string logoffPath)
		{
			return new Logoff(uri, logoffPath);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00053892 File Offset: 0x00051A92
		public ITestStep CreateOwaPingStep(Uri uri)
		{
			return new OwaPing(uri);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0005389A File Offset: 0x00051A9A
		public ITestStep CreateOwaSessionDataStep(Uri uri)
		{
			return new OwaSessionData(uri);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000538A2 File Offset: 0x00051AA2
		public ITestStep CreateOwaWebServiceStep(Uri uri, string action)
		{
			return new OwaWebService(uri, action);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000538AB File Offset: 0x00051AAB
		public ITestStep CreateOwaWebServiceStep(Uri uri, string action, RequestBody requestBody)
		{
			return new OwaWebService(uri, action, requestBody);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000538B5 File Offset: 0x00051AB5
		public ITestStep CreateOwaHealthCheckStep(Uri uri)
		{
			return new OwaHealthCheck(uri);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000538BD File Offset: 0x00051ABD
		public ITestStep CreateEstablishAffinityStep(Uri uri, string serverToHit)
		{
			return new EstablishAffinity(uri, serverToHit);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000538C6 File Offset: 0x00051AC6
		public ITestStep CreateEcpLoginScenario(Uri uri, string userName, string userDomain, SecureString password, ITestFactory testFactory)
		{
			return new EcpLogin(uri, userName, userDomain, password, testFactory);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000538D4 File Offset: 0x00051AD4
		public ITestStep CreateEcpExternalLoginAgainstSpecificServerScenario(Uri uri, string userName, string userDomain, SecureString password, string serverToHit, ITestFactory testFactory)
		{
			return new EcpExternalLoginAgainstSpecificServer(uri, userName, userDomain, password, testFactory, serverToHit);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000538E4 File Offset: 0x00051AE4
		public ITestStep CreateEcpActiveMonitoringLocalScenario(Uri uri, string userName, string userDomain, AuthenticationParameters authenticationParameters, ITestFactory testFactory, Func<EcpStartPage, ITestStep> getFeatureTestStep)
		{
			return new EcpActiveMonitoringLocal(uri, userName, userDomain, authenticationParameters, testFactory, getFeatureTestStep);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000538F4 File Offset: 0x00051AF4
		public ITestStep CreateEcpActiveMonitoringOutsideInScenario(Uri uri, string userName, SecureString password, ITestFactory testFactory, Func<EcpStartPage, ITestStep> getFeatureTestStep)
		{
			return new EcpActiveMonitoringOutsideIn(uri, userName, password, testFactory, getFeatureTestStep);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00053902 File Offset: 0x00051B02
		public ITestStep CreateEcpStartPageStep(Uri uri)
		{
			return new EcpStartPage(uri);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0005390A File Offset: 0x00051B0A
		public ITestStep CreateEcpWebServiceCallStep(Uri uri)
		{
			return new EcpWebServiceCall(uri);
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00053912 File Offset: 0x00051B12
		public ITestStep CreateRwsCallScenario(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory testFactory)
		{
			return new RwsCallScenario(uri, authenticationInfo, testFactory);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0005391C File Offset: 0x00051B1C
		public ITestStep CreateRwsAuthenticateStep(Uri uri, RwsAuthenticationInfo authenticationInfo, ITestFactory testFactory)
		{
			return new RwsAuthentication(uri, authenticationInfo, testFactory);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00053926 File Offset: 0x00051B26
		public ITestStep CreateRwsBrickAuthenticateStep(CommonAccessToken token, Uri uri)
		{
			return new RwsBrickAuthentication(token, uri);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0005392F File Offset: 0x00051B2F
		public ITestStep CreateRwsCallStep(Uri uri)
		{
			return new RwsCall(uri);
		}
	}
}
