using System;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000008 RID: 8
	public class ExceptionDetails
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002DE2 File Offset: 0x00000FE2
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002DEA File Offset: 0x00000FEA
		public DateTime ExceptionTimeUtc { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DF3 File Offset: 0x00000FF3
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002DFB File Offset: 0x00000FFB
		public string ExceptionType { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E04 File Offset: 0x00001004
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E0C File Offset: 0x0000100C
		public string Details { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002E18 File Offset: 0x00001018
		internal static ExceptionDetails Create(Exception e)
		{
			ExceptionDetails result = null;
			if (e != null)
			{
				result = new ExceptionDetails
				{
					ExceptionTimeUtc = DateTime.UtcNow,
					ExceptionType = e.GetType().ToString(),
					Details = e.ToString()
				};
			}
			return result;
		}
	}
}
