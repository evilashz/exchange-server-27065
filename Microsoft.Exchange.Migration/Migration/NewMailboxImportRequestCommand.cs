using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewMailboxImportRequestCommand : NewMailboxImportRequestCommandBase
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x00045874 File Offset: 0x00043A74
		public NewMailboxImportRequestCommand(bool whatIf) : base("New-MailboxImportRequest", NewMailboxImportRequestCommand.ExceptionsToIgnore)
		{
			base.WhatIf = whatIf;
		}

		// Token: 0x170004F9 RID: 1273
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x0004588D File Offset: 0x00043A8D
		public string Name
		{
			set
			{
				base.AddParameter("Name", value);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x0004589B File Offset: 0x00043A9B
		public object Mailbox
		{
			set
			{
				base.AddParameter("Mailbox", value);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x000458A9 File Offset: 0x00043AA9
		public string PstFilePath
		{
			set
			{
				base.AddParameter("FilePath", value);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x000458B7 File Offset: 0x00043AB7
		public bool IsArchive
		{
			set
			{
				base.AddParameter("IsArchive", value);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x000458CA File Offset: 0x00043ACA
		public string SourceRootFolder
		{
			set
			{
				base.AddParameter("SourceRootFolder", value);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x000458D8 File Offset: 0x00043AD8
		public string TargetRootFolder
		{
			set
			{
				base.AddParameter("TargetRootFolder", value);
			}
		}

		// Token: 0x040005D6 RID: 1494
		public const string CmdletName = "New-MailboxImportRequest";

		// Token: 0x040005D7 RID: 1495
		private static readonly Type[] ExceptionsToIgnore = new Type[0];
	}
}
