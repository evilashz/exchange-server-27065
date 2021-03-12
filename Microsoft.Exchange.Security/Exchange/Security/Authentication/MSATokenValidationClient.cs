using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000085 RID: 133
	public class MSATokenValidationClient : IMSATokenValidation
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0002542C File Offset: 0x0002362C
		private MSATokenValidationClient()
		{
			NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
			netNamedPipeBinding.OpenTimeout = new TimeSpan(0, 0, 10);
			netNamedPipeBinding.ReceiveTimeout = new TimeSpan(0, 0, 10);
			netNamedPipeBinding.SendTimeout = new TimeSpan(0, 0, 10);
			netNamedPipeBinding.CloseTimeout = new TimeSpan(0, 0, 10);
			this.proxyPool = DirectoryServiceProxyPool<IMSATokenValidation>.CreateDirectoryServiceProxyPool(string.Format("MSATokenValidation", new object[0]), new EndpointAddress("net.pipe://localhost/MSATokenValidation/service.svc"), ExTraceGlobals.AuthenticationTracer, 100, netNamedPipeBinding, delegate(Exception x, string y)
			{
				if (x is CommunicationException || x is TimeoutException)
				{
					return new TransientException(new LocalizedString(x.ToString()));
				}
				return x;
			}, (Exception x, string y) => x, SecurityEventLogConstants.Tuple_GeneralException, false);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000254F0 File Offset: 0x000236F0
		private static void InitializeProxyPoolIfRequired()
		{
			if (MSATokenValidationClient.globalInstance == null)
			{
				lock (MSATokenValidationClient.lockObject)
				{
					if (MSATokenValidationClient.globalInstance == null)
					{
						MSATokenValidationClient.globalInstance = new MSATokenValidationClient();
					}
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00025544 File Offset: 0x00023744
		public static MSATokenValidationClient Instance
		{
			get
			{
				MSATokenValidationClient.InitializeProxyPoolIfRequired();
				return MSATokenValidationClient.globalInstance;
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00025590 File Offset: 0x00023790
		public bool ParseCompactToken(int tokenType, string token, string siteName, int rpsTicketLifetime, out RPSProfile rpsProfile, out string errorString)
		{
			rpsProfile = null;
			errorString = null;
			RPSProfile tempRPSProfile = null;
			string tempErrorString = null;
			bool authResult = false;
			try
			{
				this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
				{
					authResult = proxy.Client.ParseCompactToken(tokenType, token, siteName, rpsTicketLifetime, out tempRPSProfile, out tempErrorString);
				}, string.Format("Calling ParseCompactToken", new object[0]), 3);
			}
			catch (Exception ex)
			{
				tempErrorString = ex.Message;
			}
			rpsProfile = tempRPSProfile;
			errorString = tempErrorString;
			return authResult;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0002567C File Offset: 0x0002387C
		public bool ValidateCompactToken(int tokenType, string token, string siteName, out string errorString)
		{
			errorString = null;
			bool authResult = false;
			string tempErrorString = null;
			try
			{
				this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
				{
					authResult = proxy.Client.ValidateCompactToken(tokenType, token, siteName, out tempErrorString);
				}, string.Format("Calling ParseCompactToken", new object[0]), 3);
			}
			catch (Exception ex)
			{
				tempErrorString = ex.Message;
			}
			errorString = tempErrorString;
			return authResult;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00025780 File Offset: 0x00023980
		public bool Authenticate(string siteName, string authPolicyOverrideValue, bool sslOffloaded, string headers, string path, string method, string querystring, string body, out RPSProfile rpsProfile, out int? rpsError, out string errorString)
		{
			errorString = null;
			rpsError = null;
			bool authResult = false;
			string tempErrorString = null;
			RPSProfile tempRPSProfile = null;
			int? tempRpsError = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				authResult = proxy.Client.Authenticate(siteName, authPolicyOverrideValue, sslOffloaded, headers, path, method, querystring, body, out tempRPSProfile, out tempRpsError, out tempErrorString);
			}, string.Format("Calling ParseCompactToken", new object[0]), 3);
			errorString = tempErrorString;
			rpsProfile = tempRPSProfile;
			rpsError = tempRpsError;
			return authResult;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000258B4 File Offset: 0x00023AB4
		public bool AuthenticateWithoutBody(string siteName, string authPolicyOverrideValue, bool sslOffloaded, string headers, string path, string method, string querystring, out bool needBody, out RPSProfile rpsProfile, out int? rpsError, out string errorString)
		{
			errorString = null;
			needBody = false;
			rpsError = null;
			bool authResult = false;
			string tempErrorString = null;
			int? tempRpsError = null;
			RPSProfile tempRPSProfile = null;
			bool tempNeedBody = false;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				authResult = proxy.Client.AuthenticateWithoutBody(siteName, authPolicyOverrideValue, sslOffloaded, headers, path, method, querystring, out tempNeedBody, out tempRPSProfile, out tempRpsError, out tempErrorString);
			}, string.Format("Calling ParseCompactToken", new object[0]), 3);
			errorString = tempErrorString;
			rpsProfile = tempRPSProfile;
			needBody = tempNeedBody;
			rpsError = tempRpsError;
			return authResult;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000259CC File Offset: 0x00023BCC
		public string GetRedirectUrl(string constructUrlParam, string siteName, string formattedReturnUrl, string authPolicy, out int? error, out string errorString)
		{
			error = null;
			errorString = null;
			string redirectUrl = null;
			string tempErrorString = null;
			int? tempError = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				redirectUrl = proxy.Client.GetRedirectUrl(constructUrlParam, siteName, formattedReturnUrl, authPolicy, out tempError, out tempErrorString);
			}, "Calling GetRedirectUrl", 3);
			errorString = tempErrorString;
			error = tempError;
			return redirectUrl;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00025A84 File Offset: 0x00023C84
		public string GetSiteProperty(string siteName, string siteProperty)
		{
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			if (siteProperty == null)
			{
				throw new ArgumentNullException("siteProperty");
			}
			string property = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				property = proxy.Client.GetSiteProperty(siteName, siteProperty);
			}, "Calling GetSiteProperty", 3);
			return property;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00025B24 File Offset: 0x00023D24
		public string GetLogoutHeaders(string siteName, out int? error, out string errorString)
		{
			string logoutHeaders = null;
			int? tempError = null;
			string tempErrorString = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				logoutHeaders = proxy.Client.GetLogoutHeaders(siteName, out tempError, out tempErrorString);
			}, "GetLogoutHeaders", 3);
			error = tempError;
			errorString = tempErrorString;
			return logoutHeaders;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00025BAC File Offset: 0x00023DAC
		public string GetRPSEnvironmentConfig()
		{
			string environementConfig = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IMSATokenValidation> proxy)
			{
				environementConfig = proxy.Client.GetRPSEnvironmentConfig();
			}, "GetRPSEnvironmentConfig", 3);
			return environementConfig;
		}

		// Token: 0x04000506 RID: 1286
		internal const int WCFTimeoutInSeconds = 10;

		// Token: 0x04000507 RID: 1287
		internal const string TokenValidationNamedPipeURI = "net.pipe://localhost/MSATokenValidation/service.svc";

		// Token: 0x04000508 RID: 1288
		private static MSATokenValidationClient globalInstance = null;

		// Token: 0x04000509 RID: 1289
		private static object lockObject = new object();

		// Token: 0x0400050A RID: 1290
		private DirectoryServiceProxyPool<IMSATokenValidation> proxyPool;
	}
}
