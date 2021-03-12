using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000031 RID: 49
	public class WTFLogSchema : LogSchema
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000B69E File Offset: 0x0000989E
		public WTFLogSchema(WTFLogConfiguration logConfiguration) : base("Microsoft Exchange Server Active Monitoring", logConfiguration.SoftwareVersion, logConfiguration.LogType, WTFLogSchema.Headers)
		{
		}

		// Token: 0x04000131 RID: 305
		private static readonly string[] Headers = new string[]
		{
			"Timestamp",
			"Instance",
			"Type",
			"Definition",
			"CreatedBy",
			"Result",
			"Component",
			"Process:Thread",
			"LogLevel",
			"Method",
			"Source",
			"Message"
		};

		// Token: 0x02000032 RID: 50
		public enum Head
		{
			// Token: 0x04000133 RID: 307
			Timestamp,
			// Token: 0x04000134 RID: 308
			Instance,
			// Token: 0x04000135 RID: 309
			Type,
			// Token: 0x04000136 RID: 310
			Definition,
			// Token: 0x04000137 RID: 311
			CreatedBy,
			// Token: 0x04000138 RID: 312
			Result,
			// Token: 0x04000139 RID: 313
			Component,
			// Token: 0x0400013A RID: 314
			ProcessAndThreadId,
			// Token: 0x0400013B RID: 315
			LogLevel,
			// Token: 0x0400013C RID: 316
			Method,
			// Token: 0x0400013D RID: 317
			Source,
			// Token: 0x0400013E RID: 318
			Message
		}
	}
}
