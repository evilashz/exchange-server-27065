using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000012 RID: 18
	internal static class Common
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000043D0 File Offset: 0x000025D0
		private static ServiceEndpointElement TryFindEndpointByNameAndContract(ServiceEndpointElementCollection endpoints, string name, string contract)
		{
			if (endpoints != null)
			{
				foreach (object obj in endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj;
					if (serviceEndpointElement.Name == name && serviceEndpointElement.Contract == contract)
					{
						return serviceEndpointElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004444 File Offset: 0x00002644
		public static void GenerateErrorResponseDontLog(XmlWriter xmlFragment, string ns, string errorCode, string message, string debugData, RequestData requestData, string assemblyQualifiedName)
		{
			if (string.IsNullOrEmpty(ns))
			{
				ns = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006";
			}
			Common.StartEnvelope(xmlFragment);
			xmlFragment.WriteStartElement("Response", ns);
			xmlFragment.WriteStartElement("Error", ns);
			xmlFragment.WriteAttributeString("Time", requestData.Timestamp);
			xmlFragment.WriteAttributeString("Id", requestData.ComputerNameHash);
			xmlFragment.WriteElementString("ErrorCode", ns, errorCode);
			xmlFragment.WriteElementString("Message", ns, message);
			xmlFragment.WriteElementString("DebugData", debugData);
			xmlFragment.WriteEndElement();
			xmlFragment.WriteEndElement();
			Common.EndEnvelope(xmlFragment);
			ExTraceGlobals.FrameworkTracer.TraceDebug((long)requestData.GetHashCode(), "[GenerateErrorResponse()] Time=\"{0}\",Id=\"{1}\",ErrorCode=\"{2}\",Message=\"{3}\",DebugData=\"{4}\"", new object[]
			{
				requestData.Timestamp,
				requestData.ComputerNameHash,
				errorCode,
				message,
				debugData
			});
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.Set(ServiceCommonMetadata.ErrorCode, errorCode);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("message", message);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("debugData", debugData);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004550 File Offset: 0x00002750
		public static void GenerateErrorResponse(XmlWriter xmlFragment, string ns, string errorCode, string message, string debugData, RequestData requestData, string assemblyQualifiedName)
		{
			Common.GenerateErrorResponseDontLog(xmlFragment, ns, errorCode, message, debugData, requestData, assemblyQualifiedName);
			Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnProvErrorResponse, Common.PeriodicKey, new object[]
			{
				requestData.Timestamp,
				requestData.ComputerNameHash,
				errorCode,
				message,
				debugData,
				requestData.EMailAddress,
				requestData.LegacyDN,
				assemblyQualifiedName
			});
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000045C4 File Offset: 0x000027C4
		public static bool DoesEmailAddressReferenceArchive(ADRecipient recipient, string emailAddress)
		{
			bool result = false;
			Guid g;
			if (AutodiscoverCommonUserSettings.TryGetExchangeGuidFromEmailAddress(emailAddress, out g) && (recipient.RecipientType == RecipientType.MailUser || recipient.RecipientType == RecipientType.UserMailbox))
			{
				ADUser aduser = (ADUser)recipient;
				if (aduser.ArchiveGuid.Equals(g))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000460A File Offset: 0x0000280A
		internal static void ThrowIfNullOrEmpty(string parameterValue, string parameterName)
		{
			if (parameterValue == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (parameterValue.Length == 0)
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000046B4 File Offset: 0x000028B4
		internal static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
			{
				bool flag = true;
				Exception ex = exception as Exception;
				if (ex != null)
				{
					ExTraceGlobals.FrameworkTracer.TraceError<Exception>(0L, "Encountered unhandled exception: {0}", ex);
					flag = Common.IsSendReportValid(ex);
				}
				ExTraceGlobals.FrameworkTracer.TraceError<bool>(0L, "SendWatsonReportOnUnhandledException isSendReportValid: {0}", flag);
				if (flag)
				{
					Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrWebException, Common.PeriodicKey, new object[]
					{
						ex.Message,
						ex.StackTrace
					});
				}
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("WatsonExceptionMessage", ex.ToString());
				return flag;
			}, ReportOptions.None);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000046DC File Offset: 0x000028DC
		internal static bool IsSendReportValid(Exception exception)
		{
			bool flag = true;
			if (exception.Data["FilterExceptionFromWatson"] is bool && (bool)exception.Data["FilterExceptionFromWatson"])
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<Exception>(0L, "[IsSendReportValid] Received {0} - skipping Watson reporting", exception);
				flag = false;
			}
			else if (exception is HttpException)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<Exception>(0L, "[IsSendReportValid()] Received HttpException {0} - skipping Watson reporting", exception);
				flag = false;
			}
			else if (exception is ThreadAbortException)
			{
				flag = false;
			}
			else if (exception is COMException && ((COMException)exception).ErrorCode == -2147024832)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<Exception>(0L, "[IsSendReportValid] Received COMException (0x80070040) {0} - skipping Watson reporting", exception);
				flag = false;
			}
			else if (exception is CommunicationException)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<Exception>(0L, "[IsSendReportValid()] Received CommunicationException {0} - skipping Watson reporting", exception);
				flag = false;
			}
			else if (exception is DataValidationException)
			{
				flag = false;
			}
			else if (exception is DataSourceOperationException)
			{
				flag = false;
			}
			else if (exception is StoragePermanentException || exception is StorageTransientException)
			{
				flag = false;
			}
			else if (exception is ServiceDiscoveryTransientException)
			{
				flag = false;
			}
			else if (exception is IOException)
			{
				flag = false;
			}
			else if (exception is OutOfMemoryException)
			{
				flag = false;
			}
			else if (exception is ADTransientException)
			{
				flag = false;
			}
			else if (exception is DataSourceTransientException)
			{
				flag = false;
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<bool>(0L, "IsSendReportValid isSendReportValid: {0}", flag);
			return flag;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000482C File Offset: 0x00002A2C
		internal static void ReportException(Exception exception, object responsibleObject, HttpContext httpContext)
		{
			if (!Common.IsSendReportValid(exception))
			{
				return;
			}
			if (ExWatson.IsWatsonReportAlreadySent(exception))
			{
				return;
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<Type>((long)responsibleObject.GetHashCode(), "[ReportException()] exception.GetType()=\"{0}\"", exception.GetType());
			bool flag = exception is AccessViolationException;
			if (Common.EventLog != null && exception != null && exception.Message.Length != 0)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<Exception>((long)responsibleObject.GetHashCode(), "[ReportException()] exception=\"{0}\"", exception);
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrWebException, Common.PeriodicKey, new object[]
				{
					exception.Message,
					exception.StackTrace
				});
				string text;
				string text2;
				string text3;
				if (httpContext != null && httpContext.Request != null && httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
				{
					text = httpContext.User.Identity.GetSecurityIdentifier().Value;
					text2 = (httpContext.Request.UserHostAddress ?? string.Empty);
					text3 = (httpContext.Request.UserHostName ?? string.Empty);
				}
				else
				{
					text = string.Empty;
					text2 = string.Empty;
					text3 = string.Empty;
				}
				ExTraceGlobals.FrameworkTracer.TraceDebug<string, string, string>((long)responsibleObject.GetHashCode(), "[ReportException()] userSid=\"{0}\";userHostAddress=\"{1}\";userHostName=\"{2}\"", text, text2, text3);
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_InfoWebSessionFailure, Common.PeriodicKey, new object[]
				{
					text,
					text2,
					text3
				});
			}
			ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, flag), ReportOptions.None);
			ExWatson.SetWatsonReportAlreadySent(exception);
			if (flag)
			{
				ExTraceGlobals.FrameworkTracer.TraceError(0L, "[ReportException()] 'Terminating the process'");
				Environment.Exit(1);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000049C8 File Offset: 0x00002BC8
		public static FileVersionInfo ServerVersion
		{
			get
			{
				return Common.serverVersion.Member;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000049D4 File Offset: 0x00002BD4
		internal static bool IsMultiTenancyEnabled
		{
			get
			{
				return Common.isMultiTenancyEnabled.Member;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000049E0 File Offset: 0x00002BE0
		public static bool IsPartnerHostedOnly
		{
			get
			{
				return Common.isPartnerHostedOnly.Member;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000049EC File Offset: 0x00002BEC
		internal static AuthenticationSchemes AutodiscoverBindingAuthenticationScheme
		{
			get
			{
				return Common.autodiscoverBindingAuthenticationScheme.Member;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000049F8 File Offset: 0x00002BF8
		internal static void StartEnvelope(XmlWriter writer)
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("Autodiscover", "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006");
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A10 File Offset: 0x00002C10
		internal static void EndEnvelope(XmlWriter writer)
		{
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004A24 File Offset: 0x00002C24
		internal static string SafeGetUserAgent(HttpRequest request)
		{
			if (request == null)
			{
				return null;
			}
			return request.UserAgent ?? request.Headers.Get("UserAgent");
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004A48 File Offset: 0x00002C48
		internal static bool CheckClientCertificate(HttpRequest request)
		{
			return FaultInjection.TraceTest<bool>((FaultInjection.LIDs)4213583165U) || (request != null && request.ClientCertificate != null && request.ClientCertificate.IsValid && string.Compare(request.ServerVariables["AUTH_TYPE"], "SSL/PCT", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A9C File Offset: 0x00002C9C
		internal static string GetIdentityNameForTrace(IIdentity identity)
		{
			string result;
			try
			{
				result = identity.Name;
			}
			catch (SystemException)
			{
				result = "<Unknown>";
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004AF0 File Offset: 0x00002CF0
		internal static ExchangePrincipal GetExchangePrincipal(Guid mailboxGuid, OrganizationId organizationId)
		{
			return RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency<ExchangePrincipal>(ServiceLatencyMetadata.ExchangePrincipalLatency, () => ExchangePrincipal.FromMailboxGuid(ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), mailboxGuid, null));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004B54 File Offset: 0x00002D54
		internal static ExchangePrincipal GetExchangePrincipal(ADUser adUser)
		{
			return RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency<ExchangePrincipal>(ServiceLatencyMetadata.ExchangePrincipalLatency, () => ExchangePrincipal.FromADUser(ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(adUser.OrganizationId), adUser));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B8C File Offset: 0x00002D8C
		internal static void LoadAuthenticatingUserInfo(ADRecipient callerRecipient)
		{
			if (callerRecipient != null && HttpContext.Current != null && HttpContext.Current.Items["AuthenticatedUser"] == null)
			{
				HttpContext.Current.Items["AuthenticatedUser"] = callerRecipient.PrimarySmtpAddress;
				if (callerRecipient.OrganizationId != null && callerRecipient.OrganizationId.OrganizationalUnit != null)
				{
					HttpContext.Current.Items["AuthenticatedUserOrganization"] = callerRecipient.OrganizationId.OrganizationalUnit.Name;
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004C17 File Offset: 0x00002E17
		public static bool IsWsSecurityAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004C2A File Offset: 0x00002E2A
		public static bool IsWsSecuritySymmetricKeyAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity/symmetrickey", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004C3D File Offset: 0x00002E3D
		public static bool IsWsSecurityX509CertAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity/x509cert", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C50 File Offset: 0x00002E50
		public static ADSessionSettings SessionSettingsFromAddress(string address)
		{
			if (Common.IsMultiTenancyEnabled && !string.IsNullOrEmpty(address) && SmtpAddress.IsValidSmtpAddress(address))
			{
				try
				{
					return ADSessionSettings.FromTenantAcceptedDomain(new SmtpAddress(address).Domain);
				}
				catch (CannotResolveTenantNameException)
				{
					ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "CreateSessionSettingsByAddresss -- Cannot locate organization for address {0}.", address);
				}
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004E80 File Offset: 0x00003080
		public static void ResolveCaller()
		{
			IIdentity identity = HttpContext.Current.User.Identity;
			if (!(identity is WindowsIdentity) && !(identity is ClientSecurityContextIdentity))
			{
				return;
			}
			try
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.CallerADLatency, delegate()
				{
					DateTime utcNow = DateTime.UtcNow;
					OrganizationId organizationId = (OrganizationId)HttpContext.Current.Items["UserOrganizationId"];
					ADSessionSettings adsessionSettings;
					if (organizationId != null)
					{
						adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
						RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("ADSessionSettingsFromOrgId", (DateTime.UtcNow - utcNow).TotalMilliseconds);
					}
					else
					{
						string memberName = HttpContext.Current.GetMemberName();
						if (string.IsNullOrEmpty(memberName) && identity is SidBasedIdentity)
						{
							memberName = (identity as SidBasedIdentity).MemberName;
						}
						adsessionSettings = Common.SessionSettingsFromAddress(memberName);
						RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("ADSessionSettingsFromAddress", (DateTime.UtcNow - utcNow).TotalMilliseconds);
					}
					HttpContext.Current.Items["ADSessionSettings"] = adsessionSettings;
					utcNow = DateTime.UtcNow;
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 989, "ResolveCaller", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\Common\\Common.cs");
					tenantOrRootOrgRecipientSession.ServerTimeout = new TimeSpan?(Common.RecipientLookupTimeout);
					ADRecipient adrecipient = tenantOrRootOrgRecipientSession.FindBySid(identity.GetSecurityIdentifier());
					RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("ADRecipientSessionFindBySid", (DateTime.UtcNow - utcNow).TotalMilliseconds);
					if (adrecipient != null)
					{
						ExTraceGlobals.FrameworkTracer.TraceDebug<ObjectId>(0L, "ResolveCaller -- Resolved caller is {0}.", adrecipient.Identity);
					}
					HttpContext.Current.Items["CallerRecipient"] = adrecipient;
					ADUser aduser = adrecipient as ADUser;
					if (aduser != null && aduser.NetID != null)
					{
						HttpContext.Current.Items["PassportUniqueId"] = aduser.NetID.ToString();
					}
				});
			}
			catch (NonUniqueRecipientException)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string>(0L, "ResolveCaller -- InternalResolveCaller returned non-unique user for {0}.", identity.Name);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004F18 File Offset: 0x00003118
		public static ADSessionSettings GetSessionSettingsForCallerScope()
		{
			ADSessionSettings adsessionSettings = HttpContext.Current.Items["ADSessionSettings"] as ADSessionSettings;
			if (adsessionSettings == null)
			{
				adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			return adsessionSettings;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004F4C File Offset: 0x0000314C
		public static string AddUserHintToUrl(Uri uri, string userHint)
		{
			userHint = userHint.Replace("@", "..");
			UriBuilder uriBuilder = new UriBuilder(uri);
			string[] segments = uri.Segments;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < segments.Length - 1; i++)
			{
				stringBuilder.Append(segments[i]);
			}
			stringBuilder.Append(userHint);
			stringBuilder.Append('/');
			stringBuilder.Append(segments[segments.Length - 1]);
			uriBuilder.Path = stringBuilder.ToString();
			return uriBuilder.ToString();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004FCC File Offset: 0x000031CC
		public static bool SkipServiceTopologyInDatacenter(VariantConfigurationSnapshot variantConfigurationSnapshot)
		{
			return Common.IsMultiTenancyEnabled && variantConfigurationSnapshot.Autodiscover.SkipServiceTopologyDiscovery.Enabled;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static bool SkipServiceTopologyInDatacenter()
		{
			return Common.IsMultiTenancyEnabled && VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.SkipServiceTopologyDiscovery.Enabled;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005028 File Offset: 0x00003228
		internal static bool IsCustomEmailRedirectEnabled()
		{
			try
			{
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\MSExchange Autodiscover", "CustomEmailRedirect", 0);
				if (value is int)
				{
					return (int)value != 0;
				}
			}
			catch (Exception ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("IsCustomEmailRedirectEnabled", ex.ToString());
			}
			return false;
		}

		// Token: 0x040000AA RID: 170
		public const string XmlDefaultResponseSchema = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006";

		// Token: 0x040000AB RID: 171
		public const string XmlElemAutodiscover = "Autodiscover";

		// Token: 0x040000AC RID: 172
		public const string XmlAtrXmlns = "xmlns";

		// Token: 0x040000AD RID: 173
		public const string XmlElemRequest = "Request";

		// Token: 0x040000AE RID: 174
		public const string XmlElemEMailAddress = "EMailAddress";

		// Token: 0x040000AF RID: 175
		public const string XmlElemLegacyDN = "LegacyDN";

		// Token: 0x040000B0 RID: 176
		public const string XmlElemResponseSchema = "AcceptableResponseSchema";

		// Token: 0x040000B1 RID: 177
		public const string RequestHeaderMapiHttp = "X-MapiHttpCapability";

		// Token: 0x040000B2 RID: 178
		public const string FilterExceptionFromWatson = "FilterExceptionFromWatson";

		// Token: 0x040000B3 RID: 179
		public const string AutodiscoverLocalPath = "/autodiscover/autodiscover.xml";

		// Token: 0x040000B4 RID: 180
		public const string WsSecurityAddress = "wssecurity";

		// Token: 0x040000B5 RID: 181
		public const string WsSecuritySymmetricKeyAddress = "wssecurity/symmetrickey";

		// Token: 0x040000B6 RID: 182
		public const string WsSecurityX509CertAddress = "wssecurity/x509cert";

		// Token: 0x040000B7 RID: 183
		public const string OAuthAddress = "oauth";

		// Token: 0x040000B8 RID: 184
		public const string CertificateString = "Certificate";

		// Token: 0x040000B9 RID: 185
		public const string AuthenticationMethodString = "AuthenticationMethod";

		// Token: 0x040000BA RID: 186
		public const string Anonymous = "Anonymous";

		// Token: 0x040000BB RID: 187
		public const string CallerRecipientItemKey = "CallerRecipient";

		// Token: 0x040000BC RID: 188
		public const string ADSessionSettingsItemKey = "ADSessionSettings";

		// Token: 0x040000BD RID: 189
		public const string UserOrganizationIdItemKey = "UserOrganizationId";

		// Token: 0x040000BE RID: 190
		public const string UserPUIDItemKey = "PassportUniqueId";

		// Token: 0x040000BF RID: 191
		internal const int Ews2010SP2SchemaMinVersion = 1937866977;

		// Token: 0x040000C0 RID: 192
		internal static readonly TimeSpan RecipientLookupTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x040000C1 RID: 193
		public static readonly ExEventLog EventLog = new ExEventLog(new Guid("A5FB0E69-BDB3-429d-B927-01F7A2E0B258"), "MSExchange Autodiscover");

		// Token: 0x040000C2 RID: 194
		public static readonly string PeriodicKey = string.Empty;

		// Token: 0x040000C3 RID: 195
		public static readonly string EndpointContract = "Microsoft.Exchange.Autodiscover.WCF.IAutodiscover";

		// Token: 0x040000C4 RID: 196
		private static readonly string EndpointNameHttps = "Https";

		// Token: 0x040000C5 RID: 197
		private static readonly string ServiceName = "Microsoft.Exchange.Autodiscover.WCF.AutodiscoverService";

		// Token: 0x040000C6 RID: 198
		private static LazyMember<FileVersionInfo> serverVersion = new LazyMember<FileVersionInfo>(() => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location));

		// Token: 0x040000C7 RID: 199
		private static LazyMember<bool> isMultiTenancyEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled);

		// Token: 0x040000C8 RID: 200
		private static LazyMember<bool> isPartnerHostedOnly = new LazyMember<bool>(delegate()
		{
			try
			{
				if (Datacenter.IsPartnerHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return false;
		});

		// Token: 0x040000C9 RID: 201
		private static LazyMember<AuthenticationSchemes> autodiscoverBindingAuthenticationScheme = new LazyMember<AuthenticationSchemes>(delegate()
		{
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/web.config");
			ServicesSection servicesSection = (ServicesSection)configuration.GetSection("system.serviceModel/services");
			if (!servicesSection.Services.ContainsKey(Common.ServiceName))
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "Service {0} was not found in web.config file.", Common.ServiceName);
				return AuthenticationSchemes.None;
			}
			ServiceElement serviceElement = servicesSection.Services[Common.ServiceName];
			ServiceEndpointElement serviceEndpointElement = Common.TryFindEndpointByNameAndContract(serviceElement.Endpoints, Common.EndpointNameHttps, Common.EndpointContract);
			if (serviceEndpointElement == null)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>(0L, "Endpoint name='{0}' contract='{1}' was not found in web.config file.", Common.EndpointNameHttps, Common.EndpointContract);
				return AuthenticationSchemes.None;
			}
			string bindingConfiguration = serviceEndpointElement.BindingConfiguration;
			BindingsSection bindingsSection = (BindingsSection)configuration.GetSection("system.serviceModel/bindings");
			if (!bindingsSection.CustomBinding.Bindings.ContainsKey(bindingConfiguration))
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "Binding {0} was not found in web.config file.", bindingConfiguration);
				return AuthenticationSchemes.None;
			}
			CustomBindingElement customBindingElement = bindingsSection.CustomBinding.Bindings[bindingConfiguration];
			HttpsTransportElement httpsTransportElement = (HttpsTransportElement)customBindingElement[typeof(HttpsTransportElement)];
			if (httpsTransportElement == null)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "https/http transport element not found on binding {0} in web.config file.", bindingConfiguration);
				return AuthenticationSchemes.None;
			}
			return httpsTransportElement.AuthenticationScheme;
		});
	}
}
