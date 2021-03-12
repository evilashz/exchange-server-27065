using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net.LinkedIn;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000F4 RID: 244
	internal static class NetServerException
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x000162FC File Offset: 0x000144FC
		static NetServerException()
		{
			NetServerException.stringIDs.Add(757455353U, "LinkedInRequestCookiesMissingOAuthSecret");
			NetServerException.stringIDs.Add(1078344448U, "InvalidMserveRequest");
			NetServerException.stringIDs.Add(703035395U, "EmptyAppAuthenticationResponse");
			NetServerException.stringIDs.Add(1478858759U, "EmptyRedirectUrlAuthenticationResponse");
			NetServerException.stringIDs.Add(1425604752U, "LinkedInQueryStringMissingOAuthVerifier");
			NetServerException.stringIDs.Add(3926725349U, "LinkedInQueryStringMissingOAuthToken");
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000163B0 File Offset: 0x000145B0
		public static LocalizedString InvalidOperationError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("InvalidOperationError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000163E4 File Offset: 0x000145E4
		public static LocalizedString LinkedInFailedToAuthenticateApp(LinkedInConfig configuration)
		{
			return new LocalizedString("LinkedInFailedToAuthenticateApp", NetServerException.ResourceManager, new object[]
			{
				configuration
			});
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001640C File Offset: 0x0001460C
		public static LocalizedString AppAuthenticationResponseTooLarge(long length)
		{
			return new LocalizedString("AppAuthenticationResponseTooLarge", NetServerException.ResourceManager, new object[]
			{
				length
			});
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00016439 File Offset: 0x00014639
		public static LocalizedString LinkedInRequestCookiesMissingOAuthSecret
		{
			get
			{
				return new LocalizedString("LinkedInRequestCookiesMissingOAuthSecret", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00016450 File Offset: 0x00014650
		public static LocalizedString InvalidMserveRequest
		{
			get
			{
				return new LocalizedString("InvalidMserveRequest", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00016468 File Offset: 0x00014668
		public static LocalizedString LinkedInInvalidOAuthResponseMissingToken(string response)
		{
			return new LocalizedString("LinkedInInvalidOAuthResponseMissingToken", NetServerException.ResourceManager, new object[]
			{
				response
			});
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00016490 File Offset: 0x00014690
		public static LocalizedString InvalidAppAuthenticationResponse(string response, FacebookAuthenticatorConfig configuration)
		{
			return new LocalizedString("InvalidAppAuthenticationResponse", NetServerException.ResourceManager, new object[]
			{
				response,
				configuration
			});
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000164BC File Offset: 0x000146BC
		public static LocalizedString EmptyAppAuthenticationResponse
		{
			get
			{
				return new LocalizedString("EmptyAppAuthenticationResponse", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000164D4 File Offset: 0x000146D4
		public static LocalizedString MserveCacheEndpointNotFound(string endpoint, string error)
		{
			return new LocalizedString("MserveCacheEndpointNotFound", NetServerException.ResourceManager, new object[]
			{
				endpoint,
				error
			});
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00016500 File Offset: 0x00014700
		public static LocalizedString EmptyRedirectUrlAuthenticationResponse
		{
			get
			{
				return new LocalizedString("EmptyRedirectUrlAuthenticationResponse", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00016518 File Offset: 0x00014718
		public static LocalizedString MserveCacheTimeoutError(string error)
		{
			return new LocalizedString("MserveCacheTimeoutError", NetServerException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00016540 File Offset: 0x00014740
		public static LocalizedString ExceptionWithStack(LocalizedString exceptionMessage, string stackTrace)
		{
			return new LocalizedString("ExceptionWithStack", NetServerException.ResourceManager, new object[]
			{
				exceptionMessage,
				stackTrace
			});
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00016574 File Offset: 0x00014774
		public static LocalizedString NestedExceptionMsg(LocalizedString message, LocalizedString innerMessage)
		{
			return new LocalizedString("NestedExceptionMsg", NetServerException.ResourceManager, new object[]
			{
				message,
				innerMessage
			});
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000165AA File Offset: 0x000147AA
		public static LocalizedString LinkedInQueryStringMissingOAuthVerifier
		{
			get
			{
				return new LocalizedString("LinkedInQueryStringMissingOAuthVerifier", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000165C4 File Offset: 0x000147C4
		public static LocalizedString UnexpectedAppAuthenticationResponse(HttpStatusCode code, string body, FacebookAuthenticatorConfig configuration)
		{
			return new LocalizedString("UnexpectedAppAuthenticationResponse", NetServerException.ResourceManager, new object[]
			{
				code,
				body,
				configuration
			});
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000165FC File Offset: 0x000147FC
		public static LocalizedString InvalidDataError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("InvalidDataError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00016630 File Offset: 0x00014830
		public static LocalizedString EndpointNotFoundError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("EndpointNotFoundError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00016664 File Offset: 0x00014864
		public static LocalizedString QuotaExceededError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("QuotaExceededError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00016695 File Offset: 0x00014895
		public static LocalizedString LinkedInQueryStringMissingOAuthToken
		{
			get
			{
				return new LocalizedString("LinkedInQueryStringMissingOAuthToken", NetServerException.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000166AC File Offset: 0x000148AC
		public static LocalizedString LinkedInInvalidOAuthResponseMissingTokenSecret(string response)
		{
			return new LocalizedString("LinkedInInvalidOAuthResponseMissingTokenSecret", NetServerException.ResourceManager, new object[]
			{
				response
			});
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000166D4 File Offset: 0x000148D4
		public static LocalizedString CommunicationError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("CommunicationError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00016708 File Offset: 0x00014908
		public static LocalizedString PermanentMserveCacheError(string error)
		{
			return new LocalizedString("PermanentMserveCacheError", NetServerException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016730 File Offset: 0x00014930
		public static LocalizedString LinkedInUnexpectedAppAuthenticationResponse(HttpStatusCode code, string body, LinkedInConfig configuration)
		{
			return new LocalizedString("LinkedInUnexpectedAppAuthenticationResponse", NetServerException.ResourceManager, new object[]
			{
				code,
				body,
				configuration
			});
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00016768 File Offset: 0x00014968
		public static LocalizedString InvalidAppAuthorizationCode(string errorMessage, FacebookAuthenticatorConfig configuration)
		{
			return new LocalizedString("InvalidAppAuthorizationCode", NetServerException.ResourceManager, new object[]
			{
				errorMessage,
				configuration
			});
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00016794 File Offset: 0x00014994
		public static LocalizedString FailedToAuthenticateApp(FacebookAuthenticatorConfig configuration)
		{
			return new LocalizedString("FailedToAuthenticateApp", NetServerException.ResourceManager, new object[]
			{
				configuration
			});
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000167BC File Offset: 0x000149BC
		public static LocalizedString TransientMserveCacheError(string error)
		{
			return new LocalizedString("TransientMserveCacheError", NetServerException.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000167E4 File Offset: 0x000149E4
		public static LocalizedString UnexpectedCharSetInAppAuthenticationResponse(string charset)
		{
			return new LocalizedString("UnexpectedCharSetInAppAuthenticationResponse", NetServerException.ResourceManager, new object[]
			{
				charset
			});
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001680C File Offset: 0x00014A0C
		public static LocalizedString TimeoutError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("TimeoutError", NetServerException.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00016840 File Offset: 0x00014A40
		public static LocalizedString ProcessRunnerToStringArgumentOutOfRangeException(string variableName, string errorMessage, string executableName, int exitCode)
		{
			return new LocalizedString("ProcessRunnerToStringArgumentOutOfRangeException", NetServerException.ResourceManager, new object[]
			{
				variableName,
				errorMessage,
				executableName,
				exitCode
			});
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00016879 File Offset: 0x00014A79
		public static LocalizedString GetLocalizedString(NetServerException.IDs key)
		{
			return new LocalizedString(NetServerException.stringIDs[(uint)key], NetServerException.ResourceManager, new object[0]);
		}

		// Token: 0x04000500 RID: 1280
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x04000501 RID: 1281
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.NetServerException", typeof(NetServerException).GetTypeInfo().Assembly);

		// Token: 0x020000F5 RID: 245
		public enum IDs : uint
		{
			// Token: 0x04000503 RID: 1283
			LinkedInRequestCookiesMissingOAuthSecret = 757455353U,
			// Token: 0x04000504 RID: 1284
			InvalidMserveRequest = 1078344448U,
			// Token: 0x04000505 RID: 1285
			EmptyAppAuthenticationResponse = 703035395U,
			// Token: 0x04000506 RID: 1286
			EmptyRedirectUrlAuthenticationResponse = 1478858759U,
			// Token: 0x04000507 RID: 1287
			LinkedInQueryStringMissingOAuthVerifier = 1425604752U,
			// Token: 0x04000508 RID: 1288
			LinkedInQueryStringMissingOAuthToken = 3926725349U
		}

		// Token: 0x020000F6 RID: 246
		private enum ParamIDs
		{
			// Token: 0x0400050A RID: 1290
			InvalidOperationError,
			// Token: 0x0400050B RID: 1291
			LinkedInFailedToAuthenticateApp,
			// Token: 0x0400050C RID: 1292
			AppAuthenticationResponseTooLarge,
			// Token: 0x0400050D RID: 1293
			LinkedInInvalidOAuthResponseMissingToken,
			// Token: 0x0400050E RID: 1294
			InvalidAppAuthenticationResponse,
			// Token: 0x0400050F RID: 1295
			MserveCacheEndpointNotFound,
			// Token: 0x04000510 RID: 1296
			MserveCacheTimeoutError,
			// Token: 0x04000511 RID: 1297
			ExceptionWithStack,
			// Token: 0x04000512 RID: 1298
			NestedExceptionMsg,
			// Token: 0x04000513 RID: 1299
			UnexpectedAppAuthenticationResponse,
			// Token: 0x04000514 RID: 1300
			InvalidDataError,
			// Token: 0x04000515 RID: 1301
			EndpointNotFoundError,
			// Token: 0x04000516 RID: 1302
			QuotaExceededError,
			// Token: 0x04000517 RID: 1303
			LinkedInInvalidOAuthResponseMissingTokenSecret,
			// Token: 0x04000518 RID: 1304
			CommunicationError,
			// Token: 0x04000519 RID: 1305
			PermanentMserveCacheError,
			// Token: 0x0400051A RID: 1306
			LinkedInUnexpectedAppAuthenticationResponse,
			// Token: 0x0400051B RID: 1307
			InvalidAppAuthorizationCode,
			// Token: 0x0400051C RID: 1308
			FailedToAuthenticateApp,
			// Token: 0x0400051D RID: 1309
			TransientMserveCacheError,
			// Token: 0x0400051E RID: 1310
			UnexpectedCharSetInAppAuthenticationResponse,
			// Token: 0x0400051F RID: 1311
			TimeoutError,
			// Token: 0x04000520 RID: 1312
			ProcessRunnerToStringArgumentOutOfRangeException
		}
	}
}
