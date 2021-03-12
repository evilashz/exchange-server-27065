using System;
using System.Collections.Generic;
using System.Globalization;
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
	// Token: 0x02000C8D RID: 3213
	[Cmdlet("New", "MailboxExportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MailboxExportRequest")]
	public sealed class NewMailboxExportRequest : NewRequest<MailboxExportRequest>
	{
		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x06007B80 RID: 31616 RVA: 0x001FA6B2 File Offset: 0x001F88B2
		// (set) Token: 0x06007B81 RID: 31617 RVA: 0x001FA6C9 File Offset: 0x001F88C9
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxExportRequest", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x06007B82 RID: 31618 RVA: 0x001FA6DC File Offset: 0x001F88DC
		// (set) Token: 0x06007B83 RID: 31619 RVA: 0x001FA6F3 File Offset: 0x001F88F3
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x06007B84 RID: 31620 RVA: 0x001FA706 File Offset: 0x001F8906
		// (set) Token: 0x06007B85 RID: 31621 RVA: 0x001FA71D File Offset: 0x001F891D
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x06007B86 RID: 31622 RVA: 0x001FA730 File Offset: 0x001F8930
		// (set) Token: 0x06007B87 RID: 31623 RVA: 0x001FA747 File Offset: 0x001F8947
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x06007B88 RID: 31624 RVA: 0x001FA75A File Offset: 0x001F895A
		// (set) Token: 0x06007B89 RID: 31625 RVA: 0x001FA780 File Offset: 0x001F8980
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x06007B8A RID: 31626 RVA: 0x001FA798 File Offset: 0x001F8998
		// (set) Token: 0x06007B8B RID: 31627 RVA: 0x001FA7A0 File Offset: 0x001F89A0
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x06007B8C RID: 31628 RVA: 0x001FA7A9 File Offset: 0x001F89A9
		// (set) Token: 0x06007B8D RID: 31629 RVA: 0x001FA7B1 File Offset: 0x001F89B1
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x06007B8E RID: 31630 RVA: 0x001FA7BA File Offset: 0x001F89BA
		// (set) Token: 0x06007B8F RID: 31631 RVA: 0x001FA7C2 File Offset: 0x001F89C2
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
		public new string ContentFilter
		{
			get
			{
				return base.ContentFilter;
			}
			set
			{
				base.ContentFilter = value;
			}
		}

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x06007B90 RID: 31632 RVA: 0x001FA7CB File Offset: 0x001F89CB
		// (set) Token: 0x06007B91 RID: 31633 RVA: 0x001FA7D3 File Offset: 0x001F89D3
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
		public new CultureInfo ContentFilterLanguage
		{
			get
			{
				return base.ContentFilterLanguage;
			}
			set
			{
				base.ContentFilterLanguage = value;
			}
		}

		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x06007B92 RID: 31634 RVA: 0x001FA7DC File Offset: 0x001F89DC
		// (set) Token: 0x06007B93 RID: 31635 RVA: 0x001FA7E4 File Offset: 0x001F89E4
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x06007B94 RID: 31636 RVA: 0x001FA7ED File Offset: 0x001F89ED
		// (set) Token: 0x06007B95 RID: 31637 RVA: 0x001FA7F5 File Offset: 0x001F89F5
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x06007B96 RID: 31638 RVA: 0x001FA7FE File Offset: 0x001F89FE
		// (set) Token: 0x06007B97 RID: 31639 RVA: 0x001FA806 File Offset: 0x001F8A06
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x06007B98 RID: 31640 RVA: 0x001FA80F File Offset: 0x001F8A0F
		// (set) Token: 0x06007B99 RID: 31641 RVA: 0x001FA817 File Offset: 0x001F8A17
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x06007B9A RID: 31642 RVA: 0x001FA820 File Offset: 0x001F8A20
		// (set) Token: 0x06007B9B RID: 31643 RVA: 0x001FA828 File Offset: 0x001F8A28
		[Parameter(Mandatory = false, ParameterSetName = "MailboxExportRequest")]
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

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x06007B9C RID: 31644 RVA: 0x001FA831 File Offset: 0x001F8A31
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxExportRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x06007B9D RID: 31645 RVA: 0x001FA854 File Offset: 0x001F8A54
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

		// Token: 0x06007B9E RID: 31646 RVA: 0x001FA8B5 File Offset: 0x001F8AB5
		protected override void InternalStateReset()
		{
			this.user = null;
			base.InternalStateReset();
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x001FA8C4 File Offset: 0x001F8AC4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result;
			using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
			{
				result = base.CreateSession();
			}
			return result;
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x001FA8FC File Offset: 0x001F8AFC
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
					base.RequestName = "MailboxExport";
				}
				if (base.ParameterSetName.Equals("MailboxExportRequest"))
				{
					this.DisallowExportFromPublicFolderMailbox();
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
					this.LocateAndChooseMdb(this.IsArchive ? this.user.ArchiveDatabase : this.user.Database, null, this.Mailbox, null, this.Mailbox, out mdbId, out mdbServerSite);
					base.MdbId = mdbId;
					base.MdbServerSite = mdbServerSite;
					base.Flags = (((this.RemoteHostName == null) ? RequestFlags.IntraOrg : RequestFlags.CrossOrg) | RequestFlags.Push);
				}
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, this.user.Id, true, MRSRequestType.MailboxExport, this.Mailbox, wildcardedSearch);
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x001FAB80 File Offset: 0x001F8D80
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.MailboxExport;
			if (dataObject.WorkloadType == RequestWorkloadType.None)
			{
				if (string.IsNullOrEmpty(this.RemoteHostName))
				{
					dataObject.WorkloadType = RequestWorkloadType.Local;
				}
				else
				{
					dataObject.WorkloadType = RequestWorkloadType.RemotePstExport;
				}
			}
			dataObject.FilePath = this.FilePath.PathName;
			if (this.user != null)
			{
				dataObject.SourceUserId = this.user.Id;
				dataObject.SourceUser = this.user;
			}
			if (base.ParameterSetName.Equals("MailboxExportRequest"))
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
					dataObject.SourceIsArchive = true;
					dataObject.SourceExchangeGuid = this.user.ArchiveGuid;
					dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.user.ArchiveDatabase);
				}
				else
				{
					dataObject.SourceIsArchive = false;
					dataObject.SourceExchangeGuid = this.user.ExchangeGuid;
					dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.user.Database);
				}
				dataObject.SourceAlias = this.user.Alias;
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

		// Token: 0x06007BA2 RID: 31650 RVA: 0x001FAD04 File Offset: 0x001F8F04
		protected override MailboxExportRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new MailboxExportRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
			return null;
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x001FAD54 File Offset: 0x001F8F54
		private void DisallowExportFromPublicFolderMailbox()
		{
			if (this.user != null && this.user.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotExportPstFromPublicFolderMailbox(this.user.Name)), ErrorCategory.InvalidArgument, this.Mailbox);
			}
		}

		// Token: 0x04003D49 RID: 15689
		public const string DefaultMailboxExportName = "MailboxExport";

		// Token: 0x04003D4A RID: 15690
		public const string TaskNoun = "MailboxExportRequest";

		// Token: 0x04003D4B RID: 15691
		public const string ParameterSetMailboxExport = "MailboxExportRequest";

		// Token: 0x04003D4C RID: 15692
		private ADUser user;
	}
}
