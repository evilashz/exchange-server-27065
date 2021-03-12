using System;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000004 RID: 4
	public class HealthHandlerResult
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000026E9 File Offset: 0x000008E9
		public HealthHandlerResult() : this(string.Empty)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026F6 File Offset: 0x000008F6
		public HealthHandlerResult(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000270D File Offset: 0x0000090D
		public string Message { get; set; }
	}
}
