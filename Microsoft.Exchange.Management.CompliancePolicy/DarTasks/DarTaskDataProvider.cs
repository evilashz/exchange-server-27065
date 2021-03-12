using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000A RID: 10
	internal sealed class DarTaskDataProvider : IConfigDataProvider
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public DarTaskDataProvider(DarTaskParams darParams, string serverFqdn)
		{
			this.darParams = darParams;
			this.serverFqdn = serverFqdn;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002BEF File Offset: 0x00000DEF
		public string Source
		{
			get
			{
				return typeof(HostRpcClient).FullName;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C00 File Offset: 0x00000E00
		public void Delete(IConfigurable instance)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C07 File Offset: 0x00000E07
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.FindPaged<TaskStoreObject>(filter, rootId, deepSearch, sortBy, 0).ToArray<TaskStoreObject>();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002E18 File Offset: 0x00001018
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			using (HostRpcClient client = new HostRpcClient(this.serverFqdn))
			{
				foreach (TaskStoreObject task in client.GetDarTask(this.darParams))
				{
					yield return (T)((object)task);
				}
			}
			yield break;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E38 File Offset: 0x00001038
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			using (HostRpcClient hostRpcClient = new HostRpcClient(this.serverFqdn))
			{
				DarTaskParams darTaskParams = new DarTaskParams
				{
					TaskId = this.darParams.TaskId,
					TenantId = this.darParams.TenantId
				};
				TaskStoreObject[] darTask = hostRpcClient.GetDarTask(darTaskParams);
				int num = 0;
				if (num < darTask.Length)
				{
					TaskStoreObject taskStoreObject = darTask[num];
					return (T)((object)taskStoreObject);
				}
			}
			return null;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002ED4 File Offset: 0x000010D4
		public void Save(IConfigurable instance)
		{
			using (HostRpcClient hostRpcClient = new HostRpcClient(this.serverFqdn))
			{
				hostRpcClient.SetDarTask((TaskStoreObject)instance);
			}
		}

		// Token: 0x04000026 RID: 38
		private readonly DarTaskParams darParams;

		// Token: 0x04000027 RID: 39
		private readonly string serverFqdn;
	}
}
