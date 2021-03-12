using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000056 RID: 86
	internal class DirectReferenceErrorReporter : IReferenceErrorReporter
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000D324 File Offset: 0x0000B524
		internal DirectReferenceErrorReporter(Task.ErrorLoggerDelegate writeError)
		{
			this.writeError = writeError;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D333 File Offset: 0x0000B533
		void IReferenceErrorReporter.ReportError(Task.ErrorLoggerDelegate writeError)
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D335 File Offset: 0x0000B535
		void IReferenceErrorReporter.ValidateReference(string parameter, string referenceValue, ValidateReferenceDelegate validateReferenceMethood)
		{
			validateReferenceMethood(this.writeError);
		}

		// Token: 0x040000E1 RID: 225
		private Task.ErrorLoggerDelegate writeError;
	}
}
