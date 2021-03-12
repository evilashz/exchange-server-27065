using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000550 RID: 1360
	internal sealed class PushNotificationCannedSet
	{
		// Token: 0x04002962 RID: 10594
		public static readonly Dictionary<PushNotificationSetupEnvironment, PushNotificationSetupConfig> PushNotificationSetupEnvironmentConfig = new Dictionary<PushNotificationSetupEnvironment, PushNotificationSetupConfig>
		{
			{
				PushNotificationSetupEnvironment.None,
				new PushNotificationSetupConfig(new PushNotificationApp[0], new PushNotificationApp[0], new string[0], new string[0], string.Empty, new Dictionary<string, string>())
			},
			{
				PushNotificationSetupEnvironment.OnPrem,
				new PushNotificationSetupConfig(new PushNotificationApp[]
				{
					PushNotificationCannedApp.OnPremProxy
				}, new PushNotificationApp[0], new string[0], new string[0], "exo", new Dictionary<string, string>())
			},
			{
				PushNotificationSetupEnvironment.SDF,
				new PushNotificationSetupConfig(new PushNotificationApp[]
				{
					PushNotificationCannedApp.ApnsMowaOfficialIPhone,
					PushNotificationCannedApp.ApnsMowaOfficialIPad,
					PushNotificationCannedApp.AzureDeviceRegistration,
					PushNotificationCannedApp.AzureGcmMowaOfficial,
					PushNotificationCannedApp.AzureHubCreation,
					PushNotificationCannedApp.AzureMowaDogfood,
					PushNotificationCannedApp.AzureOutlookWindowsImmersive,
					PushNotificationCannedApp.WnsOutlookMailOfficialWindowsImmersive,
					PushNotificationCannedApp.WnsOutlookCalendarOfficialWindowsImmersive
				}, new PushNotificationApp[]
				{
					PushNotificationCannedApp.ApnsMowaDogfood,
					PushNotificationCannedApp.ApnsMowaOfficialIPhone,
					PushNotificationCannedApp.ApnsMowaOfficialIPad,
					PushNotificationCannedApp.GcmMowaOfficial
				}, new string[0], new string[0], "exo", new Dictionary<string, string>
				{
					{
						PushNotificationCannedApp.AzureMowaDogfood.Name,
						"namsdf01"
					},
					{
						PushNotificationCannedApp.AzureOutlookWindowsImmersive.Name,
						"namsdf01"
					},
					{
						PushNotificationCannedApp.GcmMowaOfficial.Name,
						"namsdf01"
					}
				})
			},
			{
				PushNotificationSetupEnvironment.PROD,
				new PushNotificationSetupConfig(new PushNotificationApp[]
				{
					PushNotificationCannedApp.ApnsMowaDogfood,
					PushNotificationCannedApp.ApnsMowaOfficialIPhone,
					PushNotificationCannedApp.ApnsMowaOfficialIPad,
					PushNotificationCannedApp.GcmMowaOfficial
				}, new PushNotificationApp[]
				{
					PushNotificationCannedApp.ApnsMowaOfficialIPhone,
					PushNotificationCannedApp.ApnsMowaOfficialIPad,
					PushNotificationCannedApp.GcmMowaOfficial
				}, new string[0], new string[0], "exo", new Dictionary<string, string>())
			}
		};
	}
}
