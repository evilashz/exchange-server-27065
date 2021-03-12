using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F9 RID: 249
	internal class OperationDataContext : DataContext
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x000126AF File Offset: 0x000108AF
		public OperationDataContext(string operationName, OperationType operationType = OperationType.None)
		{
			this.OperationName = operationName;
			this.OperationType = operationType;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000126C5 File Offset: 0x000108C5
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x000126CD File Offset: 0x000108CD
		public string OperationName { get; private set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000126D6 File Offset: 0x000108D6
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x000126DE File Offset: 0x000108DE
		public OperationType OperationType { get; private set; }

		// Token: 0x0600092F RID: 2351 RVA: 0x000126E7 File Offset: 0x000108E7
		public override string ToString()
		{
			if (this.OperationType != OperationType.None)
			{
				return string.Format("Operation: [{0}] {1}", this.OperationType, this.OperationName);
			}
			return string.Format("Operation: {0}", this.OperationName);
		}
	}
}
