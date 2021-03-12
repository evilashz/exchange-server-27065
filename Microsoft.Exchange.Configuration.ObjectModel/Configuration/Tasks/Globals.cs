using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200000C RID: 12
	internal class Globals
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004100 File Offset: 0x00002300
		public static string AuditingComponentName
		{
			get
			{
				return Globals.auditingComponentName;
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly string auditingComponentName = "Exchange Management";
	}
}
