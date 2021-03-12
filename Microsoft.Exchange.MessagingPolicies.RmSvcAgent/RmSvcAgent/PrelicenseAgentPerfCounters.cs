using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000A RID: 10
	internal static class PrelicenseAgentPerfCounters
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000312C File Offset: 0x0000132C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PrelicenseAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PrelicenseAgentPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x0400002C RID: 44
		public const string CategoryName = "MSExchange Prelicensing Agent";

		// Token: 0x0400002D RID: 45
		private static readonly ExPerformanceCounter RateOfMessagesPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Messages Prelicensed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002E RID: 46
		public static readonly ExPerformanceCounter TotalMessagesPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Messages Prelicensed", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfMessagesPreLicensed
		});

		// Token: 0x0400002F RID: 47
		private static readonly ExPerformanceCounter RateOfMessagesFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Messages Failed To Prelicense/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000030 RID: 48
		public static readonly ExPerformanceCounter TotalMessagesFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Messages Failed To Prelicense", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfMessagesFailedToPreLicense
		});

		// Token: 0x04000031 RID: 49
		private static readonly ExPerformanceCounter RateOfDeferralsToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Rate of Deferrals to Prelicense/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000032 RID: 50
		public static readonly ExPerformanceCounter TotalDeferralsToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Number of Deferrals when Prelicensing Messages", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfDeferralsToPreLicense
		});

		// Token: 0x04000033 RID: 51
		private static readonly ExPerformanceCounter RateOfRecipientsPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Recipients Prelicensed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000034 RID: 52
		public static readonly ExPerformanceCounter TotalRecipientsPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Recipients Prelicensed", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfRecipientsPreLicensed
		});

		// Token: 0x04000035 RID: 53
		private static readonly ExPerformanceCounter RateOfRecipientsFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Recipients Failed To Prelicense/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000036 RID: 54
		public static readonly ExPerformanceCounter TotalRecipientsFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Recipients Failed To Prelicense", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfRecipientsFailedToPreLicense
		});

		// Token: 0x04000037 RID: 55
		public static readonly ExPerformanceCounter Percentile95FailedToLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Over 5% of messages failed prelicensing or server licensing in last 30 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000038 RID: 56
		private static readonly ExPerformanceCounter RateOfMessagesLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Messages Licensed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000039 RID: 57
		public static readonly ExPerformanceCounter TotalMessagesLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Messages Licensed", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfMessagesLicensed
		});

		// Token: 0x0400003A RID: 58
		private static readonly ExPerformanceCounter RateOfMessagesFailedToLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Messages Failed To License/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003B RID: 59
		public static readonly ExPerformanceCounter TotalMessagesFailedToLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Messages Failed to License", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfMessagesFailedToLicense
		});

		// Token: 0x0400003C RID: 60
		private static readonly ExPerformanceCounter RateOfDeferralsToLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Deferrals Of Messages To License/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003D RID: 61
		public static readonly ExPerformanceCounter TotalDeferralsToLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Deferrals Of Messages To License", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfDeferralsToLicense
		});

		// Token: 0x0400003E RID: 62
		private static readonly ExPerformanceCounter RateOfExternalMessagesPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "External IRM-Protected Messages Prelicensed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003F RID: 63
		public static readonly ExPerformanceCounter TotalExternalMessagesPreLicensed = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total External Messages prelicensed", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfExternalMessagesPreLicensed
		});

		// Token: 0x04000040 RID: 64
		private static readonly ExPerformanceCounter RateOfExternalMessagesFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "External Messages Failed To Prelicense/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000041 RID: 65
		public static readonly ExPerformanceCounter TotalExternalMessagesFailedToPreLicense = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total External Messages Failed To Prelicense", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfExternalMessagesFailedToPreLicense
		});

		// Token: 0x04000042 RID: 66
		private static readonly ExPerformanceCounter RateOfDeferralsToPreLicenseForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Rate of Deferrals to Prelicense External Messages/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000043 RID: 67
		public static readonly ExPerformanceCounter TotalDeferralsToPreLicenseForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Deferrals To Prelicense For External Messages", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfDeferralsToPreLicenseForExternalMessages
		});

		// Token: 0x04000044 RID: 68
		private static readonly ExPerformanceCounter RateOfRecipientsPreLicensedForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Recipients Prelicensed For External Messages/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000045 RID: 69
		public static readonly ExPerformanceCounter TotalRecipientsPreLicensedForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Recipients Prelicensed for Messages Protected by External AD RMS Server", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfRecipientsPreLicensedForExternalMessages
		});

		// Token: 0x04000046 RID: 70
		private static readonly ExPerformanceCounter RateOfRecipientsFailedToPreLicenseForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Recipients Failed To Prelicense/sec For External Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000047 RID: 71
		public static readonly ExPerformanceCounter TotalRecipientsFailedToPreLicenseForExternalMessages = new ExPerformanceCounter("MSExchange Prelicensing Agent", "Total Recipients Failed To Prelicense For External Messages", string.Empty, null, new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.RateOfRecipientsFailedToPreLicenseForExternalMessages
		});

		// Token: 0x04000048 RID: 72
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PrelicenseAgentPerfCounters.TotalMessagesPreLicensed,
			PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense,
			PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense,
			PrelicenseAgentPerfCounters.TotalRecipientsPreLicensed,
			PrelicenseAgentPerfCounters.TotalRecipientsFailedToPreLicense,
			PrelicenseAgentPerfCounters.Percentile95FailedToLicense,
			PrelicenseAgentPerfCounters.TotalMessagesLicensed,
			PrelicenseAgentPerfCounters.TotalMessagesFailedToLicense,
			PrelicenseAgentPerfCounters.TotalDeferralsToLicense,
			PrelicenseAgentPerfCounters.TotalExternalMessagesPreLicensed,
			PrelicenseAgentPerfCounters.TotalExternalMessagesFailedToPreLicense,
			PrelicenseAgentPerfCounters.TotalDeferralsToPreLicenseForExternalMessages,
			PrelicenseAgentPerfCounters.TotalRecipientsPreLicensedForExternalMessages,
			PrelicenseAgentPerfCounters.TotalRecipientsFailedToPreLicenseForExternalMessages
		};
	}
}
