using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000015 RID: 21
	public abstract class RusPublishingPipelineBase : ProbeWorkItem
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004EB8 File Offset: 0x000030B8
		public RusPublishingPipelineBase()
		{
			string forefrontBackgroundInstallPath = RusPublishingPipelineBase.GetForefrontBackgroundInstallPath("MsiInstallPath");
			ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
			{
				ExeConfigFilename = Path.Combine(forefrontBackgroundInstallPath, "Microsoft.Forefront.Hygiene.Rus.Pipeline.dll.config")
			};
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
			KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings["PrimaryActiveQdsPath"];
			this.RusPrimaryFileShareRootPath = ((keyValueConfigurationElement != null) ? keyValueConfigurationElement.Value : null);
			keyValueConfigurationElement = configuration.AppSettings.Settings["SecondaryActiveQdsPath"];
			this.RusAlternateFileShareRootPath = ((keyValueConfigurationElement != null) ? keyValueConfigurationElement.Value : null);
			if (string.IsNullOrEmpty(this.RusPrimaryFileShareRootPath) && string.IsNullOrEmpty(this.RusAlternateFileShareRootPath))
			{
				this.LogTraceErrorAndThrowApplicationException("Both RUS Primary and Alternate share root paths are empty");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004F6F File Offset: 0x0000316F
		public static string ForeFrontdlUniversalManifestCabUrl
		{
			get
			{
				return Path.Combine(RusEngine.RusEngineDownloadUrl, "metadata", "UniversalManifest.cab").Replace("\\", "/");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004F94 File Offset: 0x00003194
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004F9C File Offset: 0x0000319C
		protected string RusPrimaryFileShareRootPath { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004FA5 File Offset: 0x000031A5
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004FAD File Offset: 0x000031AD
		protected string RusAlternateFileShareRootPath { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004FB6 File Offset: 0x000031B6
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004FBE File Offset: 0x000031BE
		protected string[] Platforms { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004FC7 File Offset: 0x000031C7
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004FCF File Offset: 0x000031CF
		protected string RusEngineName { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004FD8 File Offset: 0x000031D8
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004FE0 File Offset: 0x000031E0
		protected RusEngine RusEngine { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00004FE9 File Offset: 0x000031E9
		protected bool AreManifestFilesOutOfSync(DateTime expectedDateTime, DateTime actualDateTime, TimeSpan allowedThresholdTimeSpan)
		{
			return expectedDateTime - actualDateTime > allowedThresholdTimeSpan;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004FF8 File Offset: 0x000031F8
		protected int GetIntegerExtensionAttributeFromXml(string workContextXml, string xmlNodeName, string extensionAttributeName, int defaultIntegerValue, int acceptedMinimum, int acceptedMaximum)
		{
			if (acceptedMaximum < acceptedMinimum)
			{
				throw new ArgumentException(string.Format("acceptedMaximum: {0} is less than acceptedMinimum: {1}", acceptedMaximum, acceptedMinimum));
			}
			string extensionAttributeStringFromXml = this.GetExtensionAttributeStringFromXml(workContextXml, xmlNodeName, extensionAttributeName, false);
			int num = defaultIntegerValue;
			if (!string.IsNullOrWhiteSpace(extensionAttributeStringFromXml) && (!int.TryParse(extensionAttributeStringFromXml, out num) || num < acceptedMinimum || num > acceptedMaximum))
			{
				num = defaultIntegerValue;
			}
			return num;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005058 File Offset: 0x00003258
		protected TimeSpan GetTimeSpanExtensionAttributeFromXml(string workContextXml, string xmlNodeName, string extensionAttributeName, TimeSpan defaultTimeSpanValue, TimeSpan minTimeSpan, TimeSpan maxTimeSpan)
		{
			if (maxTimeSpan < minTimeSpan)
			{
				throw new ArgumentException(string.Format("maxTimeSpan: {0} is less than minTimeSpan: {1}", maxTimeSpan, minTimeSpan));
			}
			string extensionAttributeStringFromXml = this.GetExtensionAttributeStringFromXml(workContextXml, xmlNodeName, extensionAttributeName, false);
			TimeSpan timeSpan = defaultTimeSpanValue;
			if (!string.IsNullOrWhiteSpace(extensionAttributeStringFromXml) && (!TimeSpan.TryParse(extensionAttributeStringFromXml, out timeSpan) || timeSpan < minTimeSpan || timeSpan > maxTimeSpan))
			{
				timeSpan = defaultTimeSpanValue;
			}
			return timeSpan;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000050C8 File Offset: 0x000032C8
		protected string GetExtensionAttributeStringFromXml(string workContextXml, string xmlNodeName, string extensionAttributeName, bool throwOnNullOrEmpty = true)
		{
			if (string.IsNullOrWhiteSpace(workContextXml))
			{
				throw new ArgumentNullException("WorkContext definition Xml is null or empty");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(workContextXml);
			XmlElement xmlElement = Utils.CheckXmlElement(xmlDocument.SelectSingleNode(xmlNodeName), xmlNodeName);
			string attribute = xmlElement.GetAttribute(extensionAttributeName);
			if (throwOnNullOrEmpty && string.IsNullOrWhiteSpace(attribute))
			{
				throw new ArgumentNullException("Extension Attribute value in WorkContext xml is found to be null or empty", extensionAttributeName);
			}
			return attribute;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005124 File Offset: 0x00003324
		protected Collection<PSObject> ExecuteForeFrontManagementShellScript(string psScript, bool throwOnErrors = false)
		{
			if (string.IsNullOrWhiteSpace(psScript))
			{
				throw new ArgumentNullException("psScript", "PowerShell script cannot be null or empty.");
			}
			this.TraceDebug(string.Format("[PowerShellScript: {0}]", psScript));
			Collection<PSObject> result = null;
			try
			{
				RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
				PSSnapInException ex = null;
				runspaceConfiguration.AddPSSnapIn("Microsoft.Forefront.Management.PowerShell", out ex);
				if (ex != null)
				{
					this.TraceDebug(string.Format("Non-fatal error occurred while adding the powerShell snap-in - {0}. Warning: {1}", "Microsoft.Forefront.Management.PowerShell", ex.Message));
				}
				using (Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration))
				{
					runspace.Open();
					Pipeline pipeline = runspace.CreatePipeline();
					Command item = new Command(psScript, true);
					pipeline.Commands.Add(item);
					result = pipeline.Invoke();
					if (pipeline.HadErrors)
					{
						StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
						foreach (object obj in pipeline.Error.ReadToEnd())
						{
							stringBuilder.AppendLine(obj.ToString());
						}
						string text = string.Format("Errors occurred while executing pipeline.Invoke(). Command: {0} Errors: {1}", psScript, stringBuilder.ToString());
						if (throwOnErrors)
						{
							this.LogTraceErrorAndThrowApplicationException(text);
						}
						this.TraceDebug(text);
					}
				}
			}
			catch (Exception ex2)
			{
				this.LogTraceErrorAndThrowApplicationException(string.Format("Failed to execute ForeFrontManagementShell script {0}. Exception: {1}", psScript, ex2.ToString()));
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005298 File Offset: 0x00003498
		protected string FormatBackgroundJobTaskResultsToString(Collection<PSObject> bgdTaskResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PSObject psobject in bgdTaskResults)
			{
				string value = string.Format("TaskId: {0}, StartTime: {1}, EndTime: {2}, ExecutableState: {3}, TaskCompletionStatus: {4}, BgdMachineName: {5}, RetryAttempts: {6} \n", new object[]
				{
					Convert.ToString(psobject.Properties["TaskId"].Value),
					Convert.ToString(psobject.Properties["StartTime"].Value),
					Convert.ToString(psobject.Properties["EndTime"].Value),
					Convert.ToString(psobject.Properties["ExecutableState"].Value),
					Convert.ToString(psobject.Properties["TaskCompletionStatus"].Value),
					Convert.ToString(psobject.Properties["BackgroundJobManagerMachineName"].Value),
					Convert.ToString(psobject.Properties["RetryAttempts"].Value)
				});
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000053E8 File Offset: 0x000035E8
		protected void LogTraceErrorAndThrowApplicationException(string errorMsg)
		{
			base.Result.Error = errorMsg;
			this.TraceError(errorMsg);
			throw new ApplicationException(errorMsg);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005403 File Offset: 0x00003603
		protected void TraceDebug(string debugMsg)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + debugMsg + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.BackgroundTracer, base.TraceContext, debugMsg, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Background\\Probes\\RusPublishingPipelineBase.cs", 431);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005442 File Offset: 0x00003642
		protected void TraceError(string errorMsg)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + errorMsg + " ";
			WTFDiagnostics.TraceError(ExTraceGlobals.BackgroundTracer, base.TraceContext, errorMsg, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Background\\Probes\\RusPublishingPipelineBase.cs", 441);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005484 File Offset: 0x00003684
		protected string GetRegionTag()
		{
			string exchangeLabsRegKeyValue = RusEngine.GetExchangeLabsRegKeyValue("Region");
			if (string.IsNullOrWhiteSpace(exchangeLabsRegKeyValue))
			{
				this.LogTraceErrorAndThrowApplicationException(string.Format("Registry string [{0}] value is found to be empty", exchangeLabsRegKeyValue));
			}
			string exchangeLabsRegKeyValue2 = RusEngine.GetExchangeLabsRegKeyValue("RegionServiceInstance");
			if (string.IsNullOrWhiteSpace(exchangeLabsRegKeyValue2))
			{
				this.LogTraceErrorAndThrowApplicationException(string.Format("Registry string [{0}] value is found to be empty", exchangeLabsRegKeyValue2));
			}
			return exchangeLabsRegKeyValue + exchangeLabsRegKeyValue2;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000054E0 File Offset: 0x000036E0
		private static string GetForefrontBackgroundInstallPath(string regStringName)
		{
			string result = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\FfoBackground\\Setup"))
				{
					if (registryKey == null)
					{
						throw new ApplicationException(string.Format("HKLM forefrontKey registry key [{0}] is found to be null", "SOFTWARE\\Microsoft\\FfoBackground\\Setup"));
					}
					if (registryKey.GetValue(regStringName) == null)
					{
						throw new ApplicationException(string.Format("RegistryKey.GetValue() returned null for the reg string [{0}]", regStringName));
					}
					result = (registryKey.GetValue(regStringName) as string);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("An error occured while loading registry key value [{0}]. Exception: {1}", regStringName, ex.ToString()));
			}
			return result;
		}

		// Token: 0x0400005D RID: 93
		protected const string EngineNameXmlAttribute = "EngineName";

		// Token: 0x0400005E RID: 94
		protected const string PlatformsXmlAttribute = "Platforms";

		// Token: 0x0400005F RID: 95
		protected const string RegionKey = "Region";

		// Token: 0x04000060 RID: 96
		protected const string RegionServiceInstanceKey = "RegionServiceInstance";

		// Token: 0x04000061 RID: 97
		protected const string ForeFrontManagementShellSnapIn = "Microsoft.Forefront.Management.PowerShell";

		// Token: 0x04000062 RID: 98
		protected const string RusSignaturePollingBgdJobFileName = "Microsoft.Forefront.Hygiene.Rus.SignaturePollingJob.exe";

		// Token: 0x04000063 RID: 99
		protected const string RusEngineUpdatePublisherBgdJobFileName = "Microsoft.Forefront.Hygiene.Rus.EngineUpdatePublisher.exe";

		// Token: 0x04000064 RID: 100
		protected const string RusPipelineBinaryConfigFileName = "Microsoft.Forefront.Hygiene.Rus.Pipeline.dll.config";

		// Token: 0x04000065 RID: 101
		protected const string GetBgdDefinitionCmdlet = "Get-BackgroundJobDefinition";

		// Token: 0x04000066 RID: 102
		protected const string GetBgdScheduleCmdlet = "Get-BackgroundJobSchedule";

		// Token: 0x04000067 RID: 103
		protected const string GetBgdTaskCmdlet = "Get-BackgroundJobTask";

		// Token: 0x04000068 RID: 104
		protected const string GetAsyncQueueRequestCmdlet = "Get-AsyncQueueRequest";

		// Token: 0x04000069 RID: 105
		protected const string GetAsyncQueueLogsCmdlet = "Get-AsyncQueueLog";

		// Token: 0x0400006A RID: 106
		protected const string RusPipelineJobOwnerName = "RusPipelineJob";

		// Token: 0x0400006B RID: 107
		protected const string BackgroundApplicationInstallRegPath = "SOFTWARE\\Microsoft\\FfoBackground\\Setup";

		// Token: 0x0400006C RID: 108
		protected const string BackgroundApplicationInstallRegKey = "MsiInstallPath";

		// Token: 0x0400006D RID: 109
		protected const string OpenBrace = "{";

		// Token: 0x0400006E RID: 110
		protected const string ClosedBrace = "}";
	}
}
