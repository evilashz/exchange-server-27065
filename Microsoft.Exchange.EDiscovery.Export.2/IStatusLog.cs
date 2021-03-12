using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000011 RID: 17
	internal interface IStatusLog : IDisposable
	{
		// Token: 0x06000063 RID: 99
		void ResetStatusLog(SourceInformationCollection allSourceInformation, OperationStatus status, ExportSettings exportSettings);

		// Token: 0x06000064 RID: 100
		void UpdateSourceStatus(SourceInformation source, int sourceIndex);

		// Token: 0x06000065 RID: 101
		void UpdateStatus(SourceInformationCollection allSourceInformation, OperationStatus status);

		// Token: 0x06000066 RID: 102
		ExportSettings LoadStatus(out SourceInformationCollection allSourceInformaiton, out OperationStatus status);

		// Token: 0x06000067 RID: 103
		void Delete();
	}
}
