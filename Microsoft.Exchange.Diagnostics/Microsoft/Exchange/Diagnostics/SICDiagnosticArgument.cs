using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SICDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x00025758 File Offset: 0x00023958
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["invokescan"] = typeof(bool);
			schema["issue"] = typeof(bool);
			schema["maxsize"] = typeof(int);
		}

		// Token: 0x040006CE RID: 1742
		public const string InvokeScanArgument = "invokescan";

		// Token: 0x040006CF RID: 1743
		public const string IssueArgument = "issue";

		// Token: 0x040006D0 RID: 1744
		public const string MaxSizeArgument = "maxsize";
	}
}
