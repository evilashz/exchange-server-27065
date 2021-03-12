using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000985 RID: 2437
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LoggingContext
	{
		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x001750C4 File Offset: 0x001732C4
		// (set) Token: 0x060059F9 RID: 23033 RVA: 0x001750CC File Offset: 0x001732CC
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x001750D5 File Offset: 0x001732D5
		// (set) Token: 0x060059FB RID: 23035 RVA: 0x001750DD File Offset: 0x001732DD
		public Guid TransactionId { get; private set; }

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060059FC RID: 23036 RVA: 0x001750E6 File Offset: 0x001732E6
		// (set) Token: 0x060059FD RID: 23037 RVA: 0x001750EE File Offset: 0x001732EE
		public string Context { get; private set; }

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x001750F7 File Offset: 0x001732F7
		// (set) Token: 0x060059FF RID: 23039 RVA: 0x001750FF File Offset: 0x001732FF
		public string User { get; private set; }

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06005A00 RID: 23040 RVA: 0x00175108 File Offset: 0x00173308
		// (set) Token: 0x06005A01 RID: 23041 RVA: 0x00175110 File Offset: 0x00173310
		public Stream LoggingStream { get; set; }

		// Token: 0x06005A02 RID: 23042 RVA: 0x00175119 File Offset: 0x00173319
		public LoggingContext(Guid mailboxGuid, string context, string user, Stream loggingStream)
		{
			this.TransactionId = Guid.NewGuid();
			this.MailboxGuid = mailboxGuid;
			this.Context = context;
			this.User = user;
			this.LoggingStream = loggingStream;
		}
	}
}
