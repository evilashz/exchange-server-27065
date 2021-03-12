using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreationDiagnostics
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000335E File Offset: 0x0000155E
		public CreationDiagnostics()
		{
			this.stopWatch = new Stopwatch();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003371 File Offset: 0x00001571
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003379 File Offset: 0x00001579
		public TimeSpan? MailboxCreationTime { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003382 File Offset: 0x00001582
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000338A File Offset: 0x0000158A
		public TimeSpan? AADIdentityCreationTime { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003393 File Offset: 0x00001593
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000339B File Offset: 0x0000159B
		public TimeSpan? AADCompleteCallbackTime { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000033A4 File Offset: 0x000015A4
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000033AC File Offset: 0x000015AC
		public TimeSpan? SharePointNotificationTime { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000033B5 File Offset: 0x000015B5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000033BD File Offset: 0x000015BD
		public TimeSpan? GroupCreationTime { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000033C6 File Offset: 0x000015C6
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000033CE File Offset: 0x000015CE
		public bool MailboxCreatedSuccessfully { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000033D7 File Offset: 0x000015D7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000033DF File Offset: 0x000015DF
		public Guid CmdletLogCorrelationId { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x000033E8 File Offset: 0x000015E8
		public void Start()
		{
			this.stopWatch.Start();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033F5 File Offset: 0x000015F5
		public void Stop()
		{
			this.GroupCreationTime = new TimeSpan?(this.stopWatch.Elapsed);
			this.stopWatch.Stop();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003418 File Offset: 0x00001618
		public void RecordAADTime()
		{
			this.AADIdentityCreationTime = new TimeSpan?(this.stopWatch.Elapsed);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003430 File Offset: 0x00001630
		public void RecordAADCompleteCallbackTime()
		{
			this.AADCompleteCallbackTime = new TimeSpan?(this.stopWatch.Elapsed.Subtract(this.AADIdentityCreationTime.Value));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000346C File Offset: 0x0000166C
		public void RecordSharePointNotificationTime()
		{
			this.SharePointNotificationTime = new TimeSpan?(this.stopWatch.Elapsed.Subtract(this.AADCompleteCallbackTime.Value));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034A8 File Offset: 0x000016A8
		public void RecordMailboxTime()
		{
			this.MailboxCreationTime = new TimeSpan?(this.stopWatch.Elapsed.Subtract(this.SharePointNotificationTime.Value));
		}

		// Token: 0x0400003D RID: 61
		private readonly Stopwatch stopWatch;
	}
}
