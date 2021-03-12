using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000B RID: 11
	internal sealed class DarTaskAggregateDataProvider : IConfigDataProvider
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002F18 File Offset: 0x00001118
		public DarTaskAggregateDataProvider(DarTaskAggregateParams darParams, string serverFqdn)
		{
			this.darParams = darParams;
			this.serverFqdn = serverFqdn;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F2E File Offset: 0x0000112E
		public string Source
		{
			get
			{
				return typeof(HostRpcClient).FullName;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F3F File Offset: 0x0000113F
		public void Delete(IConfigurable instance)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F46 File Offset: 0x00001146
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.FindPaged<TaskAggregateStoreObject>(filter, rootId, deepSearch, sortBy, 0).ToArray<TaskAggregateStoreObject>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003158 File Offset: 0x00001358
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			using (HostRpcClient client = new HostRpcClient(this.serverFqdn))
			{
				foreach (TaskAggregateStoreObject task in client.GetDarTaskAggregate(this.darParams))
				{
					yield return (T)((object)task);
				}
			}
			yield break;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003178 File Offset: 0x00001378
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			using (HostRpcClient hostRpcClient = new HostRpcClient(this.serverFqdn))
			{
				DarTaskAggregateParams darTaskAggregateParams = new DarTaskAggregateParams
				{
					TaskId = this.darParams.TaskId,
					TenantId = this.darParams.TenantId
				};
				TaskAggregateStoreObject[] darTaskAggregate = hostRpcClient.GetDarTaskAggregate(darTaskAggregateParams);
				int num = 0;
				if (num < darTaskAggregate.Length)
				{
					TaskAggregateStoreObject taskAggregateStoreObject = darTaskAggregate[num];
					return (T)((object)taskAggregateStoreObject);
				}
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003214 File Offset: 0x00001414
		public void Save(IConfigurable instance)
		{
			using (HostRpcClient hostRpcClient = new HostRpcClient(this.serverFqdn))
			{
				hostRpcClient.SetDarTaskAggregate((TaskAggregateStoreObject)instance);
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly DarTaskAggregateParams darParams;

		// Token: 0x04000029 RID: 41
		private readonly string serverFqdn;
	}
}
