using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000109 RID: 265
	internal static class MonitoringWebClientStrings
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x000173B8 File Offset: 0x000155B8
		static MonitoringWebClientStrings()
		{
			MonitoringWebClientStrings.stringIDs.Add(3908151889U, "ScenarioCanceled");
			MonitoringWebClientStrings.stringIDs.Add(644136196U, "UnexpectedResponseCodeReceived");
			MonitoringWebClientStrings.stringIDs.Add(2108302982U, "MissingLiveIdAuthCookies");
			MonitoringWebClientStrings.stringIDs.Add(1647397280U, "HealthCheckRequestFailed");
			MonitoringWebClientStrings.stringIDs.Add(3546529694U, "BrickAuthenticationMissingOkOrLanguageSelection");
			MonitoringWebClientStrings.stringIDs.Add(3494523549U, "EcpErrorPage");
			MonitoringWebClientStrings.stringIDs.Add(3040841166U, "MissingFormAction");
			MonitoringWebClientStrings.stringIDs.Add(1698098118U, "MissingFbaAuthCookies");
			MonitoringWebClientStrings.stringIDs.Add(3677395520U, "OwaErrorPage");
			MonitoringWebClientStrings.stringIDs.Add(1362173822U, "NoRedirectToEcpAfterLanguageSelection");
			MonitoringWebClientStrings.stringIDs.Add(1527735966U, "MissingUserContextCookie");
			MonitoringWebClientStrings.stringIDs.Add(3209757262U, "PassiveDatabase");
			MonitoringWebClientStrings.stringIDs.Add(3315439992U, "CafeErrorPage");
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000174F8 File Offset: 0x000156F8
		public static string ScenarioCanceled
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("ScenarioCanceled");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00017509 File Offset: 0x00015709
		public static string UnexpectedResponseCodeReceived
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("UnexpectedResponseCodeReceived");
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001751A File Offset: 0x0001571A
		public static string ScenarioExceptionInnerException(string message)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("ScenarioExceptionInnerException"), message);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00017531 File Offset: 0x00015731
		public static string MissingLiveIdAuthCookies
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("MissingLiveIdAuthCookies");
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00017542 File Offset: 0x00015742
		public static string NoResponseFromServerThroughPublicName(string serverName, int testCount, Uri publicName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("NoResponseFromServerThroughPublicName"), serverName, testCount, publicName);
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00017560 File Offset: 0x00015760
		public static string HealthCheckRequestFailed
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("HealthCheckRequestFailed");
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00017571 File Offset: 0x00015771
		public static string MissingOwaFbaRedirectPage(string missingKeyword)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingOwaFbaRedirectPage"), missingKeyword);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017588 File Offset: 0x00015788
		public static string MissingStaticFile(string missingKeywords)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingStaticFile"), missingKeywords);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001759F File Offset: 0x0001579F
		public static string ScenarioTimedOut(double maxTime, double totalTime)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("ScenarioTimedOut"), maxTime, totalTime);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000175C1 File Offset: 0x000157C1
		public static string MissingPreloadPage(string marker)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingPreloadPage"), marker);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000175D8 File Offset: 0x000157D8
		public static string MissingUserNameFieldFromAdfsResponse(string marker)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingUserNameFieldFromAdfsResponse"), marker);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000175EF File Offset: 0x000157EF
		public static string MissingOwaFbaPage(string missingKeyword)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingOwaFbaPage"), missingKeyword);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00017606 File Offset: 0x00015806
		public static string ActualStatusCode(string statusCodes)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("ActualStatusCode"), statusCodes);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001761D File Offset: 0x0001581D
		public static string MissingEcpStartPage(string missingKeywords)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingEcpStartPage"), missingKeywords);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00017634 File Offset: 0x00015834
		public static string MissingJavascriptEmptyBody(string variableName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingJavascriptEmptyBody"), variableName);
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001764B File Offset: 0x0001584B
		public static string BrickAuthenticationMissingOkOrLanguageSelection
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("BrickAuthenticationMissingOkOrLanguageSelection");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001765C File Offset: 0x0001585C
		public static string EcpErrorPage
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("EcpErrorPage");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001766D File Offset: 0x0001586D
		public static string MissingFormAction
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("MissingFormAction");
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001767E File Offset: 0x0001587E
		public static string MissingJsonVariable(string variableName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingJsonVariable"), variableName);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00017695 File Offset: 0x00015895
		public static string MissingOwaStartPage(string missingKeywords)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingOwaStartPage"), missingKeywords);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000176AC File Offset: 0x000158AC
		public static string MissingFbaAuthCookies
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("MissingFbaAuthCookies");
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000176BD File Offset: 0x000158BD
		public static string BadPreloadPath(string variableName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("BadPreloadPath"), variableName);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000176D4 File Offset: 0x000158D4
		public static string MissingEcpHelpDeskStartPage(string missingKeyword)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingEcpHelpDeskStartPage"), missingKeyword);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000176EB File Offset: 0x000158EB
		public static string LogonError(string errorMessage)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("LogonError"), errorMessage);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00017702 File Offset: 0x00015902
		public static string MissingLiveIdCompactToken(string redirectionLocation)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingLiveIdCompactToken"), redirectionLocation);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00017719 File Offset: 0x00015919
		public static string OwaErrorPage
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("OwaErrorPage");
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001772A File Offset: 0x0001592A
		public static string ExpectedStatusCode(string statusCodes)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("ExpectedStatusCode"), statusCodes);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00017741 File Offset: 0x00015941
		public static string NoRedirectToEcpAfterLanguageSelection
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("NoRedirectToEcpAfterLanguageSelection");
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00017752 File Offset: 0x00015952
		public static string MissingJavascriptVariable(string variableName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingJavascriptVariable"), variableName);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017769 File Offset: 0x00015969
		public static string NameResolutionFailure(string hostName)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("NameResolutionFailure"), hostName);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00017780 File Offset: 0x00015980
		public static string MissingUserContextCookie
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("MissingUserContextCookie");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00017791 File Offset: 0x00015991
		public static string PassiveDatabase
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("PassiveDatabase");
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000177A2 File Offset: 0x000159A2
		public static string MissingPasswordFieldFromAdfsResponse(string marker)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("MissingPasswordFieldFromAdfsResponse"), marker);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x000177B9 File Offset: 0x000159B9
		public static string CafeErrorPage
		{
			get
			{
				return MonitoringWebClientStrings.ResourceManager.GetString("CafeErrorPage");
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000177CC File Offset: 0x000159CC
		public static string ScenarioExceptionMessageHeader(string exceptionType, string baseMessage, string failureSource, string failureReason, string component, string hint)
		{
			return string.Format(MonitoringWebClientStrings.ResourceManager.GetString("ScenarioExceptionMessageHeader"), new object[]
			{
				exceptionType,
				baseMessage,
				failureSource,
				failureReason,
				component,
				hint
			});
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001780F File Offset: 0x00015A0F
		public static string GetLocalizedString(MonitoringWebClientStrings.IDs key)
		{
			return MonitoringWebClientStrings.ResourceManager.GetString(MonitoringWebClientStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000551 RID: 1361
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(13);

		// Token: 0x04000552 RID: 1362
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Net.MonitoringWebClientStrings", typeof(MonitoringWebClientStrings).GetTypeInfo().Assembly);

		// Token: 0x0200010A RID: 266
		public enum IDs : uint
		{
			// Token: 0x04000554 RID: 1364
			ScenarioCanceled = 3908151889U,
			// Token: 0x04000555 RID: 1365
			UnexpectedResponseCodeReceived = 644136196U,
			// Token: 0x04000556 RID: 1366
			MissingLiveIdAuthCookies = 2108302982U,
			// Token: 0x04000557 RID: 1367
			HealthCheckRequestFailed = 1647397280U,
			// Token: 0x04000558 RID: 1368
			BrickAuthenticationMissingOkOrLanguageSelection = 3546529694U,
			// Token: 0x04000559 RID: 1369
			EcpErrorPage = 3494523549U,
			// Token: 0x0400055A RID: 1370
			MissingFormAction = 3040841166U,
			// Token: 0x0400055B RID: 1371
			MissingFbaAuthCookies = 1698098118U,
			// Token: 0x0400055C RID: 1372
			OwaErrorPage = 3677395520U,
			// Token: 0x0400055D RID: 1373
			NoRedirectToEcpAfterLanguageSelection = 1362173822U,
			// Token: 0x0400055E RID: 1374
			MissingUserContextCookie = 1527735966U,
			// Token: 0x0400055F RID: 1375
			PassiveDatabase = 3209757262U,
			// Token: 0x04000560 RID: 1376
			CafeErrorPage = 3315439992U
		}

		// Token: 0x0200010B RID: 267
		private enum ParamIDs
		{
			// Token: 0x04000562 RID: 1378
			ScenarioExceptionInnerException,
			// Token: 0x04000563 RID: 1379
			NoResponseFromServerThroughPublicName,
			// Token: 0x04000564 RID: 1380
			MissingOwaFbaRedirectPage,
			// Token: 0x04000565 RID: 1381
			MissingStaticFile,
			// Token: 0x04000566 RID: 1382
			ScenarioTimedOut,
			// Token: 0x04000567 RID: 1383
			MissingPreloadPage,
			// Token: 0x04000568 RID: 1384
			MissingUserNameFieldFromAdfsResponse,
			// Token: 0x04000569 RID: 1385
			MissingOwaFbaPage,
			// Token: 0x0400056A RID: 1386
			ActualStatusCode,
			// Token: 0x0400056B RID: 1387
			MissingEcpStartPage,
			// Token: 0x0400056C RID: 1388
			MissingJavascriptEmptyBody,
			// Token: 0x0400056D RID: 1389
			MissingJsonVariable,
			// Token: 0x0400056E RID: 1390
			MissingOwaStartPage,
			// Token: 0x0400056F RID: 1391
			BadPreloadPath,
			// Token: 0x04000570 RID: 1392
			MissingEcpHelpDeskStartPage,
			// Token: 0x04000571 RID: 1393
			LogonError,
			// Token: 0x04000572 RID: 1394
			MissingLiveIdCompactToken,
			// Token: 0x04000573 RID: 1395
			ExpectedStatusCode,
			// Token: 0x04000574 RID: 1396
			MissingJavascriptVariable,
			// Token: 0x04000575 RID: 1397
			NameResolutionFailure,
			// Token: 0x04000576 RID: 1398
			MissingPasswordFieldFromAdfsResponse,
			// Token: 0x04000577 RID: 1399
			ScenarioExceptionMessageHeader
		}
	}
}
