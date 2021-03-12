using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000064 RID: 100
	public static class DeviceCapability
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		internal static bool DeviceCanHandleRedirect(IAirSyncContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Request.Version < DeviceCapability.MinDcRedirectVersion)
			{
				AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.RequestsTracer, null, "Request protocol version {0} is lower than required for redirect {1}; will be proxied.", context.Request.Version, DeviceCapability.MinDcRedirectVersion);
				return false;
			}
			if (context.Request.CommandType == CommandType.Options || string.IsNullOrEmpty(context.Request.DeviceIdentity.DeviceType))
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "Device Type is not known (OPTIONS command); request must be proxied.");
				return false;
			}
			foreach (string deviceType in DeviceCapability.RedirectDeviceTypes)
			{
				if (context.Request.DeviceIdentity.IsDeviceType(deviceType))
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "Request device type {0} can be redirected.", context.Request.DeviceIdentity.DeviceType);
					return true;
				}
			}
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "DeviceCapability.DeviceCanHandleRedirect: request will be proxied");
			return false;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		internal static bool IsDirectPushAllowed(IAirSyncContext context, out bool directPushAllowedByGeo)
		{
			directPushAllowedByGeo = true;
			if (GlobalSettings.AllowDirectPush == GlobalSettings.DirectPushEnabled.On)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "DeviceCapability.IsDirectPushAllowed: ON by registry key");
				return true;
			}
			if (GlobalSettings.AllowDirectPush == GlobalSettings.DirectPushEnabled.OnWithAddressCheck)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "DeviceCapability.IsDirectPushAllowed: ONWithAddressCheck registry key and no geolocation logic");
				return true;
			}
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "DeviceCapability.IsDirectPushAllowed: OFF by registry key");
			return false;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00020C18 File Offset: 0x0001EE18
		internal static bool DeviceSupportedForMdm(GlobalInfo globalInfo)
		{
			if (globalInfo != null && !string.IsNullOrEmpty(globalInfo.DeviceOS))
			{
				string[] array = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmSupportedPlatforms.PlatformsSupported.Split(new char[]
				{
					','
				});
				foreach (string pattern in array)
				{
					if (Regex.IsMatch(globalInfo.DeviceOS, pattern, RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040003DC RID: 988
		private static readonly int MinDcRedirectVersion = GlobalSettings.MinRedirectProtocolVersion;

		// Token: 0x040003DD RID: 989
		private static readonly string[] RedirectDeviceTypes = GlobalSettings.DeviceTypesSupportingRedirect;
	}
}
