using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000028 RID: 40
	internal class ComplianceJobProvider : IConfigDataProvider
	{
		// Token: 0x060000ED RID: 237 RVA: 0x0000619D File Offset: 0x0000439D
		public ComplianceJobProvider(OrganizationId orgId)
		{
			this.organizationId = orgId;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000061AC File Offset: 0x000043AC
		public string Source
		{
			get
			{
				return "ComplianceJobTempDB";
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000061B4 File Offset: 0x000043B4
		public void Delete(IConfigurable instance)
		{
			ComplianceJob complianceJob = (ComplianceJob)instance;
			foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in complianceJob.Bindings)
			{
				TempDatabase.Instance.Delete(keyValuePair.Value);
			}
			this.DeleteTasks(complianceJob.TenantId, complianceJob.JobRunId, null);
			TempDatabase.Instance.Delete(complianceJob);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006240 File Offset: 0x00004440
		public IEnumerable<ComplianceJob> FindComplianceJob(Guid tenantId, Guid runId)
		{
			return TempDatabase.Instance.FindComplianceJob(tenantId, runId);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000624E File Offset: 0x0000444E
		public IEnumerable<CompositeTask> FindCompositeTasks(Guid tenantId, Guid runId, ComplianceBindingType? bindingType = null, int? taskId = null)
		{
			return TempDatabase.Instance.FindCompositeTasks(tenantId, runId, bindingType, taskId);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000625F File Offset: 0x0000445F
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000064CC File Offset: 0x000046CC
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			int defaultPageSize = 100;
			bool completed = false;
			string pageCookie = string.Empty;
			int itemsToFetch = pageSize ?? int.MaxValue;
			while (itemsToFetch > 0 && !completed)
			{
				int actualPageSize = (defaultPageSize > itemsToFetch) ? itemsToFetch : defaultPageSize;
				IEnumerable<ComplianceJob> jobs = TempDatabase.Instance.FindPagedComplianceJobs(this.organizationId.OrganizationalUnit.ObjectGuid, null, ref pageCookie, out completed, actualPageSize);
				foreach (ComplianceJob job in jobs)
				{
					itemsToFetch--;
					yield return (T)((object)job);
				}
			}
			yield break;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000064F4 File Offset: 0x000046F4
		public IConfigurable FindJobsByName<T>(string jobName) where T : IConfigurable, new()
		{
			IEnumerable<T> source = TempDatabase.Instance.ReadComplianceJobByName<T>(jobName, this.organizationId.OrganizationalUnit.ObjectGuid);
			if (source.Count<T>() == 0)
			{
				return default(T);
			}
			IConfigurable configurable = source.First<T>();
			((ConfigurableObject)configurable).ResetChangeTracking(true);
			return configurable;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006550 File Offset: 0x00004750
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			IConfigurable configurable = TempDatabase.Instance.ReadComplianceJob<T>((ComplianceJobId)identity);
			((ConfigurableObject)configurable).ResetChangeTracking(true);
			return configurable;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000657B File Offset: 0x0000477B
		public void Save(IConfigurable instance)
		{
			if (instance is ComplianceJob)
			{
				this.SaveComplianceJob(instance as ComplianceJob);
				return;
			}
			if (instance is CompositeTask)
			{
				this.SaveComplianceTask(instance as CompositeTask);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000065AC File Offset: 0x000047AC
		public void SaveComplianceJob(ComplianceJob job)
		{
			if (job.ObjectState == ObjectState.New)
			{
				this.AddComplianceJob(job);
			}
			else if (job.ObjectState == ObjectState.Changed)
			{
				if (job.NewRunId)
				{
					ComplianceJob instance = (ComplianceJob)this.Read<ComplianceSearch>(job.Identity);
					this.Delete(instance);
					this.AddComplianceJob(job);
				}
				else
				{
					TempDatabase.Instance.UpdateJobTable(job);
					TempDatabase.Instance.UpdateBindingTable(job);
				}
			}
			job.NewRunId = false;
			job.ResetChangeTracking(true);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006624 File Offset: 0x00004824
		internal void UpdateWorkloadResults(Guid runId, byte[] jobResults, ComplianceBindingType bindingType, ComplianceJobStatus status)
		{
			IEnumerable<ComplianceBinding> enumerable = TempDatabase.Instance.FindComplianceJobBindings(this.organizationId.OrganizationalUnit.ObjectGuid, runId, new ComplianceBindingType?(bindingType));
			if (enumerable != null)
			{
				ComplianceBinding complianceBinding = enumerable.First<ComplianceBinding>();
				if (jobResults != null)
				{
					complianceBinding.JobResults = jobResults;
				}
				if (complianceBinding.JobStatus != ComplianceJobStatus.StatusUnknown)
				{
					complianceBinding.JobStatus = status;
				}
				switch (status)
				{
				case ComplianceJobStatus.Succeeded:
				case ComplianceJobStatus.Failed:
				case ComplianceJobStatus.PartiallySucceeded:
					this.DeleteTasks(this.organizationId.OrganizationalUnit.ObjectGuid, runId, new ComplianceBindingType?(bindingType));
					break;
				}
				TempDatabase.Instance.UpdateBindingTable(complianceBinding);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000066B8 File Offset: 0x000048B8
		private void AddComplianceJob(ComplianceJob job)
		{
			job.TenantId = this.organizationId.OrganizationalUnit.ObjectGuid;
			job.TenantInfo = this.organizationId.GetBytes(Encoding.UTF8);
			TempDatabase.Instance.InsertIntoTable<TempDatabase.ComplianceJobTable, ComplianceJob>(job);
			foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in job.Bindings)
			{
				TempDatabase.Instance.InsertIntoTable<TempDatabase.ComplianceJobBindingTable, ComplianceBinding>(keyValuePair.Value);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000674C File Offset: 0x0000494C
		private void SaveComplianceTask(CompositeTask task)
		{
			if (task.ObjectState == ObjectState.New)
			{
				task.TenantId = this.organizationId.OrganizationalUnit.ObjectGuid;
				TempDatabase.Instance.InsertIntoTable<TempDatabase.CompositeTaskTable, CompositeTask>(task);
				task.ResetChangeTracking(true);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006788 File Offset: 0x00004988
		private void DeleteTasks(Guid tenantId, Guid runId, ComplianceBindingType? type = null)
		{
			IEnumerable<CompositeTask> enumerable = TempDatabase.Instance.FindCompositeTasks(tenantId, runId, type, null);
			foreach (CompositeTask task in enumerable)
			{
				TempDatabase.Instance.Delete(task);
			}
		}

		// Token: 0x04000093 RID: 147
		private const string DatabaseName = "ComplianceJobTempDB";

		// Token: 0x04000094 RID: 148
		private readonly OrganizationId organizationId;
	}
}
