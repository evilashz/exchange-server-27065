using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000100 RID: 256
	internal class MailAppsArgument : DiagnosableArgument
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x0002C6FC File Offset: 0x0002A8FC
		public MailAppsArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002C70C File Offset: 0x0002A90C
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["org"] = typeof(string);
			schema["usr"] = typeof(string);
			schema["cmd"] = typeof(string);
			schema["len"] = typeof(int);
		}

		// Token: 0x0400057D RID: 1405
		public const string OrgArgument = "org";

		// Token: 0x0400057E RID: 1406
		public const string UserArgument = "usr";

		// Token: 0x0400057F RID: 1407
		public const string CommandArgument = "cmd";

		// Token: 0x04000580 RID: 1408
		public const string LengthArgument = "len";
	}
}
