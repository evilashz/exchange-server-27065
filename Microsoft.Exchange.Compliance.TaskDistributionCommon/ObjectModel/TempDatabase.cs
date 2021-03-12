using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x0200003B RID: 59
	internal class TempDatabase
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00007B81 File Offset: 0x00005D81
		private TempDatabase()
		{
			this.CreateDatabase();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007B8F File Offset: 0x00005D8F
		internal static TempDatabase Instance
		{
			get
			{
				if (TempDatabase.instance == null)
				{
					TempDatabase.instance = new TempDatabase();
				}
				return TempDatabase.instance;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public void Delete(ComplianceJob job)
		{
			using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dataContext = new DataContext(sqlConnection))
				{
					Table<TempDatabase.ComplianceJobTable> table = dataContext.GetTable<TempDatabase.ComplianceJobTable>();
					IQueryable<TempDatabase.ComplianceJobTable> queryable = from jobRow in table
					where jobRow.JobId == ((ComplianceJobId)job.Identity).Guid
					select jobRow;
					if (queryable != null && queryable.Count<TempDatabase.ComplianceJobTable>() > 0)
					{
						TempDatabase.ComplianceJobTable entity = queryable.First<TempDatabase.ComplianceJobTable>();
						table.DeleteOnSubmit(entity);
						dataContext.SubmitChanges();
					}
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007CEC File Offset: 0x00005EEC
		public void Delete(ComplianceBinding binding)
		{
			using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dataContext = new DataContext(sqlConnection))
				{
					Table<TempDatabase.ComplianceJobBindingTable> table = dataContext.GetTable<TempDatabase.ComplianceJobBindingTable>();
					IQueryable<TempDatabase.ComplianceJobBindingTable> queryable = from bindingRow in table
					where bindingRow.JobRunId == binding.JobRunId && (int)bindingRow.BindingType == (int)binding.BindingType
					select bindingRow;
					if (queryable != null && queryable.Count<TempDatabase.ComplianceJobBindingTable>() > 0)
					{
						TempDatabase.ComplianceJobBindingTable entity = queryable.First<TempDatabase.ComplianceJobBindingTable>();
						table.DeleteOnSubmit(entity);
						dataContext.SubmitChanges();
					}
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007E80 File Offset: 0x00006080
		public void Delete(CompositeTask task)
		{
			using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dataContext = new DataContext(sqlConnection))
				{
					Table<TempDatabase.CompositeTaskTable> table = dataContext.GetTable<TempDatabase.CompositeTaskTable>();
					IQueryable<TempDatabase.CompositeTaskTable> queryable = from taskRow in table
					where taskRow.JobRunId == task.JobRunId && taskRow.TaskId == task.TaskId
					select taskRow;
					if (queryable != null && queryable.Count<TempDatabase.CompositeTaskTable>() > 0)
					{
						TempDatabase.CompositeTaskTable entity = queryable.First<TempDatabase.CompositeTaskTable>();
						table.DeleteOnSubmit(entity);
						dataContext.SubmitChanges();
					}
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007FEC File Offset: 0x000061EC
		public IEnumerable<ComplianceJob> FindPagedComplianceJobs(Guid tenantId, string jobName, ref string pageCookie, out bool complete, int pageSize = 100)
		{
			complete = false;
			int num = -1;
			if (string.IsNullOrEmpty(pageCookie))
			{
				num = 0;
			}
			else if (!int.TryParse(pageCookie, out num) || num < 0)
			{
				complete = true;
				return Enumerable.Empty<ComplianceJob>();
			}
			IEnumerable<ComplianceJob> enumerable = this.FindPagedComplianceJobsInternal(tenantId, jobName, num, pageSize);
			if (enumerable.Count<ComplianceJob>() < pageSize)
			{
				complete = true;
			}
			num++;
			pageCookie = num.ToString();
			return enumerable;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008050 File Offset: 0x00006250
		internal void InsertIntoTable<TTable, TData>(TData data) where TTable : TempDatabase.IComplianceTable<TData>, new()
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
				{
					using (TempDatabase.ComplianceJobStore complianceJobStore = new TempDatabase.ComplianceJobStore(sqlConnection))
					{
						TTable ttable = (default(TTable) == null) ? Activator.CreateInstance<TTable>() : default(TTable);
						ttable.SetRowData(data);
						ttable.InsertRow(complianceJobStore);
						complianceJobStore.SubmitChanges();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008104 File Offset: 0x00006304
		internal void UpdateJobTable(ComplianceJob newJob)
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
				{
					using (DataContext dataContext = new DataContext(sqlConnection))
					{
						Table<TempDatabase.ComplianceJobTable> table = dataContext.GetTable<TempDatabase.ComplianceJobTable>();
						IQueryable<TempDatabase.ComplianceJobTable> queryable = from job in table
						where job.JobId == ((ComplianceJobId)newJob.Identity).Guid
						select job;
						if (queryable != null && queryable.Count<TempDatabase.ComplianceJobTable>() > 0)
						{
							TempDatabase.ComplianceJobTable complianceJobTable = queryable.First<TempDatabase.ComplianceJobTable>();
							complianceJobTable.SetRowData(newJob);
							dataContext.SubmitChanges();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000827C File Offset: 0x0000647C
		internal void UpdateBindingTable(ComplianceJob job)
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
				{
					using (DataContext dataContext = new DataContext(sqlConnection))
					{
						Table<TempDatabase.ComplianceJobBindingTable> table = dataContext.GetTable<TempDatabase.ComplianceJobBindingTable>();
						IQueryable<TempDatabase.ComplianceJobBindingTable> queryable = from binding in table
						where binding.JobRunId == job.JobRunId
						select binding;
						foreach (TempDatabase.ComplianceJobBindingTable complianceJobBindingTable in queryable)
						{
							complianceJobBindingTable.SetRowData(job);
						}
						dataContext.SubmitChanges();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000083D0 File Offset: 0x000065D0
		internal void UpdateBindingTable(ComplianceBinding newBinding)
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
				{
					using (DataContext dataContext = new DataContext(sqlConnection))
					{
						Table<TempDatabase.ComplianceJobBindingTable> table = dataContext.GetTable<TempDatabase.ComplianceJobBindingTable>();
						IQueryable<TempDatabase.ComplianceJobBindingTable> queryable = from binding in table
						where binding.JobRunId == newBinding.JobRunId && (int)binding.BindingType == (int)newBinding.BindingType
						select binding;
						foreach (TempDatabase.ComplianceJobBindingTable complianceJobBindingTable in queryable)
						{
							complianceJobBindingTable.SetRowData(newBinding);
						}
						dataContext.SubmitChanges();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008970 File Offset: 0x00006B70
		internal IEnumerable<T> ReadComplianceJobByName<T>(string jobName, Guid tenantId) where T : IConfigurable, new()
		{
			using (SqlConnection dbConn = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dbContext = new DataContext(dbConn))
				{
					Table<TempDatabase.ComplianceJobTable> jobs = dbContext.GetTable<TempDatabase.ComplianceJobTable>();
					IQueryable<TempDatabase.ComplianceJobTable> jobQuery = from job in jobs
					where job.DisplayName.Equals(jobName) && job.TenantId == tenantId
					select job;
					foreach (TempDatabase.ComplianceJobTable row in jobQuery)
					{
						ComplianceJob newJob = this.CreateComplianceJobFromTableRow<T>(row, dbContext);
						yield return (T)((object)newJob);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000089A4 File Offset: 0x00006BA4
		internal IConfigurable ReadComplianceJob<T>(ComplianceJobId identity) where T : IConfigurable, new()
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString()))
				{
					using (DataContext dataContext = new DataContext(sqlConnection))
					{
						Table<TempDatabase.ComplianceJobTable> table = dataContext.GetTable<TempDatabase.ComplianceJobTable>();
						IQueryable<TempDatabase.ComplianceJobTable> source = from job in table
						where job.JobId == identity.Guid
						select job;
						int num = source.Count<TempDatabase.ComplianceJobTable>();
						if (num == 1)
						{
							TempDatabase.ComplianceJobTable row = source.First<TempDatabase.ComplianceJobTable>();
							return this.CreateComplianceJobFromTableRow<T>(row, dataContext);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008E78 File Offset: 0x00007078
		internal IEnumerable<ComplianceJob> FindComplianceJob(Guid tenantId, Guid jobRunId)
		{
			using (SqlConnection dbConn = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dbContext = new DataContext(dbConn))
				{
					Table<TempDatabase.ComplianceJobTable> jobs = dbContext.GetTable<TempDatabase.ComplianceJobTable>();
					IQueryable<TempDatabase.ComplianceJobTable> jobQuery = from job in jobs
					where job.TenantId == tenantId && job.JobRunId == jobRunId
					select job;
					foreach (TempDatabase.ComplianceJobTable row in jobQuery)
					{
						ComplianceJob newObj = this.CreateComplianceJobFromTableRow<ComplianceSearch>(row, dbContext);
						yield return newObj;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000093A0 File Offset: 0x000075A0
		internal IEnumerable<CompositeTask> FindCompositeTasks(Guid tenantId, Guid jobRunId, ComplianceBindingType? bindingType = null, int? taskId = null)
		{
			using (SqlConnection dbConn = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dbContext = new DataContext(dbConn))
				{
					Table<TempDatabase.CompositeTaskTable> tasks = dbContext.GetTable<TempDatabase.CompositeTaskTable>();
					IQueryable<TempDatabase.CompositeTaskTable> taskQuery = from task in tasks
					where task.TenantId == tenantId && task.JobRunId == jobRunId && (taskId == null || (int?)task.TaskId == taskId) && ((int?)bindingType == (int?)null || (int?)task.BindingType == (int?)bindingType)
					select task;
					foreach (TempDatabase.CompositeTaskTable row in taskQuery)
					{
						yield return row.GetCompositeTask();
					}
				}
			}
			yield break;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009868 File Offset: 0x00007A68
		internal IEnumerable<ComplianceBinding> FindComplianceJobBindings(Guid tenantId, Guid runId, ComplianceBindingType? bindingType = null)
		{
			using (SqlConnection dbConn = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dbContext = new DataContext(dbConn))
				{
					Table<TempDatabase.ComplianceJobBindingTable> bindings = dbContext.GetTable<TempDatabase.ComplianceJobBindingTable>();
					IQueryable<TempDatabase.ComplianceJobBindingTable> bindingQuery = from binding in bindings
					where binding.JobRunId == runId && binding.TenantId == tenantId && ((int?)bindingType == (int?)null || (int)binding.BindingType == (int)bindingType.Value)
					select binding;
					foreach (TempDatabase.ComplianceJobBindingTable bindingRow in bindingQuery)
					{
						ComplianceBinding bindingObj = new ComplianceBinding();
						bindingObj.CopyFromRow(bindingRow);
						yield return bindingObj;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009CB8 File Offset: 0x00007EB8
		private IEnumerable<ComplianceJob> FindPagedComplianceJobsInternal(Guid tenantId, string jobName, int pageIndex, int pageSize = 100)
		{
			using (SqlConnection dbConn = new SqlConnection(this.GetConnectionString()))
			{
				using (DataContext dbContext = new DataContext(dbConn))
				{
					Table<TempDatabase.ComplianceJobTable> jobs = dbContext.GetTable<TempDatabase.ComplianceJobTable>();
					IQueryable<TempDatabase.ComplianceJobTable> jobQuery = (from job in jobs
					where job.TenantId == tenantId && (jobName == null || job.DisplayName.Equals(jobName))
					select job).Skip(pageIndex * pageSize).Take(pageSize);
					foreach (TempDatabase.ComplianceJobTable row in jobQuery)
					{
						ComplianceJob newObj = this.CreateComplianceJobFromTableRow<ComplianceSearch>(row, dbContext);
						yield return newObj;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009CFC File Offset: 0x00007EFC
		private ComplianceJob CreateComplianceJobFromTableRow<T>(TempDatabase.ComplianceJobTable row, DataContext dbContext) where T : IConfigurable, new()
		{
			ComplianceJob newJob = (ComplianceJob)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			newJob.CopyFromRow(row);
			Table<TempDatabase.ComplianceJobBindingTable> table = dbContext.GetTable<TempDatabase.ComplianceJobBindingTable>();
			IQueryable<TempDatabase.ComplianceJobBindingTable> queryable = from binding in table
			where binding.JobRunId == row.JobRunId && binding.TenantId == newJob.TenantId
			select binding;
			foreach (TempDatabase.ComplianceJobBindingTable complianceJobBindingTable in queryable)
			{
				newJob.Bindings[complianceJobBindingTable.BindingType].CopyFromRow(complianceJobBindingTable);
			}
			newJob.UpdateJobResults();
			return newJob;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009EA0 File Offset: 0x000080A0
		private string GetConnectionString()
		{
			return string.Format("Server={0};database={1};Trusted_Connection=Yes;", this.databaseServerName, "ComplianceJobTempDB");
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009EB8 File Offset: 0x000080B8
		private void CreateDatabase()
		{
			try
			{
				string userDomainName = Environment.UserDomainName;
				this.databaseServerName = userDomainName.Substring(0, userDomainName.IndexOf("dom", StringComparison.InvariantCultureIgnoreCase));
				string connectionString = string.Format("Server={0};Trusted_Connection=Yes;", this.databaseServerName);
				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					using (DataContext dataContext = new DataContext(sqlConnection))
					{
						Table<TempDatabase.SysDatabaseTable> table = dataContext.GetTable<TempDatabase.SysDatabaseTable>();
						IQueryable<TempDatabase.SysDatabaseTable> source = from db in table
						where db.Name == "ComplianceJobTempDB"
						select db;
						int num = source.Count<TempDatabase.SysDatabaseTable>();
						if (num <= 0)
						{
							using (TempDatabase.ComplianceJobStore complianceJobStore = new TempDatabase.ComplianceJobStore(sqlConnection))
							{
								complianceJobStore.CreateDatabase();
							}
						}
					}
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		// Token: 0x040000FF RID: 255
		private const string DatabaseName = "ComplianceJobTempDB";

		// Token: 0x04000100 RID: 256
		private const string TableNameComplianceJob = "ComplianceJobTempTable";

		// Token: 0x04000101 RID: 257
		private const string TableNameComplianceJobBinding = "ComplianceJobBindingTempTable";

		// Token: 0x04000102 RID: 258
		private const string TableNameCompositeTask = "CompositeTaskTempTable";

		// Token: 0x04000103 RID: 259
		private static TempDatabase instance;

		// Token: 0x04000104 RID: 260
		private string databaseServerName;

		// Token: 0x0200003C RID: 60
		public interface IComplianceTable<TData>
		{
			// Token: 0x06000187 RID: 391
			void SetRowData(TData data);

			// Token: 0x06000188 RID: 392
			void InsertRow(TempDatabase.ComplianceJobStore store);
		}

		// Token: 0x0200003D RID: 61
		[Database(Name = "ComplianceJobTempDB")]
		internal class ComplianceJobStore : DataContext
		{
			// Token: 0x06000189 RID: 393 RVA: 0x00009FFA File Offset: 0x000081FA
			internal ComplianceJobStore(string connection) : base(connection)
			{
				this.Jobs = base.GetTable<TempDatabase.ComplianceJobTable>();
				this.Bindings = base.GetTable<TempDatabase.ComplianceJobBindingTable>();
				this.Tasks = base.GetTable<TempDatabase.CompositeTaskTable>();
			}

			// Token: 0x0600018A RID: 394 RVA: 0x0000A027 File Offset: 0x00008227
			internal ComplianceJobStore(SqlConnection conn) : base(conn)
			{
				this.Jobs = base.GetTable<TempDatabase.ComplianceJobTable>();
				this.Bindings = base.GetTable<TempDatabase.ComplianceJobBindingTable>();
				this.Tasks = base.GetTable<TempDatabase.CompositeTaskTable>();
			}

			// Token: 0x04000105 RID: 261
			internal Table<TempDatabase.ComplianceJobTable> Jobs;

			// Token: 0x04000106 RID: 262
			internal Table<TempDatabase.ComplianceJobBindingTable> Bindings;

			// Token: 0x04000107 RID: 263
			internal Table<TempDatabase.CompositeTaskTable> Tasks;
		}

		// Token: 0x0200003E RID: 62
		[Table(Name = "dbo.ComplianceJobTempTable")]
		internal class ComplianceJobTable : TempDatabase.IComplianceTable<ComplianceJob>
		{
			// Token: 0x0600018C RID: 396 RVA: 0x0000A05C File Offset: 0x0000825C
			public void SetRowData(ComplianceJob job)
			{
				this.DisplayName = job.Name;
				this.JobId = ((ComplianceJobId)job.Identity).Guid;
				this.CreateTime = job.CreatedTime;
				this.LastModifiedTime = job.LastModifiedTime;
				this.StartTime = job.JobStartTime;
				this.EndTime = job.JobEndTime;
				this.Description = job.Description;
				this.JobObjectVersion = job.JobObjectVersion;
				this.TenantId = job.TenantId;
				this.JobType = job.JobType;
				this.CreatedBy = job.CreatedBy;
				this.RunBy = job.RunBy;
				this.JobRunId = job.JobRunId;
				this.TenantInfo = job.TenantInfo;
				this.JobData = job.JobData;
				this.Resume = job.Resume;
			}

			// Token: 0x0600018D RID: 397 RVA: 0x0000A133 File Offset: 0x00008333
			public void InsertRow(TempDatabase.ComplianceJobStore store)
			{
				store.Jobs.InsertOnSubmit(this);
			}

			// Token: 0x04000108 RID: 264
			[Column]
			public string DisplayName;

			// Token: 0x04000109 RID: 265
			[Column]
			public Guid TenantId;

			// Token: 0x0400010A RID: 266
			[Column(IsPrimaryKey = true)]
			public Guid JobId;

			// Token: 0x0400010B RID: 267
			[Column]
			public ComplianceJobObjectVersion JobObjectVersion;

			// Token: 0x0400010C RID: 268
			[Column]
			public string Description;

			// Token: 0x0400010D RID: 269
			[Column]
			public ComplianceJobType JobType;

			// Token: 0x0400010E RID: 270
			[Column]
			public DateTime CreateTime;

			// Token: 0x0400010F RID: 271
			[Column]
			public DateTime LastModifiedTime;

			// Token: 0x04000110 RID: 272
			[Column]
			public string CreatedBy;

			// Token: 0x04000111 RID: 273
			[Column]
			public byte[] TenantInfo;

			// Token: 0x04000112 RID: 274
			[Column]
			public byte[] JobData;

			// Token: 0x04000113 RID: 275
			[Column]
			public Guid JobRunId;

			// Token: 0x04000114 RID: 276
			[Column]
			public string RunBy;

			// Token: 0x04000115 RID: 277
			[Column]
			public DateTime StartTime;

			// Token: 0x04000116 RID: 278
			[Column]
			public DateTime EndTime;

			// Token: 0x04000117 RID: 279
			[Column]
			public bool Resume;
		}

		// Token: 0x0200003F RID: 63
		[Table(Name = "dbo.ComplianceJobBindingTempTable")]
		internal class ComplianceJobBindingTable : TempDatabase.IComplianceTable<ComplianceBinding>
		{
			// Token: 0x0600018F RID: 399 RVA: 0x0000A14C File Offset: 0x0000834C
			public void SetRowData(ComplianceBinding binding)
			{
				this.TenantId = binding.TenantId;
				this.BindingType = binding.BindingType;
				this.Bindings = binding.Bindings;
				this.BindingOptions = binding.BindingOptions;
				this.JobStartTime = binding.JobStartTime;
				this.JobRunId = binding.JobRunId;
				this.JobStatus = binding.JobStatus;
				this.NumberBindings = binding.NumBindings;
				this.NumberBindingsFailed = binding.NumBindingsFailed;
				this.JobResults = binding.JobResults;
				this.JobMaster = binding.JobMaster;
			}

			// Token: 0x06000190 RID: 400 RVA: 0x0000A1DD File Offset: 0x000083DD
			public void InsertRow(TempDatabase.ComplianceJobStore store)
			{
				store.Bindings.InsertOnSubmit(this);
			}

			// Token: 0x06000191 RID: 401 RVA: 0x0000A1EB File Offset: 0x000083EB
			public void SetRowData(ComplianceJob job)
			{
				this.SetRowData(job.Bindings[this.BindingType]);
			}

			// Token: 0x04000118 RID: 280
			[Column]
			public Guid TenantId;

			// Token: 0x04000119 RID: 281
			[Column(IsPrimaryKey = true)]
			public Guid JobRunId;

			// Token: 0x0400011A RID: 282
			[Column(IsPrimaryKey = true)]
			public ComplianceBindingType BindingType;

			// Token: 0x0400011B RID: 283
			[Column]
			public string Bindings;

			// Token: 0x0400011C RID: 284
			[Column]
			public ushort BindingOptions;

			// Token: 0x0400011D RID: 285
			[Column]
			public DateTime JobStartTime;

			// Token: 0x0400011E RID: 286
			[Column]
			public string JobMaster;

			// Token: 0x0400011F RID: 287
			[Column]
			public int NumberBindings;

			// Token: 0x04000120 RID: 288
			[Column]
			public int NumberBindingsFailed;

			// Token: 0x04000121 RID: 289
			[Column]
			public ComplianceJobStatus JobStatus;

			// Token: 0x04000122 RID: 290
			[Column]
			public byte[] JobResults;
		}

		// Token: 0x02000040 RID: 64
		[Table(Name = "dbo.CompositeTaskTempTable")]
		internal class CompositeTaskTable : TempDatabase.IComplianceTable<CompositeTask>
		{
			// Token: 0x06000193 RID: 403 RVA: 0x0000A20C File Offset: 0x0000840C
			public void SetRowData(CompositeTask task)
			{
				this.TenantId = task.TenantId;
				this.JobRunId = task.JobRunId;
				this.TaskId = task.TaskId;
				this.BindingType = task.BindingType;
				this.UserMaster = task.UserMaster;
				this.UserList = task.UserList;
			}

			// Token: 0x06000194 RID: 404 RVA: 0x0000A261 File Offset: 0x00008461
			public void InsertRow(TempDatabase.ComplianceJobStore store)
			{
				store.Tasks.InsertOnSubmit(this);
			}

			// Token: 0x06000195 RID: 405 RVA: 0x0000A270 File Offset: 0x00008470
			public CompositeTask GetCompositeTask()
			{
				return new CompositeTask
				{
					TenantId = this.TenantId,
					JobRunId = this.JobRunId,
					TaskId = this.TaskId,
					BindingType = this.BindingType,
					UserMaster = this.UserMaster,
					UserList = this.UserList
				};
			}

			// Token: 0x04000123 RID: 291
			[Column]
			public Guid TenantId;

			// Token: 0x04000124 RID: 292
			[Column(IsPrimaryKey = true)]
			public Guid JobRunId;

			// Token: 0x04000125 RID: 293
			[Column(IsPrimaryKey = true)]
			public int TaskId;

			// Token: 0x04000126 RID: 294
			[Column]
			public ComplianceBindingType BindingType;

			// Token: 0x04000127 RID: 295
			[Column]
			public string UserMaster;

			// Token: 0x04000128 RID: 296
			[Column]
			public string UserList;
		}

		// Token: 0x02000041 RID: 65
		internal class SysView : DataContext
		{
			// Token: 0x06000196 RID: 406 RVA: 0x0000A2CC File Offset: 0x000084CC
			internal SysView(string connection) : base(connection)
			{
			}

			// Token: 0x04000129 RID: 297
			internal Table<TempDatabase.SysDatabaseTable> Databases;
		}

		// Token: 0x02000042 RID: 66
		[Table(Name = "sys.databases")]
		internal class SysDatabaseTable
		{
			// Token: 0x0400012A RID: 298
			[Column]
			internal int DatabaseId;

			// Token: 0x0400012B RID: 299
			[Column]
			internal string Name;
		}
	}
}
