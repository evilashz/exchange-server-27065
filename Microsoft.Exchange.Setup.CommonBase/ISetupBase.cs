using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Parser;

namespace Microsoft.Exchange.Setup.CommonBase
{
	// Token: 0x02000003 RID: 3
	internal interface ISetupBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		Dictionary<string, object> ParsedArguments { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		CommandLineParser Parser { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		string SourceDir { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		string TargetDir { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5
		ExitCode HasValidArgs { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6
		// (set) Token: 0x06000007 RID: 7
		CommandInteractionHandler InteractionHandler { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8
		bool IsExchangeInstalled { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9
		ISetupLogger Logger { get; }

		// Token: 0x0600000A RID: 10
		void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs);

		// Token: 0x0600000B RID: 11
		void ReportException(Exception e);

		// Token: 0x0600000C RID: 12
		void ReportError(string error);

		// Token: 0x0600000D RID: 13
		void ReportMessage(string message);

		// Token: 0x0600000E RID: 14
		void ReportMessage();

		// Token: 0x0600000F RID: 15
		void ReportWarning(string warning);

		// Token: 0x06000010 RID: 16
		void WriteError(string error);

		// Token: 0x06000011 RID: 17
		ExitCode SetupChecks();

		// Token: 0x06000012 RID: 18
		int Run();

		// Token: 0x06000013 RID: 19
		ExitCode ProcessArguments();
	}
}
