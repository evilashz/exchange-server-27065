using System;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Filtering;
using Microsoft.Forefront.ActiveDirectoryConnector.Events;
using Microsoft.Internal.ManagedWPP;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.ActiveDirectoryConnector
{
	// Token: 0x02000004 RID: 4
	[Guid("48527140-FBF1-4BDB-8BDD-D2A76A3B8C4B")]
	[ComVisible(true)]
	public class ADFilteringSettingsWatcher : IADFilteringSettingsWatcher
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		private static ITopologyConfigurationSession SharedAdConfigurationSession
		{
			get
			{
				if (ADFilteringSettingsWatcher.adConfigurationSession == null)
				{
					lock (ADFilteringSettingsWatcher.protectionObject)
					{
						if (ADFilteringSettingsWatcher.adConfigurationSession == null)
						{
							ADFilteringSettingsWatcher.adConfigurationSession = ADHelpers.CreateDefaultReadOnlyTopologyConfigurationSession();
						}
					}
				}
				return ADFilteringSettingsWatcher.adConfigurationSession;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002128 File Offset: 0x00000328
		public void Start()
		{
			try
			{
				if (!this.RegisterConfigurationChangeHandlers())
				{
					throw new ApplicationException("Could not setup AD Change Handler");
				}
				this.ServerConfigUpdate(null);
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 10, this.GetHashCode());
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherStarted, null, null);
			}
			catch (Exception ex)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 11, this.GetHashCode(), TraceProvider.MakeStringArg(ex.ToString()));
				}
				EventNotificationItem.Publish(ExchangeComponent.AMADError.Name, "FIPS.ADFilteringSettingsWatcherStartFailed", null, ex.ToString(), ResultSeverityLevel.Error, false);
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherStartException, null, new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002218 File Offset: 0x00000418
		public void Stop()
		{
			try
			{
				this.UnRegisterConfigurationChangeHandlers();
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 12, this.GetHashCode());
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherStopped, null, null);
			}
			catch (Exception ex)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 13, this.GetHashCode(), TraceProvider.MakeStringArg(ex.ToString()));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherStopException, null, new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022D4 File Offset: 0x000004D4
		public int GetProcessId()
		{
			int result;
			try
			{
				int id;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					id = currentProcess.Id;
				}
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 14, this.GetHashCode(), TraceProvider.MakeStringArg(id.ToString()));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherProcessId, null, new object[]
				{
					id
				});
				result = id;
			}
			catch (Exception ex)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 15, this.GetHashCode(), TraceProvider.MakeStringArg(ex.ToString()));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherGetProcessIdException, null, new object[]
				{
					ex
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002484 File Offset: 0x00000684
		private bool RegisterConfigurationChangeHandlers()
		{
			if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
			{
				WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 16, this.GetHashCode());
			}
			if (this.serverNotificationCookie != null)
			{
				return true;
			}
			ADOperationResult adoperationResult;
			Server server = this.ReadServerConfig(out adoperationResult);
			if (server != null)
			{
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					this.serverNotificationCookie = ADNotificationAdapter.RegisterChangeNotification<Server>(server.Id, new ADNotificationCallback(this.ServerConfigUpdate));
					if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
					{
						WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 17, this.GetHashCode(), TraceProvider.MakeStringArg(server.Id.ToString()));
					}
					ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherChangeHandlersRegistered, null, new object[]
					{
						server.Id
					});
				});
				return adoperationResult.Succeeded;
			}
			if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
			{
				WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 18, this.GetHashCode());
			}
			ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherRegisterConfigurationChangeHandlersReadServerConfigFailed, null, null);
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002550 File Offset: 0x00000750
		private void UnRegisterConfigurationChangeHandlers()
		{
			ADNotificationListener.Stop();
			if (this.serverNotificationCookie != null)
			{
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 19, this.GetHashCode());
				}
				ADNotificationAdapter.UnregisterChangeNotification(this.serverNotificationCookie);
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherChangeHandlersUnRegistered, null, null);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025B4 File Offset: 0x000007B4
		private void ServerConfigUpdate(ADNotificationEventArgs args)
		{
			try
			{
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 20, this.GetHashCode());
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherServerConfigUpdateNotification, null, null);
				ADOperationResult adoperationResult;
				Server server = this.ReadServerConfig(out adoperationResult);
				if (server != null)
				{
					FilteringSettings filteringSettings = this.filteringSettings;
					if (this.GetFilteringSettingsFromServerConfig(server))
					{
						if (!this.filteringSettings.Equals(filteringSettings) || args == null)
						{
							this.SetFilteringSettingsToFips();
						}
						else
						{
							ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherServerConfigUpdateNoChanges, null, null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 21, this.GetHashCode(), TraceProvider.MakeStringArg(ex.ToString()));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherServerConfigUpdateException, null, new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026F0 File Offset: 0x000008F0
		private Server ReadServerConfig(out ADOperationResult opResult)
		{
			if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
			{
				WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_i(1, 22, this.GetHashCode());
			}
			Server result = null;
			if (!ADNotificationAdapter.TryReadConfiguration<Server>(delegate()
			{
				Server result2;
				try
				{
					result2 = ADFilteringSettingsWatcher.SharedAdConfigurationSession.FindLocalServer();
				}
				catch (LocalServerNotFoundException)
				{
					result2 = null;
				}
				return result2;
			}, out result, out opResult))
			{
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherReadServerConfigFailed, null, null);
				return null;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002768 File Offset: 0x00000968
		private bool GetFilteringSettingsFromServerConfig(Server server)
		{
			try
			{
				this.filteringSettings.MalwareFilteringUpdateFrequency = (int)server[ServerSchema.MalwareFilteringUpdateFrequency];
				this.filteringSettings.MalwareFilteringUpdateTimeout = (int)server[ServerSchema.MalwareFilteringUpdateTimeout];
				this.filteringSettings.MalwareFilteringPrimaryUpdatePath = (string)server[ServerSchema.MalwareFilteringPrimaryUpdatePath];
				this.filteringSettings.MalwareFilteringSecondaryUpdatePath = (string)server[ServerSchema.MalwareFilteringSecondaryUpdatePath];
			}
			catch (InvalidCastException ex)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 23, this.GetHashCode(), TraceProvider.MakeStringArg(ex.ToString()));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherGetFilteringSettingsFromServerConfigException, null, new object[]
				{
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002850 File Offset: 0x00000A50
		private bool SetFilteringSettingsToFips()
		{
			RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
			PSSnapInException ex = null;
			runspaceConfiguration.AddPSSnapIn("Microsoft.Forefront.Filtering.Management.PowerShell", out ex);
			if (ex != null)
			{
				if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 24, this.GetHashCode(), TraceProvider.MakeStringArg(ex.Message));
				}
				ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherServerConfigUpdateErrorAddingSnapin, null, new object[]
				{
					ex.Message
				});
				return false;
			}
			using (Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration))
			{
				runspace.Open();
				using (Pipeline pipeline = runspace.CreatePipeline())
				{
					Command command = new Command("Set-EngineUpdateCommonSettings");
					command.Parameters.Add("EngineDownloadTimeout", this.filteringSettings.MalwareFilteringUpdateTimeout);
					command.Parameters.Add("PrimaryUpdatePath", this.filteringSettings.MalwareFilteringPrimaryUpdatePath);
					command.Parameters.Add("SecondaryUpdatePath", this.filteringSettings.MalwareFilteringSecondaryUpdatePath);
					command.Parameters.Add("UpdateFrequency", TimeSpan.FromMinutes((double)this.filteringSettings.MalwareFilteringUpdateFrequency));
					pipeline.Commands.Add(command);
					try
					{
						pipeline.Invoke();
						if (Tracing.tracer.Level >= 5 && (Tracing.tracer.Flags & 2048) != 0)
						{
							WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_is(1, 25, this.GetHashCode(), TraceProvider.MakeStringArg(command.ToString()));
						}
						ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherSetFilteringSettingsToFips, null, new object[]
						{
							this.filteringSettings.MalwareFilteringUpdateTimeout,
							this.filteringSettings.MalwareFilteringPrimaryUpdatePath,
							this.filteringSettings.MalwareFilteringSecondaryUpdatePath,
							TimeSpan.FromMinutes((double)this.filteringSettings.MalwareFilteringUpdateFrequency)
						});
					}
					catch (RuntimeException ex2)
					{
						if (Tracing.tracer.Level >= 2 && (Tracing.tracer.Flags & 2048) != 0)
						{
							WPP_1cd3dee55f704f6905d1e53a161baad7.WPP_iss(1, 26, this.GetHashCode(), TraceProvider.MakeStringArg(command.ToString()), TraceProvider.MakeStringArg(ex2.ToString()));
						}
						ADFilteringSettingsWatcher.eventLogger.LogEvent(ADConnectorEventLogConstants.Tuple_ADFilteringSettingWatcherServerConfigUpdateErrorSettingFilteringServiceSettings, null, new object[]
						{
							command,
							ex2
						});
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000005 RID: 5
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ADConnectorTracer.Category, "Filtering ADConnector");

		// Token: 0x04000006 RID: 6
		private static ITopologyConfigurationSession adConfigurationSession;

		// Token: 0x04000007 RID: 7
		private static object protectionObject = new object();

		// Token: 0x04000008 RID: 8
		private ADNotificationRequestCookie serverNotificationCookie;

		// Token: 0x04000009 RID: 9
		private FilteringSettings filteringSettings;
	}
}
