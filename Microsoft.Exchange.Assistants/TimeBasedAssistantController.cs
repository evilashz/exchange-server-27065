using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000088 RID: 136
	internal sealed class TimeBasedAssistantController : Base, IDisposable
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00013918 File Offset: 0x00011B18
		public TimeBasedAssistantController(ThrottleGovernor governor, ITimeBasedAssistantType timeBasedAssistantType)
		{
			this.timeBasedAssistantType = timeBasedAssistantType;
			this.workCyclePeriod = this.timeBasedAssistantType.WorkCycle;
			this.workCycleCheckpoint = this.timeBasedAssistantType.WorkCycleCheckpoint;
			this.governor = governor;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00013971 File Offset: 0x00011B71
		public ThrottleGovernor Governor
		{
			get
			{
				return this.governor;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00013979 File Offset: 0x00011B79
		public ITimeBasedAssistantType TimeBasedAssistantType
		{
			get
			{
				return this.timeBasedAssistantType;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00013981 File Offset: 0x00011B81
		internal TimeSpan WorkCycle
		{
			get
			{
				return this.workCyclePeriod;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00013989 File Offset: 0x00011B89
		internal TimeSpan WorkCycleCheckPoint
		{
			get
			{
				return this.workCycleCheckpoint;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013991 File Offset: 0x00011B91
		public void Dispose()
		{
			this.DeinitializeTimer();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001399C File Offset: 0x00011B9C
		public TimeBasedDatabaseDriver[] GetCurrentAssistantDrivers()
		{
			TimeBasedDatabaseDriver[] result;
			lock (this.assistantDrivers)
			{
				result = this.assistantDrivers.Values.ToArray<TimeBasedDatabaseDriver>();
			}
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000139E8 File Offset: 0x00011BE8
		public TimeBasedDatabaseDriver GetNextDriver(Guid guidToCheck)
		{
			lock (this.assistantDrivers)
			{
				if (this.assistantDrivers == null || this.assistantDrivers.Count == 0)
				{
					return null;
				}
				bool flag2 = false;
				if (guidToCheck == Guid.Empty)
				{
					flag2 = true;
				}
				foreach (Guid guid in this.assistantDrivers.Keys)
				{
					if (flag2)
					{
						return this.assistantDrivers[guid];
					}
					if (guid == guidToCheck)
					{
						flag2 = true;
					}
				}
				using (Dictionary<Guid, TimeBasedDatabaseDriver>.KeyCollection.Enumerator enumerator2 = this.assistantDrivers.Keys.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						Guid key = enumerator2.Current;
						return this.assistantDrivers[key];
					}
				}
			}
			return null;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013B08 File Offset: 0x00011D08
		public int GetTaskCount()
		{
			int num = 0;
			lock (this.assistantDrivers)
			{
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					num += (int)timeBasedDatabaseDriver.TotalMailboxesQueued;
				}
			}
			return num;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013B90 File Offset: 0x00011D90
		public void Halt()
		{
			lock (this.assistantDrivers)
			{
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					timeBasedDatabaseDriver.Halt();
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013C10 File Offset: 0x00011E10
		public void RequestStop(HangDetector hangDetector)
		{
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: Stopping", this);
			AssistantsRpcServer.DeregisterAssistant(this.timeBasedAssistantType.NonLocalizedName);
			if (this.workCycleConfigurationTimer != null)
			{
				this.workCycleConfigurationTimer.Dispose();
				this.workCycleConfigurationTimer = null;
			}
			this.DeinitializeTimer();
			lock (this.assistantDrivers)
			{
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					if (timeBasedDatabaseDriver != null)
					{
						if (hangDetector != null)
						{
							hangDetector.AssistantName = timeBasedDatabaseDriver.Assistant.NonLocalizedName;
						}
						try
						{
							timeBasedDatabaseDriver.RequestStop();
						}
						finally
						{
							if (hangDetector != null)
							{
								hangDetector.AssistantName = "Common Code";
							}
						}
					}
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013D0C File Offset: 0x00011F0C
		public void RequestStopDatabase(Guid databaseGuid, HangDetector hangDetector)
		{
			lock (this.assistantDrivers)
			{
				if (this.assistantDrivers.ContainsKey(databaseGuid))
				{
					TimeBasedDatabaseDriver timeBasedDatabaseDriver = this.assistantDrivers[databaseGuid];
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Requesting stop of {1}", this, timeBasedDatabaseDriver);
					try
					{
						if (timeBasedDatabaseDriver != null)
						{
							if (hangDetector != null)
							{
								hangDetector.AssistantName = timeBasedDatabaseDriver.Assistant.NonLocalizedName;
							}
							timeBasedDatabaseDriver.RequestStop();
						}
						goto IL_8A;
					}
					finally
					{
						if (hangDetector != null)
						{
							hangDetector.AssistantName = "Common Code";
						}
					}
				}
				ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, Guid>((long)this.GetHashCode(), "{0}: No driver found for database {1}, no need to request stop", this, databaseGuid);
				IL_8A:;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013ED0 File Offset: 0x000120D0
		public void RunNow(Guid mailboxGuid, Guid databaseGuid, string parameters)
		{
			TimeBasedAssistantController.<>c__DisplayClass9 CS$<>8__locals1 = new TimeBasedAssistantController.<>c__DisplayClass9();
			CS$<>8__locals1.mailboxGuid = mailboxGuid;
			CS$<>8__locals1.databaseGuid = databaseGuid;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedDatabaseDriver = null;
			lock (this.assistantDrivers)
			{
				ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug((long)this.GetHashCode(), "{0}: RunNow requested for database {1} and mailbox {2} with parameters '{3}'. Number of assistant drivers: {4}", new object[]
				{
					this,
					CS$<>8__locals1.databaseGuid,
					CS$<>8__locals1.mailboxGuid,
					string.IsNullOrEmpty(parameters) ? "<null>" : parameters,
					this.assistantDrivers.Count
				});
				if (this.assistantDrivers.Count == 0)
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: There is no database driver so no work to do at this time.", this);
					throw new UnknownDatabaseException(CS$<>8__locals1.databaseGuid.ToString());
				}
				if (CS$<>8__locals1.mailboxGuid.Equals(Guid.Empty) || CS$<>8__locals1.databaseGuid.Equals(Guid.Empty))
				{
					throw new MailboxOrDatabaseNotSpecifiedException();
				}
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					if (timeBasedDatabaseDriver.DatabaseInfo.Guid == CS$<>8__locals1.databaseGuid)
					{
						ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, TimeBasedDatabaseDriver>((long)this.GetHashCode(), "{0}: Found database driver for the requested database: {1}", this, timeBasedDatabaseDriver);
						CS$<>8__locals1.selectedDatabaseDriver = timeBasedDatabaseDriver;
						break;
					}
				}
			}
			if (CS$<>8__locals1.selectedDatabaseDriver == null)
			{
				throw new UnknownDatabaseException(CS$<>8__locals1.databaseGuid.ToString());
			}
			CS$<>8__locals1.demandJobNotification = (CS$<>8__locals1.selectedDatabaseDriver.Assistant as IDemandJobNotification);
			if (CS$<>8__locals1.demandJobNotification != null)
			{
				TimeBasedAssistantController.<>c__DisplayClassb CS$<>8__locals2 = new TimeBasedAssistantController.<>c__DisplayClassb();
				CS$<>8__locals2.CS$<>8__localsa = CS$<>8__locals1;
				ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, string>((long)this.GetHashCode(), "{0}: Notifying assistant {1} that a demand job has been requested.", this, CS$<>8__locals1.selectedDatabaseDriver.Assistant.NonLocalizedName);
				CS$<>8__locals2.kit = new EmergencyKit(CS$<>8__locals1.mailboxGuid);
				CS$<>8__locals1.selectedDatabaseDriver.PoisonControl.PoisonCall(CS$<>8__locals2.kit, new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<RunNow>b__7)));
			}
			CS$<>8__locals1.selectedDatabaseDriver.RunNow(CS$<>8__locals1.mailboxGuid, parameters);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014148 File Offset: 0x00012348
		public void Start()
		{
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: Starting", this);
			AssistantsRpcServer.RegisterAssistant(this.timeBasedAssistantType.NonLocalizedName, this);
			this.workCyclePeriod = this.timeBasedAssistantType.WorkCycle;
			this.workCycleCheckpoint = this.timeBasedAssistantType.WorkCycleCheckpoint;
			this.workCycleConfigurationTimer = new Timer(new TimerCallback(this.ReadAndUpdateWorkCycleConfiguration), null, Configuration.WorkCycleUpdatePeriod, Configuration.WorkCycleUpdatePeriod);
			base.TracePfd("PFD AIS {0} {1}: Started", new object[]
			{
				32343,
				this
			});
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000141E8 File Offset: 0x000123E8
		public void StartDatabase(DatabaseInfo databaseInfo, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, DatabaseInfo>((long)this.GetHashCode(), "{0}: Starting assistant instance for database {1}", this, databaseInfo);
			lock (this.workCycleUpdateLocker)
			{
				lock (this.assistantDrivers)
				{
					if (!this.assistantDrivers.ContainsKey(databaseInfo.Guid))
					{
						TimeBasedDatabaseDriver timeBasedDatabaseDriver = this.timeBasedAssistantType.CreateDriver(this.Governor, databaseInfo, this.timeBasedAssistantType, poisonControl, databaseCounters);
						timeBasedDatabaseDriver.Start();
						this.assistantDrivers.Add(databaseInfo.Guid, timeBasedDatabaseDriver);
						TimeSpan timeSpan = this.GetDueTime();
						if (this.IsAssistantEnabled())
						{
							if (timeSpan == TimeSpan.Zero)
							{
								this.TryInitializeWorkCycleCheckpointTimer(this.workCycleCheckpoint);
							}
							else
							{
								timeSpan = ((timeSpan < TimeSpan.FromMinutes(1.0)) ? TimeSpan.FromMinutes(1.0) : timeSpan);
								this.TryInitializeWorkCycleCheckpointTimer(timeSpan);
							}
						}
						if (timeSpan == TimeSpan.Zero)
						{
							timeBasedDatabaseDriver.UpdateWorkCycle(this.workCyclePeriod);
						}
					}
				}
			}
			base.TracePfd("PFD AIS {0} {1}: Started assistant instance for database {2}", new object[]
			{
				26199,
				this,
				databaseInfo
			});
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014350 File Offset: 0x00012550
		public override string ToString()
		{
			string result;
			if ((result = this.toString) == null)
			{
				result = (this.toString = "Time-based assistant controller for " + this.timeBasedAssistantType);
			}
			return result;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014380 File Offset: 0x00012580
		public void WaitUntilStopped()
		{
			lock (this.assistantDrivers)
			{
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					timeBasedDatabaseDriver.WaitUntilStopped(this);
					timeBasedDatabaseDriver.Dispose();
				}
				this.assistantDrivers.Clear();
			}
			base.TracePfd("PFD AIS {0} {1}: Stopped", new object[]
			{
				18007,
				this
			});
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001443C File Offset: 0x0001263C
		public void WaitUntilStoppedDatabase(Guid databaseGuid)
		{
			lock (this.assistantDrivers)
			{
				if (this.assistantDrivers.ContainsKey(databaseGuid))
				{
					TimeBasedDatabaseDriver timeBasedDatabaseDriver = this.assistantDrivers[databaseGuid];
					timeBasedDatabaseDriver.WaitUntilStopped(this);
					timeBasedDatabaseDriver.Dispose();
					this.assistantDrivers.Remove(databaseGuid);
					if (this.assistantDrivers.Count == 0)
					{
						this.DeinitializeTimer();
					}
					base.TracePfd("PFD AIS {0} {1}: Stopped assistant.", new object[]
					{
						22103,
						this
					});
				}
				else
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, Guid>((long)this.GetHashCode(), "{0}: No driver found for database {1}, no need to wait stop", this, databaseGuid);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000144FC File Offset: 0x000126FC
		internal bool IsAssistantEnabled()
		{
			bool flag = false;
			lock (this.assistantDrivers)
			{
				foreach (KeyValuePair<Guid, TimeBasedDatabaseDriver> keyValuePair in this.assistantDrivers)
				{
					if (keyValuePair.Value.IsAssistantEnabled())
					{
						flag = true;
					}
				}
			}
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedAssistantController, TimeBasedAssistantIdentifier, bool>((long)this.GetHashCode(), "{0}: Assistant {1} is enabled: {2}.", this, this.TimeBasedAssistantType.Identifier, flag);
			return flag;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000145B0 File Offset: 0x000127B0
		internal bool IsVariantConfigurationChanged()
		{
			bool flag = false;
			lock (this.assistantDrivers)
			{
				flag = this.assistantDrivers.Values.Any((TimeBasedDatabaseDriver driver) => driver.IsVariantConfigurationChanged());
			}
			ExTraceGlobals.TimeBasedDriverManagerTracer.TraceDebug<TimeBasedAssistantController, TimeBasedAssistantIdentifier, bool>((long)this.GetHashCode(), "{0}: Assistant {1} is needs Variant Configuration update: {2}.", this, this.TimeBasedAssistantType.Identifier, flag);
			return flag;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00014640 File Offset: 0x00012840
		internal void ReadAndUpdateWorkCycleConfigurationForTest()
		{
			this.ReadAndUpdateWorkCycleConfiguration(null);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00014649 File Offset: 0x00012849
		internal void UpdateWorkCycleForTest()
		{
			this.UpdateWorkCycle("Test");
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014656 File Offset: 0x00012856
		private void DeinitializeTimer()
		{
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: Disposing the Work Cycle Update timer.", this);
			if (this.workCycleCheckpointTimer != null)
			{
				this.workCycleCheckpointTimer.Dispose();
				this.workCycleCheckpointTimer = null;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001468C File Offset: 0x0001288C
		private TimeSpan GetDueTime()
		{
			TimeSpan zero = TimeSpan.Zero;
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			bool flag = snapshot.WorkloadManagement.Blackout.StartTime != snapshot.WorkloadManagement.Blackout.EndTime;
			if (!flag || snapshot.WorkloadManagement.GetObject<IWorkloadSettings>(this.TimeBasedAssistantType.WorkloadType, new object[0]).EnabledDuringBlackout)
			{
				return zero;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime d = utcNow;
			DateTime dateTime = utcNow.Date + snapshot.WorkloadManagement.Blackout.StartTime;
			DateTime dateTime2 = utcNow.Date + snapshot.WorkloadManagement.Blackout.EndTime;
			dateTime = dateTime.Subtract(TimeSpan.FromHours(1.0));
			if (dateTime >= dateTime2)
			{
				dateTime2 = dateTime2.AddDays(1.0);
			}
			if (utcNow > dateTime && utcNow < dateTime2)
			{
				d = dateTime2;
			}
			return d - utcNow;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000147B0 File Offset: 0x000129B0
		private void ReadAndUpdateWorkCycleConfiguration(object state)
		{
			if (this.GetDueTime() != TimeSpan.Zero)
			{
				return;
			}
			bool flag = this.IsVariantConfigurationChanged();
			lock (this.workCycleUpdateLocker)
			{
				TimeSpan workCycle = this.timeBasedAssistantType.WorkCycle;
				TimeSpan obj2 = this.timeBasedAssistantType.WorkCycleCheckpoint;
				bool flag3 = !this.workCyclePeriod.Equals(workCycle) || !this.workCycleCheckpoint.Equals(obj2) || flag;
				this.workCyclePeriod = workCycle;
				this.workCycleCheckpoint = obj2;
				if (flag3)
				{
					this.UpdateWorkCycle("ConfigurationUpdate");
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014860 File Offset: 0x00012A60
		private void TryInitializeWorkCycleCheckpointTimer()
		{
			this.TryInitializeWorkCycleCheckpointTimer(this.GetDueTime());
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014870 File Offset: 0x00012A70
		private void TryInitializeWorkCycleCheckpointTimer(TimeSpan dueTime)
		{
			if (this.workCycleCheckpointTimer == null)
			{
				ExAssert.RetailAssert(dueTime >= TimeSpan.Zero, "DueTime should never be negative.");
				ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, TimeSpan>((long)this.GetHashCode(), "{0}: Creating the work cycle checkpoint timer: {1}", this, this.workCycleCheckpoint);
				this.workCycleCheckpointTimer = new Timer(new TimerCallback(this.UpdateWorkCycle), "Checkpoint", dueTime.Duration(), this.workCycleCheckpoint);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001491C File Offset: 0x00012B1C
		private void UpdateWorkCycle(object callerName)
		{
			bool flag = this.IsAssistantEnabled();
			ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController, bool, string>((long)this.GetHashCode(), "{0}: Updating Work Cycle. Assistant enabled: {1}. Called from: {2}", this, flag, (string)callerName);
			lock (this.workCycleUpdateLocker)
			{
				try
				{
					if (flag)
					{
						this.TryInitializeWorkCycleCheckpointTimer();
						this.workCycleCheckpointTimer.Change(this.workCycleCheckpoint, this.workCycleCheckpoint);
					}
					else
					{
						this.DeinitializeTimer();
					}
					base.CatchMeIfYouCan(delegate
					{
						lock (this)
						{
							this.UpdateWorkCycleForAllDrivers();
						}
					}, (this.TimeBasedAssistantType != null) ? this.TimeBasedAssistantType.NonLocalizedName : "<null>");
				}
				catch (AIException ex)
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceError<TimeBasedAssistantController, AIException>((long)this.GetHashCode(), "{0}: Could not update the Work Cycle: {1}", this, ex);
					base.LogEvent(AssistantsEventLogConstants.Tuple_WorkCycleCheckpointError, null, new object[]
					{
						this.timeBasedAssistantType.Name,
						ex
					});
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014A34 File Offset: 0x00012C34
		private void UpdateWorkCycleForAllDrivers()
		{
			lock (this.assistantDrivers)
			{
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver in this.assistantDrivers.Values)
				{
					timeBasedDatabaseDriver.StopWorkCycle();
				}
				if (this.IsAssistantEnabled())
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: Notifying assistant type of checkpoint.", this);
					this.timeBasedAssistantType.OnWorkCycleCheckpoint();
				}
				else
				{
					ExTraceGlobals.TimeBasedAssistantControllerTracer.TraceDebug<TimeBasedAssistantController>((long)this.GetHashCode(), "{0}: Assistant is not enabled. Will not start a new Work Cycle.", this);
				}
				foreach (TimeBasedDatabaseDriver timeBasedDatabaseDriver2 in this.assistantDrivers.Values)
				{
					timeBasedDatabaseDriver2.UpdateWorkCycle(this.workCyclePeriod);
				}
			}
		}

		// Token: 0x04000235 RID: 565
		private Dictionary<Guid, TimeBasedDatabaseDriver> assistantDrivers = new Dictionary<Guid, TimeBasedDatabaseDriver>();

		// Token: 0x04000236 RID: 566
		private ThrottleGovernor governor;

		// Token: 0x04000237 RID: 567
		private ITimeBasedAssistantType timeBasedAssistantType;

		// Token: 0x04000238 RID: 568
		private string toString;

		// Token: 0x04000239 RID: 569
		private TimeSpan workCycleCheckpoint;

		// Token: 0x0400023A RID: 570
		private Timer workCycleCheckpointTimer;

		// Token: 0x0400023B RID: 571
		private Timer workCycleConfigurationTimer;

		// Token: 0x0400023C RID: 572
		private TimeSpan workCyclePeriod;

		// Token: 0x0400023D RID: 573
		private object workCycleUpdateLocker = new object();
	}
}
