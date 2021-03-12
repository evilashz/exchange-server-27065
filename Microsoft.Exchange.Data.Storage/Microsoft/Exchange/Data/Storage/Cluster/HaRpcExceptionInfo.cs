using System;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	public class HaRpcExceptionInfo
	{
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000C39B9 File Offset: 0x000C1BB9
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x000C39C1 File Offset: 0x000C1BC1
		internal string OriginatingServer { get; set; }

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000C39CA File Offset: 0x000C1BCA
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x000C39D2 File Offset: 0x000C1BD2
		internal string OriginatingStackTrace { get; set; }

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000C39DB File Offset: 0x000C1BDB
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000C39E3 File Offset: 0x000C1BE3
		public string ErrorMessage { get; set; }

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000C39EC File Offset: 0x000C1BEC
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000C39F4 File Offset: 0x000C1BF4
		internal string DatabaseName { get; set; }
	}
}
