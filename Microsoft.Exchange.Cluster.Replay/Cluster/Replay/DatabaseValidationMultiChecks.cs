using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200021B RID: 539
	internal abstract class DatabaseValidationMultiChecks : IEnumerable<DatabaseValidationCheck>, IEnumerable
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x000527FC File Offset: 0x000509FC
		protected DatabaseValidationMultiChecks()
		{
			this.DefineChecks();
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00052817 File Offset: 0x00050A17
		protected virtual void DefineChecks()
		{
			this.AddCheck(new DatabaseCheckDatabaseIsReplicated());
			this.AddCheck(new DatabaseCheckCopyStatusNotStale());
			this.AddCheck(new DatabaseCheckCopyStatusRpcSuccessful());
			this.AddCheck(new DatabaseCheckServerInMaintenanceMode());
			this.AddCheck(new DatabaseCheckActivationDisfavored());
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00052850 File Offset: 0x00050A50
		protected void AddCheck(DatabaseValidationCheck check)
		{
			this.m_checks.Add(check);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005285E File Offset: 0x00050A5E
		public IEnumerator<DatabaseValidationCheck> GetEnumerator()
		{
			return this.m_checks.GetEnumerator();
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00052870 File Offset: 0x00050A70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.m_checks).GetEnumerator();
		}

		// Token: 0x040007F9 RID: 2041
		private List<DatabaseValidationCheck> m_checks = new List<DatabaseValidationCheck>(10);
	}
}
