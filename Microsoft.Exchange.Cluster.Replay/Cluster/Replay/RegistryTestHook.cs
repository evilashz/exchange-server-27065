using System;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200012F RID: 303
	internal static class RegistryTestHook
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00032FFC File Offset: 0x000311FC
		internal static int TargetServerVersionOverride
		{
			get
			{
				RegistryTestHook.LoadRegistryValues();
				return RegistryTestHook.s_targetServerVersionOverride;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00033008 File Offset: 0x00031208
		internal static SafetyNetVersionCheckerOverrideEnum SafetyNetVersionCheckerOverride
		{
			get
			{
				RegistryTestHook.LoadRegistryValues();
				return (SafetyNetVersionCheckerOverrideEnum)RegistryTestHook.s_safetyNetVersionCheckerOverride;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00033014 File Offset: 0x00031214
		internal static int IncReseedDelayInSecs
		{
			get
			{
				RegistryTestHook.LoadRegistryValues();
				return RegistryTestHook.s_incReseedDelayInSecs;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00033048 File Offset: 0x00031248
		internal static int SeedDelayPerCallbackInMilliSeconds
		{
			get
			{
				int tempValue = 0;
				RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\TestHook", delegate(IRegistryKey key)
				{
					tempValue = (int)key.GetValue("SeedDelayPerCallbackInMilliSeconds", 0);
				});
				return tempValue;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000330A4 File Offset: 0x000312A4
		internal static int SeedFailAfterProgressPercent
		{
			get
			{
				int tempValue = 0;
				RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\TestHook", delegate(IRegistryKey key)
				{
					tempValue = (int)key.GetValue("SeedFailAfterProgressPercent", 0);
				});
				return tempValue;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00033108 File Offset: 0x00031308
		internal static bool SeedDisableTruncationCoordination
		{
			get
			{
				bool tempValue = false;
				RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\TestHook", delegate(IRegistryKey key)
				{
					tempValue = ((int)key.GetValue("SeedDisableTruncationCoordination", 0) != 0);
				});
				return tempValue;
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000331AA File Offset: 0x000313AA
		private static void LoadRegistryValues()
		{
			if (RegistryTestHook.s_loadedRegistryValues)
			{
				return;
			}
			RegistryParameters.TryGetRegistryParameters("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\TestHook", delegate(IRegistryKey key)
			{
				RegistryTestHook.s_targetServerVersionOverride = (int)key.GetValue("TargetServerVersionOverride", RegistryTestHook.s_targetServerVersionOverride);
				RegistryTestHook.s_incReseedDelayInSecs = (int)key.GetValue("IncReseedDelayInSecs", RegistryTestHook.s_incReseedDelayInSecs);
				RegistryTestHook.s_safetyNetVersionCheckerOverride = (int)key.GetValue("SafetyNetVersionCheckerOverride", RegistryTestHook.s_safetyNetVersionCheckerOverride);
			});
			RegistryTestHook.s_loadedRegistryValues = true;
		}

		// Token: 0x040004CC RID: 1228
		private const string TestHookKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\TestHook";

		// Token: 0x040004CD RID: 1229
		private static int s_targetServerVersionOverride = 0;

		// Token: 0x040004CE RID: 1230
		private static int s_safetyNetVersionCheckerOverride = 0;

		// Token: 0x040004CF RID: 1231
		private static int s_incReseedDelayInSecs = 0;

		// Token: 0x040004D0 RID: 1232
		private static bool s_loadedRegistryValues;
	}
}
