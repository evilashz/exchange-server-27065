using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Management.Automation;
using System.Security;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Monitoring;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200041B RID: 1051
	internal abstract class AssistantTroubleshooterBase : TroubleshooterCheck
	{
		// Token: 0x06002489 RID: 9353 RVA: 0x0009167E File Offset: 0x0008F87E
		public AssistantTroubleshooterBase(PropertyBag fields) : base(fields)
		{
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00091687 File Offset: 0x0008F887
		public ExchangeServer ExchangeServer
		{
			get
			{
				return (ExchangeServer)this.fields["ExchangeServer"];
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000916A0 File Offset: 0x0008F8A0
		public bool SendCrashDump
		{
			get
			{
				return ((SwitchParameter)(this.fields["IncludeCrashDump"] ?? new SwitchParameter(false))).IsPresent;
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000916DC File Offset: 0x0008F8DC
		public override MonitoringData Resolve(MonitoringData monitoringData)
		{
			foreach (MonitoringEvent monitoringEvent in monitoringData.Events)
			{
				if (monitoringEvent.EventIdentifier == 5002)
				{
					return monitoringData;
				}
			}
			this.GetWatsonExceptionBucketAndSendWatson(false);
			this.RestartAssistantService(monitoringData);
			monitoringData.PerformanceCounters.Add(this.GetCrashDumpCountPerformanceCounter());
			return monitoringData;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0009175C File Offset: 0x0008F95C
		internal MonitoringPerformanceCounter GetCrashDumpCountPerformanceCounter()
		{
			return new MonitoringPerformanceCounter("Watson", "CrashDumpCount", "MsExchangeMailboxAssistants", (double)this.crashDumpCount);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00091779 File Offset: 0x0008F979
		protected void RestartAssistantService(MonitoringData monitoringData)
		{
			this.StopAssistantService(this.ExchangeServer, monitoringData);
			this.StartAssistantService(this.ExchangeServer, monitoringData);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00091798 File Offset: 0x0008F998
		protected void StopAssistantService(ExchangeServer server, MonitoringData monitoringData)
		{
			try
			{
				using (ServiceController serviceController = new ServiceController("MsExchangeMailboxAssistants", server.Fqdn))
				{
					if (serviceController.Status == ServiceControllerStatus.Running || serviceController.Status == ServiceControllerStatus.StartPending)
					{
						serviceController.Stop();
					}
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(5.0));
					monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5001, EventTypeEnumeration.Information, Strings.MailboxAssistantsServiceStopped(this.ExchangeServer.Name)));
				}
			}
			catch (System.ServiceProcess.TimeoutException ex)
			{
				this.SendWatsonForAssistantProcess(ex, true);
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStopped(this.ExchangeServer.Name, ex.Message));
			}
			catch (InvalidOperationException ex2)
			{
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStopped(this.ExchangeServer.Name, ex2.Message));
			}
			catch (Win32Exception ex3)
			{
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStopped(this.ExchangeServer.Name, ex3.Message));
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000918D0 File Offset: 0x0008FAD0
		protected void StartAssistantService(ExchangeServer server, MonitoringData monitoringData)
		{
			try
			{
				using (ServiceController serviceController = new ServiceController("MsExchangeMailboxAssistants", server.Fqdn))
				{
					serviceController.Start();
					serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(5.0));
					monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5002, EventTypeEnumeration.Information, Strings.MailboxAssistantsServiceStarted(this.ExchangeServer.Name)));
				}
			}
			catch (System.ServiceProcess.TimeoutException ex)
			{
				this.SendWatsonForAssistantProcess(ex, false);
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStarted(this.ExchangeServer.Name, ex.Message));
			}
			catch (InvalidOperationException ex2)
			{
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStarted(this.ExchangeServer.Name, ex2.Message));
			}
			catch (Win32Exception ex3)
			{
				monitoringData.Events.Add(this.MailboxAssistantsServiceCouldNotBeStarted(this.ExchangeServer.Name, ex3.Message));
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000919F4 File Offset: 0x0008FBF4
		protected bool GetWatsonExceptionBucketAndSendWatson(bool killProcess)
		{
			Exception e = new AssistantServiceHungException();
			return this.SendWatsonForAssistantProcess(e, killProcess);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00091A14 File Offset: 0x0008FC14
		protected bool SendWatsonForAssistantProcess(Exception e, bool killProcessAfterWatson)
		{
			bool result;
			using (Process mailboxAssistantProcess = this.GetMailboxAssistantProcess(this.ExchangeServer.Fqdn))
			{
				if (mailboxAssistantProcess == null)
				{
					result = false;
				}
				else
				{
					bool flag = false;
					if (this.SendCrashDump)
					{
						this.crashDumpCount++;
						ExWatson.SendHangWatsonReport(e, mailboxAssistantProcess);
						flag = true;
					}
					if (killProcessAfterWatson)
					{
						this.ForceKillAssistantService(mailboxAssistantProcess);
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00091A84 File Offset: 0x0008FC84
		protected PerformanceCounter GetLocalizedPerformanceCounter(string categoryName, string counterName, string instanceName, string computerName)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				string localizedPerformanceCounterName = this.GetLocalizedPerformanceCounterName(categoryName, categoryName, computerName);
				string localizedPerformanceCounterName2 = this.GetLocalizedPerformanceCounterName(categoryName, counterName, computerName);
				if (localizedPerformanceCounterName != null && localizedPerformanceCounterName2 != null)
				{
					performanceCounter = new PerformanceCounter(localizedPerformanceCounterName, localizedPerformanceCounterName2, instanceName, computerName);
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			if (performanceCounter == null)
			{
				performanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, computerName);
			}
			return performanceCounter;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00091AFC File Offset: 0x0008FCFC
		protected Process GetMailboxAssistantProcess(string serverName)
		{
			Process[] processesByName;
			if (string.IsNullOrEmpty(serverName) || serverName.StartsWith(Environment.MachineName + ".", StringComparison.InvariantCultureIgnoreCase) || serverName.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase))
			{
				processesByName = Process.GetProcessesByName("MsExchangeMailboxAssistants");
			}
			else
			{
				processesByName = Process.GetProcessesByName("MsExchangeMailboxAssistants", serverName);
			}
			if (processesByName != null && processesByName.Length > 0)
			{
				return processesByName[0];
			}
			return null;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00091B60 File Offset: 0x0008FD60
		private void ForceKillAssistantService(Process process)
		{
			ManagementObject processObject = WmiWrapper.GetProcessObject(this.ExchangeServer.Fqdn, "MsExchangeMailboxAssistants.exe");
			if (processObject != null)
			{
				uint num = (uint)processObject.InvokeMethod("Terminate", new object[]
				{
					0
				});
				if (num != 0U)
				{
					throw new Win32Exception((int)num, Strings.MailboxAssistantsServiceCouldNotBeKilled(process.MachineName).ToString());
				}
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00091BCC File Offset: 0x0008FDCC
		protected List<MdbStatus> GetOnlineMDBList()
		{
			List<MdbStatus> onlineMDBList;
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", this.ExchangeServer.Name, null, null, null))
			{
				onlineMDBList = this.GetOnlineMDBList(exRpcAdmin);
			}
			return onlineMDBList;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00091C18 File Offset: 0x0008FE18
		protected List<MdbStatus> GetOnlineMDBList(ExRpcAdmin exrpcAdmin)
		{
			MdbStatus[] array = exrpcAdmin.ListMdbStatus(false);
			List<MdbStatus> list = new List<MdbStatus>(array.Length);
			foreach (MdbStatus mdbStatus in array)
			{
				if ((mdbStatus.Status & MdbStatusFlags.Online) == MdbStatusFlags.Online && StringComparer.OrdinalIgnoreCase.Equals(mdbStatus.VServerName, this.ExchangeServer.Name))
				{
					list.Add(mdbStatus);
				}
			}
			return list;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00091C80 File Offset: 0x0008FE80
		private string GetLocalizedPerformanceCounterName(string categoryName, string categoryOrCounterName, string machineName)
		{
			CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
			string name = string.Format("SYSTEM\\CurrentControlSet\\Services\\{0}\\Performance", categoryName);
			int? num = new int?(0);
			string[] array = null;
			string result;
			using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName))
			{
				if (registryKey == null)
				{
					result = null;
				}
				else
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\"))
					{
						cultureInfo = new CultureInfo((int)registryKey2.GetValue("Server Language", cultureInfo.LCID));
						if (cultureInfo.LCID == 1033)
						{
							return categoryOrCounterName;
						}
					}
					using (RegistryKey registryKey3 = registryKey.OpenSubKey(name))
					{
						if (registryKey3 == null)
						{
							return null;
						}
						string[] array2 = (string[])registryKey3.GetValue("Counter Names", null);
						if (array2 == null)
						{
							return null;
						}
						num = (int?)registryKey3.GetValue("First Counter", null);
						if (num == null)
						{
							return null;
						}
						for (int i = 0; i < array2.Length; i++)
						{
							if (StringComparer.OrdinalIgnoreCase.Equals(array2[i], categoryOrCounterName))
							{
								num += (i + 1) * 2;
								break;
							}
						}
					}
					using (RegistryKey registryKey4 = registryKey.OpenSubKey(this.GetPerformanceCounterLanguageKey(cultureInfo)))
					{
						if (registryKey4 == null)
						{
							return null;
						}
						array = (string[])registryKey4.GetValue("Counter", null);
						if (array == null)
						{
							return null;
						}
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (StringComparer.OrdinalIgnoreCase.Equals(array[j], num.ToString()))
						{
							return array[j + 1];
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00091ECC File Offset: 0x000900CC
		private string GetPerformanceCounterLanguageKey(CultureInfo cultureInfo)
		{
			int lcid = cultureInfo.LCID;
			int num = lcid & 1023;
			if (num == 4 || num == 22)
			{
				num = lcid;
			}
			return string.Format("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Perflib\\{0}", num.ToString("X3"));
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00091F09 File Offset: 0x00090109
		private MonitoringEvent MailboxAssistantsServiceCouldNotBeStarted(string serverName, string errorMessage)
		{
			return new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5203, EventTypeEnumeration.Error, Strings.MailboxAssistantsServiceCouldNotBeStarted(serverName, errorMessage));
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00091F27 File Offset: 0x00090127
		private MonitoringEvent MailboxAssistantsServiceCouldNotBeStopped(string serverName, string errorMessage)
		{
			return new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5202, EventTypeEnumeration.Error, Strings.MailboxAssistantsServiceCouldNotBeStopped(serverName, errorMessage));
		}

		// Token: 0x04001CF5 RID: 7413
		public const string MailboxAssistantServiceName = "MsExchangeMailboxAssistants";

		// Token: 0x04001CF6 RID: 7414
		public const string ExchangeServerPropertyName = "ExchangeServer";

		// Token: 0x04001CF7 RID: 7415
		public const string IncludeCrashDump = "IncludeCrashDump";

		// Token: 0x04001CF8 RID: 7416
		public const string WatsonPerformanceObject = "Watson";

		// Token: 0x04001CF9 RID: 7417
		public const string CrashDumpCountCounter = "CrashDumpCount";

		// Token: 0x04001CFA RID: 7418
		public static string EventSource = "MSExchange Monitoring MsExchangeMailboxAssistants Troubleshooter";

		// Token: 0x04001CFB RID: 7419
		private int crashDumpCount;
	}
}
