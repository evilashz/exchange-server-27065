using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.com.IPC.WSService;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000970 RID: 2416
	[Serializable]
	internal class RightsManagementException : LocalizedException
	{
		// Token: 0x06003446 RID: 13382 RVA: 0x00080389 File Offset: 0x0007E589
		public RightsManagementException(RightsManagementFailureCode failureCode, LocalizedString message) : base(message)
		{
			this.failureCode = failureCode;
			this.isPermanent = this.IsPermanentFailure();
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000803B0 File Offset: 0x0007E5B0
		public RightsManagementException(RightsManagementFailureCode failureCode, LocalizedString message, string url) : base(message)
		{
			this.failureCode = failureCode;
			this.rmsUrl = url;
			this.isPermanent = this.IsPermanentFailure();
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000803DE File Offset: 0x0007E5DE
		public RightsManagementException(RightsManagementFailureCode failureCode, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.failureCode = failureCode;
			this.isPermanent = this.IsPermanentFailure();
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x00080406 File Offset: 0x0007E606
		public RightsManagementException(RightsManagementFailureCode failureCode, LocalizedString message, Exception innerException, string url) : base(message, innerException)
		{
			this.failureCode = failureCode;
			this.rmsUrl = url;
			this.isPermanent = this.IsPermanentFailure();
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00080438 File Offset: 0x0007E638
		protected RightsManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureCode = (RightsManagementFailureCode)info.GetInt32("FailureCode");
			this.rmsUrl = info.GetString("RmsUrl");
			this.isPermanent = this.IsPermanentFailure();
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x00080486 File Offset: 0x0007E686
		public static RightsManagementFailureCode GetFailureCodeFromXml(XmlNode node)
		{
			if (node == null)
			{
				return RightsManagementFailureCode.UnknownFailure;
			}
			if (string.Equals(node.Name, "XrML", StringComparison.OrdinalIgnoreCase))
			{
				return RightsManagementFailureCode.Success;
			}
			ExTraceGlobals.RightsManagementTracer.TraceError<string>(0L, "Failed to find XrML element. Response {0}", node.InnerText);
			return RightsManagementException.GetFailureCode(node.InnerText);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000804C4 File Offset: 0x0007E6C4
		private static RightsManagementFailureCode GetFailureCode(string message)
		{
			RightsManagementFailureCode result = RightsManagementFailureCode.UnknownFailure;
			if (!string.IsNullOrEmpty(message))
			{
				if (RightsManagementException.ContainsIgnoreCase(message, "AcquirePreLicenseInvalidLicenseeException"))
				{
					result = RightsManagementFailureCode.InvalidLicensee;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "BlackBoxIsInvalidException"))
				{
					result = RightsManagementFailureCode.InvalidBlackBox;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "InvalidRightsLabelSignatureException") || RightsManagementException.ContainsIgnoreCase(message, "InvalidSignedIssuanceLicenseException") || RightsManagementException.ContainsIgnoreCase(message, "IssuanceLicenseIsNotWithinValidTimeRangeException") || RightsManagementException.ContainsIgnoreCase(message, "RightsLabelNoMatchingIssuedPrincipalException"))
				{
					result = RightsManagementFailureCode.InvalidIssuanceLicense;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "MalformedDataVersionException") || RightsManagementException.ContainsIgnoreCase(message, "UnsupportedDataVersionException"))
				{
					result = RightsManagementFailureCode.InvalidVersion;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "UnauthorizedAccessException"))
				{
					result = RightsManagementFailureCode.UnauthorizedAccess;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "UnknownTemplateException"))
				{
					result = RightsManagementFailureCode.TemplateDoesNotExist;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "UntrustedRightsLabelException"))
				{
					result = RightsManagementFailureCode.UntrustedRightsLabel;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "AcquirePreLicenseCertificationFailedException"))
				{
					result = RightsManagementFailureCode.PreCertificationFailed;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "ADEntrySearchFailedException") || RightsManagementException.ContainsIgnoreCase(message, "DirectoryServicesPrincipalAliasesLookupException"))
				{
					result = RightsManagementFailureCode.AdEntryNotFound;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "ClusterDecommissionedException"))
				{
					result = RightsManagementFailureCode.ClusterDecommissioned;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "EnablingBitsUnsupportedException"))
				{
					result = RightsManagementFailureCode.EnablingBitsUnsupported;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "InvalidPersonaCertSignatureException") || RightsManagementException.ContainsIgnoreCase(message, "UnexpectedPersonaCertException") || RightsManagementException.ContainsIgnoreCase(message, "UntrustedPersonaCertException"))
				{
					result = RightsManagementFailureCode.InvalidPersonaCertificate;
				}
				else if (RightsManagementException.ContainsIgnoreCase(message, "DrmacIsExcludedException") || RightsManagementException.ContainsIgnoreCase(message, "NoRightsForRequestedPrincipalException"))
				{
					result = RightsManagementFailureCode.UserRightNotGranted;
				}
			}
			return result;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x0008066E File Offset: 0x0007E86E
		private static bool ContainsIgnoreCase(string str1, string str2)
		{
			return str1.IndexOf(str2, StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x00080680 File Offset: 0x0007E880
		private bool IsPermanentFailure()
		{
			if (this.failureCode == RightsManagementFailureCode.Success)
			{
				return false;
			}
			RightsManagementException.FailureType failureType = RightsManagementException.FailureType.Unknown;
			Exception innerException = base.InnerException;
			RightsManagementFailureCode rightsManagementFailureCode = RightsManagementFailureCode.UnknownFailure;
			if (innerException is RightsManagementServerException)
			{
				RightsManagementServerException ex = innerException as RightsManagementServerException;
				failureType = (ex.IsPermanentFailure ? RightsManagementException.FailureType.Permanent : RightsManagementException.FailureType.Transient);
				rightsManagementFailureCode = RightsManagementFailureCode.OfflineRmsServerFailure;
			}
			else if (innerException is SoapException)
			{
				if (string.IsNullOrEmpty(innerException.Message))
				{
					failureType = RightsManagementException.FailureType.Transient;
				}
				else
				{
					rightsManagementFailureCode = RightsManagementException.GetFailureCode(innerException.Message);
				}
			}
			else if (innerException is WebException)
			{
				WebException ex2 = innerException as WebException;
				switch (ex2.Status)
				{
				case WebExceptionStatus.NameResolutionFailure:
				case WebExceptionStatus.ConnectFailure:
				case WebExceptionStatus.SecureChannelFailure:
				case WebExceptionStatus.ProxyNameResolutionFailure:
					rightsManagementFailureCode = RightsManagementFailureCode.ConnectFailure;
					failureType = RightsManagementException.FailureType.Permanent;
					goto IL_2F1;
				case WebExceptionStatus.ProtocolError:
				{
					failureType = RightsManagementException.FailureType.Transient;
					HttpWebResponse httpWebResponse = ex2.Response as HttpWebResponse;
					if (httpWebResponse != null)
					{
						HttpStatusCode statusCode = httpWebResponse.StatusCode;
						if (statusCode <= HttpStatusCode.PartialContent)
						{
							switch (statusCode)
							{
							case HttpStatusCode.Continue:
							case HttpStatusCode.SwitchingProtocols:
								break;
							default:
								switch (statusCode)
								{
								}
								break;
							}
						}
						else
						{
							switch (statusCode)
							{
							case HttpStatusCode.MultipleChoices:
							case HttpStatusCode.MovedPermanently:
							case HttpStatusCode.Found:
							case HttpStatusCode.SeeOther:
							case HttpStatusCode.NotModified:
							case HttpStatusCode.UseProxy:
							case HttpStatusCode.Unused:
							case HttpStatusCode.TemporaryRedirect:
								rightsManagementFailureCode = RightsManagementFailureCode.Http3xxFailure;
								failureType = RightsManagementException.FailureType.Permanent;
								goto IL_2F1;
							default:
								switch (statusCode)
								{
								case HttpStatusCode.BadRequest:
								case HttpStatusCode.PaymentRequired:
								case HttpStatusCode.MethodNotAllowed:
								case HttpStatusCode.NotAcceptable:
								case HttpStatusCode.ProxyAuthenticationRequired:
								case HttpStatusCode.RequestTimeout:
								case HttpStatusCode.Conflict:
								case HttpStatusCode.Gone:
								case HttpStatusCode.LengthRequired:
								case HttpStatusCode.PreconditionFailed:
								case HttpStatusCode.RequestEntityTooLarge:
								case HttpStatusCode.RequestUriTooLong:
								case HttpStatusCode.UnsupportedMediaType:
								case HttpStatusCode.RequestedRangeNotSatisfiable:
								case HttpStatusCode.ExpectationFailed:
									break;
								case HttpStatusCode.Unauthorized:
									rightsManagementFailureCode = RightsManagementFailureCode.HttpUnauthorizedFailure;
									failureType = RightsManagementException.FailureType.Permanent;
									goto IL_2F1;
								case HttpStatusCode.Forbidden:
									rightsManagementFailureCode = RightsManagementFailureCode.HttpForbiddenFailure;
									failureType = RightsManagementException.FailureType.Permanent;
									goto IL_2F1;
								case HttpStatusCode.NotFound:
									rightsManagementFailureCode = RightsManagementFailureCode.HttpNotFoundFailure;
									failureType = RightsManagementException.FailureType.Permanent;
									goto IL_2F1;
								default:
									switch (statusCode)
									{
									case HttpStatusCode.BadGateway:
									case HttpStatusCode.ServiceUnavailable:
									case HttpStatusCode.GatewayTimeout:
										rightsManagementFailureCode = RightsManagementFailureCode.ConnectFailure;
										failureType = RightsManagementException.FailureType.Permanent;
										goto IL_2F1;
									}
									break;
								}
								break;
							}
						}
						failureType = RightsManagementException.FailureType.Transient;
						goto IL_2F1;
					}
					goto IL_2F1;
				}
				case WebExceptionStatus.TrustFailure:
					rightsManagementFailureCode = RightsManagementFailureCode.TrustFailure;
					failureType = RightsManagementException.FailureType.Permanent;
					goto IL_2F1;
				}
				failureType = RightsManagementException.FailureType.Transient;
			}
			else if (innerException is CommunicationException)
			{
				failureType = RightsManagementException.FailureType.Transient;
				if (innerException is EndpointNotFoundException || innerException is MessageSecurityException || innerException is ActionNotSupportedException)
				{
					failureType = RightsManagementException.FailureType.Permanent;
				}
				else if (innerException is FaultException<ActiveFederationFault>)
				{
					FaultException<ActiveFederationFault> faultException = innerException as FaultException<ActiveFederationFault>;
					if (faultException == null || faultException.Detail == null || faultException.Detail.IsPermanentFailure)
					{
						failureType = RightsManagementException.FailureType.Permanent;
					}
				}
			}
			else if (innerException is InvalidOperationException || innerException is IOException || innerException is UnauthorizedAccessException || innerException is WSTrustException || innerException is CryptographicException || innerException is TimeoutException)
			{
				failureType = RightsManagementException.FailureType.Transient;
			}
			else
			{
				rightsManagementFailureCode = RightsManagementException.GetFailureCode(this.Message);
			}
			IL_2F1:
			RightsManagementFailureCode rightsManagementFailureCode2 = rightsManagementFailureCode;
			if (rightsManagementFailureCode2 != RightsManagementFailureCode.UnknownFailure)
			{
				this.failureCode = rightsManagementFailureCode;
			}
			if (failureType == RightsManagementException.FailureType.Unknown)
			{
				RightsManagementFailureCode rightsManagementFailureCode3 = this.failureCode;
				if (rightsManagementFailureCode3 <= RightsManagementFailureCode.DebuggerDetected)
				{
					if (rightsManagementFailureCode3 <= RightsManagementFailureCode.ServerError)
					{
						if (rightsManagementFailureCode3 != RightsManagementFailureCode.NoConnect)
						{
							switch (rightsManagementFailureCode3)
							{
							case RightsManagementFailureCode.NeedsGroupIdentityActivation:
							case RightsManagementFailureCode.OutOfQuota:
							case RightsManagementFailureCode.AuthenticationFailed:
							case RightsManagementFailureCode.ServerError:
								break;
							case (RightsManagementFailureCode)(-2147168449):
							case RightsManagementFailureCode.ActivationFailed:
							case RightsManagementFailureCode.Aborted:
								goto IL_3E0;
							default:
								goto IL_3E0;
							}
						}
					}
					else
					{
						switch (rightsManagementFailureCode3)
						{
						case RightsManagementFailureCode.ServiceNotFound:
						case RightsManagementFailureCode.ServerNotFound:
							break;
						case RightsManagementFailureCode.UseDefault:
							goto IL_3E0;
						default:
							if (rightsManagementFailureCode3 != RightsManagementFailureCode.InvalidClientLicensorCertificate && rightsManagementFailureCode3 != RightsManagementFailureCode.DebuggerDetected)
							{
								goto IL_3E0;
							}
							break;
						}
					}
				}
				else if (rightsManagementFailureCode3 <= RightsManagementFailureCode.FederationCertificateAccessFailure)
				{
					if (rightsManagementFailureCode3 != RightsManagementFailureCode.AttachmentProtectionFailed)
					{
						switch (rightsManagementFailureCode3)
						{
						case RightsManagementFailureCode.ExchangeMisConfiguration:
						case RightsManagementFailureCode.FederatedMailboxNotSet:
						case RightsManagementFailureCode.FailedToRequestDelegationToken:
						case RightsManagementFailureCode.FederationCertificateAccessFailure:
							break;
						case RightsManagementFailureCode.InvalidRecipient:
						case RightsManagementFailureCode.FederationNotEnabled:
						case RightsManagementFailureCode.FailedToDetermineExchangeMode:
							goto IL_3E0;
						default:
							goto IL_3E0;
						}
					}
				}
				else if (rightsManagementFailureCode3 != RightsManagementFailureCode.OperationTimeout && rightsManagementFailureCode3 != RightsManagementFailureCode.ServerRightNotGranted && rightsManagementFailureCode3 != RightsManagementFailureCode.Success)
				{
					goto IL_3E0;
				}
				failureType = RightsManagementException.FailureType.Transient;
				goto IL_3E2;
				IL_3E0:
				failureType = RightsManagementException.FailureType.Permanent;
			}
			IL_3E2:
			return failureType == RightsManagementException.FailureType.Permanent;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x00080A74 File Offset: 0x0007EC74
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("FailureCode", (int)this.failureCode);
			info.AddValue("RmsUrl", this.rmsUrl, typeof(string));
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x00080AC3 File Offset: 0x0007ECC3
		// (set) Token: 0x06003451 RID: 13393 RVA: 0x00080ACB File Offset: 0x0007ECCB
		public RightsManagementFailureCode FailureCode
		{
			get
			{
				return this.failureCode;
			}
			set
			{
				this.failureCode = value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x00080AD4 File Offset: 0x0007ECD4
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x00080ADC File Offset: 0x0007ECDC
		public bool IsPermanent
		{
			get
			{
				return this.isPermanent;
			}
			set
			{
				this.isPermanent = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x00080AE5 File Offset: 0x0007ECE5
		public string RmsUrl
		{
			get
			{
				return this.rmsUrl;
			}
		}

		// Token: 0x04002C56 RID: 11350
		private const string SerializationRmsUrlAttributeName = "RmsUrl";

		// Token: 0x04002C57 RID: 11351
		private const string SerializationFailureCodeAttributeName = "FailureCode";

		// Token: 0x04002C58 RID: 11352
		private const string XrmlNode = "XrML";

		// Token: 0x04002C59 RID: 11353
		private RightsManagementFailureCode failureCode;

		// Token: 0x04002C5A RID: 11354
		private string rmsUrl = string.Empty;

		// Token: 0x04002C5B RID: 11355
		private bool isPermanent;

		// Token: 0x02000971 RID: 2417
		private enum FailureType : uint
		{
			// Token: 0x04002C5D RID: 11357
			NoFailure,
			// Token: 0x04002C5E RID: 11358
			Unknown,
			// Token: 0x04002C5F RID: 11359
			Transient,
			// Token: 0x04002C60 RID: 11360
			Permanent
		}
	}
}
