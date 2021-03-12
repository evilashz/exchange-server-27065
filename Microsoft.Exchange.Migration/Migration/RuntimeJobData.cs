using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000089 RID: 137
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RuntimeJobData : MigrationPersistableDictionary
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x00022CF6 File Offset: 0x00020EF6
		public RuntimeJobData()
		{
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00022CFE File Offset: 0x00020EFE
		public RuntimeJobData(Guid jobId) : this()
		{
			this.JobId = jobId;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600079A RID: 1946 RVA: 0x00022D10 File Offset: 0x00020F10
		// (remove) Token: 0x0600079B RID: 1947 RVA: 0x00022D48 File Offset: 0x00020F48
		internal event Action<IMigrationDataProvider> OnSaveRequested;

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00022D80 File Offset: 0x00020F80
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00022DAD File Offset: 0x00020FAD
		public Guid JobId
		{
			get
			{
				string text = base.Get<string>("JobId");
				if (string.IsNullOrEmpty(text))
				{
					return Guid.Empty;
				}
				return Guid.Parse(text);
			}
			internal set
			{
				base.Set<string>("JobId", value.ToString());
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00022DC7 File Offset: 0x00020FC7
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00022DD4 File Offset: 0x00020FD4
		public ExDateTime? RunningJobStartTime
		{
			get
			{
				return base.GetNullable<ExDateTime>("RunningJobStartTime");
			}
			set
			{
				base.SetNullable<ExDateTime>("RunningJobStartTime", value);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00022DE2 File Offset: 0x00020FE2
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00022DEF File Offset: 0x00020FEF
		public ExDateTime? RunningJobInitialStartTime
		{
			get
			{
				return base.GetNullable<ExDateTime>("RunningJobInitialStartTime");
			}
			set
			{
				base.SetNullable<ExDateTime>("RunningJobInitialStartTime", value);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00022E00 File Offset: 0x00021000
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00022E2F File Offset: 0x0002102F
		public TimeSpan RunningJobElapsedTime
		{
			get
			{
				TimeSpan? nullable = base.GetNullable<TimeSpan>("RunningJobElapsedTime");
				if (nullable == null)
				{
					return TimeSpan.Zero;
				}
				return nullable.GetValueOrDefault();
			}
			set
			{
				base.Set<TimeSpan>("RunningJobElapsedTime", value);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00022E3D File Offset: 0x0002103D
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00022E4A File Offset: 0x0002104A
		public int? RunningJobNumItemsProcessed
		{
			get
			{
				return base.GetNullable<int>("RunningJobNumItemsProcessed");
			}
			set
			{
				base.SetNullable<int>("RunningJobNumItemsProcessed", value);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00022E58 File Offset: 0x00021058
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00022E65 File Offset: 0x00021065
		public int? RunningJobNumItemsOutstanding
		{
			get
			{
				return base.GetNullable<int>("RunningJobNumItemsOutstanding");
			}
			set
			{
				base.SetNullable<int>("RunningJobNumItemsOutstanding", value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00022E73 File Offset: 0x00021073
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00022E80 File Offset: 0x00021080
		public string RunningJobDebugInfo
		{
			get
			{
				return base.Get<string>("RunningJobDebugInfo");
			}
			set
			{
				base.Set<string>("RunningJobDebugInfo", value);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00022E90 File Offset: 0x00021090
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00022EBB File Offset: 0x000210BB
		public int RunningJobNumRuns
		{
			get
			{
				int? nullable = base.GetNullable<int>("RunningJobNumRuns");
				if (nullable == null)
				{
					return 0;
				}
				return nullable.GetValueOrDefault();
			}
			set
			{
				base.Set<int>("RunningJobNumRuns", value);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00022EC9 File Offset: 0x000210C9
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00022EDF File Offset: 0x000210DF
		public string JobHistory
		{
			get
			{
				return base.Get<string>("JobHistory") ?? string.Empty;
			}
			set
			{
				base.Set<string>("JobHistory", value);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00022EF0 File Offset: 0x000210F0
		public bool IsRunning
		{
			get
			{
				return this.RunningJobInitialStartTime != null;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00022F0C File Offset: 0x0002110C
		public static RuntimeJobData Deserialize(Guid jobId, string serializedData)
		{
			RuntimeJobData runtimeJobData = new RuntimeJobData(jobId);
			if (!string.IsNullOrEmpty(serializedData))
			{
				runtimeJobData.DeserializeData(serializedData);
			}
			return runtimeJobData;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00022F30 File Offset: 0x00021130
		public void InitializeRunningJobSettings()
		{
			this.RunningJobStartTime = null;
			this.RunningJobInitialStartTime = null;
			this.RunningJobElapsedTime = TimeSpan.Zero;
			this.RunningJobNumRuns = 0;
			this.RunningJobNumItemsProcessed = null;
			this.RunningJobNumItemsOutstanding = null;
			this.RunningJobDebugInfo = null;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00022F94 File Offset: 0x00021194
		public void ResetRunspaceData()
		{
			if (this.RunningJobInitialStartTime == null)
			{
				throw new MigrationDataCorruptionException("always expect to have a running job set");
			}
			TimeSpan timeSpan = ExDateTime.UtcNow - this.RunningJobInitialStartTime.Value;
			if (this.RunningJobNumItemsProcessed != null && this.RunningJobNumItemsProcessed.Value > 0)
			{
				MigrationLogger.Log(MigrationEventType.Information, "resetting job {0} overall time {1} actual time {2} num of runs {3} num items processed {4} num items outstanding {5} debug {6}", new object[]
				{
					this.JobId,
					timeSpan,
					this.RunningJobElapsedTime,
					this.RunningJobNumRuns,
					this.RunningJobNumItemsProcessed,
					this.RunningJobNumItemsOutstanding,
					this.RunningJobDebugInfo
				});
				this.JobHistory = MigrationHelper.AppendDiagnosticHistory(this.JobHistory, new string[]
				{
					this.JobId.ToString(),
					timeSpan.ToString(),
					this.RunningJobElapsedTime.TotalSeconds.ToString(),
					this.RunningJobNumRuns.ToString(),
					this.RunningJobNumItemsProcessed.ToString(),
					this.RunningJobNumItemsOutstanding.ToString(),
					this.RunningJobDebugInfo
				});
			}
			this.InitializeRunningJobSettings();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00023128 File Offset: 0x00021328
		public MigrationProcessorResult CloseRunspace(IMigrationDataProvider dataProvider, LegacyMigrationJobProcessorResponse response, bool supportsInterrupting)
		{
			bool flag = response.Result == MigrationProcessorResult.Completed;
			if (!flag && supportsInterrupting)
			{
				if (response.Result == MigrationProcessorResult.Waiting)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "interrupting job because it's waiting", new object[0]);
					flag = true;
				}
				else if (response.Result == MigrationProcessorResult.Working && (response.NumItemsOutstanding == null || response.NumItemsOutstanding.Value <= 0))
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "interrupting job because no outstanding items {0}", new object[]
					{
						response.NumItemsOutstanding
					});
					flag = true;
				}
			}
			if (flag)
			{
				this.ResetRunspaceData();
			}
			else
			{
				if (this.RunningJobStartTime == null)
				{
					throw new MigrationDataCorruptionException("always expect to have a start time if job is set");
				}
				this.RunningJobElapsedTime = ExDateTime.UtcNow - this.RunningJobStartTime.Value;
				this.RunningJobNumRuns++;
				this.RunningJobNumItemsProcessed = response.NumItemsProcessed;
				this.RunningJobNumItemsOutstanding = response.NumItemsOutstanding;
				this.RunningJobDebugInfo = response.DebugInfo;
				if (this.RunningJobNumItemsProcessed != null && this.RunningJobNumItemsProcessed.Value > 0)
				{
					MigrationLogger.Log(MigrationEventType.Information, "job {0} finished a run of {1} with time {2} with debug {3}, items processed {4} items outstanding {5} but not removing", new object[]
					{
						this.JobId,
						this.RunningJobNumRuns,
						this.RunningJobElapsedTime,
						this.RunningJobDebugInfo,
						this.RunningJobNumItemsProcessed,
						this.RunningJobNumItemsOutstanding
					});
				}
			}
			this.RunningJobStartTime = null;
			this.SaveProperties(dataProvider);
			return response.Result;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000232D4 File Offset: 0x000214D4
		internal void SaveProperties(IMigrationDataProvider dataProvider)
		{
			if (this.OnSaveRequested != null)
			{
				this.OnSaveRequested(dataProvider);
			}
		}
	}
}
