using System;
using System.Linq;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000007 RID: 7
	public class Search
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002D00 File Offset: 0x00000F00
		public string UserPrincipalName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D09 File Offset: 0x00000F09
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002D11 File Offset: 0x00000F11
		public AuditSearchKind Kind { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002D1A File Offset: 0x00000F1A
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002D22 File Offset: 0x00000F22
		public Guid Identity { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D2B File Offset: 0x00000F2B
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002D33 File Offset: 0x00000F33
		public bool Result { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D3C File Offset: 0x00000F3C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D44 File Offset: 0x00000F44
		public int RetryAttempt { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D4D File Offset: 0x00000F4D
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D55 File Offset: 0x00000F55
		public string DiagnosticContext { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D60 File Offset: 0x00000F60
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002D94 File Offset: 0x00000F94
		public string TenantAcceptedDomain
		{
			get
			{
				if (this.UserPrincipalName == null)
				{
					return null;
				}
				return this.UserPrincipalName.Split(new char[]
				{
					'@'
				}).LastOrDefault<string>();
			}
			set
			{
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D96 File Offset: 0x00000F96
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002D9E File Offset: 0x00000F9E
		public string LastProcessedMailbox { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DA7 File Offset: 0x00000FA7
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002DAF File Offset: 0x00000FAF
		public ExceptionDetails ExceptionDetails { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public int MailboxCount { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DC9 File Offset: 0x00000FC9
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public int ResultCount { get; set; }
	}
}
