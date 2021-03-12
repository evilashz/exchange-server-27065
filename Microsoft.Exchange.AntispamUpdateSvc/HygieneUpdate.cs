using System;
using System.Runtime.InteropServices;
using Interop.WUApiLib;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.AntispamUpdate
{
	// Token: 0x02000002 RID: 2
	internal class HygieneUpdate
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public HygieneUpdate(bool consoleDiagnostics)
		{
			this.consoleDiagnostics = consoleDiagnostics;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public bool ConsoleDiagnostics
		{
			get
			{
				return this.consoleDiagnostics;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		public void DoUpdate()
		{
			if (this.ConsoleDiagnostics)
			{
				Console.WriteLine("Checking for Updates.");
			}
			try
			{
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StartScan, null, new string[0]);
				IUpdateSession updateSession = new UpdateSessionClass();
				updateSession.ClientApplicationID = "Exchange12";
				IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();
				ISearchResult searchResult = updateSearcher.Search("IsInstalled=0 and CategoryIDs contains 'ab62c5bd-5539-49f6-8aea-5a114dd42314'");
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StopScan, null, new string[0]);
				if (searchResult.Updates.Count == 0)
				{
					if (this.ConsoleDiagnostics)
					{
						Console.WriteLine("No Updates.");
					}
				}
				else
				{
					foreach (object obj in searchResult.Updates)
					{
						IUpdate update = (IUpdate)obj;
						UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_PatchAvailable, null, new string[]
						{
							update.Title
						});
						if (this.ConsoleDiagnostics)
						{
							Console.WriteLine("Title: {0}", update.Title);
						}
					}
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StartDownload, null, new string[0]);
					UpdateDownloader updateDownloader = updateSession.CreateUpdateDownloader();
					updateDownloader.Priority = 3;
					updateDownloader.Updates = searchResult.Updates;
					IDownloadResult downloadResult = updateDownloader.Download();
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StopDownload, null, new string[0]);
					for (int i = 0; i < updateDownloader.Updates.Count; i++)
					{
						if (downloadResult.GetUpdateResult(i).ResultCode == 4 || downloadResult.GetUpdateResult(i).ResultCode == 5)
						{
							UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_DownloadFailure, null, new string[]
							{
								updateDownloader.Updates[i].Title,
								downloadResult.GetUpdateResult(i).HResult.ToString()
							});
							if (this.ConsoleDiagnostics)
							{
								Console.WriteLine("Errors: {0}: {1}", updateDownloader.Updates[i].Title, downloadResult.GetUpdateResult(i).HResult);
							}
						}
					}
					bool flag = false;
					foreach (object obj2 in updateDownloader.Updates)
					{
						IUpdate update2 = (IUpdate)obj2;
						if (update2.IsDownloaded)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (this.ConsoleDiagnostics)
						{
							Console.WriteLine("Nothing to Install.");
						}
					}
					else
					{
						if (this.ConsoleDiagnostics)
						{
							Console.WriteLine("Starting Installation.");
						}
						UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StartInstall, null, new string[0]);
						IUpdateInstaller updateInstaller = updateSession.CreateUpdateInstaller();
						updateInstaller.Updates = updateDownloader.Updates;
						IInstallationResult installationResult = updateInstaller.Install();
						UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_StopInstall, null, new string[0]);
						for (int j = 0; j < updateInstaller.Updates.Count; j++)
						{
							IUpdate update3 = updateInstaller.Updates[j];
							if (installationResult.GetUpdateResult(j).ResultCode == 4 || installationResult.GetUpdateResult(j).ResultCode == 3)
							{
								UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_InstallFailure, null, new string[]
								{
									updateInstaller.Updates[j].Title,
									installationResult.GetUpdateResult(j).HResult.ToString()
								});
								if (this.ConsoleDiagnostics)
								{
									Console.WriteLine("Errors: {0}: {1}", updateInstaller.Updates[j].Title, installationResult.GetUpdateResult(j).HResult);
								}
							}
						}
						if (this.ConsoleDiagnostics)
						{
							Console.WriteLine("Finished.");
						}
					}
				}
			}
			catch (COMException ex)
			{
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_WuaFail, null, new string[]
				{
					ex.Message
				});
				if (this.ConsoleDiagnostics)
				{
					Console.WriteLine("Failed: {0}", ex.Message);
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002528 File Offset: 0x00000728
		public OptInStatus SetMicrosoftUpdateOptinStatus(OptInStatus optIn, string cabLocation)
		{
			bool flag = optIn == OptInStatus.RequestDisabled || optIn == OptInStatus.RequestNotifyDownload || optIn == OptInStatus.RequestNotifyInstall || optIn == OptInStatus.RequestScheduled;
			try
			{
				IWindowsUpdateAgentInfo windowsUpdateAgentInfo = new WindowsUpdateAgentInfoClass();
				string text = windowsUpdateAgentInfo.GetInfo("ProductVersionString") as string;
				if (string.IsNullOrEmpty(text))
				{
					if (flag)
					{
						UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOpt, null, new string[0]);
					}
					if (this.ConsoleDiagnostics)
					{
						Console.WriteLine("Unable to determine WUA Version");
					}
					return OptInStatus.NotConfigured;
				}
				Version v = new Version(text);
				if (v < HygieneUpdate.AgentMinVersion)
				{
					if (flag)
					{
						UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOpt, null, new string[0]);
					}
					if (this.ConsoleDiagnostics)
					{
						Console.WriteLine("Out of date client");
					}
					return OptInStatus.NotConfigured;
				}
			}
			catch (COMException)
			{
				if (flag)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOpt, null, new string[0]);
				}
				if (this.ConsoleDiagnostics)
				{
					Console.WriteLine("Unable to determine Windows Update Agent version");
				}
				return OptInStatus.NotConfigured;
			}
			try
			{
				IUpdateServiceManager updateServiceManager = new UpdateServiceManagerClass();
				foreach (object obj in updateServiceManager.Services)
				{
					IUpdateService updateService = (IUpdateService)obj;
					if (this.ConsoleDiagnostics)
					{
						Console.WriteLine("Service {0}: {1} {2}", updateService.ServiceID, updateService.IsRegisteredWithAU, updateService.IsManaged);
					}
					if ((string.Compare("7971f918-a847-4430-9279-4a52d1efe18d", updateService.ServiceID, StringComparison.OrdinalIgnoreCase) == 0 && updateService.IsRegisteredWithAU) || updateService.IsManaged)
					{
						if (this.ConsoleDiagnostics)
						{
							Console.WriteLine("Microsoft Update already registered.");
						}
						return OptInStatus.Configured;
					}
				}
				if (!flag)
				{
					return OptInStatus.NotConfigured;
				}
				if (string.IsNullOrEmpty(cabLocation))
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOpt, null, new string[0]);
					if (this.ConsoleDiagnostics)
					{
						Console.WriteLine("Invalid CAB location");
					}
				}
				updateServiceManager.AddService("7971f918-a847-4430-9279-4a52d1efe18d", cabLocation);
				updateServiceManager.RegisterServiceWithAU("7971f918-a847-4430-9279-4a52d1efe18d");
				IAutomaticUpdates automaticUpdates = new AutomaticUpdatesClass();
				if (!automaticUpdates.Settings.ReadOnly && automaticUpdates.Settings.NotificationLevel == null)
				{
					automaticUpdates.Settings.NotificationLevel = optIn;
					automaticUpdates.Settings.ScheduledInstallationDay = 0;
					automaticUpdates.Settings.Save();
				}
			}
			catch (COMException ex)
			{
				if (flag)
				{
					UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuOptFail, null, new string[]
					{
						ex.Message
					});
				}
				if (this.ConsoleDiagnostics)
				{
					Console.WriteLine("Failed: {0}", ex.Message);
				}
				return OptInStatus.NotConfigured;
			}
			catch (InvalidCastException ex2)
			{
				UpdateSvcEventLogger.LogEvent(AntispamUpdateServiceEventLogConstants.Tuple_MuGetFail, null, new string[]
				{
					ex2.Message
				});
				if (this.ConsoleDiagnostics)
				{
					Console.WriteLine("Failed: {0}", ex2.Message);
				}
				return OptInStatus.NotConfigured;
			}
			return OptInStatus.Configured;
		}

		// Token: 0x04000001 RID: 1
		private const string MicrosoftUpdateGUID = "7971f918-a847-4430-9279-4a52d1efe18d";

		// Token: 0x04000002 RID: 2
		private const string AgentMinVersionStr = "5.8.0.2469";

		// Token: 0x04000003 RID: 3
		private const string AgentInfo = "ProductVersionString";

		// Token: 0x04000004 RID: 4
		private const string ApplicationId = "Exchange12";

		// Token: 0x04000005 RID: 5
		private const string UpdateQuery = "IsInstalled=0 and CategoryIDs contains 'ab62c5bd-5539-49f6-8aea-5a114dd42314'";

		// Token: 0x04000006 RID: 6
		private readonly bool consoleDiagnostics;

		// Token: 0x04000007 RID: 7
		private static readonly Version AgentMinVersion = new Version("5.8.0.2469");
	}
}
