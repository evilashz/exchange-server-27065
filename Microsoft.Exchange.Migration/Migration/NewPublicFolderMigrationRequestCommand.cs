using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NewPublicFolderMigrationRequestCommand : NewPublicFolderMigrationRequestCommandBase
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0004A169 File Offset: 0x00048369
		public NewPublicFolderMigrationRequestCommand(bool whatIf) : base("New-PublicFolderMailboxMigrationRequest", NewPublicFolderMigrationRequestCommand.ExceptionsToIgnore)
		{
			base.WhatIf = whatIf;
		}

		// Token: 0x17000540 RID: 1344
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x0004A182 File Offset: 0x00048382
		public Stream CSVStream
		{
			set
			{
				base.AddParameter("CSVStream", value);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x0004A190 File Offset: 0x00048390
		public MailboxIdParameter TargetMailbox
		{
			set
			{
				base.AddParameter("TargetMailbox", value);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x0004A19E File Offset: 0x0004839E
		public OrganizationIdParameter Organization
		{
			set
			{
				base.AddParameter("Organization", value);
			}
		}

		// Token: 0x04000612 RID: 1554
		public const string CmdletName = "New-PublicFolderMailboxMigrationRequest";

		// Token: 0x04000613 RID: 1555
		private const string CSVStreamParameter = "CSVStream";

		// Token: 0x04000614 RID: 1556
		private const string TargetMailboxParameter = "TargetMailbox";

		// Token: 0x04000615 RID: 1557
		private const string OrganizationParameter = "Organization";

		// Token: 0x04000616 RID: 1558
		private static readonly Type[] ExceptionsToIgnore = new Type[]
		{
			typeof(ManagementObjectAlreadyExistsException)
		};
	}
}
