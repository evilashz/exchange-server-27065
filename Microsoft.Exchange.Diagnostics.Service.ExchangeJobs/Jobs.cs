using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Extensions.Auditing;
using Microsoft.Exchange.LogAnalyzer.Extensions.CmdletInfraLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.EwsLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.GroupEscalationLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.HttpProxyLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.HxServiceLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.HxServiceLog.EventLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.IisLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.MigrationLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.OAuthCafeLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.OutlookServiceLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.OwaLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Perflog;
using Microsoft.Exchange.LogAnalyzer.Extensions.PFAssistantLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.Rca;
using Microsoft.Exchange.LogAnalyzer.Extensions.Store;
using Microsoft.Exchange.LogAnalyzer.Extensions.TransportSyncHealthLog;
using Microsoft.ExLogAnalyzer;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs
{
	// Token: 0x0200002D RID: 45
	public class Jobs
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000723C File Offset: 0x0000543C
		public static Job NewEventLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating event log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			List<EventLogQueryParams> list = new List<EventLogQueryParams>();
			int num = 0;
			string configString;
			while ((configString = Configuration.GetConfigString("EventLogQueryParams_" + num.ToString(), null)) != null)
			{
				list.Add(EventLogQueryParams.CreateFromLine(configString));
				num++;
			}
			if (list.Count == 0)
			{
				Logger.LogErrorMessage("No event log queried defined, EventLogJob will not be added.", new object[0]);
				return null;
			}
			TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan("EventLogQueryParams_TimerDuration", TimeSpan.FromSeconds(5.0), TimeSpan.FromDays(1.0), TimeSpan.FromMinutes(5.0));
			Watermark watermark = watermarks.Get(jobName);
			LogEventLogSource logSource = new LogEventLogSource(jobName, list, configTimeSpan, new DateTime?(watermark.Timestamp));
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "CsvPassThrough", stream, watermark, watermarks.Directory);
			CsvPassThroughExtension extension = new CsvPassThroughExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created event log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007348 File Offset: 0x00005548
		public static Job NewIisHttpErrLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating IIS HTTP ERR log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text;
			bool flag = IisLogUtils.TryGetHttpErrLogPath(ref text, Jobs.GetSiteDefaultsLogDirectory("traceFailedRequestsLogging"));
			Logger.LogInformationMessage("Path to the Http Error logs is '{0}'", new object[]
			{
				(text == null) ? "null" : text
			});
			if (!flag)
			{
				Logger.LogErrorMessage("Unable to read path to the Http Error logs. IisHttpErrLogJob will not be added.", new object[0]);
				return null;
			}
			Watermark watermark = watermarks.Get(jobName);
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogIisSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "IisLog", stream, watermark, watermarks.Directory);
			IisLogExtension extension = new IisLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created IIS HTTP ERR log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007438 File Offset: 0x00005638
		public static Job NewIisLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating IIS log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = string.Empty;
			if ("v15".Equals("v14", StringComparison.OrdinalIgnoreCase))
			{
				text = "Default Web Site";
			}
			else if ("v15".Equals("v15", StringComparison.OrdinalIgnoreCase))
			{
				text = "Exchange Back End";
			}
			string text2;
			bool flag = IisLogUtils.TryGetIisLogPath(text, ref text2, Jobs.GetSiteDefaultsLogDirectory("logFile"));
			Logger.LogInformationMessage("Path to Iis logs is '{0}'", new object[]
			{
				(text2 == null) ? "null" : text2
			});
			if (!flag)
			{
				Logger.LogErrorMessage("Unable to read path to the IIS logs. IisLogJob will not be added.", new object[0]);
				return null;
			}
			Watermark watermark = watermarks.Get(jobName);
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogIisSchema(), null, text2, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "IisLog", stream, watermark, watermarks.Directory);
			IisLogExtension extension = new IisLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created IIS log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007560 File Offset: 0x00005760
		public static Job NewOwaLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating OWA log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			string configStringValue = AppConfigLoader.GetConfigStringValue("OWALogPath", Path.Combine(ExchangeSetupContext.LoggingPath, "OWA\\Server"));
			if (!Directory.Exists(configStringValue))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					configStringValue
				});
				Log.CreateLogDirectory(configStringValue);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, configStringValue, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "OwaLog", stream, watermark, watermarks.Directory);
			OwaLogExtension extension = new OwaLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created OWA log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007644 File Offset: 0x00005844
		public static Job NewRcaLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating Rca log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			string configStringValue = AppConfigLoader.GetConfigStringValue("RcaLogPath", Path.Combine(ExchangeSetupContext.LoggingPath, "RPC Client Access"));
			if (!Directory.Exists(configStringValue))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					configStringValue
				});
				Log.CreateLogDirectory(configStringValue);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, configStringValue, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "RcaLog", stream, watermark, watermarks.Directory);
			RcaLogExtension extension = new RcaLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Rca log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007728 File Offset: 0x00005928
		public static Job NewOwaClientLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating OWA Client log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			string configStringValue = AppConfigLoader.GetConfigStringValue("OWALogPath", Path.Combine(ExchangeSetupContext.LoggingPath, "OWA\\Client"));
			if (!Directory.Exists(configStringValue))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					configStringValue
				});
				Log.CreateLogDirectory(configStringValue);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, configStringValue, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "OwaClientLog", stream, watermark, watermarks.Directory);
			OwaLogExtension extension = new OwaLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created OWA Client log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000780C File Offset: 0x00005A0C
		public static Job NewEwsLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating Ews log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "EWS");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogEwsSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "EwsLog", stream, watermark, watermarks.Directory);
			EwsLogExtension extension = new EwsLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Ews log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000078E4 File Offset: 0x00005AE4
		public static Job NewHxServiceLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating HxService log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "OutlookServiceRequest");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogHxServiceSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "HxServiceLog", stream, watermark, watermarks.Directory);
			HxServiceLogExtension extension = new HxServiceLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created HxService log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000079CC File Offset: 0x00005BCC
		public static Job NewHxServiceEventLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating HxService EventLog job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "OutlookServiceEvent");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "HxServiceEventLog", stream, watermark, watermarks.Directory);
			HxServiceEventLogExtension extension = new HxServiceEventLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created HxService EventLog job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007B44 File Offset: 0x00005D44
		public static Job NewNetworkOpticsLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating NetworkOpticsLog job.", new object[0]);
			if (stream == null)
			{
				Logger.LogErrorMessage("No NetworkOpticsLog output stream, NetworkOpticsLog Job will not be added.", new object[0]);
				return null;
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			Watermark watermark = watermarks.Get(jobName);
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "NetworkOpticsProvider\\NetworkPerfMonitoringData");
			bool configBool = Configuration.GetConfigBool("PerformProcessedFileAction", false);
			if (!Directory.Exists(text))
			{
				Logger.LogErrorMessage("Unable to find the Directory for NetworkOptics logs. NetworkOpticsLogJob will not be added.", new object[0]);
				return null;
			}
			Action<string> action = delegate(string filePath)
			{
				if (string.IsNullOrEmpty(filePath))
				{
					return;
				}
				try
				{
					File.Delete(filePath);
					Log.LogInformationMessage("Deleted processed file '{0}'.", new object[]
					{
						filePath
					});
				}
				catch (IOException ex)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an IO exception: {1}", new object[]
					{
						filePath,
						ex
					});
				}
				catch (UnauthorizedAccessException ex2)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an unauthorized access exception: {1}", new object[]
					{
						filePath,
						ex2
					});
				}
			};
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogPerfLogSchema(), null, text, "*.csv", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 6, configBool ? action : null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "PerfLog", stream, watermark, watermarks.Directory);
			PerfLogExtension extension = new PerfLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			return singleStreamJob;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static Job NewTransportSyncHealthMailboxLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			string sourceFolderPath = Path.Combine(ExchangeSetupContext.InstallPath, "TransportRoles\\Logs\\SyncHealth\\Mailbox");
			return Jobs.NewTransportSyncHealthLogJob(stream, watermarks, jobName, sourceFolderPath);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007C74 File Offset: 0x00005E74
		public static Job NewTransportSyncHealthHubLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			string sourceFolderPath = "D:\\TransportRoles\\Logs\\SyncHealth\\Hub";
			return Jobs.NewTransportSyncHealthLogJob(stream, watermarks, jobName, sourceFolderPath);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007C90 File Offset: 0x00005E90
		public static Job NewOAuthCafeLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating OAuthCafe log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "HttpProxy\\Ews");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogOAuthCafeSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "OAuthCafeLog", stream, watermark, watermarks.Directory);
			OAuthCafeLogExtension extension = new OAuthCafeLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created OutlookService log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007D68 File Offset: 0x00005F68
		public static Job NewOABDownloadLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating OABDownload log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "OABDownload");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogOABDownloadSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "OABDownloadLog", stream, watermark, watermarks.Directory);
			OABDownloadLogExtension extension = new OABDownloadLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created OABDownload log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007E3F File Offset: 0x0000603F
		public static Job NewOutlookMapiHttpConnectivityLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.CreateOutlookServiceJob(stream, watermarks, jobName, Configuration.GetConfigString("OutlookMapiHttpConnectivityLogAnalyzerInputFileFormat", "*10090.csv"), "OutlookMapiHttpConnectivityLog");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007E5D File Offset: 0x0000605D
		public static Job NewOutlookRpcConnectivityLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.CreateOutlookServiceJob(stream, watermarks, jobName, Configuration.GetConfigString("OutlookRpcConnectivityLogAnalyzerInputFileFormat", "*10080.csv"), "OutlookRpcConnectivityLog");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007E7B File Offset: 0x0000607B
		public static Job NewMapiHttpTimeToConnectLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.CreateOutlookServiceJob(stream, watermarks, jobName, Configuration.GetConfigString("MapiHttpTimeToConnectLogAnalyzerInputFileFormat", "*10108.csv"), "MapiHttpTimeToConnectLog");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007E99 File Offset: 0x00006099
		public static Job NewRpcTimeToConnectLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.CreateOutlookServiceJob(stream, watermarks, jobName, Configuration.GetConfigString("RpcTimeToConnectLogAnalyzerInputFileFormat", "*10107.csv"), "RpcTimeToConnectLog");
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007EB7 File Offset: 0x000060B7
		public static Job NewCmdletInfraPowerShellAuthzLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraAuthzLogJob(stream, watermarks, jobName, 0);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007EC4 File Offset: 0x000060C4
		public static Job NewMRSAvailabilityLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating MRS Availability log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			string configStringValue = AppConfigLoader.GetConfigStringValue("MRSAvailabilityLogPath", Path.Combine(ExchangeSetupContext.LoggingPath, "MailboxReplicationService\\MRSAvailability"));
			if (!Directory.Exists(configStringValue))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					configStringValue
				});
				Log.CreateLogDirectory(configStringValue);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new MRSAvailabilityLogSchema(), null, configStringValue, "MRSAvailability_*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "MRSAvailabilityLog", stream, watermark, watermarks.Directory);
			MRSAvailabilityLogExtension extension = new MRSAvailabilityLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created MRS Availability log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007FA5 File Offset: 0x000061A5
		public static Job NewCmdletInfraPowerShellLiveIDAuthzLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraAuthzLogJob(stream, watermarks, jobName, 1);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007FB0 File Offset: 0x000061B0
		public static Job NewCmdletInfraPowerShellLegacyAuthzLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraAuthzLogJob(stream, watermarks, jobName, 2);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007FBB File Offset: 0x000061BB
		public static Job NewCmdletInfraPowerShellCmdletLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraCmdletLogJob(stream, watermarks, jobName, 0);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007FC6 File Offset: 0x000061C6
		public static Job NewCmdletInfraPowerShellLiveIDCmdletLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraCmdletLogJob(stream, watermarks, jobName, 1);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007FD1 File Offset: 0x000061D1
		public static Job NewCmdletInfraPowerShellLegacyCmdletLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraCmdletLogJob(stream, watermarks, jobName, 2);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007FDC File Offset: 0x000061DC
		public static Job NewOwaHttpProxyLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewHttpProxyLogJob(stream, watermarks, jobName, "Owa", "Owa", typeof(OwaHttpProxyLogExtension));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007FFA File Offset: 0x000061FA
		public static Job NewEwsHttpProxyLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewHttpProxyLogJob(stream, watermarks, jobName, "Ews", "Ews", typeof(EwsHttpProxyLogExtension));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008018 File Offset: 0x00006218
		public static Job NewEasHttpProxyLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewHttpProxyLogJob(stream, watermarks, jobName, "Eas", "Eas", typeof(EasHttpProxyLogExtension));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008036 File Offset: 0x00006236
		public static Job NewOutlookHttpProxyLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewHttpProxyLogJob(stream, watermarks, jobName, "Outlook", "RpcHttp", typeof(OutlookHttpProxyLogExtension));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008054 File Offset: 0x00006254
		public static Job NewCmdletInfraPSWSLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraCmdletLogJob(stream, watermarks, jobName, 3);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000805F File Offset: 0x0000625F
		public static Job NewCmdletInfraPowerShellHttpLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraHttpLogJob(stream, watermarks, jobName, 0);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000806A File Offset: 0x0000626A
		public static Job NewCmdletInfraPowerShellLiveIDHttpLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraHttpLogJob(stream, watermarks, jobName, 1);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008075 File Offset: 0x00006275
		public static Job NewCmdletInfraPowerShellLegacyHttpLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			return Jobs.NewCmdletInfraHttpLogJob(stream, watermarks, jobName, 2);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008080 File Offset: 0x00006280
		public static Job NewMailboxAuditLogAvailabilityJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating MailboxAudit log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "AuditingOptics\\MailboxAuditOptics");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new AuditLogSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "MailboxAuditLogExtension", stream, watermark, watermarks.Directory);
			MailboxAuditLogExtension extension = new MailboxAuditLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created MailboxAuditLogAvailability job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008158 File Offset: 0x00006358
		public static Job NewAdminAuditLogAvailabilityJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating AdminAudit log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "AuditingOptics\\AdminAuditOptics");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new AuditLogSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "AdminAuditLogExtension", stream, watermark, watermarks.Directory);
			AdminAuditLogExtension extension = new AdminAuditLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created AdminAuditLogAvailability job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008230 File Offset: 0x00006430
		public static Job NewSynchronousAuditSearchAvailabilityJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating synchronousAuditSearchAvailabilityJob.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "AuditingOptics\\AuditSearchOptics");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new AuditLogSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "AuditSearchLogExtension", stream, watermark, watermarks.Directory);
			AuditSearchLogExtension extension = new AuditSearchLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created synchronousAuditSearchAvailabilityJob.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008308 File Offset: 0x00006508
		public static Job NewPFAssistantLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating PFAssistant log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "PublicFolder");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new PFAssistantLogSchema(), null, text, "*PublicFolderAssistantLog*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "PFAssistantLog", stream, watermark, watermarks.Directory);
			PFAssistantLogExtension extension = new PFAssistantLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created PFAssistant log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000083E0 File Offset: 0x000065E0
		public static Job NewPFSplitHealthLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating PFSplitHealth log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "PublicFolder");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new PFAssistantLogSchema(), null, text, "*PublicFolderSplitHealth*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "PFAssistantLog", stream, watermark, watermarks.Directory);
			PFAssistantLogExtension extension = new PFAssistantLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created PFSplitHealth log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000084B8 File Offset: 0x000066B8
		public static Job NewGroupEscalationLogJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			Logger.LogInformationMessage("Creating Group Escalation log job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "GroupMessageEscalationLogs");
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, text, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "GroupEscalationLog", stream, watermark, watermarks.Directory);
			GroupEscalationLogExtension extension = new GroupEscalationLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Groups Escalation log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008590 File Offset: 0x00006790
		public static Job CreateStoreSyntheticCountersJob(OutputStream stream, Watermarks watermarks, string jobName)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (watermarks == null)
			{
				throw new ArgumentNullException("watermarks");
			}
			Logger.LogInformationMessage("Creating Store Synthetic Counters log job.", new object[0]);
			JobConfiguration jobConfiguration = new JobConfiguration(jobName);
			string text = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "Store");
			if (Directory.Exists(text))
			{
				Watermark watermark = watermarks.Get(jobName);
				LogDirectorySource logSource = new LogDirectorySource(jobName, new StoreSyntheticCountersSchema(), null, text, "SyntheticCounters*.csv", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
				SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, typeof(StoreSyntheticCountersExtension).Name, stream, watermark, watermarks.Directory);
				StoreSyntheticCountersExtension extension = new StoreSyntheticCountersExtension(singleStreamJob);
				singleStreamJob.Extension = extension;
				Logger.LogInformationMessage("Created Store Synthetic Counters job.", new object[0]);
				return singleStreamJob;
			}
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008670 File Offset: 0x00006870
		private static Job NewCmdletInfraAuthzLogJob(OutputStream stream, Watermarks watermarks, string jobName, CmdletInfraVirtualDirectory location)
		{
			string extensionName = string.Format("CmdletInfra{0}AuthzLogExtension", location);
			Logger.LogInformationMessage("Creating CmdletInfra Authz log job at {0}.", new object[]
			{
				location
			});
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, string.Format("CmdletInfra\\{0}\\AuthZ", Jobs.GetCmdletInfraVirtualDirectoryLocation(location)));
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new CmdletInfraLogSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, extensionName, stream, watermark, watermarks.Directory);
			CmdletInfraAuthzLogExtension extension = new CmdletInfraAuthzLogExtension(singleStreamJob, location);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Cmdlet Infra authz log job at {0}.", new object[]
			{
				location
			});
			return singleStreamJob;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008780 File Offset: 0x00006980
		private static Job NewCmdletInfraCmdletLogJob(OutputStream stream, Watermarks watermarks, string jobName, CmdletInfraVirtualDirectory location)
		{
			string extensionName = string.Format("CmdletInfra{0}CmdletLogExtension", location);
			Logger.LogInformationMessage("Creating CmdletInfra Cmdlet log job at {0}.", new object[]
			{
				location
			});
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text;
			if (location == 3)
			{
				text = Path.Combine(ExchangeSetupContext.LoggingPath, "CmdletInfra\\Psws");
			}
			else
			{
				text = Path.Combine(ExchangeSetupContext.LoggingPath, string.Format("CmdletInfra\\{0}\\Cmdlet", Jobs.GetCmdletInfraVirtualDirectoryLocation(location)));
			}
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new CmdletInfraLogSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, extensionName, stream, watermark, watermarks.Directory);
			CmdletInfraCmdletLogExtension extension = new CmdletInfraCmdletLogExtension(singleStreamJob, location);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Cmdlet Infra Cmdlet log job at {0}.", new object[]
			{
				location
			});
			return singleStreamJob;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000088A8 File Offset: 0x00006AA8
		private static Job NewCmdletInfraHttpLogJob(OutputStream stream, Watermarks watermarks, string jobName, CmdletInfraVirtualDirectory location)
		{
			string extensionName = string.Format("CmdletInfra{0}HttpLogExtension", location);
			Logger.LogInformationMessage("Creating CmdletInfra Http log job at {0}.", new object[]
			{
				location
			});
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, string.Format("CmdletInfra\\{0}\\Http", Jobs.GetCmdletInfraVirtualDirectoryLocation(location)));
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new CmdletInfraLogSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, extensionName, stream, watermark, watermarks.Directory);
			CmdletInfraHttpLogExtension extension = new CmdletInfraHttpLogExtension(singleStreamJob, location);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Cmdlet Infra http log job at {0}.", new object[]
			{
				location
			});
			return singleStreamJob;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000089B8 File Offset: 0x00006BB8
		private static Job NewHttpProxyLogJob(OutputStream stream, Watermarks watermarks, string jobName, string extensionNamePrefix, string logSubFolder, Type httpProxyLogExtensionType)
		{
			string extensionName = extensionNamePrefix + "HttpProxyLog";
			Logger.LogInformationMessage("Creating HttpProxyLog job for " + extensionNamePrefix, new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			string text = Path.Combine(ExchangeSetupContext.LoggingPath, "HttpProxy", logSubFolder);
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Log.CreateLogDirectory(text);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new HttpProxyLogSchema(), null, text, "*.log", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, extensionName, stream, watermark, watermarks.Directory);
			HttpProxyLogExtension extension = (HttpProxyLogExtension)Activator.CreateInstance(httpProxyLogExtensionType, new object[]
			{
				singleStreamJob
			});
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created HttpProxyLog job for " + extensionNamePrefix, new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008AC0 File Offset: 0x00006CC0
		private static string GetCmdletInfraVirtualDirectoryLocation(CmdletInfraVirtualDirectory location)
		{
			string result = null;
			switch (location)
			{
			case 0:
				result = "PowerShell-Proxy";
				break;
			case 1:
				result = "PowerShellLiveID-Proxy";
				break;
			case 2:
				result = "PowerShell";
				break;
			case 3:
				result = "PSWS";
				break;
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008B08 File Offset: 0x00006D08
		private static Job NewTransportSyncHealthLogJob(OutputStream stream, Watermarks watermarks, string jobName, string sourceFolderPath)
		{
			Logger.LogInformationMessage("Creating TransportSyncHealthLog job.", new object[0]);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(sourceFolderPath))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					sourceFolderPath
				});
				Log.CreateLogDirectory(sourceFolderPath);
			}
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogCsvSchema(), null, sourceFolderPath, "*.LOG", new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, null);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, "TransportSyncHealthLogExtension", stream, watermark, watermarks.Directory);
			TransportSyncHealthLogExtension extension = new TransportSyncHealthLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created Transport Sync Health log job.", new object[0]);
			return singleStreamJob;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008BD0 File Offset: 0x00006DD0
		private static string GetSiteDefaultsLogDirectory(string elementName)
		{
			string result = string.Empty;
			using (ServerManager serverManager = new ServerManager())
			{
				Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
				ConfigurationSection section = applicationHostConfiguration.GetSection("system.applicationHost/sites");
				ConfigurationElement childElement = section.GetChildElement("siteDefaults");
				ConfigurationElement childElement2 = childElement.GetChildElement(elementName);
				result = Convert.ToString(childElement2["directory"]).Replace("%SystemDrive%", Environment.GetEnvironmentVariable("SystemDrive"));
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private static SingleStreamJob CreateOutlookServiceJob(OutputStream stream, Watermarks watermarks, string jobName, string mask, string extensionName)
		{
			Logger.LogInformationMessage("Creating {0} job.", new object[]
			{
				jobName
			});
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			JobConfiguration jobConfiguration = new JobConfiguration(jobName);
			string text = Path.Combine(jobConfiguration.DiagnosticsRootDirectory, "OutlookService");
			Watermark watermark = watermarks.Get(jobName);
			if (!Directory.Exists(text))
			{
				Logger.LogInformationMessage("Logging folder '{0}' does not exist, creating it.", new object[]
				{
					text
				});
				Directory.CreateDirectory(text);
			}
			bool configBool = Configuration.GetConfigBool("PerformProcessedFileAction", false);
			Action<string> action = delegate(string filePath)
			{
				if (string.IsNullOrEmpty(filePath))
				{
					return;
				}
				try
				{
					File.Delete(filePath);
					Log.LogInformationMessage("Deleted processed file '{0}'.", new object[]
					{
						filePath
					});
				}
				catch (IOException ex)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an IO exception: {1}", new object[]
					{
						filePath,
						ex
					});
				}
				catch (UnauthorizedAccessException ex2)
				{
					Log.LogErrorMessage("Unable to delete processed file '{0}', due to an unauthorized access exception: {1}", new object[]
					{
						filePath,
						ex2
					});
				}
			};
			LogDirectorySource logSource = new LogDirectorySource(jobName, new LogOutlookServiceSchema(), null, text, mask, new Comparison<LogFileInfo>(LogFileInfo.LastWriteTimeComparer), new DateTime?(watermark.Timestamp), null, 4, configBool ? action : null, Encoding.Default);
			SingleStreamJob singleStreamJob = new SingleStreamJob(jobName, logSource, extensionName, stream, watermark, watermarks.Directory);
			OutlookServiceLogExtension extension = new OutlookServiceLogExtension(singleStreamJob);
			singleStreamJob.Extension = extension;
			Logger.LogInformationMessage("Created {0} job.", new object[]
			{
				jobName
			});
			return singleStreamJob;
		}
	}
}
