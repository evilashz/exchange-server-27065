using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000038 RID: 56
	internal class SystemWorkloadManager : DisposeTrackableBase, IDiagnosable, ITaskProviderManager
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00008FC0 File Offset: 0x000071C0
		private SystemWorkloadManager(IWorkloadLogger logger, bool ignoreImplicitLocalCpuResource)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.perfCounters = new WorkloadManagementPerfCounterWrapper();
			this.resourceReservationContext = new ResourceReservationContext(ignoreImplicitLocalCpuResource);
			this.classificationBlocks = new ClassificationDictionary<ClassificationBlock>((WorkloadClassification classification) => new ClassificationBlock(classification)
			{
				FairnessFactor = SystemWorkloadManager.ReadAppSettingAsInt(classification.ToString() + "Factor", this.GetDefaultFactor(classification))
			});
			this.workloadExecution = new WorkloadExecution(this);
			this.logger = new WorkloadManagementLogger(logger);
			this.classificationUpdateTimer = new Timer(new TimerCallback(this.ClassificationUpdate), null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009070 File Offset: 0x00007270
		public static WorkloadExecutionStatus Status
		{
			get
			{
				SystemWorkloadManager systemWorkloadManager = SystemWorkloadManager.manager;
				if (systemWorkloadManager != null)
				{
					return systemWorkloadManager.workloadExecution.Status;
				}
				return WorkloadExecutionStatus.NotInitialized;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009093 File Offset: 0x00007293
		public static void Initialize(IWorkloadLogger logger)
		{
			SystemWorkloadManager.Initialize(logger, true);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000909C File Offset: 0x0000729C
		public static void Initialize(IWorkloadLogger logger, bool registerDiagnostics)
		{
			SystemWorkloadManager.InternalInitialize(logger, registerDiagnostics, false);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000090A8 File Offset: 0x000072A8
		public static void Shutdown()
		{
			SystemWorkloadManager systemWorkloadManager = SystemWorkloadManager.manager;
			if (systemWorkloadManager != null)
			{
				lock (systemWorkloadManager.instanceLock)
				{
					if (SystemWorkloadManager.manager != null)
					{
						SystemWorkloadManager.manager = null;
					}
					else
					{
						systemWorkloadManager = null;
					}
				}
			}
			if (systemWorkloadManager != null)
			{
				systemWorkloadManager.Dispose();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009108 File Offset: 0x00007308
		public static void RegisterWorkload(SystemWorkloadBase workload)
		{
			SystemWorkloadManager systemWorkloadManager = SystemWorkloadManager.manager;
			if (systemWorkloadManager == null)
			{
				throw new InvalidOperationException("System workload manager is not initialized.");
			}
			systemWorkloadManager.InternalRegisterWorkload(workload);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009130 File Offset: 0x00007330
		public static void UnregisterWorkload(SystemWorkloadBase workload)
		{
			SystemWorkloadManager systemWorkloadManager = SystemWorkloadManager.manager;
			if (systemWorkloadManager == null)
			{
				throw new InvalidOperationException("System workload manager is not initialized.");
			}
			systemWorkloadManager.InternalUnregisterWorkload(workload);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009159 File Offset: 0x00007359
		public static bool IsClassificationActive(WorkloadClassification classification)
		{
			if (SystemWorkloadManager.manager == null)
			{
				throw new InvalidOperationException("System workload manager is not initialized.");
			}
			return SystemWorkloadManager.manager.classificationBlocks[classification].WorkloadCount > 0;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009185 File Offset: 0x00007385
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "SystemWorkloadManager";
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000918C File Offset: 0x0000738C
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("SystemWorkloadManager");
			if (string.IsNullOrEmpty(parameters.Argument) || string.Equals(parameters.Argument, "help", StringComparison.OrdinalIgnoreCase))
			{
				xelement.Add(new XElement("help", "Supported arguments are Workload, Task, Resource, Admission, Policy, History,."));
			}
			else
			{
				if (0 <= parameters.Argument.IndexOf("workload", StringComparison.OrdinalIgnoreCase))
				{
					this.GenerateWorkloadDiagnosticsInfo(xelement);
				}
				if (0 <= parameters.Argument.IndexOf("task", StringComparison.OrdinalIgnoreCase))
				{
					this.GenerateTaskDiagnosticsInfo(xelement);
				}
				if (0 <= parameters.Argument.IndexOf("resource", StringComparison.OrdinalIgnoreCase))
				{
					this.GenerateResourceDiagnosticsInfo(xelement);
				}
				if (0 <= parameters.Argument.IndexOf("history", StringComparison.OrdinalIgnoreCase))
				{
					this.GenerateBlackBoxDiagnosticsInfo(xelement);
				}
				if (0 <= parameters.Argument.IndexOf("admission", StringComparison.OrdinalIgnoreCase))
				{
					this.GenerateAdmissionDiagnosticsInfo(xelement);
				}
				if (0 <= parameters.Argument.IndexOf("policy", StringComparison.OrdinalIgnoreCase))
				{
					this.GeneratePoliciesDiagnosticsInfo(xelement);
				}
			}
			return xelement;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009294 File Offset: 0x00007494
		ITaskProvider ITaskProviderManager.GetNextProvider()
		{
			ITaskProvider result;
			lock (this.instanceLock)
			{
				if (this.fairnessAssignments == null || this.fairnessAssignments.Length == 0)
				{
					result = null;
				}
				else
				{
					ClassificationBlock classificationBlock = this.fairnessAssignments[this.fairnessAssignmentCursor];
					this.fairnessAssignmentCursor++;
					if (this.fairnessAssignmentCursor >= this.fairnessAssignments.Length)
					{
						this.fairnessAssignmentCursor = 0;
					}
					classificationBlock.Activate();
					result = new TaskProvider(classificationBlock);
				}
			}
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009328 File Offset: 0x00007528
		int ITaskProviderManager.GetProviderCount()
		{
			int result;
			lock (this.instanceLock)
			{
				if (this.fairnessAssignments == null)
				{
					result = 0;
				}
				else
				{
					result = this.fairnessAssignments.Length;
				}
			}
			return result;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009378 File Offset: 0x00007578
		internal static void InitializeForTesting()
		{
			SystemWorkloadManager.InitializeForTesting(false);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009380 File Offset: 0x00007580
		internal static void InitializeForTesting(bool ignoreImplicitLocalCpuResource)
		{
			SystemWorkloadManager.InitializeForTesting(DummyWorkloadLogger.Instance, ignoreImplicitLocalCpuResource);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000938D File Offset: 0x0000758D
		internal static void InitializeForTesting(IWorkloadLogger logger)
		{
			SystemWorkloadManager.InitializeForTesting(logger, false);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009396 File Offset: 0x00007596
		internal static void InitializeForTesting(IWorkloadLogger logger, bool ignoreImplicitLocalCpuResource)
		{
			if (SystemWorkloadManager.manager != null)
			{
				SystemWorkloadManager.Shutdown();
			}
			SystemWorkloadManager.InternalInitialize(logger, false, ignoreImplicitLocalCpuResource);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000093AC File Offset: 0x000075AC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.classificationUpdateTimer.Dispose();
				this.workloadExecution.Dispose();
				this.UnregisterDiagnosticInfo();
				this.resourceReservationContext.Dispose();
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000093D8 File Offset: 0x000075D8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SystemWorkloadManager>(this);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000093E0 File Offset: 0x000075E0
		private static void InternalInitialize(IWorkloadLogger logger, bool registerDiagnostics, bool ignoreImplicitLocalCpuResource)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (SystemWorkloadManager.manager != null)
			{
				throw new InvalidOperationException("System workload manager is already initialized.");
			}
			SettingOverrideSync.Instance.Start(true);
			SystemWorkloadManager systemWorkloadManager = new SystemWorkloadManager(logger, ignoreImplicitLocalCpuResource);
			if (registerDiagnostics)
			{
				systemWorkloadManager.RegisterDiagnosticInfo();
			}
			SystemWorkloadManager.manager = systemWorkloadManager;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009430 File Offset: 0x00007630
		private static int ReadAppSettingAsInt(string keyName, int defaultValue)
		{
			string s = ConfigurationManager.AppSettings[keyName];
			int num;
			if (int.TryParse(s, out num) && num > 0 && num < 100)
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009460 File Offset: 0x00007660
		private void GenerateWorkloadDiagnosticsInfo(XElement componentElement)
		{
			lock (this.instanceLock)
			{
				foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
				{
					SystemWorkloadBase[] workloads = classificationBlock.GetWorkloads();
					if (workloads != null)
					{
						XElement xelement = new XElement("Workloads");
						componentElement.Add(xelement);
						foreach (SystemWorkloadBase systemWorkloadBase in workloads)
						{
							XElement xelement2 = new XElement("Workload");
							xelement.Add(xelement2);
							xelement2.Add(new XElement("Identity", systemWorkloadBase.Id));
							xelement2.Add(new XElement("Type", systemWorkloadBase.WorkloadType));
							xelement2.Add(new XElement("Classification", systemWorkloadBase.Classification));
							xelement2.Add(new XElement("Registered", systemWorkloadBase.Registered));
							xelement2.Add(new XElement("Paused", systemWorkloadBase.Paused));
							xelement2.Add(new XElement("TaskCount", systemWorkloadBase.TaskCount));
							xelement2.Add(new XElement("BlockedTaskCount", systemWorkloadBase.BlockedTaskCount));
						}
					}
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009640 File Offset: 0x00007840
		private void GenerateTaskDiagnosticsInfo(XElement componentElement)
		{
			XElement xelement = new XElement("Tasks");
			componentElement.Add(xelement);
			lock (this.instanceLock)
			{
				int num = 0;
				foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
				{
					SystemWorkloadBase[] workloads = classificationBlock.GetWorkloads();
					if (workloads != null)
					{
						foreach (SystemWorkloadBase systemWorkloadBase in workloads)
						{
							SystemTaskBase[] runningTasks = systemWorkloadBase.GetRunningTasks();
							if (runningTasks != null)
							{
								num += runningTasks.Length;
								foreach (SystemTaskBase systemTaskBase in runningTasks)
								{
									XElement xelement2 = new XElement("Task");
									xelement.Add(xelement2);
									xelement2.Add(new XElement("Identity", systemTaskBase.Identity));
									xelement2.Add(new XElement("WorkloadIdentity", systemWorkloadBase.Id));
									xelement2.Add(new XElement("WorkloadType", systemWorkloadBase.WorkloadType));
									xelement2.Add(new XElement("Classification", systemWorkloadBase.Classification));
									xelement2.Add(new XElement("CreationTime", systemTaskBase.CreationTime));
									if (systemTaskBase.ResourceReservation != null)
									{
										foreach (ResourceKey content in systemTaskBase.ResourceReservation.Resources)
										{
											xelement2.Add(new XElement("ResourceKey", content));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009890 File Offset: 0x00007A90
		private void GenerateResourceDiagnosticsInfo(XElement componentElement)
		{
			XElement xelement = new XElement("Resources");
			componentElement.Add(xelement);
			lock (this.instanceLock)
			{
				foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
				{
					if (SystemWorkloadManager.IsClassificationActive(classificationBlock.WorkloadClassification))
					{
						foreach (ResourceKey resourceKey in ResourceHealthMonitorManager.Singleton.ResourceKeys)
						{
							XElement xelement2 = new XElement("Resource");
							xelement.Add(xelement2);
							xelement2.Add(new XElement("ResourceKey", resourceKey));
							xelement2.Add(new XElement("Classification", classificationBlock.WorkloadClassification));
							IResourceLoadMonitor resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(resourceKey);
							xelement2.Add(new XElement("Update", resourceLoadMonitor.LastUpdateUtc));
							ResourceLoad resourceLoad = resourceLoadMonitor.GetResourceLoad(classificationBlock.WorkloadClassification, false, null);
							xelement2.Add(new XElement("Load", resourceLoad));
							if (resourceLoad.Info != null)
							{
								xelement2.Add(new XElement("Info", resourceLoad.Info.ToString()));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009A78 File Offset: 0x00007C78
		private void GenerateAdmissionDiagnosticsInfo(XElement componentElement)
		{
			XElement xelement = new XElement("Admissions");
			componentElement.Add(xelement);
			lock (this.instanceLock)
			{
				foreach (IResourceAdmissionControl resourceAdmissionControl in this.resourceReservationContext.AdmissionControlManager.Values)
				{
					DefaultAdmissionControl defaultAdmissionControl = (DefaultAdmissionControl)resourceAdmissionControl;
					XElement xelement2 = new XElement("Admission");
					xelement.Add(xelement2);
					xelement2.Add(new XElement("Resource", defaultAdmissionControl.ResourceKey));
					xelement2.Add(new XElement("IsAcquired", defaultAdmissionControl.IsAcquired));
					xelement2.Add(new XElement("LastRefresh", defaultAdmissionControl.LastRefreshUtc));
					xelement2.Add(new XElement("RefreshCycle", defaultAdmissionControl.RefreshCycle));
					xelement2.Add(new XElement("MaxConcurrency", defaultAdmissionControl.MaxConcurrency));
					foreach (object obj2 in Enum.GetValues(typeof(WorkloadClassification)))
					{
						WorkloadClassification workloadClassification = (WorkloadClassification)obj2;
						if (workloadClassification != WorkloadClassification.Unknown)
						{
							XElement xelement3 = new XElement(workloadClassification.ToString());
							xelement2.Add(xelement3);
							xelement3.Add(new XElement("ActiveConcurrency", defaultAdmissionControl.GetActiveConcurrency(workloadClassification)));
							double num = 0.0;
							xelement3.Add(new XElement("ConcurrencyLimit", defaultAdmissionControl.GetConcurrencyLimit(workloadClassification, out num)));
							xelement3.Add(new XElement("DelayFactor", num));
						}
					}
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009CE0 File Offset: 0x00007EE0
		private void GenerateBlackBoxDiagnosticsInfo(XElement componentElement)
		{
			XElement xelement = new XElement("History");
			componentElement.Add(xelement);
			foreach (SystemWorkloadManagerLogEntry systemWorkloadManagerLogEntry in SystemWorkloadManagerBlackBox.GetRecords(false))
			{
				XElement xelement2 = new XElement("Entry");
				xelement.Add(xelement2);
				xelement2.Add(new XElement("Type", systemWorkloadManagerLogEntry.Type));
				xelement2.Add(new XElement("ResourceKey", systemWorkloadManagerLogEntry.Resource));
				xelement2.Add(new XElement("Classification", systemWorkloadManagerLogEntry.Classification));
				this.GenerateBlackBoxEventDiagnosticsInfo(xelement2, systemWorkloadManagerLogEntry.Type, systemWorkloadManagerLogEntry.CurrentEvent);
				if (systemWorkloadManagerLogEntry.PreviousEvent != null)
				{
					XElement xelement3 = new XElement("Previous");
					xelement2.Add(xelement3);
					this.GenerateBlackBoxEventDiagnosticsInfo(xelement3, systemWorkloadManagerLogEntry.Type, systemWorkloadManagerLogEntry.PreviousEvent);
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009DE4 File Offset: 0x00007FE4
		private void GenerateBlackBoxEventDiagnosticsInfo(XElement parent, SystemWorkloadManagerLogEntryType entryType, SystemWorkloadManagerEvent blackBoxEvent)
		{
			parent.Add(new XElement("DateTime", blackBoxEvent.DateTime));
			parent.Add(new XElement("Load", blackBoxEvent.Load));
			if (blackBoxEvent.Load.Info != null)
			{
				parent.Add(new XElement("Info", blackBoxEvent.Load.Info.ToString()));
			}
			if (entryType == SystemWorkloadManagerLogEntryType.Admission)
			{
				parent.Add(new XElement("ConcurrencyLimit", blackBoxEvent.Slots));
				parent.Add(new XElement("Delayed", blackBoxEvent.Delayed));
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009EB0 File Offset: 0x000080B0
		private void GeneratePoliciesDiagnosticsInfo(XElement componentElement)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			XElement xelement = new XElement("Policy");
			componentElement.Add(xelement);
			foreach (object obj in Enum.GetValues(typeof(ResourceMetricType)))
			{
				ResourceMetricType resourceMetricType = (ResourceMetricType)obj;
				if (resourceMetricType != ResourceMetricType.None && resourceMetricType != ResourceMetricType.Remote)
				{
					XElement xelement2 = new XElement("ResourcePolicy");
					xelement2.Add(new XElement("MetricType", resourceMetricType));
					xelement2.Add(new XElement("MaxConcurrency", snapshot.WorkloadManagement.GetObject<IResourceSettings>(resourceMetricType, new object[0]).MaxConcurrency));
					foreach (object obj2 in Enum.GetValues(typeof(WorkloadClassification)))
					{
						WorkloadClassification workloadClassification = (WorkloadClassification)obj2;
						if (workloadClassification != WorkloadClassification.Unknown)
						{
							ResourceMetricPolicy resourceMetricPolicy = new ResourceMetricPolicy(resourceMetricType, workloadClassification, snapshot);
							xelement2.Add(resourceMetricPolicy.GetDiagnosticInfo());
						}
					}
					xelement.Add(xelement2);
				}
			}
			foreach (object obj3 in Enum.GetValues(typeof(WorkloadType)))
			{
				WorkloadType workloadType = (WorkloadType)obj3;
				if (workloadType != WorkloadType.Unknown)
				{
					WorkloadPolicy workloadPolicy = new WorkloadPolicy(workloadType, snapshot);
					xelement.Add(workloadPolicy.GetDiagnosticInfo());
				}
			}
			ISystemWorkloadManagerSettings systemWorkloadManager = snapshot.WorkloadManagement.SystemWorkloadManager;
			XElement xelement3 = new XElement("WorkloadManagerPolicy");
			xelement3.Add(new XElement("MaxConcurrency", systemWorkloadManager.MaxConcurrency));
			xelement3.Add(new XElement("RefreshCycle", systemWorkloadManager.RefreshCycle));
			xelement.Add(xelement3);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A104 File Offset: 0x00008304
		private void InternalRegisterWorkload(SystemWorkloadBase workload)
		{
			if (workload == null)
			{
				throw new ArgumentNullException("workload");
			}
			WorkloadClassification classification = workload.Classification;
			if (classification == WorkloadClassification.Unknown)
			{
				throw new ArgumentException("Workload classification cannot be Unknown.", "workload");
			}
			if (workload.Registered)
			{
				throw new InvalidOperationException(string.Format("Workload {0} is already registered", workload.Id));
			}
			int num = 0;
			ClassificationBlock classificationBlock = null;
			try
			{
				classificationBlock = this.classificationBlocks[classification];
			}
			catch (KeyNotFoundException innerException)
			{
				throw new InvalidOperationException(string.Format("Classification {0} was not found in classification block dictionary.", classification), innerException);
			}
			lock (this.instanceLock)
			{
				workload.SetResourceReservationContext(this.resourceReservationContext);
				classificationBlock.AddWorkload(workload);
				num = this.GetWorkloadCount();
				this.factorDenominator += classificationBlock.FairnessFactor;
				this.RecalculateFairnessMap();
				if (this.workloadExecution.Status != WorkloadExecutionStatus.Started)
				{
					this.workloadExecution.Start();
				}
			}
			this.perfCounters.UpdateWorkloadCount((long)num);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A218 File Offset: 0x00008418
		private bool InternalUnregisterWorkload(SystemWorkloadBase workload)
		{
			if (workload == null)
			{
				throw new ArgumentNullException("workload");
			}
			int num = 0;
			bool flag = false;
			lock (this.instanceLock)
			{
				if (!workload.Registered)
				{
					return false;
				}
				ClassificationBlock classificationBlock = workload.ClassificationBlock;
				flag = classificationBlock.RemoveWorkload(workload);
				workload.SetResourceReservationContext(null);
				if (flag)
				{
					num = this.GetWorkloadCount();
					this.factorDenominator -= classificationBlock.FairnessFactor;
					this.RecalculateFairnessMap();
				}
				if (this.factorDenominator == 0)
				{
					this.workloadExecution.Stop();
				}
			}
			if (flag)
			{
				this.perfCounters.UpdateWorkloadCount((long)num);
			}
			return flag;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A2D4 File Offset: 0x000084D4
		private void RegisterDiagnosticInfo()
		{
			if (!this.registered)
			{
				ProcessAccessManager.RegisterComponent(this);
				this.registered = true;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A2EB File Offset: 0x000084EB
		private void UnregisterDiagnosticInfo()
		{
			if (this.registered)
			{
				ProcessAccessManager.UnregisterComponent(this);
				this.registered = false;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A304 File Offset: 0x00008504
		private void ClassificationUpdate(object state)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
			{
				bool flag = false;
				do
				{
					flag = false;
					lock (this.instanceLock)
					{
						SystemWorkloadBase[] workloads = classificationBlock.GetWorkloads();
						if (workloads != null)
						{
							foreach (SystemWorkloadBase systemWorkloadBase in workloads)
							{
								WorkloadClassification classification = snapshot.WorkloadManagement.GetObject<IWorkloadSettings>(systemWorkloadBase.WorkloadType, new object[0]).Classification;
								if (classification != classificationBlock.WorkloadClassification)
								{
									if (classificationBlock.RemoveWorkload(systemWorkloadBase))
									{
										this.factorDenominator -= classificationBlock.FairnessFactor;
										ClassificationBlock classificationBlock2 = this.classificationBlocks[classification];
										classificationBlock2.AddWorkload(systemWorkloadBase);
										this.factorDenominator += classificationBlock2.FairnessFactor;
										this.RecalculateFairnessMap();
									}
									flag = true;
									break;
								}
							}
						}
					}
				}
				while (flag);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A458 File Offset: 0x00008658
		private void RecalculateFairnessMap()
		{
			int num = 0;
			lock (this.instanceLock)
			{
				this.fairnessAssignmentCursor = 0;
				if (this.factorDenominator == 0)
				{
					this.fairnessAssignments = null;
					return;
				}
				this.fairnessAssignments = new ClassificationBlock[this.factorDenominator];
				int num2 = 0;
				foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
				{
					if (classificationBlock.WorkloadCount > 0)
					{
						num++;
						SystemWorkloadManagerBlackBox.AddActiveClassification(classificationBlock.WorkloadClassification);
					}
					for (int i = 0; i < classificationBlock.FairnessFactor * classificationBlock.WorkloadCount; i++)
					{
						this.fairnessAssignments[num2] = classificationBlock;
						num2++;
					}
				}
			}
			this.perfCounters.UpdateActiveClassifications((long)num);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A554 File Offset: 0x00008754
		private int GetWorkloadCount()
		{
			int num = 0;
			lock (this.instanceLock)
			{
				foreach (ClassificationBlock classificationBlock in this.classificationBlocks.Values)
				{
					num += classificationBlock.WorkloadCount;
				}
			}
			return num;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A5DC File Offset: 0x000087DC
		private int GetDefaultFactor(WorkloadClassification classification)
		{
			switch (classification)
			{
			case WorkloadClassification.Unknown:
				return 0;
			case WorkloadClassification.Discretionary:
				return 1;
			case WorkloadClassification.InternalMaintenance:
				return 2;
			case WorkloadClassification.CustomerExpectation:
				return 4;
			case WorkloadClassification.Urgent:
				return 8;
			default:
				throw new ArgumentException("Unexpected classification: " + classification, "classification");
			}
		}

		// Token: 0x0400010C RID: 268
		private const int DefaultUrgentFactor = 8;

		// Token: 0x0400010D RID: 269
		private const int DefaultCustomerExpectationFactor = 4;

		// Token: 0x0400010E RID: 270
		private const int DefaultInternalMaintenanceFactor = 2;

		// Token: 0x0400010F RID: 271
		private const int DefaultDiscretionaryFactor = 1;

		// Token: 0x04000110 RID: 272
		private const string ProcessAccessManagerComponentName = "SystemWorkloadManager";

		// Token: 0x04000111 RID: 273
		private static SystemWorkloadManager manager;

		// Token: 0x04000112 RID: 274
		private ResourceReservationContext resourceReservationContext;

		// Token: 0x04000113 RID: 275
		private ClassificationDictionary<ClassificationBlock> classificationBlocks;

		// Token: 0x04000114 RID: 276
		private ClassificationBlock[] fairnessAssignments;

		// Token: 0x04000115 RID: 277
		private int fairnessAssignmentCursor;

		// Token: 0x04000116 RID: 278
		private object instanceLock = new object();

		// Token: 0x04000117 RID: 279
		private WorkloadExecution workloadExecution;

		// Token: 0x04000118 RID: 280
		private int factorDenominator;

		// Token: 0x04000119 RID: 281
		private WorkloadManagementLogger logger;

		// Token: 0x0400011A RID: 282
		private bool registered;

		// Token: 0x0400011B RID: 283
		private WorkloadManagementPerfCounterWrapper perfCounters;

		// Token: 0x0400011C RID: 284
		private Timer classificationUpdateTimer;
	}
}
