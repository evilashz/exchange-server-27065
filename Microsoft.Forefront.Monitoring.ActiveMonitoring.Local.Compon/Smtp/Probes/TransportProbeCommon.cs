using System;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000050 RID: 80
	internal class TransportProbeCommon
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000C804 File Offset: 0x0000AA04
		public static bool IsProbeExecutionEnabled()
		{
			if (TransportProbeCommon.probeExecutionEnabled == null)
			{
				if (!ExEnvironment.IsTest)
				{
					TransportProbeCommon.probeExecutionEnabled = new bool?(true);
				}
				else
				{
					TransportProbeCommon.probeExecutionEnabled = new bool?(TransportProbeCommon.RunTransportProbesKeyPresent());
				}
			}
			return TransportProbeCommon.probeExecutionEnabled.Value;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C840 File Offset: 0x0000AA40
		internal static bool ErrorMatches(string lastResponse, string errorPattern)
		{
			Match match = Regex.Match(lastResponse.Trim().ToUpperInvariant(), errorPattern.Trim().ToUpperInvariant());
			return match.Success;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C86F File Offset: 0x0000AA6F
		internal static bool ErrorContains(string lastResponse, string errorPattern)
		{
			return lastResponse.IndexOf(errorPattern, StringComparison.OrdinalIgnoreCase) > -1;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private static bool RunTransportProbesKeyPresent()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Exchange_Test\\v15\\RunTransportProbes"))
				{
					result = (registryKey != null);
				}
			}
			catch (UnauthorizedAccessException)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.TransportTracer, new TracingContext(), "UnauthorizedAccessException opening registry key", null, "RunTransportProbesKeyPresent", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Common\\TransportProbeCommon.cs", 97);
				result = false;
			}
			catch (SecurityException)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.TransportTracer, new TracingContext(), "SecurityException opening registry key", null, "RunTransportProbesKeyPresent", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Common\\TransportProbeCommon.cs", 107);
				result = false;
			}
			return result;
		}

		// Token: 0x04000153 RID: 339
		internal const string RunTransportProbesRegistryKeyName = "Software\\Microsoft\\Exchange_Test\\v15\\RunTransportProbes";

		// Token: 0x04000154 RID: 340
		private static bool? probeExecutionEnabled;
	}
}
