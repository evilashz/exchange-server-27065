using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Management.Automation;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008AB RID: 2219
	public abstract class ManageReplService : ManageService
	{
		// Token: 0x06004E52 RID: 20050 RVA: 0x00144BCC File Offset: 0x00142DCC
		protected ManageReplService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ReplayServiceDisplayName;
			base.Description = Strings.ReplayServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(this.InstallPath, "msexchangerepl.exe");
			base.ServiceInstallContext = installContext;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.EventMessageFile = Path.Combine(this.InstallPath, "clusmsg.dll");
			base.CategoryCount = 6;
			base.AddFirewallRule(new MSExchangeReplRPCFirewallRule());
			base.AddFirewallRule(new MSExchangeReplRPCEPMapFirewallRule());
			base.AddFirewallRule(new MSExchangeReplLogCopierFirewallRule());
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00144CCC File Offset: 0x00142ECC
		internal void InstallEventManifest()
		{
			try
			{
				if (ManageEventManifest.UpdateMessageDllPath(this.CrimsonEventManifestFile, this.CrimsonEventMsgDll, this.CrimsonEventProviderName))
				{
					ManageEventManifest.Install(this.CrimsonEventManifestFile);
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.EventManifestNotUpdated(this.CrimsonEventManifestFile, this.CrimsonEventMsgDll, this.CrimsonEventProviderName)), ErrorCategory.InvalidOperation, null);
				}
				if (ManageEventManifest.UpdateMessageDllPath(this.DbFailureItemEventManifestFile, this.DbFailureItemEventMsgDll, this.DbFailureItemProviderName))
				{
					ManageEventManifest.Install(this.DbFailureItemEventManifestFile);
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.EventManifestNotUpdated(this.DbFailureItemEventManifestFile, this.DbFailureItemEventMsgDll, this.DbFailureItemProviderName)), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00144D98 File Offset: 0x00142F98
		protected void UninstallEventManifest()
		{
			try
			{
				ManageEventManifest.Uninstall(this.DbFailureItemEventManifestFile);
				ManageEventManifest.Uninstall(this.CrimsonEventManifestFile);
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00144DDC File Offset: 0x00142FDC
		protected static bool ExtractNumberUsingRegex(string input, Regex regex, out int number, out string remainderString)
		{
			number = 0;
			remainderString = string.Empty;
			Match match = regex.Match(input);
			if (match.Success && int.TryParse(match.Groups[1].Value, out number))
			{
				remainderString = input.Substring(match.Index + match.Groups[0].Value.Length - 1);
				return true;
			}
			return false;
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00144E48 File Offset: 0x00143048
		protected string ExecuteNetSh(string arguments)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			Exception ex = null;
			try
			{
				int num = ProcessRunner.Run(ManageReplService.NetshExe, arguments, -1, null, out empty, out empty2);
				TaskLogger.Trace("Executed '{0} {1}'. Exit code: {2}, output: {3}, error: {4}.", new object[]
				{
					ManageReplService.NetshExe,
					arguments,
					num,
					empty,
					empty2
				});
				if (num == 0)
				{
					return empty;
				}
			}
			catch (Win32Exception ex2)
			{
				ex = ex2;
			}
			catch (System.TimeoutException ex3)
			{
				ex = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				TaskLogger.Trace(ex.Message, new object[0]);
				this.WriteWarning(Strings.ErrorCallingNetSh(ManageReplService.NetshExe + " " + arguments, ex.Message));
			}
			return string.Empty;
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00144F34 File Offset: 0x00143134
		private void SetDynamicRange(string ipversion, string protocol, int startPort, int numberOfPorts)
		{
			if (numberOfPorts < 1025)
			{
				TaskLogger.Trace("Not setting {0} dynamic range for protocol {1} to [{2}-{3}], because this new dynamic range is lower than minimum range of {4} ports.", new object[]
				{
					ipversion,
					protocol,
					startPort,
					startPort + numberOfPorts,
					1025
				});
				ManagementEventLogConstants.Tuple_ReducedDynamicRangeFailure.LogEvent(null, new object[]
				{
					ipversion,
					protocol,
					startPort,
					numberOfPorts,
					1025
				});
				return;
			}
			TaskLogger.Trace("Setting {0} dynamic range for protocol {1} to [{2}-{3}].", new object[]
			{
				ipversion,
				protocol,
				startPort,
				startPort + numberOfPorts
			});
			string text = this.ExecuteNetSh(string.Format("int {0} set dynamicport protocol={1} startport={2} numberofports={3} store=persistent", new object[]
			{
				ipversion,
				protocol,
				startPort,
				numberOfPorts
			}));
			if (!string.IsNullOrEmpty(text))
			{
				TaskLogger.Trace(text, new object[0]);
			}
			ManagementEventLogConstants.Tuple_ReducedDynamicRangeSuccess.LogEvent(null, new object[]
			{
				ipversion,
				protocol,
				startPort,
				numberOfPorts,
				text
			});
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00145078 File Offset: 0x00143278
		private void RemovePortFromDynamicRange(string ipversion, string protocol, int portToRemove)
		{
			string text = this.ExecuteNetSh(string.Format("int {0} show dynamicport {1}", ipversion, protocol));
			string empty = string.Empty;
			int num;
			int num2;
			if (string.IsNullOrEmpty(text) || !ManageReplService.ExtractNumberUsingRegex(text, ManageReplService.StartPortRegex, out num, out empty) || !ManageReplService.ExtractNumberUsingRegex(empty, ManageReplService.NumberOfPortsRegex, out num2, out text))
			{
				ManagementEventLogConstants.Tuple_FailedToGetCurrentDynamicRange.LogEvent(null, new object[]
				{
					ipversion,
					protocol,
					text
				});
				return;
			}
			int num3 = num + num2;
			TaskLogger.Trace("Current dynamic range for {0} protocol {1} is [{2}-{3}]. Netsh: {4}", new object[]
			{
				ipversion,
				protocol,
				num,
				num3,
				text
			});
			if (portToRemove < num || portToRemove > num3)
			{
				TaskLogger.Trace("Nothing to do. Port {0} is already not withing dynamic range.", new object[]
				{
					portToRemove
				});
				return;
			}
			ManagementEventLogConstants.Tuple_CurrentDynamicRange.LogEvent(null, new object[]
			{
				ipversion,
				protocol,
				num,
				num2,
				text
			});
			TaskLogger.Trace("Going to exclude port {0} from that range.", new object[]
			{
				portToRemove
			});
			if (num3 - portToRemove + 1 < portToRemove - num)
			{
				this.SetDynamicRange(ipversion, protocol, num, portToRemove - num - 1);
				return;
			}
			this.SetDynamicRange(ipversion, protocol, portToRemove + 1, num3 - portToRemove - 1);
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x001451E4 File Offset: 0x001433E4
		private void RemovePortFromDynamicRange(int portToExclude)
		{
			this.RemovePortFromDynamicRange("ipv4", "tcp", portToExclude);
			this.RemovePortFromDynamicRange("ipv4", "udp", portToExclude);
			this.RemovePortFromDynamicRange("ipv6", "tcp", portToExclude);
			this.RemovePortFromDynamicRange("ipv6", "udp", portToExclude);
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00145235 File Offset: 0x00143435
		internal void RegisterDefaultHighAvailabilityWebServicePort()
		{
			this.RemovePortFromDynamicRange(64337);
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00145242 File Offset: 0x00143442
		internal void RegisterDefaultLogCopierPort()
		{
			this.RemovePortFromDynamicRange(64327);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00145250 File Offset: 0x00143450
		internal void RestoreDynamicPortRange()
		{
			this.SetDynamicRange("ipv4", "tcp", 6005, 59530);
			this.SetDynamicRange("ipv4", "udp", 49152, 16384);
			this.SetDynamicRange("ipv6", "tcp", 6005, 59530);
			this.SetDynamicRange("ipv6", "udp", 49152, 16384);
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x001452C5 File Offset: 0x001434C5
		protected string InstallPath
		{
			get
			{
				return ConfigurationContext.Setup.BinPath;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x001452CC File Offset: 0x001434CC
		protected string CrimsonEventManifestFile
		{
			get
			{
				return Path.Combine(this.InstallPath, "ReplayCrimsonEvents.man");
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x001452DE File Offset: 0x001434DE
		protected string CrimsonEventMsgDll
		{
			get
			{
				return Path.Combine(this.InstallPath, "ReplayCrimsonMsg.Dll");
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x001452F0 File Offset: 0x001434F0
		protected string DbFailureItemEventManifestFile
		{
			get
			{
				return Path.Combine(this.InstallPath, "ExDbFailureEvents.man");
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x00145302 File Offset: 0x00143502
		protected string DbFailureItemEventMsgDll
		{
			get
			{
				return Path.Combine(this.InstallPath, "ExDbFailureItemApi.Dll");
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x00145314 File Offset: 0x00143514
		protected string DbFailureItemProviderName
		{
			get
			{
				return "Microsoft-Exchange-MailboxDatabaseFailureItems";
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x0014531B File Offset: 0x0014351B
		protected string CrimsonEventProviderName
		{
			get
			{
				return "Microsoft-Exchange-HighAvailability";
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00145322 File Offset: 0x00143522
		protected override string Name
		{
			get
			{
				return "MSExchangeRepl";
			}
		}

		// Token: 0x04002E9B RID: 11931
		protected const int DefaultHighAvailabilityWebServicePort = 64337;

		// Token: 0x04002E9C RID: 11932
		protected const int DefaultLogCopierPort = 64327;

		// Token: 0x04002E9D RID: 11933
		protected const int MinimumDynamicRangeNumberOfPorts = 1025;

		// Token: 0x04002E9E RID: 11934
		protected const int DefaultTcpStartPort = 6005;

		// Token: 0x04002E9F RID: 11935
		protected const int DefaultTcpNumberOfPorts = 59530;

		// Token: 0x04002EA0 RID: 11936
		protected const int DefaultUdpStartPort = 49152;

		// Token: 0x04002EA1 RID: 11937
		protected const int DefaultUdpNumberOfPorts = 16384;

		// Token: 0x04002EA2 RID: 11938
		protected static Regex StartPortRegex = new Regex("\\ *:\\ *([0-9]+)[^[0-9]]?", RegexOptions.IgnoreCase);

		// Token: 0x04002EA3 RID: 11939
		protected static Regex NumberOfPortsRegex = new Regex("\\ *:\\ *([0-9]+)[^[0-9]]?", RegexOptions.IgnoreCase);

		// Token: 0x04002EA4 RID: 11940
		protected static string NetshExe = Path.Combine(Environment.SystemDirectory, "netsh.exe");

		// Token: 0x04002EA5 RID: 11941
		public bool ForceFailure;
	}
}
