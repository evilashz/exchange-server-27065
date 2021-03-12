using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.WindowsFirewall;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000B7 RID: 183
	public abstract class ManageService : ConfigureService
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x0001B144 File Offset: 0x00019344
		protected ManageService()
		{
			TaskLogger.LogEnter();
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = base.GetType().Assembly.Location;
			this.serviceInstaller = new ServiceInstaller();
			this.serviceInstaller.Context = installContext;
			this.serviceInstaller.ServiceName = this.Name;
			this.serviceInstaller.StartType = ServiceStartMode.Manual;
			this.serviceProcessInstaller = new ServiceProcessInstaller();
			this.serviceProcessInstaller.Context = installContext;
			this.serviceProcessInstaller.Account = ServiceAccount.NetworkService;
			this.serviceProcessInstaller.Installers.Add(this.serviceInstaller);
			this.ServicesDependedOn = new string[]
			{
				ManagedServiceName.ActiveDirectoryTopologyService
			};
			this.serviceFirewallRules = new List<ExchangeFirewallRule>(2);
			TaskLogger.LogExit();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001B22C File Offset: 0x0001942C
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001B239 File Offset: 0x00019439
		protected string DisplayName
		{
			get
			{
				return this.serviceInstaller.DisplayName;
			}
			set
			{
				this.serviceInstaller.DisplayName = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001B250 File Offset: 0x00019450
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001B247 File Offset: 0x00019447
		protected string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001B258 File Offset: 0x00019458
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001B265 File Offset: 0x00019465
		protected ServiceAccount Account
		{
			get
			{
				return this.serviceProcessInstaller.Account;
			}
			set
			{
				this.serviceProcessInstaller.Account = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001B273 File Offset: 0x00019473
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0001B280 File Offset: 0x00019480
		protected ServiceStartMode StartMode
		{
			get
			{
				return this.serviceInstaller.StartType;
			}
			set
			{
				this.serviceInstaller.StartType = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001B28E File Offset: 0x0001948E
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0001B29B File Offset: 0x0001949B
		protected string[] ServicesDependedOn
		{
			get
			{
				return this.serviceInstaller.ServicesDependedOn;
			}
			set
			{
				this.serviceInstaller.ServicesDependedOn = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001B2A9 File Offset: 0x000194A9
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x0001B2B6 File Offset: 0x000194B6
		protected InstallContext ServiceInstallContext
		{
			get
			{
				return this.serviceProcessInstaller.Context;
			}
			set
			{
				this.serviceProcessInstaller.Context = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001B2CD File Offset: 0x000194CD
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001B2C4 File Offset: 0x000194C4
		protected string EventMessageFile
		{
			get
			{
				return this.eventMessageFile;
			}
			set
			{
				this.eventMessageFile = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001B2DE File Offset: 0x000194DE
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0001B2D5 File Offset: 0x000194D5
		protected int CategoryCount
		{
			get
			{
				return this.categoryCount;
			}
			set
			{
				this.categoryCount = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001B2E6 File Offset: 0x000194E6
		protected ServiceInstaller ServiceInstaller
		{
			get
			{
				return this.serviceInstaller;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001B2F0 File Offset: 0x000194F0
		internal static void AddAssemblyToFirewallExceptions(string name, string fullPath, Task.TaskErrorLoggingDelegate errorHandler)
		{
			if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(name))
			{
				return;
			}
			if (ConfigurationContext.Setup.IsLonghornServer)
			{
				string args = string.Format("advfirewall firewall add rule name=\"{0}\" dir=in action=allow program=\"{1}\" localip=any remoteip=any profile=any Enable=yes", name, fullPath);
				ManageService.RunNetShProcess(args, errorHandler);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001B32C File Offset: 0x0001952C
		internal static void RemoveAssemblyFromFirewallExceptions(string name, string fullPath, Task.TaskErrorLoggingDelegate errorHandler)
		{
			if (string.IsNullOrEmpty(fullPath))
			{
				return;
			}
			if (ConfigurationContext.Setup.IsLonghornServer)
			{
				string args = string.Format("advfirewall firewall delete rule name=\"{0}\" program=\"{1}\"", name, fullPath);
				ManageService.RunNetShProcess(args, errorHandler);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001B360 File Offset: 0x00019560
		protected void Install()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Name
			});
			Hashtable hashtable = new Hashtable();
			if (!ServiceControllerUtils.IsInstalled(this.Name))
			{
				try
				{
					TaskLogger.Trace("Installing service", new object[0]);
					this.serviceProcessInstaller.Install(hashtable);
				}
				catch (Win32Exception ex)
				{
					if (1072 == ex.NativeErrorCode)
					{
						Thread.Sleep(10000);
						hashtable = new Hashtable();
						this.serviceProcessInstaller.Install(hashtable);
					}
					else
					{
						base.WriteError(new TaskWin32Exception(ex), ErrorCategory.WriteError, null);
					}
				}
				base.ConfigureServiceSidType();
				if (this.serviceFirewallRules.Count > 0)
				{
					foreach (ExchangeFirewallRule exchangeFirewallRule in this.serviceFirewallRules)
					{
						TaskLogger.Trace("Adding Windows Firewall Rule for Service {0}", new object[]
						{
							this.Name
						});
						exchangeFirewallRule.Add();
					}
				}
				this.serviceProcessInstaller.Commit(hashtable);
			}
			else
			{
				TaskLogger.Trace("Service is already installed.", new object[0]);
			}
			if (this.Description != null)
			{
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(ManageService.serviceRegPath + this.Name, true))
					{
						registryKey.SetValue(ManageService.descriptionSubKeyName, this.Description);
					}
					goto IL_197;
				}
				catch (SecurityException inner)
				{
					base.WriteError(new SecurityException(Strings.ErrorOpenKeyDeniedForWrite(ManageService.serviceRegPath + this.Name), inner), ErrorCategory.WriteError, null);
					goto IL_197;
				}
			}
			TaskLogger.Trace("No service description", new object[0]);
			IL_197:
			if (this.EventMessageFile != null)
			{
				RegistryKey registryKey2 = null;
				try
				{
					try
					{
						registryKey2 = Registry.LocalMachine.OpenSubKey(ManageService.eventLogRegPath + this.Name, true);
						if (registryKey2 == null)
						{
							registryKey2 = Registry.LocalMachine.CreateSubKey(ManageService.eventLogRegPath + this.Name, RegistryKeyPermissionCheck.ReadWriteSubTree);
						}
						registryKey2.SetValue(ManageService.eventMessageFileSubKeyName, this.EventMessageFile);
						registryKey2.SetValue(ManageService.categoryMessageFileSubKeyName, this.EventMessageFile);
						registryKey2.SetValue(ManageService.categoryCountSubKeyName, this.CategoryCount);
						registryKey2.SetValue(ManageService.typesSupportedSubKeyName, 7);
					}
					catch (SecurityException inner2)
					{
						base.WriteError(new SecurityException(Strings.ErrorOpenKeyDeniedForWrite(ManageService.serviceRegPath + this.Name), inner2), ErrorCategory.WriteError, null);
					}
					goto IL_281;
				}
				finally
				{
					if (registryKey2 != null)
					{
						registryKey2.Close();
						registryKey2 = null;
					}
				}
			}
			TaskLogger.Trace("No event message file", new object[0]);
			IL_281:
			if (base.FirstFailureActionType != ServiceActionType.None)
			{
				base.ConfigureFailureActions();
				base.ConfigureFailureActionsFlag();
			}
			else
			{
				TaskLogger.Trace("No failure actions", new object[0]);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001B668 File Offset: 0x00019868
		protected void Uninstall()
		{
			TaskLogger.LogEnter();
			if (!ServiceControllerUtils.IsInstalled(this.Name))
			{
				base.WriteVerbose(Strings.ServiceNotInstalled(this.Name));
				return;
			}
			base.WriteVerbose(Strings.WillUninstallInstalledService(this.Name));
			try
			{
				this.serviceProcessInstaller.Uninstall(null);
			}
			catch (Win32Exception ex)
			{
				if (ex.NativeErrorCode == 1060)
				{
					this.WriteWarning(Strings.ServiceAlreadyNotInstalled(this.Name));
				}
				else
				{
					base.WriteError(new ServiceUninstallFailureException(this.Name, ex.Message, ex), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (InstallException ex2)
			{
				base.WriteError(new ServiceUninstallFailureException(this.Name, ex2.Message, ex2), ErrorCategory.InvalidOperation, null);
			}
			if (this.serviceFirewallRules.Count > 0)
			{
				using (List<ExchangeFirewallRule>.Enumerator enumerator = this.serviceFirewallRules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ExchangeFirewallRule exchangeFirewallRule = enumerator.Current;
						TaskLogger.Trace("Removing Windows Firewall Rule for Service {0}", new object[]
						{
							this.Name
						});
						exchangeFirewallRule.Remove();
					}
					return;
				}
			}
			string fullPath = this.serviceProcessInstaller.Context.Parameters["assemblypath"];
			TaskLogger.Trace("Removing Service {0} from windows firewall exception", new object[]
			{
				this.Name
			});
			ManageService.RemoveAssemblyFromFirewallExceptions(this.Name, fullPath, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001B7F4 File Offset: 0x000199F4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		protected void LockdownServiceAccess()
		{
			TaskLogger.Trace("Modifying service ACL to remove Network Logon ACE.", new object[0]);
			ServiceAccessFlags serviceAccessFlags = ServiceAccessFlags.ReadControl | ServiceAccessFlags.WriteDac;
			base.DoNativeServiceTask(this.Name, serviceAccessFlags, delegate(IntPtr service)
			{
				string name = this.Name;
				IntPtr intPtr = IntPtr.Zero;
				IntPtr intPtr2 = IntPtr.Zero;
				try
				{
					int num = 65536;
					intPtr = Marshal.AllocHGlobal(num);
					int num2;
					if (!NativeMethods.QueryServiceObjectSecurity(service, SecurityInfos.DiscretionaryAcl, intPtr, num, out num2))
					{
						base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorQueryServiceObjectSecurity(name)), ErrorCategory.InvalidOperation, null);
					}
					byte[] array = new byte[num2];
					Marshal.Copy(intPtr, array, 0, num2);
					RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(array, 0);
					CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, false, rawSecurityDescriptor);
					CommonAce commonAce = null;
					SecurityIdentifier right = new SecurityIdentifier("S-1-5-11");
					for (int i = 0; i < commonSecurityDescriptor.DiscretionaryAcl.Count; i++)
					{
						CommonAce commonAce2 = (CommonAce)commonSecurityDescriptor.DiscretionaryAcl[i];
						if (commonAce2.SecurityIdentifier == right)
						{
							commonAce = commonAce2;
							break;
						}
					}
					if (commonAce == null)
					{
						TaskLogger.Trace("Service ACL was not modified as Network Logon SID is not found.", new object[0]);
					}
					else
					{
						commonSecurityDescriptor.DiscretionaryAcl.RemoveAccess(AccessControlType.Allow, commonAce.SecurityIdentifier, commonAce.AccessMask, commonAce.InheritanceFlags, commonAce.PropagationFlags);
						int binaryLength = commonSecurityDescriptor.BinaryLength;
						byte[] array2 = new byte[binaryLength];
						commonSecurityDescriptor.GetBinaryForm(array2, 0);
						intPtr2 = Marshal.AllocHGlobal(binaryLength);
						Marshal.Copy(array2, 0, intPtr2, binaryLength);
						if (!NativeMethods.SetServiceObjectSecurity(service, SecurityInfos.DiscretionaryAcl, intPtr2))
						{
							base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorSetServiceObjectSecurity(name)), ErrorCategory.InvalidOperation, null);
						}
						TaskLogger.Trace("Service ACL modified - Network Logon ACE removed.", new object[0]);
					}
				}
				finally
				{
					if (IntPtr.Zero != intPtr)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (IntPtr.Zero != intPtr2)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
			});
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001BA00 File Offset: 0x00019C00
		protected void AddFirewallRule(ExchangeFirewallRule firewallRule)
		{
			this.serviceFirewallRules.Add(firewallRule);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001BA10 File Offset: 0x00019C10
		private static void RunNetShProcess(string args, Task.TaskErrorLoggingDelegate errorHandler)
		{
			string text = null;
			string text2 = null;
			try
			{
				ProcessRunner.Run(ManageService.NetshExe, args, -1, null, out text, out text2);
			}
			catch (Win32Exception exception)
			{
				errorHandler(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (System.TimeoutException exception2)
			{
				errorHandler(exception2, ErrorCategory.OperationTimeout, null);
			}
			catch (InvalidOperationException exception3)
			{
				errorHandler(exception3, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x040001A8 RID: 424
		protected static readonly string descriptionSubKeyName = "Description";

		// Token: 0x040001A9 RID: 425
		protected static readonly string eventMessageFileSubKeyName = "EventMessageFile";

		// Token: 0x040001AA RID: 426
		protected static readonly string categoryMessageFileSubKeyName = "CategoryMessageFile";

		// Token: 0x040001AB RID: 427
		protected static readonly string categoryCountSubKeyName = "CategoryCount";

		// Token: 0x040001AC RID: 428
		protected static readonly string typesSupportedSubKeyName = "TypesSupported";

		// Token: 0x040001AD RID: 429
		protected static readonly string serviceRegPath = "SYSTEM\\CurrentControlSet\\Services\\";

		// Token: 0x040001AE RID: 430
		protected static readonly string eventLogRegPath = "SYSTEM\\CurrentControlSet\\Services\\EventLog\\Application\\";

		// Token: 0x040001AF RID: 431
		private readonly List<ExchangeFirewallRule> serviceFirewallRules;

		// Token: 0x040001B0 RID: 432
		private static string NetshExe = Path.Combine(Environment.SystemDirectory, "netsh.exe");

		// Token: 0x040001B1 RID: 433
		private string description;

		// Token: 0x040001B2 RID: 434
		private string eventMessageFile;

		// Token: 0x040001B3 RID: 435
		private int categoryCount;

		// Token: 0x040001B4 RID: 436
		private ServiceInstaller serviceInstaller;

		// Token: 0x040001B5 RID: 437
		private ServiceProcessInstaller serviceProcessInstaller;
	}
}
