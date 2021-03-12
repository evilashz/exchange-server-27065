using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002D2 RID: 722
	internal sealed class FileState
	{
		// Token: 0x06001C5D RID: 7261 RVA: 0x0007C62A File Offset: 0x0007A82A
		public FileState()
		{
			this.Reset();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0007C638 File Offset: 0x0007A838
		private static void InternalCheck(bool condition, string formatString, params object[] messageArgs)
		{
			if (!condition)
			{
				string text = string.Format(formatString, messageArgs);
				string stackTrace = Environment.StackTrace;
				ExTraceGlobals.FileCheckerTracer.TraceError<string, string>(0L, "FileState internal check failed. Message is {0}, callstack is {1}", text, stackTrace);
				throw new FileStateInternalErrorException(text);
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0007C670 File Offset: 0x0007A870
		public bool RequiredLogfilesArePresent()
		{
			lock (this)
			{
				this.InternalCheck();
				if (0L != this.m_lowestGenerationRequired)
				{
					long highestGenerationPresentWithE = this.HighestGenerationPresentWithE00;
					if (highestGenerationPresentWithE < this.m_highestGenerationRequired)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007C6CC File Offset: 0x0007A8CC
		public override string ToString()
		{
			string result;
			lock (this)
			{
				result = string.Format(CultureInfo.CurrentCulture, "LowestGenerationPresent: {1}{0}HighestGenerationPresent: {2}{0}LowestGenerationRequired: {3}{0}HighestGenerationRequired: {4}{0}LastGenerationBackedUp: {5}{0}CheckpointGeneration: {6}{0}LogfileSignature: {7}{0}LatestFullBackupTime: {8}{0}LatestIncrementalBackupTime: {9}{0}LatestDifferentialBackupTime: {10}{0}LatestCopyBackupTime: {11}{0}SnapshotBackup: {12}{0}SnapshotLatestFullBackup: {13}{0}SnapshotLatestIncrementalBackup: {14}{0}SnapshotLatestDifferentialBackup: {15}{0}SnapshotLatestCopyBackup: {16}{0}ConsistentDatabase: {17}", new object[]
				{
					Environment.NewLine,
					this.m_lowestGenerationPresent,
					this.m_highestGenerationPresent,
					this.m_lowestGenerationRequired,
					this.m_highestGenerationRequired,
					this.m_lastGenerationBackedUp,
					this.m_checkpointGeneration,
					this.m_logfileSignature,
					this.m_latestFullBackupTime,
					this.m_latestIncrementalBackupTime,
					this.m_latestDifferentialBackupTime,
					this.m_latestCopyBackupTime,
					this.m_snapshotBackup,
					this.m_snapshotLatestFullBackup,
					this.m_snapshotLatestIncrementalBackup,
					this.m_snapshotLatestDifferentialBackup,
					this.m_snapshotLatestCopyBackup,
					this.m_consistentDatabase
				});
			}
			return result;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0007C828 File Offset: 0x0007AA28
		public void Reset()
		{
			this.m_e00Generation = null;
			this.m_lowestGenerationPresent = 0L;
			this.m_highestGenerationPresent = 0L;
			this.m_lowestGenerationRequired = 0L;
			this.m_highestGenerationRequired = 0L;
			this.m_lastGenerationBackedUp = 0L;
			this.m_checkpointGeneration = 0L;
			this.m_logfileSignature = null;
			this.m_latestFullBackupTime = null;
			this.m_latestIncrementalBackupTime = null;
			this.m_latestDifferentialBackupTime = null;
			this.m_latestCopyBackupTime = null;
			this.m_snapshotBackup = null;
			this.m_snapshotLatestFullBackup = null;
			this.m_snapshotLatestIncrementalBackup = null;
			this.m_snapshotLatestDifferentialBackup = null;
			this.m_snapshotLatestCopyBackup = null;
			this.m_consistentDatabase = false;
			this.InternalCheck();
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007C8F8 File Offset: 0x0007AAF8
		public void InternalCheck()
		{
			lock (this)
			{
				FileState.InternalCheck(this.m_lowestGenerationPresent <= this.m_highestGenerationPresent, "LowestGenerationPresent > HighestGenerationPresent", new object[0]);
				FileState.InternalCheck(this.m_lowestGenerationRequired <= this.m_highestGenerationRequired, "LowestGenerationRequired > HighestGenerationRequired", new object[0]);
				if (0L == this.m_lowestGenerationPresent)
				{
					FileState.InternalCheck(0L == this.m_highestGenerationPresent, "LowestGenerationPresent is 0, but HighestGenerationPresent is set", new object[0]);
				}
				else
				{
					FileState.InternalCheck(0L != this.m_highestGenerationPresent, "LowestGenerationPresent is set, but HighestGenerationPresent is 0", new object[0]);
				}
				if (0L == this.m_lowestGenerationRequired)
				{
					FileState.InternalCheck(0L == this.m_highestGenerationRequired, "LowestGenerationRequired is 0, but HighestGenerationRequired is set", new object[0]);
				}
				else
				{
					FileState.InternalCheck(0L != this.m_highestGenerationRequired, "LowestGenerationRequired is set, but HighestGenerationRequired is 0", new object[0]);
				}
				if (this.m_consistentDatabase)
				{
					FileState.InternalCheck(this.m_highestGenerationRequired == this.m_lowestGenerationRequired, "ConsistentDatabase is true, but HighestGenerationRequired != LowestGenerationRequired", new object[0]);
				}
				if (this.m_highestGenerationRequired != this.m_lowestGenerationRequired)
				{
					FileState.InternalCheck(!this.m_consistentDatabase, "HighestGenerationRequired != LowestGenerationRequired but ConsistentDatabase is true", new object[0]);
				}
				if (this.m_latestFullBackupTime != null)
				{
					FileState.InternalCheck(this.m_lastGenerationBackedUp > 0L, "m_latestFullBackupTime is set, but m_lastGenerationBackedUp is not set", new object[0]);
				}
				if (this.m_latestIncrementalBackupTime != null)
				{
					FileState.InternalCheck(this.m_lastGenerationBackedUp > 0L, "m_latestIncrementalBackupTime is set, but m_lastGenerationBackedUp is not set", new object[0]);
					FileState.InternalCheck(this.m_latestFullBackupTime != null, "m_latestIncrementalBackupTime is set, but m_latestFullBackupTime is not set", new object[0]);
				}
				if (this.m_latestDifferentialBackupTime != null)
				{
					FileState.InternalCheck(this.m_lastGenerationBackedUp > 0L, "m_latestDifferentialBackupTime is set, but m_lastGenerationBackedUp is not set", new object[0]);
					FileState.InternalCheck(this.m_latestFullBackupTime != null, "m_latestDifferentialBackupTime is set, but m_latestFullBackupTime is not set", new object[0]);
				}
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0007CAFC File Offset: 0x0007ACFC
		public void InternalCheckLogfileSignature()
		{
			lock (this)
			{
				if (0L != this.LowestGenerationPresent)
				{
					FileState.InternalCheck(this.LogfileSignature != null, "Logfiles are present, but LogfileSignature is not set", new object[0]);
				}
				if (0L != this.LowestGenerationRequired)
				{
					FileState.InternalCheck(this.LogfileSignature != null, "Logfiles are required, but LogfileSignature is not set", new object[0]);
				}
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0007CB84 File Offset: 0x0007AD84
		public void GetLowestAndHighestGenerationsRequired(out bool databaseIsConsistent, out long lowestGenerationRequired, out long highestGenerationRequired)
		{
			lock (this)
			{
				databaseIsConsistent = this.m_consistentDatabase;
				lowestGenerationRequired = this.m_lowestGenerationRequired;
				highestGenerationRequired = this.m_highestGenerationRequired;
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0007CBD4 File Offset: 0x0007ADD4
		public void SetLowestAndHighestGenerationsPresent(long lowestGenerationPresent, long highestGenerationPresent)
		{
			FileState.InternalCheck(lowestGenerationPresent <= highestGenerationPresent, "lowestGenerationPresent > highestGenerationPresent", new object[0]);
			if (lowestGenerationPresent == 0L)
			{
				FileState.InternalCheck(highestGenerationPresent == 0L, "Highest gen must be 0 when low gen is 0", new object[0]);
			}
			if (highestGenerationPresent == 0L)
			{
				FileState.InternalCheck(lowestGenerationPresent == 0L, "Lowest gen must be 0 when hi gen is 0", new object[0]);
			}
			lock (this)
			{
				this.InternalCheck();
				this.m_lowestGenerationPresent = lowestGenerationPresent;
				this.m_highestGenerationPresent = highestGenerationPresent;
				this.InternalCheck();
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0007CC70 File Offset: 0x0007AE70
		public void SetLowestGenerationPresent(long lowestGenerationPresent)
		{
			lock (this)
			{
				FileState.InternalCheck(lowestGenerationPresent <= this.m_highestGenerationPresent, "LowestGenerationPresent > HighestGenerationPresent", new object[0]);
				this.InternalCheck();
				this.m_lowestGenerationPresent = lowestGenerationPresent;
				this.InternalCheck();
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0007CCD4 File Offset: 0x0007AED4
		public void SetLowestAndHighestGenerationsRequired(long lowestGenerationRequired, long highestGenerationRequired, bool databaseIsConsistent)
		{
			FileState.InternalCheck(lowestGenerationRequired >= 0L, "lowestGenerationRequired should be greater than 0 but we got {0}", new object[]
			{
				lowestGenerationRequired
			});
			FileState.InternalCheck(highestGenerationRequired >= 0L, "highestGenerationRequired should be greater than 0 but we got {0}", new object[]
			{
				highestGenerationRequired
			});
			FileState.InternalCheck(lowestGenerationRequired <= highestGenerationRequired, "lowestGenerationRequired > highestGenerationRequired", new object[0]);
			FileState.InternalCheck(!databaseIsConsistent || lowestGenerationRequired == highestGenerationRequired, "lowestGenerationRequired != highestGenerationRequired, but the database is consistent", new object[0]);
			lock (this)
			{
				this.InternalCheck();
				this.m_lowestGenerationRequired = lowestGenerationRequired;
				this.m_highestGenerationRequired = highestGenerationRequired;
				this.m_consistentDatabase = databaseIsConsistent;
				this.InternalCheck();
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0007CDA4 File Offset: 0x0007AFA4
		public void SetE00LogGeneration(long e00generation)
		{
			FileState.InternalCheck(e00generation > 0L, "e00generation should be greater than 0!", new object[0]);
			lock (this)
			{
				this.InternalCheck();
				this.m_e00Generation = new long?(e00generation);
				this.InternalCheck();
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0007CE08 File Offset: 0x0007B008
		public void ClearE00LogGeneration()
		{
			lock (this)
			{
				this.InternalCheck();
				this.m_e00Generation = null;
				this.InternalCheck();
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0007CE58 File Offset: 0x0007B058
		public bool IsGenerationInRequiredRange(long generation)
		{
			bool result;
			lock (this)
			{
				if (this.m_lowestGenerationRequired != 0L)
				{
					result = (generation >= this.m_lowestGenerationRequired && generation <= this.m_highestGenerationRequired);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0007CEB8 File Offset: 0x0007B0B8
		public long LowestGenerationPresent
		{
			get
			{
				long lowestGenerationPresent;
				lock (this)
				{
					this.InternalCheck();
					lowestGenerationPresent = this.m_lowestGenerationPresent;
				}
				return lowestGenerationPresent;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0007CEFC File Offset: 0x0007B0FC
		public long HighestGenerationPresentWithE00
		{
			get
			{
				long result;
				lock (this)
				{
					this.InternalCheck();
					long num = this.m_highestGenerationPresent;
					if (this.m_e00Generation != null)
					{
						num = Math.Max(this.m_e00Generation.Value, num);
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0007CF60 File Offset: 0x0007B160
		public long HighestGenerationPresent
		{
			get
			{
				long highestGenerationPresent;
				lock (this)
				{
					this.InternalCheck();
					highestGenerationPresent = this.m_highestGenerationPresent;
				}
				return highestGenerationPresent;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0007CFA4 File Offset: 0x0007B1A4
		public long LowestGenerationRequired
		{
			get
			{
				long lowestGenerationRequired;
				lock (this)
				{
					this.InternalCheck();
					lowestGenerationRequired = this.m_lowestGenerationRequired;
				}
				return lowestGenerationRequired;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0007CFE8 File Offset: 0x0007B1E8
		public long HighestGenerationRequired
		{
			get
			{
				long highestGenerationRequired;
				lock (this)
				{
					this.InternalCheck();
					highestGenerationRequired = this.m_highestGenerationRequired;
				}
				return highestGenerationRequired;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0007D02C File Offset: 0x0007B22C
		public long? E00Generation
		{
			get
			{
				long? e00Generation;
				lock (this)
				{
					this.InternalCheck();
					e00Generation = this.m_e00Generation;
				}
				return e00Generation;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0007D070 File Offset: 0x0007B270
		// (set) Token: 0x06001C72 RID: 7282 RVA: 0x0007D0B4 File Offset: 0x0007B2B4
		public long LastGenerationBackedUp
		{
			get
			{
				long lastGenerationBackedUp;
				lock (this)
				{
					this.InternalCheck();
					lastGenerationBackedUp = this.m_lastGenerationBackedUp;
				}
				return lastGenerationBackedUp;
			}
			set
			{
				lock (this)
				{
					this.m_lastGenerationBackedUp = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0007D0F8 File Offset: 0x0007B2F8
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x0007D13C File Offset: 0x0007B33C
		public long CheckpointGeneration
		{
			get
			{
				long checkpointGeneration;
				lock (this)
				{
					this.InternalCheck();
					checkpointGeneration = this.m_checkpointGeneration;
				}
				return checkpointGeneration;
			}
			set
			{
				lock (this)
				{
					this.m_checkpointGeneration = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0007D180 File Offset: 0x0007B380
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x0007D1C4 File Offset: 0x0007B3C4
		internal JET_SIGNATURE? LogfileSignature
		{
			get
			{
				JET_SIGNATURE? logfileSignature;
				lock (this)
				{
					this.InternalCheck();
					logfileSignature = this.m_logfileSignature;
				}
				return logfileSignature;
			}
			set
			{
				lock (this)
				{
					if (this.m_logfileSignature != null && (this.m_logfileSignature.Value.ulRandom != value.Value.ulRandom || this.m_logfileSignature.Value.logtimeCreate.ToUint64() != value.Value.logtimeCreate.ToUint64()))
					{
						FileState.InternalCheck(false, "Logfile signature is already set. Multiple assignment must be idempotent.", new object[0]);
					}
					this.m_logfileSignature = value;
					this.InternalCheck();
					this.InternalCheckLogfileSignature();
				}
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0007D278 File Offset: 0x0007B478
		// (set) Token: 0x06001C78 RID: 7288 RVA: 0x0007D2BC File Offset: 0x0007B4BC
		public DateTime? LatestFullBackupTime
		{
			get
			{
				DateTime? latestFullBackupTime;
				lock (this)
				{
					this.InternalCheck();
					latestFullBackupTime = this.m_latestFullBackupTime;
				}
				return latestFullBackupTime;
			}
			set
			{
				lock (this)
				{
					this.m_latestFullBackupTime = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0007D300 File Offset: 0x0007B500
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0007D344 File Offset: 0x0007B544
		public DateTime? LatestIncrementalBackupTime
		{
			get
			{
				DateTime? latestIncrementalBackupTime;
				lock (this)
				{
					this.InternalCheck();
					latestIncrementalBackupTime = this.m_latestIncrementalBackupTime;
				}
				return latestIncrementalBackupTime;
			}
			set
			{
				lock (this)
				{
					this.m_latestIncrementalBackupTime = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0007D388 File Offset: 0x0007B588
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0007D3CC File Offset: 0x0007B5CC
		public DateTime? LatestDifferentialBackupTime
		{
			get
			{
				DateTime? latestDifferentialBackupTime;
				lock (this)
				{
					this.InternalCheck();
					latestDifferentialBackupTime = this.m_latestDifferentialBackupTime;
				}
				return latestDifferentialBackupTime;
			}
			set
			{
				lock (this)
				{
					this.m_latestDifferentialBackupTime = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0007D410 File Offset: 0x0007B610
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0007D454 File Offset: 0x0007B654
		public DateTime? LatestCopyBackupTime
		{
			get
			{
				DateTime? latestCopyBackupTime;
				lock (this)
				{
					this.InternalCheck();
					latestCopyBackupTime = this.m_latestCopyBackupTime;
				}
				return latestCopyBackupTime;
			}
			set
			{
				lock (this)
				{
					this.m_latestCopyBackupTime = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0007D498 File Offset: 0x0007B698
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x0007D4DC File Offset: 0x0007B6DC
		public bool? SnapshotBackup
		{
			get
			{
				bool? snapshotBackup;
				lock (this)
				{
					this.InternalCheck();
					snapshotBackup = this.m_snapshotBackup;
				}
				return snapshotBackup;
			}
			set
			{
				lock (this)
				{
					this.m_snapshotBackup = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0007D520 File Offset: 0x0007B720
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0007D564 File Offset: 0x0007B764
		public bool? SnapshotLatestFullBackup
		{
			get
			{
				bool? snapshotLatestFullBackup;
				lock (this)
				{
					this.InternalCheck();
					snapshotLatestFullBackup = this.m_snapshotLatestFullBackup;
				}
				return snapshotLatestFullBackup;
			}
			set
			{
				lock (this)
				{
					this.m_snapshotLatestFullBackup = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x0007D5EC File Offset: 0x0007B7EC
		public bool? SnapshotLatestIncrementalBackup
		{
			get
			{
				bool? snapshotLatestIncrementalBackup;
				lock (this)
				{
					this.InternalCheck();
					snapshotLatestIncrementalBackup = this.m_snapshotLatestIncrementalBackup;
				}
				return snapshotLatestIncrementalBackup;
			}
			set
			{
				lock (this)
				{
					this.m_snapshotLatestIncrementalBackup = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0007D630 File Offset: 0x0007B830
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0007D674 File Offset: 0x0007B874
		public bool? SnapshotLatestDifferentialBackup
		{
			get
			{
				bool? snapshotLatestDifferentialBackup;
				lock (this)
				{
					this.InternalCheck();
					snapshotLatestDifferentialBackup = this.m_snapshotLatestDifferentialBackup;
				}
				return snapshotLatestDifferentialBackup;
			}
			set
			{
				lock (this)
				{
					this.m_snapshotLatestDifferentialBackup = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0007D6B8 File Offset: 0x0007B8B8
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0007D6FC File Offset: 0x0007B8FC
		public bool? SnapshotLatestCopyBackup
		{
			get
			{
				bool? snapshotLatestCopyBackup;
				lock (this)
				{
					this.InternalCheck();
					snapshotLatestCopyBackup = this.m_snapshotLatestCopyBackup;
				}
				return snapshotLatestCopyBackup;
			}
			set
			{
				lock (this)
				{
					this.m_snapshotLatestCopyBackup = value;
					this.InternalCheck();
				}
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0007D740 File Offset: 0x0007B940
		public bool ConsistentDatabase
		{
			get
			{
				bool consistentDatabase;
				lock (this)
				{
					this.InternalCheck();
					consistentDatabase = this.m_consistentDatabase;
				}
				return consistentDatabase;
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0007D784 File Offset: 0x0007B984
		internal static void InternalTest()
		{
			FileState.TestInternalCheck();
			FileState fileState = new FileState();
			fileState.AssertMembersAreZero();
			fileState.SetLowestAndHighestGenerationsPresent(2L, 3L);
			fileState.SetLowestAndHighestGenerationsRequired(8L, 9L, false);
			fileState.LastGenerationBackedUp = 1L;
			fileState.CheckpointGeneration = 7L;
			DiagCore.RetailAssert(!fileState.ConsistentDatabase, "ConsistentDatabase is {0}, expected {1}", new object[]
			{
				fileState.ConsistentDatabase,
				false
			});
			DiagCore.RetailAssert(2L == fileState.LowestGenerationPresent, "LowestGenerationPresent is {0}, expected {1}", new object[]
			{
				fileState.LowestGenerationPresent,
				2
			});
			DiagCore.RetailAssert(3L == fileState.HighestGenerationPresent, "HighestGenerationPresent is {0}, expected {1}", new object[]
			{
				fileState.HighestGenerationPresent,
				3
			});
			DiagCore.RetailAssert(8L == fileState.LowestGenerationRequired, "LowestGenerationRequired is {0}, expected {1}", new object[]
			{
				fileState.LowestGenerationRequired,
				8
			});
			DiagCore.RetailAssert(9L == fileState.HighestGenerationRequired, "HighestGenerationRequired is {0}, expected {1}", new object[]
			{
				fileState.HighestGenerationRequired,
				9
			});
			DiagCore.RetailAssert(1L == fileState.LastGenerationBackedUp, "LastGenerationBackedUp is {0}, expected {1}", new object[]
			{
				fileState.LastGenerationBackedUp,
				1
			});
			DiagCore.RetailAssert(7L == fileState.CheckpointGeneration, "CheckpointGeneration is {0}, expected {1}", new object[]
			{
				fileState.CheckpointGeneration,
				7
			});
			fileState.Reset();
			fileState.AssertMembersAreZero();
			DiagCore.RetailAssert(fileState.RequiredLogfilesArePresent(), "RequiredLogfilesArePresent", new object[0]);
			fileState.SetLowestAndHighestGenerationsPresent(18L, 21L);
			fileState.SetLowestAndHighestGenerationsRequired(18L, 21L, false);
			DiagCore.RetailAssert(fileState.RequiredLogfilesArePresent(), "RequiredLogfilesArePresent", new object[0]);
			fileState.Reset();
			fileState.SetLowestAndHighestGenerationsPresent(18L, 21L);
			fileState.SetLowestAndHighestGenerationsRequired(19L, 20L, false);
			DiagCore.RetailAssert(fileState.RequiredLogfilesArePresent(), "RequiredLogfilesArePresent", new object[0]);
			fileState.Reset();
			fileState.SetLowestAndHighestGenerationsPresent(18L, 21L);
			fileState.SetLowestAndHighestGenerationsRequired(19L, 22L, false);
			DiagCore.RetailAssert(!fileState.RequiredLogfilesArePresent(), "RequiredLogfilesArePresent", new object[0]);
			fileState.Reset();
			fileState.SetLowestAndHighestGenerationsPresent(18L, 21L);
			fileState.SetLowestAndHighestGenerationsRequired(17L, 20L, false);
			DiagCore.RetailAssert(fileState.RequiredLogfilesArePresent(), "RequiredLogfilesArePresent", new object[0]);
			fileState.Reset();
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0007DA38 File Offset: 0x0007BC38
		private static void TestInternalCheck()
		{
			FileState.InternalCheck(true, "This InternalCheck should not fire", new object[0]);
			try
			{
				FileState.InternalCheck(false, "This InternalCheck should fire", new object[0]);
				DiagCore.RetailAssert(false, "Should have thrown FileStateInternalErrorException", new object[0]);
			}
			catch (FileStateInternalErrorException)
			{
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007DA90 File Offset: 0x0007BC90
		private void AssertMembersAreZero()
		{
			DiagCore.RetailAssert(0L == this.m_lowestGenerationPresent, "m_lowestGenerationPresent is not 0", new object[0]);
			DiagCore.RetailAssert(0L == this.m_highestGenerationPresent, "m_highestGenerationPresent is not 0", new object[0]);
			DiagCore.RetailAssert(0L == this.m_lowestGenerationRequired, "m_lowestGenerationRequired is not 0", new object[0]);
			DiagCore.RetailAssert(0L == this.m_highestGenerationRequired, "m_highestGenerationRequired is not 0", new object[0]);
			DiagCore.RetailAssert(0L == this.m_lastGenerationBackedUp, "m_lastGenerationBackedUp is not 0", new object[0]);
			DiagCore.RetailAssert(0L == this.m_checkpointGeneration, "m_checkpointGeneration is not 0", new object[0]);
			DiagCore.RetailAssert(this.m_logfileSignature == null, "m_logfileSignature is not null", new object[0]);
			DiagCore.RetailAssert(this.m_latestFullBackupTime == null, "m_latestFullBackupTime is not null", new object[0]);
			DiagCore.RetailAssert(this.m_latestIncrementalBackupTime == null, "m_latestIncrementalBackupTime is not null", new object[0]);
			DiagCore.RetailAssert(this.m_latestDifferentialBackupTime == null, "m_latestDifferentialBackupTime is not null", new object[0]);
			DiagCore.RetailAssert(this.m_latestCopyBackupTime == null, "m_latestCopyBackupTime is not null", new object[0]);
			DiagCore.RetailAssert(this.m_snapshotBackup == null, "m_snapshotBackup is not null", new object[0]);
			DiagCore.RetailAssert(this.m_snapshotLatestFullBackup == null, "m_snapshotLatestFullBackup is not null", new object[0]);
			DiagCore.RetailAssert(this.m_snapshotLatestIncrementalBackup == null, "m_snapshotLatestIncrementalBackup is not null", new object[0]);
			DiagCore.RetailAssert(this.m_snapshotLatestDifferentialBackup == null, "m_snapshotLatestDifferentialBackup is not null", new object[0]);
			DiagCore.RetailAssert(this.m_snapshotLatestCopyBackup == null, "m_snapshotLatestCopyBackup is not null", new object[0]);
			DiagCore.RetailAssert(!this.m_consistentDatabase, "m_consistentDatabase is not false", new object[0]);
		}

		// Token: 0x04000BC6 RID: 3014
		private long? m_e00Generation;

		// Token: 0x04000BC7 RID: 3015
		private long m_lowestGenerationPresent;

		// Token: 0x04000BC8 RID: 3016
		private long m_highestGenerationPresent;

		// Token: 0x04000BC9 RID: 3017
		private long m_lowestGenerationRequired;

		// Token: 0x04000BCA RID: 3018
		private long m_highestGenerationRequired;

		// Token: 0x04000BCB RID: 3019
		private long m_lastGenerationBackedUp;

		// Token: 0x04000BCC RID: 3020
		private long m_checkpointGeneration;

		// Token: 0x04000BCD RID: 3021
		private JET_SIGNATURE? m_logfileSignature;

		// Token: 0x04000BCE RID: 3022
		private DateTime? m_latestFullBackupTime;

		// Token: 0x04000BCF RID: 3023
		private DateTime? m_latestIncrementalBackupTime;

		// Token: 0x04000BD0 RID: 3024
		private DateTime? m_latestDifferentialBackupTime;

		// Token: 0x04000BD1 RID: 3025
		private DateTime? m_latestCopyBackupTime;

		// Token: 0x04000BD2 RID: 3026
		private bool? m_snapshotBackup;

		// Token: 0x04000BD3 RID: 3027
		private bool? m_snapshotLatestFullBackup;

		// Token: 0x04000BD4 RID: 3028
		private bool? m_snapshotLatestIncrementalBackup;

		// Token: 0x04000BD5 RID: 3029
		private bool? m_snapshotLatestDifferentialBackup;

		// Token: 0x04000BD6 RID: 3030
		private bool? m_snapshotLatestCopyBackup;

		// Token: 0x04000BD7 RID: 3031
		private bool m_consistentDatabase;
	}
}
