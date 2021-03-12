using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Net.Wopi;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Web.Configuration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000014 RID: 20
	public class AdfsFederationAuthModule : WSFederationAuthenticationModule
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00006661 File Offset: 0x00004861
		protected AdfsFederationAuthModule()
		{
			AdfsFederationAuthModule.InitStaticVariables();
			if (AdfsFederationAuthModule.IsAdfsAuthenticationEnabled)
			{
				FederatedAuthentication.ServiceConfigurationCreated += this.FederatedAuthentication_ServiceConfigurationCreated;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00006686 File Offset: 0x00004886
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000668D File Offset: 0x0000488D
		internal static bool IsAdfsAuthenticationEnabled { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00006695 File Offset: 0x00004895
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000669C File Offset: 0x0000489C
		internal static bool IsActivityBasedAuthenticationTimeoutEnabled { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000066AB File Offset: 0x000048AB
		internal static EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000066B3 File Offset: 0x000048B3
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000066BA File Offset: 0x000048BA
		internal static TimeSpan TimeBasedAuthenticationTimeoutInterval { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000066C2 File Offset: 0x000048C2
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000066C9 File Offset: 0x000048C9
		internal static bool HasOtherAuthenticationMethod { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000066D1 File Offset: 0x000048D1
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000066D8 File Offset: 0x000048D8
		internal static AdfsIdentifyModelSection Section { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x000066E0 File Offset: 0x000048E0
		public override bool CanReadSignInResponse(HttpRequest request, bool onPage)
		{
			if (string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) && this.IsSignInResponse(request))
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::CanReadSignInResponse]: Skipping check for existing token and alwayss use the new one if exists.");
				return true;
			}
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::CanReadSignInResponse]: Calling base.CanReadSignInResponse().");
			return base.CanReadSignInResponse(request, onPage);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006738 File Offset: 0x00004938
		public override bool IsSignInResponse(HttpRequest request)
		{
			return request.UrlReferrer != null && request.UrlReferrer.IsAbsoluteUri && (request.UrlReferrer.AbsoluteUri.StartsWith(base.Issuer, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(request.UrlReferrer.AbsolutePath) || string.Equals(request.UrlReferrer.AbsolutePath, "/")) && base.IsSignInResponse(request);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000067AC File Offset: 0x000049AC
		internal static void InitStaticVariables()
		{
			if (AdfsFederationAuthModule.initialized)
			{
				return;
			}
			bool flag = !string.IsNullOrWhiteSpace(HttpRuntime.AppDomainAppId) && HttpRuntime.AppDomainAppId.EndsWith("/calendar", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				AdfsFederationAuthModule.IsAdfsAuthenticationEnabled = false;
				AdfsFederationAuthModule.initialized = true;
				return;
			}
			lock (AdfsFederationAuthModule.lockObject)
			{
				if (!AdfsFederationAuthModule.initialized)
				{
					AdfsFederationAuthModule.appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath + '/';
					ADSessionSettings adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, adsessionSettings, 242, "InitStaticVariables", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\AdfsAuth\\AdfsFederationAuthModule.cs");
					PropertyDefinition[] virtualDirectoryPropertyDefinitions = new PropertyDefinition[]
					{
						ADVirtualDirectorySchema.InternalAuthenticationMethodFlags,
						ExchangeWebAppVirtualDirectorySchema.AdfsAuthentication
					};
					ADRawEntry virtualDirectoryObject = Utility.GetVirtualDirectoryObject(Guid.Empty, topologyConfigurationSession, virtualDirectoryPropertyDefinitions);
					AdfsFederationAuthModule.authenticationMethods = (AuthenticationMethodFlags)virtualDirectoryObject[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags];
					AdfsFederationAuthModule.HasOtherAuthenticationMethod = ((AdfsFederationAuthModule.authenticationMethods & (AuthenticationMethodFlags.Fba | AuthenticationMethodFlags.WindowsIntegrated)) != AuthenticationMethodFlags.None);
					AdfsFederationAuthModule.IsAdfsAuthenticationEnabled = (bool)virtualDirectoryObject[ExchangeWebAppVirtualDirectorySchema.AdfsAuthentication];
					Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, "Successfully read ADFS Authentication configurations: HasOtherAutnenticationMethod, IsAdfsAuthenticationEnabled.");
					if (AdfsFederationAuthModule.IsAdfsAuthenticationEnabled)
					{
						bool isTestEnvironment = false;
						Utility.TryReadConfigBool("AdfsIsTest", out isTestEnvironment);
						AdfsFederationAuthModule.IsTestEnvironment = isTestEnvironment;
						ADPropertyDefinition[] properties = new ADPropertyDefinition[]
						{
							OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled,
							OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval,
							OrganizationSchema.AdfsAuthenticationRawConfiguration
						};
						ADRawEntry adrawEntry = topologyConfigurationSession.ReadADRawEntry(adsessionSettings.RootOrgId, properties);
						AdfsFederationAuthModule.IsActivityBasedAuthenticationTimeoutEnabled = !(bool)adrawEntry[OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled];
						AdfsFederationAuthModule.ActivityBasedAuthenticationTimeoutInterval = (EnhancedTimeSpan)adrawEntry[OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval];
						Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, "Successfully read ADFS Authentication configurations: IsActivityBasedAuthenticationTimeoutEnabled, ActivityBasedAuthenticationTimeoutInterval.");
						string text = (string)adrawEntry[OrganizationSchema.AdfsAuthenticationRawConfiguration];
						if (!AdfsAuthenticationConfig.TryDecode(text, out AdfsFederationAuthModule.adfsRawConfiguration) || string.IsNullOrEmpty(AdfsFederationAuthModule.adfsRawConfiguration))
						{
							Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, string.Format("Cannot enable ADFS Authentication because the configuration string is not set. String value: {0}", text ?? "null"));
							AdfsFederationAuthModule.IsAdfsAuthenticationEnabled = false;
						}
						else
						{
							int num;
							if (Utility.TryReadConfigInt("AdfsAuthModuleActivityBasedTimeoutIntervalInSeconds", out num))
							{
								AdfsFederationAuthModule.ActivityBasedAuthenticationTimeoutInterval = TimeSpan.FromSeconds((double)num);
								Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, "ADFS Activity based time out interval found in web.config.");
							}
							int num2;
							if (Utility.TryReadConfigInt("AdfsAuthModuleTimeoutIntervalInSeconds", out num2))
							{
								AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval = TimeSpan.FromSeconds((double)num2);
								Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, "ADFS Time based time out interval found in web.config.");
							}
							else
							{
								AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval = AdfsFederationAuthModule.timeBasedAuthenticationTimeoutIntervalDefault;
							}
						}
						AdfsFederationAuthModule.Section = new AdfsIdentifyModelSection();
						try
						{
							using (XmlReader xmlReader = XmlReader.Create(new StringReader(AdfsFederationAuthModule.adfsRawConfiguration)))
							{
								AdfsFederationAuthModule.Section.Deserialize(xmlReader);
							}
						}
						catch (XmlException ex)
						{
							string message = string.Format("Fail to parse ADFS raw configuration XML: {0}. Input string: {1}", ex.Message, AdfsFederationAuthModule.adfsRawConfiguration);
							Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, message);
							Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0, 0L, message);
							AdfsFederationAuthModule.IsAdfsAuthenticationEnabled = false;
						}
						catch (ConfigurationErrorsException ex2)
						{
							string message2 = string.Format("Fail to parse ADFS raw configuration XML: {0}. Input string: {1}", ex2.Message, AdfsFederationAuthModule.adfsRawConfiguration);
							Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation(0, 0L, message2);
							Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0, 0L, message2);
							AdfsFederationAuthModule.IsAdfsAuthenticationEnabled = false;
						}
					}
					AdfsFederationAuthModule.initialized = true;
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006B50 File Offset: 0x00004D50
		protected override void InitializeModule(HttpApplication application)
		{
			if (AdfsFederationAuthModule.IsAdfsAuthenticationEnabled)
			{
				base.InitializeModule(application);
				base.SecurityTokenReceived += this.AdfsFederationAuthModule_SecurityTokenReceived;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006B74 File Offset: 0x00004D74
		protected override void OnAuthenticateRequest(object sender, EventArgs eventArgs)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			if (EDiscoveryExportToolRequestPathHandler.IsEDiscoveryExportToolRequest(context.Request))
			{
				context.User = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
				return;
			}
			this.InternalOnAuthenticateRequest(sender, eventArgs);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006BB5 File Offset: 0x00004DB5
		protected override void OnEndRequest(object sender, EventArgs eventArgs)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006BB7 File Offset: 0x00004DB7
		protected override void OnPostAuthenticateRequest(object sender, EventArgs eventArgs)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006BBC File Offset: 0x00004DBC
		protected override void OnRedirectingToIdentityProvider(RedirectingToIdentityProviderEventArgs eventArgs)
		{
			base.OnRedirectingToIdentityProvider(eventArgs);
			SignInRequestMessage signInRequestMessage = eventArgs.SignInRequestMessage;
			HttpRequest request = HttpContext.Current.Request;
			UriBuilder uriBuilder = new UriBuilder(request.Url.Scheme, request.Url.Host, request.Url.Port, AdfsFederationAuthModule.appDomainAppVirtualPath);
			Uri uri = uriBuilder.Uri;
			signInRequestMessage.Realm = uri.AbsoluteUri;
			if (this.IsSignInResponse(request))
			{
				signInRequestMessage.Freshness = "0";
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006C38 File Offset: 0x00004E38
		protected override void InitializePropertiesFromConfiguration(string serviceName)
		{
			if (AdfsFederationAuthModule.IsAdfsAuthenticationEnabled)
			{
				ServiceElement element = AdfsFederationAuthModule.Section.ServiceElements.GetElement(serviceName);
				WSFederationAuthenticationElement wsfederation = element.FederatedAuthentication.WSFederation;
				base.Issuer = wsfederation.Issuer;
				base.Reply = wsfederation.Reply;
				base.RequireHttps = wsfederation.RequireHttps;
				base.Freshness = wsfederation.Freshness;
				base.AuthenticationType = wsfederation.AuthenticationType;
				base.HomeRealm = wsfederation.HomeRealm;
				base.Policy = wsfederation.Policy;
				base.Realm = wsfederation.Realm;
				base.Reply = wsfederation.Reply;
				base.SignOutReply = wsfederation.SignOutReply;
				base.Request = wsfederation.Request;
				base.RequestPtr = wsfederation.RequestPtr;
				base.Resource = wsfederation.Resource;
				base.SignInQueryString = wsfederation.SignInQueryString;
				base.SignOutQueryString = wsfederation.SignOutQueryString;
				base.PassiveRedirectEnabled = wsfederation.PassiveRedirectEnabled;
				base.PersistentCookiesOnPassiveRedirects = wsfederation.PersistentCookiesOnPassiveRedirects;
				if (AdfsFederationAuthModule.IsTestEnvironment)
				{
					base.ServiceConfiguration.CertificateValidationMode = X509CertificateValidationMode.None;
					base.ServiceConfiguration.CertificateValidator = X509CertificateValidator.None;
					return;
				}
				base.ServiceConfiguration.CertificateValidationMode = element.CertificateValidationElement.ValidationMode;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006D74 File Offset: 0x00004F74
		private void InternalOnAuthenticateRequest(object sender, EventArgs eventArgs)
		{
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Entry.");
			HttpContext context = ((HttpApplication)sender).Context;
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			if (request.IsAnonymousAuthFolderRequest())
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Request is auth folder anonymous.");
				return;
			}
			if (request.IsAdfsLogoffRequest())
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Is a ADFS logoff request.");
				Utility.DeleteFbaAuthCookies(request, response);
				AdfsTimeBasedLogonCookie.DeleteAdfsAuthCookies(request, response);
				WSFederationAuthenticationModule.FederatedSignOut(this.GetSignOutUri(), null);
				return;
			}
			if (Utility.IsResourceRequest(request.Path) && (!AuthCommon.IsFrontEnd || Utility.IsOwaRequestWithRoutingHint(request) || Utility.HasResourceRoutingHint(request) || Utility.IsAnonymousResourceRequest(request)))
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Request is for a resource that does not require authentication.");
				context.User = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
				return;
			}
			if (WopiRequestPathHandler.IsWopiRequest(context.Request, AuthCommon.IsFrontEnd))
			{
				return;
			}
			bool flag = this.IsSignInResponse(request);
			bool flag2 = request.IsSignOutCleanupRequest();
			if (!request.IsAuthenticatedByAdfs() && !flag && !flag2 && request.IsAuthenticated && !request.ExplicitPreferAdfsAuthentication())
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Already authenticated by another method.");
				return;
			}
			if (request.PreferAdfsAuthentication() || flag || flag2)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Calling the base class to authenticate the request.");
				try
				{
					base.OnAuthenticateRequest(sender, eventArgs);
				}
				catch (SecurityTokenException arg)
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<SecurityTokenException>(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Could not call base.OnAuthenticateRequest: {0}.", arg);
					response.Redirect("/owa/auth/errorfe.aspx?msg=WrongAudienceUriOrBadSigningCert");
				}
				if (flag2)
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Request is signout cleanup request.");
					AdfsTimeBasedLogonCookie.DeleteAdfsAuthCookies(request, response);
					if ((AdfsFederationAuthModule.authenticationMethods & AuthenticationMethodFlags.Fba) > AuthenticationMethodFlags.None)
					{
						Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Redirecting to FBA logout.");
						context.Response.Redirect("logoff.aspx");
					}
					else
					{
						response.Flush();
						response.End();
					}
				}
				else if (request.PreferAdfsAuthentication() && !request.IsAuthenticatedByAdfs())
				{
					if (!request.FilePath.StartsWith(AdfsFederationAuthModule.appDomainAppVirtualPath, StringComparison.OrdinalIgnoreCase))
					{
						Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Redirecting to {0}.", request.RawUrl);
						response.Redirect(request.RawUrl.Insert(request.FilePath.Length, "/"));
					}
					else if (!request.IsAjaxRequest() && !request.IsNotOwaGetOrOehRequest())
					{
						Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Redirecting AJAX request to ADFS.");
						this.RedirectToIdentityProvider("passive", request.RawUrl, base.PersistentCookiesOnPassiveRedirects);
					}
				}
			}
			if (!flag && request.IsAuthenticatedByAdfs())
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Response is not for sign in.");
				AdfsTimeBasedLogonCookie adfsTimeBasedLogonCookie;
				if (!AdfsTimeBasedLogonCookie.TryCreateFromHttpRequest(request, out adfsTimeBasedLogonCookie) || !AdfsTimeBasedLogonCookie.Validate(adfsTimeBasedLogonCookie.LogonTime, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval))
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Cookie timeout, redirect to logon page.");
					this.Relogon();
					return;
				}
				if (AdfsFederationAuthModule.IsActivityBasedAuthenticationTimeoutEnabled && !AdfsTimeBasedLogonCookie.Validate(adfsTimeBasedLogonCookie.LastActivityTime, AdfsFederationAuthModule.ActivityBasedAuthenticationTimeoutInterval))
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Activity timeout, redirect to logon page.");
					this.Relogon();
					return;
				}
				if (OwaAuthenticationHelper.IsOwaUserActivityRequest(request))
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::InternalOnAuthenticateRequest]: Request is a user action.");
					adfsTimeBasedLogonCookie.Renew();
					adfsTimeBasedLogonCookie.AddToResponse(request, response);
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000070AC File Offset: 0x000052AC
		private void Relogon()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			FederatedAuthentication.SessionAuthenticationModule.DeleteSessionTokenCookie();
			Utility.DeleteFbaAuthCookies(request, response);
			AdfsTimeBasedLogonCookie.DeleteAdfsAuthCookies(request, response);
			string returnUrlForRelogon = this.GetReturnUrlForRelogon(request);
			SignInRequestMessage signInRequestMessage = base.CreateSignInRequest("passive", returnUrlForRelogon, base.PersistentCookiesOnPassiveRedirects);
			RedirectingToIdentityProviderEventArgs redirectingToIdentityProviderEventArgs = new RedirectingToIdentityProviderEventArgs(signInRequestMessage);
			this.OnRedirectingToIdentityProvider(redirectingToIdentityProviderEventArgs);
			if (!redirectingToIdentityProviderEventArgs.Cancel)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::Relogon]: Redirecting.");
				this.LogoutAndRedirectToLogonPage(returnUrlForRelogon, redirectingToIdentityProviderEventArgs.SignInRequestMessage.RequestUrl);
				httpContext.ApplicationInstance.CompleteRequest();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000714C File Offset: 0x0000534C
		private void LogoutAndRedirectToLogonPage(string returnUrl, string requestUrl)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			if (request.IsAjaxRequest() || request.IsNotOwaGetOrOehRequest())
			{
				if (this.IsSignInResponse(request))
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::RedirectToLogonPage]: Explict log off and redirect to login page.");
					WSFederationAuthenticationModule.FederatedSignOut(this.GetSignOutUri(), request.Url);
					return;
				}
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsFederationAuthModule::RedirectToLogonPage]: Request is non GET request. Return 440.");
				response.StatusCode = 440;
				response.StatusDescription = "Login Timeout";
				response.AppendToLog("logoffReason=UnauthenticatedGuest");
				return;
			}
			else
			{
				if (returnUrl.IndexOf("/closewindow.aspx", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsFederationAuthModule::RedirectToLogonPage]: closewindow.aspx found. Redirecting to {0}.", requestUrl);
					response.Redirect(requestUrl);
					return;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("/ecp/");
				stringBuilder.Append("auth/TimeoutLogout.aspx");
				stringBuilder.Append("?");
				stringBuilder.Append("ru");
				stringBuilder.Append("=");
				stringBuilder.Append(HttpUtility.UrlEncode(requestUrl));
				string text = stringBuilder.ToString();
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsFederationAuthModule::RedirectToLogonPage]: Redirecting to {0}.", text);
				response.Redirect(text, false);
				return;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007278 File Offset: 0x00005478
		private string GetReturnUrlForRelogon(HttpRequest request)
		{
			string text = null;
			if (this.IsSignInResponse(request))
			{
				text = this.GetReturnUrlFromResponse(request);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = request.RawUrl;
			}
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsFederationAuthModule::GetReturnUrlForRelogon]: returnUrl={0}.", text);
			return text;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000072BC File Offset: 0x000054BC
		private void AdfsFederationAuthModule_SecurityTokenReceived(object sender, SecurityTokenReceivedEventArgs e)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			SamlSecurityToken samlSecurityToken = e.SecurityToken as SamlSecurityToken;
			if (samlSecurityToken != null)
			{
				DateTime validFrom = samlSecurityToken.ValidFrom;
				foreach (SamlStatement samlStatement in samlSecurityToken.Assertion.Statements)
				{
					SamlAuthenticationStatement samlAuthenticationStatement = samlStatement as SamlAuthenticationStatement;
					if (samlAuthenticationStatement != null)
					{
						TimeSpan timeSpan = validFrom.Subtract(samlAuthenticationStatement.AuthenticationInstant);
						if (!AdfsTimeBasedLogonCookie.Validate(timeSpan, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval) || (AdfsFederationAuthModule.IsActivityBasedAuthenticationTimeoutEnabled && !AdfsTimeBasedLogonCookie.Validate(timeSpan, AdfsFederationAuthModule.ActivityBasedAuthenticationTimeoutInterval)))
						{
							e.Cancel = true;
							break;
						}
						break;
					}
				}
			}
			if (e.Cancel)
			{
				this.Relogon();
				return;
			}
			AdfsTimeBasedLogonCookie adfsTimeBasedLogonCookie = AdfsTimeBasedLogonCookie.CreateFromCurrentTime();
			adfsTimeBasedLogonCookie.AddToResponse(request, response);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000073A8 File Offset: 0x000055A8
		private void FederatedAuthentication_ServiceConfigurationCreated(object sender, ServiceConfigurationCreatedEventArgs e)
		{
			e.ServiceConfiguration = new AdfsServiceConfiguration(AdfsFederationAuthModule.Section.ServiceElements.GetElement(string.Empty));
			e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(new AdfsSessionSecurityTokenHandler());
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000073E0 File Offset: 0x000055E0
		private Uri GetSignOutUri()
		{
			Uri uri = new Uri(base.Issuer);
			if (string.IsNullOrEmpty(uri.Query))
			{
				uri = new Uri(base.Issuer + "?wa=wsignout1.0");
			}
			else
			{
				uri = new Uri(base.Issuer + "&wa=wsignout1.0");
			}
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<Uri>(0L, "[AdfsFederationAuthModule::GetSignOutUri]: signoutUri={0}.", uri);
			return uri;
		}

		// Token: 0x04000128 RID: 296
		public const string SignOutParameter = "wa=wsignout1.0";

		// Token: 0x04000129 RID: 297
		public const string ActivityBasedAuthenticationTimeoutIntervalKey = "AdfsAuthModuleActivityBasedTimeoutIntervalInSeconds";

		// Token: 0x0400012A RID: 298
		public const string TimeBasedAuthenticationTimeoutIntervalKey = "AdfsAuthModuleTimeoutIntervalInSeconds";

		// Token: 0x0400012B RID: 299
		public const string RedirectUrlParam = "ru";

		// Token: 0x0400012C RID: 300
		public const string FrontEndErrorPage = "/owa/auth/errorfe.aspx";

		// Token: 0x0400012D RID: 301
		private const string CalendarVDirPostfix = "/calendar";

		// Token: 0x0400012E RID: 302
		private const string IsTestWebConfigKey = "AdfsIsTest";

		// Token: 0x0400012F RID: 303
		private static readonly TimeSpan timeBasedAuthenticationTimeoutIntervalDefault = TimeSpan.FromHours(23.0);

		// Token: 0x04000130 RID: 304
		private static AuthenticationMethodFlags authenticationMethods;

		// Token: 0x04000131 RID: 305
		private static bool initialized = false;

		// Token: 0x04000132 RID: 306
		private static bool IsTestEnvironment;

		// Token: 0x04000133 RID: 307
		private static string appDomainAppVirtualPath = null;

		// Token: 0x04000134 RID: 308
		private static string adfsRawConfiguration;

		// Token: 0x04000135 RID: 309
		private static object lockObject = new object();
	}
}
