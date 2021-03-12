using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000059 RID: 89
	public abstract class EscalateResponderBase : ResponderWorkItem
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000168FA File Offset: 0x00014AFA
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00016902 File Offset: 0x00014B02
		protected NotificationServiceClass? EscalationNotificationType
		{
			get
			{
				return this.escalationNotificationType;
			}
			set
			{
				this.escalationNotificationType = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001690B File Offset: 0x00014B0B
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00016913 File Offset: 0x00014B13
		protected string CustomEscalationSubject
		{
			get
			{
				return this.customEscalationSubject;
			}
			set
			{
				this.customEscalationSubject = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001691C File Offset: 0x00014B1C
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00016924 File Offset: 0x00014B24
		protected string CustomEscalationMessage
		{
			get
			{
				return this.customEscalationMessage;
			}
			set
			{
				this.customEscalationMessage = value;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001692D File Offset: 0x00014B2D
		public EscalateResponderBase()
		{
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00016941 File Offset: 0x00014B41
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00016948 File Offset: 0x00014B48
		internal static string DefaultEscalationSubject
		{
			get
			{
				return EscalateResponderBase.DefaultEscalationSubjectInternal;
			}
			set
			{
				EscalateResponderBase.DefaultEscalationSubjectInternal = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00016950 File Offset: 0x00014B50
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00016957 File Offset: 0x00014B57
		internal static string DefaultEscalationMessage
		{
			get
			{
				return EscalateResponderBase.DefaultEscalationMessageInternal;
			}
			set
			{
				EscalateResponderBase.DefaultEscalationMessageInternal = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001695F File Offset: 0x00014B5F
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00016966 File Offset: 0x00014B66
		internal static string HealthSetEscalationSubjectPrefix
		{
			get
			{
				return EscalateResponderBase.HealthSetEscalationSubjectPrefixInternal;
			}
			set
			{
				EscalateResponderBase.HealthSetEscalationSubjectPrefixInternal = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001696E File Offset: 0x00014B6E
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00016975 File Offset: 0x00014B75
		internal static string HealthSetMaintenanceEscalationSubjectPrefix
		{
			get
			{
				return EscalateResponderBase.HealthSetMaintenanceEscalationSubjectPrefixInternal;
			}
			set
			{
				EscalateResponderBase.HealthSetMaintenanceEscalationSubjectPrefixInternal = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001697D File Offset: 0x00014B7D
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00016984 File Offset: 0x00014B84
		internal static HealthSetEscalationHelper EscalationHelper
		{
			get
			{
				return EscalateResponderBase.EscalationHelperInternal;
			}
			set
			{
				EscalateResponderBase.EscalationHelperInternal = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001698C File Offset: 0x00014B8C
		protected static string LoadFromResourceAttributeValue
		{
			get
			{
				return "LoadFromResourceAttributeValue";
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00016993 File Offset: 0x00014B93
		protected CancellationToken LocalCancellationToken
		{
			get
			{
				return this.localCancellationToken;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001699B File Offset: 0x00014B9B
		protected RemotePowerShell RemotePowershell
		{
			get
			{
				return this.remotePowerShell;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000169A3 File Offset: 0x00014BA3
		protected virtual bool IncludeHealthSetEscalationInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000169A8 File Offset: 0x00014BA8
		protected static bool IsOBDGallatinMachine
		{
			get
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
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00016A04 File Offset: 0x00014C04
		private static string LocalMachineVersion
		{
			get
			{
				if (EscalateResponderBase.localMachineVersion == null)
				{
					try
					{
						AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).Single<object>();
						EscalateResponderBase.localMachineVersion = string.Format("{0}-{1}", assemblyFileVersionAttribute.Version, Environment.OSVersion.Version.ToString());
					}
					catch (InvalidOperationException)
					{
						EscalateResponderBase.localMachineVersion = string.Empty;
					}
				}
				return EscalateResponderBase.localMachineVersion;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00016A80 File Offset: 0x00014C80
		private static bool IsFfoMachine
		{
			get
			{
				return DatacenterRegistry.IsForefrontForOffice() || EscalateResponderBase.IsFfoCentralAdminRoleInstalled;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00016A90 File Offset: 0x00014C90
		private static bool IsFfoCentralAdminRoleInstalled
		{
			get
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CentralAdminRole");
				if (registryKey != null)
				{
					registryKey.Close();
					registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PowerShell\\1\\PowerShellSnapIns\\Microsoft.Exchange.Management.Powershell.FfoCentralAdmin");
					if (registryKey != null)
					{
						registryKey.Close();
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00016AD2 File Offset: 0x00014CD2
		public static void SetScopeInformation(ResponderDefinition definition, string location, string forest, string dag, string site, string region)
		{
			EscalateResponderBase.SetScopeInformation(definition, location, forest, dag, site, region, null, null);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00016AE3 File Offset: 0x00014CE3
		public static void SetScopeInformation(ResponderDefinition definition, string location, string forest, string dag, string site, string region, string capacityUnit)
		{
			EscalateResponderBase.SetScopeInformation(definition, location, forest, dag, site, region, capacityUnit, null);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00016AF8 File Offset: 0x00014CF8
		public static void SetScopeInformation(ResponderDefinition definition, string location, string forest, string dag, string site, string region, string capacityUnit, string rack)
		{
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("ScopeLocation", location);
			dictionary.Add("ScopeForest", forest);
			dictionary.Add("ScopeDag", dag);
			dictionary.Add("ScopeSite", site);
			dictionary.Add("ScopeRegion", region);
			if (!string.IsNullOrEmpty(capacityUnit))
			{
				dictionary.Add("ScopeCapacityUnit", capacityUnit);
			}
			if (!string.IsNullOrEmpty(rack))
			{
				dictionary.Add("ScopeRack", rack);
			}
			EscalateResponderBase.AddExtensionAttributes(definition, dictionary);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00016B8C File Offset: 0x00014D8C
		public static void AddExtensionAttributes(ResponderDefinition definition, Dictionary<string, string> attributeValueByName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!string.IsNullOrWhiteSpace(definition.ExtensionAttributes))
			{
				try
				{
					xmlDocument.LoadXml(definition.ExtensionAttributes);
				}
				catch (XmlException)
				{
					throw new Exception("EscalateResponderBase.SetServiceAndAlertSource: Existing ExtensionAttributes contains invalid XML.");
				}
				if (xmlDocument.DocumentElement.Name != "ExtensionAttributes")
				{
					throw new Exception("EscalateResponderBase.SetServiceAndAlertSource: Existing ExtensionAttributes has invalid schema.");
				}
			}
			else
			{
				xmlDocument.LoadXml("<ExtensionAttributes />");
			}
			foreach (KeyValuePair<string, string> keyValuePair in attributeValueByName)
			{
				EscalateResponderBase.SetExtensionAttribute(xmlDocument, keyValuePair.Key, keyValuePair.Value);
			}
			definition.ExtensionAttributes = xmlDocument.OuterXml;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00016C5C File Offset: 0x00014E5C
		internal virtual EscalationState GetEscalationState(bool? isHealthy, CancellationToken cancellationToken)
		{
			if (isHealthy == null)
			{
				return EscalationState.Unknown;
			}
			if (isHealthy.Value)
			{
				return EscalationState.Green;
			}
			if (this.GetEscalationEnvironment() == EscalationEnvironment.OnPrem)
			{
				return EscalationState.Red;
			}
			NotificationServiceClass notificationServiceClass = this.escalationNotificationType ?? base.Definition.NotificationServiceClass;
			switch (notificationServiceClass)
			{
			case NotificationServiceClass.Urgent:
				return EscalationState.Red;
			case NotificationServiceClass.UrgentInTraining:
				return EscalationState.Yellow;
			case NotificationServiceClass.Scheduled:
			{
				DailySchedulePattern dailySchedulePattern = new DailySchedulePattern(base.Definition.DailySchedulePattern);
				DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, dailySchedulePattern.TimeZoneInfo);
				if (dailySchedulePattern.ScheduledDays.Contains(dateTime.DayOfWeek) && dailySchedulePattern.StartTime.TimeOfDay <= dateTime.TimeOfDay && dateTime.TimeOfDay <= dailySchedulePattern.EndTime.TimeOfDay)
				{
					return EscalationState.Orange;
				}
				return EscalationState.DarkYellow;
			}
			default:
				throw new NotSupportedException(string.Format("NotificationServiceClass value not supported: {0}", notificationServiceClass.ToString()));
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00016D5B File Offset: 0x00014F5B
		internal virtual void BeforeContentGeneration(ResponseMessageReader propertyReader)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00016DB0 File Offset: 0x00014FB0
		internal virtual void GetEscalationSubjectAndMessage(MonitorResult monitorResult, out string escalationSubject, out string escalationMessage, bool rethrow = false, Action<ResponseMessageReader> textGeneratorModifier = null)
		{
			ResponseMessageReader responseMessageReader = new ResponseMessageReader();
			responseMessageReader.AddObject<string>("0", monitorResult.Component.Name);
			responseMessageReader.AddObjectResolver<string>("1", delegate
			{
				if (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn)
				{
					return string.Format("{0}/{1}/{2}", base.Definition.TargetPartition, base.Definition.TargetGroup, base.Definition.TargetResource);
				}
				return base.Definition.TargetResource;
			});
			responseMessageReader.AddObject<DateTime?>("2", monitorResult.FirstAlertObservedTime);
			responseMessageReader.AddObject<double>("3", monitorResult.TotalValue);
			responseMessageReader.AddObject<MonitorResult>("Monitor", monitorResult);
			responseMessageReader.AddObject<ProbeResult>("Probe", this.GetLastFailedProbeResult(monitorResult));
			try
			{
				MonitorDefinition result = base.Broker.GetMonitorDefinition(monitorResult.WorkItemId).ExecuteAsync(this.LocalCancellationToken, base.TraceContext).Result;
				if (result != null)
				{
					responseMessageReader.AddObject<MonitorDefinition>("MonitorDefinition", result);
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to get MonitorDefinition to use as a replacement object: {0}", ex.ToString(), null, "GetEscalationSubjectAndMessage", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 659);
			}
			this.BeforeContentGeneration(responseMessageReader);
			if (textGeneratorModifier != null)
			{
				textGeneratorModifier(responseMessageReader);
			}
			try
			{
				escalationSubject = responseMessageReader.ReplaceValues((!string.IsNullOrWhiteSpace(this.customEscalationSubject)) ? this.customEscalationSubject : base.Definition.EscalationSubject);
			}
			catch (Exception ex2)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to replace user-defined escalation subject: {0}", ex2.ToString(), null, "GetEscalationSubjectAndMessage", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 675);
				base.Result.StateAttribute4 = "Subject: " + ex2.ToString();
				if (rethrow)
				{
					throw;
				}
				escalationSubject = null;
			}
			if (string.IsNullOrWhiteSpace(escalationSubject))
			{
				escalationSubject = responseMessageReader.ReplaceValues(EscalateResponderBase.DefaultEscalationSubject);
			}
			string text = (!string.IsNullOrWhiteSpace(this.customEscalationMessage)) ? this.customEscalationMessage : base.Definition.EscalationMessage;
			if (this.ReadAttribute(EscalateResponderBase.LoadFromResourceAttributeValue, false))
			{
				text = EscalateResponderBase.GetNonLocalizableResourceAsString(base.GetType().Assembly, text);
			}
			try
			{
				escalationMessage = responseMessageReader.ReplaceValues(text);
			}
			catch (Exception ex3)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to replace user-defined escalation message: {0}", ex3.ToString(), null, "GetEscalationSubjectAndMessage", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 707);
				if (string.IsNullOrEmpty(base.Result.StateAttribute4))
				{
					base.Result.StateAttribute4 = ex3.ToString();
				}
				else
				{
					ResponderResult result2 = base.Result;
					result2.StateAttribute4 = result2.StateAttribute4 + "Body: " + ex3.ToString();
				}
				if (rethrow)
				{
					throw;
				}
				escalationMessage = null;
			}
			if (string.IsNullOrWhiteSpace(escalationMessage))
			{
				escalationMessage = responseMessageReader.ReplaceValues(EscalateResponderBase.DefaultEscalationMessage);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00017054 File Offset: 0x00015254
		protected void SetServiceAndTeam(string escalationService, string escalationTeam)
		{
			if (string.IsNullOrEmpty(escalationService))
			{
				throw new ArgumentException("You must specify a service. Use the Get-OnCallService cmdlet in the Smart Alert system for a current list of valid service names.");
			}
			if (string.IsNullOrEmpty(escalationTeam))
			{
				throw new ArgumentException("You must specify a team that belongs to the service. Use the Get-OnCallTeam cmdlet in the Smart Alert system for a current list of valid team names.");
			}
			this.escalationService = escalationService;
			this.escalationTeam = escalationTeam;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001708C File Offset: 0x0001528C
		protected internal static void SetActiveMonitoringCertificateSettings(ResponderDefinition definition)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(EscalateResponderBase.ActiveMonitoringRegistryPath, false))
			{
				if (registryKey != null)
				{
					string text;
					if (string.IsNullOrWhiteSpace(definition.Account) && (text = (string)registryKey.GetValue("RPSCertificateSubject", null)) != null)
					{
						definition.Account = text;
					}
					if (string.IsNullOrWhiteSpace(definition.Endpoint) && (text = (string)registryKey.GetValue("RPSEndpoint", null)) != null)
					{
						definition.Endpoint = text;
					}
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017918 File Offset: 0x00015B18
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.localCancellationToken = cancellationToken;
			Task<ResponderResult> lastSuccessfulRecoveryAttemptedResponderResult = this.GetLastSuccessfulRecoveryAttemptedResponderResult(cancellationToken);
			lastSuccessfulRecoveryAttemptedResponderResult.Continue(delegate(ResponderResult lastResponderResult)
			{
				this.Result.ResponseAction = EscalateResponderBase.ResponseAction.Defer.ToString();
				DateTime startTime = SqlDateTime.MinValue.Value;
				if (lastResponderResult != null)
				{
					startTime = lastResponderResult.ExecutionStartTime;
					this.Result.StateAttribute1 = lastResponderResult.StateAttribute1;
					this.Result.StateAttribute2 = lastResponderResult.StateAttribute2;
					this.Result.StateAttribute3 = lastResponderResult.StateAttribute3;
				}
				IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition.AlertMask, startTime, this.Result.ExecutionStartTime);
				Task<MonitorResult> task = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, this.TraceContext);
				task.Continue(delegate(MonitorResult lastMonitorResult)
				{
					if (lastMonitorResult != null)
					{
						if (lastMonitorResult.Component == null)
						{
							lastMonitorResult.Component = new Component(lastMonitorResult.ComponentName);
						}
						if (lastMonitorResult.IsAlert)
						{
							if (lastResponderResult != null && string.Compare(lastMonitorResult.ComponentName, lastResponderResult.StateAttribute2, true) == 0 && lastResponderResult.StateAttribute1 != null)
							{
								if (this.Definition.MinimumSecondsBetweenEscalates == -1 || this.Definition.WaitIntervalSeconds == -1)
								{
									this.Result.IsThrottled = true;
								}
								else
								{
									this.Result.IsThrottled = (DateTime.Parse(lastResponderResult.StateAttribute1) > this.Result.ExecutionStartTime);
								}
							}
							WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "EscalateResponderBase.DoResponderWork: Component {0} is Alerting. Throttled:{1}", lastMonitorResult.ComponentName, this.Result.IsThrottled.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 859);
							if (!this.Result.IsThrottled)
							{
								this.BeforeEscalate(cancellationToken);
								if (this.Definition.UploadScopeNotification)
								{
									EscalateResponderBase.UnhealthyMonitoringEvent unhealthyMonitoringEvent = this.GetUnhealthyMonitoringEvent(lastMonitorResult, !this.Definition.AlwaysEscalateOnMonitorChanges && this.IncludeHealthSetEscalationInfo);
									this.AddScopeNotification(lastMonitorResult, unhealthyMonitoringEvent.Subject, unhealthyMonitoringEvent.Message);
								}
								if (this.Definition.SuppressEscalation)
								{
									this.Result.ResponseAction = EscalateResponderBase.ResponseAction.EscalationSuppressed.ToString();
									return;
								}
								EscalationState escalationState = this.GetEscalationState(new bool?(false), cancellationToken);
								bool flag = false;
								if (escalationState == EscalationState.Red || escalationState == EscalationState.Orange)
								{
									flag = true;
								}
								else if (escalationState == EscalationState.DarkYellow)
								{
									flag = false;
									this.Result.ResponseAction = EscalateResponderBase.ResponseAction.DeferNonBusinessHours.ToString();
								}
								else if (escalationState == EscalationState.Yellow)
								{
									flag = true;
								}
								string name = lastMonitorResult.Component.Name;
								bool flag2 = false;
								string text = null;
								if (!this.Definition.AlwaysEscalateOnMonitorChanges)
								{
									text = Guid.NewGuid().ToString();
									HealthSetEscalationState healthSetEscalationState = EscalateResponderBase.EscalationHelper.LockHealthSetEscalationStateIfRequired(name, escalationState, text);
									if (healthSetEscalationState != null)
									{
										WTFDiagnostics.TraceDebug<string, string, string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "EscalateResponderBase.DoResponderWork: LockHealthSetEscalationStateIfRequired(healthSetName:{0}, escalationState:{1}, lockOwnerId:{2}) returned healthSetEscalationState with: EscalationState:{3} LockOwnerId:{4}", name, escalationState.ToString(), text, healthSetEscalationState.EscalationState.ToString(), healthSetEscalationState.LockOwnerId, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 930);
										if (healthSetEscalationState.EscalationState >= escalationState && healthSetEscalationState.LockOwnerId == null)
										{
											flag = false;
											flag2 = false;
											this.Result.ResponseAction = EscalateResponderBase.ResponseAction.DeferHealthSetSuppression.ToString();
										}
										else
										{
											if (healthSetEscalationState.EscalationState >= escalationState || !(healthSetEscalationState.LockOwnerId == text))
											{
												return;
											}
											flag2 = true;
										}
									}
								}
								if (flag)
								{
									this.Escalate(lastMonitorResult, escalationState);
									this.Result.ResponseAction = EscalateResponderBase.ResponseAction.Escalate.ToString();
									if (this.Definition.MinimumSecondsBetweenEscalates > 0)
									{
										this.Result.StateAttribute1 = DateTime.UtcNow.AddSeconds((double)this.Definition.MinimumSecondsBetweenEscalates).ToString();
									}
									else if (this.Definition.WaitIntervalSeconds > 0)
									{
										this.Result.StateAttribute1 = DateTime.UtcNow.AddSeconds((double)this.Definition.WaitIntervalSeconds).ToString();
									}
									else
									{
										this.Result.StateAttribute1 = DateTime.UtcNow.AddSeconds(14400.0).ToString();
									}
									this.Result.StateAttribute2 = lastMonitorResult.ComponentName;
									this.AfterEscalate(cancellationToken);
								}
								else
								{
									bool flag3 = false;
									if (lastResponderResult != null && lastResponderResult.StateAttribute3 != null)
									{
										flag3 = (DateTime.Parse(lastResponderResult.StateAttribute3) > this.Result.ExecutionStartTime);
									}
									WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "EscalateResponderBase.DoResponderWork: Not escalating component {0}. logEventIsThrottled:{1}", lastMonitorResult.ComponentName, flag3.ToString(), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1010);
									if (!flag3)
									{
										this.LogCustomUnhealthyEvent(this.GetUnhealthyMonitoringEvent(lastMonitorResult, false));
										this.Result.StateAttribute3 = DateTime.UtcNow.AddSeconds(14400.0).ToString();
									}
								}
								if (!this.Definition.AlwaysEscalateOnMonitorChanges && flag2 && !EscalateResponderBase.EscalationHelper.SetHealthSetEscalationState(name, escalationState, text))
								{
									WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.TraceContext, "EscalateResponderBase.DoResponderWork: SetHealthSetEscalationState(healthSetName:{0}, escalationState:{1}, lockOwnerId:{2}) returned false, returning.", name, escalationState.ToString(), text, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1031);
									return;
								}
							}
							else
							{
								this.Result.ResponseAction = EscalateResponderBase.ResponseAction.Throttled.ToString();
								this.Result.StateAttribute1 = lastResponderResult.StateAttribute1;
								this.Result.StateAttribute2 = lastMonitorResult.ComponentName;
								this.Result.StateAttribute3 = lastResponderResult.StateAttribute3;
							}
						}
					}
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001796C File Offset: 0x00015B6C
		protected virtual NotificationServiceClass GetNotificationServiceClass(MonitorResult monitorResult)
		{
			return base.Definition.NotificationServiceClass;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017979 File Offset: 0x00015B79
		protected virtual void BeforeEscalate(CancellationToken cancellationToken)
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001797B File Offset: 0x00015B7B
		protected virtual void AfterEscalate(CancellationToken cancellationToken)
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00017980 File Offset: 0x00015B80
		protected virtual void InvokeNewServiceAlert(Guid alertGuid, string alertTypeId, string alertName, string alertDescription, DateTime raisedTime, string escalationTeam, string service, string alertSource, bool isDatacenter, bool urgent, string environment, string location, string forest, string dag, string site, string region, string capacityUnit, string rack, string alertCategory)
		{
			this.InvokeNewServiceAlert(alertGuid, alertTypeId, alertName, alertDescription, raisedTime, escalationTeam, service, alertSource, isDatacenter, urgent, environment, location, forest, dag, site, region, capacityUnit, rack, alertCategory, false, false);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000179B8 File Offset: 0x00015BB8
		protected virtual void InvokeNewServiceAlert(Guid alertGuid, string alertTypeId, string alertName, string alertDescription, DateTime raisedTime, string escalationTeam, string service, string alertSource, bool isDatacenter, bool urgent, string environment, string location, string forest, string dag, string site, string region, string capacityUnit, string rack, string alertCategory, bool isIncident)
		{
			this.InvokeNewServiceAlert(alertGuid, alertTypeId, alertName, alertDescription, raisedTime, escalationTeam, service, alertSource, isDatacenter, urgent, environment, location, forest, dag, site, region, capacityUnit, rack, alertCategory, isIncident, false);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000179F4 File Offset: 0x00015BF4
		protected virtual void InvokeNewServiceAlert(Guid alertGuid, string alertTypeId, string alertName, string alertDescription, DateTime raisedTime, string escalationTeam, string service, string alertSource, bool isDatacenter, bool urgent, string environment, string location, string forest, string dag, string site, string region, string capacityUnit, string rack, string alertCategory, bool isIncident, bool skipSuppression)
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
				string value;
				if (string.IsNullOrWhiteSpace(base.Definition.TargetGroup))
				{
					value = Environment.MachineName;
				}
				else
				{
					value = base.Definition.TargetGroup;
				}
				pscommand.AddParameter("MachineName", value);
				if (EscalateResponderBase.IsFfoMachine || EscalateResponderBase.IsOBDGallatinMachine)
				{
					pscommand.AddParameter("MachineProvisioningState", "Provisioned");
					pscommand.AddParameter("MachineMonitoringState", "On");
					if (!string.IsNullOrEmpty(EscalateResponderBase.LocalMachineVersion))
					{
						pscommand.AddParameter("MachineVersion", EscalateResponderBase.LocalMachineVersion);
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
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "EscalateResponderBase.InvokeNewServiceAlert: Escalating alert '{0}' via command '{1}'...", alertName, stringBuilder.ToString(), null, "InvokeNewServiceAlert", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1402);
			try
			{
				this.remotePowerShell.InvokePSCommand(pscommand);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "EscalateResponderBase.InvokeNewServiceAlert: Successfully escalated alert '{0}'.", alertName, null, "InvokeNewServiceAlert", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1406);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("EscalateResponderBase.InvokeNewServiceAlert: Unexpected failure when escalating alert '{0}'\r\n\r\nException: {1}\r\n\r\nCommand: '{2}'", alertName, ex.ToString(), stringBuilder.ToString()));
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00017D94 File Offset: 0x00015F94
		protected virtual bool ShouldRaiseActiveMonitoringAlerts(EscalationEnvironment environment)
		{
			switch (environment)
			{
			case EscalationEnvironment.Datacenter:
			{
				bool result = false;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(EscalateResponderBase.ActiveMonitoringRegistryPath, false))
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

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017E0C File Offset: 0x0001600C
		protected override bool? ShouldAlwaysInvoke()
		{
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "[{0}] This is an escalation type responder and should always be invoked.", base.GetType().FullName, null, "ShouldAlwaysInvoke", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1452);
			return new bool?(true);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017E44 File Offset: 0x00016044
		protected void CreateRunspace()
		{
			if (string.IsNullOrWhiteSpace(base.Definition.Account) || string.IsNullOrWhiteSpace(base.Definition.Endpoint))
			{
				EscalateResponderBase.SetActiveMonitoringCertificateSettings(base.Definition);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "No authentication values were defined in EscalateResponderWorkDefinition. Certification settings have now been set.", null, "CreateRunspace", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1469);
			}
			if (!string.IsNullOrWhiteSpace(base.Definition.AccountPassword))
			{
				this.remotePowerShell = RemotePowerShell.CreateRemotePowerShellByCredential(new Uri(base.Definition.Endpoint), base.Definition.Account, base.Definition.AccountPassword, this.GetEscalationEnvironment() != EscalationEnvironment.OutsideIn);
				return;
			}
			if (base.Definition.Endpoint.Contains(";"))
			{
				this.remotePowerShell = RemotePowerShell.CreateRemotePowerShellByCertificate(base.Definition.Endpoint.Split(new char[]
				{
					';'
				}), base.Definition.Account, this.GetEscalationEnvironment() != EscalationEnvironment.OutsideIn);
				return;
			}
			this.remotePowerShell = RemotePowerShell.CreateRemotePowerShellByCertificate(new Uri(base.Definition.Endpoint), base.Definition.Account, this.GetEscalationEnvironment() != EscalationEnvironment.OutsideIn);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017F81 File Offset: 0x00016181
		internal virtual void LogCustomUnhealthyEvent(EscalateResponderBase.UnhealthyMonitoringEvent unhealthyEvent)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017F83 File Offset: 0x00016183
		internal virtual string GetFFOForestName()
		{
			return string.Empty;
		}

		// Token: 0x060005B5 RID: 1461
		internal abstract EscalationEnvironment GetEscalationEnvironment();

		// Token: 0x060005B6 RID: 1462
		internal abstract ScopeMappingEndpoint GetScopeMappingEndpoint();

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017F8C File Offset: 0x0001618C
		public static string GetNonLocalizableResourceAsString(Assembly manifestAssembly, string resourceName)
		{
			string result;
			using (Stream manifestResourceStream = manifestAssembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00017FE4 File Offset: 0x000161E4
		private static void SetExtensionAttribute(XmlDocument xml, string attributeName, string attributeValue)
		{
			if (!string.IsNullOrWhiteSpace(attributeValue))
			{
				if (xml["ExtensionAttributes"].Attributes[attributeName] == null)
				{
					xml["ExtensionAttributes"].Attributes.Append(xml.CreateAttribute(attributeName));
				}
				xml["ExtensionAttributes"].Attributes[attributeName].Value = attributeValue;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001804C File Offset: 0x0001624C
		private void Escalate(MonitorResult monitorResult, EscalationState escalationState)
		{
			ServiceHealthStatus serviceHealthStatus;
			if (base.Definition.TargetHealthState == ServiceHealthStatus.None)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "EscalateResponderBase.Escalate: TargetHealthState was not defined and ManagedAvailability was not used -- proceed to escalate using health state: {0}", ServiceHealthStatus.Unrecoverable.ToString(), null, "Escalate", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1593);
				serviceHealthStatus = ServiceHealthStatus.Unrecoverable;
			}
			else
			{
				serviceHealthStatus = base.Definition.TargetHealthState;
			}
			EscalateResponderBase.UnhealthyMonitoringEvent unhealthyMonitoringEvent = this.GetUnhealthyMonitoringEvent(monitorResult, !base.Definition.AlwaysEscalateOnMonitorChanges && this.IncludeHealthSetEscalationInfo);
			this.LogCustomUnhealthyEvent(unhealthyMonitoringEvent);
			if (serviceHealthStatus != ServiceHealthStatus.Healthy && this.ShouldRaiseActiveMonitoringAlerts(this.GetEscalationEnvironment()))
			{
				string service;
				if (!string.IsNullOrWhiteSpace(this.escalationService))
				{
					service = this.escalationService;
				}
				else if (!string.IsNullOrWhiteSpace(base.Definition.EscalationService))
				{
					service = base.Definition.EscalationService;
				}
				else
				{
					service = monitorResult.Component.Service;
				}
				string text;
				if (!string.IsNullOrWhiteSpace(this.escalationTeam))
				{
					text = this.escalationTeam;
				}
				else if (!string.IsNullOrWhiteSpace(base.Definition.EscalationTeam))
				{
					text = base.Definition.EscalationTeam;
				}
				else
				{
					text = monitorResult.Component.EscalationTeam;
				}
				string alertSource = "LocalActiveMonitoring";
				bool isDatacenter = this.GetEscalationEnvironment() == EscalationEnvironment.Datacenter;
				string environment = string.Empty;
				string location = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeLocation")) ? base.Definition.Attributes["ScopeLocation"] : null;
				string forest = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeForest")) ? base.Definition.Attributes["ScopeForest"] : null;
				string dag = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeDag")) ? base.Definition.Attributes["ScopeDag"] : null;
				string site = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeSite")) ? base.Definition.Attributes["ScopeSite"] : null;
				string region = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeRegion")) ? base.Definition.Attributes["ScopeRegion"] : null;
				string capacityUnit = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeCapacityUnit")) ? base.Definition.Attributes["ScopeCapacityUnit"] : null;
				string rack = (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn && base.Definition.Attributes.ContainsKey("ScopeRack")) ? base.Definition.Attributes["ScopeRack"] : null;
				NotificationServiceClass notificationServiceClass = this.escalationNotificationType ?? base.Definition.NotificationServiceClass;
				string alertCategory;
				switch (notificationServiceClass)
				{
				case NotificationServiceClass.UrgentInTraining:
					alertCategory = NotificationServiceClass.UrgentInTraining.ToString();
					break;
				case NotificationServiceClass.Scheduled:
					alertCategory = NotificationServiceClass.Scheduled.ToString();
					break;
				default:
					alertCategory = null;
					break;
				}
				bool urgent = escalationState == EscalationState.Red;
				if (notificationServiceClass == NotificationServiceClass.UrgentInTraining || notificationServiceClass == NotificationServiceClass.Scheduled)
				{
					urgent = false;
				}
				if (EscalateResponderBase.IsFfoMachine)
				{
					this.FfoEscalationOverrides(ref service, ref text);
					site = this.GetExchangeLabsServiceTag();
					location = this.GetExchangeLabsDatacenterName();
					forest = this.GetFFOForestName();
					string exchangeLabsServiceName;
					if ((exchangeLabsServiceName = this.GetExchangeLabsServiceName()) != null)
					{
						if (!(exchangeLabsServiceName == "FopeDevTest"))
						{
							if (!(exchangeLabsServiceName == "FopeDogfood"))
							{
								if (exchangeLabsServiceName == "FopeProd")
								{
									environment = "FSPROD";
								}
							}
							else
							{
								environment = "FSDF";
							}
						}
						else
						{
							environment = "TEST";
						}
					}
				}
				Guid alertGuid = Guid.NewGuid();
				base.Result.ResponseResource = alertGuid.ToString();
				this.InvokeNewServiceAlert(alertGuid, base.Definition.AlertTypeId, unhealthyMonitoringEvent.Subject, unhealthyMonitoringEvent.Message, monitorResult.ExecutionStartTime, text, service, alertSource, isDatacenter, urgent, environment, location, forest, dag, site, region, capacityUnit, rack, alertCategory);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018464 File Offset: 0x00016664
		private ProbeResult GetLastFailedProbeResult(MonitorResult monitorResult)
		{
			if (this.lastFailedProbeResult == null && monitorResult.LastFailedProbeResultId > 0)
			{
				this.lastFailedProbeResult = base.Broker.GetProbeResult(monitorResult.LastFailedProbeId, monitorResult.LastFailedProbeResultId).ExecuteAsync(this.LocalCancellationToken, base.TraceContext).Result;
			}
			return this.lastFailedProbeResult;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000184BC File Offset: 0x000166BC
		private EscalateResponderBase.UnhealthyMonitoringEvent GetUnhealthyMonitoringEvent(MonitorResult monitorResult, bool includeHealthSetEscalationInfo = false)
		{
			string text;
			string message;
			this.GetEscalationSubjectAndMessage(monitorResult, out text, out message, false, null);
			string name = monitorResult.Component.Name;
			if (includeHealthSetEscalationInfo)
			{
				string format = EscalateResponderBase.HealthSetEscalationSubjectPrefix;
				if (base.Definition.AlertTypeId.IndexOf("MaintenanceFailureMonitor") == 0)
				{
					format = EscalateResponderBase.HealthSetMaintenanceEscalationSubjectPrefix;
				}
				text = string.Format(format, name, monitorResult.ResultName, text);
				EscalateResponderBase.EscalationHelper.ExtendEscalationMessage(name, ref message);
			}
			if (ScopeMappingEndpoint.IsSystemMonitoringEnvironment)
			{
				this.ExtendEscalationSubjectAndMessageForSystemMonitoring(monitorResult, ref text, ref message);
			}
			if (this.GetEscalationEnvironment() == EscalationEnvironment.OutsideIn)
			{
				text = string.Format("{0}: {1}", Settings.HostedServiceName, text);
			}
			return new EscalateResponderBase.UnhealthyMonitoringEvent
			{
				HealthSet = name,
				Subject = text,
				Message = message,
				Monitor = monitorResult.ResultName
			};
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000185F8 File Offset: 0x000167F8
		private void ExtendEscalationSubjectAndMessageForSystemMonitoring(MonitorResult monitorResult, ref string escalationSubject, ref string escalationMessage)
		{
			string sourceScope = monitorResult.SourceScope;
			string name = monitorResult.Component.Name;
			if (string.IsNullOrWhiteSpace(sourceScope))
			{
				return;
			}
			if (!monitorResult.IsAlert)
			{
				return;
			}
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(sourceScope, monitorResult.ExecutionStartTime - TimeSpan.FromSeconds((double)base.Definition.MinimumSecondsBetweenEscalates));
			Dictionary<string, List<ProbeResult>> healthSetsMap = new Dictionary<string, List<ProbeResult>>();
			probeResults.ExecuteAsync(delegate(ProbeResult result)
			{
				List<ProbeResult> list4;
				if (!healthSetsMap.TryGetValue(result.HealthSetName, out list4))
				{
					list4 = new List<ProbeResult>();
					healthSetsMap.Add(result.HealthSetName, list4);
				}
				list4.Add(result);
			}, this.localCancellationToken, base.TraceContext).Wait(this.localCancellationToken);
			if (healthSetsMap.Count == 0)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "No probe results found for scope '{0}'.", sourceScope, null, "ExtendEscalationSubjectAndMessageForSystemMonitoring", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1851);
				return;
			}
			List<ProbeResult> list;
			if (!healthSetsMap.TryGetValue(name, out list))
			{
				WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to get the probe results for scope '{0}' and health set '{1}'.", sourceScope, name, null, "ExtendEscalationSubjectAndMessageForSystemMonitoring", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1859);
				return;
			}
			list.Sort((ProbeResult x, ProbeResult y) => y.ExecutionEndTime.CompareTo(x.ExecutionEndTime));
			ProbeResult probeResult = list.Find((ProbeResult r) => r.ResultType == ResultType.Failed);
			if (probeResult != null)
			{
				string valueFromDataXml = this.GetValueFromDataXml("EscalationSubject", probeResult.Data);
				if (!string.IsNullOrWhiteSpace(valueFromDataXml))
				{
					escalationSubject = string.Format("{0}: {1}", escalationSubject, valueFromDataXml);
				}
				string valueFromDataXml2 = this.GetValueFromDataXml("EscalationMessage", probeResult.Data);
				if (!string.IsNullOrWhiteSpace(valueFromDataXml2))
				{
					escalationMessage = string.Format("{0}{1}{2}", escalationMessage, "<br><br><hr><br>", valueFromDataXml2);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("<b>States of all monitors within the '{0}' health set:</b><br>", name);
				stringBuilder.AppendFormat("Note: Data may be stale.<br><br>", new object[0]);
				string[] headers = new string[]
				{
					"Name",
					"State",
					"Source",
					"Type"
				};
				List<string[]> list2 = new List<string[]>(list.Count);
				foreach (ProbeResult probeResult2 in list)
				{
					list2.Add(new string[]
					{
						probeResult2.ResultName,
						(probeResult2.ResultType == ResultType.Succeeded) ? "Healthy" : "Unhealthy",
						probeResult2.StateAttribute11,
						probeResult2.StateAttribute12
					});
				}
				stringBuilder.AppendFormat("{0}", TableDecorator.CreateTable(headers, list2));
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("<br><br><b>States of all health sets within the '{0}' {1} scope:</b><br>", sourceScope, probeResult.ScopeType);
				stringBuilder2.AppendFormat("Note: Data may be stale.<br><br>", new object[0]);
				string[] headers2 = new string[]
				{
					"HealthSet",
					"State",
					"LastTransitionTime",
					"MonitorCount"
				};
				List<string[]> list3 = new List<string[]>(healthSetsMap.Count);
				foreach (KeyValuePair<string, List<ProbeResult>> healthSetResults in healthSetsMap)
				{
					list3.Add(this.AggregateHealthSet(healthSetResults));
				}
				stringBuilder2.AppendFormat("{0}", TableDecorator.CreateTable(headers2, list3));
				escalationMessage = string.Format("{0}{1}{2}{3}{1}", new object[]
				{
					escalationMessage,
					"<br><br><hr><br>",
					stringBuilder.ToString(),
					stringBuilder2.ToString()
				});
				return;
			}
			WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Unable to find the last failed probe result for scope '{0}' and health set '{1}'.", sourceScope, name, null, "ExtendEscalationSubjectAndMessageForSystemMonitoring", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 1923);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000189E8 File Offset: 0x00016BE8
		private string GetValueFromDataXml(string name, string data)
		{
			try
			{
				XElement xelement = XElement.Parse(data);
				return xelement.Element(name).Value;
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018A28 File Offset: 0x00016C28
		private string[] AggregateHealthSet(KeyValuePair<string, List<ProbeResult>> healthSetResults)
		{
			List<string> list = new List<string>();
			list.Add(healthSetResults.Key);
			bool flag = true;
			DateTime t = DateTime.MinValue;
			foreach (ProbeResult probeResult in healthSetResults.Value)
			{
				flag &= (probeResult.ResultType == ResultType.Succeeded);
				if (t < probeResult.ExecutionEndTime)
				{
					t = probeResult.ExecutionEndTime;
				}
			}
			list.Add(flag ? "Healthy" : "Unhealthy");
			list.Add(t.ToString());
			list.Add(healthSetResults.Value.Count.ToString());
			return list.ToArray();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018AFC File Offset: 0x00016CFC
		private void AddScopeNotification(MonitorResult monitorResult, string escalationSubject = null, string escalationMessage = null)
		{
			if (!base.Definition.UploadScopeNotification)
			{
				return;
			}
			if (this.GetEscalationEnvironment() == EscalationEnvironment.OnPrem)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(monitorResult.TargetScopes))
			{
				return;
			}
			try
			{
				ProbeResult probeResult = this.GetLastFailedProbeResult(monitorResult);
				string sourceInstanceType;
				if (base.Broker.IsLocal())
				{
					sourceInstanceType = "LAM";
				}
				else if (ScopeMappingEndpoint.IsSystemMonitoringEnvironment)
				{
					sourceInstanceType = "SM";
				}
				else
				{
					sourceInstanceType = "XAM";
				}
				bool isMultiSourceInstance = !base.Broker.IsLocal();
				string text = null;
				if (!string.IsNullOrWhiteSpace(escalationSubject) && !string.IsNullOrWhiteSpace(escalationMessage))
				{
					text = string.Format("<EscalationSubject><![CDATA[{0}]]></EscalationSubject><EscalationMessage><![CDATA[{1}]]></EscalationMessage>", escalationSubject, escalationMessage);
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					text = string.Format("<Data>{0}</Data>", text);
				}
				string name = monitorResult.Component.Name;
				string[] array = monitorResult.TargetScopes.Split(new char[]
				{
					';'
				});
				foreach (string scopeName in array)
				{
					ScopeNotificationCache.Instance.AddScopeNotificationRawData(new ScopeNotificationRawData
					{
						NotificationName = string.Format("Escalation/{0}/{1}", name, monitorResult.ResultName),
						ScopeName = scopeName,
						HealthSetName = name,
						HealthState = (int)this.TranslateHealthState(monitorResult.HealthState),
						MachineName = Environment.MachineName,
						SourceInstanceName = ((!string.IsNullOrWhiteSpace(Settings.InstanceName)) ? Settings.InstanceName : Environment.MachineName),
						SourceInstanceType = sourceInstanceType,
						IsMultiSourceInstance = isMultiSourceInstance,
						ExecutionStartTime = (monitorResult.FirstAlertObservedTime ?? monitorResult.ExecutionStartTime),
						ExecutionEndTime = monitorResult.ExecutionEndTime,
						Error = ((probeResult != null) ? probeResult.Error : null),
						Exception = ((probeResult != null) ? probeResult.Exception : null),
						ExecutionContext = ((probeResult != null) ? probeResult.ExecutionContext : null),
						FailureContext = ((probeResult != null) ? probeResult.FailureContext : null),
						Data = text
					});
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to add scope data to cache: {0}", ex.ToString(), null, "AddScopeNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\EscalateResponderBase.cs", 2078);
				if (base.Definition.UploadScopeNotification && base.Definition.SuppressEscalation)
				{
					throw;
				}
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018D70 File Offset: 0x00016F70
		private ResultType TranslateHealthState(ServiceHealthStatus healthStateValue)
		{
			switch (healthStateValue)
			{
			case ServiceHealthStatus.None:
			case ServiceHealthStatus.Healthy:
				return ResultType.Succeeded;
			default:
				return ResultType.Failed;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018D94 File Offset: 0x00016F94
		private void FfoEscalationOverrides(ref string escalationService, ref string escalationTeam)
		{
			string key;
			if (escalationService == "Exchange" && (key = escalationTeam) != null)
			{
				if (<PrivateImplementationDetails>{9749D367-AD09-4C78-AF69-A8924CA98201}.$$method0x600055d-1 == null)
				{
					<PrivateImplementationDetails>{9749D367-AD09-4C78-AF69-A8924CA98201}.$$method0x600055d-1 = new Dictionary<string, int>(10)
					{
						{
							"Central Admin",
							0
						},
						{
							"Deployment",
							1
						},
						{
							"Directory and LiveId Auth",
							2
						},
						{
							"High Availability",
							3
						},
						{
							"Monitoring",
							4
						},
						{
							"Networking",
							5
						},
						{
							"Ops support",
							6
						},
						{
							"Optics",
							7
						},
						{
							"Performance",
							8
						},
						{
							"Security",
							9
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{9749D367-AD09-4C78-AF69-A8924CA98201}.$$method0x600055d-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
						escalationService = "EOP";
						escalationTeam = "Deployment and Configuration Management";
						return;
					case 1:
						escalationService = "EOP";
						escalationTeam = "Deployment and Configuration Management";
						return;
					case 2:
						escalationService = "EOP";
						escalationTeam = "Directory and Database Infrastructure";
						return;
					case 3:
						escalationService = "EOP";
						escalationTeam = "Service Automation & Monitoring";
						return;
					case 4:
						escalationService = "EOP";
						escalationTeam = "Service Automation & Monitoring";
						return;
					case 5:
						escalationService = "EOP";
						escalationTeam = "Deployment and Configuration Management";
						return;
					case 6:
						escalationService = "EOP";
						escalationTeam = "Ops SE";
						return;
					case 7:
						escalationService = "EOP";
						escalationTeam = "Intelligence and Reporting";
						return;
					case 8:
						escalationService = "EOP";
						escalationTeam = "Intelligence and Reporting";
						return;
					case 9:
						escalationService = "EOP";
						escalationTeam = "Ops SE";
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00018F28 File Offset: 0x00017128
		private string GetExchangeLabsServiceName()
		{
			string result = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs", false))
			{
				if (registryKey != null)
				{
					result = (string)registryKey.GetValue("ServiceName", string.Empty);
				}
			}
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00018F84 File Offset: 0x00017184
		private string GetExchangeLabsServiceTag()
		{
			string result = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs", false))
			{
				if (registryKey != null)
				{
					result = (string)registryKey.GetValue("ServiceTag", string.Empty);
				}
			}
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018FE0 File Offset: 0x000171E0
		private string GetExchangeLabsDatacenterName()
		{
			string result = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs", false))
			{
				if (registryKey != null)
				{
					result = (string)registryKey.GetValue("Datacenter", string.Empty);
				}
			}
			return result;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019048 File Offset: 0x00017248
		private Task<ResponderResult> GetLastSuccessfulRecoveryAttemptedResponderResult(CancellationToken cancellationToken)
		{
			Task<ResponderResult> lastResponderResultById = this.GetLastResponderResultById(cancellationToken);
			return this.ContinueOnTaskWithFallback<ResponderResult>(lastResponderResultById, (CancellationToken cancellation) => this.GetLastResponderResultByName(cancellation), cancellationToken);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019074 File Offset: 0x00017274
		private Task<ResponderResult> GetLastResponderResultById(CancellationToken cancellationToken)
		{
			IDataAccessQuery<ResponderResult> lastSuccessfulRecoveryAttemptedResponderResult = base.Broker.GetLastSuccessfulRecoveryAttemptedResponderResult(base.Definition, DateTime.UtcNow - SqlDateTime.MinValue.Value);
			return lastSuccessfulRecoveryAttemptedResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000190BC File Offset: 0x000172BC
		private Task<ResponderResult> GetLastResponderResultByName(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute6 = 1.0;
			IDataAccessQuery<ResponderResult> lastSuccessfulRecoveryAttemptedResponderResultByName = base.Broker.GetLastSuccessfulRecoveryAttemptedResponderResultByName(base.Definition, DateTime.UtcNow - SqlDateTime.MinValue.Value);
			return lastSuccessfulRecoveryAttemptedResponderResultByName.ExecuteAsync(cancellationToken, base.TraceContext);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00019118 File Offset: 0x00017318
		private Task<TResult> ContinueOnTaskWithFallback<TResult>(Task<TResult> task, Func<CancellationToken, Task<TResult>> fallback, CancellationToken cancellationToken)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			this.ContinueOnTaskWithFallback<TResult>(task, fallback, taskCompletionSource, cancellationToken);
			return taskCompletionSource.Task;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000191D0 File Offset: 0x000173D0
		private void ContinueOnTaskWithFallback<TResult>(Task<TResult> task, Func<CancellationToken, Task<TResult>> fallback, TaskCompletionSource<TResult> completionSource, CancellationToken cancellationToken)
		{
			task.Continue(delegate(TResult result)
			{
				try
				{
					task.Wait();
				}
				catch (Exception exception)
				{
					if (fallback == null)
					{
						completionSource.SetException(exception);
						return;
					}
				}
				if (result != null || fallback == null)
				{
					completionSource.SetResult(result);
					return;
				}
				this.ContinueOnTaskWithFallback<TResult>(fallback(cancellationToken), null, completionSource, cancellationToken);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x040003C5 RID: 965
		internal const string MaintenanceEscalationAlertTypeId = "MaintenanceFailureMonitor";

		// Token: 0x040003C6 RID: 966
		private const string LoadFromResourceAttributeValueName = "LoadFromResourceAttributeValue";

		// Token: 0x040003C7 RID: 967
		public const string AlertSource = "LocalActiveMonitoring";

		// Token: 0x040003C8 RID: 968
		private string escalationService;

		// Token: 0x040003C9 RID: 969
		private string escalationTeam;

		// Token: 0x040003CA RID: 970
		private NotificationServiceClass? escalationNotificationType = null;

		// Token: 0x040003CB RID: 971
		private string customEscalationSubject;

		// Token: 0x040003CC RID: 972
		private string customEscalationMessage;

		// Token: 0x040003CD RID: 973
		private static readonly string ActiveMonitoringRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x040003CE RID: 974
		protected static string DefaultEscalationSubjectInternal = "Exchange Server Alert: The {0} Health Set detected {1} is unhealthy.";

		// Token: 0x040003CF RID: 975
		protected static string DefaultEscalationMessageInternal = "The {0} Health Set has detected a problem with {1} at {2}. Attempts to auto-recover from this condition have failed and requires Administrator attention.";

		// Token: 0x040003D0 RID: 976
		protected static string HealthSetEscalationSubjectPrefixInternal = "{0} Health Set unhealthy ({1}) - {2}";

		// Token: 0x040003D1 RID: 977
		protected static string HealthSetMaintenanceEscalationSubjectPrefixInternal = "{0} Health Set maintenance unhealthy ({1}) - {2}";

		// Token: 0x040003D2 RID: 978
		protected static HealthSetEscalationHelper EscalationHelperInternal = new HealthSetEscalationHelper();

		// Token: 0x040003D3 RID: 979
		private static string localMachineVersion = null;

		// Token: 0x040003D4 RID: 980
		private RemotePowerShell remotePowerShell;

		// Token: 0x040003D5 RID: 981
		private ProbeResult lastFailedProbeResult;

		// Token: 0x040003D6 RID: 982
		private CancellationToken localCancellationToken;

		// Token: 0x0200005A RID: 90
		protected enum ResponseAction
		{
			// Token: 0x040003DA RID: 986
			Escalate,
			// Token: 0x040003DB RID: 987
			Defer,
			// Token: 0x040003DC RID: 988
			DeferNonBusinessHours,
			// Token: 0x040003DD RID: 989
			DeferHealthSetSuppression,
			// Token: 0x040003DE RID: 990
			Throttled,
			// Token: 0x040003DF RID: 991
			EscalationSuppressed
		}

		// Token: 0x0200005B RID: 91
		internal static class AttributeNames
		{
			// Token: 0x040003E0 RID: 992
			internal const string MinimumSecondsBetweenEscalates = "MinimumSecondsBetweenEscalates";
		}

		// Token: 0x0200005C RID: 92
		internal class DefaultValues
		{
			// Token: 0x040003E1 RID: 993
			internal const int MinimumSecondsBetweenEscalates = 14400;
		}

		// Token: 0x0200005D RID: 93
		internal class UnhealthyMonitoringEvent
		{
			// Token: 0x170001DD RID: 477
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001927F File Offset: 0x0001747F
			// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00019287 File Offset: 0x00017487
			internal string HealthSet { get; set; }

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00019290 File Offset: 0x00017490
			// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00019298 File Offset: 0x00017498
			internal string Subject { get; set; }

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000192A1 File Offset: 0x000174A1
			// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000192A9 File Offset: 0x000174A9
			internal string Message { get; set; }

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000192B2 File Offset: 0x000174B2
			// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000192BA File Offset: 0x000174BA
			internal string Monitor { get; set; }
		}
	}
}
