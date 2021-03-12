using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.ServiceHost.Messages;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000002 RID: 2
	internal abstract class ServiceHostBase : ExServiceBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D7 File Offset: 0x000002D7
		protected static Guid ComponentGuid { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E6 File Offset: 0x000002E6
		protected static ExEventLog Log { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020EE File Offset: 0x000002EE
		internal ServiceHostBase(string serviceName, ModuleMap[] modules)
		{
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.ServiceName = serviceName;
			this.moduleMap = modules;
			base.AutoLog = false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002130 File Offset: 0x00000330
		private static bool IsRunningAsService()
		{
			bool flag = false;
			bool flag2 = false;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				IdentityReferenceCollection groups = current.Groups;
				foreach (IdentityReference identityReference in groups)
				{
					if (identityReference is SecurityIdentifier)
					{
						SecurityIdentifier securityIdentifier = (SecurityIdentifier)identityReference;
						if (securityIdentifier.IsWellKnown(WellKnownSidType.ServiceSid))
						{
							flag = true;
						}
						if (securityIdentifier.IsWellKnown(WellKnownSidType.InteractiveSid))
						{
							flag2 = true;
						}
					}
				}
			}
			return flag || (!flag && !flag2);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
		internal static void MainEntry(ServiceHostBase svc, string[] args)
		{
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeTcbPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeSecurityPrivilege",
				"SeImpersonatePrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			Globals.InitializeMultiPerfCounterInstance("EMS");
			bool flag = false;
			bool flag2 = true;
			foreach (string text in args)
			{
				string a = text.ToLower();
				if (a == "-console")
				{
					flag = true;
				}
				else
				{
					if (!(a == "-noprompt"))
					{
						Console.WriteLine("invalid options\n");
						return;
					}
					flag2 = false;
				}
			}
			flag |= !ServiceHostBase.IsRunningAsService();
			if (flag)
			{
				if (flag2)
				{
					Console.WriteLine("Hit <enter\t> to start...");
				}
				Console.ReadLine();
			}
			if (flag)
			{
				ExServiceBase.RunAsConsole(svc);
				return;
			}
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (!current.IsSystem)
				{
					ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_NotRunningAsLocalSystem, string.Empty, new object[0]);
					Environment.Exit(5);
				}
			}
			ServiceBase.Run(svc);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000231C File Offset: 0x0000051C
		protected virtual void InitializeThrottling()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002320 File Offset: 0x00000520
		private void GetInstalledRoles(out ServerRole installedRoles, out RunInExchangeMode exchangeModeThisServer)
		{
			exchangeModeThisServer = RunInExchangeMode.None;
			installedRoles = ServerRole.None;
			Exception ex = null;
			try
			{
				if (Datacenter.IsMicrosoftHostedOnly(true))
				{
					exchangeModeThisServer = RunInExchangeMode.ExchangeDatacenter;
				}
				else if (Datacenter.IsDatacenterDedicated(true))
				{
					exchangeModeThisServer = RunInExchangeMode.DatacenterDedicated;
				}
				else
				{
					exchangeModeThisServer = RunInExchangeMode.Enterprise;
				}
			}
			catch (CannotDetermineExchangeModeException ex2)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_UnhandledException, string.Empty, new object[]
				{
					ex2.Message
				});
			}
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 268, "GetInstalledRoles", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Service\\Common\\ServiceHostBase.cs");
				Server server = topologyConfigurationSession.FindLocalServer();
				installedRoles = server.CurrentServerRole;
			}
			catch (LocalServerNotFoundException ex3)
			{
				ex = ex3;
			}
			catch (ADTransientException ex4)
			{
				ex = ex4;
			}
			catch (ADExternalException ex5)
			{
				ex = ex5;
			}
			catch (DataValidationException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_UnhandledException, string.Empty, new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000243C File Offset: 0x0000063C
		private void LoadModules(bool wasCalledFromOnStart)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.Location;
			text = Path.GetDirectoryName(text);
			bool isRunningAsService = ServiceHostBase.IsRunningAsService();
			this.servicelets = new List<Servicelet>();
			RunInExchangeMode runInExchangeMode;
			this.GetInstalledRoles(out this.serverRole, out runInExchangeMode);
			if (this.serverRole == ServerRole.None)
			{
				return;
			}
			if ((ServerRole.Mailbox & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_MailboxRoleInstalled, string.Empty, new object[0]);
			}
			if ((ServerRole.ClientAccess & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ClientAccessRoleInstalled, string.Empty, new object[0]);
			}
			if ((ServerRole.UnifiedMessaging & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_UnifiedMessagingRoleInstalled, string.Empty, new object[0]);
			}
			if ((ServerRole.HubTransport & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_HubTransportRoleInstalled, string.Empty, new object[0]);
			}
			if ((ServerRole.Edge & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_EdgeTransportRoleInstalled, string.Empty, new object[0]);
			}
			if ((ServerRole.Cafe & this.serverRole) != ServerRole.None)
			{
				ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_CafeRoleInstalled, string.Empty, new object[0]);
			}
			foreach (ModuleMap moduleMap in this.moduleMap)
			{
				if ((moduleMap.Role & this.serverRole) != ServerRole.None && (moduleMap.ExcludeIfRole & this.serverRole) == ServerRole.None && (moduleMap.RunInExchangeMode & runInExchangeMode) == runInExchangeMode)
				{
					if (!moduleMap.IsEnabled)
					{
						this.LogExWatsonTimeoutCmdletStateChange(string.Format("skipping module {0}", moduleMap.ModuleName), null);
					}
					else
					{
						if (wasCalledFromOnStart)
						{
							base.ExRequestAdditionalTime((int)TimeSpan.FromSeconds(30.0).TotalMilliseconds);
						}
						ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_Loading, string.Empty, new object[]
						{
							moduleMap.ModuleName
						});
						try
						{
							Assembly assembly = Assembly.LoadFrom(Path.Combine(text, moduleMap.ModuleName));
							Servicelet servicelet = (Servicelet)assembly.CreateInstance(moduleMap.TypeName);
							servicelet.SetRoleInfo(this.serverRole, isRunningAsService);
							servicelet.TypeName = moduleMap.TypeName;
							servicelet.ModuleName = moduleMap.ModuleName;
							this.servicelets.Add(servicelet);
						}
						catch (FileNotFoundException)
						{
							ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ErrorLoadingNotFound, string.Empty, new object[]
							{
								moduleMap.ModuleName
							});
						}
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026E8 File Offset: 0x000008E8
		private void LoadAndStartServicelets(bool wasCalledFromOnStart)
		{
			lock (this)
			{
				this.LogExWatsonTimeoutCmdletStateChange("Called LoadAndStartServicelets()", null);
				this.LoadModules(wasCalledFromOnStart);
				this.InitializeThrottling();
				if (this.serverRole == ServerRole.None)
				{
					ThreadPool.QueueUserWorkItem(delegate(object unused)
					{
						Thread.Sleep(this.startupRetry);
						this.LoadAndStartServicelets(false);
					});
					if (this.startupRetry < TimeSpan.FromMinutes(16.0))
					{
						this.startupRetry += this.startupRetry;
					}
				}
				else
				{
					foreach (Servicelet servicelet in this.servicelets)
					{
						if (wasCalledFromOnStart)
						{
							base.ExRequestAdditionalTime((int)TimeSpan.FromSeconds(30.0).TotalMilliseconds);
						}
						this.LogExWatsonTimeoutCmdletStateChange("Calling OnStart()", servicelet);
						servicelet.OnStart();
					}
					this.LogExWatsonTimeoutCmdletStateChange("OnStart for Servicelets complete.", null);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000280C File Offset: 0x00000A0C
		private void StopAndUnloadServicelets()
		{
			lock (this)
			{
				foreach (Servicelet servicelet in this.servicelets)
				{
					this.LogExWatsonTimeoutCmdletStateChange("Calling OnStop()", servicelet);
					servicelet.OnStop();
				}
				foreach (Servicelet servicelet2 in this.servicelets)
				{
					base.ExRequestAdditionalTime((int)TimeSpan.FromSeconds(30.0).TotalMilliseconds);
					this.LogExWatsonTimeoutCmdletStateChange("Calling Join()", servicelet2);
					servicelet2.Join();
				}
				this.LogExWatsonTimeoutCmdletStateChange("Completed StopAndUnloadServicelets()", null);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002908 File Offset: 0x00000B08
		private void LogExWatsonTimeoutCmdletStateChange(string action, Servicelet servicelet)
		{
			string info;
			if (servicelet == null)
			{
				info = action;
			}
			else
			{
				info = string.Format("Module: '{0}' Type: '{1}' : '{2}'", servicelet.ModuleName, servicelet.TypeName, string.IsNullOrEmpty(action) ? string.Empty : action);
			}
			base.LogExWatsonTimeoutServiceStateChangeInfo(info);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000294C File Offset: 0x00000B4C
		protected override void OnStartInternal(string[] args)
		{
			base.ExRequestAdditionalTime((int)TimeSpan.FromMinutes(4.0).TotalMilliseconds);
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ServiceStarting, string.Empty, null);
			ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents();
			try
			{
				ADSession.DisableAdminTopologyMode();
				ADSession.GetCurrentConfigDCForLocalForest();
			}
			catch (ADTransientException)
			{
			}
			this.LoadAndStartServicelets(true);
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ServiceStarted, string.Empty, null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000029D0 File Offset: 0x00000BD0
		protected override void OnStopInternal()
		{
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ServiceStopping, string.Empty, null);
			ExchangeDiagnosticsHelper.UnRegisterDiagnosticsComponents();
			this.StopAndUnloadServicelets();
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_ServiceStopped, string.Empty, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002A0C File Offset: 0x00000C0C
		protected override void OnCustomCommandInternal(int command)
		{
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_HandlingCustomCommand, string.Empty, new object[]
			{
				command
			});
			lock (this)
			{
				foreach (Servicelet servicelet in this.servicelets)
				{
					servicelet.OnCustomCommand(command);
				}
			}
			ServiceHostBase.Log.LogEvent(MSExchangeServiceHostEventLogConstants.Tuple_CustomCommandHandled, string.Empty, new object[]
			{
				command
			});
		}

		// Token: 0x04000001 RID: 1
		private readonly ModuleMap[] moduleMap;

		// Token: 0x04000002 RID: 2
		private TimeSpan startupRetry = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000003 RID: 3
		private ServerRole serverRole;

		// Token: 0x04000004 RID: 4
		private List<Servicelet> servicelets;
	}
}
