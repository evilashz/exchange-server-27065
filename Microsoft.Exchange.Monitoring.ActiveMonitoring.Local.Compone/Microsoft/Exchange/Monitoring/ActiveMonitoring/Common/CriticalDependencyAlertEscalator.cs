﻿using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000590 RID: 1424
	internal sealed class CriticalDependencyAlertEscalator
	{
		// Token: 0x0600239A RID: 9114 RVA: 0x000D4F38 File Offset: 0x000D3138
		public CriticalDependencyAlertEscalator(Trace trace, TracingContext traceContext)
		{
			this.trace = trace;
			this.traceContext = traceContext;
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000D4F50 File Offset: 0x000D3150
		private static string LocalMachineVersion
		{
			get
			{
				if (CriticalDependencyAlertEscalator.localMachineVersion == null)
				{
					try
					{
						AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).Single<object>();
						CriticalDependencyAlertEscalator.localMachineVersion = string.Format("{0}-{1}", assemblyFileVersionAttribute.Version, Environment.OSVersion.Version.ToString());
					}
					catch (InvalidOperationException)
					{
						CriticalDependencyAlertEscalator.localMachineVersion = string.Empty;
					}
				}
				return CriticalDependencyAlertEscalator.localMachineVersion;
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000D4FCC File Offset: 0x000D31CC
		private static bool IsOBDGallatinMachine()
		{
			bool result = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs", false))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("IsOBDGallatinMachine", null);
					if (value != null)
					{
						result = ((int)value == 1);
					}
				}
			}
			return result;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000D5028 File Offset: 0x000D3228
		private static bool IsFfoMachine()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ForefrontForOfficeMode");
					if (value != null && value is int && (int)value == 1)
					{
						return true;
					}
				}
			}
			RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CentralAdminRole");
			if (registryKey2 != null)
			{
				registryKey2.Close();
				registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellSnapIns\\Microsoft.Exchange.Management.Powershell.FfoCentralAdmin");
				if (registryKey2 != null)
				{
					registryKey2.Close();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000D50C0 File Offset: 0x000D32C0
		internal static bool RunningInMicrosoftDatacenter()
		{
			string text;
			string text2;
			return CriticalDependencyAlertEscalator.GetCertificateSubjectAndEndpoint(out text, out text2);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000D50D8 File Offset: 0x000D32D8
		private static bool GetCertificateSubjectAndEndpoint(out string subject, out string endpoint)
		{
			subject = null;
			endpoint = null;
			bool result = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(CriticalDependencyAlertEscalator.ActiveMonitoringRegistryPath, false))
			{
				if (registryKey != null)
				{
					subject = (string)registryKey.GetValue("RPSCertificateSubject", null);
					endpoint = (string)registryKey.GetValue("RPSEndpoint", null);
					if (subject != null && endpoint != null)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000D5150 File Offset: 0x000D3350
		private void InvokeNewServiceAlert(Guid alertGuid, string alertTypeId, string alertName, string alertDescription, DateTime raisedTime, string escalationTeam, string service, string alertSource, bool isDatacenter, bool urgent, string environment, string location, string forest, string dag, string site, string region, string capacityUnit, string rack, string alertCategory, bool isIncident, bool skipSuppression)
		{
			if (string.IsNullOrWhiteSpace(escalationTeam))
			{
				throw new ArgumentException("escalationTeam");
			}
			if (string.IsNullOrWhiteSpace(service))
			{
				throw new ArgumentException("service");
			}
			if (string.IsNullOrWhiteSpace(alertSource))
			{
				throw new ArgumentException("alertSource");
			}
			if (string.IsNullOrWhiteSpace(alertName))
			{
				throw new ArgumentException("alertName");
			}
			if (string.IsNullOrWhiteSpace(alertDescription))
			{
				throw new ArgumentException("alertDescription");
			}
			this.CreateRunspace();
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("New-ServiceAlert");
			pscommand.AddParameter("AlertTypeId", alertTypeId);
			pscommand.AddParameter("AlertId", alertGuid);
			pscommand.AddParameter("AlertName", alertName);
			pscommand.AddParameter("AlertDescription", alertDescription);
			pscommand.AddParameter("RaisedTime", raisedTime);
			pscommand.AddParameter("EscalationTeam", escalationTeam);
			pscommand.AddParameter("Service", service);
			pscommand.AddParameter("AlertSource", alertSource);
			pscommand.AddParameter("IsUrgent", urgent);
			pscommand.AddParameter("IsIncident", isIncident);
			pscommand.AddParameter("SkipSuppression", skipSuppression);
			if (isDatacenter)
			{
				string machineName = Environment.MachineName;
				pscommand.AddParameter("MachineName", machineName);
				if (CriticalDependencyAlertEscalator.IsFfoMachine() || CriticalDependencyAlertEscalator.IsOBDGallatinMachine())
				{
					pscommand.AddParameter("MachineProvisioningState", "Provisioned");
					pscommand.AddParameter("MachineMonitoringState", "On");
					if (!string.IsNullOrEmpty(CriticalDependencyAlertEscalator.LocalMachineVersion))
					{
						pscommand.AddParameter("MachineVersion", CriticalDependencyAlertEscalator.LocalMachineVersion);
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(environment))
			{
				pscommand.AddParameter("Environment", environment);
			}
			if (!string.IsNullOrWhiteSpace(location))
			{
				pscommand.AddParameter("Location", location);
			}
			if (!string.IsNullOrWhiteSpace(forest))
			{
				pscommand.AddParameter("Forest", forest);
			}
			if (!string.IsNullOrWhiteSpace(dag))
			{
				pscommand.AddParameter("Dag", dag);
			}
			if (!string.IsNullOrWhiteSpace(site))
			{
				pscommand.AddParameter("Site", site);
			}
			if (!string.IsNullOrWhiteSpace(region))
			{
				pscommand.AddParameter("Region", region);
			}
			if (!string.IsNullOrWhiteSpace(capacityUnit))
			{
				pscommand.AddParameter("CapacityUnit", capacityUnit);
			}
			if (!string.IsNullOrWhiteSpace(rack))
			{
				pscommand.AddParameter("Rack", capacityUnit);
			}
			if (!string.IsNullOrEmpty(alertCategory))
			{
				pscommand.AddParameter("AlertCategory", alertCategory);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(pscommand.Commands[0].CommandText);
			foreach (CommandParameter commandParameter in pscommand.Commands[0].Parameters)
			{
				stringBuilder.AppendFormat(" -{0}:{1}", commandParameter.Name, commandParameter.Value.ToString());
			}
			WTFDiagnostics.TraceDebug<string, string>(this.trace, this.traceContext, "CriticalDependencyAlertEscalator.InvokeNewServiceAlert: Escalating alert '{0}' via command '{1}'...", alertName, stringBuilder.ToString(), null, "InvokeNewServiceAlert", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\CriticalDependencyAlertEscalator.cs", 387);
			try
			{
				this.remotePowerShell.InvokePSCommand(pscommand);
				WTFDiagnostics.TraceInformation<string>(this.trace, this.traceContext, "CriticalDependencyAlertEscalator.InvokeNewServiceAlert: Successfully escalated alert '{0}'.", alertName, null, "InvokeNewServiceAlert", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\CriticalDependencyVerification\\CriticalDependencyAlertEscalator.cs", 391);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("CriticalDependencyAlertEscalator.InvokeNewServiceAlert: Unexpected failure when escalating alert '{0}'\r\n\r\nException: {1}\r\n\r\nCommand: '{2}'", alertName, ex.ToString(), stringBuilder.ToString()));
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000D54D0 File Offset: 0x000D36D0
		private bool ShouldRaiseActiveMonitoringAlerts(EscalationEnvironment environment)
		{
			switch (environment)
			{
			case EscalationEnvironment.Datacenter:
			{
				bool result = false;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(CriticalDependencyAlertEscalator.ActiveMonitoringRegistryPath, false))
				{
					if (registryKey != null)
					{
						result = (0 != (int)registryKey.GetValue("AlertsEnabled", 0));
					}
				}
				return result;
			}
			case EscalationEnvironment.OutsideIn:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000D5548 File Offset: 0x000D3748
		private void CreateRunspace()
		{
			string text = null;
			string certificateSubjectDN = null;
			if (!CriticalDependencyAlertEscalator.GetCertificateSubjectAndEndpoint(out certificateSubjectDN, out text))
			{
				throw new Exception("Can't create Remote PowerShell runspace. Missing endpoint and/or certificate settings.");
			}
			if (text.Contains(";"))
			{
				this.remotePowerShell = RemotePowerShell.CreateRemotePowerShellByCertificate(text.Split(new char[]
				{
					';'
				}), certificateSubjectDN, this.GetEscalationEnvironment() != EscalationEnvironment.OutsideIn);
				return;
			}
			this.remotePowerShell = RemotePowerShell.CreateRemotePowerShellByCertificate(new Uri(text), certificateSubjectDN, this.GetEscalationEnvironment() != EscalationEnvironment.OutsideIn);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000D55C8 File Offset: 0x000D37C8
		private EscalationEnvironment GetEscalationEnvironment()
		{
			if (this.escalationEnvironment == null)
			{
				if (CriticalDependencyAlertEscalator.RunningInMicrosoftDatacenter())
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.Datacenter);
				}
				else
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.OnPrem);
				}
				if (CriticalDependencyAlertEscalator.IsOBDGallatinMachine())
				{
					this.escalationEnvironment = new EscalationEnvironment?(EscalationEnvironment.Datacenter);
				}
			}
			return this.escalationEnvironment.Value;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000D5624 File Offset: 0x000D3824
		public void Escalate(string alertSubject, string alertMessage, string alertTypeId, string escalationService, string escalationTeam)
		{
			if (!this.ShouldRaiseActiveMonitoringAlerts(this.GetEscalationEnvironment()))
			{
				return;
			}
			string alertSource = "LocalActiveMonitoring";
			bool isDatacenter = this.GetEscalationEnvironment() == EscalationEnvironment.Datacenter;
			string empty = string.Empty;
			string location = null;
			string forest = null;
			string dag = null;
			string site = null;
			string region = null;
			string capacityUnit = null;
			string rack = null;
			string alertCategory = null;
			bool urgent = true;
			bool isIncident = false;
			bool skipSuppression = false;
			Guid alertGuid = Guid.NewGuid();
			this.InvokeNewServiceAlert(alertGuid, alertTypeId, alertSubject, alertMessage, DateTime.UtcNow, escalationTeam, escalationService, alertSource, isDatacenter, urgent, empty, location, forest, dag, site, region, capacityUnit, rack, alertCategory, isIncident, skipSuppression);
		}

		// Token: 0x0400197E RID: 6526
		private static readonly string ActiveMonitoringRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x0400197F RID: 6527
		private static string localMachineVersion = null;

		// Token: 0x04001980 RID: 6528
		private RemotePowerShell remotePowerShell;

		// Token: 0x04001981 RID: 6529
		private Trace trace;

		// Token: 0x04001982 RID: 6530
		private TracingContext traceContext;

		// Token: 0x04001983 RID: 6531
		private EscalationEnvironment? escalationEnvironment;
	}
}
