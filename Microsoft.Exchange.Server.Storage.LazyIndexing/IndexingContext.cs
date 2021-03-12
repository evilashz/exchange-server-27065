using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000013 RID: 19
	public class IndexingContext : Context
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00005045 File Offset: 0x00003245
		internal new static IndexingContext Create(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture)
		{
			return new IndexingContext(executionDiagnostics, securityContext, clientType, culture);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005050 File Offset: 0x00003250
		internal IndexingContext(ExecutionDiagnostics executionDiagnostics) : base(executionDiagnostics)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005059 File Offset: 0x00003259
		internal IndexingContext(ExecutionDiagnostics executionDiagnostics, ClientSecurityContext securityContext, ClientType clientType, CultureInfo culture) : base(executionDiagnostics, securityContext, clientType, culture)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005068 File Offset: 0x00003268
		public override void OnBeforeTableAccess(Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (!base.IsSharedMailboxOperation)
			{
				return;
			}
			switch (operationType)
			{
			case Connection.OperationType.Query:
				if (table.Equals(DatabaseSchema.PseudoIndexMaintenanceTable(base.Database).Table) || table.Equals(DatabaseSchema.PseudoIndexControlTable(base.Database).Table) || table.Equals(DatabaseSchema.PseudoIndexDefinitionTable(base.Database).Table) || table.Name.StartsWith("pi", StringComparison.Ordinal))
				{
					return;
				}
				break;
			case Connection.OperationType.Insert:
				if ((table.Equals(DatabaseSchema.PseudoIndexMaintenanceTable(base.Database).Table) || table.Name.StartsWith("pi", StringComparison.Ordinal)) && base.IsUserExclusiveLocked(Context.UserLockCheckFrame.Scope.LogicalIndex))
				{
					return;
				}
				break;
			case Connection.OperationType.Update:
				if ((table.Equals(DatabaseSchema.PseudoIndexControlTable(base.Database).Table) || table.Name.StartsWith("pi", StringComparison.Ordinal)) && base.IsUserExclusiveLocked(Context.UserLockCheckFrame.Scope.LogicalIndex))
				{
					return;
				}
				break;
			case Connection.OperationType.Delete:
				if (table.Name.StartsWith("pi", StringComparison.Ordinal) && base.IsUserExclusiveLocked(Context.UserLockCheckFrame.Scope.LogicalIndex))
				{
					return;
				}
				break;
			case Connection.OperationType.CreateTable:
				if (base.IsUserExclusiveLocked(Context.UserLockCheckFrame.Scope.LogicalIndex))
				{
					return;
				}
				break;
			}
			base.OnBeforeTableAccess(operationType, table, partitionValues);
		}
	}
}
