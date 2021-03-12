using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C84 RID: 3204
	[Cmdlet("Get", "MergeRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMergeRequest : GetRequest<MergeRequestIdParameter, MergeRequest>
	{
		// Token: 0x06007B1B RID: 31515 RVA: 0x001F92D4 File Offset: 0x001F74D4
		public GetMergeRequest()
		{
			this.sourceUserId = null;
			this.targetUserId = null;
		}

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x06007B1C RID: 31516 RVA: 0x001F92EA File Offset: 0x001F74EA
		// (set) Token: 0x06007B1D RID: 31517 RVA: 0x001F9301 File Offset: 0x001F7501
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
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

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x06007B1E RID: 31518 RVA: 0x001F9314 File Offset: 0x001F7514
		// (set) Token: 0x06007B1F RID: 31519 RVA: 0x001F932B File Offset: 0x001F752B
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		[ValidateNotNull]
		public MailboxOrMailUserIdParameter SourceMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["SourceMailbox"];
			}
			set
			{
				base.Fields["SourceMailbox"] = value;
			}
		}

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x06007B20 RID: 31520 RVA: 0x001F933E File Offset: 0x001F753E
		// (set) Token: 0x06007B21 RID: 31521 RVA: 0x001F9346 File Offset: 0x001F7546
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		[ValidateNotNull]
		public MailboxOrMailUserIdParameter Mailbox
		{
			get
			{
				return base.InternalMailbox;
			}
			set
			{
				base.InternalMailbox = value;
			}
		}

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x06007B22 RID: 31522 RVA: 0x001F934F File Offset: 0x001F754F
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.Merge;
			}
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x001F9352 File Offset: 0x001F7552
		protected override void InternalStateReset()
		{
			this.sourceUserId = null;
			this.targetUserId = null;
			base.InternalStateReset();
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x001F9368 File Offset: 0x001F7568
		protected override RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = base.InternalFilterBuilder();
			if (requestIndexEntryQueryFilter == null)
			{
				requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			}
			requestIndexEntryQueryFilter.RequestType = this.RequestType;
			requestIndexEntryQueryFilter.IndexId = new RequestIndexId(RequestIndexLocation.AD);
			if (this.sourceUserId != null)
			{
				requestIndexEntryQueryFilter.SourceMailbox = this.sourceUserId;
			}
			if (this.targetUserId != null)
			{
				requestIndexEntryQueryFilter.TargetMailbox = this.targetUserId;
			}
			return requestIndexEntryQueryFilter;
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x001F93C8 File Offset: 0x001F75C8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.SourceMailbox != null)
			{
				ADUser aduser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.SourceMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
				this.sourceUserId = aduser.Id;
			}
			if (this.TargetMailbox != null)
			{
				ADUser aduser2 = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.TargetMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
				this.targetUserId = aduser2.Id;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003D2C RID: 15660
		public const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003D2D RID: 15661
		public const string ParameterSourceMailbox = "SourceMailbox";

		// Token: 0x04003D2E RID: 15662
		private ADObjectId sourceUserId;

		// Token: 0x04003D2F RID: 15663
		private ADObjectId targetUserId;
	}
}
