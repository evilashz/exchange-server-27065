using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000118 RID: 280
	internal static class DataDrivenConstants
	{
		// Token: 0x04001C9D RID: 7325
		public const string Identity = "Identity";

		// Token: 0x04001C9E RID: 7326
		public const string Force = "Force";

		// Token: 0x04001C9F RID: 7327
		public const string ShouldContinueContextKey = "ShouldContinueContext";

		// Token: 0x04001CA0 RID: 7328
		public const string LambdaExpression = "LambdaExpression";

		// Token: 0x04001CA1 RID: 7329
		public const string LambdaSeparator = "=>";

		// Token: 0x04001CA2 RID: 7330
		public const string UnlimitedValueString = "unlimited";

		// Token: 0x04001CA3 RID: 7331
		public const string ShouldContinue = "ShouldContinue";

		// Token: 0x04001CA4 RID: 7332
		public const string LastErrorContext = "LastErrorContext";

		// Token: 0x04001CA5 RID: 7333
		public static readonly string[] ReservedVariableNames = new string[]
		{
			"ShouldContinue",
			"LastErrorContext"
		};
	}
}
