using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x0200033C RID: 828
	internal abstract class ManageDelegationClient
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001C4F RID: 7247
		protected abstract CustomSoapHttpClientProtocol Client { get; }

		// Token: 0x06001C50 RID: 7248
		public abstract void AddUri(string applicationId, string uri);

		// Token: 0x06001C51 RID: 7249
		public abstract void RemoveUri(string applicationId, string uri);

		// Token: 0x06001C52 RID: 7250
		public abstract void ReserveDomain(string applicationId, string domain, string programId);

		// Token: 0x06001C53 RID: 7251
		public abstract void ReleaseDomain(string applicationId, string domain);

		// Token: 0x06001C54 RID: 7252 RVA: 0x0007D954 File Offset: 0x0007BB54
		protected ManageDelegationClient(string serviceEndpoint, string certificateThumbprint, WriteVerboseDelegate writeVerbose)
		{
			this.serviceEndpoint = serviceEndpoint;
			this.certificateThumbprint = certificateThumbprint;
			this.writeVerbose = writeVerbose;
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0007D971 File Offset: 0x0007BB71
		protected string ServiceEndpoint
		{
			get
			{
				return this.serviceEndpoint;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0007D979 File Offset: 0x0007BB79
		protected X509Certificate2 Certificate
		{
			get
			{
				if (this.certificateThumbprint == null)
				{
					throw new ArgumentNullException("certificate");
				}
				if (this.certificate == null)
				{
					this.certificate = FederationCertificate.LoadCertificateWithPrivateKey(this.certificateThumbprint, this.WriteVerbose);
				}
				return this.certificate;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0007D9B3 File Offset: 0x0007BBB3
		protected WriteVerboseDelegate WriteVerbose
		{
			get
			{
				return this.writeVerbose;
			}
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0007D9BC File Offset: 0x0007BBBC
		protected void ExecuteAndHandleError(string description, ManageDelegationClient.WebMethodDelegate webMethod)
		{
			LocalizedException ex = null;
			try
			{
				this.ExecuteAndRetry(description, webMethod);
			}
			catch (SoapException exception)
			{
				ex = this.GetLiveDomainServicesAccessExceptionToThrow(exception);
			}
			catch (WebException exception2)
			{
				ex = ManageDelegationClient.GetCommunicationException(exception2);
			}
			catch (IOException exception3)
			{
				ex = ManageDelegationClient.GetCommunicationException(exception3);
			}
			catch (SocketException exception4)
			{
				ex = ManageDelegationClient.GetCommunicationException(exception4);
			}
			catch (InvalidOperationException exception5)
			{
				ex = ManageDelegationClient.GetCommunicationException(exception5);
			}
			if (ex != null)
			{
				this.WriteVerbose(Strings.LiveDomainServicesRequestFailed(ManageDelegationClient.GetExceptionDetails(ex)));
				throw ex;
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0007DA68 File Offset: 0x0007BC68
		protected static bool InvalidCertificateHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None || SslConfiguration.AllowExternalUntrustedCerts;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0007DA7C File Offset: 0x0007BC7C
		private void ExecuteAndRetry(string description, ManageDelegationClient.WebMethodDelegate webMethod)
		{
			DateTime t = DateTime.UtcNow + ManageDelegationClient.ErrorRetryLimit;
			string validDirectUrl = this.ServiceEndpoint;
			this.Client.AllowAutoRedirect = false;
			WebProxy webProxy = LiveConfiguration.GetWebProxy(this.WriteVerbose);
			if (webProxy != null)
			{
				this.Client.Proxy = webProxy;
			}
			int num = 0;
			for (;;)
			{
				this.Client.Url = validDirectUrl;
				try
				{
					this.WriteVerbose(Strings.CallingDomainServicesEndPoint(description, validDirectUrl));
					webMethod();
				}
				catch (WebException ex)
				{
					if (DateTime.UtcNow > t)
					{
						throw;
					}
					HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
					if (httpWebResponse == null)
					{
						throw;
					}
					HttpStatusCode statusCode = httpWebResponse.StatusCode;
					if (statusCode != HttpStatusCode.Found)
					{
						if (statusCode != HttpStatusCode.Forbidden)
						{
							throw;
						}
						Thread.Sleep(ManageDelegationClient.ErrorRetryInterval);
					}
					else
					{
						num++;
						if (num > 3)
						{
							throw;
						}
						validDirectUrl = this.GetValidDirectUrl(httpWebResponse);
						if (validDirectUrl == null)
						{
							throw;
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0007DB6C File Offset: 0x0007BD6C
		private string GetValidDirectUrl(HttpWebResponse webResponse)
		{
			string text = webResponse.Headers[HttpResponseHeader.Location];
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				return null;
			}
			Uri uri = new Uri(text, UriKind.Absolute);
			if (uri.Scheme != Uri.UriSchemeHttps)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0007DBAF File Offset: 0x0007BDAF
		private static LocalizedException GetCommunicationException(Exception exception)
		{
			return new LiveDomainServicesException(Strings.ErrorLiveDomainServicesAccess(exception.Message), exception);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0007DBC4 File Offset: 0x0007BDC4
		private static string GetExceptionDetails(Exception exception)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(10000);
			while (exception != null)
			{
				stringBuilder.AppendFormat("[{0}]: {1}", num.ToString(), exception.GetType().FullName);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(exception.Message);
				ManageDelegationClient.DetailException(stringBuilder, exception as SoapException);
				ManageDelegationClient.DetailException(stringBuilder, exception as WebException);
				ManageDelegationClient.DetailException(stringBuilder, exception as SocketException);
				if (exception.StackTrace != null)
				{
					stringBuilder.AppendLine(exception.StackTrace);
				}
				exception = exception.InnerException;
				num++;
				if (exception != null)
				{
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007DC70 File Offset: 0x0007BE70
		private static void DetailException(StringBuilder detail, SoapException soapException)
		{
			if (soapException == null)
			{
				return;
			}
			if (soapException.Code != null)
			{
				detail.AppendFormat("Code: {0}", soapException.Code);
				detail.AppendLine();
			}
			if (soapException.SubCode != null)
			{
				int num = 0;
				detail.Append("SubCode: ");
				SoapFaultSubCode subCode = soapException.SubCode;
				while (subCode != null)
				{
					if (subCode.Code != null)
					{
						detail.Append(subCode.Code.ToString());
					}
					subCode = subCode.SubCode;
					num++;
					if (num > 10)
					{
						break;
					}
					if (subCode != null)
					{
						detail.Append(", ");
					}
				}
				detail.AppendLine();
			}
			if (soapException.Detail != null)
			{
				detail.AppendFormat("Detail: {0}", soapException.Detail.OuterXml);
				detail.AppendLine();
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0007DD38 File Offset: 0x0007BF38
		private static void DetailException(StringBuilder detail, WebException webException)
		{
			if (webException == null)
			{
				return;
			}
			detail.AppendFormat("Status: {0} ({1})", webException.Status, (int)webException.Status);
			detail.AppendLine();
			if (webException.Response != null)
			{
				if (webException.Response.Headers != null && webException.Response.Headers.Count > 0)
				{
					detail.AppendLine("Response headers:");
					foreach (string text in webException.Response.Headers.AllKeys)
					{
						detail.AppendFormat("  {0}: {1}", text, webException.Response.Headers[text]);
						detail.AppendLine();
					}
				}
				Stream responseStream = webException.Response.GetResponseStream();
				if (responseStream != null)
				{
					using (responseStream)
					{
						if (responseStream.CanRead)
						{
							if (responseStream.CanSeek)
							{
								try
								{
									responseStream.Seek(0L, SeekOrigin.Begin);
								}
								catch (IOException)
								{
								}
								catch (NotSupportedException)
								{
								}
								catch (ObjectDisposedException)
								{
								}
							}
							using (StreamReader streamReader = new StreamReader(responseStream))
							{
								try
								{
									string value = streamReader.ReadToEnd();
									detail.AppendLine("Response body:");
									detail.AppendLine(value);
								}
								catch (IOException)
								{
								}
							}
						}
					}
				}
			}
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				detail.AppendFormat("StatusCode: {0}", httpWebResponse.StatusCode);
				detail.AppendLine();
				if (httpWebResponse.StatusDescription != null)
				{
					detail.AppendFormat("StatusDescription: {0}", httpWebResponse.StatusDescription);
					detail.AppendLine();
				}
				if (httpWebResponse.Server != null)
				{
					detail.AppendFormat("Server: {0}", httpWebResponse.Server);
					detail.AppendLine();
				}
			}
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007DF34 File Offset: 0x0007C134
		private static void DetailException(StringBuilder detail, SocketException socketException)
		{
			if (socketException == null)
			{
				return;
			}
			detail.AppendFormat("SocketErrorCode: {0}", socketException.SocketErrorCode);
			detail.AppendLine();
			detail.AppendFormat("ErrorCode: {0}", socketException.ErrorCode);
			detail.AppendLine();
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007DF84 File Offset: 0x0007C184
		private LocalizedException GetLiveDomainServicesAccessExceptionToThrow(SoapException exception)
		{
			int num;
			if (exception.Detail != null && exception.Detail.HasChildNodes && int.TryParse(exception.Detail.FirstChild.InnerText.Trim(), out num))
			{
				DomainError domainError = (DomainError)num;
				return new LiveDomainServicesException(domainError, this.GetMessageFromDomainError(domainError, exception), exception);
			}
			return new LiveDomainServicesException(Strings.ErrorLiveDomainServicesAccess(exception.Message), exception);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007DFE8 File Offset: 0x0007C1E8
		private LocalizedString GetMessageFromDomainError(DomainError domainError, SoapException exception)
		{
			if (domainError <= DomainError.ProofOfOwnershipNotValid)
			{
				switch (domainError)
				{
				case DomainError.InvalidPartner:
					return Strings.ErrorLiveDomainInaccessibleEpr(exception.Message, this.ServiceEndpoint);
				case DomainError.InvalidPartnerCert:
				case DomainError.InvalidManagementCertificate:
					return Strings.ErrorCertificateNotValid(this.Certificate.Subject, this.Certificate.Thumbprint, exception.Message);
				case DomainError.PartnerNotAuthorized:
				case DomainError.MemberNotAuthorized:
				case DomainError.MemberNotAuthenticated:
					return Strings.ErrorLiveIdAuthentication(exception.Message);
				default:
					switch (domainError)
					{
					case DomainError.InvalidDomainName:
					case DomainError.BlockedDomainName:
					case DomainError.InvalidDomainConfigId:
						return Strings.ErrorLiveIdDomainNameInvalid(exception.Message);
					case DomainError.DomainNotReserved:
						return Strings.ErrorsDomainNotReserved;
					case DomainError.DomainUnavailable:
						return Strings.ErrorLiveDomainReservationError(exception.Message);
					case DomainError.DomainPendingChanges:
					case DomainError.DomainSuspended:
					case DomainError.DomainPendingConfiguration:
						return Strings.ErrorLiveIdDomainTemporarilyUnavailable(exception.Message);
					case DomainError.NotPermittedForDomain:
						return Strings.ErrorLiveDomainUriNotUnique(exception.Message);
					case DomainError.ProofOfOwnershipNotValid:
						return Strings.ErrorProofOfOwnershipNotValid;
					}
					break;
				}
			}
			else
			{
				switch (domainError)
				{
				case DomainError.MemberNameInvalid:
				case DomainError.MemberNameBlocked:
				case DomainError.MemberNameUnavailable:
				case DomainError.MemberNameBlank:
				case DomainError.MemberNameIncludesInvalidChars:
				case DomainError.MemberNameIncludesDots:
				case DomainError.MemberNameInUse:
				case DomainError.ManagedMemberExists:
				case DomainError.ManagedMemberNotExists:
				case DomainError.UnmanagedMemberExists:
				case DomainError.UnmanagedMemberNotExists:
				case DomainError.MaxMembershipLimit:
				case DomainError.PasswordBlank:
				case DomainError.PasswordTooShort:
				case DomainError.PasswordTooLong:
				case DomainError.PasswordIncludesMemberName:
				case DomainError.PasswordIncludesInvalidChars:
				case DomainError.PasswordInvalid:
				case DomainError.InvalidNetId:
				case DomainError.InvalidOffer:
					break;
				default:
					switch (domainError)
					{
					case DomainError.InternalError:
					case DomainError.InvalidParameter:
					case DomainError.ExchangeError:
					case DomainError.SubscriptionServicesError:
					case DomainError.TestForcedError:
						break;
					case DomainError.PassportError:
						return this.GetPassportErrorMessage(exception);
					case DomainError.ServiceDown:
						return Strings.ErrorLiveIdServiceDown(exception.Message);
					default:
						if (domainError == DomainError.NYI)
						{
							return Strings.ErrorLiveDomainServicesUnexpectedResult(Strings.ErrorDomainServicesNotYetImplemented);
						}
						break;
					}
					break;
				}
			}
			return Strings.ErrorLiveDomainServicesUnexpectedResult(domainError.ToString() + " " + exception.Message);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0007E1B0 File Offset: 0x0007C3B0
		private LocalizedString GetPassportErrorMessage(SoapException se)
		{
			int num = 0;
			try
			{
				string text = se.Detail.LastChild.InnerText.Trim().ToUpperInvariant();
				if (text.StartsWith("0X"))
				{
					text = text.Substring(2);
				}
				int.TryParse(text, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out num);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			int num2 = num;
			if (num2 == -2147198366)
			{
				return Strings.ErrorLiveDomainUriNotUnique(se.Message);
			}
			return Strings.ErrorLiveIdError(se.Message);
		}

		// Token: 0x0400183B RID: 6203
		private const int MaximumRedirects = 3;

		// Token: 0x0400183C RID: 6204
		private static readonly TimeSpan ErrorRetryLimit = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400183D RID: 6205
		private static readonly TimeSpan ErrorRetryInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400183E RID: 6206
		private readonly string serviceEndpoint;

		// Token: 0x0400183F RID: 6207
		private readonly string certificateThumbprint;

		// Token: 0x04001840 RID: 6208
		private X509Certificate2 certificate;

		// Token: 0x04001841 RID: 6209
		private WriteVerboseDelegate writeVerbose;

		// Token: 0x0200033D RID: 829
		// (Invoke) Token: 0x06001C66 RID: 7270
		protected delegate void WebMethodDelegate();
	}
}
