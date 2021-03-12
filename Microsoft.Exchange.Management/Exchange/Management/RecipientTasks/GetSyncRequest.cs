using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CBC RID: 3260
	[Cmdlet("Get", "SyncRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetSyncRequest : GetRequest<SyncRequestIdParameter, SyncRequest>
	{
		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x06007CFE RID: 31998 RVA: 0x001FF538 File Offset: 0x001FD738
		// (set) Token: 0x06007CFF RID: 31999 RVA: 0x001FF540 File Offset: 0x001FD740
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering", ValueFromPipeline = true)]
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

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x06007D00 RID: 32000 RVA: 0x001FF549 File Offset: 0x001FD749
		protected override RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(this.targetUser.Id);
			}
		}

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x06007D01 RID: 32001 RVA: 0x001FF55B File Offset: 0x001FD75B
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.Sync;
			}
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x001FF560 File Offset: 0x001FD760
		protected override RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = base.InternalFilterBuilder();
			if (requestIndexEntryQueryFilter == null)
			{
				requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			}
			requestIndexEntryQueryFilter.RequestType = this.RequestType;
			requestIndexEntryQueryFilter.IndexId = this.DefaultRequestIndexId;
			requestIndexEntryQueryFilter.TargetMailbox = this.targetUser.Id;
			return requestIndexEntryQueryFilter;
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x001FF5A8 File Offset: 0x001FD7A8
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result;
			using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
			{
				if (this.Mailbox == null)
				{
					ADObjectId adObjectId;
					if (!base.TryGetExecutingUserId(out adObjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					this.Mailbox = new MailboxOrMailUserIdParameter(adObjectId);
				}
				result = base.CreateSession();
			}
			return result;
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x001FF608 File Offset: 0x001FD808
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.targetUser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.Mailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
			TaskLogger.LogExit();
		}

		// Token: 0x04003DAC RID: 15788
		private ADUser targetUser;
	}
}
