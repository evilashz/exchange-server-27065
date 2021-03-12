using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000091 RID: 145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ElcEwsClientHelper
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0002B0FC File Offset: 0x000292FC
		public static Uri DiscoverEwsUrl(IMailboxInfo mailbox)
		{
			return BackEndLocator.GetBackEndWebServicesUrl(mailbox);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002B16C File Offset: 0x0002936C
		public static Uri DiscoverCloudArchiveEwsUrl(ADUser user)
		{
			Uri result = null;
			string text = null;
			string domain = user.ArchiveDomain.Domain;
			Uri uri = null;
			EndPointDiscoveryInfo endPointDiscoveryInfo;
			bool flag = RemoteDiscoveryEndPoint.TryGetDiscoveryEndPoint(OrganizationId.ForestWideOrgId, domain, null, null, null, out uri, out endPointDiscoveryInfo);
			if (endPointDiscoveryInfo != null && endPointDiscoveryInfo.Status != EndPointDiscoveryInfo.DiscoveryStatus.Success)
			{
				ElcEwsClientHelper.Tracer.TraceDebug<SmtpAddress, EndPointDiscoveryInfo.DiscoveryStatus, string>(0L, "Getting autodiscover url for {0} encountered problem with status {1}. {2}", user.PrimarySmtpAddress, endPointDiscoveryInfo.Status, endPointDiscoveryInfo.Message);
			}
			if (!flag || uri == null)
			{
				ElcEwsClientHelper.Tracer.TraceError<SmtpAddress>(0L, "Failed to get autodiscover URL for {0}.", user.PrimarySmtpAddress);
				return null;
			}
			SmtpAddress archiveAddress = new SmtpAddress(SmtpProxyAddress.EncapsulateExchangeGuid(domain, user.ArchiveGuid));
			Guid value = Guid.NewGuid();
			OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(OrganizationId.ForestWideOrgId, user, domain);
			oauthCredentialsForAppActAsToken.ClientRequestId = new Guid?(value);
			AutodiscoverService service = new AutodiscoverService(EwsWsSecurityUrl.FixForAnonymous(uri), 4)
			{
				Credentials = new OAuthCredentials(oauthCredentialsForAppActAsToken),
				PreAuthenticate = true,
				UserAgent = ElcEwsClientHelper.GetOAuthUserAgent("ElcAutoDiscoverClient")
			};
			service.ClientRequestId = value.ToString();
			service.ReturnClientRequestId = true;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ElcEwsClientHelper.CertificateErrorHandler));
				GetUserSettingsResponse response = null;
				Exception arg = null;
				bool flag2 = ElcEwsClientHelper.ExecuteEwsCall(delegate
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
							ElcEwsClientHelper.Tracer.TraceError<SmtpAddress, SmtpAddress>(0L, "Sucessfully called autodiscover, but did not retrieve a url for {0}/{1}.", user.PrimarySmtpAddress, archiveAddress);
						}
					}
					else
					{
						ElcEwsClientHelper.Tracer.TraceError(0L, "Unable to autodiscover EWS endpoint for {0}/{1}, error code: {2}, message {3}.", new object[]
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
					ElcEwsClientHelper.Tracer.TraceError<SmtpAddress, SmtpAddress, Exception>(0L, "Unable to autodiscover EWS endpoint for {0}/{1}, exception {2}.", user.PrimarySmtpAddress, archiveAddress, arg);
				}
			}
			finally
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(ElcEwsClientHelper.CertificateErrorHandler));
			}
			if (!string.IsNullOrEmpty(text))
			{
				result = new Uri(text);
			}
			return result;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002B418 File Offset: 0x00029618
		public static string GetOAuthUserAgent(string component)
		{
			return string.Format("Exchange/{0}.{1}.{2}.{3}/{4}/", new object[]
			{
				ServerVersion.InstalledVersion.Major,
				ServerVersion.InstalledVersion.Minor,
				ServerVersion.InstalledVersion.Build,
				ServerVersion.InstalledVersion.Revision,
				component
			});
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002B484 File Offset: 0x00029684
		public static string ConvertRetentionPropertyForService(object propertyValue, MapiPropertyTypeType propertyType)
		{
			if (propertyValue == null)
			{
				return null;
			}
			if (propertyType == MapiPropertyTypeType.Binary)
			{
				return Convert.ToBase64String((byte[])propertyValue);
			}
			return Convert.ToString(propertyValue);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002B4B0 File Offset: 0x000296B0
		public static object ConvertRetentionPropertyFromService(object propertyValue, MapiPropertyTypeType propertyType)
		{
			if (propertyValue == null)
			{
				return null;
			}
			if (propertyValue is string)
			{
				try
				{
					if (propertyType == MapiPropertyTypeType.Binary)
					{
						return Convert.FromBase64String((string)propertyValue);
					}
					if (propertyType != MapiPropertyTypeType.Integer)
					{
						return propertyValue;
					}
					return Convert.ToInt32((string)propertyValue);
				}
				catch (FormatException arg)
				{
					ElcEwsClientHelper.Tracer.TraceError<FormatException>(0L, "Unable to convert property, exception {0}.", arg);
				}
				catch (ArgumentException arg2)
				{
					ElcEwsClientHelper.Tracer.TraceError<ArgumentException>(0L, "Unable to convert property, exception {0}.", arg2);
				}
				return propertyValue;
			}
			return propertyValue;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002B548 File Offset: 0x00029748
		private static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
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
			ElcEwsClientHelper.Tracer.TraceError<SslPolicyErrors>(0L, "Failed because SSL certificate contains the following errors: {0}", sslPolicyErrors);
			return false;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002B574 File Offset: 0x00029774
		private static bool ExecuteEwsCall(Action action, out Exception exception)
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
				ElcEwsClientHelper.Tracer.TraceError<Exception>(0L, "Unable to execute EWS call. Exception {0}", exception);
			}
			return exception == null;
		}

		// Token: 0x0400041F RID: 1055
		public const string ComponentId = "ElcClient";

		// Token: 0x04000420 RID: 1056
		private const string AutoDiscoveryComponentId = "ElcAutoDiscoverClient";

		// Token: 0x04000421 RID: 1057
		private static readonly Trace Tracer = ExTraceGlobals.ELCTracer;
	}
}
