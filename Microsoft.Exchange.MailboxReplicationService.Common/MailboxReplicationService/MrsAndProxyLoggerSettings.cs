using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001BF RID: 447
	internal class MrsAndProxyLoggerSettings : ActivityContextLogFileSettings
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000275FF File Offset: 0x000257FF
		protected override string LogTypeName
		{
			get
			{
				return "Mailbox Replication Log";
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00027606 File Offset: 0x00025806
		protected override string LogSubFolderName
		{
			get
			{
				return "MailboxReplicationService";
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0002760D File Offset: 0x0002580D
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MailboxReplicationServiceTracer;
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00027614 File Offset: 0x00025814
		internal static MrsAndProxyLoggerSettings Load()
		{
			return new MrsAndProxyLoggerSettings();
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002761B File Offset: 0x0002581B
		private MrsAndProxyLoggerSettings()
		{
		}

		// Token: 0x04000982 RID: 2434
		internal const string LoggerSubFolderName = "MailboxReplicationService";
	}
}
