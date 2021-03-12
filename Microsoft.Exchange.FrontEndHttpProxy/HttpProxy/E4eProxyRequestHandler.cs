using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000A9 RID: 169
	internal class E4eProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00025B9F File Offset: 0x00023D9F
		internal E4eProxyRequestHandler()
		{
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00025BA7 File Offset: 0x00023DA7
		protected override bool WillAddProtocolSpecificCookiesToClientResponse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00025BAA File Offset: 0x00023DAA
		internal static bool IsE4ePayloadRequest(HttpRequest request)
		{
			return request.FilePath.EndsWith("store.ashx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00025BC0 File Offset: 0x00023DC0
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			if (E4eProxyRequestHandler.IsErrorPageRequest(base.ClientRequest))
			{
				return new AnonymousAnchorMailbox(this);
			}
			if (E4eProxyRequestHandler.IsE4eInvalidStoreRequest(base.ClientRequest))
			{
				this.ThrowRedirectException(E4eProxyRequestHandler.GetErrorUrl(E4eProxyRequestHandler.E4eErrorType.InvalidStoreRequest));
			}
			bool flag = E4eProxyRequestHandler.IsE4ePostPayloadRequest(base.ClientRequest);
			this.GetSenderInfo(flag);
			string text = this.senderEmailAddress;
			if (!string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text))
			{
				string recipientEmailAddress = base.ClientRequest.QueryString["RecipientEmailAddress"];
				if (flag)
				{
					if (E4eBackoffListCache.Instance.ShouldBackOff(text, recipientEmailAddress))
					{
						PerfCounters.HttpProxyCountersInstance.RejectedConnectionCount.Increment();
						this.ThrowRedirectException(E4eProxyRequestHandler.GetErrorUrl(E4eProxyRequestHandler.E4eErrorType.ThrottlingRestriction));
					}
					else
					{
						PerfCounters.HttpProxyCountersInstance.AcceptedConnectionCount.Increment();
					}
				}
				return new SmtpWithDomainFallbackAnchorMailbox(text, this)
				{
					UseServerCookie = true
				};
			}
			if (BEResourceRequestHandler.IsResourceRequest(base.ClientRequest.Url.LocalPath))
			{
				return new AnonymousAnchorMailbox(this);
			}
			string text2 = string.Format("The sender's email address is not valid. Email={0}, SMTP={1}", this.senderEmailAddress, text);
			base.Logger.AppendGenericError("Invalid sender email address", text2);
			throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.EndpointNotFound, text2);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00025CE0 File Offset: 0x00023EE0
		protected override bool HandleBackEndCalculationException(Exception exception, AnchorMailbox anchorMailbox, string label)
		{
			HttpProxyException ex = exception as HttpProxyException;
			if (ex != null && ex.ErrorCode == HttpProxySubErrorCode.DomainNotFound)
			{
				HttpException exception2 = new HttpException(302, E4eProxyRequestHandler.GetErrorUrl(E4eProxyRequestHandler.E4eErrorType.OrgNotExisting));
				return base.HandleBackEndCalculationException(exception2, anchorMailbox, label);
			}
			return base.HandleBackEndCalculationException(exception, anchorMailbox, label);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00025D28 File Offset: 0x00023F28
		protected override bool ShouldCopyCookieToClientResponse(Cookie cookie)
		{
			return !cookie.Name.Equals("X-E4eBudgetType", StringComparison.OrdinalIgnoreCase) && !cookie.Name.Equals("X-E4eEmailAddress", StringComparison.OrdinalIgnoreCase) && !cookie.Name.Equals("X-E4eBackOffUntilUtc", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00025D68 File Offset: 0x00023F68
		protected override void CopySupplementalCookiesToClientResponse()
		{
			this.UpdateBackoffCache();
			if (!string.IsNullOrEmpty(this.senderEmailAddress))
			{
				HttpCookie httpCookie = new HttpCookie("X-SenderEmailAddress", this.senderEmailAddress);
				httpCookie.HttpOnly = true;
				httpCookie.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie);
			}
			if (!string.IsNullOrEmpty(this.senderOrganization))
			{
				HttpCookie httpCookie2 = new HttpCookie("X-SenderOrganization", this.senderOrganization);
				httpCookie2.HttpOnly = true;
				httpCookie2.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie2);
			}
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00025E0F File Offset: 0x0002400F
		private static bool IsE4ePostPayloadRequest(HttpRequest request)
		{
			return request.HttpMethod.Equals(HttpMethod.Post.ToString(), StringComparison.OrdinalIgnoreCase) && E4eProxyRequestHandler.IsE4ePayloadRequest(request);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00025E32 File Offset: 0x00024032
		private static bool IsE4eInvalidStoreRequest(HttpRequest request)
		{
			return request.HttpMethod.Equals(HttpMethod.Get.ToString(), StringComparison.OrdinalIgnoreCase) && E4eProxyRequestHandler.IsE4ePayloadRequest(request);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00025E58 File Offset: 0x00024058
		private static bool IsErrorPageRequest(HttpRequest request)
		{
			if (request.HttpMethod.Equals(HttpMethod.Get.ToString(), StringComparison.OrdinalIgnoreCase) && request.FilePath.EndsWith("ErrorPage.aspx", StringComparison.OrdinalIgnoreCase))
			{
				string value = request.QueryString["code"];
				E4eProxyRequestHandler.E4eErrorType e4eErrorType;
				bool flag = Enum.TryParse<E4eProxyRequestHandler.E4eErrorType>(value, true, out e4eErrorType);
				return flag && (e4eErrorType == E4eProxyRequestHandler.E4eErrorType.OrgNotExisting || e4eErrorType == E4eProxyRequestHandler.E4eErrorType.InvalidStoreRequest);
			}
			return false;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00025EC0 File Offset: 0x000240C0
		private static string GetErrorUrl(E4eProxyRequestHandler.E4eErrorType type)
		{
			string text = string.Format("/Encryption/ErrorPage.aspx?src={0}&code={1}", 0, (int)type);
			try
			{
				string member = HttpProxyGlobals.LocalMachineFqdn.Member;
				if (!string.IsNullOrEmpty(member))
				{
					text = text + "&fe=" + HttpUtility.UrlEncode(member);
				}
			}
			catch (Exception)
			{
			}
			return text;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00025F20 File Offset: 0x00024120
		private void GetSenderInfo(bool isE4ePostPayloadRequest)
		{
			if (isE4ePostPayloadRequest)
			{
				this.senderEmailAddress = base.ClientRequest.QueryString["SenderEmailAddress"];
				this.senderOrganization = base.ClientRequest.QueryString["SenderOrganization"];
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "SMTP-EmailAddressFromUrlQuery");
				return;
			}
			HttpCookie httpCookie = base.ClientRequest.Cookies["X-SenderEmailAddress"];
			this.senderEmailAddress = ((httpCookie == null) ? null : httpCookie.Value);
			HttpCookie httpCookie2 = base.ClientRequest.Cookies["X-SenderOrganization"];
			this.senderOrganization = ((httpCookie2 == null) ? null : httpCookie2.Value);
			base.Logger.Set(HttpProxyMetadata.RoutingHint, "SMTP-EmailAddressFromCookie");
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00025FE8 File Offset: 0x000241E8
		private void UpdateBackoffCache()
		{
			bool flag = E4eProxyRequestHandler.IsE4ePostPayloadRequest(base.ClientRequest);
			if (flag)
			{
				string serverResponseCookieValue = this.GetServerResponseCookieValue("X-E4eBudgetType");
				string serverResponseCookieValue2 = this.GetServerResponseCookieValue("X-E4eEmailAddress");
				string serverResponseCookieValue3 = this.GetServerResponseCookieValue("X-E4eBackOffUntilUtc");
				E4eBackoffListCache.Instance.UpdateCache(serverResponseCookieValue, serverResponseCookieValue2, serverResponseCookieValue3);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00026038 File Offset: 0x00024238
		private string GetServerResponseCookieValue(string cookieName)
		{
			Cookie cookie = base.ServerResponse.Cookies[cookieName];
			if (cookie != null)
			{
				return cookie.Value;
			}
			return string.Empty;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00026068 File Offset: 0x00024268
		private void ThrowRedirectException(string redirectUrl)
		{
			if (!string.IsNullOrEmpty(this.senderEmailAddress))
			{
				HttpCookie httpCookie = new HttpCookie("X-SenderEmailAddress", this.senderEmailAddress);
				httpCookie.HttpOnly = true;
				httpCookie.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie);
			}
			if (!string.IsNullOrEmpty(this.senderOrganization))
			{
				HttpCookie httpCookie2 = new HttpCookie("X-SenderOrganization", this.senderOrganization);
				httpCookie2.HttpOnly = true;
				httpCookie2.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie2);
			}
			throw new HttpException(302, redirectUrl);
		}

		// Token: 0x0400041E RID: 1054
		private string senderEmailAddress;

		// Token: 0x0400041F RID: 1055
		private string senderOrganization;

		// Token: 0x020000AA RID: 170
		private enum E4eErrorType
		{
			// Token: 0x04000421 RID: 1057
			GenericError,
			// Token: 0x04000422 RID: 1058
			ConfigError,
			// Token: 0x04000423 RID: 1059
			ThrottlingRestriction,
			// Token: 0x04000424 RID: 1060
			OrgNotExisting,
			// Token: 0x04000425 RID: 1061
			AuthenticationFailure,
			// Token: 0x04000426 RID: 1062
			UploadFailure,
			// Token: 0x04000427 RID: 1063
			ClientFailure,
			// Token: 0x04000428 RID: 1064
			InvalidCredentials,
			// Token: 0x04000429 RID: 1065
			InvalidEmailAddress,
			// Token: 0x0400042A RID: 1066
			InvalidMetadata,
			// Token: 0x0400042B RID: 1067
			InvalidMessage,
			// Token: 0x0400042C RID: 1068
			MessageNotFound,
			// Token: 0x0400042D RID: 1069
			MessageNotAuthorized,
			// Token: 0x0400042E RID: 1070
			TransientFailure,
			// Token: 0x0400042F RID: 1071
			SessionTimeout,
			// Token: 0x04000430 RID: 1072
			ProbeRequest,
			// Token: 0x04000431 RID: 1073
			ClientException,
			// Token: 0x04000432 RID: 1074
			InvalidStoreRequest,
			// Token: 0x04000433 RID: 1075
			OTPSendPerSession,
			// Token: 0x04000434 RID: 1076
			OTPSendAcrossSession,
			// Token: 0x04000435 RID: 1077
			OTPAttemptPerSession,
			// Token: 0x04000436 RID: 1078
			OTPAttemptAcrossSession,
			// Token: 0x04000437 RID: 1079
			OTPDisabled,
			// Token: 0x04000438 RID: 1080
			OTPPasscodeExpired
		}

		// Token: 0x020000AB RID: 171
		private enum E4eErrorSource
		{
			// Token: 0x0400043A RID: 1082
			Store,
			// Token: 0x0400043B RID: 1083
			Auth,
			// Token: 0x0400043C RID: 1084
			Backend,
			// Token: 0x0400043D RID: 1085
			Client,
			// Token: 0x0400043E RID: 1086
			Generic,
			// Token: 0x0400043F RID: 1087
			OTP
		}

		// Token: 0x020000AC RID: 172
		private class E4eConstants
		{
			// Token: 0x04000440 RID: 1088
			public const string ErrorPage = "ErrorPage.aspx";

			// Token: 0x04000441 RID: 1089
			public const string ErrorCode = "code";

			// Token: 0x04000442 RID: 1090
			public const string PostPayloadFilePath = "store.ashx";

			// Token: 0x04000443 RID: 1091
			public const string RecipientEmailAddress = "RecipientEmailAddress";

			// Token: 0x04000444 RID: 1092
			public const string SenderEmailAddress = "SenderEmailAddress";

			// Token: 0x04000445 RID: 1093
			public const string SenderOrganization = "SenderOrganization";

			// Token: 0x04000446 RID: 1094
			public const string XSenderEmailAddress = "X-SenderEmailAddress";

			// Token: 0x04000447 RID: 1095
			public const string XSenderOrganization = "X-SenderOrganization";

			// Token: 0x04000448 RID: 1096
			public const string XBudgetTypeCookieName = "X-E4eBudgetType";

			// Token: 0x04000449 RID: 1097
			public const string XEmailAddressCookieName = "X-E4eEmailAddress";

			// Token: 0x0400044A RID: 1098
			public const string XBackoffUntilUtcCookieName = "X-E4eBackOffUntilUtc";
		}
	}
}
