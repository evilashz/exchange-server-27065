using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000086 RID: 134
	internal interface IFfoAuditSession
	{
		// Token: 0x060004E2 RID: 1250
		IEnumerable<AuditProperty> FindAuditPropertiesByInstance(Guid partitionId, Guid instanceId, string entityName);

		// Token: 0x060004E3 RID: 1251
		IEnumerable<AuditProperty> FindAuditPropertiesByAuditId(Guid partitionId, Guid auditId);

		// Token: 0x060004E4 RID: 1252
		IEnumerable<AuditHistoryResult> FindAuditHistory(string entityName, Guid? entityInstanceId, Guid partitionId, DateTime startTime, DateTime? endTime);

		// Token: 0x060004E5 RID: 1253
		IEnumerable<AuditHistoryResult> SearchAuditHistory(string entityName, string searchString, Guid? entityInstanceId, Guid partitionId, DateTime startTime, DateTime? endTime);

		// Token: 0x060004E6 RID: 1254
		void SetEntityData(Guid partitionId, string tableName, string columnName, string condition, string newValue);
	}
}
