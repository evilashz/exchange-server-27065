using System;
using System.IO;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000133 RID: 307
	// (Invoke) Token: 0x06000F86 RID: 3974
	internal delegate MigrationJobReportingCursor MigrationJobReportWriterDelegate(MigrationJobReportingCursor cursorInitialPosition, StreamWriter successWriter, StreamWriter failureWriter);
}
