using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016B RID: 363
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ComponentInfoBasedTask : Task
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x0003D184 File Offset: 0x0003B384
		protected ComponentInfoBasedTask()
		{
			this.monadConnection = new MonadConnection("pooled=false");
			this.monadConnection.Open();
			if (ExManagementApplicationLogger.IsLowEventCategoryEnabled(4))
			{
				this.IsCmdletLogEntriesEnabled = true;
				base.Fields["CmdletLogEntriesEnabled"] = new bool?(true);
			}
			base.Fields["InstallationMode"] = InstallationModes.Unknown;
			base.Fields["IsDatacenter"] = new SwitchParameter(false);
			base.Fields["IsDatacenterDedicated"] = new SwitchParameter(false);
			base.Fields["IsPartnerHosted"] = new SwitchParameter(false);
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(CmdletAttribute), false);
			this.taskVerb = ((CmdletAttribute)customAttributes[0]).VerbName;
			this.taskNoun = ((CmdletAttribute)customAttributes[0]).NounName;
			this.implementsResume = true;
			this.isResuming = false;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003D2AA File Offset: 0x0003B4AA
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ComponentInfoBaseTaskModuleFactory();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003D2B1 File Offset: 0x0003B4B1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.monadConnection != null)
			{
				this.monadConnection.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
		private bool IsCmdletLogEntriesEnabled { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D3C RID: 3388
		protected abstract LocalizedString Description { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0003D2E1 File Offset: 0x0003B4E1
		protected virtual bool IsInnerRunspaceRBACEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		protected virtual bool IsInnerRunspaceThrottlingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0003D2E7 File Offset: 0x0003B4E7
		protected virtual ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientApplication
		{
			get
			{
				return ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003D2EA File Offset: 0x0003B4EA
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x0003D2F2 File Offset: 0x0003B4F2
		protected bool ShouldWriteLogFile
		{
			get
			{
				return this.shouldWriteLogFile;
			}
			set
			{
				this.shouldWriteLogFile = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0003D2FB File Offset: 0x0003B4FB
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0003D303 File Offset: 0x0003B503
		protected bool IsTenantOrganization
		{
			get
			{
				return this.isTenantOrganization;
			}
			set
			{
				this.isTenantOrganization = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0003D30C File Offset: 0x0003B50C
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0003D314 File Offset: 0x0003B514
		protected bool ShouldLoadDatacenterConfigFile
		{
			get
			{
				return this.shouldLoadDatacenterConfigFile;
			}
			set
			{
				this.shouldLoadDatacenterConfigFile = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0003D31D File Offset: 0x0003B51D
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x0003D334 File Offset: 0x0003B534
		protected virtual InstallationModes InstallationMode
		{
			get
			{
				return (InstallationModes)base.Fields["InstallationMode"];
			}
			set
			{
				base.Fields["InstallationMode"] = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003D34C File Offset: 0x0003B54C
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0003D363 File Offset: 0x0003B563
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0003D376 File Offset: 0x0003B576
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0003D38D File Offset: 0x0003B58D
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDatacenter
		{
			get
			{
				return (SwitchParameter)base.Fields["IsDatacenter"];
			}
			set
			{
				base.Fields["IsDatacenter"] = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0003D3A5 File Offset: 0x0003B5A5
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDatacenterDedicated
		{
			get
			{
				return (SwitchParameter)base.Fields["IsDatacenterDedicated"];
			}
			set
			{
				base.Fields["IsDatacenterDedicated"] = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0003D3D4 File Offset: 0x0003B5D4
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0003D3EB File Offset: 0x0003B5EB
		[Parameter(Mandatory = false)]
		public SwitchParameter IsPartnerHosted
		{
			get
			{
				return (SwitchParameter)base.Fields["IsPartnerHosted"];
			}
			set
			{
				base.Fields["IsPartnerHosted"] = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003D403 File Offset: 0x0003B603
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0003D40B File Offset: 0x0003B60B
		internal List<string> ComponentInfoFileNames
		{
			get
			{
				return this.componentInfoFileNames;
			}
			set
			{
				this.componentInfoFileNames = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0003D414 File Offset: 0x0003B614
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x0003D41C File Offset: 0x0003B61C
		internal SetupComponentInfoCollection ComponentInfoList
		{
			get
			{
				return this.componentInfoList;
			}
			set
			{
				this.componentInfoList = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0003D425 File Offset: 0x0003B625
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x0003D42D File Offset: 0x0003B62D
		internal bool ImplementsResume
		{
			get
			{
				return this.implementsResume;
			}
			set
			{
				this.implementsResume = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0003D436 File Offset: 0x0003B636
		protected string Platform
		{
			get
			{
				if (IntPtr.Size != 8)
				{
					return "i386";
				}
				return "amd64";
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003D44B File Offset: 0x0003B64B
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0003D462 File Offset: 0x0003B662
		[Parameter(Mandatory = false)]
		public LongPath UpdatesDir
		{
			get
			{
				return (LongPath)base.Fields["UpdatesDir"];
			}
			set
			{
				base.Fields["UpdatesDir"] = value;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003D475 File Offset: 0x0003B675
		protected virtual void CheckInstallationMode()
		{
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003D477 File Offset: 0x0003B677
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (ExchangePropertyContainer.IsCmdletLogEnabled(base.SessionState))
			{
				this.IsCmdletLogEntriesEnabled = true;
			}
			this.PropagateExchangePropertyContainer();
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003D49C File Offset: 0x0003B69C
		private void PropagateExchangePropertyContainer()
		{
			ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication = this.ClientApplication;
			if (this.IsInnerRunspaceThrottlingEnabled && base.ExchangeRunspaceConfig != null && clientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown)
			{
				clientApplication = base.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication;
			}
			ADServerSettings adServerSettingsOverride = null;
			base.CurrentTaskContext.TryGetItem<ADServerSettings>("CmdletServerSettings", ref adServerSettingsOverride);
			ExchangePropertyContainer.PropagateExchangePropertyContainer(base.SessionState, this.monadConnection.RunspaceProxy, this.IsInnerRunspaceRBACEnabled, this.IsInnerRunspaceThrottlingEnabled, adServerSettingsOverride, clientApplication);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003D50D File Offset: 0x0003B70D
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			if (this.IsCmdletLogEntriesEnabled)
			{
				this.IsCmdletLogEntriesEnabled = false;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003D524 File Offset: 0x0003B724
		protected override void InternalStopProcessing()
		{
			base.InternalStopProcessing();
			if (this.IsCmdletLogEntriesEnabled)
			{
				this.IsCmdletLogEntriesEnabled = false;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003D53C File Offset: 0x0003B73C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.CheckInstallationMode();
			ConfigurationStatus configurationStatus = new ConfigurationStatus(this.taskNoun);
			RolesUtility.GetConfiguringStatus(ref configurationStatus);
			if (this.ImplementsResume && configurationStatus.Action != InstallationModes.Unknown && configurationStatus.Watermark != null)
			{
				this.isResuming = true;
				if (configurationStatus.Action != this.InstallationMode && (configurationStatus.Action != InstallationModes.Install || this.InstallationMode != InstallationModes.Uninstall))
				{
					base.WriteError(new IllegalResumptionException(configurationStatus.Action.ToString(), this.InstallationMode.ToString()), ErrorCategory.InvalidOperation, null);
				}
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003D5EC File Offset: 0x0003B7EC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.ComponentInfoFileNames == null || this.ComponentInfoFileNames.Count == 0)
				{
					throw new NoComponentInfoFilesException();
				}
				this.ComponentInfoList = new SetupComponentInfoCollection();
				try
				{
					foreach (string path in this.ComponentInfoFileNames)
					{
						string fileName = Path.Combine(Role.SetupComponentInfoFilePath, path);
						this.ComponentInfoList.Add(RolesUtility.ReadSetupComponentInfoFile(fileName));
					}
				}
				catch (FileNotFoundException exception)
				{
					base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
				}
				catch (XmlDeserializationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidData, null);
				}
				this.GenerateAndExecuteTaskScript(this.IsTenantOrganization ? InstallationCircumstances.TenantOrganization : InstallationCircumstances.Standalone);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		protected virtual void PopulateContextVariables()
		{
			DateTime dateTime = (DateTime)ExDateTime.Now;
			base.Fields["InvocationID"] = string.Format("{0}{1:0000}{2}", dateTime.ToString("yyyyMMdd-HHmmss"), dateTime.Millisecond, ComponentInfoBasedTask.random.Next());
			base.Fields["ProductPlatform"] = ((IntPtr.Size == 8) ? "amd64" : "i386");
			if (this.ShouldLoadDatacenterConfigFile && this.InstallationMode != InstallationModes.Uninstall)
			{
				ParameterCollection parameterCollection = RolesUtility.ReadSetupParameters(this.IsDatacenter || this.IsDatacenterDedicated);
				foreach (Parameter parameter in parameterCollection)
				{
					base.Fields["Datacenter" + parameter.Name] = parameter.EffectiveValue;
				}
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		protected virtual void SetRunspaceVariables()
		{
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003D7EA File Offset: 0x0003B9EA
		protected virtual bool ShouldExecuteComponentTasks()
		{
			return base.ShouldProcess(this.taskNoun, this.taskVerb, null);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003D800 File Offset: 0x0003BA00
		internal bool GenerateAndExecuteTaskScript(InstallationCircumstances installationCircumstance)
		{
			this.completedSteps = 0;
			bool flag = false;
			ConfigurationStatus configurationStatus = new ConfigurationStatus(this.taskNoun, this.InstallationMode);
			string text = string.Format("{0}-{1}", this.taskVerb, this.taskNoun);
			TaskLogger.LogEnter();
			bool flag2 = this.ShouldExecuteComponentTasks();
			StringBuilder stringBuilder = new StringBuilder();
			List<TaskInfo>.Enumerator enumerator = default(List<TaskInfo>.Enumerator);
			this.PopulateContextVariables();
			try
			{
				string path = string.Format("{0}-{1}.ps1", text, base.Fields["InvocationID"]);
				string text2 = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, path);
				base.WriteVerbose(Strings.WritingInformationScript(text2));
				if (this.shouldWriteLogFile)
				{
					this.logFileStream = new StreamWriter(text2);
					this.logFileStream.AutoFlush = true;
				}
				this.WriteLogFile(Strings.SetupLogHeader(this.taskNoun, this.taskVerb, (DateTime)ExDateTime.Now));
				this.WriteLogFile(Strings.VariablesSection);
				if (base.ServerSettings != null)
				{
					this.monadConnection.RunspaceProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, base.ServerSettings);
				}
				this.SetRunspaceVariables();
				SortedList<string, object> sortedList = new SortedList<string, object>();
				foreach (object obj in base.Fields)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					sortedList.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
				foreach (KeyValuePair<string, object> keyValuePair in sortedList)
				{
					string text3 = this.GenerateScriptVarCommand(keyValuePair.Key, keyValuePair.Value);
					this.WriteLogFile(text3);
					if (flag2)
					{
						this.ExecuteScript(text3, false, 0, LocalizedString.Empty);
					}
				}
				this.FilterComponents();
				if (this.InstallationMode == InstallationModes.Uninstall)
				{
					base.WriteVerbose(Strings.ReversingTaskList);
					this.ComponentInfoList.Reverse();
					foreach (SetupComponentInfo setupComponentInfo in this.ComponentInfoList)
					{
						setupComponentInfo.Tasks.Reverse();
					}
				}
				List<SetupComponentInfo>.Enumerator enumerator5 = this.ComponentInfoList.GetEnumerator();
				bool flag3 = false;
				this.FindStartingTask(ref enumerator5, ref enumerator, ref flag3, this.InstallationMode, installationCircumstance);
				using (enumerator5)
				{
					bool flag4 = true;
					bool flag5 = true;
					this.WriteLogFile(Strings.ComponentTaskSection);
					this.totalSteps = this.CountStepsToBeExecuted(this.ComponentInfoList, this.InstallationMode, installationCircumstance);
					while (flag4)
					{
						SetupComponentInfo setupComponentInfo2 = enumerator5.Current;
						string name = setupComponentInfo2.Name;
						LocalizedString localizedString;
						if (string.IsNullOrEmpty(setupComponentInfo2.DescriptionId))
						{
							localizedString = Strings.SetupProgressGenericComponent;
						}
						else
						{
							try
							{
								Strings.IDs key = (Strings.IDs)Enum.Parse(typeof(Strings.IDs), setupComponentInfo2.DescriptionId, false);
								localizedString = Strings.GetLocalizedString(key);
							}
							catch (ArgumentException)
							{
								localizedString = Strings.SetupProgressGenericComponent;
							}
						}
						base.WriteVerbose(Strings.ProcessingComponent(name, localizedString));
						this.WriteLogFile(Strings.ComponentSection(name));
						while (flag5)
						{
							TaskInfo taskInfo = enumerator.Current;
							string task = taskInfo.GetTask(this.InstallationMode, installationCircumstance);
							if (string.IsNullOrEmpty(task))
							{
								flag5 = enumerator.MoveNext();
							}
							else if (!this.IsTaskIncluded(taskInfo, enumerator5.Current))
							{
								flag5 = enumerator.MoveNext();
							}
							else
							{
								string text4 = task;
								string id = taskInfo.GetID();
								int weight = taskInfo.GetWeight(this.InstallationMode);
								bool flag6 = !flag3 && taskInfo.IsFatal(this.InstallationMode);
								flag3 = false;
								string description = taskInfo.GetDescription(this.InstallationMode);
								LocalizedString localizedString2;
								if (string.IsNullOrEmpty(description))
								{
									localizedString2 = localizedString;
								}
								else
								{
									try
									{
										Strings.IDs key2 = (Strings.IDs)Enum.Parse(typeof(Strings.IDs), description, false);
										localizedString2 = Strings.GetLocalizedString(key2);
									}
									catch (ArgumentException)
									{
										localizedString2 = localizedString;
									}
								}
								this.WriteLogFile(string.Format("# [ID = {0:x}, Wt = {1}, isFatal = {2}] \"{3}\"", new object[]
								{
									taskInfo.GetID(),
									weight,
									flag6,
									localizedString2
								}));
								this.WriteLogFile(ExDateTime.Now + ":" + text4);
								if (flag2)
								{
									configurationStatus.Watermark = id;
									if (this.ImplementsResume)
									{
										RolesUtility.SetConfiguringStatus(configurationStatus);
									}
									if (!text4.Contains("\n"))
									{
										text4 = "\n\t" + text4 + "\n\n";
									}
									ExDateTime now = ExDateTime.Now;
									bool flag7 = this.ExecuteScript(text4, !flag6, weight, localizedString2);
									TimeSpan timeSpan = ExDateTime.Now - now;
									if (ComponentInfoBasedTask.monitoredCmdlets.Contains(text.ToLowerInvariant()) && timeSpan.CompareTo(this.executionTimeThreshold) > 0)
									{
										if (taskInfo is ServicePlanTaskInfo)
										{
											ServicePlanTaskInfo servicePlanTaskInfo = (ServicePlanTaskInfo)taskInfo;
											stringBuilder.AppendLine(string.Format("Task {0}__{1} had execution time: {2}.", servicePlanTaskInfo.FileId, servicePlanTaskInfo.FeatureName, timeSpan.ToString()));
										}
										else
										{
											stringBuilder.AppendLine(string.Format("Task {0} had execution time: {1}.", taskInfo.GetID(), timeSpan.ToString()));
										}
									}
									flag = (!flag7 && flag6);
									if (flag)
									{
										base.WriteVerbose(new LocalizedString(string.Format("[ERROR-REFERENCE] Id={0} Component={1}", taskInfo.GetID(), taskInfo.Component)));
										base.WriteVerbose(Strings.HaltingExecution);
										break;
									}
								}
								flag5 = enumerator.MoveNext();
							}
						}
						if (flag)
						{
							break;
						}
						flag4 = enumerator5.MoveNext();
						if (flag4)
						{
							enumerator.Dispose();
							enumerator = enumerator5.Current.Tasks.GetEnumerator();
							flag5 = enumerator.MoveNext();
						}
					}
					base.WriteVerbose(Strings.FinishedComponentTasks);
				}
			}
			catch (Exception ex)
			{
				base.WriteVerbose(Strings.ExceptionOccured(ex.ToString()));
				flag = true;
				this.OnHalt(ex);
				throw;
			}
			finally
			{
				if (!string.IsNullOrEmpty(stringBuilder.ToString()) && ComponentInfoBasedTask.monitoredCmdlets.Contains(text.ToLowerInvariant()))
				{
					if (base.Fields["TenantName"] != null)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ExecuteTaskScriptOptic, new string[]
						{
							text,
							base.Fields["TenantName"].ToString(),
							stringBuilder.ToString()
						});
					}
					else if (base.Fields["OrganizationHierarchicalPath"] != null)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ExecuteTaskScriptOptic, new string[]
						{
							text,
							base.Fields["OrganizationHierarchicalPath"].ToString(),
							stringBuilder.ToString()
						});
					}
					else
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ExecuteTaskScriptOptic, new string[]
						{
							text,
							string.Empty,
							stringBuilder.ToString()
						});
					}
				}
				if (flag)
				{
					if (this.IsCmdletLogEntriesEnabled)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ComponentTaskFailed, this.GetComponentEventParameters(this.GetVerboseInformation(this.GetCmdletLogEntries())));
					}
					base.WriteProgress(this.Description, Strings.ProgressStatusFailed, 100);
				}
				else
				{
					if (this.IsCmdletLogEntriesEnabled)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ComponentTaskExecutedSuccessfully, this.GetComponentEventParameters(this.GetVerboseInformation(this.GetCmdletLogEntries())));
					}
					base.WriteProgress(this.Description, Strings.ProgressStatusCompleted, 100);
					if (flag2)
					{
						RolesUtility.ClearConfiguringStatus(configurationStatus);
						if (text == "Start-PostSetup")
						{
							foreach (string roleName in base.Fields["Roles"].ToString().Split(new char[]
							{
								','
							}))
							{
								RolesUtility.SetPostSetupVersion(roleName, (Version)base.Fields["TargetVersion"]);
							}
							RolesUtility.SetPostSetupVersion("AdminTools", (Version)base.Fields["TargetVersion"]);
						}
					}
				}
				if (this.logFileStream != null)
				{
					this.logFileStream.Close();
					this.logFileStream = null;
				}
				enumerator.Dispose();
				TaskLogger.LogExit();
			}
			return !flag;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		protected virtual void OnHalt(Exception e)
		{
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003E169 File Offset: 0x0003C369
		protected virtual void FilterComponents()
		{
			this.ComponentInfoList.RemoveAll(delegate(SetupComponentInfo component)
			{
				if (!component.IsDatacenterOnly && !component.IsPartnerHostedOnly && !component.IsDatacenterDedicatedOnly)
				{
					return false;
				}
				if (this.IsDatacenter)
				{
					return !component.IsDatacenterOnly;
				}
				if (this.IsDatacenterDedicated)
				{
					return !component.IsDatacenterDedicatedOnly;
				}
				return !this.IsPartnerHosted || !component.IsPartnerHostedOnly;
			});
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003E183 File Offset: 0x0003C383
		private void WriteLogFile(string line)
		{
			if (this.shouldWriteLogFile)
			{
				this.logFileStream.WriteLine(line);
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003E19C File Offset: 0x0003C39C
		private string GenerateScriptVarCommand(object varName, object varValue)
		{
			string result = string.Empty;
			if (varValue == null)
			{
				result = string.Format("$Role{0} = $null", varName);
			}
			else if (varValue is bool || varValue is SwitchParameter)
			{
				result = string.Format("$Role{0} = ${1}", varName, varValue);
			}
			else if (varValue is string[])
			{
				result = string.Format("$Role{0} = {1}", varName, Globals.PowerShellArrayFromStringArray((string[])varValue));
			}
			else if (varValue is MultiValuedProperty<string>)
			{
				result = string.Format("$Role{0} = {1}", varName, Globals.PowerShellArrayFromStringArray(((MultiValuedProperty<string>)varValue).ToArray()));
			}
			else if (varValue is MultiValuedProperty<Capability>)
			{
				MultiValuedProperty<Capability> multiValuedProperty = varValue as MultiValuedProperty<Capability>;
				MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
				foreach (Capability capability in multiValuedProperty)
				{
					multiValuedProperty2.Add(capability.ToString());
				}
				result = string.Format("$Role{0} = {1}", varName, Globals.PowerShellArrayFromStringArray(multiValuedProperty2.ToArray()));
			}
			else if (varValue is Version)
			{
				Version version = (Version)varValue;
				result = string.Format("$Role{0} = '{1}.{2:D2}.{3:D4}.{4:D3}'", new object[]
				{
					varName,
					version.Major,
					version.Minor,
					version.Build,
					version.Revision
				});
			}
			else
			{
				result = string.Format("$Role{0} = '{1}'", varName, varValue.ToString().Replace("'", "''"));
			}
			return result;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003E340 File Offset: 0x0003C540
		private void FindStartingTask(ref List<SetupComponentInfo>.Enumerator componentEnumerator, ref List<TaskInfo>.Enumerator taskEnumerator, ref bool nextTaskNonFatal, InstallationModes installationMode, InstallationCircumstances installationCircumstance)
		{
			bool flag = false;
			if (this.ImplementsResume && this.isResuming && installationMode != InstallationModes.BuildToBuildUpgrade)
			{
				ConfigurationStatus configurationStatus = new ConfigurationStatus(this.taskNoun, this.InstallationMode);
				RolesUtility.GetConfiguringStatus(ref configurationStatus);
				base.WriteVerbose(Strings.LookingForTask(configurationStatus.Action.ToString(), configurationStatus.Watermark));
				while (!flag && componentEnumerator.MoveNext())
				{
					taskEnumerator = componentEnumerator.Current.Tasks.GetEnumerator();
					while (!flag && taskEnumerator.MoveNext())
					{
						if (taskEnumerator.Current.GetID() == configurationStatus.Watermark)
						{
							flag = true;
							if (this.InstallationMode == InstallationModes.Uninstall && configurationStatus.Action == InstallationModes.Install)
							{
								nextTaskNonFatal = true;
							}
						}
						else if (!string.IsNullOrEmpty(taskEnumerator.Current.GetTask(installationMode, installationCircumstance)))
						{
							this.completedSteps += taskEnumerator.Current.GetWeight(installationMode);
						}
					}
				}
				if (!flag)
				{
					base.WriteVerbose(Strings.CouldNotFindTask);
					this.completedSteps = 0;
				}
			}
			if (!flag)
			{
				componentEnumerator = this.ComponentInfoList.GetEnumerator();
				while (componentEnumerator.MoveNext())
				{
					SetupComponentInfo setupComponentInfo = componentEnumerator.Current;
					taskEnumerator = setupComponentInfo.Tasks.GetEnumerator();
					if (taskEnumerator.MoveNext())
					{
						flag = true;
						break;
					}
					base.WriteVerbose(Strings.ComponentEmpty(componentEnumerator.Current.Name));
				}
				if (!flag)
				{
					throw new EmptyTaskListException();
				}
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003E4BC File Offset: 0x0003C6BC
		protected bool ExecuteScript(string script, bool handleError, int subSteps, LocalizedString statusDescription)
		{
			bool flag = false;
			if (this.IsCmdletLogEntriesEnabled)
			{
				this.GetCmdletLogEntries().IncreaseIndentation();
			}
			try
			{
				flag = this.InternalExecuteScript(script, handleError, subSteps, statusDescription);
			}
			finally
			{
				if (this.IsCmdletLogEntriesEnabled)
				{
					if (!flag)
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ScriptExecutionFailed, this.GetScriptEventParameters(this.GetVerboseInformation(this.GetCmdletLogEntries())));
					}
					else
					{
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ScriptExecutionSuccessfully, this.GetScriptEventParameters(this.GetVerboseInformation(this.GetCmdletLogEntries())));
					}
					this.GetCmdletLogEntries().DecreaseIndentation();
				}
			}
			return flag;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0003E588 File Offset: 0x0003C788
		private bool InternalExecuteScript(string script, bool handleError, int subSteps, LocalizedString statusDescription)
		{
			bool result = false;
			WorkUnit workUnit = new WorkUnit();
			bool newSubProgressReceived = false;
			int completedSubSteps = 0;
			try
			{
				script.TrimEnd(new char[]
				{
					'\n'
				});
				string script2 = script.Replace("\n", "\r\n");
				if (handleError)
				{
					base.WriteVerbose(Strings.ExecutingScriptNonFatal(script2));
				}
				else
				{
					base.WriteVerbose(Strings.ExecutingScript(script2));
				}
				script = string.Format("$error.Clear(); {0}", script);
				MonadCommand monadCommand = new MonadCommand(script, this.monadConnection);
				monadCommand.CommandType = CommandType.Text;
				monadCommand.ProgressReport += delegate(object sender, ProgressReportEventArgs e)
				{
					if (subSteps == 0)
					{
						return;
					}
					completedSubSteps = subSteps * e.ProgressRecord.PercentComplete / 100;
					newSubProgressReceived = true;
				};
				bool flag = false;
				try
				{
					TaskLogger.IncreaseIndentation();
					TaskLogger.LogErrorAsWarning = handleError;
					MonadAsyncResult monadAsyncResult = monadCommand.BeginExecute(new WorkUnit[]
					{
						workUnit
					});
					while (!flag)
					{
						flag = monadAsyncResult.AsyncWaitHandle.WaitOne(200, false);
						if (newSubProgressReceived)
						{
							base.WriteProgress(this.Description, statusDescription, (this.completedSteps + completedSubSteps) * 100 / this.totalSteps);
							newSubProgressReceived = false;
						}
						if (base.Stopping)
						{
							break;
						}
					}
					if (base.Stopping)
					{
						monadCommand.Cancel();
					}
					else
					{
						monadCommand.EndExecute(monadAsyncResult);
					}
				}
				catch (CommandExecutionException ex)
				{
					if (ex.InnerException != null)
					{
						throw new ScriptExecutionException(Strings.ErrorCommandExecutionException(script, ex.InnerException.ToString()), ex.InnerException);
					}
					throw;
				}
				finally
				{
					TaskLogger.DecreaseIndentation();
				}
				this.completedSteps += subSteps;
				result = true;
			}
			catch (CmdletInvocationException ex2)
			{
				result = false;
				if (!handleError)
				{
					throw;
				}
				base.WriteVerbose(Strings.IgnoringException(ex2.ToString()));
				base.WriteVerbose(Strings.WillContinueProcessing);
			}
			if (workUnit.Errors.Count > 0)
			{
				result = false;
				int count = workUnit.Errors.Count;
				base.WriteVerbose(Strings.ErrorDuringTaskExecution(count));
				for (int i = 0; i < count; i++)
				{
					ErrorRecord errorRecord = workUnit.Errors[i];
					base.WriteVerbose(Strings.ErrorRecordReport(errorRecord.ToString(), i));
					if (!handleError)
					{
						base.WriteVerbose(Strings.ErrorRecordReport(errorRecord.Exception.ToString(), i));
						ScriptExecutionException exception = new ScriptExecutionException(Strings.ErrorCommandExecutionException(script, errorRecord.Exception.ToString()), errorRecord.Exception);
						this.WriteError(exception, errorRecord.CategoryInfo.Category, errorRecord.TargetObject, false);
					}
				}
				if (handleError)
				{
					base.WriteVerbose(Strings.WillIgnoreNoncriticalErrors);
					base.WriteVerbose(Strings.WillContinueProcessing);
				}
			}
			return result;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003E878 File Offset: 0x0003CA78
		private static string GetValueString(object value)
		{
			if (value == null)
			{
				return "$null";
			}
			ICollection collection = value as ICollection;
			if (collection == null)
			{
				return value.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value.ToString() + " {");
			foreach (object value2 in collection)
			{
				stringBuilder.Append(ComponentInfoBasedTask.GetValueString(value2));
				stringBuilder.Append("; ");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003E928 File Offset: 0x0003CB28
		private int CountStepsToBeExecuted(SetupComponentInfoCollection componentList, InstallationModes installationMode, InstallationCircumstances installationCircumstance)
		{
			int num = 0;
			int num2 = 0;
			foreach (SetupComponentInfo setupComponentInfo in componentList)
			{
				foreach (TaskInfo taskInfo in setupComponentInfo.Tasks)
				{
					if (!string.IsNullOrEmpty(taskInfo.GetTask(installationMode, installationCircumstance)) && this.IsTaskIncluded(taskInfo, setupComponentInfo))
					{
						num2++;
						num += taskInfo.GetWeight(installationMode);
					}
				}
			}
			base.WriteVerbose(Strings.FoundTasksToExecute(num2));
			return num;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		private bool IsTaskIncluded(TaskInfo task, SetupComponentInfo parentComponent)
		{
			return !this.IsDatacenterDedicated || !parentComponent.IsDatacenterDedicatedOnly || !task.ExcludeInDatacenterDedicated;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003EA08 File Offset: 0x0003CC08
		protected string GetFqdnOrNetbiosName()
		{
			string result;
			try
			{
				result = Dns.GetHostEntry(Dns.GetHostName()).HostName;
			}
			catch (SocketException)
			{
				result = Environment.MachineName;
			}
			return result;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003EA44 File Offset: 0x0003CC44
		protected string GetNetBIOSName(string name)
		{
			string text = null;
			int num = name.IndexOf('.');
			if (num != -1)
			{
				try
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1551, "GetNetBIOSName", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\ComponentInfoBasedTask.cs");
					topologyConfigurationSession.UseConfigNC = false;
					topologyConfigurationSession.UseGlobalCatalog = true;
					ADComputer adcomputer = topologyConfigurationSession.FindComputerByHostName(name);
					if (adcomputer != null)
					{
						text = adcomputer.Name;
					}
				}
				catch (DataSourceOperationException)
				{
				}
				catch (DataSourceTransientException)
				{
				}
				catch (DataValidationException)
				{
				}
				if (text == null)
				{
					text = name.Substring(0, num);
				}
			}
			else
			{
				text = name;
			}
			return text;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003EAE4 File Offset: 0x0003CCE4
		internal CmdletLogEntries GetCmdletLogEntries()
		{
			return ExchangePropertyContainer.GetCmdletLogEntries(base.SessionState);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		private string[] GetScriptEventParameters(string verboseInfo)
		{
			return new string[]
			{
				base.ProcessId.ToString(),
				Thread.CurrentThread.ManagedThreadId.ToString(),
				verboseInfo
			};
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003EB34 File Offset: 0x0003CD34
		private string[] GetComponentEventParameters(string verboseInfo)
		{
			return new string[]
			{
				base.ProcessId.ToString(),
				Thread.CurrentThread.ManagedThreadId.ToString(),
				(base.MyInvocation.MyCommand != null) ? base.MyInvocation.MyCommand.Name : base.MyInvocation.InvocationName,
				verboseInfo
			};
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		private string GetVerboseInformation(CmdletLogEntries logEntries)
		{
			StringBuilder stringBuilder = new StringBuilder(31000);
			int num = 0;
			foreach (string text in logEntries.GetCurrentIndentationEntries())
			{
				if (num + (text.Length + 1) > 31000)
				{
					break;
				}
				num += text.Length + 1;
				stringBuilder.Append("\n");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000684 RID: 1668
		private const int MaxLogEntryStringSize = 31000;

		// Token: 0x04000685 RID: 1669
		internal MonadConnection monadConnection;

		// Token: 0x04000686 RID: 1670
		private int completedSteps;

		// Token: 0x04000687 RID: 1671
		private int totalSteps;

		// Token: 0x04000688 RID: 1672
		private static Random random = new Random();

		// Token: 0x04000689 RID: 1673
		private bool shouldWriteLogFile = true;

		// Token: 0x0400068A RID: 1674
		private StreamWriter logFileStream;

		// Token: 0x0400068B RID: 1675
		private bool shouldLoadDatacenterConfigFile = true;

		// Token: 0x0400068C RID: 1676
		private readonly TimeSpan executionTimeThreshold = new TimeSpan(0, 0, 5);

		// Token: 0x0400068D RID: 1677
		private static readonly List<string> monitoredCmdlets = new List<string>
		{
			"new-organization",
			"remove-organization"
		};

		// Token: 0x0400068E RID: 1678
		private bool isTenantOrganization;

		// Token: 0x0400068F RID: 1679
		private List<string> componentInfoFileNames;

		// Token: 0x04000690 RID: 1680
		private SetupComponentInfoCollection componentInfoList;

		// Token: 0x04000691 RID: 1681
		private readonly string taskVerb;

		// Token: 0x04000692 RID: 1682
		private readonly string taskNoun;

		// Token: 0x04000693 RID: 1683
		private bool implementsResume;

		// Token: 0x04000694 RID: 1684
		private bool isResuming;
	}
}
