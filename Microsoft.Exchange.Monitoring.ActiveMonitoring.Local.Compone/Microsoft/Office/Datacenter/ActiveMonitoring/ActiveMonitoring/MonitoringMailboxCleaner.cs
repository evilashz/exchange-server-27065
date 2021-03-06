using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.ActiveMonitoring
{
	// Token: 0x0200020E RID: 526
	public sealed class MonitoringMailboxCleaner : ProbeWorkItem
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x000624C4 File Offset: 0x000606C4
		public static ProbeDefinition CreateDefinition()
		{
			return new ProbeDefinition
			{
				AssemblyPath = MonitoringMailboxCleaner.AssemblyPath,
				Name = "MonitoringMailboxCleaner",
				TypeName = MonitoringMailboxCleaner.TypeName,
				ServiceName = ExchangeComponent.Monitoring.Name,
				RecurrenceIntervalSeconds = 3600,
				TimeoutSeconds = 1200,
				Enabled = true
			};
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00062548 File Offset: 0x00060748
		protected override void DoWork(CancellationToken cancellationToken)
		{
			int num = this.ReadAttribute("DeletesPerExecution", 20);
			string format = this.ReadAttribute("MailboxDisplayNamePattern", "HealthMailbox-{0}-*");
			if (!DirectoryAccessor.Instance.Server.IsMailboxServer)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "This is not a mailboxserver.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 96);
				return;
			}
			ICollection<MailboxDatabaseInfo> source = null;
			try
			{
				if (LocalEndpointManager.Instance.MailboxDatabaseEndpoint != null)
				{
					source = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.UnverifiedMailboxDatabaseInfoCollectionForBackendLiveIdAuthenticationProbe;
				}
			}
			catch (Exception)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "MailboxdatabaseEndpoint not initialized.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 111);
				return;
			}
			List<ADUser> list = new List<ADUser>();
			string text = string.Format(format, MonitoringMailboxCleaner.localhost);
			IEnumerable<ADUser> enumerable = DirectoryAccessor.Instance.SearchMonitoringMailboxesByDisplayName(text);
			if (enumerable != null)
			{
				list.AddRange(enumerable);
			}
			WTFDiagnostics.TraceInformation<int, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Found {0} mailboxes with DisplayName: {1}", list.Count<ADUser>(), text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 125);
			int num2 = 0;
			int num3 = 0;
			using (List<ADUser>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADUser monitoringMailbox = enumerator.Current;
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Check mailbox {0}", monitoringMailbox.DisplayName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 132);
					cancellationToken.ThrowIfCancellationRequested();
					if (this.isFeMonitoringMailboxDisplayName(monitoringMailbox.DisplayName))
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Mailbox {0} is a cafe mailbox", monitoringMailbox.DisplayName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 138);
					}
					else if (source.Any((MailboxDatabaseInfo d) => d.MonitoringAccountUserPrincipalName.Equals(monitoringMailbox.UserPrincipalName)))
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Mailbox {0} is currently being used", monitoringMailbox.DisplayName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 145);
					}
					else
					{
						if (num2 >= num)
						{
							break;
						}
						try
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Attempting to delete monitoring mailbox {0}.", monitoringMailbox.UserPrincipalName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 157);
							num2++;
							DirectoryAccessor.Instance.DeleteMonitoringMailbox(monitoringMailbox);
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Successfully deleted monitoring mailbox {0}.", monitoringMailbox.UserPrincipalName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 162);
							num3++;
						}
						catch (Exception arg)
						{
							WTFDiagnostics.TraceError<string, Exception>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Could not delete monitoring mailbox {0}. Exception: {1}", monitoringMailbox.UserPrincipalName, arg, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 167);
						}
					}
				}
			}
			base.Result.StateAttribute1 = string.Format("Total deleted mailboxes: {0}", num3);
			base.Result.StateAttribute2 = string.Format("Total monitoring mailboxes found: {0}", list.Count);
			base.Result.StateAttribute3 = string.Format("Max mailboxes deletions: {0}", num);
			base.Result.StateAttribute4 = string.Format("DisplayName: {0}", text);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000628E0 File Offset: 0x00060AE0
		private bool isFeMonitoringMailboxDisplayName(string displayName)
		{
			bool result;
			try
			{
				string hostName = Dns.GetHostName();
				string text = string.Format("HealthMailbox-{0}-", hostName);
				int num = 0;
				result = int.TryParse(displayName.Substring(text.Length), out num);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Error when telling if it is a FE monitoring mailbox {0}", ex.ToString(), null, "isFeMonitoringMailboxDisplayName", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\MonitoringMailboxCleaner.cs", 193);
				result = true;
			}
			return result;
		}

		// Token: 0x04000B1A RID: 2842
		private const string MonitoringMailboxCleanerName = "MonitoringMailboxCleaner";

		// Token: 0x04000B1B RID: 2843
		private const string defaultMailboxDisplayNamePattern = "HealthMailbox-{0}-*";

		// Token: 0x04000B1C RID: 2844
		private const int defaultMaxDeletesPerExecution = 20;

		// Token: 0x04000B1D RID: 2845
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000B1E RID: 2846
		private static readonly string TypeName = typeof(MonitoringMailboxCleaner).FullName;

		// Token: 0x04000B1F RID: 2847
		private static string localhost = Dns.GetHostName();
	}
}
