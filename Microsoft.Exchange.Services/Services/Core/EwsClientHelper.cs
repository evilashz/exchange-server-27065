using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EwsClientHelper
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001335D File Offset: 0x0001155D
		internal static int RemoteEwsClientTimeout
		{
			get
			{
				if (EwsClientHelper.remoteEwsClientTimeout < 0)
				{
					EwsClientHelper.remoteEwsClientTimeout = Global.GetAppSettingAsInt("RemoteEwsClientTimeout", 540000);
				}
				return EwsClientHelper.remoteEwsClientTimeout;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013380 File Offset: 0x00011580
		public static string DiscoverEwsUrl(IMailboxInfo mailbox)
		{
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(mailbox);
			if (!(backEndWebServicesUrl != null))
			{
				return null;
			}
			return backEndWebServicesUrl.ToString();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000133A8 File Offset: 0x000115A8
		public static string DiscoverEwsUrl(BaseServerIdInfo serverInfo)
		{
			BackEndServer backEndServer = new BackEndServer(serverInfo.ServerFQDN, serverInfo.ServerVersion);
			Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
			if (!(backEndWebServicesUrl != null))
			{
				return null;
			}
			return backEndWebServicesUrl.ToString();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000133E0 File Offset: 0x000115E0
		public static ExchangeServiceBinding CreateBinding(AuthZClientInfo callerInfo, string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentNullException("url");
			}
			return new ExchangeServiceBinding("EwsFanoutClient", new RemoteCertificateValidationCallback(EwsClientHelper.CertificateErrorHandler))
			{
				Authenticator = EwsClientHelper.CreateNetworkServiceAuthenticator(callerInfo),
				RequestServerVersionValue = new RequestServerVersion
				{
					Version = Microsoft.Exchange.SoapWebClient.EWS.ExchangeVersionType.Exchange2013
				},
				Url = url,
				UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent("EwsFanoutClient")
			};
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000134AC File Offset: 0x000116AC
		public static string DiscoverArchiveCrossPremiseEwsUrl(IMailboxInfo archiveMailbox, ADUser user)
		{
			string text = null;
			string domain = user.ArchiveDomain.Domain;
			Uri uri = null;
			EndPointDiscoveryInfo endPointDiscoveryInfo;
			bool flag = RemoteDiscoveryEndPoint.TryGetDiscoveryEndPoint(OrganizationId.ForestWideOrgId, domain, null, null, null, out uri, out endPointDiscoveryInfo);
			if (endPointDiscoveryInfo != null && endPointDiscoveryInfo.Status != EndPointDiscoveryInfo.DiscoveryStatus.Success)
			{
				RequestDetailsLogger.Current.AppendGenericInfo("DiscoveryStatus", endPointDiscoveryInfo.Status);
				ExTraceGlobals.ExceptionTracer.TraceDebug<SmtpAddress, EndPointDiscoveryInfo.DiscoveryStatus, string>(0L, "Getting autodiscover url for {0} encountered problem with status {1}. {2}", user.PrimarySmtpAddress, endPointDiscoveryInfo.Status, endPointDiscoveryInfo.Message);
			}
			if (!flag || uri == null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<SmtpAddress>(0L, "Failed to get autodiscover URL for {0}.", user.PrimarySmtpAddress);
				return null;
			}
			SmtpAddress archiveAddress = new SmtpAddress(SmtpProxyAddress.EncapsulateExchangeGuid(domain, archiveMailbox.MailboxGuid));
			Guid value = Guid.NewGuid();
			OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(OrganizationId.ForestWideOrgId, user, domain);
			oauthCredentialsForAppActAsToken.ClientRequestId = new Guid?(value);
			AutodiscoverService service = new AutodiscoverService(EwsWsSecurityUrl.FixForAnonymous(uri), 4)
			{
				Credentials = new OAuthCredentials(oauthCredentialsForAppActAsToken),
				PreAuthenticate = true,
				UserAgent = EwsClientHelper.GetOAuthUserAgent("EwsAutoDiscoverClient")
			};
			service.HttpHeaders["client-request-id"] = value.ToString();
			service.HttpHeaders["return-client-request-id"] = "true";
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(EwsClientHelper.CertificateErrorHandler));
				GetUserSettingsResponse response = null;
				Exception arg = null;
				bool flag2 = EwsClientHelper.ExecuteEwsCall(delegate
				{
					response = service.GetUserSettings(archiveAddress.ToString(), new UserSettingName[]
					{
						58
					});
				}, out arg);
				if (flag2)
				{
					if (response.ErrorCode == null)
					{
						if (!response.TryGetSettingValue<string>(58, ref text) || string.IsNullOrEmpty(text))
						{
							ExTraceGlobals.ExceptionTracer.TraceError<SmtpAddress, SmtpAddress>(0L, "Sucessfully called autodiscover, but did not retrieve a url for {0}/{1}.", user.PrimarySmtpAddress, archiveAddress);
						}
					}
					else
					{
						ExTraceGlobals.ExceptionTracer.TraceError(0L, "Unable to autodiscover EWS endpoint for {0}/{1}, error code: {2}, message {3}.", new object[]
						{
							user.PrimarySmtpAddress,
							archiveAddress,
							response.ErrorCode,
							response.ErrorMessage
						});
					}
				}
				else
				{
					ExTraceGlobals.ExceptionTracer.TraceError<SmtpAddress, SmtpAddress, Exception>(0L, "Unable to autodiscover EWS endpoint for {0}/{1}, exception {2}.", user.PrimarySmtpAddress, archiveAddress, arg);
				}
			}
			finally
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(EwsClientHelper.CertificateErrorHandler));
			}
			return text;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013778 File Offset: 0x00011978
		public static ExchangeServiceBinding CreateArchiveCrossPremiseBinding(ADUser user, string url, bool applicationOnly)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentNullException("url");
			}
			Guid value = Guid.NewGuid();
			OAuthCredentials oauthCredentials;
			if (applicationOnly)
			{
				oauthCredentials = OAuthCredentials.GetOAuthCredentialsForAppToken(user.OrganizationId, user.ArchiveDomain.Domain);
			}
			else
			{
				oauthCredentials = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, user.ArchiveDomain.Domain);
			}
			oauthCredentials.ClientRequestId = new Guid?(value);
			ExchangeServiceBinding exchangeServiceBinding = new ExchangeServiceBinding("EwsFanoutClient", new RemoteCertificateValidationCallback(EwsClientHelper.CertificateErrorHandler))
			{
				UseDefaultCredentials = false,
				PreAuthenticate = true,
				Credentials = oauthCredentials,
				RequestServerVersionValue = new RequestServerVersion
				{
					Version = Microsoft.Exchange.SoapWebClient.EWS.ExchangeVersionType.Exchange2013
				},
				Url = url,
				UserAgent = EwsClientHelper.GetOAuthUserAgent("EwsFanoutClient")
			};
			exchangeServiceBinding.HttpHeaders["client-request-id"] = value.ToString();
			exchangeServiceBinding.HttpHeaders["return-client-request-id"] = "true";
			return exchangeServiceBinding;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001386D File Offset: 0x00011A6D
		public static ExchangeServiceBinding GetArchiveServiceBinding(AuthZClientInfo effectiveCaller, IExchangePrincipal primaryPrincipal)
		{
			return EwsClientHelper.GetArchiveServiceBinding(effectiveCaller, primaryPrincipal, null, null);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013878 File Offset: 0x00011A78
		public static ExchangeServiceBinding GetArchiveServiceBinding(AuthZClientInfo effectiveCaller, IExchangePrincipal primaryPrincipal, ADUser user, string[] applicationRoles)
		{
			ExchangeServiceBinding exchangeServiceBinding = null;
			ADUser user2 = user ?? effectiveCaller.UserIdentity.ADUser;
			IMailboxInfo archiveMailbox = primaryPrincipal.GetArchiveMailbox();
			if (archiveMailbox != null)
			{
				switch (archiveMailbox.ArchiveState)
				{
				case ArchiveState.Local:
				{
					string text = EwsClientHelper.DiscoverEwsUrl(archiveMailbox);
					if (!string.IsNullOrEmpty(text))
					{
						exchangeServiceBinding = EwsClientHelper.CreateBinding(effectiveCaller, text);
					}
					break;
				}
				case ArchiveState.HostedProvisioned:
				{
					string key = primaryPrincipal.MailboxInfo.MailboxGuid.ToString();
					string text = ArchiveServiceDiscoveryCache.Singleton.Get(key);
					if (string.IsNullOrEmpty(text))
					{
						text = EwsClientHelper.DiscoverArchiveCrossPremiseEwsUrl(archiveMailbox, user2);
						if (!string.IsNullOrEmpty(text))
						{
							if (ArchiveServiceDiscoveryCache.Singleton.Contains(key))
							{
								ArchiveServiceDiscoveryCache.Singleton.Remove(key);
							}
							ArchiveServiceDiscoveryCache.Singleton.Add(key, text);
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						exchangeServiceBinding = EwsClientHelper.CreateArchiveCrossPremiseBinding(user2, text, applicationRoles != null && applicationRoles.Length > 0);
					}
					break;
				}
				}
			}
			if (exchangeServiceBinding != null && applicationRoles != null && applicationRoles.Length > 0)
			{
				exchangeServiceBinding.ManagementRole = new ManagementRoleType
				{
					ApplicationRoles = applicationRoles
				};
			}
			return exchangeServiceBinding;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013990 File Offset: 0x00011B90
		public static bool ExecuteEwsCall(Action action, out Exception exception)
		{
			exception = null;
			try
			{
				action();
			}
			catch (SoapException ex)
			{
				exception = ex;
			}
			catch (WebException ex2)
			{
				exception = ex2;
			}
			catch (ServiceRequestException ex3)
			{
				exception = ex3;
			}
			catch (IOException ex4)
			{
				exception = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				exception = ex5;
			}
			catch (LocalizedException ex6)
			{
				exception = ex6;
			}
			if (exception != null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<Exception>(0L, "Unable to execute EWS call. Exception {0}", exception);
			}
			return exception == null;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013A38 File Offset: 0x00011C38
		public static K Convert<T, K>(T source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(T));
			SafeXmlSerializer safeXmlSerializer2 = new SafeXmlSerializer(typeof(K));
			K result;
			try
			{
				K k;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					safeXmlSerializer.Serialize(memoryStream, source);
					memoryStream.Position = 0L;
					k = (K)((object)safeXmlSerializer2.Deserialize(memoryStream));
				}
				result = k;
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<InvalidOperationException>(0L, "Unable to convert EWS types. Exception {0}", ex);
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInternalServerError, ex);
			}
			return result;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013AF8 File Offset: 0x00011CF8
		internal static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				return true;
			}
			if (SslConfiguration.AllowInternalUntrustedCerts)
			{
				return true;
			}
			ExTraceGlobals.ExceptionTracer.TraceError<SslPolicyErrors>(0L, "Failed because SSL certificate contains the following errors: {0}", sslPolicyErrors);
			return false;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013B24 File Offset: 0x00011D24
		private static SoapHttpClientAuthenticator CreateNetworkServiceAuthenticator(AuthZClientInfo callerInfo)
		{
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<LocalizedException>(0L, "Unable to impersonate network service to call EWS due to exception {0}", NetworkServiceImpersonator.Exception);
			}
			SecurityAccessToken securityAccessToken = new SecurityAccessToken();
			callerInfo.ClientSecurityContext.SetSecurityAccessToken(securityAccessToken);
			SoapHttpClientAuthenticator soapHttpClientAuthenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			soapHttpClientAuthenticator.AdditionalSoapHeaders.Add(new SerializedSecurityContextType
			{
				UserSid = securityAccessToken.UserSid,
				GroupSids = EwsClientHelper.SidStringAndAttributesConverter(securityAccessToken.GroupSids),
				RestrictedGroupSids = EwsClientHelper.SidStringAndAttributesConverter(securityAccessToken.RestrictedGroupSids),
				PrimarySmtpAddress = callerInfo.PrimarySmtpAddress
			});
			return soapHttpClientAuthenticator;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013BBC File Offset: 0x00011DBC
		private static SidAndAttributesType[] SidStringAndAttributesConverter(SidStringAndAttributes[] sidStringAndAttributesArray)
		{
			if (sidStringAndAttributesArray == null)
			{
				return null;
			}
			SidAndAttributesType[] array = new SidAndAttributesType[sidStringAndAttributesArray.Length];
			for (int i = 0; i < sidStringAndAttributesArray.Length; i++)
			{
				array[i] = new SidAndAttributesType
				{
					SecurityIdentifier = sidStringAndAttributesArray[i].SecurityIdentifier,
					Attributes = sidStringAndAttributesArray[i].Attributes
				};
			}
			return array;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013C0C File Offset: 0x00011E0C
		private static string GetOAuthUserAgent(string component)
		{
			return string.Format("Exchange/{0}.{1}.{2}.{3}/{4}", new object[]
			{
				ServerVersion.InstalledVersion.Major,
				ServerVersion.InstalledVersion.Minor,
				ServerVersion.InstalledVersion.Build,
				ServerVersion.InstalledVersion.Revision,
				component
			});
		}

		// Token: 0x04000621 RID: 1569
		private const int DefaultRemoteEwsClientTimeout = 540000;

		// Token: 0x04000622 RID: 1570
		private const string AutoDiscoveryComponentId = "EwsAutoDiscoverClient";

		// Token: 0x04000623 RID: 1571
		private const string ServiceBindingComponentId = "EwsFanoutClient";

		// Token: 0x04000624 RID: 1572
		private const string ClientRequestId = "client-request-id";

		// Token: 0x04000625 RID: 1573
		private const string ReturnClientRequestId = "return-client-request-id";

		// Token: 0x04000626 RID: 1574
		private const string AppSettingRemoteEwsClientTimeout = "RemoteEwsClientTimeout";

		// Token: 0x04000627 RID: 1575
		private static int remoteEwsClientTimeout = -1;
	}
}
