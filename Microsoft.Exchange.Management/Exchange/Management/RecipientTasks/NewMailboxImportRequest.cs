using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C94 RID: 3220
	[Cmdlet("New", "MailboxImportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MailboxImportRequest")]
	public sealed class NewMailboxImportRequest : NewRequest<MailboxImportRequest>
	{
		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x06007BB6 RID: 31670 RVA: 0x001FAF1A File Offset: 0x001F911A
		// (set) Token: 0x06007BB7 RID: 31671 RVA: 0x001FAF31 File Offset: 0x001F9131
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxImportRequest", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public MailboxOrMailUserIdParameter Mailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x06007BB8 RID: 31672 RVA: 0x001FAF44 File Offset: 0x001F9144
		// (set) Token: 0x06007BB9 RID: 31673 RVA: 0x001FAF5B File Offset: 0x001F915B
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxImportRequest")]
		public LongPath FilePath
		{
			get
			{
				return (LongPath)base.Fields["FilePath"];
			}
			set
			{
				base.Fields["FilePath"] = value;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x06007BBA RID: 31674 RVA: 0x001FAF6E File Offset: 0x001F916E
		// (set) Token: 0x06007BBB RID: 31675 RVA: 0x001FAF85 File Offset: 0x001F9185
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public int? ContentCodePage
		{
			get
			{
				return (int?)base.Fields["ContentCodePage"];
			}
			set
			{
				base.Fields["ContentCodePage"] = value;
			}
		}

		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x06007BBC RID: 31676 RVA: 0x001FAF9D File Offset: 0x001F919D
		// (set) Token: 0x06007BBD RID: 31677 RVA: 0x001FAFB4 File Offset: 0x001F91B4
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public string SourceRootFolder
		{
			get
			{
				return (string)base.Fields["SourceRootFolder"];
			}
			set
			{
				base.Fields["SourceRootFolder"] = value;
			}
		}

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x06007BBE RID: 31678 RVA: 0x001FAFC7 File Offset: 0x001F91C7
		// (set) Token: 0x06007BBF RID: 31679 RVA: 0x001FAFDE File Offset: 0x001F91DE
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public string TargetRootFolder
		{
			get
			{
				return (string)base.Fields["TargetRootFolder"];
			}
			set
			{
				base.Fields["TargetRootFolder"] = value;
			}
		}

		// Token: 0x1700265C RID: 9820
		// (get) Token: 0x06007BC0 RID: 31680 RVA: 0x001FAFF1 File Offset: 0x001F91F1
		// (set) Token: 0x06007BC1 RID: 31681 RVA: 0x001FB017 File Offset: 0x001F9217
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public SwitchParameter IsArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsArchive"] ?? false);
			}
			set
			{
				base.Fields["IsArchive"] = value;
			}
		}

		// Token: 0x1700265D RID: 9821
		// (get) Token: 0x06007BC2 RID: 31682 RVA: 0x001FB02F File Offset: 0x001F922F
		// (set) Token: 0x06007BC3 RID: 31683 RVA: 0x001FB037 File Offset: 0x001F9237
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x1700265E RID: 9822
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x001FB040 File Offset: 0x001F9240
		// (set) Token: 0x06007BC5 RID: 31685 RVA: 0x001FB048 File Offset: 0x001F9248
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new Fqdn RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x1700265F RID: 9823
		// (get) Token: 0x06007BC6 RID: 31686 RVA: 0x001FB051 File Offset: 0x001F9251
		// (set) Token: 0x06007BC7 RID: 31687 RVA: 0x001FB059 File Offset: 0x001F9259
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x17002660 RID: 9824
		// (get) Token: 0x06007BC8 RID: 31688 RVA: 0x001FB062 File Offset: 0x001F9262
		// (set) Token: 0x06007BC9 RID: 31689 RVA: 0x001FB06A File Offset: 0x001F926A
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x17002661 RID: 9825
		// (get) Token: 0x06007BCA RID: 31690 RVA: 0x001FB073 File Offset: 0x001F9273
		// (set) Token: 0x06007BCB RID: 31691 RVA: 0x001FB07B File Offset: 0x001F927B
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new SwitchParameter ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x17002662 RID: 9826
		// (get) Token: 0x06007BCC RID: 31692 RVA: 0x001FB084 File Offset: 0x001F9284
		// (set) Token: 0x06007BCD RID: 31693 RVA: 0x001FB08C File Offset: 0x001F928C
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new ConflictResolutionOption ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x17002663 RID: 9827
		// (get) Token: 0x06007BCE RID: 31694 RVA: 0x001FB095 File Offset: 0x001F9295
		// (set) Token: 0x06007BCF RID: 31695 RVA: 0x001FB09D File Offset: 0x001F929D
		[Parameter(Mandatory = false, ParameterSetName = "MailboxImportRequest")]
		public new FAICopyOption AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x17002664 RID: 9828
		// (get) Token: 0x06007BD0 RID: 31696 RVA: 0x001FB0A6 File Offset: 0x001F92A6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxImportRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x17002665 RID: 9829
		// (get) Token: 0x06007BD1 RID: 31697 RVA: 0x001FB0C8 File Offset: 0x001F92C8
		protected override KeyValuePair<string, LocalizedString>[] ExtendedAttributes
		{
			get
			{
				return new KeyValuePair<string, LocalizedString>[]
				{
					new KeyValuePair<string, LocalizedString>("FilePath", new LocalizedString(this.FilePath.ToString())),
					new KeyValuePair<string, LocalizedString>("Mailbox", new LocalizedString(this.user.DisplayName))
				};
			}
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x001FB129 File Offset: 0x001F9329
		protected override void InternalStateReset()
		{
			this.user = null;
			base.InternalStateReset();
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x001FB138 File Offset: 0x001F9338
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result;
			using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
			{
				result = base.CreateSession();
			}
			return result;
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x001FB170 File Offset: 0x001F9370
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.ValidateRootFolders(this.SourceRootFolder, this.TargetRootFolder);
				if (!this.FilePath.IsUnc)
				{
					base.WriteError(new NonUNCFilePathPermanentException(this.FilePath.PathName), ErrorCategory.InvalidArgument, this.FilePath);
				}
				bool flag = !OrganizationId.ForestWideOrgId.Equals(base.ExecutingUserOrganizationId);
				bool flag2 = this.RemoteHostName != null;
				if (flag && !flag2)
				{
					base.WriteError(new RemoteMailboxImportNeedRemoteProxyException(), ErrorCategory.InvalidArgument, this);
				}
				this.user = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.Mailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
				bool wildcardedSearch = false;
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					wildcardedSearch = true;
					base.RequestName = "MailboxImport";
				}
				if (base.ParameterSetName.Equals("MailboxImportRequest"))
				{
					this.DisallowImportForPublicFolderMailbox();
					if (this.user.RecipientType != RecipientType.UserMailbox)
					{
						base.WriteError(new InvalidRecipientTypePermanentException(this.user.ToString(), this.user.RecipientType.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
					}
					if (this.IsArchive && (this.user.ArchiveGuid == Guid.Empty || this.user.ArchiveDatabase == null))
					{
						base.WriteError(new MailboxLacksArchivePermanentException(this.user.ToString()), ErrorCategory.InvalidArgument, this.IsArchive);
					}
					if (!this.IsArchive && this.user.Database == null)
					{
						base.WriteError(new MailboxLacksDatabasePermanentException(this.user.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
					}
					base.RescopeToOrgId(this.user.OrganizationId);
					ADObjectId mdbId = null;
					ADObjectId mdbServerSite = null;
					this.LocateAndChooseMdb(null, this.IsArchive ? this.user.ArchiveDatabase : this.user.Database, null, this.Mailbox, this.Mailbox, out mdbId, out mdbServerSite);
					base.MdbId = mdbId;
					base.MdbServerSite = mdbServerSite;
					base.Flags = (RequestFlags.IntraOrg | RequestFlags.Pull);
				}
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, this.user.Id, true, MRSRequestType.MailboxImport, this.Mailbox, wildcardedSearch);
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x001FB41C File Offset: 0x001F961C
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.MailboxImport;
			if (dataObject.WorkloadType == RequestWorkloadType.None)
			{
				if (string.IsNullOrEmpty(this.RemoteHostName))
				{
					dataObject.WorkloadType = RequestWorkloadType.Local;
				}
				else
				{
					dataObject.WorkloadType = RequestWorkloadType.RemotePstIngestion;
				}
			}
			dataObject.ContentCodePage = this.ContentCodePage;
			dataObject.FilePath = this.FilePath.PathName;
			if (this.user != null)
			{
				dataObject.TargetUserId = this.user.Id;
				dataObject.TargetUser = this.user;
			}
			if (base.ParameterSetName.Equals("MailboxImportRequest"))
			{
				if (!string.IsNullOrEmpty(this.SourceRootFolder))
				{
					dataObject.SourceRootFolder = this.SourceRootFolder;
				}
				if (!string.IsNullOrEmpty(this.TargetRootFolder))
				{
					dataObject.TargetRootFolder = this.TargetRootFolder;
				}
				if (this.IsArchive)
				{
					dataObject.TargetIsArchive = true;
					dataObject.TargetExchangeGuid = this.user.ArchiveGuid;
					dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.user.ArchiveDatabase);
				}
				else
				{
					dataObject.TargetIsArchive = false;
					dataObject.TargetExchangeGuid = this.user.ExchangeGuid;
					dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.user.Database);
				}
				dataObject.TargetAlias = this.user.Alias;
				if (this.RemoteCredential != null)
				{
					dataObject.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, null);
				}
				if (!string.IsNullOrEmpty(this.RemoteHostName))
				{
					dataObject.RemoteHostName = this.RemoteHostName;
				}
			}
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x001FB5AC File Offset: 0x001F97AC
		protected override MailboxImportRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new MailboxImportRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
			return null;
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x001FB5FC File Offset: 0x001F97FC
		private void DisallowImportForPublicFolderMailbox()
		{
			if (this.user != null && this.user.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotImportPstToPublicFolderMailbox(this.user.Name)), ErrorCategory.InvalidArgument, this.Mailbox);
			}
		}

		// Token: 0x04003D4D RID: 15693
		public const string DefaultMailboxImportName = "MailboxImport";

		// Token: 0x04003D4E RID: 15694
		public const string TaskNoun = "MailboxImportRequest";

		// Token: 0x04003D4F RID: 15695
		public const string ParameterSetMailboxImport = "MailboxImportRequest";

		// Token: 0x04003D50 RID: 15696
		private ADUser user;
	}
}
