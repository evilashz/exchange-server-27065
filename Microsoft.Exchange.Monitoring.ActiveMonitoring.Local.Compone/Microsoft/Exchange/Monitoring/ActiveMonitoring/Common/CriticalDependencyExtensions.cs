using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200058F RID: 1423
	internal static class CriticalDependencyExtensions
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x000D4C40 File Offset: 0x000D2E40
		public static void RaiseAlert(this ICriticalDependency dependency, DateTime lastFixAttempt, Trace trace, TracingContext traceContext)
		{
			string name = dependency.Name;
			string text = string.Format("Critical monitoring dependency '{0}' failed.", name);
			string text2 = string.Format("Critical monitoring dependency '{0}' failed validation while the Health Manager was starting a new worker process. Automated recovery of this dependency failed at {1}. The Health Manager may not be able to report accurate health status of this machine nor run other recovery actions.", name, lastFixAttempt.ToString("u"));
			string text3 = string.Format("HealthManager/CriticalDependency/{0}", name);
			string escalationService = dependency.EscalationService;
			string escalationTeam = dependency.EscalationTeam;
			string message = string.Format("Raising alert for critical dependency failure '{0}'. Service: '{1}'. Team: '{2}'. AlertTypeId: '{3}'. AlertSubject: '{4}'. AlertMessage: {5}", new object[]
			{
				dependency.Name,
				escalationService,
				escalationTeam,
				text3,
				text,
				text2
			});
			WTFDiagnostics.TraceInformation(trace, traceContext, message, null, "RaiseAlert", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\CriticalDependencyExtensions.cs", 79);
			CriticalDependencyAlertEscalator criticalDependencyAlertEscalator = new CriticalDependencyAlertEscalator(trace, traceContext);
			criticalDependencyAlertEscalator.Escalate(text, text2, text3, escalationService, escalationTeam);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000D4CFC File Offset: 0x000D2EFC
		public static void RecordFixAttempt(this ICriticalDependency dependency)
		{
			CriticalDependencyExtensions.RecordTime(dependency.Name, "LastFixAttempt", DateTime.UtcNow);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000D4D13 File Offset: 0x000D2F13
		public static void RecordSuccessfulTest(this ICriticalDependency dependency)
		{
			CriticalDependencyExtensions.RecordTime(dependency.Name, "LastSuccessfulTest", DateTime.UtcNow);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000D4D2A File Offset: 0x000D2F2A
		public static void RecordFailedTest(this ICriticalDependency dependency)
		{
			CriticalDependencyExtensions.RecordTime(dependency.Name, "LastFailedTest", DateTime.UtcNow);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000D4D41 File Offset: 0x000D2F41
		public static void RecordEscalation(this ICriticalDependency dependency)
		{
			CriticalDependencyExtensions.RecordTime(dependency.Name, "LastEscalation", DateTime.UtcNow);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000D4D58 File Offset: 0x000D2F58
		public static void RetrieveHistory(this ICriticalDependency dependency, out DateTime lastSuccessfulTest, out DateTime lastFailedTest, out DateTime lastFixAttempt, out DateTime lastEscalation)
		{
			lastSuccessfulTest = DateTime.MinValue;
			lastFailedTest = DateTime.MinValue;
			lastFixAttempt = DateTime.MinValue;
			lastEscalation = DateTime.MinValue;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(CriticalDependencyExtensions.CriticalDependencyRegistryPath))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(dependency.Name))
					{
						if (registryKey2 != null)
						{
							string text = (string)registryKey2.GetValue("LastSuccessfulTest");
							string text2 = (string)registryKey2.GetValue("LastFailedTest");
							string text3 = (string)registryKey2.GetValue("LastFixAttempt");
							string text4 = (string)registryKey2.GetValue("LastEscalation");
							DateTime dateTime;
							if (text != null && DateTime.TryParseExact(text, "u", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
							{
								lastSuccessfulTest = dateTime;
							}
							DateTime dateTime2;
							if (text2 != null && DateTime.TryParseExact(text2, "u", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime2))
							{
								lastFailedTest = dateTime2;
							}
							DateTime dateTime3;
							if (text3 != null && DateTime.TryParseExact(text3, "u", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime3))
							{
								lastFixAttempt = dateTime3;
							}
							DateTime dateTime4;
							if (text4 != null && DateTime.TryParseExact(text4, "u", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime4))
							{
								lastEscalation = dateTime4;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000D4EBC File Offset: 0x000D30BC
		private static void RecordTime(string name, string occurrence, DateTime time)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(CriticalDependencyExtensions.CriticalDependencyRegistryPath))
			{
				using (RegistryKey registryKey2 = registryKey.CreateSubKey(name))
				{
					registryKey2.SetValue(occurrence, time.ToString("u"), RegistryValueKind.String);
				}
			}
		}

		// Token: 0x04001976 RID: 6518
		private const string alertTypeIdTemplate = "HealthManager/CriticalDependency/{0}";

		// Token: 0x04001977 RID: 6519
		private const string alertSubjectTemplate = "Critical monitoring dependency '{0}' failed.";

		// Token: 0x04001978 RID: 6520
		private const string alertMessageTemplate = "Critical monitoring dependency '{0}' failed validation while the Health Manager was starting a new worker process. Automated recovery of this dependency failed at {1}. The Health Manager may not be able to report accurate health status of this machine nor run other recovery actions.";

		// Token: 0x04001979 RID: 6521
		private const string LastFixAttemptRegistryValue = "LastFixAttempt";

		// Token: 0x0400197A RID: 6522
		private const string LastEscalationRegistryValue = "LastEscalation";

		// Token: 0x0400197B RID: 6523
		private const string LastSuccessfulTestRegistryValue = "LastSuccessfulTest";

		// Token: 0x0400197C RID: 6524
		private const string LastFailedTestRegistryValue = "LastFailedTest";

		// Token: 0x0400197D RID: 6525
		private static readonly string CriticalDependencyRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\CriticalDependencyVerification\\";
	}
}
