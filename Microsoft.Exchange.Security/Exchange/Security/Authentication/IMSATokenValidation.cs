using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000070 RID: 112
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.Security.Authentication.IMSATokenValidation")]
	public interface IMSATokenValidation
	{
		// Token: 0x060003B6 RID: 950
		[OperationContract]
		bool ParseCompactToken(int tokenType, string token, string siteName, int rpsTicketLifetime, out RPSProfile rpsProfile, out string errorString);

		// Token: 0x060003B7 RID: 951
		[OperationContract]
		bool ValidateCompactToken(int tokenType, string token, string siteName, out string errorString);

		// Token: 0x060003B8 RID: 952
		[OperationContract]
		bool Authenticate(string siteName, string authPolicyOverrideValue, bool sslOffloaded, string headers, string path, string method, string querystring, string body, out RPSProfile rpsProfile, out int? rpsError, out string errorString);

		// Token: 0x060003B9 RID: 953
		[OperationContract]
		bool AuthenticateWithoutBody(string siteName, string authPolicyOverrideValue, bool sslOffloaded, string headers, string path, string method, string querystring, out bool needBody, out RPSProfile rpsProfile, out int? rpsError, out string errorString);

		// Token: 0x060003BA RID: 954
		[OperationContract]
		string GetRedirectUrl(string constructUrlParam, string siteName, string returnUrl, string authPolicy, out int? error, out string errorString);

		// Token: 0x060003BB RID: 955
		[OperationContract]
		string GetSiteProperty(string siteName, string siteProperty);

		// Token: 0x060003BC RID: 956
		[OperationContract]
		string GetLogoutHeaders(string siteName, out int? error, out string errorString);

		// Token: 0x060003BD RID: 957
		[OperationContract]
		string GetRPSEnvironmentConfig();
	}
}
