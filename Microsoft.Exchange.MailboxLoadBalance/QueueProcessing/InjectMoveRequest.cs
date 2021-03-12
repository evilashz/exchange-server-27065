using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing
{
	// Token: 0x020000DD RID: 221
	internal class InjectMoveRequest : CmdletExecutionRequest<MoveRequestStatistics>
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x000135A0 File Offset: 0x000117A0
		public InjectMoveRequest(string batchName, DirectoryMailbox mailbox, ILogger logger, bool protect, RequestPriority requestPriority, ADObjectId mailboxObjectId, DirectoryIdentity targetDatabaseIdentity, ILoadBalanceSettings settings, CmdletExecutionPool cmdletPool) : base("New-MoveRequest", cmdletPool, logger)
		{
			this.settings = settings;
			this.TargetDatabase = targetDatabaseIdentity;
			this.BatchName = batchName;
			this.Mailbox = mailbox;
			this.Protect = protect;
			AnchorUtil.ThrowOnNullArgument(mailbox, "mailbox");
			base.Command.AddParameter("Identity", mailboxObjectId);
			base.Command.AddParameter("WorkloadType", RequestWorkloadType.LoadBalancing);
			if (!string.IsNullOrWhiteSpace(batchName))
			{
				base.Command.AddParameter("BatchName", batchName);
			}
			if (targetDatabaseIdentity != null)
			{
				DatabaseIdParameter value = new DatabaseIdParameter(targetDatabaseIdentity.ADObjectId);
				if (mailbox.IsArchiveOnly)
				{
					base.Command.AddParameter("ArchiveTargetDatabase", value);
					base.Command.AddParameter("ArchiveOnly");
				}
				else
				{
					base.Command.AddParameter("TargetDatabase", value);
					base.Command.AddParameter("PrimaryOnly");
				}
			}
			base.Command.AddParameter("CompletedRequestAgeLimit", 0);
			base.Command.AddParameter("Priority", requestPriority);
			if (protect)
			{
				base.Command.AddParameter("Protect");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000136D8 File Offset: 0x000118D8
		public override IEnumerable<ResourceKey> Resources
		{
			get
			{
				return new ADResourceKey[]
				{
					ADResourceKey.Key
				};
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x000136F5 File Offset: 0x000118F5
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x000136FD File Offset: 0x000118FD
		private string BatchName { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00013706 File Offset: 0x00011906
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0001370E File Offset: 0x0001190E
		private DirectoryMailbox Mailbox { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00013717 File Offset: 0x00011917
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0001371F File Offset: 0x0001191F
		private bool Protect { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00013728 File Offset: 0x00011928
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00013730 File Offset: 0x00011930
		private DirectoryIdentity TargetDatabase { get; set; }

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001373C File Offset: 0x0001193C
		public override RequestDiagnosticData GetDiagnosticData(bool verbose)
		{
			InjectMoveRequestDiagnosticData injectMoveRequestDiagnosticData = (InjectMoveRequestDiagnosticData)base.GetDiagnosticData(verbose);
			injectMoveRequestDiagnosticData.ArchiveOnly = this.Mailbox.IsArchiveOnly;
			injectMoveRequestDiagnosticData.BatchName = this.BatchName;
			injectMoveRequestDiagnosticData.Mailbox = this.Mailbox;
			injectMoveRequestDiagnosticData.Protect = this.Protect;
			injectMoveRequestDiagnosticData.TargetDatabase = this.TargetDatabase;
			injectMoveRequestDiagnosticData.TargetDatabaseName = this.TargetDatabase.Name;
			injectMoveRequestDiagnosticData.MovedMailboxGuid = this.Mailbox.Guid;
			injectMoveRequestDiagnosticData.SourceDatabaseName = this.Mailbox.GetDatabaseForMailbox().Name;
			return injectMoveRequestDiagnosticData;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000137D0 File Offset: 0x000119D0
		protected override RequestDiagnosticData CreateDiagnosticData()
		{
			return new InjectMoveRequestDiagnosticData();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000137D7 File Offset: 0x000119D7
		protected override void ProcessRequest()
		{
			if (this.settings.DontCreateMoveRequests)
			{
				base.Command.AddParameter("WhatIf");
			}
			base.ProcessRequest();
		}

		// Token: 0x0400029C RID: 668
		private readonly ILoadBalanceSettings settings;
	}
}
