using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9F RID: 3231
	[Cmdlet("Get", "MailboxRestoreRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxRestoreRequest : GetRequest<MailboxRestoreRequestIdParameter, MailboxRestoreRequest>
	{
		// Token: 0x06007C0D RID: 31757 RVA: 0x001FC39B File Offset: 0x001FA59B
		public GetMailboxRestoreRequest()
		{
			this.targetUserId = null;
			this.sourceDatabaseId = null;
		}

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x06007C0E RID: 31758 RVA: 0x001FC3B1 File Offset: 0x001FA5B1
		// (set) Token: 0x06007C0F RID: 31759 RVA: 0x001FC3C8 File Offset: 0x001FA5C8
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public DatabaseIdParameter SourceDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourceDatabase"];
			}
			set
			{
				base.Fields["SourceDatabase"] = value;
			}
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x06007C10 RID: 31760 RVA: 0x001FC3DB File Offset: 0x001FA5DB
		// (set) Token: 0x06007C11 RID: 31761 RVA: 0x001FC3F2 File Offset: 0x001FA5F2
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		[ValidateNotNull]
		public MailboxOrMailUserIdParameter TargetMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x06007C12 RID: 31762 RVA: 0x001FC405 File Offset: 0x001FA605
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.MailboxRestore;
			}
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001FC408 File Offset: 0x001FA608
		protected override void InternalStateReset()
		{
			this.targetUserId = null;
			this.sourceDatabaseId = null;
			base.InternalStateReset();
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x001FC420 File Offset: 0x001FA620
		protected override RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = base.InternalFilterBuilder();
			if (requestIndexEntryQueryFilter == null)
			{
				requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			}
			requestIndexEntryQueryFilter.RequestType = this.RequestType;
			requestIndexEntryQueryFilter.IndexId = new RequestIndexId(RequestIndexLocation.AD);
			if (this.targetUserId != null)
			{
				requestIndexEntryQueryFilter.TargetMailbox = this.targetUserId;
			}
			if (this.sourceDatabaseId != null)
			{
				requestIndexEntryQueryFilter.SourceDatabase = this.sourceDatabaseId;
			}
			return requestIndexEntryQueryFilter;
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x001FC480 File Offset: 0x001FA680
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.TargetMailbox != null)
			{
				ADUser aduser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.TargetMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
				this.targetUserId = aduser.Id;
			}
			if (this.SourceDatabase != null)
			{
				this.SourceDatabase.AllowLegacy = true;
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.SourceDatabase, base.ConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.SourceDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.SourceDatabase.ToString())));
				this.sourceDatabaseId = mailboxDatabase.Id;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003D5E RID: 15710
		public const string ParameterSourceDatabase = "SourceDatabase";

		// Token: 0x04003D5F RID: 15711
		public const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003D60 RID: 15712
		private ADObjectId sourceDatabaseId;

		// Token: 0x04003D61 RID: 15713
		private ADObjectId targetUserId;
	}
}
