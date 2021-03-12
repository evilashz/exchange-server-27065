using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar
{
	// Token: 0x02000003 RID: 3
	internal class ExDarTaskAggregateProvider : DarTaskAggregateProvider
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002173 File Offset: 0x00000373
		public ExDarTaskAggregateProvider(DarServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002182 File Offset: 0x00000382
		public override void Delete(string scopeId, string taskType)
		{
			InstanceManager.Current.TaskAggregates.Remove(scopeId, taskType, OperationContext.CorrelationId);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219B File Offset: 0x0000039B
		public override DarTaskAggregate Find(string scopeId, string taskType)
		{
			return InstanceManager.Current.TaskAggregates.Get(scopeId, taskType, OperationContext.CorrelationId);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D0 File Offset: 0x000003D0
		public override IEnumerable<DarTaskAggregate> FindAll(string scopeId)
		{
			return from type in this.provider.DarTaskFactory.GetAllTaskTypes()
			select this.Find(scopeId, type);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002212 File Offset: 0x00000412
		public override void Save(DarTaskAggregate taskAggregate)
		{
			TaskHelper.Validate(taskAggregate, this.provider);
			InstanceManager.Current.TaskAggregates.Set(taskAggregate, OperationContext.CorrelationId);
		}

		// Token: 0x04000003 RID: 3
		private DarServiceProvider provider;
	}
}
