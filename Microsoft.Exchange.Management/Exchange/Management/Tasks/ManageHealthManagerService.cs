using System;
using System.Configuration.Install;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000600 RID: 1536
	public abstract class ManageHealthManagerService : ManageService
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000E2D28 File Offset: 0x000E0F28
		public ManageHealthManagerService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.HealthManagerServiceDisplayName;
			base.Description = Strings.HealthManagerServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeHMHost.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new string[]
			{
				"eventlog",
				"MSExchangeADTopology"
			};
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.ActiveMonitoring.EventLog.dll");
			base.CategoryCount = 2;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000E2E21 File Offset: 0x000E1021
		protected override string Name
		{
			get
			{
				return "MSExchangeHM";
			}
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000E2E28 File Offset: 0x000E1028
		protected void RegisterProcessManagerEventLog()
		{
			RegistryKey registryKey = null;
			string text = ManageService.eventLogRegPath + "MSExchangeHMHost";
			string value = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Common.ProcessManagerMsg.dll");
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(text, true);
				if (registryKey == null)
				{
					registryKey = Registry.LocalMachine.CreateSubKey(text, RegistryKeyPermissionCheck.ReadWriteSubTree);
				}
				registryKey.SetValue(ManageService.eventMessageFileSubKeyName, value);
				registryKey.SetValue(ManageService.categoryMessageFileSubKeyName, value);
				registryKey.SetValue(ManageService.categoryCountSubKeyName, 1);
				registryKey.SetValue(ManageService.typesSupportedSubKeyName, 7);
			}
			catch (SecurityException inner)
			{
				base.WriteError(new SecurityException(Strings.ErrorOpenKeyDeniedForWrite(text), inner), ErrorCategory.WriteError, null);
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
					registryKey = null;
				}
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000E2EF8 File Offset: 0x000E10F8
		protected void PersistManagedAvailabilityServersUsgSid()
		{
			SecurityIdentifier securityIdentifier = null;
			try
			{
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 153, "PersistManagedAvailabilityServersUsgSid", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Service\\ManageHealthManagerService.cs");
				securityIdentifier = rootOrganizationRecipientSession.GetWellKnownExchangeGroupSid(WellKnownGuid.MaSWkGuid);
			}
			catch (Exception)
			{
			}
			if (securityIdentifier != null)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(ManageHealthManagerService.RegistryPathBase))
				{
					registryKey.SetValue("ManagedAvailabilityServersUsgSid", securityIdentifier.ToString());
				}
				string name = "SOFTWARE\\Microsoft\\ExchangeServer";
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(name, true))
				{
					if (registryKey2 != null)
					{
						RegistrySecurity accessControl = registryKey2.GetAccessControl();
						accessControl.AddAccessRule(new RegistryAccessRule(securityIdentifier, RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
						registryKey2.SetAccessControl(accessControl);
						using (RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SecurePipeServers\\winreg", true))
						{
							RegistrySecurity accessControl2 = registryKey3.GetAccessControl();
							accessControl2.AddAccessRule(new RegistryAccessRule(securityIdentifier, RegistryRights.ExecuteKey, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
							registryKey3.SetAccessControl(accessControl2);
						}
					}
				}
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000E303C File Offset: 0x000E123C
		protected void RemoveManagedAvailabilityServersUsgSidCache()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(ManageHealthManagerService.RegistryPathBase))
			{
				registryKey.DeleteValue("ManagedAvailabilityServersUsgSid", false);
			}
		}

		// Token: 0x04002561 RID: 9569
		private const string ManagedAvailabilityServersUsgSidValueName = "ManagedAvailabilityServersUsgSid";

		// Token: 0x04002562 RID: 9570
		private const string ServiceShortName = "MSExchangeHM";

		// Token: 0x04002563 RID: 9571
		private const string ServiceBinaryName = "MSExchangeHMHost.exe";

		// Token: 0x04002564 RID: 9572
		private const string EventLogBinaryName = "Microsoft.Exchange.ActiveMonitoring.EventLog.dll";

		// Token: 0x04002565 RID: 9573
		private const string HostShortName = "MSExchangeHMHost";

		// Token: 0x04002566 RID: 9574
		private const string HostEventLogBinaryName = "Microsoft.Exchange.Common.ProcessManagerMsg.dll";

		// Token: 0x04002567 RID: 9575
		private static readonly string RegistryPathBase = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";
	}
}
