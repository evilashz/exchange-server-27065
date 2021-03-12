using System;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000071 RID: 113
	internal sealed class OnlineDatabase : Base, IDisposable
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000105F9 File Offset: 0x0000E7F9
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00010601 File Offset: 0x0000E801
		public bool RestartRequired
		{
			get
			{
				return this.eventController != null && this.eventController.RestartRequired;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00010618 File Offset: 0x0000E818
		public EventController EventController
		{
			get
			{
				return this.eventController;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010620 File Offset: 0x0000E820
		public OnlineDatabase(DatabaseInfo databaseInfo, DatabaseManager databaseManager)
		{
			this.databaseInfo = databaseInfo;
			this.databaseManager = databaseManager;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010636 File Offset: 0x0000E836
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "OnlineDatabase for database '" + this.databaseInfo.DisplayName + "'";
			}
			return this.toString;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010666 File Offset: 0x0000E866
		public void Dispose()
		{
			if (this.eventController != null)
			{
				this.eventController.Dispose();
				this.eventController = null;
			}
			this.DisposePerformanceCounters();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010688 File Offset: 0x0000E888
		public void Start()
		{
			ExTraceGlobals.OnlineDatabaseTracer.TraceDebug<OnlineDatabase>((long)this.GetHashCode(), "{0}: Starting", this);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			EventBasedAssistantCollection eventBasedAssistantCollection = null;
			try
			{
				PoisonEventControl poisonControl = new PoisonEventControl(this.databaseManager.PoisonControlMaster, this.DatabaseInfo);
				PoisonMailboxControl poisonControl2 = new PoisonMailboxControl(this.databaseManager.PoisonControlMaster, this.DatabaseInfo);
				string text = this.databaseManager.ServiceName + "-" + this.DatabaseInfo.DatabaseName;
				ExTraceGlobals.OnlineDatabaseTracer.TraceDebug<OnlineDatabase, string>((long)this.GetHashCode(), "{0}: Creating performance counters instance {1}", this, text);
				this.performanceCounters = new PerformanceCountersPerDatabaseInstance(text, this.databaseManager.PerformanceCountersTotal);
				this.performanceCounters.Reset();
				if (this.databaseManager.AssistantTypes != null)
				{
					eventBasedAssistantCollection = EventBasedAssistantCollection.Create(this.databaseInfo, this.databaseManager.AssistantTypes);
					if (eventBasedAssistantCollection != null)
					{
						if (this.databaseInfo.IsPublic)
						{
							this.eventController = new EventControllerPublic(this.databaseInfo, eventBasedAssistantCollection, poisonControl, this.performanceCounters, this.databaseManager.EventGovernor);
						}
						else
						{
							this.eventController = new EventControllerPrivate(this.databaseInfo, eventBasedAssistantCollection, poisonControl, this.performanceCounters, this.databaseManager.EventGovernor);
						}
						eventBasedAssistantCollection = null;
						this.eventController.Start();
						flag2 = true;
					}
				}
				if (!this.databaseInfo.IsPublic && this.databaseManager.TimeBasedDriverManager != null)
				{
					this.databaseManager.TimeBasedDriverManager.StartDatabase(this.databaseInfo, poisonControl2, this.performanceCounters);
					flag3 = true;
				}
				flag = true;
			}
			finally
			{
				if (eventBasedAssistantCollection != null)
				{
					eventBasedAssistantCollection.Dispose();
				}
				if (!flag)
				{
					ExTraceGlobals.OnlineDatabaseTracer.TraceError<OnlineDatabase>((long)this.GetHashCode(), "{0}: unable to start", this);
					if (this.eventController != null)
					{
						if (flag2)
						{
							this.eventController.Stop();
						}
						this.eventController.Dispose();
						this.eventController = null;
					}
				}
				if (!flag3 && !flag2)
				{
					this.DisposePerformanceCounters();
				}
			}
			base.TracePfd("PFS AIS {0} {1}: Started", new object[]
			{
				20567,
				this
			});
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public void RequestStop(HangDetector hangDetector)
		{
			ExTraceGlobals.OnlineDatabaseTracer.TraceDebug<OnlineDatabase>((long)this.GetHashCode(), "{0}: Requesting stop", this);
			if (this.eventController != null)
			{
				this.eventController.RequestStop(hangDetector);
			}
			if (this.databaseManager.TimeBasedDriverManager != null)
			{
				this.databaseManager.TimeBasedDriverManager.RequestStopDatabase(this.databaseInfo.Guid, hangDetector);
			}
			base.TracePfd("PFD AIS {0} {1}: Stop requested", new object[]
			{
				28759,
				this
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010938 File Offset: 0x0000EB38
		public void WaitUntilStopped()
		{
			ExTraceGlobals.OnlineDatabaseTracer.TraceDebug<OnlineDatabase>((long)this.GetHashCode(), "{0}: Waiting until stopped", this);
			if (this.eventController != null)
			{
				this.eventController.WaitUntilStopped();
			}
			if (this.databaseManager.TimeBasedDriverManager != null)
			{
				this.databaseManager.TimeBasedDriverManager.WaitUntilStoppedDatabase(this.databaseInfo.Guid);
			}
			this.DisposePerformanceCounters();
			base.TracePfd("PFD AIS {0} {1}: Stopped", new object[]
			{
				20055,
				this
			});
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000109C1 File Offset: 0x0000EBC1
		public void Stop(HangDetector hangDetector)
		{
			this.RequestStop(hangDetector);
			this.WaitUntilStopped();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000109D0 File Offset: 0x0000EBD0
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableOnlineDatabase queryableOnlineDatabase = queryableObject as QueryableOnlineDatabase;
			if (queryableOnlineDatabase != null)
			{
				queryableOnlineDatabase.DatabaseName = this.databaseInfo.DatabaseName;
				queryableOnlineDatabase.DatabaseGuid = this.databaseInfo.Guid;
				queryableOnlineDatabase.RestartRequired = this.RestartRequired;
				QueryableEventController queryableObject2 = new QueryableEventController();
				this.eventController.ExportToQueryableObject(queryableObject2);
				queryableOnlineDatabase.EventController = queryableObject2;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010A38 File Offset: 0x0000EC38
		private void DisposePerformanceCounters()
		{
			if (this.performanceCounters != null)
			{
				ExTraceGlobals.OnlineDatabaseTracer.TraceDebug<OnlineDatabase, string>((long)this.GetHashCode(), "{0}: Removing performance counters instance {1}", this, this.performanceCounters.Name);
				this.performanceCounters.Reset();
				this.performanceCounters.Close();
				this.performanceCounters.Remove();
				this.performanceCounters = null;
			}
		}

		// Token: 0x040001E5 RID: 485
		private EventController eventController;

		// Token: 0x040001E6 RID: 486
		private string toString;

		// Token: 0x040001E7 RID: 487
		private DatabaseInfo databaseInfo;

		// Token: 0x040001E8 RID: 488
		private DatabaseManager databaseManager;

		// Token: 0x040001E9 RID: 489
		private PerformanceCountersPerDatabaseInstance performanceCounters;
	}
}
