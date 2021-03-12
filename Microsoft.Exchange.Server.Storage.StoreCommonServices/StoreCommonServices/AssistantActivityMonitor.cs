using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200001F RID: 31
	public sealed class AssistantActivityMonitor
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000102A8 File Offset: 0x0000E4A8
		private AssistantActivityMonitor(StoreDatabase database)
		{
			this.database = database;
			this.assistantActivityStates = new AssistantActivityState[5];
			for (int i = 0; i < 5; i++)
			{
				this.assistantActivityStates[i] = new AssistantActivityState((RequiredMaintenanceResourceType)i);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public static TimeSpan MaintenanceControlPeriod
		{
			get
			{
				return AssistantActivityMonitor.maintenanceControlPeriod;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000102EF File Offset: 0x0000E4EF
		public AssistantActivityState[] AssistantActivityStates
		{
			get
			{
				return this.assistantActivityStates;
			}
		}

		// Token: 0x17000070 RID: 112
		public AssistantActivityState this[RequiredMaintenanceResourceType requiredMaintenanceResourceType]
		{
			get
			{
				return this.assistantActivityStates[(int)requiredMaintenanceResourceType];
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00010301 File Offset: 0x0000E501
		public static AssistantActivityMonitor Instance(StoreDatabase database)
		{
			return database.ComponentData[AssistantActivityMonitor.assistantStateTrackerSlot] as AssistantActivityMonitor;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00010318 File Offset: 0x0000E518
		public static IEnumerable<AssistantActivityState> GetAssistantActivitySnapshot(Context context)
		{
			return AssistantActivityMonitor.Instance(context.Database).assistantActivityStates;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0001032C File Offset: 0x0000E52C
		public static void PublishActiveMonitoringNotification(RequiredMaintenanceResourceType resourceType, string databaseName, ResultSeverityLevel resultSeverityLevel)
		{
			NotificationItem notificationItem = new EventNotificationItem(ExchangeComponent.Store.Name, string.Format("{0}.{1}", "StoreMaintenanceHandler", resourceType), databaseName, AssistantActivityMonitor.MaintenanceControlPeriod.ToString(), resultSeverityLevel);
			notificationItem.Publish(false);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001037A File Offset: 0x0000E57A
		public void UpdateAssistantActivityState(RequiredMaintenanceResourceType requiredMaintenanceResourceType, bool maintenanceRequested)
		{
			this.assistantActivityStates[(int)requiredMaintenanceResourceType].AssistantIsActiveInLastMonitoringPeriod = true;
			if (maintenanceRequested)
			{
				this.assistantActivityStates[(int)requiredMaintenanceResourceType].LastTimeRequested = DateTime.UtcNow;
				return;
			}
			this.assistantActivityStates[(int)requiredMaintenanceResourceType].LastTimePerformed = DateTime.UtcNow;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000103B2 File Offset: 0x0000E5B2
		internal static void Initialize()
		{
			if (AssistantActivityMonitor.assistantStateTrackerSlot == -1)
			{
				AssistantActivityMonitor.assistantStateTrackerSlot = StoreDatabase.AllocateComponentDataSlot();
			}
			AssistantActivityMonitor.maintenanceControlPeriod = ConfigurationSchema.MaintenanceControlPeriod.Value;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000103D8 File Offset: 0x0000E5D8
		internal static void MountHandler(Context context, StoreDatabase database)
		{
			AssistantActivityMonitor value = new AssistantActivityMonitor(database);
			database.ComponentData[AssistantActivityMonitor.assistantStateTrackerSlot] = value;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00010400 File Offset: 0x0000E600
		internal static void MountedHandler(Context context, StoreDatabase database)
		{
			AssistantActivityMonitor assistantActivityMonitor = database.ComponentData[AssistantActivityMonitor.assistantStateTrackerSlot] as AssistantActivityMonitor;
			RecurringTask<AssistantActivityMonitor> task = new RecurringTask<AssistantActivityMonitor>(TaskExecutionWrapper<AssistantActivityMonitor>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.MaintenanceIdleCheck, ClientType.System, context.Database.MdbGuid), new TaskExecutionWrapper<AssistantActivityMonitor>.TaskCallback<Context>(assistantActivityMonitor.MaintenanceIdleCheckTask)), assistantActivityMonitor, AssistantActivityMonitor.MaintenanceControlPeriod, false);
			context.Database.TaskList.Add(task, true);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00010467 File Offset: 0x0000E667
		internal static void DismountHandler(StoreDatabase database)
		{
			database.ComponentData[AssistantActivityMonitor.assistantStateTrackerSlot] = null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0001047C File Offset: 0x0000E67C
		private void MaintenanceIdleCheckTask(Context context, AssistantActivityMonitor thisObject, Func<bool> shouldCallbackContinue)
		{
			using (context.AssociateWithDatabase(this.database))
			{
				if (this.database.IsOnlineActive)
				{
					for (int i = 0; i < 5; i++)
					{
						if (this.assistantActivityStates[i].AssistantIsActiveInLastMonitoringPeriod)
						{
							this.assistantActivityStates[i].AssistantIsActiveInLastMonitoringPeriod = false;
							AssistantActivityMonitor.PublishActiveMonitoringNotification((RequiredMaintenanceResourceType)i, this.database.MdbName, ResultSeverityLevel.Informational);
						}
						else if (this.database.MountTime < DateTime.UtcNow - AssistantActivityMonitor.MaintenanceControlPeriod)
						{
							Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MaintenanceProcessorIsIdle, new object[]
							{
								(RequiredMaintenanceResourceType)i,
								this.database.MdbName,
								AssistantActivityMonitor.MaintenanceControlPeriod.ToString()
							});
						}
					}
				}
			}
		}

		// Token: 0x040001D8 RID: 472
		public const string MaintenanceAssistantTriggerString = "StoreMaintenanceHandler";

		// Token: 0x040001D9 RID: 473
		private static int assistantStateTrackerSlot = -1;

		// Token: 0x040001DA RID: 474
		private static TimeSpan maintenanceControlPeriod;

		// Token: 0x040001DB RID: 475
		private StoreDatabase database;

		// Token: 0x040001DC RID: 476
		private AssistantActivityState[] assistantActivityStates;
	}
}
