using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000C3 RID: 195
	internal static class MSDiagnosticsHeader
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00030B78 File Offset: 0x0002ED78
		public static void AppendInvalidOAuthTokenExceptionToBackendResponse(HttpContext httpContext, InvalidOAuthTokenException exception)
		{
			HttpResponse response = httpContext.Response;
			if (response == null || httpContext.User == null || httpContext.User.Identity == null)
			{
				return;
			}
			OAuthIdentity oauthIdentity = httpContext.User.Identity as OAuthIdentity;
			if (oauthIdentity != null && oauthIdentity.IsAuthenticatedAtBackend)
			{
				MSDiagnosticsHeader.AppendChallengeAndDiagnosticsHeadersToResponse(response, exception.ErrorCategory, exception.Message);
				httpContext.Items["OAuthError"] = exception.ToString();
				httpContext.Items["OAuthErrorCategory"] = exception.ErrorCategory.ToString();
				string str = httpContext.Items["OAuthExtraInfo"] as string;
				httpContext.Items["OAuthExtraInfo"] = str + string.Format("ErrorCode:{0}", exception.ErrorCode);
				return;
			}
			response.AppendHeader(WellKnownHeader.Authentication, Constants.BearerAuthenticationType);
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append(exception.ErrorCode.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(exception.Message);
			response.AppendHeader(MSDiagnosticsHeader.HeaderNameFromBackend, stringBuilder.ToString());
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00030CAD File Offset: 0x0002EEAD
		internal static void AppendChallengeAndDiagnosticsHeadersToResponse(HttpResponse httpResponse, OAuthErrorCategory errorCategory, string errorDescription)
		{
			httpResponse.AppendHeader(Constants.WWWAuthenticateHeader, string.Format("{0}, {1}=\"invalid_token\"", ConfigProvider.Instance.Configuration.ChallengeResponseString, Constants.ChallengeTokens.Error));
			httpResponse.AppendHeader(MSDiagnosticsHeader.HeaderName, MSDiagnosticsHeader.GenerateDiagnosticsString(errorCategory, errorDescription));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00030CEA File Offset: 0x0002EEEA
		public static void AppendToResponse(OAuthErrorCategory errorCategory, string errorDescription, HttpResponse httpResponse = null)
		{
			if (httpResponse == null && HttpContext.Current != null)
			{
				httpResponse = HttpContext.Current.Response;
			}
			if (httpResponse == null)
			{
				return;
			}
			httpResponse.AppendHeader(MSDiagnosticsHeader.HeaderName, MSDiagnosticsHeader.GenerateDiagnosticsString(errorCategory, errorDescription));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00030D18 File Offset: 0x0002EF18
		public static string GenerateDiagnosticsString(OAuthErrorCategory errorCategory, string errorDescription)
		{
			string empty = string.Empty;
			MSDiagnosticsHeader.errorMapping.Value.TryGetValue(errorCategory, out empty);
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("{0};", (int)errorCategory);
			if (!string.IsNullOrEmpty(errorDescription))
			{
				stringBuilder.AppendFormat("reason=\"{0}\";", errorDescription);
			}
			stringBuilder.AppendFormat("error_category=\"{0}\"", empty);
			return stringBuilder.ToString();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00030D84 File Offset: 0x0002EF84
		internal static bool TryParseHeaderFromBackend(string diagnostics, out OAuthErrors errorCode, out string message)
		{
			errorCode = OAuthErrors.NoError;
			message = null;
			if (string.IsNullOrEmpty(diagnostics))
			{
				return false;
			}
			int num = diagnostics.IndexOf('|');
			if (num < 0)
			{
				return false;
			}
			string value = diagnostics.Substring(0, num);
			if (!Enum.TryParse<OAuthErrors>(value, out errorCode))
			{
				return false;
			}
			if (num + 1 >= diagnostics.Length)
			{
				return false;
			}
			message = diagnostics.Substring(num + 1);
			return true;
		}

		// Token: 0x04000658 RID: 1624
		public static readonly string HeaderName = "x-ms-diagnostics";

		// Token: 0x04000659 RID: 1625
		public static readonly string HeaderNameFromBackend = "x-ms-diagnostics-from-backend";

		// Token: 0x0400065A RID: 1626
		private static readonly Lazy<Dictionary<OAuthErrorCategory, string>> errorMapping = new Lazy<Dictionary<OAuthErrorCategory, string>>(() => new Dictionary<OAuthErrorCategory, string>
		{
			{
				OAuthErrorCategory.InvalidSignature,
				"invalid_signature"
			},
			{
				OAuthErrorCategory.InvalidToken,
				"invalid_token"
			},
			{
				OAuthErrorCategory.TokenExpired,
				"token_expired"
			},
			{
				OAuthErrorCategory.InvalidResource,
				"invalid_resource"
			},
			{
				OAuthErrorCategory.InvalidTenant,
				"invalid_tenant"
			},
			{
				OAuthErrorCategory.InvalidUser,
				"invalid_user"
			},
			{
				OAuthErrorCategory.InvalidClient,
				"invalid_client"
			},
			{
				OAuthErrorCategory.InternalError,
				"internal_error"
			},
			{
				OAuthErrorCategory.InvalidGrant,
				"invalid_grant"
			},
			{
				OAuthErrorCategory.InvalidCertificate,
				"invalid_certificate"
			},
			{
				OAuthErrorCategory.OAuthNotAvailable,
				"oauth_not_available"
			}
		});
	}
}
