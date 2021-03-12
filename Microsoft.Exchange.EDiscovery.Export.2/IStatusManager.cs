using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200007A RID: 122
	internal interface IStatusManager : IDisposable
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007F2 RID: 2034
		SourceInformationCollection AllSourceInformation { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007F3 RID: 2035
		OperationStatus CurrentStatus { get; }

		// Token: 0x060007F4 RID: 2036
		bool BeginProcedure(ProcedureType procedureRequest);

		// Token: 0x060007F5 RID: 2037
		void EndProcedure();

		// Token: 0x060007F6 RID: 2038
		void Checkpoint(string sourceId);

		// Token: 0x060007F7 RID: 2039
		void Rollback(bool removeStatusLog);
	}
}
