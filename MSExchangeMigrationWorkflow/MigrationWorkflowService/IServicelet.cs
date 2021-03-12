using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MigrationWorkflowService
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IServicelet
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		bool IsEnabled { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		string Name { get; }

		// Token: 0x06000003 RID: 3
		bool Initialize();

		// Token: 0x06000004 RID: 4
		IEnumerable<IDiagnosable> GetDiagnosableComponents();

		// Token: 0x06000005 RID: 5
		void Run();

		// Token: 0x06000006 RID: 6
		void Stop();
	}
}
