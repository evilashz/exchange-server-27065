using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F0 RID: 240
	internal static class Configuration
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001C39C File Offset: 0x0001A59C
		public static int MaximumRequestStreamSize
		{
			get
			{
				if (Configuration.maximumRequestStreamSize == -1)
				{
					Configuration.maximumRequestStreamSize = Configuration.ReadIntValue("MaximumRequestStreamSize", 409600);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using MaximumRequestStreamSize = {0}", Configuration.maximumRequestStreamSize);
				}
				return Configuration.maximumRequestStreamSize;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001C3D5 File Offset: 0x0001A5D5
		public static int MaximumQueryIntervalDays
		{
			get
			{
				if (Configuration.maximumQueryIntervalDays == -1)
				{
					Configuration.maximumQueryIntervalDays = Configuration.ReadIntValue("MaximumQueryIntervalDays", 62);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using MaximumQueryIntervalDays = {0}", Configuration.maximumQueryIntervalDays);
				}
				return Configuration.maximumQueryIntervalDays;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001C40B File Offset: 0x0001A60B
		public static int MaximumIdentityArraySize
		{
			get
			{
				if (Configuration.maximumIdentityArraySize == -1)
				{
					Configuration.maximumIdentityArraySize = Configuration.ReadIntValue("MaximumIdentityArraySize", 100);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using MaximumIdentityArraySize = {0}", Configuration.maximumIdentityArraySize);
				}
				return Configuration.maximumIdentityArraySize;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001C441 File Offset: 0x0001A641
		public static int MaximumResultSetSize
		{
			get
			{
				if (Configuration.maximumResultSetSize == -1)
				{
					Configuration.maximumResultSetSize = Configuration.ReadIntValue("MaximumResultSetSize", 1000);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using MaximumResultSetSize = {0}", Configuration.maximumResultSetSize);
				}
				return Configuration.maximumResultSetSize;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001C47A File Offset: 0x0001A67A
		public static int ConnectionPoolSize
		{
			get
			{
				if (Configuration.connectionPoolSize == -1)
				{
					Configuration.connectionPoolSize = Configuration.ReadIntValue("ConnectionPoolSize", 255);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using ConnectionPoolSize = {0}", Configuration.connectionPoolSize);
				}
				return Configuration.connectionPoolSize;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001C4B3 File Offset: 0x0001A6B3
		public static TimeSpan WebRequestTimeoutInSeconds
		{
			get
			{
				if (Configuration.webRequestTimeoutInSeconds == -1)
				{
					Configuration.webRequestTimeoutInSeconds = Configuration.ReadIntValue("WebRequestTimeoutInSeconds", 25);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using WebRequestTimeoutInSeconds = {0}", Configuration.webRequestTimeoutInSeconds);
				}
				return TimeSpan.FromSeconds((double)Configuration.webRequestTimeoutInSeconds);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001C4EF File Offset: 0x0001A6EF
		public static int MaximumGroupMemberCount
		{
			get
			{
				if (Configuration.maximumGroupMemberCount == -1)
				{
					Configuration.maximumGroupMemberCount = Configuration.ReadIntValue("MaximumGroupMemberCount", 20);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using MaximumGroupMemberCount = {0}", Configuration.maximumGroupMemberCount);
				}
				return Configuration.maximumGroupMemberCount;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001C525 File Offset: 0x0001A725
		public static bool UseSSLForCrossSiteRequests
		{
			get
			{
				if (Configuration.useSSLForCrossSiteRequests == -1)
				{
					Configuration.useSSLForCrossSiteRequests = Configuration.ReadIntValue("UseSSLForCrossSiteRequests", 1);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using UseSSLForCrossSiteRequests = {0}", Configuration.useSSLForCrossSiteRequests);
				}
				return Configuration.useSSLForCrossSiteRequests > 0;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001C560 File Offset: 0x0001A760
		public static bool UseSSLForAutoDiscoverRequests
		{
			get
			{
				if (Configuration.useSSLForAutoDiscoverRequests == -1)
				{
					Configuration.useSSLForAutoDiscoverRequests = Configuration.ReadIntValue("UseSSLForAutoDiscoverRequests", 1);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using UseSSLForAutoDiscoverRequests = {0}", Configuration.useSSLForAutoDiscoverRequests);
				}
				return Configuration.useSSLForAutoDiscoverRequests > 0;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001C59B File Offset: 0x0001A79B
		public static bool DisableGzipForProxyRequests
		{
			get
			{
				if (Configuration.disableGzipForProxyRequests == -1)
				{
					Configuration.disableGzipForProxyRequests = Configuration.ReadIntValue("DisableGzipForProxyRequests", 0);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using DisableGzipForProxyRequests = {0}", Configuration.disableGzipForProxyRequests);
				}
				return Configuration.disableGzipForProxyRequests > 0;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
		public static int ADRefreshIntervalInMinutes
		{
			get
			{
				if (Configuration.adRefreshIntervalInMinutes == -1)
				{
					Configuration.adRefreshIntervalInMinutes = Configuration.ReadIntValue("ADRefreshIntervalInMinutes", 60);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using ADRefreshIntervalInMinutes = {0}", Configuration.adRefreshIntervalInMinutes);
				}
				return Configuration.adRefreshIntervalInMinutes;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001C60C File Offset: 0x0001A80C
		public static TimeSpan IntraSiteTimeout
		{
			get
			{
				if (Configuration.intraSiteTimeoutInSeconds == -1)
				{
					Configuration.intraSiteTimeoutInSeconds = Configuration.ReadIntValue("IntraSiteTimeoutInSeconds", 50);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using IntraSiteTimeoutInSeconds = {0}", Configuration.intraSiteTimeoutInSeconds);
				}
				return TimeSpan.FromSeconds((double)Configuration.intraSiteTimeoutInSeconds);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001C648 File Offset: 0x0001A848
		public static bool BypassProxyForCrossSiteRequests
		{
			get
			{
				if (Configuration.bypassProxyForCrossSiteRequests == -1)
				{
					Configuration.bypassProxyForCrossSiteRequests = Configuration.ReadIntValue("BypassProxyForCrossSiteRequests", 1);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using BypassProxyForCrossSiteRequests = {0}", Configuration.bypassProxyForCrossSiteRequests);
				}
				return Configuration.bypassProxyForCrossSiteRequests > 0;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001C683 File Offset: 0x0001A883
		public static bool BypassProxyForCrossForestRequests
		{
			get
			{
				if (Configuration.bypassProxyForCrossForestRequests == -1)
				{
					Configuration.bypassProxyForCrossForestRequests = Configuration.ReadIntValue("BypassProxyForCrossForestRequests", 0);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using BypassProxyForCrossForestRequests = {0}", Configuration.bypassProxyForCrossForestRequests);
				}
				return Configuration.bypassProxyForCrossForestRequests > 0;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001C6BE File Offset: 0x0001A8BE
		public static TimeSpan RecipientResolutionTimeout
		{
			get
			{
				if (Configuration.recipientResolutionTimeoutInSeconds == -1)
				{
					Configuration.recipientResolutionTimeoutInSeconds = Configuration.ReadIntValue("RecipientResolutionTimeoutInSeconds", 10);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using RecipientResolutionTimeoutInSeconds = {0}", Configuration.recipientResolutionTimeoutInSeconds);
				}
				return TimeSpan.FromSeconds((double)Configuration.recipientResolutionTimeoutInSeconds);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		public static int MaximumDatabasesInQuery
		{
			get
			{
				if (Configuration.maximumDatabasesInQuery == -1)
				{
					Configuration.maximumDatabasesInQuery = Configuration.ReadIntValue("MaximumDatabasesInQuery", 100);
					Configuration.ConfigurationTracer.TraceDebug<string, int>(0L, "Using {0} = {1}", "MaximumDatabasesInQuery", Configuration.maximumDatabasesInQuery);
					if (Configuration.maximumDatabasesInQuery < 1)
					{
						Configuration.ConfigurationTracer.TraceError(0L, "The {0} setting in the configuration file has been assigned the value {1}, which is lower than the minimum supported value {2}. The Availability service is resetting the {0} value to {2}. The maximum {0} value supported is {3}", new object[]
						{
							"MaximumDatabasesInQuery",
							Configuration.maximumDatabasesInQuery,
							1,
							1000
						});
						Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidMinimumDatabasesInQuery, "MaximumDatabasesInQuery", new object[]
						{
							Globals.ProcessId,
							Configuration.maximumDatabasesInQuery,
							1
						});
						Configuration.maximumDatabasesInQuery = 1;
					}
					else if (Configuration.maximumDatabasesInQuery > 1000)
					{
						Configuration.ConfigurationTracer.TraceError(0L, "The {0} setting in the configuration file has been assigned the value {1}, which exceeds the maximum supported value {2}. The Availability service is resetting the {0} value to {2}. The minimum {0} value supported is {3}", new object[]
						{
							"MaximumDatabasesInQuery",
							Configuration.maximumDatabasesInQuery,
							1000,
							1
						});
						Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidMaximumDatabasesInQuery, "MaximumDatabasesInQuery", new object[]
						{
							Globals.ProcessId,
							Configuration.maximumDatabasesInQuery,
							1000
						});
						Configuration.maximumDatabasesInQuery = 1000;
					}
				}
				return Configuration.maximumDatabasesInQuery;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001C876 File Offset: 0x0001AA76
		public static bool UseDisabledAccount
		{
			get
			{
				if (Configuration.useDisabledAccount == -1)
				{
					Configuration.useDisabledAccount = Configuration.ReadIntValue("UseDisabledAccount", -1);
					Configuration.ConfigurationTracer.TraceDebug<int>(0L, "Using UseDisabledAccount = {0}", Configuration.useDisabledAccount);
				}
				return Configuration.useDisabledAccount > 0;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		private static int ReadIntValue(string name, int defaultValue)
		{
			int result;
			if (!int.TryParse(Configuration.parameterCollection[name], out result))
			{
				Configuration.ConfigurationTracer.TraceError<string>(0L, "Error while parsing configuration value {0}. Default value is being used.", name);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x040003A8 RID: 936
		internal const int MinFreeBusyMergeInterval = 5;

		// Token: 0x040003A9 RID: 937
		internal const int MaxFreeBusyMergeInterval = 1440;

		// Token: 0x040003AA RID: 938
		internal const int MaximumRequestStreamSizeDefault = 409600;

		// Token: 0x040003AB RID: 939
		internal const int MaximumQueryIntervalDaysDefault = 62;

		// Token: 0x040003AC RID: 940
		internal const int MaximumIdentityArraySizeDefault = 100;

		// Token: 0x040003AD RID: 941
		internal const int MaximumResultSetSizeDefault = 1000;

		// Token: 0x040003AE RID: 942
		internal const int MapiConnectionPoolSizeDefault = 255;

		// Token: 0x040003AF RID: 943
		internal const int WebRequestTimeoutInSecondsDefault = 25;

		// Token: 0x040003B0 RID: 944
		internal const int MaximumGroupMemberCountDefault = 20;

		// Token: 0x040003B1 RID: 945
		internal const int UseSSLForCrossSiteRequestsDefault = 1;

		// Token: 0x040003B2 RID: 946
		internal const int UseSSLForAutoDiscoverRequestsDefault = 1;

		// Token: 0x040003B3 RID: 947
		internal const int BypassProxyForCrossSiteRequestsDefault = 1;

		// Token: 0x040003B4 RID: 948
		internal const int BypassProxyForCrossForestRequestsDefault = 0;

		// Token: 0x040003B5 RID: 949
		internal const int DisableGzipForProxyRequestsDefault = 0;

		// Token: 0x040003B6 RID: 950
		internal const int ADRefreshIntervalInMinutesDefault = 60;

		// Token: 0x040003B7 RID: 951
		internal const int IntraSiteTimeoutInSecondsDefault = 50;

		// Token: 0x040003B8 RID: 952
		internal const int RecipientResolutionTimeoutInSecondsDefault = 10;

		// Token: 0x040003B9 RID: 953
		internal const int MaximumDatabasesInQueryDefault = 100;

		// Token: 0x040003BA RID: 954
		internal const int MaximumDatabasesInQueryAllowed = 1000;

		// Token: 0x040003BB RID: 955
		internal const int MinimumDatabasesInQueryAllowed = 1;

		// Token: 0x040003BC RID: 956
		internal const string MaximumDatabasesInQueryKey = "MaximumDatabasesInQuery";

		// Token: 0x040003BD RID: 957
		public static StringAppSettingsEntry DnsServerAddress = new StringAppSettingsEntry("DnsIpAddress", null, Configuration.ConfigurationTracer);

		// Token: 0x040003BE RID: 958
		public static BoolAppSettingsEntry BypassDnsCache = new BoolAppSettingsEntry("BypassDnsCache", false, Configuration.ConfigurationTracer);

		// Token: 0x040003BF RID: 959
		public static TimeSpanAppSettingsEntry RemoteUriInvalidCacheDurationInSeconds = new TimeSpanAppSettingsEntry("RemoteUriInvalidCacheDurationInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromHours(1.0), Configuration.ConfigurationTracer);

		// Token: 0x040003C0 RID: 960
		public static TimeSpanAppSettingsEntry AutodiscoverSrvRecordLookupTimeoutInSeconds = new TimeSpanAppSettingsEntry("AutodiscoverSrvRecordLookupTimeoutInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(5.0), Configuration.ConfigurationTracer);

		// Token: 0x040003C1 RID: 961
		public static BoolAppSettingsEntry UnsafeAuthenticatedConnectionSharing = new BoolAppSettingsEntry("UnsafeAuthenticatedConnectionSharing", true, Configuration.ConfigurationTracer);

		// Token: 0x040003C2 RID: 962
		private static NameValueCollection parameterCollection = ConfigurationManager.AppSettings;

		// Token: 0x040003C3 RID: 963
		private static int maximumRequestStreamSize = -1;

		// Token: 0x040003C4 RID: 964
		private static int maximumQueryIntervalDays = -1;

		// Token: 0x040003C5 RID: 965
		private static int maximumIdentityArraySize = -1;

		// Token: 0x040003C6 RID: 966
		private static int maximumResultSetSize = -1;

		// Token: 0x040003C7 RID: 967
		private static int connectionPoolSize = -1;

		// Token: 0x040003C8 RID: 968
		private static int webRequestTimeoutInSeconds = -1;

		// Token: 0x040003C9 RID: 969
		private static int maximumGroupMemberCount = -1;

		// Token: 0x040003CA RID: 970
		private static int useSSLForCrossSiteRequests = -1;

		// Token: 0x040003CB RID: 971
		private static int useSSLForAutoDiscoverRequests = -1;

		// Token: 0x040003CC RID: 972
		private static int bypassProxyForCrossSiteRequests = -1;

		// Token: 0x040003CD RID: 973
		private static int bypassProxyForCrossForestRequests = -1;

		// Token: 0x040003CE RID: 974
		private static int adRefreshIntervalInMinutes = -1;

		// Token: 0x040003CF RID: 975
		private static int intraSiteTimeoutInSeconds = -1;

		// Token: 0x040003D0 RID: 976
		private static int disableGzipForProxyRequests = -1;

		// Token: 0x040003D1 RID: 977
		private static int recipientResolutionTimeoutInSeconds = -1;

		// Token: 0x040003D2 RID: 978
		private static int maximumDatabasesInQuery = -1;

		// Token: 0x040003D3 RID: 979
		private static int useDisabledAccount = -1;

		// Token: 0x040003D4 RID: 980
		private static readonly Trace ConfigurationTracer = ExTraceGlobals.ConfigurationTracer;
	}
}
