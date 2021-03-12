using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F7 RID: 503
	[Cmdlet("Install", "MailboxRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallMailboxRole : ManageMailboxRole
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0004C264 File Offset: 0x0004A464
		public InstallMailboxRole()
		{
			base.Fields["MailboxDatabaseName"] = "Mailbox Database";
			base.Fields["PublicFolderDatabaseName"] = "Public Folder Database";
			base.Fields["CustomerFeedbackEnabled"] = null;
			base.Fields["MdbName"] = null;
			base.Fields["DbFilePath"] = null;
			base.Fields["LogFolderPath"] = null;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004C2E5 File Offset: 0x0004A4E5
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallMailboxRoleDescription;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004C2EC File Offset: 0x0004A4EC
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0004C303 File Offset: 0x0004A503
		[Parameter]
		public string MdbName
		{
			get
			{
				return (string)base.Fields["MdbName"];
			}
			set
			{
				base.Fields["MdbName"] = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004C316 File Offset: 0x0004A516
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0004C32D File Offset: 0x0004A52D
		[Parameter]
		public string DbFilePath
		{
			get
			{
				return (string)base.Fields["DbFilePath"];
			}
			set
			{
				base.Fields["DbFilePath"] = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004C340 File Offset: 0x0004A540
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0004C357 File Offset: 0x0004A557
		[Parameter]
		public string LogFolderPath
		{
			get
			{
				return (string)base.Fields["LogFolderPath"];
			}
			set
			{
				base.Fields["LogFolderPath"] = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004C36A File Offset: 0x0004A56A
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0004C381 File Offset: 0x0004A581
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}
	}
}
