using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C5F RID: 3167
	[Cmdlet("Get", "FolderMoveRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetFolderMoveRequest : GetRequest<FolderMoveRequestIdParameter, FolderMoveRequest>
	{
		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x06007851 RID: 30801 RVA: 0x001EA0D9 File Offset: 0x001E82D9
		// (set) Token: 0x06007852 RID: 30802 RVA: 0x001EA0F0 File Offset: 0x001E82F0
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public MailboxIdParameter SourceMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["SourceMailbox"];
			}
			set
			{
				base.Fields["SourceMailbox"] = value;
			}
		}

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x06007853 RID: 30803 RVA: 0x001EA103 File Offset: 0x001E8303
		// (set) Token: 0x06007854 RID: 30804 RVA: 0x001EA11A File Offset: 0x001E831A
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public MailboxIdParameter TargetMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x06007855 RID: 30805 RVA: 0x001EA12D File Offset: 0x001E832D
		protected override MRSRequestType RequestType
		{
			get
			{
				return MRSRequestType.FolderMove;
			}
		}

		// Token: 0x06007856 RID: 30806 RVA: 0x001EA131 File Offset: 0x001E8331
		protected override void InternalStateReset()
		{
			this.sourceMailboxUser = null;
			this.targetMailboxUser = null;
			base.InternalStateReset();
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x001EA148 File Offset: 0x001E8348
		protected override RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = base.InternalFilterBuilder();
			if (requestIndexEntryQueryFilter == null)
			{
				requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			}
			requestIndexEntryQueryFilter.RequestType = this.RequestType;
			requestIndexEntryQueryFilter.IndexId = new RequestIndexId(RequestIndexLocation.AD);
			if (this.sourceMailboxUser != null)
			{
				requestIndexEntryQueryFilter.SourceMailbox = this.sourceMailboxUser.Id;
			}
			if (this.targetMailboxUser != null)
			{
				requestIndexEntryQueryFilter.TargetMailbox = this.targetMailboxUser.Id;
			}
			return requestIndexEntryQueryFilter;
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x001EA1B0 File Offset: 0x001E83B0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.SourceMailbox != null)
				{
					this.sourceMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.SourceMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.SourceMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.SourceMailbox.ToString())), ExchangeErrorCategory.Client);
				}
				if (this.TargetMailbox != null)
				{
					this.targetMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), ExchangeErrorCategory.Client);
				}
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04003C0F RID: 15375
		public const string ParameterSourceMailbox = "SourceMailbox";

		// Token: 0x04003C10 RID: 15376
		public const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003C11 RID: 15377
		private ADUser sourceMailboxUser;

		// Token: 0x04003C12 RID: 15378
		private ADUser targetMailboxUser;
	}
}
