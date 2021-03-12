using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000321 RID: 801
	internal static class GeneralCounters
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x0006AC24 File Offset: 0x00068E24
		public static void GetPerfCounterInfo(XElement element)
		{
			if (GeneralCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in GeneralCounters.AllCounters)
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

		// Token: 0x04000E1E RID: 3614
		public const string CategoryName = "MSExchangeUMGeneral";

		// Token: 0x04000E1F RID: 3615
		private static readonly ExPerformanceCounter TotalCallsPerSecond = new ExPerformanceCounter("MSExchangeUMGeneral", "Total Calls per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E20 RID: 3616
		public static readonly ExPerformanceCounter TotalCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Total Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			GeneralCounters.TotalCallsPerSecond
		});

		// Token: 0x04000E21 RID: 3617
		public static readonly ExPerformanceCounter CallsDisconnectedByUserFailure = new ExPerformanceCounter("MSExchangeUMGeneral", "Calls Disconnected by User Failure", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E22 RID: 3618
		public static readonly ExPerformanceCounter CurrentCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E23 RID: 3619
		public static readonly ExPerformanceCounter CurrentVoicemailCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Voice Mail Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E24 RID: 3620
		public static readonly ExPerformanceCounter CurrentFaxCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Fax Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E25 RID: 3621
		public static readonly ExPerformanceCounter CurrentSubscriberAccessCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Subscriber Access Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E26 RID: 3622
		public static readonly ExPerformanceCounter CurrentAutoAttendantCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Auto Attendant Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E27 RID: 3623
		public static readonly ExPerformanceCounter CurrentPlayOnPhoneCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Play on Phone Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E28 RID: 3624
		public static readonly ExPerformanceCounter CurrentUnauthenticatedPilotNumberCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Unauthenticated Pilot Number Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E29 RID: 3625
		public static readonly ExPerformanceCounter CurrentPromptEditingCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Current Prompt Editing Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2A RID: 3626
		public static readonly ExPerformanceCounter TotalPlayOnPhoneCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Total Play on Phone Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2B RID: 3627
		public static readonly ExPerformanceCounter AverageCallDuration = new ExPerformanceCounter("MSExchangeUMGeneral", "Average Call Duration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2C RID: 3628
		public static readonly ExPerformanceCounter AverageRecentCallDuration = new ExPerformanceCounter("MSExchangeUMGeneral", "Average Recent Call Duration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2D RID: 3629
		public static readonly ExPerformanceCounter UserResponseLatency = new ExPerformanceCounter("MSExchangeUMGeneral", "User Response Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2E RID: 3630
		public static readonly ExPerformanceCounter DelayedCalls = new ExPerformanceCounter("MSExchangeUMGeneral", "Delayed Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E2F RID: 3631
		public static readonly ExPerformanceCounter CallDurationExceeded = new ExPerformanceCounter("MSExchangeUMGeneral", "Call Duration Exceeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E30 RID: 3632
		public static readonly ExPerformanceCounter OCSUserEventNotifications = new ExPerformanceCounter("MSExchangeUMGeneral", "OCS User Event Notifications", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E31 RID: 3633
		public static readonly ExPerformanceCounter AverageMWILatency = new ExPerformanceCounter("MSExchangeUMGeneral", "Average MWI Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E32 RID: 3634
		public static readonly ExPerformanceCounter CallerResolutionsAttempted = new ExPerformanceCounter("MSExchangeUMGeneral", "Caller ID Resolutions Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E33 RID: 3635
		public static readonly ExPerformanceCounter CallerResolutionsSucceeded = new ExPerformanceCounter("MSExchangeUMGeneral", "Caller ID Resolutions Succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E34 RID: 3636
		public static readonly ExPerformanceCounter PercentageSuccessfulCallerResolutions = new ExPerformanceCounter("MSExchangeUMGeneral", "% Successful Caller ID Resolutions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E35 RID: 3637
		public static readonly ExPerformanceCounter PercentageSuccessfulCallerResolutions_Base = new ExPerformanceCounter("MSExchangeUMGeneral", "Base counter for % Successful CallerID resolutions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E36 RID: 3638
		public static readonly ExPerformanceCounter ExtensionCallerResolutionsAttempted = new ExPerformanceCounter("MSExchangeUMGeneral", "Extension Caller ID Resolutions Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E37 RID: 3639
		public static readonly ExPerformanceCounter ExtensionCallerResolutionsSucceeded = new ExPerformanceCounter("MSExchangeUMGeneral", "Extension Caller ID Resolutions Succeeded", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E38 RID: 3640
		public static readonly ExPerformanceCounter PercentageSuccessfulExtensionCallerResolutions = new ExPerformanceCounter("MSExchangeUMGeneral", "% Successful Extension Caller ID Resolutions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E39 RID: 3641
		public static readonly ExPerformanceCounter PercentageSuccessfulExtensionCallerResolutions_Base = new ExPerformanceCounter("MSExchangeUMGeneral", "Base counter for % Successful Extension CallerID resolutions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E3A RID: 3642
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			GeneralCounters.TotalCalls,
			GeneralCounters.CallsDisconnectedByUserFailure,
			GeneralCounters.CurrentCalls,
			GeneralCounters.CurrentVoicemailCalls,
			GeneralCounters.CurrentFaxCalls,
			GeneralCounters.CurrentSubscriberAccessCalls,
			GeneralCounters.CurrentAutoAttendantCalls,
			GeneralCounters.CurrentPlayOnPhoneCalls,
			GeneralCounters.CurrentUnauthenticatedPilotNumberCalls,
			GeneralCounters.CurrentPromptEditingCalls,
			GeneralCounters.TotalPlayOnPhoneCalls,
			GeneralCounters.AverageCallDuration,
			GeneralCounters.AverageRecentCallDuration,
			GeneralCounters.UserResponseLatency,
			GeneralCounters.DelayedCalls,
			GeneralCounters.CallDurationExceeded,
			GeneralCounters.OCSUserEventNotifications,
			GeneralCounters.AverageMWILatency,
			GeneralCounters.CallerResolutionsAttempted,
			GeneralCounters.CallerResolutionsSucceeded,
			GeneralCounters.PercentageSuccessfulCallerResolutions,
			GeneralCounters.PercentageSuccessfulCallerResolutions_Base,
			GeneralCounters.ExtensionCallerResolutionsAttempted,
			GeneralCounters.ExtensionCallerResolutionsSucceeded,
			GeneralCounters.PercentageSuccessfulExtensionCallerResolutions,
			GeneralCounters.PercentageSuccessfulExtensionCallerResolutions_Base
		};
	}
}
