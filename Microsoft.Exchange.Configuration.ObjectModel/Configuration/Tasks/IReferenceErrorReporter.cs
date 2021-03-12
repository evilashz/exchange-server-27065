using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000055 RID: 85
	internal interface IReferenceErrorReporter
	{
		// Token: 0x0600037E RID: 894
		void ReportError(Task.ErrorLoggerDelegate writeError);

		// Token: 0x0600037F RID: 895
		void ValidateReference(string parameter, string referenceValue, ValidateReferenceDelegate validateReferenceMethood);
	}
}
