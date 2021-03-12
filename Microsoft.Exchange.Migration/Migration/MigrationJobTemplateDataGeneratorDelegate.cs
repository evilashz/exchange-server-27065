using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000134 RID: 308
	// (Invoke) Token: 0x06000F8A RID: 3978
	internal delegate IDictionary<string, string> MigrationJobTemplateDataGeneratorDelegate(MigrationJobReportingCursor migrationReportData, string successReportLink, string failureReportLink);
}
