using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using Microsoft.Passport.RPS;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000115 RID: 277
	public class RPSHttpAuthClient : IDisposable
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x0003B884 File Offset: 0x00039A84
		public RPSHttpAuthClient(bool isMSA, RPS orgIdRps, int rpsTicketLifetime = 3600)
		{
			if (orgIdRps == null)
			{
				throw new ArgumentException("RPS object cannot be null");
			}
			this.httpAuth = new RPSHttpAuth(orgIdRps);
			this.isMSA = isMSA;
			this.rpsTicketLifetime = rpsTicketLifetime;
			this.orgIdRps = orgIdRps;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0003B8BB File Offset: 0x00039ABB
		public void Dispose()
		{
			if (this.httpAuth != null)
			{
				this.httpAuth.Dispose();
				this.httpAuth = null;
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		public RPSProfile Authenticate(string siteName, string authPolicyOverrideValue, bool sslOffloaded, HttpRequest request, RPSPropBag propBag, out int? rpsErrorCode, out string errorString, out RPSTicket rpsTicket)
		{
			RPSProfile rpsprofile = null;
			errorString = null;
			rpsTicket = null;
			rpsErrorCode = null;
			if (!this.isMSA)
			{
				try
				{
					rpsTicket = this.httpAuth.Authenticate(siteName, request, propBag);
					if (rpsTicket != null)
					{
						propBag["SlidingWindow"] = 0;
						if (!string.IsNullOrEmpty(authPolicyOverrideValue))
						{
							propBag["AuthPolicy"] = authPolicyOverrideValue;
						}
						if (!rpsTicket.Validate(propBag))
						{
							errorString = "Validate failed.";
						}
						else
						{
							rpsprofile = RPSCommon.ParseRPSTicket(rpsTicket, this.rpsTicketLifetime, this.GetHashCode(), false, out errorString, false);
							if (rpsprofile != null)
							{
								if (propBag["RPSAuthState"] != null)
								{
									rpsprofile.RPSAuthState = (uint)propBag["RPSAuthState"];
								}
								rpsprofile.ResponseHeader = (string)propBag["RPSRespHeaders"];
							}
						}
					}
				}
				catch (COMException ex)
				{
					rpsErrorCode = new int?(ex.ErrorCode);
					errorString = ex.Message;
				}
				return rpsprofile;
			}
			if (request == null)
			{
				throw new ArgumentException("request cannot be null.");
			}
			try
			{
				string headers = request.ServerVariables["ALL_RAW"];
				string path = request.ServerVariables["PATH_INFO"];
				string method = request.ServerVariables["REQUEST_METHOD"];
				string querystring = request.ServerVariables["QUERY_STRING"];
				bool flag = false;
				MSATokenValidationClient.Instance.AuthenticateWithoutBody(siteName, authPolicyOverrideValue, sslOffloaded, headers, path, method, querystring, out flag, out rpsprofile, out rpsErrorCode, out errorString);
				if (rpsprofile == null && flag)
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					request.InputStream.Seek(0L, SeekOrigin.Begin);
					byte[] bytes = request.BinaryRead(request.TotalBytes);
					string @string = utf8Encoding.GetString(bytes);
					MSATokenValidationClient.Instance.Authenticate(siteName, authPolicyOverrideValue, sslOffloaded, headers, path, method, querystring, @string, out rpsprofile, out rpsErrorCode, out errorString);
				}
				return rpsprofile;
			}
			catch (Exception ex2)
			{
				errorString = ex2.Message;
			}
			return null;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public string GetLogoutHeaders(string siteName, out int? rpsErrorCode, out string errorString)
		{
			rpsErrorCode = null;
			errorString = null;
			string result = null;
			if (!this.isMSA)
			{
				try
				{
					return this.httpAuth.GetLogoutHeaders(siteName);
				}
				catch (COMException ex)
				{
					rpsErrorCode = new int?(ex.ErrorCode);
					errorString = ex.Message;
					return result;
				}
			}
			result = MSATokenValidationClient.Instance.GetLogoutHeaders(siteName, out rpsErrorCode, out errorString);
			return result;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0003BB58 File Offset: 0x00039D58
		public void WriteHeaders(HttpResponse response, string headers)
		{
			this.httpAuth.WriteHeaders(response, headers);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003BB67 File Offset: 0x00039D67
		public string GetSiteProperty(string siteName, string siteProperty)
		{
			if (!this.isMSA)
			{
				return RPSCommon.GetSiteProperty(this.orgIdRps, siteName, siteProperty);
			}
			return MSATokenValidationClient.Instance.GetSiteProperty(siteName, siteProperty);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003BB8B File Offset: 0x00039D8B
		public string GetRedirectUrl(string constructUrlParam, string siteName, string formattedReturnUrl, string authPolicy, out int? rpsErrorCode, out string errorString)
		{
			if (!this.isMSA)
			{
				return RPSCommon.GetRedirectUrl(this.orgIdRps, constructUrlParam, siteName, formattedReturnUrl, authPolicy, out rpsErrorCode, out errorString);
			}
			return MSATokenValidationClient.Instance.GetRedirectUrl(constructUrlParam, siteName, formattedReturnUrl, authPolicy, out rpsErrorCode, out errorString);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003BBBD File Offset: 0x00039DBD
		public string GetCurrentEnvironment()
		{
			if (!this.isMSA)
			{
				return RPSCommon.GetRPSEnvironmentConfig(this.orgIdRps);
			}
			return MSATokenValidationClient.Instance.GetRPSEnvironmentConfig();
		}

		// Token: 0x04000834 RID: 2100
		private RPSHttpAuth httpAuth;

		// Token: 0x04000835 RID: 2101
		private readonly bool isMSA;

		// Token: 0x04000836 RID: 2102
		private readonly int rpsTicketLifetime;

		// Token: 0x04000837 RID: 2103
		private RPS orgIdRps;
	}
}
