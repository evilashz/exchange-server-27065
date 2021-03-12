using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000089 RID: 137
	internal sealed class TimeBasedAssistantControllerWrapper : SystemWorkloadBase, IDisposable
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00014B48 File Offset: 0x00012D48
		public TimeBasedAssistantControllerWrapper(TimeBasedAssistantController controller)
		{
			this.controller = controller;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00014B78 File Offset: 0x00012D78
		public TimeBasedAssistantController Controller
		{
			get
			{
				return this.controller;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014B80 File Offset: 0x00012D80
		public override WorkloadType WorkloadType
		{
			get
			{
				return this.Controller.TimeBasedAssistantType.WorkloadType;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00014B92 File Offset: 0x00012D92
		public override string Id
		{
			get
			{
				return this.Controller.TimeBasedAssistantType.Identifier.ToString();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00014BAE File Offset: 0x00012DAE
		public override int TaskCount
		{
			get
			{
				return this.GetTaskCount();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00014BB6 File Offset: 0x00012DB6
		public override int BlockedTaskCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014BB9 File Offset: 0x00012DB9
		public void Dispose()
		{
			this.controller.Dispose();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00014BC8 File Offset: 0x00012DC8
		protected override SystemTaskBase GetTask(ResourceReservationContext context)
		{
			List<Guid> list = null;
			Guid guid = this.lastProcessedDriverGuid;
			lock (this.instanceLock)
			{
				TimeBasedAssistantTask timeBasedAssistantTask = null;
				List<TimeBasedAssistantTask> list2 = new List<TimeBasedAssistantTask>();
				foreach (SystemTaskBase systemTaskBase in this.tasksWaitingExecution)
				{
					TimeBasedAssistantTask timeBasedAssistantTask2 = (TimeBasedAssistantTask)systemTaskBase;
					IEnumerable<ResourceKey> resourceDependencies = timeBasedAssistantTask2.ResourceDependencies;
					if (resourceDependencies != null)
					{
						ResourceReservation reservation = context.GetReservation(this, resourceDependencies);
						if (reservation != null)
						{
							timeBasedAssistantTask2.ResourceReservation = reservation;
							timeBasedAssistantTask = timeBasedAssistantTask2;
							break;
						}
					}
					else
					{
						list2.Add(timeBasedAssistantTask2);
					}
				}
				foreach (TimeBasedAssistantTask value in list2)
				{
					this.tasksWaitingExecution.Remove(value);
				}
				if (timeBasedAssistantTask != null)
				{
					this.tasksWaitingExecution.Remove(timeBasedAssistantTask);
					return timeBasedAssistantTask;
				}
			}
			TimeBasedDatabaseDriver nextDriver;
			ResourceReservation reservation2;
			for (;;)
			{
				nextDriver = this.Controller.GetNextDriver(guid);
				if (nextDriver == null)
				{
					break;
				}
				guid = nextDriver.DatabaseInfo.Guid;
				if (list != null && list.Contains(guid))
				{
					goto Block_4;
				}
				if (!nextDriver.HasTask())
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<Guid, LocalizedString>((long)this.GetHashCode(), "Skipping database '{0}' for assistant '{1}'. There is no task to execute.", guid, this.Controller.TimeBasedAssistantType.Name);
				}
				else
				{
					IEnumerable<ResourceKey> resourceDependencies2 = nextDriver.ResourceDependencies;
					if (resourceDependencies2 != null)
					{
						reservation2 = context.GetReservation(this, resourceDependencies2);
						if (reservation2 != null)
						{
							goto IL_1E1;
						}
						ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<Guid, LocalizedString>((long)this.GetHashCode(), "Skipping database '{0}' for assistant '{1}'. Dependent resources are not currently available for this assistant.", guid, this.Controller.TimeBasedAssistantType.Name);
					}
					else
					{
						ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<Guid, LocalizedString>((long)this.GetHashCode(), "The driver for database {0} assistant {1} did not return any resource dependencies. This is possible only when the driver is not started. Skipping tasks from this driver.", guid, this.Controller.TimeBasedAssistantType.Name);
					}
				}
				if (list == null)
				{
					list = new List<Guid>();
				}
				list.Add(guid);
			}
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<LocalizedString>((long)this.GetHashCode(), "There are no drivers available for the assistant controller '{0}' at this time. No task available for execution.", this.Controller.TimeBasedAssistantType.Name);
			return null;
			Block_4:
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<LocalizedString>((long)this.GetHashCode(), "Could not find any tasks to execute for the assistant controller '{0}'. No task available for execution.", this.Controller.TimeBasedAssistantType.Name);
			return null;
			IL_1E1:
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<Guid, LocalizedString>((long)this.GetHashCode(), "A task is available for execution on database {0} for assistant {1}. Submitting the task to RUBS for execution", guid, this.Controller.TimeBasedAssistantType.Name);
			this.lastProcessedDriverGuid = guid;
			return new TimeBasedAssistantTask(this, nextDriver, reservation2);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00014E58 File Offset: 0x00013058
		protected override void YieldTask(SystemTaskBase task)
		{
			lock (this.instanceLock)
			{
				this.tasksWaitingExecution.AddLast(task);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014EA0 File Offset: 0x000130A0
		private int GetTaskCount()
		{
			return this.Controller.GetTaskCount() + this.tasksWaitingExecution.Count;
		}

		// Token: 0x0400023F RID: 575
		private Guid lastProcessedDriverGuid = Guid.Empty;

		// Token: 0x04000240 RID: 576
		private TimeBasedAssistantController controller;

		// Token: 0x04000241 RID: 577
		private LinkedList<SystemTaskBase> tasksWaitingExecution = new LinkedList<SystemTaskBase>();

		// Token: 0x04000242 RID: 578
		private object instanceLock = new object();
	}
}
