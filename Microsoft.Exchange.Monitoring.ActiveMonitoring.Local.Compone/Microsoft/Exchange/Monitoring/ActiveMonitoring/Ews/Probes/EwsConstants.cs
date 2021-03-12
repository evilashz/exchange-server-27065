using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x0200016F RID: 367
	public class EwsConstants
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0004362E File Offset: 0x0004182E
		public static string LocalProbeEndpoint
		{
			get
			{
				return Uri.UriSchemeHttps + "://localhost/";
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00043640 File Offset: 0x00041840
		public static string AutodiscoverSvcEndpoint
		{
			get
			{
				return EwsConstants.LocalProbeEndpoint.TrimEnd(new char[]
				{
					'/'
				}) + "/autodiscover/autodiscover.svc";
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00043670 File Offset: 0x00041870
		public static string AutodiscoverXmlEndpoint
		{
			get
			{
				return EwsConstants.LocalProbeEndpoint.TrimEnd(new char[]
				{
					'/'
				}) + "/autodiscover/autodiscover.xml";
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000436A0 File Offset: 0x000418A0
		public static string EwsEndpoint
		{
			get
			{
				return EwsConstants.LocalProbeEndpoint.TrimEnd(new char[]
				{
					'/'
				}) + "/ews/exchange.asmx";
			}
		}

		// Token: 0x040007A8 RID: 1960
		public const int DefaultProbeRecurrenceIntervalSeconds = 300;

		// Token: 0x040007A9 RID: 1961
		public const int DefaultMonitoringIntervalSeconds = 1800;

		// Token: 0x040007AA RID: 1962
		public const int DefaultMonitoringRecurrenceIntervalSeconds = 0;

		// Token: 0x040007AB RID: 1963
		public const int DefaultApiRetryCount = 1;

		// Token: 0x040007AC RID: 1964
		public const int DefaultApiRetrySleep = 5000;

		// Token: 0x040007AD RID: 1965
		public const int MaxSyncItemsCount = 512;

		// Token: 0x040007AE RID: 1966
		public const int MinimumTimeLimit = 5000;

		// Token: 0x040007AF RID: 1967
		public const int DefaultUnhealthyTransitionSpanInMinutes = 20;

		// Token: 0x040007B0 RID: 1968
		public const int DefaultUnrecoverableTransitionSpanInMinutes = 20;

		// Token: 0x040007B1 RID: 1969
		public const int DefaultDegradedTransitionSpanInMinutes = 0;

		// Token: 0x040007B2 RID: 1970
		public const int DefaultIISRecycleRetryCount = 1;

		// Token: 0x040007B3 RID: 1971
		public const int DefaultIISRecycleRetrySpanInSeconds = 30;

		// Token: 0x040007B4 RID: 1972
		public const int DefaultFailedProbeThreshold = 4;

		// Token: 0x040007B5 RID: 1973
		public const int DefaultProbeTimeoutSpanInSeconds = 20;

		// Token: 0x040007B6 RID: 1974
		public const string DefaultOperationName = "ConvertId";

		// Token: 0x040007B7 RID: 1975
		public const string DefaultServerRole = "Mailbox";

		// Token: 0x040007B8 RID: 1976
		public const string DefaultProbeName = "EwsProtocolSelfTest";

		// Token: 0x040007B9 RID: 1977
		public const string DefaultUserAgentPart = "AMProbe";

		// Token: 0x040007BA RID: 1978
		public const string ClientStatisticsHeader = "X-ClientStatistics";

		// Token: 0x040007BB RID: 1979
		public const string GetFolderOperationName = "GetFolder";

		// Token: 0x040007BC RID: 1980
		public const string SendEmailOperationName = "SendEmail";

		// Token: 0x040007BD RID: 1981
		public const string ConvertIdOperationName = "ConvertId";

		// Token: 0x040007BE RID: 1982
		public const string RequestIdHeader = "RequestId";

		// Token: 0x040007BF RID: 1983
		public const string TrustAnySslCertificateAttributeName = "TrustAnySslCertificate";

		// Token: 0x040007C0 RID: 1984
		public const string PrimaryAuthNAttributeName = "PrimaryAuthN";

		// Token: 0x040007C1 RID: 1985
		public const string IsOutsideInMonitoringAttributeName = "IsOutsideInMonitoring";

		// Token: 0x040007C2 RID: 1986
		public const string ExchangeSkuAttributeName = "ExchangeSku";

		// Token: 0x040007C3 RID: 1987
		public const string TargetPortAttributeName = "TargetPort";

		// Token: 0x040007C4 RID: 1988
		public const string VerboseAttributeName = "Verbose";

		// Token: 0x040007C5 RID: 1989
		public const string ApiRetryCountAttributeName = "ApiRetryCount";

		// Token: 0x040007C6 RID: 1990
		public const string ApiRetrySleepInMillisecondsAttributeName = "ApiRetrySleepInMilliseconds";

		// Token: 0x040007C7 RID: 1991
		public const string UseXropEndPointAttributeName = "UseXropEndPoint";

		// Token: 0x040007C8 RID: 1992
		public const string DomainAttributeName = "Domain";

		// Token: 0x040007C9 RID: 1993
		public const string UserAgentPartAttributeName = "UserAgentPart";

		// Token: 0x040007CA RID: 1994
		public const string OperationNameAttributeName = "OperationName";

		// Token: 0x040007CB RID: 1995
		public const string IncludeExchangeRpcUrlAttributeName = "IncludeExchangeRpcUrl";

		// Token: 0x040007CC RID: 1996
		public const string EWSDeepTestProbeName = "EWSDeepTest";

		// Token: 0x040007CD RID: 1997
		public const string EWSSelfTestProbeName = "EWSSelfTest";

		// Token: 0x040007CE RID: 1998
		public const string EWSCtpTestProbeName = "EWSCtpTest";

		// Token: 0x040007CF RID: 1999
		public const string AutodiscoverSelfTestProbeName = "AutodiscoverSelfTest";

		// Token: 0x040007D0 RID: 2000
		public const string AutodiscoverCtpTestProbeName = "AutodiscoverCtpTest";

		// Token: 0x040007D1 RID: 2001
		public const string MSExchangeAutoDiscoverAppPoolName = "MSExchangeAutoDiscoverAppPool";

		// Token: 0x040007D2 RID: 2002
		public const string MSExchangeServicesAppPoolName = "MSExchangeServicesAppPool";

		// Token: 0x040007D3 RID: 2003
		public const string ServerRoleAttributeName = "ServerRole";

		// Token: 0x040007D4 RID: 2004
		public const string ProbeNameAttributeName = "ProbeType";

		// Token: 0x040007D5 RID: 2005
		public const string ProbeRecurrenceSpanAttributeName = "ProbeRecurrenceSpan";

		// Token: 0x040007D6 RID: 2006
		public const string ProbeTimeoutSpanAttributeName = "ProbeTimeoutSpan";

		// Token: 0x040007D7 RID: 2007
		public const string MonitoringIntervalSpanAttributeName = "MonitoringIntervalSpan";

		// Token: 0x040007D8 RID: 2008
		public const string MonitoringRecurrenceIntervalSpanAttributeName = "MonitoringRecurrenceIntervalSpan";

		// Token: 0x040007D9 RID: 2009
		public const string DegradedTransitionSpanAttributeName = "DegradedTransitionSpan";

		// Token: 0x040007DA RID: 2010
		public const string UnhealthyTransitionSpanAttributeName = "UnhealthyTransitionSpan";

		// Token: 0x040007DB RID: 2011
		public const string UnrecoverableTransitionSpanAttributeName = "UnrecoverableTransitionSpan";

		// Token: 0x040007DC RID: 2012
		public const string IISRecycleRetryCountAttributeName = "IISRecycleRetryCount";

		// Token: 0x040007DD RID: 2013
		public const string IISRecycleRetrySpanAttributeName = "IISRecycleRetrySpan";

		// Token: 0x040007DE RID: 2014
		public const string FailedProbeThresholdAttributeName = "FailedProbeThreshold";

		// Token: 0x040007DF RID: 2015
		public const string IISRecycleResponderEnabledAttributeName = "IISRecycleResponderEnabled";

		// Token: 0x040007E0 RID: 2016
		public const string FailoverResponderEnabledAttributeName = "FailoverResponderEnabled";

		// Token: 0x040007E1 RID: 2017
		public const string AlertResponderEnabledAttributeName = "AlertResponderEnabled";

		// Token: 0x040007E2 RID: 2018
		public const string EnablePagedAlertsAttributeName = "EnablePagedAlerts";

		// Token: 0x040007E3 RID: 2019
		public const string CreateRespondersForTestAttributeName = "CreateRespondersForTest";

		// Token: 0x040007E4 RID: 2020
		public const string BaseNameAttributeName = "BaseName";

		// Token: 0x040007E5 RID: 2021
		public const string Name = "Name";

		// Token: 0x040007E6 RID: 2022
		public const string ServiceName = "ServiceName";

		// Token: 0x040007E7 RID: 2023
		public const string TargetResource = "TargetResource";

		// Token: 0x040007E8 RID: 2024
		public const string Account = "Account";

		// Token: 0x040007E9 RID: 2025
		public const string Password = "Password";

		// Token: 0x040007EA RID: 2026
		public const string Endpoint = "Endpoint";

		// Token: 0x040007EB RID: 2027
		public const string TimeoutSeconds = "TimeoutSeconds";
	}
}
